using System.Data;
using hotel_management.BLL;
using hotel_management.Domain;

namespace hotel_management;

public sealed class FrontDeskForm : Form
{
    private readonly StayService _stayService = new();
    private readonly PaymentService _paymentService = new();

    private readonly ComboBox _customerCombo = BuildCombo();
    private readonly DateTimePicker _entryDatePicker = BuildDatePicker();
    private readonly DataGridView _stayGrid = BuildGrid();
    private readonly TextBox _staySearchText = new();

    private readonly Label _selectedStayLabel = new() { AutoSize = true, Text = "ยังไม่ได้เลือกการเข้าพัก" };
    private readonly Label _selectedCustomerLabel = new() { AutoSize = true, Text = "-" };
    private readonly ComboBox _roomCombo = BuildCombo();
    private readonly DateTimePicker _checkInPicker = BuildDatePicker();
    private readonly DateTimePicker _checkOutPicker = BuildDatePicker();
    private readonly TextBox _noteText = new();
    private readonly DataGridView _stayDetailGrid = BuildGrid();

    private readonly Label _selectedRoomLabel = new() { AutoSize = true, Text = "-" };
    private readonly TextBox _receiptCustomerText = new() { ReadOnly = true };
    private readonly TextBox _receiptAddressText = new() { ReadOnly = true };
    private readonly TextBox _receiptPhoneText = new() { ReadOnly = true };
    private readonly TextBox _grossAmountText = new() { ReadOnly = true };
    private readonly TextBox _discountText = new();
    private readonly TextBox _netAmountText = new() { ReadOnly = true };
    private readonly DateTimePicker _receiptDatePicker = BuildDatePicker();

    private string _selectedStayId = string.Empty;
    private string _selectedCustomerName = string.Empty;
    private string _selectedCustomerAddress = string.Empty;
    private string _selectedCustomerPhone = string.Empty;
    private int _selectedStaySequence;
    private decimal _selectedGrossAmount;

    public FrontDeskForm()
    {
        InitializeUi();
        LoadAllData();
    }

    private void InitializeUi()
    {
        Text = "ระบบหน้าเคาน์เตอร์";
        Width = 1500;
        Height = 920;
        StartPosition = FormStartPosition.CenterScreen;

        Panel topBar = new() { Dock = DockStyle.Top, Height = 54, Padding = new Padding(12, 10, 12, 10) };
        Label title = new()
        {
            Text = "จัดการการเข้าพักแบบง่าย: 1 Stay มีหลายห้องได้",
            AutoSize = true,
            Left = 12,
            Top = 15,
            Font = new Font(Font.FontFamily, 12, FontStyle.Bold)
        };
        Button openCrudButton = BuildButton("จัดการข้อมูลหลัก", (_, _) => new MasterCrudForm().ShowDialog(this));
        openCrudButton.Left = 980;
        openCrudButton.Top = 10;
        Button openSearchButton = BuildButton("ค้นหา/รายงาน", (_, _) => new SearchForm().ShowDialog(this));
        openSearchButton.Left = 1145;
        openSearchButton.Top = 10;
        topBar.Controls.Add(title);
        topBar.Controls.Add(openCrudButton);
        topBar.Controls.Add(openSearchButton);

        SplitContainer mainSplit = new() { Dock = DockStyle.Fill, SplitterDistance = 430 };
        mainSplit.Panel1.Controls.Add(BuildStayBrowserPanel());
        mainSplit.Panel2.Controls.Add(BuildStayWorkspacePanel());

        Controls.Add(mainSplit);
        Controls.Add(topBar);
    }

    private Control BuildStayBrowserPanel()
    {
        Panel panel = new() { Dock = DockStyle.Fill };

        GroupBox createGroup = BuildGroup("สร้างการเข้าพักใหม่", DockStyle.Top, 150);
        AddLabeledControl(createGroup, "ลูกค้า", _customerCombo, 16, 28, 360);
        AddLabeledControl(createGroup, "วันที่ทำรายการ", _entryDatePicker, 16, 86, 220);
        Button createStayButton = BuildButton("สร้าง Stay", (_, _) => CreateStay());
        createStayButton.Left = 250;
        createStayButton.Top = 105;
        createGroup.Controls.Add(createStayButton);

        GroupBox listGroup = BuildGroup("รายการ Stay", DockStyle.Fill, 0);
        Panel searchPanel = new() { Dock = DockStyle.Top, Height = 62 };
        AddLabeledControl(searchPanel, "ค้นหา Stay / ลูกค้า", _staySearchText, 12, 8, 250);
        Button refreshButton = BuildButton("รีเฟรช", (_, _) => LoadStayList());
        refreshButton.Left = 280;
        refreshButton.Top = 26;
        searchPanel.Controls.Add(refreshButton);
        _staySearchText.TextChanged += (_, _) => ApplyStayFilter();
        _stayGrid.CellClick += (_, e) => SelectStayFromGrid(e.RowIndex);
        listGroup.Controls.Add(_stayGrid);
        listGroup.Controls.Add(searchPanel);

        panel.Controls.Add(listGroup);
        panel.Controls.Add(createGroup);
        return panel;
    }

    private Control BuildStayWorkspacePanel()
    {
        Panel panel = new() { Dock = DockStyle.Fill };

        GroupBox summaryGroup = BuildGroup("Stay ที่เลือก", DockStyle.Top, 84);
        _selectedStayLabel.Left = 16;
        _selectedStayLabel.Top = 28;
        _selectedStayLabel.Font = new Font(Font.FontFamily, 11, FontStyle.Bold);
        _selectedCustomerLabel.Left = 16;
        _selectedCustomerLabel.Top = 52;
        summaryGroup.Controls.Add(_selectedStayLabel);
        summaryGroup.Controls.Add(_selectedCustomerLabel);

        GroupBox addRoomGroup = BuildGroup("เพิ่มห้องใน Stay นี้", DockStyle.Top, 162);
        AddLabeledControl(addRoomGroup, "ห้องพัก", _roomCombo, 16, 28, 240);
        AddLabeledControl(addRoomGroup, "วันที่เข้าพัก", _checkInPicker, 276, 28, 170);
        AddLabeledControl(addRoomGroup, "วันที่ออก", _checkOutPicker, 466, 28, 170);
        AddLabeledControl(addRoomGroup, "หมายเหตุ", _noteText, 16, 88, 430);
        Button checkRoomButton = BuildButton("ค้นหาห้องว่าง", (_, _) => OpenAvailableRoomFinder());
        checkRoomButton.Left = 466;
        checkRoomButton.Top = 106;
        Button addRoomButton = BuildButton("เพิ่มห้องเข้าพัก", (_, _) => AddStayDetail());
        addRoomButton.Left = 642;
        addRoomButton.Top = 106;
        Button deleteRoomButton = BuildButton("ลบรายการห้องที่เลือก", (_, _) => DeleteSelectedStayDetail());
        deleteRoomButton.Left = 818;
        deleteRoomButton.Top = 106;
        addRoomGroup.Controls.Add(checkRoomButton);
        addRoomGroup.Controls.Add(addRoomButton);
        addRoomGroup.Controls.Add(deleteRoomButton);

        GroupBox paymentGroup = BuildGroup("ชำระเงินสำหรับห้องที่เลือก", DockStyle.Bottom, 196);
        _selectedRoomLabel.Left = 16;
        _selectedRoomLabel.Top = 28;
        _selectedRoomLabel.Font = new Font(Font.FontFamily, 10, FontStyle.Bold);
        AddLabeledControl(paymentGroup, "ชื่อลูกค้า", _receiptCustomerText, 16, 50, 260);
        AddLabeledControl(paymentGroup, "ที่อยู่", _receiptAddressText, 296, 50, 280);
        AddLabeledControl(paymentGroup, "โทรศัพท์", _receiptPhoneText, 596, 50, 180);
        AddLabeledControl(paymentGroup, "ยอดก่อนลด", _grossAmountText, 16, 112, 160);
        AddLabeledControl(paymentGroup, "ส่วนลด", _discountText, 196, 112, 140);
        AddLabeledControl(paymentGroup, "ยอดสุทธิ", _netAmountText, 356, 112, 160);
        AddLabeledControl(paymentGroup, "วันที่ใบเสร็จ", _receiptDatePicker, 536, 112, 180);
        Button calculateButton = BuildButton("คำนวณ", (_, _) => UpdateNetAmount());
        calculateButton.Left = 736;
        calculateButton.Top = 130;
        Button payButton = BuildButton("บันทึกชำระเงิน", (_, _) => ProcessPayment());
        payButton.Left = 896;
        payButton.Top = 130;
        paymentGroup.Controls.Add(_selectedRoomLabel);
        paymentGroup.Controls.Add(calculateButton);
        paymentGroup.Controls.Add(payButton);
        _discountText.TextChanged += (_, _) => UpdateNetAmount();

        GroupBox detailGroup = BuildGroup("รายการห้องทั้งหมดใน Stay", DockStyle.Fill, 0);
        _stayDetailGrid.CellClick += (_, e) => SelectStayDetailFromGrid(e.RowIndex);
        detailGroup.Controls.Add(_stayDetailGrid);

        panel.Controls.Add(detailGroup);
        panel.Controls.Add(paymentGroup);
        panel.Controls.Add(addRoomGroup);
        panel.Controls.Add(summaryGroup);
        return panel;
    }

    private void LoadAllData()
    {
        try
        {
            LoadLookups();
            LoadStayList();
            ClearSelection();
        }
        catch (Exception ex)
        {
            ShowError($"โหลดข้อมูลไม่สำเร็จ: {ex.Message}");
        }
    }

    private void LoadLookups()
    {
        BindCombo(_customerCombo, _stayService.GetCustomerLookup(), "MemberLabel", "MemberId");
        BindCombo(_roomCombo, _stayService.GetRoomLookup(), "RoomLabel", "RoomId");
    }

    private void LoadStayList()
    {
        _stayGrid.DataSource = _stayService.GetStays();
        ApplyStayFilter();
        ConfigureStayGrid();
    }

    private void LoadStayDetails()
    {
        _stayDetailGrid.DataSource = string.IsNullOrWhiteSpace(_selectedStayId)
            ? null
            : _stayService.GetStayDetails(_selectedStayId);

        ConfigureStayDetailGrid();
        ResetPaymentPanel();
    }

    private void CreateStay()
    {
        if (!TryGetComboInt(_customerCombo, out int memberId))
        {
            ShowError("เลือกลูกค้าให้ถูกต้อง");
            return;
        }

        Stay stay = new()
        {
            StayId = string.Empty,
            EntryDate = _entryDatePicker.Value.Date,
            MemberId = memberId.ToString()
        };

        ServiceResult<int> result = _stayService.CreateStay(stay);
        ShowResult(result.Success ? ServiceResult.Ok(result.Message) : ServiceResult.Fail(result.Message));
        if (!result.Success)
        {
            return;
        }

        LoadStayList();
        SelectStayById(result.Data);
    }

    private void CheckRoomAvailability()
    {
        if (!EnsureStaySelected()) return;
        if (!TryGetComboInt(_roomCombo, out int roomId))
        {
            ShowError("เลือกห้องพักให้ถูกต้อง");
            return;
        }

        ShowResult(_stayService.CheckRoomAvailability(roomId.ToString(), _checkInPicker.Value, _checkOutPicker.Value));
    }

    private void OpenAvailableRoomFinder()
    {
        if (!EnsureStaySelected()) return;
        if (_checkOutPicker.Value.Date <= _checkInPicker.Value.Date)
        {
            ShowError("วันที่ออกต้องมากกว่าวันที่เข้าพัก");
            return;
        }

        using AvailableRoomPickerForm form = new(_checkInPicker.Value.Date, _checkOutPicker.Value.Date);
        if (form.ShowDialog(this) != DialogResult.OK || form.SelectedRoomId is null)
        {
            return;
        }

        _roomCombo.SelectedValue = form.SelectedRoomId.Value;
        string roomLabel = form.SelectedRoomId.Value.ToString();
        if (_roomCombo.SelectedItem is DataRowView selectedRoom && selectedRoom.Row.Table.Columns.Contains("RoomLabel"))
        {
            roomLabel = selectedRoom["RoomLabel"]?.ToString() ?? roomLabel;
        }

        MessageBox.Show($"เลือกห้อง {roomLabel} แล้ว", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void AddStayDetail()
    {
        if (!EnsureStaySelected()) return;
        if (!TryGetComboInt(_roomCombo, out int roomId))
        {
            ShowError("เลือกห้องพักให้ถูกต้อง");
            return;
        }

        StayDetail detail = new()
        {
            StayId = _selectedStayId,
            CheckInDate = _checkInPicker.Value.Date,
            CheckOutDate = _checkOutPicker.Value.Date,
            Note = _noteText.Text.Trim(),
            PaymentStatus = "0",
            RoomNumber = roomId.ToString()
        };

        ServiceResult result = _stayService.AddStayDetail(detail);
        ShowResult(result);
        if (!result.Success)
        {
            return;
        }

        LoadStayDetails();
    }

    private void DeleteSelectedStayDetail()
    {
        if (!EnsureStaySelected() || _selectedStaySequence <= 0)
        {
            ShowError("เลือกรายการห้องก่อนลบ");
            return;
        }

        ServiceResult result = _stayService.DeleteStayDetail(_selectedStayId, _selectedStaySequence);
        ShowResult(result);
        if (result.Success)
        {
            LoadStayDetails();
        }
    }

    private void ProcessPayment()
    {
        if (!EnsureStaySelected() || _selectedStaySequence <= 0)
        {
            ShowError("เลือกรายการห้องที่จะชำระเงินก่อน");
            return;
        }

        if (!decimal.TryParse(_discountText.Text.Trim(), out decimal discount))
        {
            ShowError("ส่วนลดต้องเป็นตัวเลข");
            return;
        }

        Receipt receipt = new()
        {
            ReceiptId = string.Empty,
            Date = _receiptDatePicker.Value,
            CustomerName = _selectedCustomerName,
            Address = _selectedCustomerAddress,
            Phone = _selectedCustomerPhone,
            Discount = discount
        };

        ServiceResult result = _paymentService.ProcessPayment(receipt, _selectedStayId, _selectedStaySequence, _selectedGrossAmount);
        ShowResult(result);
        if (result.Success)
        {
            LoadStayDetails();
        }
    }

    private void SelectStayFromGrid(int rowIndex)
    {
        if (!TryGetRow(_stayGrid, rowIndex, out DataGridViewRow row))
        {
            return;
        }

        _selectedStayId = row.Cells["StayId"].Value?.ToString() ?? string.Empty;
        _selectedCustomerName = row.Cells["MemberName"].Value?.ToString() ?? string.Empty;
        _selectedCustomerAddress = row.Cells["Address"].Value?.ToString() ?? string.Empty;
        _selectedCustomerPhone = row.Cells["Phone"].Value?.ToString() ?? string.Empty;

        _selectedStayLabel.Text = $"Stay #{_selectedStayId}   วันที่ทำรายการ: {Convert.ToDateTime(row.Cells["EntryDate"].Value):dd/MM/yyyy}";
        _selectedCustomerLabel.Text = $"ลูกค้า: {_selectedCustomerName}";

        _receiptCustomerText.Text = _selectedCustomerName;
        _receiptAddressText.Text = _selectedCustomerAddress;
        _receiptPhoneText.Text = _selectedCustomerPhone;

        LoadStayDetails();
    }

    private void SelectStayDetailFromGrid(int rowIndex)
    {
        if (!TryGetRow(_stayDetailGrid, rowIndex, out DataGridViewRow row))
        {
            return;
        }

        _selectedStaySequence = Convert.ToInt32(row.Cells["StaySequence"].Value ?? 0);
        _selectedGrossAmount = Convert.ToDecimal(row.Cells["GrossAmount"].Value ?? 0m);
        int roomId = Convert.ToInt32(row.Cells["RoomId"].Value ?? 0);
        int paymentStatus = Convert.ToInt32(row.Cells["PaymentStatus"].Value ?? 0);

        _selectedRoomLabel.Text = $"ห้อง {roomId} / ลำดับ {_selectedStaySequence} / สถานะ: {(paymentStatus == 1 ? "ชำระแล้ว" : "ค้างชำระ")}";
        _grossAmountText.Text = _selectedGrossAmount.ToString("N2");
        if (paymentStatus == 1)
        {
            _discountText.Text = "0";
            _netAmountText.Text = _selectedGrossAmount.ToString("N2");
        }
        else
        {
            UpdateNetAmount();
        }
    }

    private void UpdateNetAmount()
    {
        if (!decimal.TryParse(_discountText.Text.Trim(), out decimal discount))
        {
            discount = 0m;
        }

        decimal net = _selectedGrossAmount - discount;
        if (net < 0) net = 0;
        _netAmountText.Text = net.ToString("N2");
    }

    private void ResetPaymentPanel()
    {
        _selectedStaySequence = 0;
        _selectedGrossAmount = 0m;
        _selectedRoomLabel.Text = "-";
        _grossAmountText.Text = string.Empty;
        _discountText.Text = "0";
        _netAmountText.Text = string.Empty;
    }

    private void ClearSelection()
    {
        _selectedStayId = string.Empty;
        _selectedCustomerName = string.Empty;
        _selectedCustomerAddress = string.Empty;
        _selectedCustomerPhone = string.Empty;
        _selectedStayLabel.Text = "ยังไม่ได้เลือกการเข้าพัก";
        _selectedCustomerLabel.Text = "-";
        _stayDetailGrid.DataSource = null;
        ResetPaymentPanel();
    }

    private void ApplyStayFilter()
    {
        if (_stayGrid.DataSource is not DataTable table)
        {
            return;
        }

        string clean = _staySearchText.Text.Trim().Replace("'", "''");
        table.DefaultView.RowFilter = string.IsNullOrWhiteSpace(clean)
            ? string.Empty
            : $"CONVERT([StayId], 'System.String') LIKE '%{clean}%' OR [MemberName] LIKE '%{clean}%'";
    }

    private void ConfigureStayGrid()
    {
        if (_stayGrid.Columns.Count == 0) return;
        if (_stayGrid.Columns.Contains("StayId")) _stayGrid.Columns["StayId"]!.HeaderText = "Stay";
        if (_stayGrid.Columns.Contains("EntryDate")) _stayGrid.Columns["EntryDate"]!.HeaderText = "วันที่";
        if (_stayGrid.Columns.Contains("MemberId")) _stayGrid.Columns["MemberId"]!.HeaderText = "ลูกค้า ID";
        if (_stayGrid.Columns.Contains("MemberName")) _stayGrid.Columns["MemberName"]!.HeaderText = "ชื่อลูกค้า";
        if (_stayGrid.Columns.Contains("Address")) _stayGrid.Columns["Address"]!.Visible = false;
        if (_stayGrid.Columns.Contains("Phone")) _stayGrid.Columns["Phone"]!.Visible = false;
    }

    private void ConfigureStayDetailGrid()
    {
        if (_stayDetailGrid.Columns.Count == 0) return;
        if (_stayDetailGrid.Columns.Contains("StayId")) _stayDetailGrid.Columns["StayId"]!.HeaderText = "Stay";
        if (_stayDetailGrid.Columns.Contains("StaySequence")) _stayDetailGrid.Columns["StaySequence"]!.HeaderText = "ลำดับ";
        if (_stayDetailGrid.Columns.Contains("CheckInDate")) _stayDetailGrid.Columns["CheckInDate"]!.HeaderText = "เข้า";
        if (_stayDetailGrid.Columns.Contains("CheckOutDate")) _stayDetailGrid.Columns["CheckOutDate"]!.HeaderText = "ออก";
        if (_stayDetailGrid.Columns.Contains("Comment")) _stayDetailGrid.Columns["Comment"]!.HeaderText = "หมายเหตุ";
        if (_stayDetailGrid.Columns.Contains("PaymentStatus")) _stayDetailGrid.Columns["PaymentStatus"]!.HeaderText = "ชำระ";
        if (_stayDetailGrid.Columns.Contains("ReceiptId")) _stayDetailGrid.Columns["ReceiptId"]!.HeaderText = "ใบเสร็จ";
        if (_stayDetailGrid.Columns.Contains("RoomId")) _stayDetailGrid.Columns["RoomId"]!.HeaderText = "ห้อง";
        if (_stayDetailGrid.Columns.Contains("PricePerDay")) _stayDetailGrid.Columns["PricePerDay"]!.HeaderText = "ราคาต่อวัน";
        if (_stayDetailGrid.Columns.Contains("GrossAmount")) _stayDetailGrid.Columns["GrossAmount"]!.HeaderText = "รวม";
    }

    private void SelectStayById(int stayId)
    {
        foreach (DataGridViewRow row in _stayGrid.Rows)
        {
            if (row.Cells["StayId"].Value is null) continue;
            if (Convert.ToInt32(row.Cells["StayId"].Value) != stayId) continue;
            row.Selected = true;
            _stayGrid.CurrentCell = row.Cells[0];
            SelectStayFromGrid(row.Index);
            break;
        }
    }

    private bool EnsureStaySelected()
    {
        if (!string.IsNullOrWhiteSpace(_selectedStayId))
        {
            return true;
        }

        ShowError("เลือก Stay ก่อน");
        return false;
    }

    private static GroupBox BuildGroup(string title, DockStyle dock, int height)
    {
        GroupBox group = new() { Text = title, Dock = dock, Padding = new Padding(10) };
        if (height > 0)
        {
            group.Height = height;
        }

        return group;
    }

    private static DataGridView BuildGrid()
    {
        return new DataGridView
        {
            Dock = DockStyle.Fill,
            ReadOnly = true,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            MultiSelect = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            AllowUserToAddRows = false
        };
    }

    private static ComboBox BuildCombo()
    {
        return new ComboBox
        {
            DropDownStyle = ComboBoxStyle.DropDownList,
            Width = 240
        };
    }

    private static DateTimePicker BuildDatePicker()
    {
        return new DateTimePicker
        {
            Format = DateTimePickerFormat.Short,
            Width = 180
        };
    }

    private static Button BuildButton(string text, EventHandler onClick)
    {
        Button button = new() { Text = text, Width = 150, Height = 32 };
        button.Click += onClick;
        return button;
    }

    private static void AddLabeledControl(Control parent, string labelText, Control input, int x, int y, int width)
    {
        Label label = new() { Text = labelText, Left = x, Top = y, Width = width, Height = 20 };
        input.Left = x;
        input.Top = y + 22;
        input.Width = width;
        parent.Controls.Add(label);
        parent.Controls.Add(input);
    }

    private static void BindCombo(ComboBox comboBox, DataTable table, string displayMember, string valueMember)
    {
        comboBox.DataSource = table;
        comboBox.DisplayMember = displayMember;
        comboBox.ValueMember = valueMember;
    }

    private static bool TryGetComboInt(ComboBox comboBox, out int value)
    {
        value = 0;
        if (comboBox.SelectedValue is null || comboBox.SelectedValue is DataRowView)
        {
            return false;
        }

        if (comboBox.SelectedValue is int direct)
        {
            value = direct;
            return true;
        }

        return int.TryParse(comboBox.SelectedValue.ToString(), out value);
    }

    private static bool TryGetRow(DataGridView grid, int rowIndex, out DataGridViewRow row)
    {
        row = null!;
        if (rowIndex < 0 || rowIndex >= grid.Rows.Count)
        {
            return false;
        }

        row = grid.Rows[rowIndex];
        return true;
    }

    private static void ShowError(string message)
    {
        MessageBox.Show(message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    private static void ShowResult(ServiceResult result)
    {
        MessageBox.Show(
            result.Message,
            result.Success ? "สำเร็จ" : "แจ้งเตือน",
            MessageBoxButtons.OK,
            result.Success ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
    }
}

using System.Data;
using hotel_management.BLL;
using hotel_management.Domain;

namespace hotel_management;

public sealed class HotelSystemForm : Form
{
    private readonly RoomService _roomService = new();
    private readonly EquipmentService _equipmentService = new();
    private readonly CustomerService _customerService = new();
    private readonly StayService _stayService = new();
    private readonly PaymentService _paymentService = new();

    private readonly DataGridView _roomGrid = BuildGrid();
    private readonly TextBox _roomSearchText = new();
    private readonly TextBox _roomIdText = new();
    private readonly TextBox _roomFloorText = new();
    private readonly ComboBox _roomLevelCombo = BuildCombo();
    private readonly ComboBox _roomCategoryCombo = BuildCombo();

    private readonly DataGridView _equipmentGrid = BuildGrid();
    private readonly TextBox _equipmentSearchText = new();
    private readonly DataGridView _roomEquipmentGrid = BuildGrid();
    private readonly TextBox _deviceIdText = new();
    private readonly TextBox _deviceNameText = new();
    private readonly TextBox _deviceBrandText = new();
    private readonly TextBox _deviceModelText = new();
    private readonly TextBox _deviceUnitPriceText = new();
    private readonly TextBox _devicePictureText = new();
    private readonly ComboBox _deviceStatusCombo = BuildCombo();
    private readonly ComboBox _deviceCategoryCombo = BuildCombo();
    private readonly ComboBox _assignRoomCombo = BuildCombo();
    private readonly ComboBox _assignDeviceCombo = BuildCombo();

    private readonly DataGridView _customerGrid = BuildGrid();
    private readonly TextBox _customerSearchText = new();
    private readonly TextBox _memberIdText = new();
    private readonly TextBox _nationalIdText = new();
    private readonly TextBox _nameText = new();
    private readonly TextBox _lastNameText = new();
    private readonly TextBox _nationalityText = new();
    private readonly TextBox _addressText = new();
    private readonly TextBox _phoneText = new();
    private readonly TextBox _emailText = new();

    private readonly DataGridView _stayGrid = BuildGrid();
    private readonly TextBox _staySearchText = new();
    private readonly DataGridView _stayDetailGrid = BuildGrid();
    private readonly TextBox _stayIdText = new();
    private readonly DateTimePicker _entryDatePicker = BuildDatePicker();
    private readonly ComboBox _stayMemberCombo = BuildCombo();
    private readonly ComboBox _detailStayCombo = BuildCombo();
    private readonly ComboBox _detailRoomCombo = BuildCombo();
    private readonly DateTimePicker _checkInPicker = BuildDatePicker();
    private readonly DateTimePicker _checkOutPicker = BuildDatePicker();
    private readonly TextBox _detailNoteText = new();

    private readonly DataGridView _pendingPaymentGrid = BuildGrid();
    private readonly TextBox _paymentSearchText = new();
    private readonly DataGridView _receiptGrid = BuildGrid();
    private readonly Label _selectedStayLabel = new() { AutoSize = true, Text = "รายการที่เลือก: -" };
    private readonly TextBox _receiptIdText = new();
    private readonly DateTimePicker _receiptDatePicker = BuildDatePicker();
    private readonly TextBox _receiptCustomerNameText = new();
    private readonly TextBox _receiptAddressText = new();
    private readonly TextBox _receiptPhoneText = new();
    private readonly TextBox _grossText = new() { ReadOnly = true };
    private readonly TextBox _discountText = new();
    private readonly TextBox _netText = new() { ReadOnly = true };

    private string _selectedStayId = string.Empty;
    private int _selectedStaySequenceNo;
    private decimal _selectedGrossAmount;

    public HotelSystemForm()
    {
        InitializeUi();
        ConfigureIdentityInputs();
        LoadAllData();
    }

    private void ConfigureIdentityInputs()
    {
        _deviceIdText.ReadOnly = true;
        _memberIdText.ReadOnly = true;
        _stayIdText.ReadOnly = true;
        _receiptIdText.ReadOnly = true;

        _deviceIdText.Text = "Auto";
        _memberIdText.Text = "Auto";
        _stayIdText.Text = "Auto";
        _receiptIdText.Text = "Auto";
    }

    private void InitializeUi()
    {
        Text = "ระบบจัดการโรงแรม";
        Width = 1450;
        Height = 900;
        StartPosition = FormStartPosition.CenterScreen;

        Panel header = new()
        {
            Dock = DockStyle.Top,
            Height = 46
        };
        Button openCrudButton = BuildButton("ไปหน้า CRUD ทั้งหมด", (_, _) => OpenFeatureFormFromMdi<MasterCrudForm>());
        openCrudButton.Left = 10;
        openCrudButton.Top = 7;
        Button openSearchButton = BuildButton("ไปหน้าค้นหา/รายงาน", (_, _) => OpenFeatureFormFromMdi<SearchForm>());
        openSearchButton.Left = 190;
        openSearchButton.Top = 7;
        header.Controls.Add(openCrudButton);
        header.Controls.Add(openSearchButton);

        TabControl tabs = new() { Dock = DockStyle.Fill };
        tabs.TabPages.Add(CreateRoomsTab());
        tabs.TabPages.Add(CreateEquipmentTab());
        tabs.TabPages.Add(CreateCustomersTab());
        tabs.TabPages.Add(CreateCheckInTab());
        tabs.TabPages.Add(CreatePaymentTab());
        Controls.Add(tabs);
        Controls.Add(header);
    }

    private TabPage CreateRoomsTab()
    {
        TabPage tab = new("ห้องพัก");
        Panel searchPanel = BuildInputPanel(80);
        AddLabeledControl(searchPanel, "ส่วนค้นหา: เลขห้อง/ชั้น/ระดับ/ประเภท", _roomSearchText, 10, 10, 520);
        Button roomClearBtn = BuildButton("ล้างค้นหา", (_, _) => _roomSearchText.Text = string.Empty);
        roomClearBtn.Left = 550;
        roomClearBtn.Top = 32;
        searchPanel.Controls.Add(roomClearBtn);
        _roomSearchText.TextChanged += (_, _) => ApplyRoomFilter();

        Panel input = BuildInputPanel(120);
        AddLabeledControl(input, "ส่วนจัดการ: เลขห้อง", _roomIdText, 10, 10);
        AddLabeledControl(input, "ชั้น", _roomFloorText, 290, 10);
        AddLabeledControl(input, "ระดับห้อง", _roomLevelCombo, 570, 10);
        AddLabeledControl(input, "ประเภทห้อง", _roomCategoryCombo, 850, 10);

        FlowLayoutPanel actions = BuildActions();
        actions.Controls.AddRange(
        [
            BuildButton("เพิ่ม", (_, _) => AddRoom()),
            BuildButton("แก้ไข", (_, _) => UpdateRoom()),
            BuildButton("ลบ", (_, _) => DeleteRoom()),
            BuildButton("รีเฟรช", (_, _) => LoadRoomData())
        ]);

        _roomGrid.CellClick += (_, e) => OnRoomGridSelected(e.RowIndex);
        tab.Controls.Add(_roomGrid);
        tab.Controls.Add(actions);
        tab.Controls.Add(input);
        tab.Controls.Add(searchPanel);
        return tab;
    }

    private TabPage CreateEquipmentTab()
    {
        TabPage tab = new("อุปกรณ์");
        Panel searchPanel = BuildInputPanel(80);
        AddLabeledControl(searchPanel, "ส่วนค้นหา: รหัส/ชื่อ/ยี่ห้อ/รุ่น", _equipmentSearchText, 10, 10, 520);
        Button equipmentClearBtn = BuildButton("ล้างค้นหา", (_, _) => _equipmentSearchText.Text = string.Empty);
        equipmentClearBtn.Left = 550;
        equipmentClearBtn.Top = 32;
        searchPanel.Controls.Add(equipmentClearBtn);
        _equipmentSearchText.TextChanged += (_, _) => ApplyEquipmentFilter();

        Panel input = BuildInputPanel(180);
        AddLabeledControl(input, "ส่วนจัดการ: รหัสอุปกรณ์", _deviceIdText, 10, 10);
        AddLabeledControl(input, "ชื่ออุปกรณ์", _deviceNameText, 280, 10);
        AddLabeledControl(input, "ยี่ห้อ", _deviceBrandText, 550, 10);
        AddLabeledControl(input, "รุ่น", _deviceModelText, 820, 10);
        AddLabeledControl(input, "ราคา/หน่วย", _deviceUnitPriceText, 1090, 10);
        AddLabeledControl(input, "รูปภาพ (ข้อความ)", _devicePictureText, 10, 80, 380);
        AddLabeledControl(input, "สถานะ (0/1)", _deviceStatusCombo, 420, 80);
        AddLabeledControl(input, "ประเภทอุปกรณ์", _deviceCategoryCombo, 690, 80);

        FlowLayoutPanel actions = BuildActions();
        actions.Controls.AddRange(
        [
            BuildButton("เพิ่ม", (_, _) => AddEquipment()),
            BuildButton("แก้ไข", (_, _) => UpdateEquipment()),
            BuildButton("ลบ", (_, _) => DeleteEquipment()),
            BuildButton("รีเฟรช", (_, _) => LoadEquipmentData())
        ]);

        Panel assignPanel = BuildInputPanel(90);
        AddLabeledControl(assignPanel, "จัดการอุปกรณ์ในห้อง: ห้อง", _assignRoomCombo, 10, 10, 280);
        AddLabeledControl(assignPanel, "อุปกรณ์", _assignDeviceCombo, 320, 10, 320);
        Button assignBtn = BuildButton("ผูกเข้าห้อง", (_, _) => AssignEquipmentToRoom());
        assignBtn.Left = 670;
        assignBtn.Top = 34;
        Button removeBtn = BuildButton("ยกเลิกการผูก", (_, _) => RemoveEquipmentFromRoom());
        removeBtn.Left = 820;
        removeBtn.Top = 34;
        Button viewBtn = BuildButton("ดูรายการในห้อง", (_, _) => LoadRoomEquipment());
        viewBtn.Left = 990;
        viewBtn.Top = 34;
        assignPanel.Controls.Add(assignBtn);
        assignPanel.Controls.Add(removeBtn);
        assignPanel.Controls.Add(viewBtn);

        SplitContainer split = new() { Dock = DockStyle.Fill, Orientation = Orientation.Horizontal, SplitterDistance = 220 };
        split.Panel1.Controls.Add(_equipmentGrid);
        split.Panel2.Controls.Add(_roomEquipmentGrid);

        _equipmentGrid.CellClick += (_, e) => OnEquipmentGridSelected(e.RowIndex);

        tab.Controls.Add(split);
        tab.Controls.Add(assignPanel);
        tab.Controls.Add(actions);
        tab.Controls.Add(input);
        tab.Controls.Add(searchPanel);
        return tab;
    }

    private TabPage CreateCustomersTab()
    {
        TabPage tab = new("ลูกค้า");
        Panel searchPanel = BuildInputPanel(80);
        AddLabeledControl(searchPanel, "ส่วนค้นหา: ชื่อ/นามสกุล/โทรศัพท์", _customerSearchText, 10, 10, 520);
        Button customerClearBtn = BuildButton("ล้างค้นหา", (_, _) => _customerSearchText.Text = string.Empty);
        customerClearBtn.Left = 550;
        customerClearBtn.Top = 32;
        searchPanel.Controls.Add(customerClearBtn);

        Panel input = BuildInputPanel(200);
        AddLabeledControl(input, "ส่วนจัดการ: รหัสลูกค้า", _memberIdText, 10, 10);
        AddLabeledControl(input, "เลขบัตร/พาสปอร์ต", _nationalIdText, 280, 10);
        AddLabeledControl(input, "ชื่อ", _nameText, 550, 10);
        AddLabeledControl(input, "นามสกุล", _lastNameText, 820, 10);
        AddLabeledControl(input, "สัญชาติ", _nationalityText, 1090, 10);
        AddLabeledControl(input, "ที่อยู่", _addressText, 10, 80, 380);
        AddLabeledControl(input, "โทรศัพท์", _phoneText, 420, 80);
        AddLabeledControl(input, "อีเมล", _emailText, 690, 80);

        _customerSearchText.TextChanged += (_, _) => LoadCustomerData(_customerSearchText.Text);

        FlowLayoutPanel actions = BuildActions();
        actions.Controls.AddRange(
        [
            BuildButton("เพิ่ม", (_, _) => AddCustomer()),
            BuildButton("แก้ไข", (_, _) => UpdateCustomer()),
            BuildButton("ลบ", (_, _) => DeleteCustomer()),
            BuildButton("รีเฟรช", (_, _) => LoadCustomerData(_customerSearchText.Text))
        ]);

        _customerGrid.CellClick += (_, e) => OnCustomerGridSelected(e.RowIndex);

        tab.Controls.Add(_customerGrid);
        tab.Controls.Add(actions);
        tab.Controls.Add(input);
        tab.Controls.Add(searchPanel);
        return tab;
    }

    private TabPage CreateCheckInTab()
    {
        TabPage tab = new("เช็คอิน");
        Panel searchPanel = BuildInputPanel(80);
        AddLabeledControl(searchPanel, "ส่วนค้นหา: รหัสการเข้าพัก", _staySearchText, 10, 10, 380);
        Button stayClearBtn = BuildButton("ล้างค้นหา", (_, _) => _staySearchText.Text = string.Empty);
        stayClearBtn.Left = 420;
        stayClearBtn.Top = 32;
        searchPanel.Controls.Add(stayClearBtn);
        _staySearchText.TextChanged += (_, _) => ApplyStayFilter();

        Panel stayHeader = BuildInputPanel(100);
        AddLabeledControl(stayHeader, "ส่วนจัดการ: รหัสการเข้าพัก", _stayIdText, 10, 10);
        AddLabeledControl(stayHeader, "วันที่ทำรายการ", _entryDatePicker, 280, 10);
        AddLabeledControl(stayHeader, "ลูกค้า", _stayMemberCombo, 550, 10, 380);
        Button createStayBtn = BuildButton("สร้างการเข้าพัก", (_, _) => CreateStay());
        createStayBtn.Left = 960;
        createStayBtn.Top = 34;
        stayHeader.Controls.Add(createStayBtn);

        Panel detailPanel = BuildInputPanel(120);
        AddLabeledControl(detailPanel, "รหัสการเข้าพัก", _detailStayCombo, 10, 10, 280);
        AddLabeledControl(detailPanel, "ห้องพัก", _detailRoomCombo, 320, 10, 280);
        AddLabeledControl(detailPanel, "วันที่เข้าพัก", _checkInPicker, 630, 10);
        AddLabeledControl(detailPanel, "วันที่ออก", _checkOutPicker, 900, 10);
        AddLabeledControl(detailPanel, "หมายเหตุ", _detailNoteText, 10, 70, 550);
        Button checkBtn = BuildButton("ตรวจสอบห้องว่าง", (_, _) => CheckRoomAvailability());
        checkBtn.Left = 590;
        checkBtn.Top = 86;
        Button addDetailBtn = BuildButton("เพิ่มรายการเข้าพัก", (_, _) => AddStayDetail());
        addDetailBtn.Left = 770;
        addDetailBtn.Top = 86;
        Button refreshDetailBtn = BuildButton("รีเฟรช", (_, _) => LoadStayData());
        refreshDetailBtn.Left = 960;
        refreshDetailBtn.Top = 86;
        detailPanel.Controls.Add(checkBtn);
        detailPanel.Controls.Add(addDetailBtn);
        detailPanel.Controls.Add(refreshDetailBtn);

        SplitContainer split = new() { Dock = DockStyle.Fill, Orientation = Orientation.Horizontal, SplitterDistance = 230 };
        split.Panel1.Controls.Add(_stayGrid);
        split.Panel2.Controls.Add(_stayDetailGrid);
        _stayGrid.CellClick += (_, e) => OnStayGridSelected(e.RowIndex);

        tab.Controls.Add(split);
        tab.Controls.Add(detailPanel);
        tab.Controls.Add(stayHeader);
        tab.Controls.Add(searchPanel);
        return tab;
    }

    private TabPage CreatePaymentTab()
    {
        TabPage tab = new("ชำระเงิน");
        Panel searchPanel = BuildInputPanel(80);
        AddLabeledControl(searchPanel, "ส่วนค้นหา: StayID/ชื่อลูกค้า", _paymentSearchText, 10, 10, 520);
        Button paymentClearBtn = BuildButton("ล้างค้นหา", (_, _) => _paymentSearchText.Text = string.Empty);
        paymentClearBtn.Left = 550;
        paymentClearBtn.Top = 32;
        searchPanel.Controls.Add(paymentClearBtn);
        _paymentSearchText.TextChanged += (_, _) => ApplyPaymentFilter();

        Panel top = BuildInputPanel(190);
        _selectedStayLabel.Left = 10;
        _selectedStayLabel.Top = 10;
        top.Controls.Add(_selectedStayLabel);
        AddLabeledControl(top, "ส่วนจัดการ: เลขที่ใบเสร็จ", _receiptIdText, 10, 35);
        AddLabeledControl(top, "วันที่ใบเสร็จ", _receiptDatePicker, 280, 35);
        AddLabeledControl(top, "ชื่อลูกค้า", _receiptCustomerNameText, 550, 35, 320);
        AddLabeledControl(top, "ที่อยู่", _receiptAddressText, 890, 35, 320);
        AddLabeledControl(top, "โทรศัพท์", _receiptPhoneText, 10, 105);
        AddLabeledControl(top, "ยอดก่อนลด", _grossText, 280, 105);
        AddLabeledControl(top, "ส่วนลด", _discountText, 550, 105);
        AddLabeledControl(top, "ยอดสุทธิ", _netText, 820, 105);

        Button calcNetBtn = BuildButton("คำนวณยอดสุทธิ", (_, _) => CalculateNet());
        calcNetBtn.Left = 1090;
        calcNetBtn.Top = 130;
        Button payBtn = BuildButton("บันทึกการชำระเงิน", (_, _) => ProcessPayment());
        payBtn.Left = 1240;
        payBtn.Top = 130;
        top.Controls.Add(calcNetBtn);
        top.Controls.Add(payBtn);

        SplitContainer split = new() { Dock = DockStyle.Fill, Orientation = Orientation.Horizontal, SplitterDistance = 250 };
        split.Panel1.Controls.Add(_pendingPaymentGrid);
        split.Panel2.Controls.Add(_receiptGrid);
        _pendingPaymentGrid.CellClick += (_, e) => OnPendingPaymentSelected(e.RowIndex);

        tab.Controls.Add(split);
        tab.Controls.Add(top);
        tab.Controls.Add(searchPanel);
        return tab;
    }

    private void LoadAllData()
    {
        try
        {
            _deviceStatusCombo.DataSource = new[]
            {
                new { Key = 0, Value = "0 - ใช้งานได้" },
                new { Key = 1, Value = "1 - ใช้งานไม่ได้" }
            };
            _deviceStatusCombo.DisplayMember = "Value";
            _deviceStatusCombo.ValueMember = "Key";

            LoadRoomLookups();
            LoadRoomData();
            LoadEquipmentLookups();
            LoadEquipmentData();
            LoadCustomerData();
            LoadStayLookups();
            LoadStayData();
            LoadPaymentData();
        }
        catch (Exception ex)
        {
            ShowError($"โหลดข้อมูลจากฐานข้อมูลไม่สำเร็จ: {ex.Message}");
        }
    }

    private void LoadRoomLookups()
    {
        BindCombo(_roomLevelCombo, _roomService.GetLevels(), "LevelName", "LevelId");
        BindCombo(_roomCategoryCombo, _roomService.GetCategories(), "CategoryName", "CategoryId");
    }

    private void LoadRoomData()
    {
        _roomGrid.DataSource = _roomService.GetRooms();
        ApplyRoomFilter();
        LoadEquipmentLookups();
        LoadStayLookups();
    }

    private void LoadEquipmentLookups()
    {
        BindCombo(_deviceCategoryCombo, _equipmentService.GetCategories(), "CategoryName", "CategoryId");
        BindCombo(_assignRoomCombo, _equipmentService.GetRoomLookup(), "RoomLabel", "RoomId");
        BindCombo(_assignDeviceCombo, _equipmentService.GetDeviceLookup(), "DeviceLabel", "DeviceId");
    }

    private void LoadEquipmentData()
    {
        _equipmentGrid.DataSource = _equipmentService.GetEquipments();
        ApplyEquipmentFilter();
        LoadEquipmentLookups();
        LoadRoomEquipment();
    }

    private void LoadRoomEquipment()
    {
        string roomId = _assignRoomCombo.SelectedValue?.ToString() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(roomId))
        {
            _roomEquipmentGrid.DataSource = null;
            return;
        }

        _roomEquipmentGrid.DataSource = _equipmentService.GetEquipmentByRoom(roomId);
    }

    private void LoadCustomerData(string keyword = "")
    {
        _customerGrid.DataSource = _customerService.GetCustomers(keyword);
        LoadStayLookups();
    }

    private void LoadStayLookups()
    {
        BindCombo(_stayMemberCombo, _stayService.GetCustomerLookup(), "MemberLabel", "MemberId");
        BindCombo(_detailStayCombo, _stayService.GetStayLookup(), "StayLabel", "StayId");
        BindCombo(_detailRoomCombo, _stayService.GetRoomLookup(), "RoomLabel", "RoomId");
    }

    private void LoadStayData()
    {
        _stayGrid.DataSource = _stayService.GetStays();
        ApplyStayFilter();
        _stayDetailGrid.DataSource = _stayService.GetStayDetails();
        LoadStayLookups();
        LoadPaymentData();
    }

    private void LoadPaymentData()
    {
        _pendingPaymentGrid.DataSource = _paymentService.GetPendingStayDetails();
        ApplyPaymentFilter();
        _receiptGrid.DataSource = _paymentService.GetReceipts();
    }

    private void AddRoom()
    {
        if (!int.TryParse(_roomFloorText.Text.Trim(), out int floor))
        {
            ShowError("ชั้นต้องเป็นตัวเลขเท่านั้น");
            return;
        }

        Room room = new()
        {
            RoomId = _roomIdText.Text.Trim(),
            Floor = floor,
            LevelId = _roomLevelCombo.SelectedValue?.ToString() ?? string.Empty,
            CategoryId = _roomCategoryCombo.SelectedValue?.ToString() ?? string.Empty
        };

        ShowResult(_roomService.Add(room));
        LoadRoomData();
    }

    private void UpdateRoom()
    {
        if (!int.TryParse(_roomFloorText.Text.Trim(), out int floor))
        {
            ShowError("ชั้นต้องเป็นตัวเลขเท่านั้น");
            return;
        }

        Room room = new()
        {
            RoomId = _roomIdText.Text.Trim(),
            Floor = floor,
            LevelId = _roomLevelCombo.SelectedValue?.ToString() ?? string.Empty,
            CategoryId = _roomCategoryCombo.SelectedValue?.ToString() ?? string.Empty
        };

        ShowResult(_roomService.Update(room));
        LoadRoomData();
    }

    private void DeleteRoom()
    {
        ShowResult(_roomService.Delete(_roomIdText.Text));
        LoadRoomData();
    }

    private void OnRoomGridSelected(int rowIndex)
    {
        if (rowIndex < 0 || rowIndex >= _roomGrid.Rows.Count) return;
        DataGridViewRow row = _roomGrid.Rows[rowIndex];
        _roomIdText.Text = row.Cells["RoomId"].Value?.ToString() ?? string.Empty;
        _roomFloorText.Text = row.Cells["Floor"].Value?.ToString() ?? string.Empty;
        _roomLevelCombo.SelectedValue = row.Cells["LevelId"].Value?.ToString() ?? string.Empty;
        _roomCategoryCombo.SelectedValue = row.Cells["CategoryId"].Value?.ToString() ?? string.Empty;
    }

    private void AddEquipment()
    {
        if (!decimal.TryParse(_deviceUnitPriceText.Text.Trim(), out decimal unitPrice))
        {
            ShowError("ราคา/หน่วย ต้องเป็นตัวเลข");
            return;
        }

        Equipment equipment = BuildEquipmentFromInputs(unitPrice);
        equipment.DeviceId = string.Empty;
        ShowResult(_equipmentService.Add(equipment));
        LoadEquipmentData();
    }

    private void UpdateEquipment()
    {
        if (!decimal.TryParse(_deviceUnitPriceText.Text.Trim(), out decimal unitPrice))
        {
            ShowError("ราคา/หน่วย ต้องเป็นตัวเลข");
            return;
        }

        Equipment equipment = BuildEquipmentFromInputs(unitPrice);
        ShowResult(_equipmentService.Update(equipment));
        LoadEquipmentData();
    }

    private void DeleteEquipment()
    {
        ShowResult(_equipmentService.Delete(_deviceIdText.Text));
        LoadEquipmentData();
    }

    private void AssignEquipmentToRoom()
    {
        string roomId = _assignRoomCombo.SelectedValue?.ToString() ?? string.Empty;
        string deviceId = _assignDeviceCombo.SelectedValue?.ToString() ?? string.Empty;
        ShowResult(_equipmentService.AssignToRoom(roomId, deviceId));
        LoadRoomEquipment();
    }

    private void RemoveEquipmentFromRoom()
    {
        string roomId = _assignRoomCombo.SelectedValue?.ToString() ?? string.Empty;
        string deviceId = _assignDeviceCombo.SelectedValue?.ToString() ?? string.Empty;
        ShowResult(_equipmentService.RemoveFromRoom(roomId, deviceId));
        LoadRoomEquipment();
    }

    private void OnEquipmentGridSelected(int rowIndex)
    {
        if (rowIndex < 0 || rowIndex >= _equipmentGrid.Rows.Count) return;
        DataGridViewRow row = _equipmentGrid.Rows[rowIndex];
        _deviceIdText.Text = row.Cells["DeviceId"].Value?.ToString() ?? string.Empty;
        _deviceNameText.Text = row.Cells["DeviceName"].Value?.ToString() ?? string.Empty;
        _deviceBrandText.Text = row.Cells["Brand"].Value?.ToString() ?? string.Empty;
        _deviceModelText.Text = row.Cells["Model"].Value?.ToString() ?? string.Empty;
        _deviceUnitPriceText.Text = row.Cells["UnitPrice"].Value?.ToString() ?? "0";
        _devicePictureText.Text = row.Cells["DevicePicture"].Value?.ToString() ?? string.Empty;
        _deviceStatusCombo.SelectedValue = Convert.ToInt32(row.Cells["DeviceStatus"].Value ?? 0);
        _deviceCategoryCombo.SelectedValue = row.Cells["CategoryId"].Value?.ToString() ?? string.Empty;
    }

    private Equipment BuildEquipmentFromInputs(decimal unitPrice)
    {
        return new Equipment
        {
            DeviceId = _deviceIdText.Text.Trim(),
            DeviceName = _deviceNameText.Text.Trim(),
            Brand = _deviceBrandText.Text.Trim(),
            Model = _deviceModelText.Text.Trim(),
            UnitPrice = unitPrice,
            DevicePicture = _devicePictureText.Text.Trim(),
            DeviceStatus = Convert.ToInt32(_deviceStatusCombo.SelectedValue),
            CategoryId = _deviceCategoryCombo.SelectedValue?.ToString() ?? string.Empty
        };
    }

    private void AddCustomer()
    {
        Customer customer = BuildCustomerFromInputs();
        customer.MemberId = string.Empty;
        ShowResult(_customerService.Add(customer));
        LoadCustomerData(_customerSearchText.Text);
    }

    private void UpdateCustomer()
    {
        ShowResult(_customerService.Update(BuildCustomerFromInputs()));
        LoadCustomerData(_customerSearchText.Text);
    }

    private void DeleteCustomer()
    {
        ShowResult(_customerService.Delete(_memberIdText.Text));
        LoadCustomerData(_customerSearchText.Text);
    }

    private Customer BuildCustomerFromInputs()
    {
        return new Customer
        {
            MemberId = _memberIdText.Text.Trim(),
            NationalId = _nationalIdText.Text.Trim(),
            Name = _nameText.Text.Trim(),
            LastName = _lastNameText.Text.Trim(),
            Nationality = _nationalityText.Text.Trim(),
            Address = _addressText.Text.Trim(),
            Phone = _phoneText.Text.Trim(),
            Email = _emailText.Text.Trim()
        };
    }

    private void OnCustomerGridSelected(int rowIndex)
    {
        if (rowIndex < 0 || rowIndex >= _customerGrid.Rows.Count) return;
        DataGridViewRow row = _customerGrid.Rows[rowIndex];
        _memberIdText.Text = row.Cells["MemberId"].Value?.ToString() ?? string.Empty;
        _nationalIdText.Text = row.Cells["NationalId"].Value?.ToString() ?? string.Empty;
        _nameText.Text = row.Cells["Name"].Value?.ToString() ?? string.Empty;
        _lastNameText.Text = row.Cells["LastName"].Value?.ToString() ?? string.Empty;
        _nationalityText.Text = row.Cells["Nationality"].Value?.ToString() ?? string.Empty;
        _addressText.Text = row.Cells["Address"].Value?.ToString() ?? string.Empty;
        _phoneText.Text = row.Cells["Phone"].Value?.ToString() ?? string.Empty;
        _emailText.Text = row.Cells["Email"].Value?.ToString() ?? string.Empty;
    }

    private void CreateStay()
    {
        Stay stay = new()
        {
            StayId = string.Empty,
            EntryDate = _entryDatePicker.Value.Date,
            MemberId = _stayMemberCombo.SelectedValue?.ToString() ?? string.Empty
        };
        ServiceResult<int> result = _stayService.CreateStay(stay);
        ShowResult(result.Success ? ServiceResult.Ok(result.Message) : ServiceResult.Fail(result.Message));
        LoadStayData();
    }

    private void CheckRoomAvailability()
    {
        string roomId = _detailRoomCombo.SelectedValue?.ToString() ?? string.Empty;
        ShowResult(_stayService.CheckRoomAvailability(roomId, _checkInPicker.Value, _checkOutPicker.Value));
    }

    private void AddStayDetail()
    {
        StayDetail detail = new()
        {
            StayId = _detailStayCombo.SelectedValue?.ToString() ?? string.Empty,
            CheckInDate = _checkInPicker.Value.Date,
            CheckOutDate = _checkOutPicker.Value.Date,
            Note = _detailNoteText.Text.Trim(),
            PaymentStatus = "0",
            RoomNumber = _detailRoomCombo.SelectedValue?.ToString() ?? string.Empty
        };
        ShowResult(_stayService.AddStayDetail(detail));
        LoadStayData();
    }

    private void OnStayGridSelected(int rowIndex)
    {
        if (rowIndex < 0 || rowIndex >= _stayGrid.Rows.Count) return;
        string stayId = _stayGrid.Rows[rowIndex].Cells["StayId"].Value?.ToString() ?? string.Empty;
        _detailStayCombo.SelectedValue = stayId;
        _stayDetailGrid.DataSource = _stayService.GetStayDetails(stayId);
    }

    private void OnPendingPaymentSelected(int rowIndex)
    {
        if (rowIndex < 0 || rowIndex >= _pendingPaymentGrid.Rows.Count) return;
        DataGridViewRow row = _pendingPaymentGrid.Rows[rowIndex];
        _selectedStayId = row.Cells["StayID"].Value?.ToString() ?? string.Empty;
        _selectedStaySequenceNo = Convert.ToInt32(row.Cells["StaySequence"].Value ?? 0);
        _selectedGrossAmount = Convert.ToDecimal(row.Cells["GrossAmount"].Value ?? 0m);

        _selectedStayLabel.Text = $"รายการที่เลือก: รหัสเข้าพัก {_selectedStayId} / ลำดับ {_selectedStaySequenceNo}";
        _receiptCustomerNameText.Text = row.Cells["CustomerName"].Value?.ToString() ?? string.Empty;
        _receiptAddressText.Text = row.Cells["Address"].Value?.ToString() ?? string.Empty;
        _receiptPhoneText.Text = row.Cells["Phone"].Value?.ToString() ?? string.Empty;
        _grossText.Text = _selectedGrossAmount.ToString("N2");
        CalculateNet();
    }

    private void CalculateNet()
    {
        if (!decimal.TryParse(_discountText.Text.Trim(), out decimal discount))
        {
            discount = 0m;
        }

        decimal net = _selectedGrossAmount - discount;
        if (net < 0) net = 0;
        _netText.Text = net.ToString("N2");
    }

    private void ProcessPayment()
    {
        if (!decimal.TryParse(_discountText.Text.Trim(), out decimal discount))
        {
            ShowError("ส่วนลดต้องเป็นตัวเลข");
            return;
        }

        Receipt receipt = new()
        {
            ReceiptId = string.Empty,
            Date = _receiptDatePicker.Value,
            CustomerName = _receiptCustomerNameText.Text.Trim(),
            Address = _receiptAddressText.Text.Trim(),
            Phone = _receiptPhoneText.Text.Trim(),
            Discount = discount
        };

        ShowResult(_paymentService.ProcessPayment(receipt, _selectedStayId, _selectedStaySequenceNo, _selectedGrossAmount));
        LoadPaymentData();
        LoadStayData();
    }

    private static DataGridView BuildGrid()
    {
        return new DataGridView
        {
            Dock = DockStyle.Fill,
            ReadOnly = true,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            MultiSelect = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        };
    }

    private static ComboBox BuildCombo()
    {
        return new ComboBox
        {
            DropDownStyle = ComboBoxStyle.DropDownList,
            Width = 250
        };
    }

    private static DateTimePicker BuildDatePicker()
    {
        return new DateTimePicker
        {
            Format = DateTimePickerFormat.Short,
            Width = 250
        };
    }

    private static Panel BuildInputPanel(int height)
    {
        return new Panel
        {
            Dock = DockStyle.Top,
            Height = height
        };
    }

    private static FlowLayoutPanel BuildActions()
    {
        return new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            Height = 46,
            Padding = new Padding(8, 6, 8, 6)
        };
    }

    private static Button BuildButton(string text, EventHandler onClick)
    {
        Button button = new()
        {
            Text = text,
            Width = 160,
            Height = 32,
            Margin = new Padding(8, 0, 0, 0)
        };
        button.Click += onClick;
        return button;
    }

    private static void AddLabeledControl(Control parent, string label, Control input, int x, int y, int width = 250)
    {
        Label lbl = new() { Text = label, Left = x, Top = y, Width = width, Height = 20 };
        input.Left = x;
        input.Top = y + 22;
        input.Width = width;
        parent.Controls.Add(lbl);
        parent.Controls.Add(input);
    }

    private static void BindCombo(ComboBox comboBox, DataTable table, string displayMember, string valueMember)
    {
        comboBox.DataSource = table;
        comboBox.DisplayMember = displayMember;
        comboBox.ValueMember = valueMember;
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

    private void ApplyRoomFilter()
    {
        ApplyGridFilter(_roomGrid, _roomSearchText.Text, "RoomId", "Floor", "LevelName", "CategoryName");
    }

    private void ApplyEquipmentFilter()
    {
        ApplyGridFilter(_equipmentGrid, _equipmentSearchText.Text, "DeviceId", "DeviceName", "Brand", "Model");
    }

    private void ApplyStayFilter()
    {
        ApplyGridFilter(_stayGrid, _staySearchText.Text, "StayId", "MemberName", "MemberId");
    }

    private void ApplyPaymentFilter()
    {
        ApplyGridFilter(_pendingPaymentGrid, _paymentSearchText.Text, "StayID", "CustomerName", "MemberId");
    }

    private static void ApplyGridFilter(DataGridView grid, string keyword, params string[] columns)
    {
        if (grid.DataSource is not DataTable table)
        {
            return;
        }

        string clean = keyword.Trim().Replace("'", "''");
        if (string.IsNullOrWhiteSpace(clean))
        {
            table.DefaultView.RowFilter = string.Empty;
            return;
        }

        List<string> conditions = new();
        foreach (string column in columns)
        {
            if (table.Columns.Contains(column))
            {
                conditions.Add($"CONVERT([{column}], 'System.String') LIKE '%{clean}%'");
            }
        }

        table.DefaultView.RowFilter = conditions.Count == 0 ? string.Empty : string.Join(" OR ", conditions);
    }

    private void OpenFeatureFormFromMdi<T>() where T : Form, new()
    {
        if (MdiParent is null)
        {
            return;
        }

        foreach (Form child in MdiParent.MdiChildren)
        {
            if (child is T)
            {
                child.Activate();
                child.WindowState = FormWindowState.Maximized;
                return;
            }
        }

        T form = new()
        {
            MdiParent = MdiParent,
            WindowState = FormWindowState.Maximized
        };
        form.Show();
    }
}

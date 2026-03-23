using hotel_management.BLL;

namespace hotel_management;

public sealed class AvailableRoomPickerForm : Form
{
    private readonly RoomService _roomService = new();

    private readonly DateTimePicker _checkInPicker = BuildDatePicker();
    private readonly DateTimePicker _checkOutPicker = BuildDatePicker();
    private readonly TextBox _keywordText = new();
    private readonly DataGridView _roomGrid = BuildGrid();

    public int? SelectedRoomId { get; private set; }

    public AvailableRoomPickerForm(DateTime checkInDate, DateTime checkOutDate)
    {
        _checkInPicker.Value = checkInDate.Date;
        _checkOutPicker.Value = checkOutDate.Date;

        InitializeUi();
        LoadAvailableRooms();
    }

    private void InitializeUi()
    {
        Text = "ค้นหาห้องว่าง";
        Width = 920;
        Height = 620;
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;

        Panel topPanel = new() { Dock = DockStyle.Top, Height = 96, Padding = new Padding(12) };
        AddLabeledControl(topPanel, "วันที่เข้าพัก", _checkInPicker, 12, 10, 160);
        AddLabeledControl(topPanel, "วันที่ออก", _checkOutPicker, 188, 10, 160);
        AddLabeledControl(topPanel, "คำค้นหา", _keywordText, 364, 10, 220);

        Button searchButton = BuildButton("ค้นหา", (_, _) => LoadAvailableRooms());
        searchButton.Left = 600;
        searchButton.Top = 34;

        Button chooseButton = BuildButton("เลือกห้อง", (_, _) => ChooseRoom());
        chooseButton.Left = 720;
        chooseButton.Top = 34;

        topPanel.Controls.Add(searchButton);
        topPanel.Controls.Add(chooseButton);

        _roomGrid.CellDoubleClick += (_, e) =>
        {
            if (e.RowIndex >= 0)
            {
                ChooseRoom();
            }
        };

        Controls.Add(_roomGrid);
        Controls.Add(topPanel);
    }

    private void LoadAvailableRooms()
    {
        if (_checkOutPicker.Value.Date <= _checkInPicker.Value.Date)
        {
            MessageBox.Show("วันที่ออกต้องมากกว่าวันที่เข้าพัก", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            _roomGrid.DataSource = _roomService.GetAvailableRooms(_checkInPicker.Value.Date, _checkOutPicker.Value.Date, _keywordText.Text);
            ConfigureGrid();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"โหลดห้องว่างไม่สำเร็จ: {ex.Message}", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ChooseRoom()
    {
        if (_roomGrid.CurrentRow?.Cells["RoomId"].Value is null)
        {
            MessageBox.Show("เลือกรายการห้องก่อน", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        SelectedRoomId = Convert.ToInt32(_roomGrid.CurrentRow.Cells["RoomId"].Value);
        DialogResult = DialogResult.OK;
        Close();
    }

    private void ConfigureGrid()
    {
        if (_roomGrid.Columns.Count == 0)
        {
            return;
        }

        if (_roomGrid.Columns.Contains("RoomId")) _roomGrid.Columns["RoomId"]!.HeaderText = "หมายเลขห้อง";
        if (_roomGrid.Columns.Contains("Floor")) _roomGrid.Columns["Floor"]!.HeaderText = "ชั้น";
        if (_roomGrid.Columns.Contains("LevelName")) _roomGrid.Columns["LevelName"]!.HeaderText = "ระดับห้อง";
        if (_roomGrid.Columns.Contains("PricePerDay")) _roomGrid.Columns["PricePerDay"]!.HeaderText = "ราคา/วัน";
        if (_roomGrid.Columns.Contains("CategoryName")) _roomGrid.Columns["CategoryName"]!.HeaderText = "ประเภทห้อง";
        if (_roomGrid.Columns.Contains("RoomStatus")) _roomGrid.Columns["RoomStatus"]!.HeaderText = "สถานะ";
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

    private static DateTimePicker BuildDatePicker()
    {
        return new DateTimePicker
        {
            Format = DateTimePickerFormat.Short,
            Width = 160
        };
    }

    private static Button BuildButton(string text, EventHandler onClick)
    {
        Button button = new() { Text = text, Width = 100, Height = 32 };
        button.Click += onClick;
        return button;
    }

    private static void AddLabeledControl(Control parent, string labelText, Control control, int x, int y, int width)
    {
        Label label = new() { Text = labelText, Left = x, Top = y, Width = width, Height = 20 };
        control.Left = x;
        control.Top = y + 24;
        control.Width = width;
        parent.Controls.Add(label);
        parent.Controls.Add(control);
    }
}

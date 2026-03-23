using System.Data;
using Microsoft.Data.SqlClient;
using hotel_management.DAL;

namespace hotel_management;

public sealed class SearchForm : Form
{
    private readonly DataGridView _roomValidGrid = BuildGrid();
    private readonly TextBox _roomValidSearchText = new();

    private readonly DataGridView _roomDeviceGrid = BuildGrid();
    private readonly ComboBox _roomDeviceRoomCombo = BuildCombo();

    private readonly DataGridView _roomCategoryGrid = BuildGrid();
    private readonly ComboBox _roomCategoryCombo = BuildCombo();

    private readonly DataGridView _staySearchGrid = BuildGrid();
    private readonly TextBox _customerNameSearchText = new();
    private readonly DateTimePicker _entryDateSearchPicker = BuildDatePicker();

    public SearchForm()
    {
        InitializeUi();
        LoadLookups();
        LoadAllSearchData();
    }

    private void InitializeUi()
    {
        Text = "หน้าค้นหาและรายงาน";
        Width = 1300;
        Height = 820;
        StartPosition = FormStartPosition.CenterScreen;

        TabControl tabs = new() { Dock = DockStyle.Fill };
        tabs.TabPages.Add(CreateRoomValidityTab());
        tabs.TabPages.Add(CreateRoomDevicesByRoomTab());
        tabs.TabPages.Add(CreateRoomWithCategoryTab());
        tabs.TabPages.Add(CreateStaySearchTab());
        Controls.Add(tabs);
    }

    private TabPage CreateRoomValidityTab()
    {
        TabPage tab = new("ห้องใช้งานได้หรือไม่");
        Panel input = BuildInputPanel(90);
        AddLabeledControl(input, "ค้นหาห้อง", _roomValidSearchText, 10, 10, 260);
        Button search = BuildButton("ค้นหา", (_, _) => LoadRoomValidity());
        search.Left = 290;
        search.Top = 34;
        Button clear = BuildButton("ล้าง", (_, _) => { _roomValidSearchText.Text = string.Empty; LoadRoomValidity(); });
        clear.Left = 430;
        clear.Top = 34;
        input.Controls.Add(search);
        input.Controls.Add(clear);

        tab.Controls.Add(_roomValidGrid);
        tab.Controls.Add(input);
        return tab;
    }

    private TabPage CreateRoomDevicesByRoomTab()
    {
        TabPage tab = new("อุปกรณ์ในห้องจาก RoomId");
        Panel input = BuildInputPanel(90);
        AddLabeledControl(input, "เลือกห้อง", _roomDeviceRoomCombo, 10, 10, 260);
        Button search = BuildButton("ค้นหา", (_, _) => LoadRoomDevicesByRoom());
        search.Left = 290;
        search.Top = 34;
        input.Controls.Add(search);

        tab.Controls.Add(_roomDeviceGrid);
        tab.Controls.Add(input);
        return tab;
    }

    private TabPage CreateRoomWithCategoryTab()
    {
        TabPage tab = new("ห้องพักตามประเภทห้อง");
        Panel input = BuildInputPanel(90);
        AddLabeledControl(input, "ประเภทห้อง", _roomCategoryCombo, 10, 10, 320);
        Button search = BuildButton("ค้นหา", (_, _) => LoadRoomWithCategory());
        search.Left = 350;
        search.Top = 34;
        Button all = BuildButton("ทั้งหมด", (_, _) => LoadRoomWithCategory(true));
        all.Left = 490;
        all.Top = 34;
        input.Controls.Add(search);
        input.Controls.Add(all);

        tab.Controls.Add(_roomCategoryGrid);
        tab.Controls.Add(input);
        return tab;
    }

    private TabPage CreateStaySearchTab()
    {
        TabPage tab = new("ค้นหา Stay ด้วยชื่อลูกค้าหรือวันที่");
        Panel input = BuildInputPanel(90);
        AddLabeledControl(input, "ชื่อลูกค้า", _customerNameSearchText, 10, 10, 260);
        AddLabeledControl(input, "Entry Date", _entryDateSearchPicker, 290, 10, 220);
        Button byName = BuildButton("ค้นหาด้วยชื่อ", (_, _) => LoadStayByNameOrDate(useName: true));
        byName.Left = 530;
        byName.Top = 34;
        Button byDate = BuildButton("ค้นหาด้วยวันที่", (_, _) => LoadStayByNameOrDate(useName: false));
        byDate.Left = 690;
        byDate.Top = 34;
        Button all = BuildButton("ทั้งหมด", (_, _) => LoadStayByNameOrDate(useName: false, all: true));
        all.Left = 860;
        all.Top = 34;
        input.Controls.Add(byName);
        input.Controls.Add(byDate);
        input.Controls.Add(all);

        tab.Controls.Add(_staySearchGrid);
        tab.Controls.Add(input);
        return tab;
    }

    private void LoadLookups()
    {
        DataTable roomLookup = SqlDataAccess.Query("SELECT RoomId, CAST(RoomId AS NVARCHAR(20)) AS RoomLabel FROM [Rooms] ORDER BY RoomId");
        _roomDeviceRoomCombo.DataSource = roomLookup;
        _roomDeviceRoomCombo.DisplayMember = "RoomLabel";
        _roomDeviceRoomCombo.ValueMember = "RoomId";

        DataTable categoryLookup = SqlDataAccess.Query("SELECT CateGoryId AS CategoryId, CategoryName FROM [Room Categories] ORDER BY CateGoryId");
        _roomCategoryCombo.DataSource = categoryLookup;
        _roomCategoryCombo.DisplayMember = "CategoryName";
        _roomCategoryCombo.ValueMember = "CategoryId";
    }

    private void LoadAllSearchData()
    {
        LoadRoomValidity();
        LoadRoomDevicesByRoom();
        LoadRoomWithCategory(true);
        LoadStayByNameOrDate(useName: false, all: true);
    }

    private void LoadRoomValidity()
    {
        const string sql = """
                           SELECT r.RoomId,
                                  r.Floor,
                                  rl.LevelName,
                                  rc.CategoryName,
                                  CASE
                                      WHEN EXISTS
                                      (
                                          SELECT 1
                                          FROM [Stay List] sl
                                          WHERE sl.RoomId = r.RoomId
                                            AND CAST(GETDATE() AS DATE) >= CAST(sl.CheckInDate AS DATE)
                                            AND CAST(GETDATE() AS DATE) < CAST(sl.CheckOutDate AS DATE)
                                      )
                                      THEN N'ไม่ว่าง'
                                      ELSE N'ว่าง'
                                  END AS RoomStatus
                           FROM [Rooms] r
                           INNER JOIN [Room Levels] rl ON rl.LevelId = r.LevelId
                           INNER JOIN [Room Categories] rc ON rc.CateGoryId = r.CategoryId
                           WHERE @Search = N'' OR CAST(r.RoomId AS NVARCHAR(20)) LIKE @LikeSearch
                           ORDER BY r.RoomId
                           """;
        string keyword = _roomValidSearchText.Text.Trim();
        _roomValidGrid.DataSource = SqlDataAccess.Query(sql,
            new SqlParameter("@Search", keyword),
            new SqlParameter("@LikeSearch", $"%{keyword}%"));
    }

    private void LoadRoomDevicesByRoom()
    {
        if (_roomDeviceRoomCombo.SelectedValue is null) return;
        const string sql = """
                           SELECT rd.RoomId,
                                  rd.DeviceId,
                                  d.DeviceName,
                                  d.Brand,
                                  d.Model,
                                  CASE WHEN d.DeviceStatus = 0 THEN N'ใช้งานได้' ELSE N'ใช้งานไม่ได้' END AS DeviceStatus
                           FROM [Room Devices] rd
                           INNER JOIN [Devices] d ON d.DeviceId = rd.DeviceId
                           WHERE rd.RoomId = @RoomId
                           ORDER BY rd.DeviceId
                           """;
        _roomDeviceGrid.DataSource = SqlDataAccess.Query(sql, new SqlParameter("@RoomId", Convert.ToInt32(_roomDeviceRoomCombo.SelectedValue)));
    }

    private void LoadRoomWithCategory(bool all = false)
    {
        const string sql = """
                           SELECT r.RoomId,
                                  r.Floor,
                                  r.CategoryId,
                                  rc.CategoryName,
                                  r.LevelId
                           FROM [Rooms] r
                           INNER JOIN [Room Categories] rc ON rc.CateGoryId = r.CategoryId
                           WHERE @All = 1 OR r.CategoryId = @CategoryId
                           ORDER BY r.RoomId
                           """;
        int categoryId = _roomCategoryCombo.SelectedValue is null ? 0 : Convert.ToInt32(_roomCategoryCombo.SelectedValue);
        _roomCategoryGrid.DataSource = SqlDataAccess.Query(sql,
            new SqlParameter("@All", all ? 1 : 0),
            new SqlParameter("@CategoryId", categoryId));
    }

    private void LoadStayByNameOrDate(bool useName, bool all = false)
    {
        const string sql = """
                           SELECT s.StayId,
                                  s.EntryDate,
                                  s.MemberId,
                                  m.Name + N' ' + m.LastName AS CustomerName
                           FROM [Stay] s
                           INNER JOIN [Member] m ON m.MemberId = s.MemberId
                           WHERE @All = 1
                              OR (@UseName = 1 AND (m.Name LIKE @NameSearch OR m.LastName LIKE @NameSearch))
                              OR (@UseName = 0 AND CAST(s.EntryDate AS DATE) = CAST(@EntryDate AS DATE))
                           ORDER BY s.StayId DESC
                           """;
        _staySearchGrid.DataSource = SqlDataAccess.Query(sql,
            new SqlParameter("@All", all ? 1 : 0),
            new SqlParameter("@UseName", useName ? 1 : 0),
            new SqlParameter("@NameSearch", $"%{_customerNameSearchText.Text.Trim()}%"),
            new SqlParameter("@EntryDate", _entryDateSearchPicker.Value.Date));
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
        return new Panel { Dock = DockStyle.Top, Height = height };
    }

    private static Button BuildButton(string text, EventHandler onClick)
    {
        Button button = new() { Text = text, Width = 140, Height = 32 };
        button.Click += onClick;
        return button;
    }

    private static void AddLabeledControl(Control parent, string labelText, Control control, int x, int y, int width = 240)
    {
        Label label = new() { Text = labelText, Left = x, Top = y, Width = width, Height = 20 };
        control.Left = x;
        control.Top = y + 24;
        control.Width = width;
        parent.Controls.Add(label);
        parent.Controls.Add(control);
    }
}

using System.Data;
using Microsoft.Data.SqlClient;
using hotel_management.DAL;

namespace hotel_management;

public sealed class MasterCrudForm : Form
{
    private readonly DataGridView _deviceCategoryGrid = BuildGrid();
    private readonly TextBox _deviceCategoryNameText = new();
    private readonly Label _deviceCategoryIdLabel = new() { AutoSize = true, Text = "-" };

    private readonly DataGridView _roomCategoryGrid = BuildGrid();
    private readonly TextBox _roomCategoryNameText = new();
    private readonly Label _roomCategoryIdLabel = new() { AutoSize = true, Text = "-" };

    private readonly DataGridView _roomLevelGrid = BuildGrid();
    private readonly TextBox _roomLevelNameText = new();
    private readonly TextBox _roomLevelPriceText = new();
    private readonly Label _roomLevelIdLabel = new() { AutoSize = true, Text = "-" };

    private readonly DataGridView _roomGrid = BuildGrid();
    private readonly TextBox _roomIdText = new();
    private readonly TextBox _roomFloorText = new();
    private readonly ComboBox _roomLevelCombo = BuildCombo();
    private readonly ComboBox _roomCategoryCombo = BuildCombo();

    private readonly DataGridView _roomDeviceGrid = BuildGrid();
    private readonly ComboBox _roomDeviceRoomCombo = BuildCombo();
    private readonly ComboBox _roomDeviceDeviceCombo = BuildCombo();
    private int? _selectedRoomDeviceRoomId;
    private int? _selectedRoomDeviceDeviceId;

    private readonly DataGridView _stayGrid = BuildGrid();
    private readonly DateTimePicker _stayEntryDatePicker = BuildDatePicker();
    private readonly ComboBox _stayMemberCombo = BuildCombo();
    private readonly Label _stayIdLabel = new() { AutoSize = true, Text = "-" };

    public MasterCrudForm()
    {
        InitializeUi();
        LoadAllData();
        ApplyColumnLengthRules();
    }

    private void InitializeUi()
    {
        Text = "จัดการข้อมูลหลัก (CRUD)";
        Width = 1300;
        Height = 820;
        StartPosition = FormStartPosition.CenterScreen;

        TabControl tabs = new() { Dock = DockStyle.Fill };
        tabs.TabPages.Add(CreateDeviceCategoryTab());
        tabs.TabPages.Add(CreateRoomCategoryTab());
        tabs.TabPages.Add(CreateRoomLevelTab());
        tabs.TabPages.Add(CreateRoomTab());
        tabs.TabPages.Add(CreateRoomDeviceTab());
        tabs.TabPages.Add(CreateStayTab());
        Controls.Add(tabs);
    }

    private TabPage CreateDeviceCategoryTab()
    {
        TabPage tab = new("ประเภทอุปกรณ์");
        Panel input = BuildInputPanel(110);
        AddLabeledControl(input, "รหัสประเภท (Identity)", _deviceCategoryIdLabel, 10, 10);
        AddLabeledControl(input, "ชื่อประเภท", _deviceCategoryNameText, 320, 10, 320);

        FlowLayoutPanel actions = BuildActions();
        actions.Controls.AddRange(
        [
            BuildButton("เพิ่ม", (_, _) => AddDeviceCategory()),
            BuildButton("แก้ไข", (_, _) => UpdateDeviceCategory()),
            BuildButton("ลบ", (_, _) => DeleteDeviceCategory()),
            BuildButton("รีเฟรช", (_, _) => LoadDeviceCategories())
        ]);

        _deviceCategoryGrid.CellClick += (_, e) => OnDeviceCategorySelected(e.RowIndex);
        tab.Controls.Add(_deviceCategoryGrid);
        tab.Controls.Add(actions);
        tab.Controls.Add(input);
        return tab;
    }

    private TabPage CreateRoomCategoryTab()
    {
        TabPage tab = new("ประเภทห้องพัก");
        Panel input = BuildInputPanel(110);
        AddLabeledControl(input, "รหัสประเภท (Identity)", _roomCategoryIdLabel, 10, 10);
        AddLabeledControl(input, "ชื่อประเภทห้อง", _roomCategoryNameText, 320, 10, 320);

        FlowLayoutPanel actions = BuildActions();
        actions.Controls.AddRange(
        [
            BuildButton("เพิ่ม", (_, _) => AddRoomCategory()),
            BuildButton("แก้ไข", (_, _) => UpdateRoomCategory()),
            BuildButton("ลบ", (_, _) => DeleteRoomCategory()),
            BuildButton("รีเฟรช", (_, _) => LoadRoomCategories())
        ]);

        _roomCategoryGrid.CellClick += (_, e) => OnRoomCategorySelected(e.RowIndex);
        tab.Controls.Add(_roomCategoryGrid);
        tab.Controls.Add(actions);
        tab.Controls.Add(input);
        return tab;
    }

    private TabPage CreateRoomLevelTab()
    {
        TabPage tab = new("ระดับห้องพัก");
        Panel input = BuildInputPanel(110);
        AddLabeledControl(input, "รหัสระดับ (Identity)", _roomLevelIdLabel, 10, 10);
        AddLabeledControl(input, "ชื่อระดับ", _roomLevelNameText, 320, 10, 250);
        AddLabeledControl(input, "ราคาต่อวัน", _roomLevelPriceText, 590, 10, 250);

        FlowLayoutPanel actions = BuildActions();
        actions.Controls.AddRange(
        [
            BuildButton("เพิ่ม", (_, _) => AddRoomLevel()),
            BuildButton("แก้ไข", (_, _) => UpdateRoomLevel()),
            BuildButton("ลบ", (_, _) => DeleteRoomLevel()),
            BuildButton("รีเฟรช", (_, _) => LoadRoomLevels())
        ]);

        _roomLevelGrid.CellClick += (_, e) => OnRoomLevelSelected(e.RowIndex);
        tab.Controls.Add(_roomLevelGrid);
        tab.Controls.Add(actions);
        tab.Controls.Add(input);
        return tab;
    }

    private TabPage CreateRoomTab()
    {
        TabPage tab = new("ห้องพัก");
        Panel input = BuildInputPanel(110);
        AddLabeledControl(input, "หมายเลขห้อง", _roomIdText, 10, 10);
        AddLabeledControl(input, "ชั้น", _roomFloorText, 250, 10);
        AddLabeledControl(input, "ระดับห้อง", _roomLevelCombo, 490, 10, 260);
        AddLabeledControl(input, "ประเภทห้อง", _roomCategoryCombo, 770, 10, 260);

        FlowLayoutPanel actions = BuildActions();
        actions.Controls.AddRange(
        [
            BuildButton("เพิ่ม", (_, _) => AddRoom()),
            BuildButton("แก้ไข", (_, _) => UpdateRoom()),
            BuildButton("ลบ", (_, _) => DeleteRoom()),
            BuildButton("รีเฟรช", (_, _) => LoadRooms())
        ]);

        _roomGrid.CellClick += (_, e) => OnRoomSelected(e.RowIndex);
        tab.Controls.Add(_roomGrid);
        tab.Controls.Add(actions);
        tab.Controls.Add(input);
        return tab;
    }

    private TabPage CreateRoomDeviceTab()
    {
        TabPage tab = new("อุปกรณ์ในห้อง");
        Panel input = BuildInputPanel(110);
        AddLabeledControl(input, "ห้องพัก", _roomDeviceRoomCombo, 10, 10, 280);
        AddLabeledControl(input, "อุปกรณ์", _roomDeviceDeviceCombo, 320, 10, 320);

        FlowLayoutPanel actions = BuildActions();
        actions.Controls.AddRange(
        [
            BuildButton("เพิ่ม", (_, _) => AddRoomDevice()),
            BuildButton("แก้ไข", (_, _) => UpdateRoomDevice()),
            BuildButton("ลบ", (_, _) => DeleteRoomDevice()),
            BuildButton("รีเฟรช", (_, _) => LoadRoomDevices())
        ]);

        _roomDeviceGrid.CellClick += (_, e) => OnRoomDeviceSelected(e.RowIndex);
        tab.Controls.Add(_roomDeviceGrid);
        tab.Controls.Add(actions);
        tab.Controls.Add(input);
        return tab;
    }

    private TabPage CreateStayTab()
    {
        TabPage tab = new("การเข้าพัก");
        Panel input = BuildInputPanel(110);
        AddLabeledControl(input, "รหัสการเข้าพัก (Identity)", _stayIdLabel, 10, 10);
        AddLabeledControl(input, "วันที่ทำรายการ", _stayEntryDatePicker, 320, 10, 260);
        AddLabeledControl(input, "ลูกค้า", _stayMemberCombo, 600, 10, 380);

        FlowLayoutPanel actions = BuildActions();
        actions.Controls.AddRange(
        [
            BuildButton("เพิ่ม", (_, _) => AddStay()),
            BuildButton("แก้ไข", (_, _) => UpdateStay()),
            BuildButton("ลบ", (_, _) => DeleteStay()),
            BuildButton("รีเฟรช", (_, _) => LoadStays())
        ]);

        _stayGrid.CellClick += (_, e) => OnStaySelected(e.RowIndex);
        tab.Controls.Add(_stayGrid);
        tab.Controls.Add(actions);
        tab.Controls.Add(input);
        return tab;
    }

    private void LoadAllData()
    {
        try
        {
            LoadLookups();
            LoadDeviceCategories();
            LoadRoomCategories();
            LoadRoomLevels();
            LoadRooms();
            LoadRoomDevices();
            LoadStays();
        }
        catch (Exception ex)
        {
            ShowError($"โหลดข้อมูลไม่สำเร็จ: {ex.Message}");
        }
    }

    private void ApplyColumnLengthRules()
    {
        // Read actual NVARCHAR lengths from DB schema to avoid stale hardcoded limits.
        _deviceCategoryNameText.MaxLength = GetNVarCharLength("Device Category", "CategoryName");
        _roomCategoryNameText.MaxLength = GetNVarCharLength("Room Categories", "CategoryName");
        _roomLevelNameText.MaxLength = GetNVarCharLength("Room Levels", "LevelName");
    }

    private static int GetNVarCharLength(string tableName, string columnName)
    {
        const string sql = """
                           SELECT CHARACTER_MAXIMUM_LENGTH
                           FROM INFORMATION_SCHEMA.COLUMNS
                           WHERE TABLE_NAME = @TableName
                             AND COLUMN_NAME = @ColumnName
                           """;

        object? value = SqlDataAccess.Scalar(sql,
            new SqlParameter("@TableName", tableName),
            new SqlParameter("@ColumnName", columnName));

        // -1 means NVARCHAR(MAX) -> keep WinForms default unlimited (0).
        if (value is null || value == DBNull.Value) return 0;
        int len = Convert.ToInt32(value);
        return len < 0 ? 0 : len;
    }

    private void LoadLookups()
    {
        BindCombo(_roomLevelCombo, SqlDataAccess.Query("SELECT LevelId, LevelName FROM [Room Levels] ORDER BY LevelId"), "LevelName", "LevelId");
        BindCombo(_roomCategoryCombo, SqlDataAccess.Query("SELECT CateGoryId AS CategoryId, CategoryName FROM [Room Categories] ORDER BY CateGoryId"), "CategoryName", "CategoryId");
        BindCombo(_roomDeviceRoomCombo, SqlDataAccess.Query("SELECT RoomId, CAST(RoomId AS NVARCHAR(20)) AS RoomLabel FROM [Rooms] ORDER BY RoomId"), "RoomLabel", "RoomId");
        BindCombo(_roomDeviceDeviceCombo, SqlDataAccess.Query("SELECT DeviceId, DeviceName + N' (' + CAST(DeviceId AS NVARCHAR(20)) + N')' AS DeviceLabel FROM [Devices] ORDER BY DeviceId"), "DeviceLabel", "DeviceId");
        BindCombo(_stayMemberCombo, SqlDataAccess.Query("SELECT MemberId, Name + N' ' + LastName + N' (' + CAST(MemberId AS NVARCHAR(20)) + N')' AS MemberLabel FROM [Member] ORDER BY MemberId"), "MemberLabel", "MemberId");
    }

    private void LoadDeviceCategories()
    {
        _deviceCategoryGrid.DataSource = SqlDataAccess.Query("SELECT CategoryId, CategoryName FROM [Device Category] ORDER BY CategoryId");
    }

    private void LoadRoomCategories()
    {
        _roomCategoryGrid.DataSource = SqlDataAccess.Query("SELECT CateGoryId AS CategoryId, CategoryName FROM [Room Categories] ORDER BY CateGoryId");
        LoadLookups();
    }

    private void LoadRoomLevels()
    {
        _roomLevelGrid.DataSource = SqlDataAccess.Query("SELECT LevelId, LevelName, PricePerDay FROM [Room Levels] ORDER BY LevelId");
        LoadLookups();
    }

    private void LoadRooms()
    {
        const string sql = """
                           SELECT r.RoomId, r.Floor, r.LevelId, rl.LevelName, r.CategoryId, rc.CategoryName
                           FROM [Rooms] r
                           INNER JOIN [Room Levels] rl ON rl.LevelId = r.LevelId
                           INNER JOIN [Room Categories] rc ON rc.CateGoryId = r.CategoryId
                           ORDER BY r.RoomId
                           """;
        _roomGrid.DataSource = SqlDataAccess.Query(sql);
        LoadLookups();
    }

    private void LoadRoomDevices()
    {
        const string sql = """
                           SELECT rd.RoomId, rd.DeviceId, d.DeviceName
                           FROM [Room Devices] rd
                           INNER JOIN [Devices] d ON d.DeviceId = rd.DeviceId
                           ORDER BY rd.RoomId, rd.DeviceId
                           """;
        _roomDeviceGrid.DataSource = SqlDataAccess.Query(sql);
        LoadLookups();
    }

    private void LoadStays()
    {
        const string sql = """
                           SELECT s.StayId, s.EntryDate, s.MemberId, m.Name + N' ' + m.LastName AS CustomerName
                           FROM [Stay] s
                           INNER JOIN [Member] m ON m.MemberId = s.MemberId
                           ORDER BY s.StayId DESC
                           """;
        _stayGrid.DataSource = SqlDataAccess.Query(sql);
        LoadLookups();
    }

    private void AddDeviceCategory()
    {
        if (string.IsNullOrWhiteSpace(_deviceCategoryNameText.Text))
        {
            ShowError("กรอกชื่อประเภทอุปกรณ์");
            return;
        }
        if (!ValidateTextLength(_deviceCategoryNameText.Text, _deviceCategoryNameText.MaxLength, "ชื่อประเภทอุปกรณ์")) return;

        SqlDataAccess.Execute("INSERT INTO [Device Category] (CategoryName) VALUES (@Name)",
            new SqlParameter("@Name", _deviceCategoryNameText.Text.Trim()));
        LoadDeviceCategories();
    }

    private void UpdateDeviceCategory()
    {
        if (!int.TryParse(_deviceCategoryIdLabel.Text, out int id))
        {
            ShowError("เลือกรายการก่อนแก้ไข");
            return;
        }
        if (!ValidateTextLength(_deviceCategoryNameText.Text, _deviceCategoryNameText.MaxLength, "ชื่อประเภทอุปกรณ์")) return;

        SqlDataAccess.Execute("UPDATE [Device Category] SET CategoryName=@Name WHERE CategoryId=@Id",
            new SqlParameter("@Name", _deviceCategoryNameText.Text.Trim()),
            new SqlParameter("@Id", id));
        LoadDeviceCategories();
    }

    private void DeleteDeviceCategory()
    {
        if (!int.TryParse(_deviceCategoryIdLabel.Text, out int id))
        {
            ShowError("เลือกรายการก่อนลบ");
            return;
        }

        try
        {
            SqlDataAccess.Execute("DELETE FROM [Device Category] WHERE CategoryId=@Id", new SqlParameter("@Id", id));
            LoadDeviceCategories();
        }
        catch (SqlException ex) when (ex.Number == 547)
        {
            ShowError("ลบไม่ได้: มีอุปกรณ์ใช้งานประเภทนี้อยู่");
        }
    }

    private void AddRoomCategory()
    {
        if (string.IsNullOrWhiteSpace(_roomCategoryNameText.Text))
        {
            ShowError("กรอกชื่อประเภทห้อง");
            return;
        }
        if (!ValidateTextLength(_roomCategoryNameText.Text, _roomCategoryNameText.MaxLength, "ชื่อประเภทห้อง")) return;

        SqlDataAccess.Execute("INSERT INTO [Room Categories] (CategoryName) VALUES (@Name)",
            new SqlParameter("@Name", _roomCategoryNameText.Text.Trim()));
        LoadRoomCategories();
    }

    private void UpdateRoomCategory()
    {
        if (!int.TryParse(_roomCategoryIdLabel.Text, out int id))
        {
            ShowError("เลือกรายการก่อนแก้ไข");
            return;
        }
        if (!ValidateTextLength(_roomCategoryNameText.Text, _roomCategoryNameText.MaxLength, "ชื่อประเภทห้อง")) return;

        SqlDataAccess.Execute("UPDATE [Room Categories] SET CategoryName=@Name WHERE CateGoryId=@Id",
            new SqlParameter("@Name", _roomCategoryNameText.Text.Trim()),
            new SqlParameter("@Id", id));
        LoadRoomCategories();
    }

    private void DeleteRoomCategory()
    {
        if (!int.TryParse(_roomCategoryIdLabel.Text, out int id))
        {
            ShowError("เลือกรายการก่อนลบ");
            return;
        }

        try
        {
            SqlDataAccess.Execute("DELETE FROM [Room Categories] WHERE CateGoryId=@Id", new SqlParameter("@Id", id));
            LoadRoomCategories();
        }
        catch (SqlException ex) when (ex.Number == 547)
        {
            ShowError("ลบไม่ได้: มีห้องพักที่ผูกประเภทนี้อยู่");
        }
    }

    private void AddRoomLevel()
    {
        if (string.IsNullOrWhiteSpace(_roomLevelNameText.Text) || !decimal.TryParse(_roomLevelPriceText.Text.Trim(), out decimal price))
        {
            ShowError("กรอกชื่อระดับและราคาต่อวันให้ถูกต้อง");
            return;
        }
        if (!ValidateTextLength(_roomLevelNameText.Text, _roomLevelNameText.MaxLength, "ชื่อระดับห้อง")) return;

        SqlDataAccess.Execute("INSERT INTO [Room Levels] (LevelName, PricePerDay) VALUES (@Name,@Price)",
            new SqlParameter("@Name", _roomLevelNameText.Text.Trim()),
            new SqlParameter("@Price", price));
        LoadRoomLevels();
    }

    private void UpdateRoomLevel()
    {
        if (!int.TryParse(_roomLevelIdLabel.Text, out int id) || !decimal.TryParse(_roomLevelPriceText.Text.Trim(), out decimal price))
        {
            ShowError("เลือกรายการและกรอกข้อมูลให้ถูกต้อง");
            return;
        }
        if (!ValidateTextLength(_roomLevelNameText.Text, _roomLevelNameText.MaxLength, "ชื่อระดับห้อง")) return;

        SqlDataAccess.Execute("UPDATE [Room Levels] SET LevelName=@Name, PricePerDay=@Price WHERE LevelId=@Id",
            new SqlParameter("@Name", _roomLevelNameText.Text.Trim()),
            new SqlParameter("@Price", price),
            new SqlParameter("@Id", id));
        LoadRoomLevels();
    }

    private void DeleteRoomLevel()
    {
        if (!int.TryParse(_roomLevelIdLabel.Text, out int id))
        {
            ShowError("เลือกรายการก่อนลบ");
            return;
        }

        try
        {
            SqlDataAccess.Execute("DELETE FROM [Room Levels] WHERE LevelId=@Id", new SqlParameter("@Id", id));
            LoadRoomLevels();
        }
        catch (SqlException ex) when (ex.Number == 547)
        {
            ShowError("ลบไม่ได้: มีห้องพักใช้ระดับห้องนี้อยู่");
        }
    }

    private void AddRoom()
    {
        if (!int.TryParse(_roomIdText.Text.Trim(), out int roomId) || !int.TryParse(_roomFloorText.Text.Trim(), out int floor))
        {
            ShowError("หมายเลขห้องและชั้นต้องเป็นตัวเลข");
            return;
        }

        SqlDataAccess.Execute(
            "INSERT INTO [Rooms] (RoomId, Floor, LevelId, CategoryId) VALUES (@RoomId,@Floor,@LevelId,@CategoryId)",
            new SqlParameter("@RoomId", roomId),
            new SqlParameter("@Floor", floor),
            new SqlParameter("@LevelId", Convert.ToInt32(_roomLevelCombo.SelectedValue)),
            new SqlParameter("@CategoryId", Convert.ToInt32(_roomCategoryCombo.SelectedValue)));
        LoadRooms();
    }

    private void UpdateRoom()
    {
        if (!int.TryParse(_roomIdText.Text.Trim(), out int roomId) || !int.TryParse(_roomFloorText.Text.Trim(), out int floor))
        {
            ShowError("หมายเลขห้องและชั้นต้องเป็นตัวเลข");
            return;
        }

        SqlDataAccess.Execute(
            "UPDATE [Rooms] SET Floor=@Floor, LevelId=@LevelId, CategoryId=@CategoryId WHERE RoomId=@RoomId",
            new SqlParameter("@RoomId", roomId),
            new SqlParameter("@Floor", floor),
            new SqlParameter("@LevelId", Convert.ToInt32(_roomLevelCombo.SelectedValue)),
            new SqlParameter("@CategoryId", Convert.ToInt32(_roomCategoryCombo.SelectedValue)));
        LoadRooms();
    }

    private void DeleteRoom()
    {
        if (!int.TryParse(_roomIdText.Text.Trim(), out int roomId))
        {
            ShowError("เลือกรายการก่อนลบ");
            return;
        }

        try
        {
            SqlDataAccess.Execute("DELETE FROM [Rooms] WHERE RoomId=@RoomId", new SqlParameter("@RoomId", roomId));
            LoadRooms();
        }
        catch (SqlException ex) when (ex.Number == 547)
        {
            ShowError("ลบไม่ได้: ห้องนี้ถูกใช้งานใน Stay หรือ Room Devices");
        }
    }

    private void AddRoomDevice()
    {
        SqlDataAccess.Execute(
            "INSERT INTO [Room Devices] (DeviceId, RoomId) VALUES (@DeviceId,@RoomId)",
            new SqlParameter("@DeviceId", Convert.ToInt32(_roomDeviceDeviceCombo.SelectedValue)),
            new SqlParameter("@RoomId", Convert.ToInt32(_roomDeviceRoomCombo.SelectedValue)));
        LoadRoomDevices();
    }

    private void UpdateRoomDevice()
    {
        if (_selectedRoomDeviceRoomId is null || _selectedRoomDeviceDeviceId is null)
        {
            ShowError("เลือกรายการก่อนแก้ไข");
            return;
        }

        SqlDataAccess.Execute(
            """
            UPDATE [Room Devices]
            SET DeviceId=@NewDeviceId, RoomId=@NewRoomId
            WHERE DeviceId=@OldDeviceId AND RoomId=@OldRoomId
            """,
            new SqlParameter("@NewDeviceId", Convert.ToInt32(_roomDeviceDeviceCombo.SelectedValue)),
            new SqlParameter("@NewRoomId", Convert.ToInt32(_roomDeviceRoomCombo.SelectedValue)),
            new SqlParameter("@OldDeviceId", _selectedRoomDeviceDeviceId.Value),
            new SqlParameter("@OldRoomId", _selectedRoomDeviceRoomId.Value));
        LoadRoomDevices();
    }

    private void DeleteRoomDevice()
    {
        if (_selectedRoomDeviceRoomId is null || _selectedRoomDeviceDeviceId is null)
        {
            ShowError("เลือกรายการก่อนลบ");
            return;
        }

        SqlDataAccess.Execute(
            "DELETE FROM [Room Devices] WHERE DeviceId=@DeviceId AND RoomId=@RoomId",
            new SqlParameter("@DeviceId", _selectedRoomDeviceDeviceId.Value),
            new SqlParameter("@RoomId", _selectedRoomDeviceRoomId.Value));
        LoadRoomDevices();
    }

    private void AddStay()
    {
        SqlDataAccess.Execute(
            "INSERT INTO [Stay] (EntryDate, MemberId) VALUES (@EntryDate,@MemberId)",
            new SqlParameter("@EntryDate", _stayEntryDatePicker.Value),
            new SqlParameter("@MemberId", Convert.ToInt32(_stayMemberCombo.SelectedValue)));
        LoadStays();
    }

    private void UpdateStay()
    {
        if (!int.TryParse(_stayIdLabel.Text, out int stayId))
        {
            ShowError("เลือกรายการก่อนแก้ไข");
            return;
        }

        SqlDataAccess.Execute(
            "UPDATE [Stay] SET EntryDate=@EntryDate, MemberId=@MemberId WHERE StayId=@StayId",
            new SqlParameter("@EntryDate", _stayEntryDatePicker.Value),
            new SqlParameter("@MemberId", Convert.ToInt32(_stayMemberCombo.SelectedValue)),
            new SqlParameter("@StayId", stayId));
        LoadStays();
    }

    private void DeleteStay()
    {
        if (!int.TryParse(_stayIdLabel.Text, out int stayId))
        {
            ShowError("เลือกรายการก่อนลบ");
            return;
        }

        try
        {
            SqlDataAccess.Execute("DELETE FROM [Stay] WHERE StayId=@StayId", new SqlParameter("@StayId", stayId));
            LoadStays();
        }
        catch (SqlException ex) when (ex.Number == 547)
        {
            ShowError("ลบไม่ได้: มี Stay List ที่อ้างอิงรายการนี้อยู่");
        }
    }

    private void OnDeviceCategorySelected(int rowIndex)
    {
        if (!TryGetRow(_deviceCategoryGrid, rowIndex, out DataGridViewRow row)) return;
        _deviceCategoryIdLabel.Text = row.Cells["CategoryId"].Value?.ToString() ?? "-";
        _deviceCategoryNameText.Text = row.Cells["CategoryName"].Value?.ToString() ?? string.Empty;
    }

    private void OnRoomCategorySelected(int rowIndex)
    {
        if (!TryGetRow(_roomCategoryGrid, rowIndex, out DataGridViewRow row)) return;
        _roomCategoryIdLabel.Text = row.Cells["CategoryId"].Value?.ToString() ?? "-";
        _roomCategoryNameText.Text = row.Cells["CategoryName"].Value?.ToString() ?? string.Empty;
    }

    private void OnRoomLevelSelected(int rowIndex)
    {
        if (!TryGetRow(_roomLevelGrid, rowIndex, out DataGridViewRow row)) return;
        _roomLevelIdLabel.Text = row.Cells["LevelId"].Value?.ToString() ?? "-";
        _roomLevelNameText.Text = row.Cells["LevelName"].Value?.ToString() ?? string.Empty;
        _roomLevelPriceText.Text = row.Cells["PricePerDay"].Value?.ToString() ?? string.Empty;
    }

    private void OnRoomSelected(int rowIndex)
    {
        if (!TryGetRow(_roomGrid, rowIndex, out DataGridViewRow row)) return;
        _roomIdText.Text = row.Cells["RoomId"].Value?.ToString() ?? string.Empty;
        _roomFloorText.Text = row.Cells["Floor"].Value?.ToString() ?? string.Empty;
        _roomLevelCombo.SelectedValue = row.Cells["LevelId"].Value ?? 0;
        _roomCategoryCombo.SelectedValue = row.Cells["CategoryId"].Value ?? 0;
    }

    private void OnRoomDeviceSelected(int rowIndex)
    {
        if (!TryGetRow(_roomDeviceGrid, rowIndex, out DataGridViewRow row)) return;
        _selectedRoomDeviceRoomId = Convert.ToInt32(row.Cells["RoomId"].Value ?? 0);
        _selectedRoomDeviceDeviceId = Convert.ToInt32(row.Cells["DeviceId"].Value ?? 0);
        _roomDeviceRoomCombo.SelectedValue = _selectedRoomDeviceRoomId;
        _roomDeviceDeviceCombo.SelectedValue = _selectedRoomDeviceDeviceId;
    }

    private void OnStaySelected(int rowIndex)
    {
        if (!TryGetRow(_stayGrid, rowIndex, out DataGridViewRow row)) return;
        _stayIdLabel.Text = row.Cells["StayId"].Value?.ToString() ?? "-";
        if (DateTime.TryParse(row.Cells["EntryDate"].Value?.ToString(), out DateTime date))
        {
            _stayEntryDatePicker.Value = date;
        }

        _stayMemberCombo.SelectedValue = row.Cells["MemberId"].Value ?? 0;
    }

    private static bool TryGetRow(DataGridView grid, int rowIndex, out DataGridViewRow row)
    {
        row = null!;
        if (rowIndex < 0 || rowIndex >= grid.Rows.Count) return false;
        row = grid.Rows[rowIndex];
        return true;
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
            Width = 240
        };
    }

    private static DateTimePicker BuildDatePicker()
    {
        return new DateTimePicker
        {
            Format = DateTimePickerFormat.Short,
            Width = 240
        };
    }

    private static Panel BuildInputPanel(int height)
    {
        return new Panel { Dock = DockStyle.Top, Height = height };
    }

    private static FlowLayoutPanel BuildActions()
    {
        return new FlowLayoutPanel { Dock = DockStyle.Top, Height = 48, Padding = new Padding(8, 8, 8, 8) };
    }

    private static Button BuildButton(string text, EventHandler onClick)
    {
        Button button = new() { Text = text, Width = 130, Height = 32, Margin = new Padding(8, 0, 0, 0) };
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

    private static void BindCombo(ComboBox combo, DataTable table, string displayMember, string valueMember)
    {
        combo.DataSource = table;
        combo.DisplayMember = displayMember;
        combo.ValueMember = valueMember;
    }

    private static void ShowError(string message)
    {
        MessageBox.Show(message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    private static bool ValidateTextLength(string value, int maxLength, string fieldName)
    {
        string text = value.Trim();
        if (maxLength <= 0 || text.Length <= maxLength)
        {
            return true;
        }

        ShowError($"{fieldName}ยาวเกินกำหนด (สูงสุด {maxLength} ตัวอักษร)");
        return false;
    }
}

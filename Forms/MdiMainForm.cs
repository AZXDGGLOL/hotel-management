namespace hotel_management;

public sealed class MdiMainForm : Form
{
    public MdiMainForm()
    {
        Text = "ระบบจัดการโรงแรม";
        Width = 1500;
        Height = 900;
        IsMdiContainer = true;
        StartPosition = FormStartPosition.CenterScreen;

        MenuStrip menu = new();

        ToolStripMenuItem menuCore = new("ระบบหลัก");
        ToolStripMenuItem openMain = new("เปิดระบบหลักเดิม");

        ToolStripMenuItem menuCrud = new("จัดการข้อมูล");
        ToolStripMenuItem openCrud = new("CRUD ทั้งหมด (แท็บเดียว)");

        ToolStripMenuItem menuSearch = new("ค้นหา/รายงาน");
        ToolStripMenuItem openSearch = new("หน้าค้นหา (แท็บแยก)");

        ToolStripMenuItem menuManage = new("จัดการหน้าจอ");
        ToolStripMenuItem closeAll = new("ปิดหน้าจอทั้งหมด");

        openMain.ShortcutKeys = Keys.Control | Keys.D1;
        openCrud.ShortcutKeys = Keys.Control | Keys.D2;
        openSearch.ShortcutKeys = Keys.Control | Keys.D3;

        openMain.Click += (_, _) => OpenChild<HotelSystemForm>();
        openCrud.Click += (_, _) => OpenChild<MasterCrudForm>();
        openSearch.Click += (_, _) => OpenChild<SearchForm>();
        closeAll.Click += (_, _) => CloseAllChildren();

        menuCore.DropDownItems.Add(openMain);
        menuCrud.DropDownItems.Add(openCrud);
        menuSearch.DropDownItems.Add(openSearch);
        menuManage.DropDownItems.Add(closeAll);

        ToolStripMenuItem menuWindow = new("หน้าต่าง");
        ToolStripMenuItem tileHorizontal = new("เรียงแนวนอน");
        ToolStripMenuItem tileVertical = new("เรียงแนวตั้ง");
        ToolStripMenuItem cascade = new("เรียงซ้อน");
        ToolStripMenuItem maximizeActive = new("ขยายหน้าจอปัจจุบัน");

        tileHorizontal.Click += (_, _) => LayoutMdi(MdiLayout.TileHorizontal);
        tileVertical.Click += (_, _) => LayoutMdi(MdiLayout.TileVertical);
        cascade.Click += (_, _) => LayoutMdi(MdiLayout.Cascade);
        maximizeActive.Click += (_, _) =>
        {
            if (ActiveMdiChild is not null)
            {
                ActiveMdiChild.WindowState = FormWindowState.Maximized;
            }
        };

        menuWindow.DropDownItems.Add(tileHorizontal);
        menuWindow.DropDownItems.Add(tileVertical);
        menuWindow.DropDownItems.Add(cascade);
        menuWindow.DropDownItems.Add(new ToolStripSeparator());
        menuWindow.DropDownItems.Add(maximizeActive);

        ToolStripMenuItem menuApp = new("โปรแกรม");
        ToolStripMenuItem exit = new("ออกจากโปรแกรม");
        exit.ShortcutKeys = Keys.Alt | Keys.F4;
        exit.Click += (_, _) => Close();
        menuApp.DropDownItems.Add(exit);

        menu.Items.Add(menuCore);
        menu.Items.Add(menuCrud);
        menu.Items.Add(menuSearch);
        menu.Items.Add(menuManage);
        menu.Items.Add(menuWindow);
        menu.Items.Add(menuApp);

        MainMenuStrip = menu;
        Controls.Add(menu);

        Shown += (_, _) => OpenChild<HotelSystemForm>();
    }

    private void OpenChild<T>() where T : Form, new()
    {
        foreach (Form child in MdiChildren)
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
            MdiParent = this,
            WindowState = FormWindowState.Maximized
        };
        form.Show();
    }

    private void CloseAllChildren()
    {
        foreach (Form child in MdiChildren.ToArray())
        {
            child.Close();
        }
    }
}

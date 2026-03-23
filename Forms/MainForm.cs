namespace hotel_management {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
            this.IsMdiContainer = true;
        }

        private void searchFunctionToolStripMenuItem_Click(object sender, EventArgs e) {
            OpenChildForm<RoomSearchForm>();
        }

        private void checkInoutToolStripMenuItem_Click(object sender, EventArgs e) {
            OpenChildForm<CheckInOutForm>();
        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e) {
            OpenChildForm<CustomerForm>();
        }

        private void inRoomEqmToolStripMenuItem_Click(object sender, EventArgs e) {
            OpenChildForm<EquipmentInRoomForm>();
        }

        private void eqmTypeToolStripMenuItem_Click(object sender, EventArgs e) {
            OpenChildForm<EquipmentTypeForm>();
        }

        private void roomTierToolStripMenuItem_Click(object sender, EventArgs e) {
            OpenChildForm<RoomLevelForm>();
        }

        private void roomInfoToolStripMenuItem_Click(object sender, EventArgs e) {
            OpenChildForm<RoomInfoForm>();
        }

        private void roomTypeToolStripMenuItem_Click(object sender, EventArgs e) {
            OpenChildForm<RoomTypeForm>();
        }

        private void deviceSearchToolStripMenuItem_Click(object sender, EventArgs e) {
            OpenChildForm<DeviceSearchForm>();
        }

        private T OpenChildForm<T>(Action<T>? configure = null) where T : Form, new() {
            foreach (Form child in MdiChildren.ToArray()) {
                child.Close();
            }

            T form = new T {
                MdiParent = this,
                WindowState = FormWindowState.Maximized
            };

            configure?.Invoke(form);
            form.Show();
            return form;
        }
    }
}

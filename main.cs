namespace hotel_management
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
        }

        private void searchFuntionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new search().Show();

        }

        private void checkinoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new checkin_out().Show();
        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new customer().Show();
        }

        private void inRoomEqmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new equipment_in_room().Show();
        }

        private void eqmTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new equipment_type().Show();
        }

        private void roomTierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new lv_room().Show();
        }

        private void roomInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new room_info().Show();
        }

        private void roomTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new room_type().Show();
        }

        private void deviceSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new device_search().Show();
        }
    }
}

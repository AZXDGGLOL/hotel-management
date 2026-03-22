namespace hotel_management
{
    partial class main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            mainToolStripMenuItem = new ToolStripMenuItem();
            eDITToolStripMenuItem = new ToolStripMenuItem();
            checkinoutToolStripMenuItem = new ToolStripMenuItem();
            customerToolStripMenuItem = new ToolStripMenuItem();
            inRoomEqmToolStripMenuItem = new ToolStripMenuItem();
            eqmTypeToolStripMenuItem = new ToolStripMenuItem();
            roomTierToolStripMenuItem = new ToolStripMenuItem();
            roomInfoToolStripMenuItem = new ToolStripMenuItem();
            roomTypeToolStripMenuItem = new ToolStripMenuItem();
            searchFuntionToolStripMenuItem = new ToolStripMenuItem();
            roomSearchToolStripMenuItem = new ToolStripMenuItem();
            deviceSearchToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { mainToolStripMenuItem, searchFuntionToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1160, 28);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // mainToolStripMenuItem
            // 
            mainToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { eDITToolStripMenuItem });
            mainToolStripMenuItem.Name = "mainToolStripMenuItem";
            mainToolStripMenuItem.Size = new Size(56, 24);
            mainToolStripMenuItem.Text = "main";
            // 
            // eDITToolStripMenuItem
            // 
            eDITToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { checkinoutToolStripMenuItem, customerToolStripMenuItem, inRoomEqmToolStripMenuItem, eqmTypeToolStripMenuItem, roomTierToolStripMenuItem, roomInfoToolStripMenuItem, roomTypeToolStripMenuItem });
            eDITToolStripMenuItem.Name = "eDITToolStripMenuItem";
            eDITToolStripMenuItem.Size = new Size(123, 26);
            eDITToolStripMenuItem.Text = "EDIT";
            // 
            // checkinoutToolStripMenuItem
            // 
            checkinoutToolStripMenuItem.Name = "checkinoutToolStripMenuItem";
            checkinoutToolStripMenuItem.Size = new Size(178, 26);
            checkinoutToolStripMenuItem.Text = "checkin-out";
            checkinoutToolStripMenuItem.Click += checkinoutToolStripMenuItem_Click;
            // 
            // customerToolStripMenuItem
            // 
            customerToolStripMenuItem.Name = "customerToolStripMenuItem";
            customerToolStripMenuItem.Size = new Size(178, 26);
            customerToolStripMenuItem.Text = "customer";
            customerToolStripMenuItem.Click += customerToolStripMenuItem_Click;
            // 
            // inRoomEqmToolStripMenuItem
            // 
            inRoomEqmToolStripMenuItem.Name = "inRoomEqmToolStripMenuItem";
            inRoomEqmToolStripMenuItem.Size = new Size(178, 26);
            inRoomEqmToolStripMenuItem.Text = "in room eqm";
            inRoomEqmToolStripMenuItem.Click += inRoomEqmToolStripMenuItem_Click;
            // 
            // eqmTypeToolStripMenuItem
            // 
            eqmTypeToolStripMenuItem.Name = "eqmTypeToolStripMenuItem";
            eqmTypeToolStripMenuItem.Size = new Size(178, 26);
            eqmTypeToolStripMenuItem.Text = "eqm type";
            eqmTypeToolStripMenuItem.Click += eqmTypeToolStripMenuItem_Click;
            // 
            // roomTierToolStripMenuItem
            // 
            roomTierToolStripMenuItem.Name = "roomTierToolStripMenuItem";
            roomTierToolStripMenuItem.Size = new Size(178, 26);
            roomTierToolStripMenuItem.Text = "room tier";
            roomTierToolStripMenuItem.Click += roomTierToolStripMenuItem_Click;
            // 
            // roomInfoToolStripMenuItem
            // 
            roomInfoToolStripMenuItem.Name = "roomInfoToolStripMenuItem";
            roomInfoToolStripMenuItem.Size = new Size(178, 26);
            roomInfoToolStripMenuItem.Text = "room info";
            roomInfoToolStripMenuItem.Click += roomInfoToolStripMenuItem_Click;
            // 
            // roomTypeToolStripMenuItem
            // 
            roomTypeToolStripMenuItem.Name = "roomTypeToolStripMenuItem";
            roomTypeToolStripMenuItem.Size = new Size(178, 26);
            roomTypeToolStripMenuItem.Text = "room type";
            roomTypeToolStripMenuItem.Click += roomTypeToolStripMenuItem_Click;
            // 
            // searchFuntionToolStripMenuItem
            // 
            searchFuntionToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { roomSearchToolStripMenuItem, deviceSearchToolStripMenuItem });
            searchFuntionToolStripMenuItem.Name = "searchFuntionToolStripMenuItem";
            searchFuntionToolStripMenuItem.Size = new Size(116, 24);
            searchFuntionToolStripMenuItem.Text = "search funtion";
            // 
            // roomSearchToolStripMenuItem
            // 
            roomSearchToolStripMenuItem.Name = "roomSearchToolStripMenuItem";
            roomSearchToolStripMenuItem.Size = new Size(224, 26);
            roomSearchToolStripMenuItem.Text = "room search";
            roomSearchToolStripMenuItem.Click += searchFuntionToolStripMenuItem_Click;
            // 
            // deviceSearchToolStripMenuItem
            // 
            deviceSearchToolStripMenuItem.Name = "deviceSearchToolStripMenuItem";
            deviceSearchToolStripMenuItem.Size = new Size(224, 26);
            deviceSearchToolStripMenuItem.Text = "device search";
            deviceSearchToolStripMenuItem.Click += deviceSearchToolStripMenuItem_Click;
            // 
            // main
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1160, 590);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "main";
            Text = "main";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem mainToolStripMenuItem;
        private ToolStripMenuItem eDITToolStripMenuItem;
        private ToolStripMenuItem checkinoutToolStripMenuItem;
        private ToolStripMenuItem customerToolStripMenuItem;
        private ToolStripMenuItem inRoomEqmToolStripMenuItem;
        private ToolStripMenuItem eqmTypeToolStripMenuItem;
        private ToolStripMenuItem roomTierToolStripMenuItem;
        private ToolStripMenuItem roomInfoToolStripMenuItem;
        private ToolStripMenuItem roomTypeToolStripMenuItem;
        private ToolStripMenuItem searchFuntionToolStripMenuItem;
        private ToolStripMenuItem roomSearchToolStripMenuItem;
        private ToolStripMenuItem deviceSearchToolStripMenuItem;
    }
}

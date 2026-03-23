namespace hotel_management
{
    partial class RoomInfoForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridView1 = new DataGridView();
            btnadd = new Button();
            btnupd = new Button();
            btndel = new Button();
            label1 = new Label();
            txtCategoryid = new TextBox();
            txtLevelID = new TextBox();
            txtFloor = new TextBox();
            txtRoomID = new TextBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            txtsearchbar = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 49);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(765, 520);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // btnadd
            // 
            btnadd.Location = new Point(783, 264);
            btnadd.Name = "btnadd";
            btnadd.Size = new Size(112, 64);
            btnadd.TabIndex = 1;
            btnadd.Text = "Add";
            btnadd.UseVisualStyleBackColor = true;
            btnadd.Click += btnadd_Click;
            // 
            // btnupd
            // 
            btnupd.Location = new Point(901, 264);
            btnupd.Name = "btnupd";
            btnupd.Size = new Size(109, 64);
            btnupd.TabIndex = 2;
            btnupd.Text = "Update";
            btnupd.UseVisualStyleBackColor = true;
            btnupd.Click += btnupd_Click;
            // 
            // btndel
            // 
            btndel.Location = new Point(838, 334);
            btndel.Name = "btndel";
            btndel.Size = new Size(106, 66);
            btndel.TabIndex = 3;
            btndel.Text = "Delete";
            btndel.UseVisualStyleBackColor = true;
            btndel.Click += btndel_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 15);
            label1.Name = "label1";
            label1.Size = new Size(51, 20);
            label1.TabIndex = 5;
            label1.Text = "search";
            // 
            // txtCategoryid
            // 
            txtCategoryid.Location = new Point(783, 231);
            txtCategoryid.Name = "txtCategoryid";
            txtCategoryid.Size = new Size(227, 27);
            txtCategoryid.TabIndex = 7;
            // 
            // txtLevelID
            // 
            txtLevelID.Location = new Point(783, 178);
            txtLevelID.Name = "txtLevelID";
            txtLevelID.Size = new Size(227, 27);
            txtLevelID.TabIndex = 8;
            txtLevelID.TextChanged += txtLevelID_TextChanged;
            // 
            // txtFloor
            // 
            txtFloor.Location = new Point(784, 125);
            txtFloor.Name = "txtFloor";
            txtFloor.Size = new Size(226, 27);
            txtFloor.TabIndex = 9;
            // 
            // txtRoomID
            // 
            txtRoomID.Location = new Point(784, 72);
            txtRoomID.Name = "txtRoomID";
            txtRoomID.Size = new Size(226, 27);
            txtRoomID.TabIndex = 10;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(849, 208);
            label2.Name = "label2";
            label2.Size = new Size(84, 20);
            label2.TabIndex = 11;
            label2.Text = "CategoryID";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(865, 155);
            label3.Name = "label3";
            label3.Size = new Size(58, 20);
            label3.TabIndex = 12;
            label3.Text = "LevelID";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(873, 102);
            label4.Name = "label4";
            label4.Size = new Size(43, 20);
            label4.TabIndex = 13;
            label4.Text = "Floor";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(859, 49);
            label5.Name = "label5";
            label5.Size = new Size(64, 20);
            label5.TabIndex = 14;
            label5.Text = "RoomID";
            // 
            // txtsearchbar
            // 
            txtsearchbar.Location = new Point(88, 12);
            txtsearchbar.Name = "txtsearchbar";
            txtsearchbar.Size = new Size(640, 27);
            txtsearchbar.TabIndex = 6;
            txtsearchbar.TextChanged += txtsearchbar_TextChanged;
            // 
            // RoomInfoForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1022, 581);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(txtRoomID);
            Controls.Add(txtFloor);
            Controls.Add(txtLevelID);
            Controls.Add(txtCategoryid);
            Controls.Add(txtsearchbar);
            Controls.Add(label1);
            Controls.Add(btndel);
            Controls.Add(btnupd);
            Controls.Add(btnadd);
            Controls.Add(dataGridView1);
            Name = "RoomInfoForm";
            Text = "Room Info";
            Load += room_info_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Button btnadd;
        private Button btnupd;
        private Button btndel;
        private Label label1;
        private TextBox txtCategoryid;
        private TextBox txtLevelID;
        private TextBox txtFloor;
        private TextBox txtRoomID;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox txtsearchbar;
    }
}

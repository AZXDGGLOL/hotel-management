namespace hotel_management
{
    partial class EquipmentTypeForm
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
            dgvDevice = new DataGridView();
            btnadd = new Button();
            btndel = new Button();
            btnupd = new Button();
            label1 = new Label();
            txtsearchbar = new TextBox();
            txtname = new TextBox();
            txtid = new TextBox();
            label4 = new Label();
            label5 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvDevice).BeginInit();
            SuspendLayout();
            // 
            // dgvDevice
            // 
            dgvDevice.AllowUserToAddRows = false;
            dgvDevice.AllowUserToDeleteRows = false;
            dgvDevice.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvDevice.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDevice.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDevice.Location = new Point(12, 58);
            dgvDevice.Name = "dgvDevice";
            dgvDevice.ReadOnly = true;
            dgvDevice.RowHeadersWidth = 51;
            dgvDevice.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDevice.Size = new Size(736, 546);
            dgvDevice.TabIndex = 0;
            dgvDevice.CellContentClick += dgvDevice_CellContentClick;
            // 
            // btnadd
            // 
            btnadd.Location = new Point(754, 185);
            btnadd.Name = "btnadd";
            btnadd.Size = new Size(105, 59);
            btnadd.TabIndex = 1;
            btnadd.Text = "Add";
            btnadd.UseVisualStyleBackColor = true;
            btnadd.Click += btnadd_Click;
            // 
            // btndel
            // 
            btndel.Location = new Point(811, 250);
            btndel.Name = "btndel";
            btndel.Size = new Size(104, 59);
            btndel.TabIndex = 2;
            btndel.Text = "Delete";
            btndel.UseVisualStyleBackColor = true;
            btndel.Click += btndel_Click;
            // 
            // btnupd
            // 
            btnupd.Location = new Point(869, 185);
            btnupd.Name = "btnupd";
            btnupd.Size = new Size(105, 59);
            btnupd.TabIndex = 3;
            btnupd.Text = "Update";
            btnupd.UseVisualStyleBackColor = true;
            btnupd.Click += btnupd_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(30, 15);
            label1.Name = "label1";
            label1.Size = new Size(51, 20);
            label1.TabIndex = 5;
            label1.Text = "search";
            // 
            // txtsearchbar
            // 
            txtsearchbar.Location = new Point(102, 12);
            txtsearchbar.Name = "txtsearchbar";
            txtsearchbar.Size = new Size(636, 27);
            txtsearchbar.TabIndex = 6;
            txtsearchbar.TextChanged += txtsearchbar_TextChanged;
            // 
            // txtname
            // 
            txtname.Location = new Point(778, 133);
            txtname.Name = "txtname";
            txtname.Size = new Size(192, 27);
            txtname.TabIndex = 7;
            // 
            // txtid
            // 
            txtid.Location = new Point(778, 70);
            txtid.Name = "txtid";
            txtid.Size = new Size(196, 27);
            txtid.TabIndex = 8;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(819, 47);
            label4.Name = "label4";
            label4.Size = new Size(100, 20);
            label4.TabIndex = 13;
            label4.Text = "device typeID";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(811, 110);
            label5.Name = "label5";
            label5.Size = new Size(126, 20);
            label5.TabIndex = 14;
            label5.Text = "device type name";
            // 
            // EquipmentTypeForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(986, 616);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(txtid);
            Controls.Add(txtname);
            Controls.Add(txtsearchbar);
            Controls.Add(label1);
            Controls.Add(btnupd);
            Controls.Add(btndel);
            Controls.Add(btnadd);
            Controls.Add(dgvDevice);
            Name = "EquipmentTypeForm";
            Text = "Equipment Type";
            Load += equipment_type_Load;
            ((System.ComponentModel.ISupportInitialize)dgvDevice).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvDevice;
        private Button btnadd;
        private Button btndel;
        private Button btnupd;
        private Label label1;
        private TextBox txtsearchbar;
        private TextBox txtname;
        private TextBox txtid;
        private Label label4;
        private Label label5;
    }
}

namespace hotel_management
{
    partial class equipment_in_room
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
            dgvdevice = new DataGridView();
            label1 = new Label();
            txtsearchbar = new TextBox();
            btnadd = new Button();
            btnupd = new Button();
            btndel = new Button();
            txtModel = new TextBox();
            txtbrand = new TextBox();
            txtdevicename = new TextBox();
            txtdeviceID = new TextBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            txtunitprice = new TextBox();
            txtdevicestatus = new TextBox();
            label7 = new Label();
            label8 = new Label();
            picdevice = new PictureBox();
            label9 = new Label();
            txtcategoryID = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dgvdevice).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picdevice).BeginInit();
            SuspendLayout();
            // 
            // dgvdevice
            // 
            dgvdevice.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvdevice.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvdevice.Location = new Point(12, 52);
            dgvdevice.Name = "dgvdevice";
            dgvdevice.RowHeadersWidth = 51;
            dgvdevice.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvdevice.Size = new Size(775, 600);
            dgvdevice.TabIndex = 0;
            dgvdevice.CellContentClick += dgvdevice_CellContentClick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(36, 22);
            label1.Name = "label1";
            label1.Size = new Size(51, 20);
            label1.TabIndex = 1;
            label1.Text = "search";
            // 
            // txtsearchbar
            // 
            txtsearchbar.Location = new Point(112, 15);
            txtsearchbar.Name = "txtsearchbar";
            txtsearchbar.Size = new Size(636, 27);
            txtsearchbar.TabIndex = 2;
            txtsearchbar.TextChanged += txtsearchbar_TextChanged;
            // 
            // btnadd
            // 
            btnadd.Location = new Point(806, 536);
            btnadd.Name = "btnadd";
            btnadd.Size = new Size(116, 56);
            btnadd.TabIndex = 3;
            btnadd.Text = "add";
            btnadd.UseVisualStyleBackColor = true;
            btnadd.Click += btnadd_Click;
            // 
            // btnupd
            // 
            btnupd.Location = new Point(944, 536);
            btnupd.Name = "btnupd";
            btnupd.Size = new Size(111, 56);
            btnupd.TabIndex = 4;
            btnupd.Text = "update";
            btnupd.UseVisualStyleBackColor = true;
            btnupd.Click += btnupd_Click;
            // 
            // btndel
            // 
            btndel.Location = new Point(876, 598);
            btndel.Name = "btndel";
            btndel.Size = new Size(110, 54);
            btndel.TabIndex = 5;
            btndel.Text = "delete";
            btndel.UseVisualStyleBackColor = true;
            btndel.Click += btndel_Click;
            // 
            // txtModel
            // 
            txtModel.Location = new Point(836, 249);
            txtModel.Name = "txtModel";
            txtModel.Size = new Size(198, 27);
            txtModel.TabIndex = 7;
            // 
            // txtbrand
            // 
            txtbrand.Location = new Point(836, 196);
            txtbrand.Name = "txtbrand";
            txtbrand.Size = new Size(198, 27);
            txtbrand.TabIndex = 8;
            // 
            // txtdevicename
            // 
            txtdevicename.Location = new Point(836, 143);
            txtdevicename.Name = "txtdevicename";
            txtdevicename.Size = new Size(198, 27);
            txtdevicename.TabIndex = 9;
            // 
            // txtdeviceID
            // 
            txtdeviceID.Location = new Point(836, 90);
            txtdeviceID.Name = "txtdeviceID";
            txtdeviceID.Size = new Size(198, 27);
            txtdeviceID.TabIndex = 10;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(892, 67);
            label2.Name = "label2";
            label2.Size = new Size(67, 20);
            label2.TabIndex = 11;
            label2.Text = "deviceID";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(888, 120);
            label3.Name = "label3";
            label3.Size = new Size(89, 20);
            label3.TabIndex = 12;
            label3.Text = "devicename";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(904, 173);
            label4.Name = "label4";
            label4.Size = new Size(48, 20);
            label4.TabIndex = 13;
            label4.Text = "brand";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(902, 226);
            label5.Name = "label5";
            label5.Size = new Size(52, 20);
            label5.TabIndex = 14;
            label5.Text = "model";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(892, 279);
            label6.Name = "label6";
            label6.Size = new Size(67, 20);
            label6.TabIndex = 15;
            label6.Text = "unitprice";
            // 
            // txtunitprice
            // 
            txtunitprice.Location = new Point(836, 302);
            txtunitprice.Name = "txtunitprice";
            txtunitprice.Size = new Size(198, 27);
            txtunitprice.TabIndex = 16;
            // 
            // txtdevicestatus
            // 
            txtdevicestatus.Location = new Point(836, 355);
            txtdevicestatus.Name = "txtdevicestatus";
            txtdevicestatus.Size = new Size(198, 27);
            txtdevicestatus.TabIndex = 17;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(888, 332);
            label7.Name = "label7";
            label7.Size = new Size(90, 20);
            label7.TabIndex = 18;
            label7.Text = "devicestatus";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(888, 385);
            label8.Name = "label8";
            label8.Size = new Size(98, 20);
            label8.TabIndex = 19;
            label8.Text = "devicepicture";
            // 
            // picdevice
            // 
            picdevice.Location = new Point(806, 408);
            picdevice.Name = "picdevice";
            picdevice.Size = new Size(249, 122);
            picdevice.TabIndex = 20;
            picdevice.TabStop = false;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(888, 14);
            label9.Name = "label9";
            label9.Size = new Size(82, 20);
            label9.TabIndex = 21;
            label9.Text = "categoryID";
            // 
            // txtcategoryID
            // 
            txtcategoryID.Location = new Point(836, 37);
            txtcategoryID.Name = "txtcategoryID";
            txtcategoryID.Size = new Size(198, 27);
            txtcategoryID.TabIndex = 22;
            // 
            // equipment_in_room
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1067, 664);
            Controls.Add(txtcategoryID);
            Controls.Add(label9);
            Controls.Add(picdevice);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(txtdevicestatus);
            Controls.Add(txtunitprice);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(txtdeviceID);
            Controls.Add(txtdevicename);
            Controls.Add(txtbrand);
            Controls.Add(txtModel);
            Controls.Add(btndel);
            Controls.Add(btnupd);
            Controls.Add(btnadd);
            Controls.Add(txtsearchbar);
            Controls.Add(label1);
            Controls.Add(dgvdevice);
            Name = "equipment_in_room";
            Text = "equipment_in_room";
            Load += equipment_in_room_Load;
            ((System.ComponentModel.ISupportInitialize)dgvdevice).EndInit();
            ((System.ComponentModel.ISupportInitialize)picdevice).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvdevice;
        private Label label1;
        private TextBox txtsearchbar;
        private Button btnadd;
        private Button btnupd;
        private Button btndel;
        private TextBox txtModel;
        private TextBox txtbrand;
        private TextBox txtdevicename;
        private TextBox txtdeviceID;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextBox txtunitprice;
        private TextBox txtdevicestatus;
        private Label label7;
        private Label label8;
        private PictureBox picdevice;
        private Label label9;
        private TextBox txtcategoryID;
    }
}
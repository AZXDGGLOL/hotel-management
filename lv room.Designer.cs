namespace hotel_management
{
    partial class lv_room
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
            txtsearchbar = new TextBox();
            txtpriceperday = new TextBox();
            txtLVname = new TextBox();
            txtLVID = new TextBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 50);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(620, 440);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // btnadd
            // 
            btnadd.Location = new Point(638, 225);
            btnadd.Name = "btnadd";
            btnadd.Size = new Size(99, 53);
            btnadd.TabIndex = 1;
            btnadd.Text = "Add";
            btnadd.UseVisualStyleBackColor = true;
            btnadd.Click += btnadd_Click;
            // 
            // btnupd
            // 
            btnupd.Location = new Point(743, 225);
            btnupd.Name = "btnupd";
            btnupd.Size = new Size(103, 53);
            btnupd.TabIndex = 2;
            btnupd.Text = "Update";
            btnupd.UseVisualStyleBackColor = true;
            btnupd.Click += btnupd_Click;
            // 
            // btndel
            // 
            btndel.Location = new Point(690, 284);
            btndel.Name = "btndel";
            btndel.Size = new Size(98, 51);
            btndel.TabIndex = 3;
            btndel.Text = "delete";
            btndel.UseVisualStyleBackColor = true;
            btndel.Click += btndel_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(51, 20);
            label1.TabIndex = 5;
            label1.Text = "search";
            // 
            // txtsearchbar
            // 
            txtsearchbar.Location = new Point(87, 6);
            txtsearchbar.Name = "txtsearchbar";
            txtsearchbar.Size = new Size(545, 27);
            txtsearchbar.TabIndex = 6;
            txtsearchbar.TextChanged += textBox1_TextChanged;
            // 
            // txtpriceperday
            // 
            txtpriceperday.Location = new Point(638, 192);
            txtpriceperday.Name = "txtpriceperday";
            txtpriceperday.Size = new Size(208, 27);
            txtpriceperday.TabIndex = 8;
            // 
            // txtLVname
            // 
            txtLVname.Location = new Point(638, 139);
            txtLVname.Name = "txtLVname";
            txtLVname.Size = new Size(208, 27);
            txtLVname.TabIndex = 9;
            // 
            // txtLVID
            // 
            txtLVID.Location = new Point(638, 86);
            txtLVID.Name = "txtLVID";
            txtLVID.Size = new Size(214, 27);
            txtLVID.TabIndex = 10;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(713, 63);
            label2.Name = "label2";
            label2.Size = new Size(37, 20);
            label2.TabIndex = 11;
            label2.Text = "LvID";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(704, 116);
            label3.Name = "label3";
            label3.Size = new Size(59, 20);
            label3.TabIndex = 12;
            label3.Text = "Lvname";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(690, 169);
            label4.Name = "label4";
            label4.Size = new Size(88, 20);
            label4.TabIndex = 13;
            label4.Text = "priceperday";
            // 
            // lv_room
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(858, 502);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(txtLVID);
            Controls.Add(txtLVname);
            Controls.Add(txtpriceperday);
            Controls.Add(txtsearchbar);
            Controls.Add(label1);
            Controls.Add(btndel);
            Controls.Add(btnupd);
            Controls.Add(btnadd);
            Controls.Add(dataGridView1);
            Name = "lv_room";
            Text = "lv_room";
            Load += lv_room_Load;
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
        private TextBox txtsearchbar;
        private TextBox txtpriceperday;
        private TextBox txtLVname;
        private TextBox txtLVID;
        private Label label2;
        private Label label3;
        private Label label4;
    }
}
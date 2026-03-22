namespace hotel_management
{
    partial class room_type
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
            dgvroom = new DataGridView();
            label1 = new Label();
            txtsearchbar = new TextBox();
            btnadd = new Button();
            btnupd = new Button();
            txtCategoryname = new TextBox();
            txtCategoryID = new TextBox();
            label2 = new Label();
            label3 = new Label();
            btndel = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvroom).BeginInit();
            SuspendLayout();
            // 
            // dgvroom
            // 
            dgvroom.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvroom.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvroom.Location = new Point(12, 56);
            dgvroom.Name = "dgvroom";
            dgvroom.RowHeadersWidth = 51;
            dgvroom.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvroom.Size = new Size(740, 541);
            dgvroom.TabIndex = 0;
            dgvroom.CellContentClick += dgvroom_CellContentClick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(23, 19);
            label1.Name = "label1";
            label1.Size = new Size(51, 20);
            label1.TabIndex = 1;
            label1.Text = "search";
            // 
            // txtsearchbar
            // 
            txtsearchbar.Location = new Point(91, 19);
            txtsearchbar.Name = "txtsearchbar";
            txtsearchbar.Size = new Size(629, 27);
            txtsearchbar.TabIndex = 2;
            txtsearchbar.TextChanged += txtsearchbar_TextChanged;
            // 
            // btnadd
            // 
            btnadd.Location = new Point(758, 142);
            btnadd.Name = "btnadd";
            btnadd.Size = new Size(111, 59);
            btnadd.TabIndex = 3;
            btnadd.Text = "Add";
            btnadd.UseVisualStyleBackColor = true;
            btnadd.Click += btnadd_Click;
            // 
            // btnupd
            // 
            btnupd.Location = new Point(875, 142);
            btnupd.Name = "btnupd";
            btnupd.Size = new Size(106, 59);
            btnupd.TabIndex = 4;
            btnupd.Text = "Update";
            btnupd.UseVisualStyleBackColor = true;
            btnupd.Click += btnupd_Click;
            // 
            // txtCategoryname
            // 
            txtCategoryname.Location = new Point(758, 109);
            txtCategoryname.Name = "txtCategoryname";
            txtCategoryname.Size = new Size(223, 27);
            txtCategoryname.TabIndex = 9;
            // 
            // txtCategoryID
            // 
            txtCategoryID.Location = new Point(758, 56);
            txtCategoryID.Name = "txtCategoryID";
            txtCategoryID.Size = new Size(223, 27);
            txtCategoryID.TabIndex = 10;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(823, 33);
            label2.Name = "label2";
            label2.Size = new Size(85, 20);
            label2.TabIndex = 11;
            label2.Text = "CateGoryID";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(811, 86);
            label3.Name = "label3";
            label3.Size = new Size(109, 20);
            label3.TabIndex = 12;
            label3.Text = "CategoryName";
            // 
            // btndel
            // 
            btndel.Location = new Point(823, 207);
            btndel.Name = "btndel";
            btndel.Size = new Size(97, 60);
            btndel.TabIndex = 5;
            btndel.Text = "Delete";
            btndel.UseVisualStyleBackColor = true;
            btndel.Click += btndel_Click;
            // 
            // room_type
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(993, 609);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(txtCategoryID);
            Controls.Add(txtCategoryname);
            Controls.Add(btndel);
            Controls.Add(btnupd);
            Controls.Add(btnadd);
            Controls.Add(txtsearchbar);
            Controls.Add(label1);
            Controls.Add(dgvroom);
            Name = "room_type";
            Text = "room_type";
            Load += room_type_Load;
            ((System.ComponentModel.ISupportInitialize)dgvroom).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvroom;
        private Label label1;
        private TextBox txtsearchbar;
        private Button btnadd;
        private Button btnupd;
        private TextBox txtCategoryname;
        private TextBox txtCategoryID;
        private Label label2;
        private Label label3;
        private Button btndel;
    }
}
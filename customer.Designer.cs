namespace hotel_management
{
    partial class customer
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
            txtsearchbar = new TextBox();
            label1 = new Label();
            txtlast = new TextBox();
            txtFirst = new TextBox();
            txtNationID = new TextBox();
            txtMemberID = new TextBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            txtNationlity = new TextBox();
            txtAddress = new TextBox();
            txtEmail = new TextBox();
            txtPhone = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 39);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(716, 570);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // btnadd
            // 
            btnadd.Location = new Point(734, 433);
            btnadd.Name = "btnadd";
            btnadd.Size = new Size(102, 50);
            btnadd.TabIndex = 1;
            btnadd.Text = "Add";
            btnadd.UseVisualStyleBackColor = true;
            btnadd.Click += btnadd_Click;
            // 
            // btnupd
            // 
            btnupd.Location = new Point(851, 433);
            btnupd.Name = "btnupd";
            btnupd.Size = new Size(108, 50);
            btnupd.TabIndex = 2;
            btnupd.Text = "Update";
            btnupd.UseVisualStyleBackColor = true;
            btnupd.Click += btnupd_Click;
            // 
            // btndel
            // 
            btndel.Location = new Point(793, 489);
            btndel.Name = "btndel";
            btndel.Size = new Size(108, 56);
            btndel.TabIndex = 3;
            btndel.Text = "Delete";
            btndel.UseVisualStyleBackColor = true;
            btndel.Click += btndel_Click;
            // 
            // txtsearchbar
            // 
            txtsearchbar.Location = new Point(69, 6);
            txtsearchbar.Name = "txtsearchbar";
            txtsearchbar.Size = new Size(595, 27);
            txtsearchbar.TabIndex = 5;
            txtsearchbar.TextChanged += txtsearchbar_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 12);
            label1.Name = "label1";
            label1.Size = new Size(51, 20);
            label1.TabIndex = 6;
            label1.Text = "search";
            // 
            // txtlast
            // 
            txtlast.Location = new Point(734, 189);
            txtlast.Name = "txtlast";
            txtlast.Size = new Size(225, 27);
            txtlast.TabIndex = 7;
            // 
            // txtFirst
            // 
            txtFirst.Location = new Point(734, 136);
            txtFirst.Name = "txtFirst";
            txtFirst.Size = new Size(225, 27);
            txtFirst.TabIndex = 8;
            // 
            // txtNationID
            // 
            txtNationID.Location = new Point(734, 85);
            txtNationID.Name = "txtNationID";
            txtNationID.Size = new Size(225, 27);
            txtNationID.TabIndex = 9;
            // 
            // txtMemberID
            // 
            txtMemberID.Location = new Point(734, 32);
            txtMemberID.Name = "txtMemberID";
            txtMemberID.Size = new Size(225, 27);
            txtMemberID.TabIndex = 10;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(802, 9);
            label2.Name = "label2";
            label2.Size = new Size(80, 20);
            label2.TabIndex = 11;
            label2.Text = "MemberID";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(808, 62);
            label3.Name = "label3";
            label3.Size = new Size(69, 20);
            label3.TabIndex = 12;
            label3.Text = "NationID";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(808, 115);
            label4.Name = "label4";
            label4.Size = new Size(76, 20);
            label4.TabIndex = 13;
            label4.Text = "FirstName";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(808, 166);
            label5.Name = "label5";
            label5.Size = new Size(72, 20);
            label5.TabIndex = 14;
            label5.Text = "lastName";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(818, 377);
            label6.Name = "label6";
            label6.Size = new Size(50, 20);
            label6.TabIndex = 22;
            label6.Text = "Phone";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(818, 324);
            label7.Name = "label7";
            label7.Size = new Size(46, 20);
            label7.TabIndex = 21;
            label7.Text = "Email";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(808, 273);
            label8.Name = "label8";
            label8.Size = new Size(60, 20);
            label8.TabIndex = 20;
            label8.Text = "address";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(808, 219);
            label9.Name = "label9";
            label9.Size = new Size(74, 20);
            label9.TabIndex = 19;
            label9.Text = "Nationlity";
            // 
            // txtNationlity
            // 
            txtNationlity.Location = new Point(734, 243);
            txtNationlity.Name = "txtNationlity";
            txtNationlity.Size = new Size(225, 27);
            txtNationlity.TabIndex = 18;
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(734, 296);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(225, 27);
            txtAddress.TabIndex = 17;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(734, 347);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(225, 27);
            txtEmail.TabIndex = 16;
            // 
            // txtPhone
            // 
            txtPhone.Location = new Point(734, 400);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(225, 27);
            txtPhone.TabIndex = 15;
            // 
            // customer
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(971, 621);
            Controls.Add(label6);
            Controls.Add(label7);
            Controls.Add(label8);
            Controls.Add(label9);
            Controls.Add(txtNationlity);
            Controls.Add(txtAddress);
            Controls.Add(txtEmail);
            Controls.Add(txtPhone);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(txtMemberID);
            Controls.Add(txtNationID);
            Controls.Add(txtFirst);
            Controls.Add(txtlast);
            Controls.Add(label1);
            Controls.Add(txtsearchbar);
            Controls.Add(btndel);
            Controls.Add(btnupd);
            Controls.Add(btnadd);
            Controls.Add(dataGridView1);
            Name = "customer";
            Text = "customer";
            Load += customer_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Button btnadd;
        private Button btnupd;
        private Button btndel;
        private TextBox txtsearchbar;
        private Label label1;
        private TextBox txtlast;
        private TextBox txtFirst;
        private TextBox txtNationID;
        private TextBox txtMemberID;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private TextBox txtNationlity;
        private TextBox txtAddress;
        private TextBox txtEmail;
        private TextBox txtPhone;
    }
}
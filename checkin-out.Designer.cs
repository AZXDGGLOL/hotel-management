namespace hotel_management
{
    partial class checkin_out
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
            btnAdd = new Button();
            btnupd = new Button();
            btndel = new Button();
            dataGridView1 = new DataGridView();
            txtsearchbar = new TextBox();
            label1 = new Label();
            txtStayid = new TextBox();
            txtSS = new TextBox();
            StayID = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            txtroomID = new TextBox();
            txtReceiptID = new TextBox();
            txtPaymentStatus = new TextBox();
            txtcomment = new TextBox();
            dtpcheckin = new DateTimePicker();
            dtpCheckout = new DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(806, 443);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(105, 61);
            btnAdd.TabIndex = 0;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnadd_Click;
            // 
            // btnupd
            // 
            btnupd.Location = new Point(928, 443);
            btnupd.Name = "btnupd";
            btnupd.Size = new Size(102, 61);
            btnupd.TabIndex = 1;
            btnupd.Text = "Update";
            btnupd.UseVisualStyleBackColor = true;
            btnupd.Click += btnupd_Click;
            // 
            // btndel
            // 
            btndel.Location = new Point(874, 510);
            btndel.Name = "btndel";
            btndel.Size = new Size(109, 62);
            btndel.TabIndex = 2;
            btndel.Text = "Delete";
            btndel.UseVisualStyleBackColor = true;
            btndel.Click += btndel_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 42);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(788, 536);
            dataGridView1.TabIndex = 4;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // txtsearchbar
            // 
            txtsearchbar.Location = new Point(94, 12);
            txtsearchbar.Name = "txtsearchbar";
            txtsearchbar.Size = new Size(667, 27);
            txtsearchbar.TabIndex = 5;
            txtsearchbar.TextChanged += txtsearchbar_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 15);
            label1.Name = "label1";
            label1.Size = new Size(51, 20);
            label1.TabIndex = 6;
            label1.Text = "search";
            // 
            // txtStayid
            // 
            txtStayid.Location = new Point(806, 42);
            txtStayid.Name = "txtStayid";
            txtStayid.Size = new Size(224, 27);
            txtStayid.TabIndex = 7;
            // 
            // txtSS
            // 
            txtSS.Location = new Point(806, 95);
            txtSS.Name = "txtSS";
            txtSS.Size = new Size(224, 27);
            txtSS.TabIndex = 8;
            // 
            // StayID
            // 
            StayID.AutoSize = true;
            StayID.Location = new Point(896, 19);
            StayID.Name = "StayID";
            StayID.Size = new Size(52, 20);
            StayID.TabIndex = 11;
            StayID.Text = "StayID";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(874, 72);
            label3.Name = "label3";
            label3.Size = new Size(101, 20);
            label3.TabIndex = 12;
            label3.Text = "StaySeqeunce";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(874, 125);
            label4.Name = "label4";
            label4.Size = new Size(88, 20);
            label4.TabIndex = 13;
            label4.Text = "checkindate";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(874, 178);
            label5.Name = "label5";
            label5.Size = new Size(100, 20);
            label5.TabIndex = 14;
            label5.Text = "Checkoutdate";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(896, 387);
            label6.Name = "label6";
            label6.Size = new Size(60, 20);
            label6.TabIndex = 22;
            label6.Text = "roomID";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(888, 334);
            label7.Name = "label7";
            label7.Size = new Size(74, 20);
            label7.TabIndex = 21;
            label7.Text = "ReceiptID";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(878, 281);
            label8.Name = "label8";
            label8.Size = new Size(105, 20);
            label8.TabIndex = 20;
            label8.Text = "PaymentStatus";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(884, 231);
            label9.Name = "label9";
            label9.Size = new Size(72, 20);
            label9.TabIndex = 19;
            label9.Text = "comment";
            // 
            // txtroomID
            // 
            txtroomID.Location = new Point(806, 410);
            txtroomID.Name = "txtroomID";
            txtroomID.Size = new Size(224, 27);
            txtroomID.TabIndex = 18;
            // 
            // txtReceiptID
            // 
            txtReceiptID.Location = new Point(806, 357);
            txtReceiptID.Name = "txtReceiptID";
            txtReceiptID.Size = new Size(224, 27);
            txtReceiptID.TabIndex = 17;
            // 
            // txtPaymentStatus
            // 
            txtPaymentStatus.Location = new Point(806, 304);
            txtPaymentStatus.Name = "txtPaymentStatus";
            txtPaymentStatus.Size = new Size(224, 27);
            txtPaymentStatus.TabIndex = 16;
            // 
            // txtcomment
            // 
            txtcomment.Location = new Point(806, 251);
            txtcomment.Name = "txtcomment";
            txtcomment.Size = new Size(224, 27);
            txtcomment.TabIndex = 15;
            // 
            // dtpcheckin
            // 
            dtpcheckin.Location = new Point(806, 148);
            dtpcheckin.Name = "dtpcheckin";
            dtpcheckin.Size = new Size(224, 27);
            dtpcheckin.TabIndex = 23;
            // 
            // dtpCheckout
            // 
            dtpCheckout.Location = new Point(806, 201);
            dtpCheckout.Name = "dtpCheckout";
            dtpCheckout.Size = new Size(224, 27);
            dtpCheckout.TabIndex = 24;
            // 
            // checkin_out
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1042, 584);
            Controls.Add(dtpCheckout);
            Controls.Add(dtpcheckin);
            Controls.Add(label6);
            Controls.Add(label7);
            Controls.Add(label8);
            Controls.Add(label9);
            Controls.Add(txtroomID);
            Controls.Add(txtReceiptID);
            Controls.Add(txtPaymentStatus);
            Controls.Add(txtcomment);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(StayID);
            Controls.Add(txtSS);
            Controls.Add(txtStayid);
            Controls.Add(label1);
            Controls.Add(txtsearchbar);
            Controls.Add(dataGridView1);
            Controls.Add(btndel);
            Controls.Add(btnupd);
            Controls.Add(btnAdd);
            Name = "checkin_out";
            Text = "checkin_out";
            Load += checkin_out_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnAdd;
        private Button btnupd;
        private Button btndel;
        private DataGridView dataGridView1;
        private TextBox txtsearchbar;
        private Label label1;
        private TextBox txtStayid;
        private TextBox txtSS;
        private TextBox txtcheckin;
        private TextBox textBox5;
        private Label StayID;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private TextBox txtroomID;
        private TextBox txtReceiptID;
        private TextBox txtPaymentStatus;
        private TextBox txtcomment;
        private DateTimePicker dtpcheckin;
        private DateTimePicker dtpCheckout;
    }
}
namespace DbToClass
{
    partial class Login
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
            this.dataGridView_FieIdInfo = new System.Windows.Forms.DataGridView();
            this.dataGridView_TableName = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.ModelCreateBtn = new System.Windows.Forms.Button();
            this.OperCreateBtn = new System.Windows.Forms.Button();
            this.ModelCreate1Btn = new System.Windows.Forms.Button();
            this.OperCreate2Btn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_FieIdInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_TableName)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView_FieIdInfo
            // 
            this.dataGridView_FieIdInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_FieIdInfo.Location = new System.Drawing.Point(120, 28);
            this.dataGridView_FieIdInfo.Name = "dataGridView_FieIdInfo";
            this.dataGridView_FieIdInfo.RowTemplate.Height = 23;
            this.dataGridView_FieIdInfo.Size = new System.Drawing.Size(600, 500);
            this.dataGridView_FieIdInfo.TabIndex = 0;
            // 
            // dataGridView_TableName
            // 
            this.dataGridView_TableName.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_TableName.Location = new System.Drawing.Point(10, 28);
            this.dataGridView_TableName.Name = "dataGridView_TableName";
            this.dataGridView_TableName.RowHeadersVisible = false;
            this.dataGridView_TableName.RowTemplate.Height = 23;
            this.dataGridView_TableName.Size = new System.Drawing.Size(100, 500);
            this.dataGridView_TableName.TabIndex = 1;
            this.dataGridView_TableName.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_TableName_CellClick);
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(384, 143);
            this.panel1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(126, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "正在加载中，请稍后";
            // 
            // ModelCreateBtn
            // 
            this.ModelCreateBtn.Location = new System.Drawing.Point(20, 560);
            this.ModelCreateBtn.Name = "ModelCreateBtn";
            this.ModelCreateBtn.Size = new System.Drawing.Size(91, 23);
            this.ModelCreateBtn.TabIndex = 5;
            this.ModelCreateBtn.Text = "模型文件1生成";
            this.ModelCreateBtn.UseVisualStyleBackColor = true;
            this.ModelCreateBtn.Click += new System.EventHandler(this.ModelCreateBtn_Click);
            // 
            // OperCreateBtn
            // 
            this.OperCreateBtn.Location = new System.Drawing.Point(120, 560);
            this.OperCreateBtn.Name = "OperCreateBtn";
            this.OperCreateBtn.Size = new System.Drawing.Size(91, 23);
            this.OperCreateBtn.TabIndex = 6;
            this.OperCreateBtn.Text = "操作文件1生成";
            this.OperCreateBtn.UseVisualStyleBackColor = true;
            this.OperCreateBtn.Click += new System.EventHandler(this.OperCreateBtn_Click);
            // 
            // ModelCreate1Btn
            // 
            this.ModelCreate1Btn.Location = new System.Drawing.Point(220, 560);
            this.ModelCreate1Btn.Name = "ModelCreate1Btn";
            this.ModelCreate1Btn.Size = new System.Drawing.Size(91, 23);
            this.ModelCreate1Btn.TabIndex = 8;
            this.ModelCreate1Btn.Text = "模型文件2生成";
            this.ModelCreate1Btn.UseVisualStyleBackColor = true;
            this.ModelCreate1Btn.Click += new System.EventHandler(this.ModelCreate1Btn_Click);
            // 
            // OperCreate2Btn
            // 
            this.OperCreate2Btn.Location = new System.Drawing.Point(320, 560);
            this.OperCreate2Btn.Name = "OperCreate2Btn";
            this.OperCreate2Btn.Size = new System.Drawing.Size(91, 23);
            this.OperCreate2Btn.TabIndex = 9;
            this.OperCreate2Btn.Text = "操作文件2生成";
            this.OperCreate2Btn.UseVisualStyleBackColor = true;
            this.OperCreate2Btn.Click += new System.EventHandler(this.OperCreate2Btn_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 611);
            this.Controls.Add(this.OperCreate2Btn);
            this.Controls.Add(this.ModelCreate1Btn);
            this.Controls.Add(this.OperCreateBtn);
            this.Controls.Add(this.ModelCreateBtn);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridView_TableName);
            this.Controls.Add(this.dataGridView_FieIdInfo);
            this.MaximumSize = new System.Drawing.Size(750, 650);
            this.MinimumSize = new System.Drawing.Size(750, 650);
            this.Name = "Login";
            this.Text = "123";
            this.Load += new System.EventHandler(this.Login_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_FieIdInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_TableName)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_FieIdInfo;
        private System.Windows.Forms.DataGridView dataGridView_TableName;
        //private Common.Winform.MyPanel panel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ModelCreateBtn;
        private System.Windows.Forms.Button OperCreateBtn;
        private System.Windows.Forms.Button ModelCreate1Btn;
        private System.Windows.Forms.Button OperCreate2Btn;
    }
}
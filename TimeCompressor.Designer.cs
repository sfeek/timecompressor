namespace Time_Compressor
{
    partial class TimeCompressor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TimeCompressor));
            this.btn_compress = new System.Windows.Forms.Button();
            this.txtCount = new System.Windows.Forms.TextBox();
            this.lblCount = new System.Windows.Forms.Label();
            this.lblRecordNumber = new System.Windows.Forms.Label();
            this.chkFieldSeparators = new System.Windows.Forms.CheckBox();
            this.txtZThresh = new System.Windows.Forms.TextBox();
            this.lblZThresh = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_compress
            // 
            this.btn_compress.Location = new System.Drawing.Point(71, 123);
            this.btn_compress.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_compress.Name = "btn_compress";
            this.btn_compress.Size = new System.Drawing.Size(275, 76);
            this.btn_compress.TabIndex = 0;
            this.btn_compress.Text = "Time Compress";
            this.btn_compress.UseVisualStyleBackColor = true;
            this.btn_compress.Click += new System.EventHandler(this.btn_compress_Click);
            // 
            // txtCount
            // 
            this.txtCount.Location = new System.Drawing.Point(132, 15);
            this.txtCount.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCount.Name = "txtCount";
            this.txtCount.Size = new System.Drawing.Size(55, 22);
            this.txtCount.TabIndex = 1;
            this.txtCount.Text = "60";
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(68, 18);
            this.lblCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(50, 16);
            this.lblCount.TabIndex = 2;
            this.lblCount.Text = "Interval";
            // 
            // lblRecordNumber
            // 
            this.lblRecordNumber.AutoSize = true;
            this.lblRecordNumber.Location = new System.Drawing.Point(216, 18);
            this.lblRecordNumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRecordNumber.Name = "lblRecordNumber";
            this.lblRecordNumber.Size = new System.Drawing.Size(62, 16);
            this.lblRecordNumber.TabIndex = 3;
            this.lblRecordNumber.Text = "Record #";
            // 
            // chkFieldSeparators
            // 
            this.chkFieldSeparators.AutoSize = true;
            this.chkFieldSeparators.Location = new System.Drawing.Point(71, 70);
            this.chkFieldSeparators.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkFieldSeparators.Name = "chkFieldSeparators";
            this.chkFieldSeparators.Size = new System.Drawing.Size(129, 20);
            this.chkFieldSeparators.TabIndex = 4;
            this.chkFieldSeparators.Text = "Field Separators";
            this.chkFieldSeparators.UseVisualStyleBackColor = true;
            // 
            // txtZThresh
            // 
            this.txtZThresh.Location = new System.Drawing.Point(308, 70);
            this.txtZThresh.Name = "txtZThresh";
            this.txtZThresh.Size = new System.Drawing.Size(55, 22);
            this.txtZThresh.TabIndex = 5;
            this.txtZThresh.Text = "3.0";
            // 
            // lblZThresh
            // 
            this.lblZThresh.AutoSize = true;
            this.lblZThresh.Location = new System.Drawing.Point(242, 73);
            this.lblZThresh.Name = "lblZThresh";
            this.lblZThresh.Size = new System.Drawing.Size(60, 16);
            this.lblZThresh.TabIndex = 6;
            this.lblZThresh.Text = "Z Thresh";
            // 
            // TimeCompressor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 229);
            this.Controls.Add(this.lblZThresh);
            this.Controls.Add(this.txtZThresh);
            this.Controls.Add(this.chkFieldSeparators);
            this.Controls.Add(this.lblRecordNumber);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.txtCount);
            this.Controls.Add(this.btn_compress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "TimeCompressor";
            this.Text = "Time Compressor v1.6";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_compress;
        private System.Windows.Forms.TextBox txtCount;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label lblRecordNumber;
        private System.Windows.Forms.CheckBox chkFieldSeparators;
        private System.Windows.Forms.TextBox txtZThresh;
        private System.Windows.Forms.Label lblZThresh;
    }
}


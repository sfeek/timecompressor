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
            this.SuspendLayout();
            // 
            // btn_compress
            // 
            this.btn_compress.Location = new System.Drawing.Point(53, 100);
            this.btn_compress.Name = "btn_compress";
            this.btn_compress.Size = new System.Drawing.Size(206, 62);
            this.btn_compress.TabIndex = 0;
            this.btn_compress.Text = "Time Compress";
            this.btn_compress.UseVisualStyleBackColor = true;
            this.btn_compress.Click += new System.EventHandler(this.btn_compress_Click);
            // 
            // txtCount
            // 
            this.txtCount.Location = new System.Drawing.Point(99, 12);
            this.txtCount.Name = "txtCount";
            this.txtCount.Size = new System.Drawing.Size(42, 20);
            this.txtCount.TabIndex = 1;
            this.txtCount.Text = "60";
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(51, 15);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(42, 13);
            this.lblCount.TabIndex = 2;
            this.lblCount.Text = "Interval";
            // 
            // lblRecordNumber
            // 
            this.lblRecordNumber.AutoSize = true;
            this.lblRecordNumber.Location = new System.Drawing.Point(162, 15);
            this.lblRecordNumber.Name = "lblRecordNumber";
            this.lblRecordNumber.Size = new System.Drawing.Size(52, 13);
            this.lblRecordNumber.TabIndex = 3;
            this.lblRecordNumber.Text = "Record #";
            // 
            // chkFieldSeparators
            // 
            this.chkFieldSeparators.AutoSize = true;
            this.chkFieldSeparators.Location = new System.Drawing.Point(53, 57);
            this.chkFieldSeparators.Name = "chkFieldSeparators";
            this.chkFieldSeparators.Size = new System.Drawing.Size(102, 17);
            this.chkFieldSeparators.TabIndex = 4;
            this.chkFieldSeparators.Text = "Field Separators";
            this.chkFieldSeparators.UseVisualStyleBackColor = true;
            // 
            // TimeCompressor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 186);
            this.Controls.Add(this.chkFieldSeparators);
            this.Controls.Add(this.lblRecordNumber);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.txtCount);
            this.Controls.Add(this.btn_compress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "TimeCompressor";
            this.Text = "Time Compressor v1.5";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_compress;
        private System.Windows.Forms.TextBox txtCount;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label lblRecordNumber;
        private System.Windows.Forms.CheckBox chkFieldSeparators;
    }
}


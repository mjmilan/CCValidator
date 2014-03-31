namespace TestProject
{
    partial class Form1
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
            this.lstResults = new System.Windows.Forms.ListBox();
            this.butSimpleCall = new System.Windows.Forms.Button();
            this.butBulkCall = new System.Windows.Forms.Button();
            this.butAsyncBulkCall = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstResults
            // 
            this.lstResults.FormattingEnabled = true;
            this.lstResults.Location = new System.Drawing.Point(24, 28);
            this.lstResults.Name = "lstResults";
            this.lstResults.Size = new System.Drawing.Size(544, 95);
            this.lstResults.TabIndex = 0;
            // 
            // butSimpleCall
            // 
            this.butSimpleCall.Location = new System.Drawing.Point(28, 150);
            this.butSimpleCall.Name = "butSimpleCall";
            this.butSimpleCall.Size = new System.Drawing.Size(95, 20);
            this.butSimpleCall.TabIndex = 1;
            this.butSimpleCall.Text = "Simple Call";
            this.butSimpleCall.UseVisualStyleBackColor = true;
            this.butSimpleCall.Click += new System.EventHandler(this.butSimpleCall_Click);
            // 
            // butBulkCall
            // 
            this.butBulkCall.Location = new System.Drawing.Point(242, 150);
            this.butBulkCall.Name = "butBulkCall";
            this.butBulkCall.Size = new System.Drawing.Size(95, 20);
            this.butBulkCall.TabIndex = 2;
            this.butBulkCall.Text = "Bulk Call";
            this.butBulkCall.UseVisualStyleBackColor = true;
            this.butBulkCall.Click += new System.EventHandler(this.butBulkCall_Click);
            // 
            // butAsyncBulkCall
            // 
            this.butAsyncBulkCall.Location = new System.Drawing.Point(473, 150);
            this.butAsyncBulkCall.Name = "butAsyncBulkCall";
            this.butAsyncBulkCall.Size = new System.Drawing.Size(95, 20);
            this.butAsyncBulkCall.TabIndex = 3;
            this.butAsyncBulkCall.Text = "Async Bulk Call";
            this.butAsyncBulkCall.UseVisualStyleBackColor = true;
            this.butAsyncBulkCall.Click += new System.EventHandler(this.butAsyncBulkCall_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 192);
            this.Controls.Add(this.butAsyncBulkCall);
            this.Controls.Add(this.butBulkCall);
            this.Controls.Add(this.butSimpleCall);
            this.Controls.Add(this.lstResults);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstResults;
        private System.Windows.Forms.Button butSimpleCall;
        private System.Windows.Forms.Button butBulkCall;
        private System.Windows.Forms.Button butAsyncBulkCall;
    }
}


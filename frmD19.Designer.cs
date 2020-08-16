namespace Hansa
{
    partial class FrmD19
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
            this.lstD19 = new System.Windows.Forms.ListView();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstD19
            // 
            this.lstD19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstD19.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstD19.GridLines = true;
            this.lstD19.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstD19.HideSelection = false;
            this.lstD19.Location = new System.Drawing.Point(0, 0);
            this.lstD19.MultiSelect = false;
            this.lstD19.Name = "lstD19";
            this.lstD19.Scrollable = false;
            this.lstD19.Size = new System.Drawing.Size(508, 194);
            this.lstD19.TabIndex = 0;
            this.lstD19.TabStop = false;
            this.lstD19.UseCompatibleStateImageBehavior = false;
            this.lstD19.View = System.Windows.Forms.View.Details;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(244, 83);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(1, 1);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // FrmD19
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(508, 172);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lstD19);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmD19";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmD1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lstD19;
        private System.Windows.Forms.Button btnClose;
    }
}
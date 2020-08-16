namespace Hansa
{
    partial class FrmD6
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
            this.lstD6 = new System.Windows.Forms.ListView();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstD6
            // 
            this.lstD6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstD6.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstD6.GridLines = true;
            this.lstD6.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstD6.HideSelection = false;
            this.lstD6.Location = new System.Drawing.Point(0, 0);
            this.lstD6.MultiSelect = false;
            this.lstD6.Name = "lstD6";
            this.lstD6.Scrollable = false;
            this.lstD6.Size = new System.Drawing.Size(313, 194);
            this.lstD6.TabIndex = 0;
            this.lstD6.TabStop = false;
            this.lstD6.UseCompatibleStateImageBehavior = false;
            this.lstD6.View = System.Windows.Forms.View.Details;
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
            // FrmD6
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(313, 172);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lstD6);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmD6";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Shelf";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lstD6;
        private System.Windows.Forms.Button btnClose;
    }
}
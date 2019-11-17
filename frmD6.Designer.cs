namespace HASA
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
            this.lstCantilever = new System.Windows.Forms.ListView();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstCantilever
            // 
            this.lstCantilever.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstCantilever.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstCantilever.GridLines = true;
            this.lstCantilever.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstCantilever.Location = new System.Drawing.Point(0, 0);
            this.lstCantilever.MultiSelect = false;
            this.lstCantilever.Name = "lstCantilever";
            this.lstCantilever.Scrollable = false;
            this.lstCantilever.Size = new System.Drawing.Size(313, 194);
            this.lstCantilever.TabIndex = 0;
            this.lstCantilever.TabStop = false;
            this.lstCantilever.UseCompatibleStateImageBehavior = false;
            this.lstCantilever.View = System.Windows.Forms.View.Details;
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
            // FrmCantilever
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(313, 172);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lstCantilever);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCantilever";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Shelf";
            this.Load += new System.EventHandler(this.FrmD6_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lstCantilever;
        private System.Windows.Forms.Button btnClose;
    }
}
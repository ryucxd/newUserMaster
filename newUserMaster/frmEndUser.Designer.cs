namespace newUserMaster
{
    partial class frmEndUser
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
            this.dteEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblEndDte = new System.Windows.Forms.Label();
            this.btnEnd = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dteEndDate
            // 
            this.dteEndDate.Location = new System.Drawing.Point(119, 28);
            this.dteEndDate.Name = "dteEndDate";
            this.dteEndDate.Size = new System.Drawing.Size(139, 20);
            this.dteEndDate.TabIndex = 0;
            // 
            // lblEndDte
            // 
            this.lblEndDte.Location = new System.Drawing.Point(29, 27);
            this.lblEndDte.Name = "lblEndDte";
            this.lblEndDte.Size = new System.Drawing.Size(86, 20);
            this.lblEndDte.TabIndex = 1;
            this.lblEndDte.Text = "Users End Date";
            this.lblEndDte.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnEnd
            // 
            this.btnEnd.Location = new System.Drawing.Point(146, 65);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(75, 23);
            this.btnEnd.TabIndex = 2;
            this.btnEnd.Text = "End user";
            this.btnEnd.UseVisualStyleBackColor = true;
            this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(65, 65);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmEndUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(287, 106);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnEnd);
            this.Controls.Add(this.lblEndDte);
            this.Controls.Add(this.dteEndDate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmEndUser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "End User Master";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dteEndDate;
        private System.Windows.Forms.Label lblEndDte;
        private System.Windows.Forms.Button btnEnd;
        private System.Windows.Forms.Button btnCancel;
    }
}
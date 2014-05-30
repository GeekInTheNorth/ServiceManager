namespace ServiceManager
{
    partial class AddService
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
            this.label1 = new System.Windows.Forms.Label();
            this.cbServices = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbServiceName = new System.Windows.Forms.TextBox();
            this.tbServiceDisplayName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbServiceStatus = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.bnCancel = new System.Windows.Forms.Button();
            this.bnOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select Service";
            // 
            // cbServices
            // 
            this.cbServices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbServices.FormattingEnabled = true;
            this.cbServices.Location = new System.Drawing.Point(94, 9);
            this.cbServices.Name = "cbServices";
            this.cbServices.Size = new System.Drawing.Size(307, 21);
            this.cbServices.TabIndex = 1;
            this.cbServices.SelectedIndexChanged += new System.EventHandler(this.cbServices_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Service Name";
            // 
            // tbServiceName
            // 
            this.tbServiceName.Location = new System.Drawing.Point(94, 36);
            this.tbServiceName.Name = "tbServiceName";
            this.tbServiceName.ReadOnly = true;
            this.tbServiceName.Size = new System.Drawing.Size(307, 20);
            this.tbServiceName.TabIndex = 3;
            // 
            // tbServiceDisplayName
            // 
            this.tbServiceDisplayName.Location = new System.Drawing.Point(94, 62);
            this.tbServiceDisplayName.Name = "tbServiceDisplayName";
            this.tbServiceDisplayName.Size = new System.Drawing.Size(307, 20);
            this.tbServiceDisplayName.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Display Name";
            // 
            // tbServiceStatus
            // 
            this.tbServiceStatus.Location = new System.Drawing.Point(94, 88);
            this.tbServiceStatus.Name = "tbServiceStatus";
            this.tbServiceStatus.ReadOnly = true;
            this.tbServiceStatus.Size = new System.Drawing.Size(307, 20);
            this.tbServiceStatus.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Status";
            // 
            // bnCancel
            // 
            this.bnCancel.Location = new System.Drawing.Point(326, 114);
            this.bnCancel.Name = "bnCancel";
            this.bnCancel.Size = new System.Drawing.Size(75, 23);
            this.bnCancel.TabIndex = 8;
            this.bnCancel.Text = "Cancel";
            this.bnCancel.UseVisualStyleBackColor = true;
            this.bnCancel.Click += new System.EventHandler(this.bnCancel_Click);
            // 
            // bnOk
            // 
            this.bnOk.Location = new System.Drawing.Point(245, 114);
            this.bnOk.Name = "bnOk";
            this.bnOk.Size = new System.Drawing.Size(75, 23);
            this.bnOk.TabIndex = 9;
            this.bnOk.Text = "OK";
            this.bnOk.UseVisualStyleBackColor = true;
            this.bnOk.Click += new System.EventHandler(this.bnOk_Click);
            // 
            // AddService
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(426, 158);
            this.Controls.Add(this.bnOk);
            this.Controls.Add(this.bnCancel);
            this.Controls.Add(this.tbServiceStatus);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbServiceDisplayName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbServiceName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbServices);
            this.Controls.Add(this.label1);
            this.Name = "AddService";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddService";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbServices;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbServiceName;
        private System.Windows.Forms.TextBox tbServiceDisplayName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbServiceStatus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button bnCancel;
        private System.Windows.Forms.Button bnOk;
    }
}
namespace ED_Explorer {
   partial class SettingsForm {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing) {
         if (disposing && (components != null)) {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent() {
         this.CommanderNameLabel = new System.Windows.Forms.Label();
         this.CommanderName = new System.Windows.Forms.TextBox();
         this.Password = new System.Windows.Forms.TextBox();
         this.PasswordLabel = new System.Windows.Forms.Label();
         this.Save = new System.Windows.Forms.Button();
         this.Email = new System.Windows.Forms.TextBox();
         this.EmailLabel = new System.Windows.Forms.Label();
         this.Close = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // CommanderNameLabel
         // 
         this.CommanderNameLabel.AutoSize = true;
         this.CommanderNameLabel.Location = new System.Drawing.Point(13, 58);
         this.CommanderNameLabel.Name = "CommanderNameLabel";
         this.CommanderNameLabel.Size = new System.Drawing.Size(94, 13);
         this.CommanderNameLabel.TabIndex = 0;
         this.CommanderNameLabel.Text = "Commander Name";
         // 
         // CommanderName
         // 
         this.CommanderName.Location = new System.Drawing.Point(16, 75);
         this.CommanderName.Name = "CommanderName";
         this.CommanderName.Size = new System.Drawing.Size(325, 20);
         this.CommanderName.TabIndex = 1;
         // 
         // Password
         // 
         this.Password.Location = new System.Drawing.Point(16, 118);
         this.Password.Name = "Password";
         this.Password.Size = new System.Drawing.Size(325, 20);
         this.Password.TabIndex = 3;
         this.Password.UseSystemPasswordChar = true;
         // 
         // PasswordLabel
         // 
         this.PasswordLabel.AutoSize = true;
         this.PasswordLabel.Location = new System.Drawing.Point(13, 101);
         this.PasswordLabel.Name = "PasswordLabel";
         this.PasswordLabel.Size = new System.Drawing.Size(112, 13);
         this.PasswordLabel.TabIndex = 2;
         this.PasswordLabel.Text = "ED-Explorer Password";
         // 
         // Save
         // 
         this.Save.DialogResult = System.Windows.Forms.DialogResult.OK;
         this.Save.Location = new System.Drawing.Point(16, 162);
         this.Save.Name = "Save";
         this.Save.Size = new System.Drawing.Size(75, 23);
         this.Save.TabIndex = 4;
         this.Save.Text = "Save";
         this.Save.UseVisualStyleBackColor = true;
         this.Save.Click += new System.EventHandler(this.Save_Click);
         // 
         // Email
         // 
         this.Email.Location = new System.Drawing.Point(16, 35);
         this.Email.Name = "Email";
         this.Email.Size = new System.Drawing.Size(325, 20);
         this.Email.TabIndex = 6;
         // 
         // EmailLabel
         // 
         this.EmailLabel.AutoSize = true;
         this.EmailLabel.Location = new System.Drawing.Point(13, 18);
         this.EmailLabel.Name = "EmailLabel";
         this.EmailLabel.Size = new System.Drawing.Size(35, 13);
         this.EmailLabel.TabIndex = 5;
         this.EmailLabel.Text = "E-mail";
         // 
         // Close
         // 
         this.Close.DialogResult = System.Windows.Forms.DialogResult.Abort;
         this.Close.Location = new System.Drawing.Point(265, 162);
         this.Close.Name = "Close";
         this.Close.Size = new System.Drawing.Size(75, 23);
         this.Close.TabIndex = 7;
         this.Close.Text = "Close";
         this.Close.UseVisualStyleBackColor = true;
         // 
         // SettingsForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(353, 197);
         this.Controls.Add(this.Close);
         this.Controls.Add(this.Email);
         this.Controls.Add(this.EmailLabel);
         this.Controls.Add(this.Save);
         this.Controls.Add(this.Password);
         this.Controls.Add(this.PasswordLabel);
         this.Controls.Add(this.CommanderName);
         this.Controls.Add(this.CommanderNameLabel);
         this.Name = "SettingsForm";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
         this.Text = "Settings";
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Label CommanderNameLabel;
      private System.Windows.Forms.TextBox CommanderName;
      private System.Windows.Forms.TextBox Password;
      private System.Windows.Forms.Label PasswordLabel;
      private System.Windows.Forms.Button Save;
      private System.Windows.Forms.TextBox Email;
      private System.Windows.Forms.Label EmailLabel;
      private System.Windows.Forms.Button Close;
   }
}
using System;
using System.Windows.Forms;
using ED_Explorer.Models;
using ED_Explorer.Services;

namespace ED_Explorer {
   public partial class SettingsForm : Form {
      public SettingsForm() {
         InitializeComponent();
         Save.Enabled = AppModel.Settings == null;
         if (AppModel.Settings != null)
         {
            Password.Text = AppModel.Settings.Email;
            CommanderName.Text = AppModel.Settings.Commander;
            Password.Text = Guid.NewGuid().ToString();
         }
      }

      private void Save_Click(object sender, EventArgs e) {
         if (!FormFieldsAreValid()) return;

         var setting = new Settings {
            Email = Email.Text.Trim(),
            Commander = CommanderName.Text.Trim(),
            Password = Encryption.Encrypt(Password.Text.Trim())
         };

         AppModel.Settings = SettingsServices.Save(setting, AppModel.DbPath);

         if (null == AppModel.Settings) {
            MessageBox.Show(@"Settings was not saved..");
         } else {
            MessageBox.Show(@"Save successful");
            Save.Enabled = AppModel.Settings == null;
         }
      }

      private bool FormFieldsAreValid() {
         if (string.IsNullOrWhiteSpace(CommanderName.Text))
         {
            MessageBox.Show(@"Commander Name entry required");
            return false;
         }

         if (string.IsNullOrWhiteSpace(Email.Text))
         {
            MessageBox.Show(@"Email entry required");
            return false;
         }

         if (string.IsNullOrWhiteSpace(PasswordLabel.Text))
         {
            MessageBox.Show(@"Password entry required");
            return false;
         }
         return true;
      }
   }
}

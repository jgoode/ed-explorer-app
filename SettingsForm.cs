using System;
using System.Windows.Forms;
using ED_Explorer.Models;
using ED_Explorer.Services;

namespace ED_Explorer {
   public partial class SettingsForm : Form {
      public SettingsForm() {
         InitializeComponent();
         Save.Enabled = AppModel.Settings == null;
         if (AppModel.Settings != null) {
            CommanderName.Text = AppModel.Settings.Commander;
            Password.Text = Guid.NewGuid().ToString();
         }
      }

      private void Save_Click(object sender, EventArgs e) {
         if (string.IsNullOrWhiteSpace(CommanderName.Text)) {
            MessageBox.Show(@"Commander Name entry required");
            return;
         }

         if (string.IsNullOrWhiteSpace(PasswordLabel.Text)) {
            MessageBox.Show(@"Password entry required");
         }

         var setting = new Settings {
            Commander = CommanderName.Text,
            Password = Encryption.Encrypt(Password.Text)
         };

         AppModel.Settings = SettingsServices.Save(setting, AppModel.DbPath);

         if (null == AppModel.Settings) {
            MessageBox.Show(@"Settings was not saved..");
         } else {
            MessageBox.Show(@"Save successful");
            Save.Enabled = AppModel.Settings == null;
         }
      }
   }
}

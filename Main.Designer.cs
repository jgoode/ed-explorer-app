namespace ED_Explorer {
   partial class Main {
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
         this.menuStrip1 = new System.Windows.Forms.MenuStrip();
         this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.profileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.testsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.authenticateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.statusStrip1 = new System.Windows.Forms.StatusStrip();
         this.panel1 = new System.Windows.Forms.Panel();
         this.richTextBoxScroll1 = new ExtendedControls.RichTextBoxScroll();
         this.menuStrip1.SuspendLayout();
         this.panel1.SuspendLayout();
         this.SuspendLayout();
         // 
         // menuStrip1
         // 
         this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.testsToolStripMenuItem});
         this.menuStrip1.Location = new System.Drawing.Point(0, 0);
         this.menuStrip1.Name = "menuStrip1";
         this.menuStrip1.Size = new System.Drawing.Size(573, 24);
         this.menuStrip1.TabIndex = 0;
         this.menuStrip1.Text = "menuStrip1";
         // 
         // fileToolStripMenuItem
         // 
         this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
         this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
         this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
         this.fileToolStripMenuItem.Text = "File";
         // 
         // exitToolStripMenuItem
         // 
         this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
         this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
         this.exitToolStripMenuItem.Text = "Exit";
         this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
         // 
         // settingsToolStripMenuItem
         // 
         this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.profileToolStripMenuItem});
         this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
         this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
         this.settingsToolStripMenuItem.Text = "Settings";
         // 
         // profileToolStripMenuItem
         // 
         this.profileToolStripMenuItem.Name = "profileToolStripMenuItem";
         this.profileToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
         this.profileToolStripMenuItem.Text = "Profile";
         this.profileToolStripMenuItem.Click += new System.EventHandler(this.profileToolStripMenuItem_Click);
         // 
         // testsToolStripMenuItem
         // 
         this.testsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.authenticateToolStripMenuItem});
         this.testsToolStripMenuItem.Name = "testsToolStripMenuItem";
         this.testsToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
         this.testsToolStripMenuItem.Text = "Tests";
         // 
         // authenticateToolStripMenuItem
         // 
         this.authenticateToolStripMenuItem.Name = "authenticateToolStripMenuItem";
         this.authenticateToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
         this.authenticateToolStripMenuItem.Text = "Authenticate";
         this.authenticateToolStripMenuItem.Click += new System.EventHandler(this.authenticateToolStripMenuItem_Click);
         // 
         // statusStrip1
         // 
         this.statusStrip1.Location = new System.Drawing.Point(0, 278);
         this.statusStrip1.Name = "statusStrip1";
         this.statusStrip1.Size = new System.Drawing.Size(573, 22);
         this.statusStrip1.TabIndex = 1;
         this.statusStrip1.Text = "statusStrip1";
         // 
         // panel1
         // 
         this.panel1.Controls.Add(this.richTextBoxScroll1);
         this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel1.Location = new System.Drawing.Point(0, 24);
         this.panel1.Name = "panel1";
         this.panel1.Padding = new System.Windows.Forms.Padding(10, 2, 2, 2);
         this.panel1.Size = new System.Drawing.Size(573, 254);
         this.panel1.TabIndex = 3;
         // 
         // richTextBoxScroll1
         // 
         this.richTextBoxScroll1.BorderColor = System.Drawing.Color.Transparent;
         this.richTextBoxScroll1.BorderColorScaling = 0.5F;
         this.richTextBoxScroll1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.richTextBoxScroll1.HideScrollBar = true;
         this.richTextBoxScroll1.Location = new System.Drawing.Point(10, 2);
         this.richTextBoxScroll1.Margin = new System.Windows.Forms.Padding(5);
         this.richTextBoxScroll1.Name = "richTextBoxScroll1";
         this.richTextBoxScroll1.ScrollBarWidth = 20;
         this.richTextBoxScroll1.ShowLineCount = false;
         this.richTextBoxScroll1.Size = new System.Drawing.Size(561, 250);
         this.richTextBoxScroll1.TabIndex = 3;
         // 
         // Main
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(573, 300);
         this.Controls.Add(this.panel1);
         this.Controls.Add(this.statusStrip1);
         this.Controls.Add(this.menuStrip1);
         this.MainMenuStrip = this.menuStrip1;
         this.Name = "Main";
         this.Text = "ED-Explorer Client";
         this.Load += new System.EventHandler(this.Main_Load);
         this.menuStrip1.ResumeLayout(false);
         this.menuStrip1.PerformLayout();
         this.panel1.ResumeLayout(false);
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.MenuStrip menuStrip1;
      private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem profileToolStripMenuItem;
      private System.Windows.Forms.StatusStrip statusStrip1;
      private System.Windows.Forms.ToolStripMenuItem testsToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem authenticateToolStripMenuItem;
      private System.Windows.Forms.Panel panel1;
      private ExtendedControls.RichTextBoxScroll richTextBoxScroll1;
   }
}


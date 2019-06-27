namespace Snipping_OCR
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.notifyMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuEngine = new System.Windows.Forms.ToolStripComboBox();
            this.mnuLanguageCombo = new System.Windows.Forms.ToolStripComboBox();
            this.mnuSnip = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuClipboardNow = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMonitorClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipText = "Hola";
            this.notifyIcon.BalloonTipTitle = "OCR";
            this.notifyIcon.ContextMenuStrip = this.notifyMenu;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "OCR";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // notifyMenu
            // 
            this.notifyMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.notifyMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEngine,
            this.mnuLanguageCombo,
            this.mnuSnip,
            this.mnuClipboardNow,
            this.mnuMonitorClipboard,
            this.exitToolStripMenuItem,
            this.mnuExit});
            this.notifyMenu.Name = "notifyMenu";
            this.notifyMenu.Size = new System.Drawing.Size(186, 174);
            // 
            // mnuEngine
            // 
            this.mnuEngine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mnuEngine.Items.AddRange(new object[] {
            "SpaceOCR",
            "Tesseract"});
            this.mnuEngine.Name = "mnuEngine";
            this.mnuEngine.Size = new System.Drawing.Size(121, 23);
            // 
            // mnuLanguageCombo
            // 
            this.mnuLanguageCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mnuLanguageCombo.Items.AddRange(new object[] {
            "English",
            "Spanish"});
            this.mnuLanguageCombo.Name = "mnuLanguageCombo";
            this.mnuLanguageCombo.Size = new System.Drawing.Size(121, 23);
            this.mnuLanguageCombo.Click += new System.EventHandler(this.toolStripComboBox1_Click);
            // 
            // mnuSnip
            // 
            this.mnuSnip.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.mnuSnip.Name = "mnuSnip";
            this.mnuSnip.Size = new System.Drawing.Size(185, 22);
            this.mnuSnip.Text = "Snip (CTRL+WIN+C)";
            this.mnuSnip.Click += new System.EventHandler(this.mnuSnip_Click);
            // 
            // mnuClipboardNow
            // 
            this.mnuClipboardNow.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.mnuClipboardNow.Name = "mnuClipboardNow";
            this.mnuClipboardNow.Size = new System.Drawing.Size(185, 22);
            this.mnuClipboardNow.Text = "Process Clipboard";
            this.mnuClipboardNow.Click += new System.EventHandler(this.mnuClipboardNow_Click);
            // 
            // mnuMonitorClipboard
            // 
            this.mnuMonitorClipboard.Name = "mnuMonitorClipboard";
            this.mnuMonitorClipboard.Size = new System.Drawing.Size(185, 22);
            this.mnuMonitorClipboard.Text = "Monitor clipboard";
            this.mnuMonitorClipboard.Click += new System.EventHandler(this.mnuMonitorClipboard_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(182, 6);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(185, 22);
            this.mnuExit.Text = "Exit";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 441);
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "MainForm";
            this.Text = "OCR";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.notifyMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip notifyMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuMonitorClipboard;
        private System.Windows.Forms.ToolStripSeparator exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.ToolStripMenuItem mnuClipboardNow;
        private System.Windows.Forms.ToolStripComboBox mnuLanguageCombo;
        private System.Windows.Forms.ToolStripMenuItem mnuSnip;
        private System.Windows.Forms.ToolStripComboBox mnuEngine;
    }
}


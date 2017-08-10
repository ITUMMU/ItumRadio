namespace itumRadio
{
    partial class ItumRadio
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
            this.volumeControl = new System.Windows.Forms.TrackBar();
            this.stationList = new System.Windows.Forms.ComboBox();
            this.reverseButton = new System.Windows.Forms.Button();
            this.forwardButton = new System.Windows.Forms.Button();
            this.stopButtonbutton = new System.Windows.Forms.Button();
            this.playButton = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.stationLogo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.volumeControl)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stationLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // volumeControl
            // 
            this.volumeControl.Location = new System.Drawing.Point(256, 39);
            this.volumeControl.Name = "volumeControl";
            this.volumeControl.Size = new System.Drawing.Size(104, 45);
            this.volumeControl.TabIndex = 7;
            this.volumeControl.Scroll += new System.EventHandler(this.volumeControl_Scroll);
            // 
            // stationList
            // 
            this.stationList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.stationList.FormattingEnabled = true;
            this.stationList.Location = new System.Drawing.Point(12, 12);
            this.stationList.Name = "stationList";
            this.stationList.Size = new System.Drawing.Size(349, 21);
            this.stationList.Sorted = true;
            this.stationList.TabIndex = 1;
            this.stationList.SelectedIndexChanged += new System.EventHandler(this.stationList_SelectedIndexChanged);
            // 
            // reverseButton
            // 
            this.reverseButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.reverseButton.Image = global::itumRadio.Properties.Resources.buttonReverse;
            this.reverseButton.Location = new System.Drawing.Point(12, 39);
            this.reverseButton.Margin = new System.Windows.Forms.Padding(0);
            this.reverseButton.Name = "reverseButton";
            this.reverseButton.Size = new System.Drawing.Size(56, 56);
            this.reverseButton.TabIndex = 2;
            this.reverseButton.UseVisualStyleBackColor = true;
            this.reverseButton.Click += new System.EventHandler(this.reverseButton_Click);
            // 
            // forwardButton
            // 
            this.forwardButton.BackColor = System.Drawing.Color.Transparent;
            this.forwardButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.forwardButton.Image = global::itumRadio.Properties.Resources.buttonForward;
            this.forwardButton.Location = new System.Drawing.Point(195, 39);
            this.forwardButton.Name = "forwardButton";
            this.forwardButton.Size = new System.Drawing.Size(56, 56);
            this.forwardButton.TabIndex = 5;
            this.forwardButton.UseVisualStyleBackColor = false;
            this.forwardButton.Click += new System.EventHandler(this.forwardButton_Click);
            // 
            // stopButtonbutton
            // 
            this.stopButtonbutton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.stopButtonbutton.Image = global::itumRadio.Properties.Resources.buttonStop;
            this.stopButtonbutton.Location = new System.Drawing.Point(73, 39);
            this.stopButtonbutton.Name = "stopButtonbutton";
            this.stopButtonbutton.Size = new System.Drawing.Size(56, 56);
            this.stopButtonbutton.TabIndex = 3;
            this.stopButtonbutton.UseVisualStyleBackColor = true;
            this.stopButtonbutton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // playButton
            // 
            this.playButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.playButton.Image = global::itumRadio.Properties.Resources.buttonPlay;
            this.playButton.Location = new System.Drawing.Point(134, 39);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(56, 56);
            this.playButton.TabIndex = 4;
            this.playButton.UseVisualStyleBackColor = true;
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressBar,
            this.StatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 105);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(464, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // progressBar
            // 
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(100, 16);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            // 
            // StatusLabel
            // 
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(67, 17);
            this.StatusLabel.Text = "StatusLabel";
            // 
            // stationLogo
            // 
            this.stationLogo.Location = new System.Drawing.Point(367, 12);
            this.stationLogo.Name = "stationLogo";
            this.stationLogo.Size = new System.Drawing.Size(83, 83);
            this.stationLogo.TabIndex = 8;
            this.stationLogo.TabStop = false;
            // 
            // ItumRadio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 127);
            this.Controls.Add(this.stationLogo);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.stationList);
            this.Controls.Add(this.volumeControl);
            this.Controls.Add(this.reverseButton);
            this.Controls.Add(this.forwardButton);
            this.Controls.Add(this.stopButtonbutton);
            this.Controls.Add(this.playButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ItumRadio";
            this.Text = "Itum Radio";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ItumRadio_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.volumeControl)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stationLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.Button stopButtonbutton;
        private System.Windows.Forms.Button forwardButton;
        private System.Windows.Forms.Button reverseButton;
        private System.Windows.Forms.TrackBar volumeControl;
        private System.Windows.Forms.ComboBox stationList;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.PictureBox stationLogo;
    }
}


namespace Engine
{
    partial class SystemSettingForm
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
            this.backgroundColorDialog = new System.Windows.Forms.ColorDialog();
            this.btnPlay = new System.Windows.Forms.Button();
            this.MusicVolumeSlider = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.AudioGroup = new System.Windows.Forms.GroupBox();
            this.EffectsVolumeSlider = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.MasterVolumeSlider = new System.Windows.Forms.TrackBar();
            this.lblBackgroundAlpha = new System.Windows.Forms.Label();
            this.VideoGroup = new System.Windows.Forms.GroupBox();
            this.FullscreenCombo = new System.Windows.Forms.ComboBox();
            this.FullscreenLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ResolutionCombo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.MonitorCombo = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.MusicVolumeSlider)).BeginInit();
            this.AudioGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EffectsVolumeSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MasterVolumeSlider)).BeginInit();
            this.VideoGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // backgroundColorDialog
            // 
            this.backgroundColorDialog.AnyColor = true;
            // 
            // btnPlay
            // 
            this.btnPlay.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnPlay.Location = new System.Drawing.Point(345, 378);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(75, 25);
            this.btnPlay.TabIndex = 6;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // MusicVolumeSlider
            // 
            this.MusicVolumeSlider.Location = new System.Drawing.Point(114, 111);
            this.MusicVolumeSlider.Maximum = 100;
            this.MusicVolumeSlider.Name = "MusicVolumeSlider";
            this.MusicVolumeSlider.Size = new System.Drawing.Size(281, 42);
            this.MusicVolumeSlider.TabIndex = 5;
            this.MusicVolumeSlider.TickFrequency = 10;
            this.MusicVolumeSlider.Value = 80;
            this.MusicVolumeSlider.Scroll += new System.EventHandler(this.MusicVolumeSlider_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Music Volume";
            // 
            // AudioGroup
            // 
            this.AudioGroup.Controls.Add(this.EffectsVolumeSlider);
            this.AudioGroup.Controls.Add(this.label2);
            this.AudioGroup.Controls.Add(this.MasterVolumeSlider);
            this.AudioGroup.Controls.Add(this.lblBackgroundAlpha);
            this.AudioGroup.Controls.Add(this.MusicVolumeSlider);
            this.AudioGroup.Controls.Add(this.label1);
            this.AudioGroup.Location = new System.Drawing.Point(12, 193);
            this.AudioGroup.Name = "AudioGroup";
            this.AudioGroup.Size = new System.Drawing.Size(419, 165);
            this.AudioGroup.TabIndex = 14;
            this.AudioGroup.TabStop = false;
            this.AudioGroup.Text = "Audio";
            // 
            // EffectsVolumeSlider
            // 
            this.EffectsVolumeSlider.Location = new System.Drawing.Point(114, 70);
            this.EffectsVolumeSlider.Maximum = 100;
            this.EffectsVolumeSlider.Name = "EffectsVolumeSlider";
            this.EffectsVolumeSlider.Size = new System.Drawing.Size(281, 42);
            this.EffectsVolumeSlider.TabIndex = 4;
            this.EffectsVolumeSlider.TickFrequency = 10;
            this.EffectsVolumeSlider.Value = 80;
            this.EffectsVolumeSlider.Scroll += new System.EventHandler(this.EffectsVolumeSlider_Scroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Effects Volume";
            // 
            // MasterVolumeSlider
            // 
            this.MasterVolumeSlider.Location = new System.Drawing.Point(114, 33);
            this.MasterVolumeSlider.Maximum = 100;
            this.MasterVolumeSlider.Name = "MasterVolumeSlider";
            this.MasterVolumeSlider.Size = new System.Drawing.Size(281, 42);
            this.MasterVolumeSlider.TabIndex = 3;
            this.MasterVolumeSlider.TickFrequency = 10;
            this.MasterVolumeSlider.Value = 80;
            this.MasterVolumeSlider.Scroll += new System.EventHandler(this.MasterVolumeSlider_Scroll);
            // 
            // lblBackgroundAlpha
            // 
            this.lblBackgroundAlpha.AutoSize = true;
            this.lblBackgroundAlpha.Location = new System.Drawing.Point(17, 33);
            this.lblBackgroundAlpha.Name = "lblBackgroundAlpha";
            this.lblBackgroundAlpha.Size = new System.Drawing.Size(77, 13);
            this.lblBackgroundAlpha.TabIndex = 15;
            this.lblBackgroundAlpha.Text = "Master Volume";
            // 
            // VideoGroup
            // 
            this.VideoGroup.Controls.Add(this.FullscreenCombo);
            this.VideoGroup.Controls.Add(this.FullscreenLabel);
            this.VideoGroup.Controls.Add(this.label4);
            this.VideoGroup.Controls.Add(this.ResolutionCombo);
            this.VideoGroup.Controls.Add(this.label3);
            this.VideoGroup.Controls.Add(this.MonitorCombo);
            this.VideoGroup.Location = new System.Drawing.Point(12, 12);
            this.VideoGroup.Name = "VideoGroup";
            this.VideoGroup.Size = new System.Drawing.Size(419, 157);
            this.VideoGroup.TabIndex = 15;
            this.VideoGroup.TabStop = false;
            this.VideoGroup.Text = "Video";
            // 
            // FullscreenCombo
            // 
            this.FullscreenCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FullscreenCombo.FormattingEnabled = true;
            this.FullscreenCombo.Location = new System.Drawing.Point(149, 109);
            this.FullscreenCombo.Name = "FullscreenCombo";
            this.FullscreenCombo.Size = new System.Drawing.Size(246, 21);
            this.FullscreenCombo.TabIndex = 2;
            this.FullscreenCombo.SelectedIndexChanged += new System.EventHandler(this.FullscreenCombo_SelectedIndexChanged);
            // 
            // FullscreenLabel
            // 
            this.FullscreenLabel.AutoSize = true;
            this.FullscreenLabel.Location = new System.Drawing.Point(17, 112);
            this.FullscreenLabel.Name = "FullscreenLabel";
            this.FullscreenLabel.Size = new System.Drawing.Size(111, 13);
            this.FullscreenLabel.TabIndex = 22;
            this.FullscreenLabel.Text = "Fullscreen/Windowed";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Screen Resolution";
            // 
            // ResolutionCombo
            // 
            this.ResolutionCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ResolutionCombo.FormattingEnabled = true;
            this.ResolutionCombo.Location = new System.Drawing.Point(149, 63);
            this.ResolutionCombo.Name = "ResolutionCombo";
            this.ResolutionCombo.Size = new System.Drawing.Size(246, 21);
            this.ResolutionCombo.TabIndex = 1;
            this.ResolutionCombo.SelectedIndexChanged += new System.EventHandler(this.ResolutionCombo_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Screen";
            // 
            // MonitorCombo
            // 
            this.MonitorCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MonitorCombo.FormattingEnabled = true;
            this.MonitorCombo.Location = new System.Drawing.Point(149, 23);
            this.MonitorCombo.Name = "MonitorCombo";
            this.MonitorCombo.Size = new System.Drawing.Size(246, 21);
            this.MonitorCombo.TabIndex = 0;
            this.MonitorCombo.SelectedIndexChanged += new System.EventHandler(this.MonitorCombo_SelectedIndexChanged);
            // 
            // SystemSettingForm
            // 
            this.AcceptButton = this.btnPlay;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.btnPlay;
            this.ClientSize = new System.Drawing.Size(441, 415);
            this.Controls.Add(this.VideoGroup);
            this.Controls.Add(this.AudioGroup);
            this.Controls.Add(this.btnPlay);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SystemSettingForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Jigsaw Engine - System Settings";
            ((System.ComponentModel.ISupportInitialize)(this.MusicVolumeSlider)).EndInit();
            this.AudioGroup.ResumeLayout(false);
            this.AudioGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EffectsVolumeSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MasterVolumeSlider)).EndInit();
            this.VideoGroup.ResumeLayout(false);
            this.VideoGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ColorDialog backgroundColorDialog;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.TrackBar MusicVolumeSlider;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox AudioGroup;
        private System.Windows.Forms.TrackBar EffectsVolumeSlider;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar MasterVolumeSlider;
        private System.Windows.Forms.Label lblBackgroundAlpha;
        private System.Windows.Forms.GroupBox VideoGroup;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox ResolutionCombo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox MonitorCombo;
        private System.Windows.Forms.Label FullscreenLabel;
        private System.Windows.Forms.ComboBox FullscreenCombo;
    }
}
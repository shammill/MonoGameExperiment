using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Engine.Utility;
using Engine.Settings;
using Engine.Utility.ScreenModels;
using Engine.Utility.ScreenInterrogatory;
using Engine.Localization;

namespace Engine
{
    public partial class SystemSettingForm : Form
    {
        private List<Monitor> Monitors = new List<Monitor>();
        private List<WindowOption> WindowOptions = new List<WindowOption>() { new Fullscreen(), new Windowed(), new BorderlessWindow() };
        //private DeviceManager Device;


        public SystemSettingForm()
        {
            //Device = device;
            InitializeComponent();
            PopulateSystemMonitorInformation();

            ResolutionCombo.SelectedIndexChanged -= ResolutionCombo_SelectedIndexChanged;

            PopulateMonitorCombo();
            PopulateFullScreenCombo();

            LoadMonitorComboSetting();
            PopulateResolutionCombo();
            LoadResolutionComboSetting();
            LoadFullscreenComboSetting();
            LoadAudioSettings();

            ResolutionCombo.SelectedIndexChanged += ResolutionCombo_SelectedIndexChanged;
        }

        // Gets possible resolutions, monitor names.
        public void PopulateSystemMonitorInformation()
        {
            Monitors = ScreenInterrogatory.GetMonitorInformation();
        }

        public void PopulateMonitorCombo()
        {
            if (Monitors.Count == 0)
            {
                var newMonitor = new Monitor("1") { Id = 1, FriendlyName = Local.DefaultMonitorName };
            }

            foreach (var monitor in Monitors)
            {
                MonitorCombo.Items.Add(new ComboBoxItem { Text = monitor.ComboName, Value = monitor.Name });
            }
        }

        public void PopulateResolutionCombo()
        {
            var selectedMonitor = MonitorCombo.SelectedItem as ComboBoxItem;
            var monitor = Monitors.Where(x => x.Name == selectedMonitor.Value.ToString()).First();
            ResolutionCombo.Items.Clear();
            foreach (Size resolution in monitor.Resolutions)
            {
                ResolutionCombo.Items.Add(new ComboBoxItem() { Text = resolution.ToResolutionString(), Value = resolution });
            }

            if (ResolutionCombo.Items.Count == 0)
            {
                ResolutionCombo.Items.Add(new ComboBoxItem() { Text = "No supported resolutions.", Value = new Size(0, 0) });
            }
        }

        public void PopulateFullScreenCombo()
        {
            foreach (var option in WindowOptions)
            {
                FullscreenCombo.Items.Add(option.Name);
            }
        }

        public void LoadMonitorComboSetting()
        {
            var value = SystemSettings.Default.Video_Monitor;

            if (value.IsNullOrEmpty() || !MonitorComboContainsValue(value))
                MonitorCombo.SelectedItem = MonitorCombo.Items[0];

            foreach (var item in MonitorCombo.Items)
            {
                if ((string)(item as ComboBoxItem).Value == value)
                {
                    MonitorCombo.SelectedItem = item;
                }
            }

        }

        public bool MonitorComboContainsValue(string value)
        {
            foreach (ComboBoxItem item in MonitorCombo.Items)
            {
                if (item.Value.ToString() == value)
                    return true;
            }
            return false;
        }


        public void LoadResolutionComboSetting()
        {
            var value = SystemSettings.Default.Video_Resolution;

            bool containsItem = false;
            foreach (var item in ResolutionCombo.Items)
            {
                if ((Size)(item as ComboBoxItem).Value == value)
                {
                    ResolutionCombo.SelectedItem = item;
                    containsItem = true;
                }
            }

            if (value == null || !containsItem)
                ResolutionCombo.SelectedItem = ResolutionCombo.Items[0];
        }

        public void LoadFullscreenComboSetting()
        {
            var fullscreenValue = SystemSettings.Default.Video_IsFullscreen;
            var borderValue = SystemSettings.Default.Video_IsBorderless;
            var option = ScreenInterrogatory.GetWindowOption(WindowOptions, fullscreenValue, borderValue);
            var optionName = option.Name;

            if (optionName.IsNullOrEmpty() || !FullscreenCombo.Items.Contains(optionName))
                FullscreenCombo.SelectedItem = FullscreenCombo.Items[0];

            FullscreenCombo.SelectedItem = optionName;
        }

        public void LoadAudioSettings()
        {
            MasterVolumeSlider.Value = SystemSettings.Default.Audio_MasterVolume;
            MusicVolumeSlider.Value = SystemSettings.Default.Audio_MusicVolume;
            EffectsVolumeSlider.Value = SystemSettings.Default.Audio_MusicVolume;
        }

        private void MonitorCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItem = MonitorCombo.SelectedItem as ComboBoxItem;
            string value = MonitorCombo.SelectedItem == null ? "" : selectedItem.Value.ToString();
            SystemSettings.Default.Video_Monitor = value;
            SystemSettings.Default.Save();

            PopulateResolutionCombo();
            ResolutionCombo.SelectedItem = ResolutionCombo.Items[0];
        }

        private void ResolutionCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItem = ResolutionCombo.SelectedItem as ComboBoxItem;
            if (selectedItem == null)
            {
                ResolutionCombo.SelectedItem = ResolutionCombo.Items[0];
            }
            SystemSettings.Default.Video_Resolution = (Size)selectedItem.Value;
            SystemSettings.Default.Save();
        }

        private void FullscreenCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var value = FullscreenCombo.SelectedItem.ToString();
            var selectedOption = WindowOptions.Where(x => x.Name.ToLower() == value.ToLower()).FirstOrDefault();
            SystemSettings.Default.Video_IsBorderless = selectedOption.IsBorderless;
            SystemSettings.Default.Video_IsFullscreen = selectedOption.IsFullscreen;
            SystemSettings.Default.Save();
        }

        private void MasterVolumeSlider_Scroll(object sender, EventArgs e)
        {
            SystemSettings.Default.Audio_MasterVolume = MasterVolumeSlider.Value;
            SystemSettings.Default.Save();
        }

        private void EffectsVolumeSlider_Scroll(object sender, EventArgs e)
        {
            SystemSettings.Default.Audio_EffectsVolume = EffectsVolumeSlider.Value;
            SystemSettings.Default.Save();
        }

        private void MusicVolumeSlider_Scroll(object sender, EventArgs e)
        {
            SystemSettings.Default.Audio_MusicVolume = MusicVolumeSlider.Value;
            SystemSettings.Default.Save();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            var chosenScreen = Screen.AllScreens.Where(x => x.DeviceName == SystemSettings.Default.Video_Monitor).FirstOrDefault();


            SystemSettings.Default.Video_Location = chosenScreen.Bounds.Location;
            SystemSettings.Default.Save();
            SystemSettings.Default.Reload();
            this.Hide();

            using (var game = new GameMain())
                game.Run();

            //MainMenuForm mainMenuForm = new MainMenuForm(Device);
            //mainMenuForm.Show();
        }
    }
}

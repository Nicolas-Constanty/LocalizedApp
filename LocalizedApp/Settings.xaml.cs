using System;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;

namespace LocalizedApp
{
    /// <summary>
    /// Logique d'interaction pour Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        private String[] _lang_array;
        private String[] _language_codes;

        public Settings()
        {
            Resources.MergedDictionaries.Add(Application.Current.MainWindow.Resources);
            InitializeComponent();
            LoadLangCodes();
            InitComboBoxLabel();
        }

        private void LoadLangCodes()
        {
            String language_codes = GetKey("language_codes");
            _language_codes = language_codes.Split(',');
        }

        private void InitComboBoxLabel()
        {
            ComboBox_Languages.SelectionChanged -= ComboBox_Languages_SelectionChanged;
            String languages = GetKey("languages");
            if (!String.IsNullOrEmpty(languages))
            {
                _lang_array = languages.Split(',');
                for (int i = 0; i < _lang_array.Length; i++)
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.SetResourceReference(ComboBoxItem.ContentProperty, "_" + _lang_array[i]);
                    ComboBox_Languages.Items.Add(item);
                }
            }
            ComboBox_Languages.Text = FindResource("_Culture_Lang").ToString();
            ComboBox_Languages.SelectionChanged += ComboBox_Languages_SelectionChanged;
        }

        private void ComboBox_Languages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            App app = App.Current as App;
            ComboBox cmb = sender as ComboBox;
            LanguagesHandler.Instance.LoadLanguage(_language_codes[cmb.SelectedIndex], this);
        }

        static public String GetKey(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                String result = appSettings[key] ?? null;
                return result;
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
                return null;
            }
        }
        static public void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}

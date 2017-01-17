using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
            String language_codes = App.GetSetting("language_codes");
            _language_codes = language_codes.Split(',');
        }

        private void InitComboBoxLabel()
        {
            ComboBox_Languages.SelectionChanged -= ComboBox_Languages_SelectionChanged;
            String languages = App.GetSetting("languages");
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
            app.LoadLanguage(_language_codes[cmb.SelectedIndex], this);
        }
    }
}

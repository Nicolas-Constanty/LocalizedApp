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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LocalizedApp
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Settings _settings_windows = null;

        public MainWindow()
        {
            InitializeComponent();
            App app = (App)Application.Current;
            app.LoadConfig();
            //LoadDynamicResource("Languages\\en-EN.xaml");
        }
        private void LoadDynamicResource(String StyleToUse)
        {

            ResourceDictionary dic = new ResourceDictionary { Source = new Uri(StyleToUse, UriKind.Relative) };
            Resources.MergedDictionaries.Clear();
            Resources.MergedDictionaries.Add(dic);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (_settings_windows == null)
                _settings_windows = new Settings();
            if (_settings_windows.Visibility != Visibility.Visible)
                _settings_windows.Show();
            else
                _settings_windows.Hide();
        }
    }
}

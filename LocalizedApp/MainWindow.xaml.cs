using System;
using System.Windows;

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
            LanguagesHandler.Instance.LoadConfig();
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
                _settings_windows.Visibility = Visibility.Hidden;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_settings_windows != null)
            {
                _settings_windows.CloseSetting();
                _settings_windows.Close();
            }
        }
    }
}

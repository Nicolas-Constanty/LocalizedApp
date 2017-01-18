using Common;
using System;
using System.Threading;
using System.Windows;

namespace LocalizedApp
{
    class LanguagesHandler : Common.DesignPattern.Singleton<LanguagesHandler>
    {
        private MainWindow _main_window = null;

        public LanguagesHandler()
        {
            if (Application.Current.MainWindow != null)
                _main_window = Application.Current.MainWindow as MainWindow;
            else
                Debug.Warning("Main Window is null, you shoud call SetMainWindow");
        }

        public void SetMainWindow(MainWindow main_window)
        {
            if (main_window == null)
                Debug.Warning("Main Window is null");
            _main_window = main_window;
        }

        public void LoadConfig()
        {
            if (_main_window != null)
            {
                String user_settings = Settings.GetKey("lang");
                if (String.IsNullOrEmpty(user_settings))
                    SetDefaultLanguageDictionary();
                else
                {
                    Console.WriteLine(user_settings);
                    ResourceDictionary dict = new ResourceDictionary();
                    dict.Source = new Uri("Languages\\" + user_settings + ".xaml", UriKind.Relative);
                    Application.Current.Resources.MergedDictionaries.Clear();
                    Application.Current.Resources.MergedDictionaries.Add(dict);
                    _main_window.Resources.MergedDictionaries.Clear();
                    _main_window.Resources.MergedDictionaries.Add(dict);
                }
            }
            else
                Debug.Warning("Main Window is null, you shoud call SetMainWindow");
        }

        public void LoadLanguage(String name)
        {
            if (_main_window != null)
            {
                ResourceDictionary dict = new ResourceDictionary();
                dict.Source = new Uri("Languages\\" + name + ".xaml", UriKind.Relative);
                Application.Current.Resources.MergedDictionaries.Clear();
                Application.Current.Resources.MergedDictionaries.Add(dict);
                _main_window.Resources.MergedDictionaries.Clear();
                _main_window.Resources.MergedDictionaries.Add(dict);
                Settings.AddUpdateAppSettings("lang", name);
            }
            else
                Debug.Warning("Main Window is null, you shoud call SetMainWindow");
        }

        public void LoadLanguage(String name, Window w)
        {
            if (_main_window != null)
            {
                ResourceDictionary dict = new ResourceDictionary();
                dict.Source = new Uri("Languages\\" + name + ".xaml", UriKind.Relative);
                Application.Current.Resources.MergedDictionaries.Clear();
                Application.Current.Resources.MergedDictionaries.Add(dict);
                 _main_window.Resources.MergedDictionaries.Clear();
                 _main_window.Resources.MergedDictionaries.Add(dict);
                if (w != null)
                {
                    w.Resources.MergedDictionaries.Clear();
                    w.Resources.MergedDictionaries.Add(dict);
                }
                Settings.AddUpdateAppSettings("lang", name);
            }
            else
                Debug.Warning("Main Window is null, you shoud call SetMainWindow");
        }

        private void SetDefaultLanguageDictionary()
        {
            if (_main_window != null)
            {
                ResourceDictionary dict = new ResourceDictionary();
                String current_culture = Thread.CurrentThread.CurrentCulture.ToString();
                switch (current_culture)
                {
                    case "en-EN":
                        dict.Source = new Uri("Languages\\" + current_culture + ".xaml", UriKind.Relative);
                        Settings.AddUpdateAppSettings("lang", current_culture);
                        break;
                    case "fr-FR":
                        dict.Source = new Uri("Languages\\" + current_culture + ".xaml", UriKind.Relative);
                        Settings.AddUpdateAppSettings("lang", current_culture);
                        break;
                    default:
                        dict.Source = new Uri("Languages\\en-EN.xaml", UriKind.Relative);
                        Settings.AddUpdateAppSettings("lang", "en-FR");
                        break;
                }
                Application.Current.Resources.MergedDictionaries.Clear();
                Application.Current.Resources.MergedDictionaries.Add(dict);
                _main_window.Resources.MergedDictionaries.Clear();
                _main_window.Resources.MergedDictionaries.Add(dict);
            }
            else
                Debug.Warning("Main Window is null, you shoud call SetMainWindow");
        }
        
    }
}

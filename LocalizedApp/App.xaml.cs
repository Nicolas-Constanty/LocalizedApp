using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace LocalizedApp
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        }

        public void LoadConfig()
        {
            String user_settings = GetSetting("lang");
            if (String.IsNullOrEmpty(user_settings))
                SetDefaultLanguageDictionary();
            else
            {
                Console.WriteLine(user_settings);
                ResourceDictionary dict = new ResourceDictionary();
                dict.Source = new Uri("Languages\\" + user_settings + ".xaml", UriKind.Relative);
                Application.Current.Resources.MergedDictionaries.Clear();
                Application.Current.Resources.MergedDictionaries.Add(dict);
                if (MainWindow != null)
                {
                    MainWindow.Resources.MergedDictionaries.Clear();
                    MainWindow.Resources.MergedDictionaries.Add(dict);
                }
            }
        }

        public void LoadLanguage(String name)
        {
            ResourceDictionary dict = new ResourceDictionary();
            dict.Source = new Uri("Languages\\" + name + ".xaml", UriKind.Relative);
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(dict);
            if (MainWindow != null)
            {
                MainWindow.Resources.MergedDictionaries.Clear();
                MainWindow.Resources.MergedDictionaries.Add(dict);
            }
        }

        public void LoadLanguage(String name, Window w)
        {
            ResourceDictionary dict = new ResourceDictionary();
            dict.Source = new Uri("Languages\\" + name + ".xaml", UriKind.Relative);
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(dict);
            if (MainWindow != null)
            {
                MainWindow.Resources.MergedDictionaries.Clear();
                MainWindow.Resources.MergedDictionaries.Add(dict);
            }
            if (w != null)
            {
                w.Resources.MergedDictionaries.Clear();
                w.Resources.MergedDictionaries.Add(dict);
            }
        }

        private void SetDefaultLanguageDictionary()
        {
            if (MainWindow != null)
            {
                ResourceDictionary dict = new ResourceDictionary();
                String current_culture = Thread.CurrentThread.CurrentCulture.ToString();
                switch (current_culture)
                {
                    case "en-EN":
                        dict.Source = new Uri("Languages\\" + current_culture + ".xaml", UriKind.Relative);
                        AddUpdateAppSettings("lang", current_culture);
                        break;
                    case "fr-FR":
                        dict.Source = new Uri("Languages\\" + current_culture + ".xaml", UriKind.Relative);
                        AddUpdateAppSettings("lang", current_culture);
                        break;
                    default:
                        dict.Source = new Uri("Languages\\en-EN.xaml", UriKind.Relative);
                        AddUpdateAppSettings("lang", "en-FR");
                        break;
                }
                Application.Current.Resources.MergedDictionaries.Clear();
                Application.Current.Resources.MergedDictionaries.Add(dict);
                MainWindow.Resources.MergedDictionaries.Clear();
                MainWindow.Resources.MergedDictionaries.Add(dict);
            }
            
        }
        public static String GetSetting(string key)
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
        static void AddUpdateAppSettings(string key, string value)
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
    }
}

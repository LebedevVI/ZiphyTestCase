using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.
namespace TestCase
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnKeyDownHandler(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                OnGoButtonClick(sender, e);
            }
        }

        private async void OnGoButtonClick(object sender, RoutedEventArgs e)
        {
            Uri uri;
            if (MakeUri(Source.Text, out uri))
            { 
                
                Browser.Source = uri;
                Browser.GoForward();
            }
            else
            {
                ContentDialog invalideSourceDialog = new ContentDialog
                {
                    Title = "Некорректный адрес",
                    Content = "Проверьте правильность ввода",
                    CloseButtonText = "Ok"
                };
                invalideSourceDialog.XamlRoot = this.Content.XamlRoot;
                ContentDialogResult result = await invalideSourceDialog.ShowAsync();
            }
        }

        public bool MakeUri(string source, out Uri uri)
        {
            try
            {
                if (!source.Contains("http://") && !source.Contains("https://"))
                {
                    source = "https://" + source;
                }
                uri = new Uri(source);
                Source.Text = source;
                return true;
            }
            catch
            {
                uri = null;
                return false;
            }
        }
    }
}

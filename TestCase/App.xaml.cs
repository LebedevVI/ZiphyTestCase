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
using Microsoft.UI.Xaml.Shapes;

using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;

using System.Threading;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.
namespace TestCase
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            _mWindow = new MainWindow();
            _mWindow.Activate();
            if (!MutexCheck())
            {
                if (_mWindow.Content is FrameworkElement fe)
                {
                    fe.Loaded += (ss, ee) => MultiAppError();
                }

            }
        }

        static Mutex Mutex;
        static bool MutexCheck()
        {
            bool isNew;
            Mutex = new Mutex(true, "TestCaseZiphy", out isNew);
            return isNew;
        }

        protected async void MultiAppError()
        {
            ContentDialog multiAppDialog = new ContentDialog
            {
                Title = "Программа уже запущена",
                Content = "Закройте и попробуйте вновь",
                CloseButtonText = "Ok"
            };
            multiAppDialog.XamlRoot = _mWindow.Content.XamlRoot;
            ContentDialogResult result = await multiAppDialog.ShowAsync();
            Application.Current.Exit();
        }

        private Window _mWindow;
    }
}

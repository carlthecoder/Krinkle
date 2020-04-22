using System;
using Utilities.Logging;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace KrinklesHell
{
    sealed partial class App : Application
    {
        private static string deviceFamily;
        private ILogger _logger;

        /// <summary>
        /// The logical equivalent of main() or WinMain
        /// Initializes the singleton application object.
        /// </summary>
        public App()
        {
            InitializeComponent();
            Suspending += OnSuspending;
            Resuming += OnResuming;

            //API check to ensure the "RequiresPointerMode" property exists, ensuring project is running on build 14393 or later
            if (Windows.Foundation.Metadata.ApiInformation.IsPropertyPresent("Windows.UI.Xaml.Application", "RequiresPointerMode"))
            {
                //If running on the Xbox, disable the default on screen pointer
                if (IsXbox())
                {
                    Application.Current.RequiresPointerMode = ApplicationRequiresPointerMode.WhenRequested;
                }
            }

            // Initiate Ioc for dependency injection
            IocRegister.Initialize();
            _logger = Ioc.Resolve<ILogger>();
        }

        /// <summary>
        /// Detection code in Windows 10 to identify the platform it is being run on
        /// This function returns true if the project is running on an XboxOne
        /// </summary>
        public static bool IsXbox()
        {
            if (deviceFamily == null)
                deviceFamily = Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily;

            return deviceFamily == "Windows.Xbox";
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            // By default we want to fill the entire core window.
            var view = ApplicationView.GetForCurrentView();
            view.SetDesiredBoundsMode(ApplicationViewBoundsMode.UseCoreWindow);

            //#if DEBUG
            //            if (System.Diagnostics.Debugger.IsAttached)
            //            {
            //                this.DebugSettings.EnableFrameRateCounter = true;
            //            }
            //#endif

            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                rootFrame.Navigate(typeof(GamePage), e.Arguments);
            }

            // Ensure the current window is active
            Window.Current.Activate();

            _logger.Trace("Window Activated - Application running!");
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            _logger.Trace("Suspending application");

            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity

            deferral.Complete();
        }

        /// <summary>
        /// Invoked when application execution is resumed,
        /// without knowing whether the application will be resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the resume request.</param>
        /// <param name="e">Details about the resume request.</param>
        private void OnResuming(object sender, object e)
        {
            _logger.Trace("Resumed application.");
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Gaming.XboxGameBar;

using System.Diagnostics;
using Microsoft.Gaming.XboxGameBar.Authentication;

using Windows.UI.Xaml.Shapes;

namespace Gamebar_Crosshair
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Crosshair : Page
    {
        //private XboxGameBarWidget widget = null;
        //private XboxGameBarWidgetControl widgetControl = null;

        //Rectangle vertical_line = new Rectangle();
        //Rectangle horizontal_line = new Rectangle();

        //Windows.UI.Color color = Windows.UI.Colors.Red;
        //double length = 50;
        //double thickness = 1;

        //private void LengthSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        //{
        //    Slider slider = sender as Slider;
        //    if (slider != null)
        //    {
        //        length = slider.Value;
        //    }

        //    vertical_line.Height = length;
        //    horizontal_line.Width = length;
        //}

        //private void ThicknessSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        //{
        //    Slider slider = sender as Slider;
        //    if (slider != null)
        //    {
        //        thickness = slider.Value;
        //    }

        //    vertical_line.Width = thickness;
        //    horizontal_line.Height = thickness;
        //}

        private XboxGameBarWidget widget = null;
        private XboxGameBarWidgetControl widgetControl = null;
        private XboxGameBarWebAuthenticationBroker gameBarWebAuth = null;
        private SolidColorBrush widgetBlackBrush = null;
        private SolidColorBrush widgetWhiteBrush = null;

        public Crosshair()
        {
            this.InitializeComponent();

            //Slider lengthSlider = new Slider();
            //lengthSlider.Header = "Length";
            //lengthSlider.Width = 200;
            //lengthSlider.Minimum = 1;
            //lengthSlider.Maximum = 150;
            //lengthSlider.Value = length;
            //lengthSlider.ValueChanged += LengthSlider_ValueChanged;
            //layoutRoot.Children.Add(lengthSlider);

            ////Slider thicknessSlider = new Slider();
            ////thicknessSlider.Header = "Thickness";
            ////thicknessSlider.Width = 200;
            ////thicknessSlider.ValueChanged += ThicknessSlider_ValueChanged;
            ////layoutRoot.Children.Add(thicknessSlider);

            //vertical_line.Fill = new SolidColorBrush(color);
            //vertical_line.Width = thickness;
            //vertical_line.Height = length;
            //layoutRoot.Children.Add(vertical_line);

            //horizontal_line.Fill = new SolidColorBrush(color);
            //horizontal_line.Width = length;
            //horizontal_line.Height = thickness;
            //layoutRoot.Children.Add(horizontal_line);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            widget = e.Parameter as XboxGameBarWidget;
            widgetControl = new XboxGameBarWidgetControl(widget);
            gameBarWebAuth = new XboxGameBarWebAuthenticationBroker(widget);

            widgetBlackBrush = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 38, 38, 38));
            widgetWhiteBrush = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 219, 219, 219));

            // Hook up events for when the ui is updated.
            widget.SettingsClicked += Widget_SettingsClicked;
            widget.PinnedChanged += Widget_PinnedChanged;
            widget.FavoritedChanged += Widget_FavoritedChanged;
            widget.RequestedOpacityChanged += Widget_RequestedOpacityChanged;
            widget.RequestedThemeChanged += Widget_RequestedThemeChanged;
            widget.VisibleChanged += Widget_VisibleChanged;
            widget.WindowStateChanged += Widget_WindowStateChanged;
            widget.GameBarDisplayModeChanged += Widget_GameBarDisplayModeChanged;

            SetPinnedStateTextBox();
            SetFavoritedState();
            SetRequestedOpacityState();
            SetRequestedThemeState();
            OutputVisibleState();
            OutputWindowState();
            OutputGameBarDisplayMode();
            SetBackgroundColor();
            SetBackgroundOpacity();

            HorizontalResizeSupportedCheckBox.IsChecked = widget.HorizontalResizeSupported;
            VerticalResizeSupportedCheckBox.IsChecked = widget.VerticalResizeSupported;
            PinningSupportedCheckBox.IsChecked = widget.PinningSupported;
            SettingsSupportedCheckBox.IsChecked = widget.SettingsSupported;

            MinWindowHeightBox.Text = widget.MinWindowSize.Height.ToString();
            MinWindowWidthBox.Text = widget.MinWindowSize.Width.ToString();
            MaxWindowHeightBox.Text = widget.MaxWindowSize.Height.ToString();
            MaxWindowWidthBox.Text = widget.MaxWindowSize.Width.ToString();
        }

        private async void ActivateAsyncAppExtIdButton_Click(object sender, RoutedEventArgs e)
        {
            String text = ActivateAsyncAppExtId.Text;
            await widgetControl.ActivateAsync(text);
        }

        private async void ActivateAsyncAppIdButton_Click(object sender, RoutedEventArgs e)
        {
            await widgetControl.ActivateAsync(ActivateAsyncAppId.Text, ActivateAsyncAppExtId.Text);
        }

        private async void ActivateWithUriAsyncButton_Click(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri(ActivateAsyncUri.Text);
            await widgetControl.ActivateWithUriAsync(uri);
        }

        private async void MinimizeAsyncAppIdButton_Click(object sender, RoutedEventArgs e)
        {
            await widgetControl.MinimizeAsync(ActivateAsyncAppExtId.Text);
        }

        private async void MinimizeAsyncAppExtIdButton_Click(object sender, RoutedEventArgs e)
        {
            await widgetControl.MinimizeAsync(ActivateAsyncAppId.Text, ActivateAsyncAppExtId.Text);
        }

        private async void RestoreAsyncAppExtIdButton_Click(object sender, RoutedEventArgs e)
        {
            await widgetControl.RestoreAsync(ActivateAsyncAppExtId.Text);
        }

        private async void RestoreAsyncAppIdButton_Click(object sender, RoutedEventArgs e)
        {
            await widgetControl.RestoreAsync(ActivateAsyncAppId.Text, ActivateAsyncAppExtId.Text);
        }

        private async void CloseAsyncAppExtIdButton_Click(object sender, RoutedEventArgs e)
        {
            await widgetControl.CloseAsync(ActivateAsyncAppExtId.Text);
        }

        private async void CloseAsyncAppIdButton_Click(object sender, RoutedEventArgs e)
        {
            await widgetControl.CloseAsync(ActivateAsyncAppId.Text, ActivateAsyncAppExtId.Text);
        }

        private async void TryResizeWindowAsync_Click(object sender, RoutedEventArgs e)
        {
            Windows.Foundation.Size size;
            size.Height = int.Parse(WindowHeightBox.Text);
            size.Width = int.Parse(WindowWidthBox.Text);
            await widget.TryResizeWindowAsync(size);
        }

        private async void AuthenticateAsync_Click(object sender, RoutedEventArgs e)
        {
            if (RequestUriBox.Text == "" || CallbackUriBox.Text == "")
            {
                return;
            }

            Uri requestUri = new Uri(RequestUriBox.Text);
            Uri callbackUri = new Uri(CallbackUriBox.Text);
            XboxGameBarWebAuthenticationResult result = await gameBarWebAuth.AuthenticateAsync(
                XboxGameBarWebAuthenticationOptions.None,
                requestUri,
                callbackUri);

            Debug.WriteLine("ResponseData: " + result.ResponseData);
            Debug.WriteLine("ResponseStatus: " + result.ResponseStatus.ToString());
            Debug.WriteLine("ResponseErrorDetail: " + result.ResponseErrorDetail);
        }

        private void HorizontalResizeSupportedCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            widget.HorizontalResizeSupported = true;
        }

        private void HorizontalResizeSupportedCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            widget.HorizontalResizeSupported = false;
        }

        private void VerticalResizeSupportedCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            widget.VerticalResizeSupported = true;
        }

        private void VerticalResizeSupportedCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            widget.VerticalResizeSupported = false;
        }

        private void PinningSupportedCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            widget.PinningSupported = true;
        }

        private void PinningSupportedCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            widget.PinningSupported = false;
        }

        private void SettingsSupportedCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            widget.SettingsSupported = true;
        }

        private void SettingsSupportedCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            widget.SettingsSupported = false;
        }

        private void MinWindowSize_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Size size;
                size.Height = Convert.ToDouble(MinWindowHeightBox.Text);
                size.Width = Convert.ToDouble(MinWindowWidthBox.Text);
                widget.MinWindowSize = size;
            }
            catch (FormatException)
            {
                Debug.WriteLine("Text box must contain valid number");
            }
        }

        private void MaxWindowSize_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Size size;
                size.Height = Convert.ToDouble(MaxWindowHeightBox.Text);
                size.Width = Convert.ToDouble(MaxWindowWidthBox.Text);
                widget.MaxWindowSize = size;
            }
            catch (FormatException)
            {
                Debug.WriteLine("Text box must contain valid number");
            }
        }

        private async void Widget_SettingsClicked(XboxGameBarWidget sender, object args)
        {
            await widget.ActivateSettingsAsync();
        }

        private async void Widget_PinnedChanged(XboxGameBarWidget sender, object args)
        {
            await PinnedStateTextBlock.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                SetPinnedStateTextBox();
            });
        }

        private async void Widget_FavoritedChanged(XboxGameBarWidget sender, object args)
        {
            await FavoritedTextBlock.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                SetFavoritedState();
            });
        }

        private async void Widget_RequestedOpacityChanged(XboxGameBarWidget sender, object args)
        {
            await RequestedOpacityTextBlock.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                SetRequestedOpacityState();
                SetBackgroundOpacity();
            });
        }

        private async void Widget_RequestedThemeChanged(XboxGameBarWidget sender, object args)
        {
            await RequestedThemeTextBlock.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                SetRequestedThemeState();
                SetBackgroundColor();
            });
        }

        private void Widget_VisibleChanged(XboxGameBarWidget sender, object args)
        {
            OutputVisibleState();
        }

        private void Widget_WindowStateChanged(XboxGameBarWidget sender, object args)
        {
            OutputWindowState();
        }

        private void Widget_GameBarDisplayModeChanged(XboxGameBarWidget sender, object args)
        {
            OutputGameBarDisplayMode();
        }

        private void SetPinnedStateTextBox()
        {
            PinnedStateTextBlock.Text = widget.Pinned.ToString();
        }

        private void SetFavoritedState()
        {
            FavoritedTextBlock.Text = widget.Favorited.ToString();
        }

        private void SetBackgroundColor()
        {
            this.RequestedTheme = widget.RequestedTheme;
            BackgroundGrid.Background = (widget.RequestedTheme == ElementTheme.Dark) ? widgetBlackBrush : widgetWhiteBrush;
        }

        private void SetBackgroundOpacity()
        {
            BackgroundGrid.Opacity = widget.RequestedOpacity;
        }

        private void SetRequestedOpacityState()
        {
            RequestedOpacityTextBlock.Text = widget.RequestedOpacity.ToString();
        }

        private void SetRequestedThemeState()
        {
            RequestedThemeTextBlock.Text = widget.RequestedTheme.ToString();
        }

        private void OutputVisibleState()
        {
            Debug.WriteLine("Visible: " + widget.Visible.ToString());
        }

        private void OutputWindowState()
        {
            Debug.WriteLine("Window State: " + widget.WindowState.ToString());
        }

        private void OutputGameBarDisplayMode()
        {
            Debug.WriteLine("Game Bar View Mode: " + widget.GameBarDisplayMode.ToString());
        }

        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{
        //    // you will need access to the XboxGameBarWidget, in this case it was passed as a parameter when navigating to the widget page, your implementation may differ.
        //    XboxGameBarWidget widget = e.Parameter as XboxGameBarWidget;
        //    XboxGameBarWidgetControl widgetControl = new XboxGameBarWidgetControl(widget);

        //    //widget.SettingsClicked += Widget_SettingsClicked;

        //    // subscribe for RequestedOpacityChanged events
        //    //widget.RequestedOpacityChanged += Widget_RequestedOpacityChanged;
        //}

        //private async void Widget_SettingsClicked(XboxGameBarWidget sender, object args)
        //{
        //    // if necessary pre-configure any required data needed by the settings widget prior to activation
        //    // ...

        //    await sender.ActivateSettingsAsync();
        //}

        //private async void Widget_RequestedOpacityChanged(XboxGameBarWidget sender, object args)
        //{
        //    // be sure to dispatch to the correct UI thread for this widget, Game Bar events are not guaranteed to come in on the same thread.
        //    await RequestedOpacityTextBlock.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
        //    {
        //        // adjust the opacity of your background as appropriate
        //        layoutRoot.Opacity = widget.RequestedOpacity;
        //    });
        //}
    }
}
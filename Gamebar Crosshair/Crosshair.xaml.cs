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
using Windows.UI.Xaml.Shapes;
using Microsoft.Gaming.XboxGameBar;
using System.Diagnostics;
using Microsoft.Gaming.XboxGameBar.Authentication;
using Windows.UI.Core;

namespace Gamebar_Crosshair
{

    public sealed partial class Crosshair : Page
    {
        private XboxGameBarWidget widget = null;

        static Rectangle verticalLine = new Rectangle();
        static Rectangle horizontalLine = new Rectangle();

        public Crosshair()
        {
            this.InitializeComponent();

            layoutRoot.Children.Add(verticalLine);
            layoutRoot.Children.Add(horizontalLine);

            drawCrosshair();
        }

        static void drawCrosshair()
        {
            verticalLine.Fill = new SolidColorBrush(CrosshairData.color);
            verticalLine.Width = CrosshairData.thickness;
            verticalLine.Height = CrosshairData.length;

            horizontalLine.Fill = new SolidColorBrush(CrosshairData.color);
            horizontalLine.Width = CrosshairData.length;
            horizontalLine.Height = CrosshairData.thickness;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            widget = e.Parameter as XboxGameBarWidget;

            widget.SettingsClicked += Widget_SettingsClicked;
        }

        private async void Widget_SettingsClicked(XboxGameBarWidget sender, object args)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                drawCrosshair();
            });

            await widget.ActivateSettingsAsync();
        }
    }
}
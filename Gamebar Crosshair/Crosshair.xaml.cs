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

namespace Gamebar_Crosshair
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Crosshair : Page
    {
        Rectangle vertical_line = new Rectangle();
        Rectangle horizontal_line = new Rectangle();

        Windows.UI.Color color = Windows.UI.Colors.Red;
        double length = 50;
        double thickness = 1;

        private void LengthSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            Slider slider = sender as Slider;
            if (slider != null)
            {
                length = slider.Value;
            }

            vertical_line.Height = length;
            horizontal_line.Width = length;
        }

        private void ThicknessSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            Slider slider = sender as Slider;
            if (slider != null)
            {
                thickness = slider.Value;
            }

            vertical_line.Width = thickness;
            horizontal_line.Height = thickness;
        }

        public Crosshair()
        {
            this.InitializeComponent();

            Slider lengthSlider = new Slider();
            lengthSlider.Header = "Length";
            lengthSlider.Width = 200;
            lengthSlider.Minimum = 1;
            lengthSlider.Maximum = 150;
            lengthSlider.Value = length;
            lengthSlider.ValueChanged += LengthSlider_ValueChanged;
            layoutRoot.Children.Add(lengthSlider);

            //Slider thicknessSlider = new Slider();
            //thicknessSlider.Header = "Thickness";
            //thicknessSlider.Width = 200;
            //thicknessSlider.ValueChanged += ThicknessSlider_ValueChanged;
            //layoutRoot.Children.Add(thicknessSlider);

            vertical_line.Fill = new SolidColorBrush(color);
            vertical_line.Width = thickness;
            vertical_line.Height = length;
            layoutRoot.Children.Add(vertical_line);

            horizontal_line.Fill = new SolidColorBrush(color);
            horizontal_line.Width = length;
            horizontal_line.Height = thickness;
            layoutRoot.Children.Add(horizontal_line);
        }
    }
}
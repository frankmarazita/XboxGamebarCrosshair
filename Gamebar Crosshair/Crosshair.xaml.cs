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

    public sealed partial class Crosshair : Page
    {
        Rectangle verticalLine = new Rectangle();
        Rectangle horizontalLine = new Rectangle();

        Windows.UI.Color color = Windows.UI.Colors.Red;
        double length = 50;
        double thickness = 1;

        private void lengthSliderValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
			if (sender is Slider slider)
            {
                length = slider.Value;
            }

            verticalLine.Height = length;
            horizontalLine.Width = length;
        }

        private void thicknessSliderValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
			if (sender is Slider slider)
            {
                thickness = slider.Value;
            }

            verticalLine.Width = thickness;
            horizontalLine.Height = thickness;
        }

        public Crosshair()
        {
            this.InitializeComponent();

			Slider lengthSlider = new Slider
			{
				Header = "Length",
				Width = 200,
				Minimum = 1,
				Maximum = 150,
				Value = length
			};
			lengthSlider.ValueChanged += lengthSliderValueChanged;
            layoutRoot.Children.Add(lengthSlider);

			Slider thicknessSlider = new Slider
			{
				Header = "Thickness", Width = 200
			};
			thicknessSlider.ValueChanged += thicknessSliderValueChanged;
			layoutRoot.Children.Add(thicknessSlider);

			verticalLine.Fill = new SolidColorBrush(color);
			verticalLine.Width = thickness;
			verticalLine.Height = length;
            layoutRoot.Children.Add(verticalLine);

            horizontalLine.Fill = new SolidColorBrush(color);
			horizontalLine.Width = length;
			horizontalLine.Height = thickness;
            layoutRoot.Children.Add(horizontalLine);
        }
    }
}
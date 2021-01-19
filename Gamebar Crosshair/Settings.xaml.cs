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

namespace Gamebar_Crosshair
{

    public sealed partial class Settings : Page
    {
		Rectangle verticalLine = new Rectangle();
		Rectangle horizontalLine = new Rectangle();

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

		public Settings()
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
			layoutLength.Children.Add(lengthSlider);

			Slider thicknessSlider = new Slider
			{
				Header = "Thickness",
				Width = 200
			};
			thicknessSlider.ValueChanged += thicknessSliderValueChanged;
			layoutThickness.Children.Add(thicknessSlider);
		}


    }
}
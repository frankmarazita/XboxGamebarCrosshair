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

		private void lengthSliderValueChanged(object sender, RangeBaseValueChangedEventArgs e)
		{
			if (sender is Slider slider)
			{
				CrosshairData.length = slider.Value;
			}

			verticalLine.Height = CrosshairData.length;
			horizontalLine.Width = CrosshairData.length;
		}

		private void thicknessSliderValueChanged(object sender, RangeBaseValueChangedEventArgs e)
		{
			if (sender is Slider slider)
			{
				CrosshairData.thickness = slider.Value;
			}

			verticalLine.Width = CrosshairData.thickness;
			horizontalLine.Height = CrosshairData.thickness;
		}

		private void colorPickerColorChanged(object sender, ColorChangedEventArgs e)
        {
			if (sender is ColorPicker colorPicker)
            {
                 CrosshairData.color = colorPicker.Color;
            }
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
				Value = CrosshairData.length
			};
			lengthSlider.ValueChanged += lengthSliderValueChanged;
			layoutLength.Children.Add(lengthSlider);

			Slider thicknessSlider = new Slider
			{
				Header = "Thickness",
				Width = 200,
				Minimum = 1,
				Maximum = 150,
				Value = CrosshairData.thickness
			};
			thicknessSlider.ValueChanged += thicknessSliderValueChanged;
			layoutThickness.Children.Add(thicknessSlider);

			ColorPicker colorPicker = new ColorPicker();
			colorPicker.ColorChanged += colorPickerColorChanged;
			myColorPicker.Children.Add(colorPicker);
		}
    }
}
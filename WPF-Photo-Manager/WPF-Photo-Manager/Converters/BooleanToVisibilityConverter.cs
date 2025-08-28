using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WPF_Photo_Manager.Converters
{
    /// <summary>
    /// Converts a boolean value to a Visibility value.
    /// True becomes Visible, False becomes Collapsed.
    /// </summary>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BooleanToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Converts a boolean value to a Visibility value.
        /// </summary>
        /// <param name="value">The boolean value to convert (e.g., IsLoading).</param>
        /// <param name="targetType">The type of the target property (should be Visibility).</param>
        /// <param name="parameter">An optional parameter.</param>
        /// <param name="culture">The culture information.</param>
        /// <returns>Visibility.Visible if value is true, otherwise Visibility.Collapsed.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isVisible && isVisible)
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        /// <summary>
        /// Converts a Visibility value back to a boolean.
        /// This method is not used in this application but is required by the interface.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}

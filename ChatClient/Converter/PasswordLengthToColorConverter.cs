using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace ChatClient.Converter
{
    class PasswordLengthToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var passwordLength = (int)value;
            return (passwordLength < 3) ? Brushes.Red :
                (passwordLength < 5) ? Brushes.Orange :
                (passwordLength < 7) ? Brushes.Yellow :
                (passwordLength < 9) ? Brushes.GreenYellow : Brushes.Lime;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

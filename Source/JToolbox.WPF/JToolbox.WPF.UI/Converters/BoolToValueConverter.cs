﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace JToolbox.WPF.UI.Converters
{
    public class BoolToValueConverter<T> : IValueConverter
    {
        public BoolToValueConverter()
        {
        }

        public BoolToValueConverter(T trueValue, T falseValue)
        {
            FalseValue = falseValue;
            TrueValue = trueValue;
        }

        public T FalseValue { get; set; }
        public T TrueValue { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return FalseValue;
            else
                return Convert((bool)value);
        }

        public T Convert(bool value)
        {
            return value ? TrueValue : FalseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null ? value.Equals(TrueValue) : false;
        }
    }
}
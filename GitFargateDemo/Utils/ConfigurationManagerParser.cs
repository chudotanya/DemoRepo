using System;
using System.Configuration;

namespace GitFargateDemo.Utils
{
    public static class ConfigurationManagerParser
    {
        #region Methods

        public static TEnum GetEnumProperty<TEnum>(string propertyName, TEnum defaultValue) where TEnum : struct
        {
            var valueProp = ConfigurationManager.AppSettings[propertyName];

            if (string.IsNullOrEmpty(valueProp) ||
                !Enum.TryParse(valueProp, out TEnum value))
            {
                value = defaultValue;
            }

            return value;
        }

        public static bool GetBooleanProperty(string propertyName, bool defaultValue)
        {
            var valueProp = ConfigurationManager.AppSettings[propertyName];

            if (string.IsNullOrEmpty(valueProp) ||
                !bool.TryParse(valueProp, out var value))
            {
                value = defaultValue;
            }

            return value;
        }

        public static int GetIntProperty(string propName, int defaultPropValue)
        {
            var prop = ConfigurationManager.AppSettings[propName];
            if (string.IsNullOrEmpty(propName) ||
                !int.TryParse(prop, out var propValue))
            {
                propValue = defaultPropValue;
            }

            return propValue;
        }

        public static byte GetByteProperty(string propName, byte defaultPropValue)
        {
            var prop = ConfigurationManager.AppSettings[propName];
            if (string.IsNullOrEmpty(propName) ||
                !byte.TryParse(prop, out var propValue))
            {
                propValue = defaultPropValue;
            }

            return propValue;
        }

        public static string GetStringProperty(string propertyName, string defaultValue = null)
        {
            var value = ConfigurationManager.AppSettings[propertyName];

            if (string.IsNullOrEmpty(value))
            {
                value = defaultValue;
            }

            return value;
        }

        public static TimeSpan GetTimeSpanProperty(string propertyName, TimeSpan defaultValue)
        {
            var valueProp = ConfigurationManager.AppSettings[propertyName];

            if (string.IsNullOrEmpty(valueProp) ||
                !TimeSpan.TryParse(valueProp, out var value))
            {
                value = defaultValue;
            }

            return value;
        }

        #endregion
    }
}

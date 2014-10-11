using System;

namespace Demo.WinForms
{
    internal static class EnumConverter
    {
        public static TEnum? ConvertFrom<TEnum>(object obj) where TEnum: struct
        {
            return ConvertFrom(typeof (TEnum), obj) as TEnum?;
        }

        public static object ConvertFrom(Type enumType, object obj)
        {
            if (!enumType.IsEnum)
                return null;

            var s = obj as string;
            if (s == null)
                return null;

            try
            {
                return Enum.Parse(enumType, s, true);
            }
            catch (Exception ex)
            {
            }

            return null;
        }
    }
}
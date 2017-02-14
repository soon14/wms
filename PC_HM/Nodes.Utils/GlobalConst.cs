using System;

namespace Nodes.Utils
{
    public class GlobalConst
    {
        public const int INT_NULL = int.MinValue;
        public static DateTime DATE_NULL = DateTime.MaxValue;
        public static decimal DECIMAL_NULL = decimal.MinValue;
        public static string STRING_NULL = null;
        public static string STRING_EMPTY = string.Empty;

        public static bool IsNullDate(DateTime date)
        {
            return date == DATE_NULL;
        }

        public static bool IsNullInt(int integerVal)
        {
            return integerVal == INT_NULL;
        }

        public static bool IsNullDecimal(decimal decVal)
        {
            return decVal == DECIMAL_NULL;
        }

        public static bool IsNull(object value)
        {
            return value == null || value == DBNull.Value;
        }

        public static bool IsNullOrEmpty(object value)
        {
            if (IsNull(value)) return true;

            return string.IsNullOrEmpty(ConvertUtil.ToString(value));
        }
    }
}

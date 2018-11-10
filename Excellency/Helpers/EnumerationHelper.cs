using Excellency.Models;
using System;

namespace Excellency
{
    public static class EnumerationHelper
    {
        public static T ToEnum<T>(this int Value)
        {
            return (T)Enum.Parse(typeof(TransactionStatus), Value.ToString());
        }
        public static int ToInt(this TransactionStatus Status)
        {
            return (int)(Status);
        }
    }
}

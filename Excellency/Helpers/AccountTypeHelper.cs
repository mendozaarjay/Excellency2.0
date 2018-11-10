using Excellency.Models;
using System;

namespace Excellency
{
    public static class AccountTypeHelper
    {
        public static T ToEnum<T>(this int Value)
        {
            return (T)Enum.Parse(typeof(AccountType), Value.ToString());
        }
        public static int ToInt(this AccountType type)
        {
            return (int)(type);
        }
    }
}

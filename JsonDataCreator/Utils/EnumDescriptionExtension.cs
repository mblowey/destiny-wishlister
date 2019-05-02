using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace JsonDataCreator.Utils
{
    public static class EnumDescriptionExtension
    {
        public static string GetDescription(this Enum value)
        {
            Type enumType = value.GetType();

            MemberInfo[] memberInfo = enumType.GetMember(value.ToString());

            if (memberInfo?.Length > 0)
            {
                var attribute = memberInfo[0].GetCustomAttribute(typeof(System.ComponentModel.DescriptionAttribute), false);

                if (attribute is System.ComponentModel.DescriptionAttribute descAttribute)
                {
                    return descAttribute.Description;
                }
            }

            return value.ToString();
        }
    }
}

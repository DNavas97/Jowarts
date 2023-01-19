using System;
using System.ComponentModel;

namespace EWorldsCore.Base.Scripts.Utils
{
    /// <summary>
    /// Enum utilities class which can return the enumerable description, or give the type by name
    /// </summary>
    public static class EnumUtils
    {
        private static T GetEnumByName<T>(string enumValue) where T : Enum
        {
            foreach (var item in typeof(T).GetFields())
            {
                if (item.Name == enumValue) return (T) item.GetValue(null);
            }

            throw new ArgumentException("Enum item " + enumValue + " could not be found");
        }
    
        public static bool TryGetEnumByName<T>(string enumValue, out T result) where T : Enum
        {
            try
            {
                result = GetEnumByName<T>(enumValue);
                return true;
            }
            catch (ArgumentException)
            {
                result = default;
                return false;
            }
        }

        public static string GetEnumDescription<T>(T enumType) where T : Enum
        {
            foreach (var item in typeof(T).GetFields())
            {
                if (item.Name != enumType.ToString()) continue;
                
                if (Attribute.GetCustomAttribute(item, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    return attribute.Description;
                }
                break;
            }
            
            throw new ArgumentException("Enum item " + enumType + " of enum: " + typeof(T) + " could not be found");
        }
    }
}


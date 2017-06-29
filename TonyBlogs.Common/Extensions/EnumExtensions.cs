using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

public static class EnumExtensions
{
    /// <summary>
    /// 获取枚举的描述
    /// 枚举必须打上DescriptionAttribute标签
    /// </summary>
    /// <param name="@enum">枚举</param>
    /// <!--如果枚举没有DescriptionAttribute特性，那么将返回枚举的ToString()值，
    /// 如果枚举与类型不匹配，则返回String.Empty-->
    /// <returns>枚举的描述值</returns>
    public static string GetDescription(this object @enum)
    {
        var dic = GetEnumDic(@enum.GetType());
        var enumStr = @enum.ToString();
        return dic.ContainsKey(enumStr) ? dic[enumStr] : string.Empty;
    }

    public static TEnum GetEnum<TEnum>(this int curr)
    {
        return (TEnum)Enum.ToObject(typeof(TEnum), curr);
    }


    #region 私有方法
    private static ConcurrentDictionary<Type, Dictionary<string, string>>
        enumDescriptionDic = new ConcurrentDictionary<Type, Dictionary<string, string>>();
    private static ConcurrentDictionary<Type, Dictionary<int, string>>
        enumDescriptionDicInt = new ConcurrentDictionary<Type, Dictionary<int, string>>();



    public static bool Validate(this Enum @enum)
    {
        return @enum.ToString() != "0";
    }
    ///<summary>
    /// 返回 Dic,取enum的FiledName值作为key
    ///</summary>
    ///<param name="enumType"></param>
    ///<returns>Dic</returns>
    public static Dictionary<string, string> GetEnumDic(Type enumType)
    {
        return enumDescriptionDic.GetOrAdd(enumType, t =>
        {
            var dic = new Dictionary<string, string>();
            var fieldinfos = enumType.GetFields();
            foreach (var field in fieldinfos)
            {
                if (field.FieldType.IsEnum)
                {
                    var objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    var innerID = Convert.ToInt32(field.GetValue(enumType));
                    if (objs.Length > 0)
                        dic.Add(field.Name, ((DescriptionAttribute)objs[0]).Description);
                    else
                        dic.Add(field.Name, field.Name);
                }
            }
            return dic;
        });
    }

    ///<summary>
    /// 返回 Dic,取enum的FiledName值作为key
    ///</summary>
    ///<param name="enumType"></param>
    ///<returns>Dic</returns>
    public static Dictionary<int, string> GetEnumDicNew(Type enumType)
    {
        return enumDescriptionDicInt.GetOrAdd(enumType, t =>
        {
            var dic = new Dictionary<int, string>();
            var fieldinfos = enumType.GetFields();
            foreach (var field in fieldinfos)
            {
                if (field.FieldType.IsEnum)
                {
                    var objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    var innerID = Convert.ToInt32(field.GetValue(enumType));
                    if (objs.Length > 0)
                        dic.Add(innerID, ((DescriptionAttribute)objs[0]).Description);
                    else
                        dic.Add(innerID, field.Name);
                }
            }
            return dic;
        });
    }

    private static ConcurrentDictionary<Type, Dictionary<string, string>>
    enumDescriptionKeyDic = new ConcurrentDictionary<Type, Dictionary<string, string>>();

    ///<summary>
    /// 返回 Dic，取enum的int值作为key
    ///</summary>
    ///<param name="enumType"></param>
    ///<returns>Dic</returns>
    public static Dictionary<string, string> GetEnumDicKey(Type enumType)
    {
        return enumDescriptionKeyDic.GetOrAdd(enumType, t =>
        {
            var dic = new Dictionary<string, string>();
            var fieldinfos = enumType.GetFields();
            foreach (var field in fieldinfos)
            {
                if (field.FieldType.IsEnum)
                {
                    var objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    var innerID = Convert.ToInt32(field.GetValue(enumType)).ToString();
                    if (objs.Length > 0)
                        dic.Add(innerID, ((DescriptionAttribute)objs[0]).Description);
                    else
                        dic.Add(innerID, field.Name);
                }
            }
            return dic;
        });
    }

    #endregion


}

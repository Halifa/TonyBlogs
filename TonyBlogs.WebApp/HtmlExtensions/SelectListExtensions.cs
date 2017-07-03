using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;


public static class SelectListExtensions
{
    /// <summary>
    /// 转换Enum对象为 SelectList
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    /// <param name="enumObj"></param>
    /// <param name="showDefault">已Enum的Description特性值作为默认选项</param>
    /// <param name="valueAsID">是否把描述值用于option的value</param>
    /// <param name="useKey">true:取enum的int值作为key.flase:取enum的FiledName值作为key</param>
    /// <returns></returns>
    public static SelectList ToSelectDescriptionList<TEnum>(this TEnum enumObj, bool showDefault = false, bool valueAsID = false, bool useKey = false)
        where TEnum : struct, IComparable, IFormattable, IConvertible
    {
        //var values = from TEnum e in Enum.GetValues(typeof(TEnum))
        //             select new { Id = e, Name = EnumExtensions.GetEnumDic(e.GetType()) };

        Dictionary<string, string> dictionary = null;
        if (useKey)
            dictionary = EnumExtensions.GetEnumDicKey(enumObj.GetType());
        else
            dictionary = EnumExtensions.GetEnumDic(enumObj.GetType());
        var values = (from dic in dictionary
                        select new { Id = valueAsID ? dic.Value : dic.Key, Name = dic.Value }).ToList();
        if (showDefault)
        {
            var descAttr =
                enumObj.GetType().GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as
                DescriptionAttribute;
            if (descAttr != null)
            {
                values.Insert(0, new { Id = "0", Name = descAttr.Description });
            }
        }


        return new SelectList(values, "Id", "Name", enumObj);
    }

    public static IEnumerable<SelectListItem> ToSelectList<TValue>(this Dictionary<TValue, string> map, string defaultText)
    {
        if (string.IsNullOrEmpty(defaultText)) defaultText = "";

        var result = map.Select(keyPair => new SelectListItem
        {
            Text = keyPair.Value,
            Value = keyPair.Key.ToString(),
            Selected = defaultText.Equals(keyPair.Value.ToString())
        }).ToList();

        if (!result.Any(item => item.Selected))
        {
            result.Insert(0, new SelectListItem()
            {
                Text = defaultText,
                Value = "0"
            });
        }
        return result;
    }
}

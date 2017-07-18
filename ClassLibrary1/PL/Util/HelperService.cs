using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL.Helpers;
using System.Web.Mvc;
using BLL.DTO.Enums;

namespace PL.Util
{
    public static class HelperService
    {
        public static SelectList GetSelectListFromEnum<E>()
        {
            if (typeof(E).BaseType != typeof(Enum))
            {
                throw new InvalidCastException();
            }
            var list = Enum.GetValues(typeof(E))
            .Cast<E>()
            .Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = v.ToString()
            }).ToList();
            return new SelectList(list, "Text", "Value");
        }

        public static Dictionary<string, string> ParseExceptionList(ValidationException ex)
        {
            var exceptionDictionary = new Dictionary<string, string>();
            var properyArray = ex.Property.Split('|');
            var messageArray = ex.Message.Split('|');
            for (int i = 0; i < properyArray.Count()-1; i++)
            {
                exceptionDictionary.Add(properyArray[i], messageArray[i]);
            }
            return exceptionDictionary;
        }
    }
}
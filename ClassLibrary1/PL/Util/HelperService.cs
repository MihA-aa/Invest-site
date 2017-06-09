using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}
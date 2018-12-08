using System;
using System.ComponentModel.DataAnnotations;

namespace Vespoli.Api.Helpers
{
    public class CustomDateRangeAttribute : RangeAttribute
    {
        public CustomDateRangeAttribute() : base(typeof(DateTime), "01/01/2000", DateTime.Now.ToString())
        {

        }
    }
}
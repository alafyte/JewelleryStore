using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryStore
{
    public class DateOfBirth : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is null)
                return false;
            var date = (DateTime)value;
            if (date.Year == 1 && date.Month == 1 && date.Day == 1 ||
                date.Year == DateTime.Now.Year && date.Month == DateTime.Now.Month && date.Day == DateTime.Now.Day)
                return false;
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CFT.Demo.Admin.ValidateAttributes
{
    public class NoSpaceAttribute:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value != null && value.ToString().Contains(" "))
            {
                return new ValidationResult("字符串不能包含空格");
            }
            else
            {
                return ValidationResult.Success;
            }

            return base.IsValid(value, validationContext);
        }
    }
}

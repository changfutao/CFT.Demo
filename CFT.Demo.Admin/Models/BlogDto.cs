using CFT.Demo.Admin.ValidateAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CFT.Demo.Admin.Models
{
    public class BlogDto:IValidatableObject
    {
        [Display(Name="主键")]
        [Required(ErrorMessage ="主键不能为空")]
        public int Id { get; set; }
        [Display(Name="标题")]
        [Required(ErrorMessage = "标题不能为空")]
        [MinLength(6,ErrorMessage = "标题最少6位")]
        [NoSpace]
        public string Title { get; set; }
        [Display(Name = "Url")]
        [Required(ErrorMessage = "Url不能为空")]
        public string Url { get; set; }
        [Display(Name="Level")]
        [Range(1,5,ErrorMessage = "Level必须是1位到5位")]
        public int Level { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
           if(!IsContainSpace(Url))
            {
                yield return new ValidationResult("Url不能包含空格", new[] { nameof(Url) });
            }
        }

        private bool IsContainSpace(string value)
        {
            if(value != null && value.ToString().Contains(" "))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

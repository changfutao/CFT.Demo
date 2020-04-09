using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CFT.Demo.Admin.Models
{
    /// <summary>
    /// 第一步:添加一个需要验证的数据模型
    /// </summary>
    public class UserDto
    {
        public string Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SIN { get; set; }
    }

    /// <summary>
    /// 第二步: 添加校验器类
    /// </summary>
    public class UserValidator:AbstractValidator<UserDto>
    {
        public UserValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("FirstName is not empty");

            RuleFor(x => x.FirstName)
               .MinimumLength(10)
               .WithMessage("FirstName 最小长度为10");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("LastName is not empty");

            RuleFor(x => x.SIN)
                .NotNull()
                .WithMessage("SIN is not null");
               
        }
    }

    public static class Utilities
    {
        public static bool IsValidSIN(int sin)
        {
            if (sin < 0 || sin > 99999998) return false;
            int checksum = 0;
            for (int i = 4; i != 0; i--)
            {
                checksum += sin % 10;
                sin /= 10;

                int addend = 2 * (sin % 10);

                if (addend >= 10) addend -= 9;

                checksum += addend;
                sin /= 10;
            }

            return (checksum + sin) % 10 == 0;
        }
    }
}

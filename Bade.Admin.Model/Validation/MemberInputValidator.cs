#region

using Bade.Admin.Model.Model;
using FluentValidation;

#endregion



namespace Bade.Admin.Model.Validation
{
    public class MemberInputValidator : AbstractValidator<MemberRequest>
    {
        public MemberInputValidator()
        {
            RuleFor(x => x.UserName)
                .NotNull()
                .WithMessage("{0} boş olamaz");

            RuleFor(x => x.UserName)
                .Length(2, 20)
                .WithMessage("Kullanıcı Adı {0} ile {1} karakter arasında olmaldır");

            RuleFor(x => x.Email)
                .EmailAddress()
                .NotNull()
                .Length(0, 60)
                .WithMessage("Geçerli bir E-Mail adresi giriniz");
        }
    }
}
using DTOLayer.DTOs.ContactDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules.ContactUs
{
    public class SendContactUsValidator:AbstractValidator<SendMessageDTO>
    {
        public SendContactUsValidator()
        {
            RuleFor(x => x.Mail).NotEmpty().WithMessage("Mail alanı boş geçilemez");
            RuleFor(x => x.Subject).NotEmpty().WithMessage("Konu alanı boş geçilemez");
            RuleFor(x => x.Name).NotEmpty().WithMessage("İsim alanı boş geçilemez");
            RuleFor(x => x.MessageBody).NotEmpty().WithMessage("Mesaj alanı boş geçilemez");
        }
    }
}

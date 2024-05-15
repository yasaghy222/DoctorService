using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoctorService.Validations;
using FluentValidation;

namespace FileService
{
    public class AddFileValidation : AbstractValidator<AddFileDto>
    {
        public AddFileValidation()
        {
            RuleFor(f => f.File).SetValidator(f => new FileValidator<AddFileDto>(f.File));

            RuleFor(f => f.Address).NotEmpty()
                        .NotNull()
                        .MaximumLength(100);
        }
    }
}
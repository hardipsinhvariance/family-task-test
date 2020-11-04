﻿using Domain.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi.Validators.Commands
{
    public class CreateMemberCommandValidator: AbstractValidator<CreateMemberCommand>
    {
        public CreateMemberCommandValidator()
        {
            RuleFor(x => x.FirstName).NotNull().NotEmpty();
            RuleFor(x => x.LastName).NotNull().NotEmpty();
            RuleFor(x => x.Email).EmailAddress().NotNull().NotEmpty();
            //This was written twise. Hence, I commented it
            //RuleFor(x => x.FirstName).NotNull().NotEmpty();
        }
    }
}

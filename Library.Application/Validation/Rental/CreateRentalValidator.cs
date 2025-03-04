using FluentValidation;
using Library.Application.Commands.RentalCommand;
using Library.Application.Queries.Interface;
using Library.Application.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Validation.Rental
{
    public class CreateRentalValidator : AbstractValidator<CreateRentalCommand>
    {
        private readonly IBookCopyQuery _bookCopyQuery;

        public CreateRentalValidator(IBookCopyQuery bookCopyQuery)
        {
            _bookCopyQuery = bookCopyQuery;

            RuleFor(x => x.UserId).NotEqual(0).WithMessage("Usuario no válido");
            RuleFor(x => x.CopyId).NotEqual(0).WithMessage("Libro no válido")
                .MustAsync(ValidateBookCopyExists).WithMessage("El libro especificado no existe o no está disponible");
            RuleFor(x => x.Quantity)
                .LessThanOrEqualTo(3).WithMessage("Solo se permiten tres libros")
                .NotEqual(0).WithMessage("Cantidad no valida");

            
        }

        private async Task<bool> ValidateBookCopyExists(int copyId, CancellationToken cancellationToken)
        {
            var request = new BookRequest { Id = copyId, pagination = null };
            var result = await _bookCopyQuery.GetByCopyId(request);
            return result.Any();
        }
    }
}

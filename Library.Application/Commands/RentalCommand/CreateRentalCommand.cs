using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Commands.RentalCommand
{
    public record CreateRentalCommand(int UserId, int CopyId, int Quantity): IRequest<Response<int>>;
}

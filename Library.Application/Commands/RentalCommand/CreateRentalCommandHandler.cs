using Library.Application.Generic;
using Library.Domain.Aggregates.Rental;
using Library.Domain.Util;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Commands.RentalCommand
{
    public class CreateRentalCommandHandler : IRequestHandler<CreateRentalCommand, Response<int>>
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IValuesSettings _iValuesSettings;

        public CreateRentalCommandHandler(
            IRentalRepository rentalRepository,
            IValuesSettings iValuesSettings)
        {
            _rentalRepository = rentalRepository;
            _iValuesSettings = iValuesSettings;
        }
        public async Task<Response<int>> Handle(CreateRentalCommand request, CancellationToken cancellationToken)
        {
            var dateNow = DateTime.Now.Peru(_iValuesSettings.GetTimeZone());
            var o = new Rental()
            {
                UserId = request.UserId,
                CopyId = request.CopyId,
                RegisterDatetime = dateNow
            };

            var result = await _rentalRepository.Register(o);

            return new Response<int>(result, "OK");
        }
    }
}

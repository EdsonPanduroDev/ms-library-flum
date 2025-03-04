using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Aggregates.Rental
{
    public interface IRentalRepository
    {
        Task<int> Register(Rental rental);
    }
}

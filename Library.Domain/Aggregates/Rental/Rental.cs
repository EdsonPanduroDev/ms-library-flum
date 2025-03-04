using Library.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Aggregates.Rental
{
    public class Rental : Entity
    {
        public int RentalId { get; set; }
        public int CopyId { get; set; }
        public int UserId { get; set; }
    }
}

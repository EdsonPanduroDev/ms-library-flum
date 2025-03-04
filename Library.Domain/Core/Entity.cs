using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Core
{
    public class Entity
    {
        public int RegisterUserId { get; set; }
        public string RegisterUserFullname { get; set; }
        public DateTime RegisterDatetime { get; set; }
        public int? UpdateUserId { get; set; }
        public string UpdateUserFullname { get; set; }
        public DateTime? UpdateDatetime { get; set; }
    }
}

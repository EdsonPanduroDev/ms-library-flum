using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Util
{
    public class PaginationRequest
    {
        private Pagination _pagination;

        public int pageIndex { get; set; }

        public int pageSize { get; set; }

        public string sort { get; set; }

        public Pagination pagination
        {
            get
            {
                if (_pagination == null)
                {
                    if (pageIndex > 0 && pageSize > 0)
                    {
                        _pagination = new Pagination(pageIndex, pageSize, sort);
                    }
                    else
                    {
                        _pagination = new Pagination(sort);
                    }
                }

                return _pagination;
            }
            set
            {
                _pagination = value;
            }
        }
    }
}

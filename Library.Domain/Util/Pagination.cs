using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Util
{
    public class Pagination
    {
        private int _total = 0;

        public int pageIndex { get; set; }

        public int pageSize { get; set; }

        public int pageCount { get; set; }

        public string sort { get; set; }

        public int total
        {
            get
            {
                return _total;
            }
            set
            {
                if (pageSize > 0)
                {
                    pageCount = value / pageSize + ((value % pageSize != 0) ? 1 : 0);
                }
                else
                {
                    pageCount = 1;
                }

                _total = value;
            }
        }

        public Pagination(string sort)
        {
            this.sort = ((sort == null) ? "" : sort);
        }

        public Pagination(int pageIndex, int pageSize, string sort)
        {
            this.pageIndex = pageIndex;
            this.pageSize = pageSize;
            this.sort = ((sort == null) ? "" : sort);
        }
    }
}

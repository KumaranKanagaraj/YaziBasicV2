using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YaziBasicV2.Helpers
{
    public class VerityResourceParameters
    {
        const int maxPageSize = 5;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 5;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }

    }
}

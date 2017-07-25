using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TonyBlogs.DTO
{
    public class PageSearchDTO
    {
        protected int PageMaxSize = 1000;
        private int _pageIndex;
        private int _pageSize;

        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex
        {
            get { return _pageIndex; }
            set
            {
                if (value < 1)
                {
                    value = 1;
                }
                _pageIndex = value;
            }
        }

        /// <summary>
        /// 每页条数
        /// </summary>
        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                if (value > PageMaxSize)
                {
                    value = PageMaxSize;
                }
                _pageSize = value;
            }
        }
    }
}

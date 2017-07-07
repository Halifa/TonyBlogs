using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TonyBlogs.DTO
{
    public class ExecuteResult
    {
        public ExecuteResult() { }

        public ExecuteResult(bool isSuccess, string message)
        {
            this.IsSuccess = isSuccess;
            this.Message = message;
        }

        public bool IsSuccess { get; set; }

        public string Message { get; set; }
    }
}

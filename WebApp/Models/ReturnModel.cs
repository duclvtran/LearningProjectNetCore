using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class ReturnModel<T>
    {
        public ReturnModel(T data)
        {
            Code = 1;
            Message = "Thành công";
            Data = data;
        }
        public int Code { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}

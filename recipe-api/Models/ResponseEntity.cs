using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

namespace recipe_api.Models
{
    public class ResponseEntity<T>
    {
        public TransactionStatus transactionStatus { get; set; }
        public string returnMessage { get; set; } = "";

        public T ReturnData { get; set; }

        public string status
        {
            get
            {
                return transactionStatus.ToString();
            }
        }
    }
    public enum TransactionStatus
    {
        None,
        Success,
        Failed,
        InternalServerError
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BankAPITest.Models
{

    /// <summary>
    /// 
    /// </summary>
    public class TransactionData
    {
        [Key]
        public long TransactionId { get; set; }

        [ForeignKey("AccountNumber")] 
        public int AccountNumber { get; set; }

        public string TransactionType { get; set; }

        public DateTime Date { get; set; }

        public decimal Amount { get; set; }

        public decimal CurrentBalance { get; set; }

        public string Comment { get; set; }
    }

}

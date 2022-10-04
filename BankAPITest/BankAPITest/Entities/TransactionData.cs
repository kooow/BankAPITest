using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankAPITest.Entities
{

    /// <summary>
    /// 
    /// </summary>
    public class TransactionData : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("AccountNumber")] 
        public int AccountNumber { get; set; }

        public string TransactionType { get; set; }

        public DateTime Date { get; set; }

        public decimal Amount { get; set; }

        public decimal CurrentBalance { get; set; }

        public string Comment { get; set; }
    }

}

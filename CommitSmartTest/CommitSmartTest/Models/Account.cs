using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CommitSmartTest.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Account
    {
        [Key]
        public int AccountId { get; set; }

        public string Name { get; set; }

        public int AccountNumber { get; set; }

        public int AccountType { get; set; }

        public decimal Balance { get; set; }

        public DateTime ModifyDate { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        [JsonIgnore]
        public virtual ICollection<TransactionData> Transactions { get; set; }

    }

}
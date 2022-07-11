using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankAPITest.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class User : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual List<Account> Accounts { get; set; }
    }

}
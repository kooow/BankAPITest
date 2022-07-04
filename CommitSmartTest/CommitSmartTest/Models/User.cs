using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CommitSmartTest.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class User
    {
        [Key]
        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual List<Account> Accounts { get; set; }

    }

}
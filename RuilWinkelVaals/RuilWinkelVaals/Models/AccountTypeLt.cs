using System;
using System.Collections.Generic;

#nullable disable

namespace RuilWinkelVaals.Models
{
    public partial class AccountTypeLt
    {
        public AccountTypeLt()
        {
            ProfileData = new HashSet<ProfileDatum>();
        }

        public int Id { get; set; }
        public string AccountType { get; set; }

        public virtual ICollection<ProfileDatum> ProfileData { get; set; }
    }
}

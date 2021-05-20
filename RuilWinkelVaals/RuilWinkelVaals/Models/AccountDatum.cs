using System;
using System.Collections.Generic;

#nullable disable

namespace RuilWinkelVaals.Models
{
    public partial class AccountDatum
    {
        public int Id { get; set; }
        public int? ProfileId { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }
        public bool? Blocked { get; set; }
        public DateTime? DateBlocked { get; set; }

        public virtual ProfileDatum Profile { get; set; }
    }
}

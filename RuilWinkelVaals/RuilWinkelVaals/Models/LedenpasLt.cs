using System;
using System.Collections.Generic;

#nullable disable

namespace RuilWinkelVaals.Models
{
    public partial class LedenpasLt
    {
        public LedenpasLt()
        {
            ProfileData = new HashSet<ProfileDatum>();
        }

        public int Id { get; set; }
        public string Status { get; set; }

        public virtual ICollection<ProfileDatum> ProfileData { get; set; }
    }
}

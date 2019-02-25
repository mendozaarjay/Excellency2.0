using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.Models
{
    public class PeerEvaluationHeader
    {
        public int Id { get; set; }
        public Account Employee { get; set; }
        public Account CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsExpired { get; set; } = false;
        public virtual Status Status { get; set; }
        public virtual EvaluationSeason EvaluationSeason { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.Models
{
    public class PeerEvaluationLine
    {
        public int Id { get; set; }
        public virtual PeerEvaluationHeader PeerEvaluationHeader { get; set; }
        public virtual PeerCriteria PeerCriteria { get; set; }
        public int Score { get; set; }
        public string Comment { get; set; }
    }
}

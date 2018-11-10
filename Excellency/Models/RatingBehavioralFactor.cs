using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excellency.Models
{
    public class RatingBehavioralFactor
    {
        public int Id { get; set; }
        public virtual RatingHeader RatingHeader { get; set; }
        public virtual BehavioralFactor BehavioralFactor { get; set; }
        public virtual BehavioralFactorItem BehavioralFactorItem { get; set; }
        public int Score { get; set; }
        public string Comment { get; set; }
    }
}

using System.Collections.Generic;

namespace Excellency.ViewModels
{
    public class PositionIndexViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public IEnumerable<PositionViewModel> Positions { get; set; }
        public IEnumerable<PositionLevelItem> Levels { get; set; }
    }
}

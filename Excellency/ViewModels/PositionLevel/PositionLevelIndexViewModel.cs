using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Excellency.ViewModels
{
    public class PositionLevelIndexViewModel
    {
        public IEnumerable<PositionLevelItem> PositionLevels { get; set; }
        public PositionLevelItem Item { get; set; }
        public IEnumerable<SelectListItem> Levels { get; set; }
    }
}

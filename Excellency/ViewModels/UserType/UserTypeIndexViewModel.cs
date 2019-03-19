using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Excellency.ViewModels
{
    public class UserTypeIndexViewModel
    {
        [NotMapped]
        public IEnumerable<UserTypeItem> UserTypes { get; set; }
        public UserTypeItem Item { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FXProject.Shared.DbTables
{
    public class FX_Location
    {
        [Key]
        public int LocationID { get; set; }
        public string LocationName { get; set; } = null!;
        public string LocationDescription { get; set; } = null!;
    }
}

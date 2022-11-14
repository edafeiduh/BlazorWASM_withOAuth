using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FXProject.Shared.DbTables
{
    public class FX_RBA
    {
        [Key]
        public int RoleID { get; set; }
        public string RoleName { get; set; } = null!;
        public string RoleDescription { get; set; } = null!;
    }
}

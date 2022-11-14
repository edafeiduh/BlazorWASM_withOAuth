using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FXProject.Shared.DbTables
{
    // This MS SQL Db Tabble manages the User to Role Relationship... 
    // One User can have multiple role (This Rule only applies to the System Admin)
    // All Other Users can ONLY have One User Role... Which is a One to One relationship

    public class FX_UserRoleMap
    {
        [Key]
        public int UserRole_PK { get; set; }
        public int UserID_FK { get; set; } 
        public int RoleID_FK { get; set; } 
    }
}

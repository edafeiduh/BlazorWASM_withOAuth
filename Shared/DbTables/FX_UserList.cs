using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FXProject.Shared.DbTables
{
    public class FX_UserList
    {

        [Key]
        public int UserID { get; set; }
        public string UserFirstName { get; set; } = null!;
        public string UserLastName { get; set; } = null!;
        public string UserEmail { get; set; } = null!;
        public int UserLocation { get; set; }

        //Created an Object of the UserLocation to get the other columns associated with the Foreign Key 
        [ForeignKey ("UserLocation")]
        public FX_Location MyLocation { get; set; } = null!;
        public bool UserIsActive { get; set; }

    }
}

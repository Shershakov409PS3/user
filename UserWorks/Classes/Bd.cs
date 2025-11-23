using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserWorks.Models;

namespace UserWorks.Classes
{
    internal class Bd
    {
       public static DEM_users_Entities Context { get; set; } = new DEM_users_Entities();
    }
}

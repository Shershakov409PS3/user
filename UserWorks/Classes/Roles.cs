using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserWorks.Models;

namespace UserWorks.Classes
{
    internal class Roles
    {
        public static List<Role> All { get; set; } = Bd.Context.Role.ToList();
    }
}

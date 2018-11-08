using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PracticaAPI.Models
{
    public class User
    {
    
        public int Id { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public String UserName { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace AoS_Web_Application.Models
{
    public class Army
    {
        [Key]
        public int Id { get; set; }
        public string GrandAlliance { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [ForeignKey("IdentityUser")]
        public string UserId { get; set; }
        public virtual IdentityUser IdentityUser { get; set; }
        
        

        public Army() { }
    }
}

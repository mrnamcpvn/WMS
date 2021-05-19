
    using System.ComponentModel.DataAnnotations;
namespace WMS_API.Models
{
    public class User
    {
        [Key]
        public int id { get; set; }

        [Required]
        [StringLength(20)]
        public string username { get; set; }

        [Required]
        [StringLength(32)]
        public string password { get; set; }

        [StringLength(50)]
        public string fullname { get; set; }

        public bool? status { get; set; }
    }
}

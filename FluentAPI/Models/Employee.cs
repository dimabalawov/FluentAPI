using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentAPI.Models
{
    [Table("STAFF")]
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        [Column("FirstName")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(20)]
        [Column("LastName")]
        public string LastName { get; set; }
        [Required]
        [MaxLength(20)]
        [Column("Position")]
        public string Position { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebGamesCRUD.Models
{
    [Table("Genre")]
    public class Genre
    {
        [Key]
        [Column("ID_Genre")]
        public int IdGenre { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebGamesCRUD.Models
{
    [Table("Game")]
    public class Game
    {
        [Key]
        [Column("ID_Game")]
        public int IdGame { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Developer { get; set; }
        [Column("ID_ListGenre")] 
        public int? IdListGenre { get; set; }
    }
}

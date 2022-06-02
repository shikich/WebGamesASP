using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebGamesCRUD.Models.View
{
    [Table("Game")]
    public class Game_ListGenre_Genre
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
        public List<ListGenre> GroupListGenre { get; set; }
        public List<GameGenre> GroupGenres { get; set; }
    }
}

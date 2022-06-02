using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebGamesCRUD.Models
{
    [Keyless]
    [Table("ListGenre")]
    public class ListGenre
    {
        [Column("ID_ListGenre")]
        public int IdListGenre { get; set; }
        [Column("ID_Genre")]
        public int IdGenre { get; set; }

        [ForeignKey(nameof(IdGenre))]
        public virtual Genre IdGenreNavigation { get; set; }
    }

}

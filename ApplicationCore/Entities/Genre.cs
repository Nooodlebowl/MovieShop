using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Genre
    {   
        public int Id { get; set; }

        //data annotations way
        [MaxLength(24)]
        public string Name { get; set; }

        //Navigation Property
        public ICollection<MovieGenre> MoviesOfGenre { get; set; }
    }
}

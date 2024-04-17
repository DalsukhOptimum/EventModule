using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ActivityEntity
    {
        //setting the vallidation for Id so no one can send a string here
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int ActivityId { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }
        public int Price { get; set; }
        public long EventId { get; set; }

        public string Flag { get; set; }

        public string ImageType { get; set; }
    }
}

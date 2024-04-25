using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class EventEntity
    {
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int EventId { get; set; }

        public string Name { get; set; }   

        public string Description { get; set; }
       
        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string Image { get; set; }

        public string Flag { get; set; }
        //setting the vallidation for Price so no one can send a string here
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int Price { get; set; }
        //setting the vallidation for ActivityId so no one can send a string here
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public string ActivityId { get; set; }    

        public string ImageType { get; set; }   
        public int IsPublish { get; set; }

        public int Month { get; set; }

    }



}

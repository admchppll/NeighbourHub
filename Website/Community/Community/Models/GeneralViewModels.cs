using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Models
{
    public class EventSearchView
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [DisplayFormat(DataFormatString = "{0:ddd d MMMM yyyy}")]
        public System.DateTime Date { get; set; }
        [DisplayFormat(DataFormatString= "{0:hh\\:mm}")]
        public System.TimeSpan Time { get; set; }
        [DisplayFormat(DataFormatString ="{0:#0.##}")]
        public double Distance { get; set; }
        public byte Length { get; set; }
    }
}

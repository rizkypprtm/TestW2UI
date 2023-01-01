using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestW2uiWeb.Model
{
    public class SportEventModel
    {
        public List<DataE> data { get; set; }
        public MetaE meta { get; set; }
    }

    public class DataE
    {
        public string eventDate { get; set; }
        public string eventName { get; set; }
        public string eventType { get; set; }
        public int id { get; set; }
        public Organizer organizer { get; set; }
    }

    public class LinksE
    {
        public string next { get; set; }
    }

    public class MetaE
    {
        public PaginationE pagination { get; set; }
    }

    public class Organizer
    {
        public int id { get; set; }
        public string imageLocation { get; set; }
        public string organizerName { get; set; }
    }

    public class PaginationE
    {
        public int total { get; set; }
        public int count { get; set; }
        public int per_page { get; set; }
        public int current_page { get; set; }
        public int total_pages { get; set; }
        public LinksE links { get; set; }
    }

    public class insertEventParam
    {
        public string eventDate { get; set; }
        public string eventType { get; set; }
        public string eventName { get; set; }
        public string organizerId { get; set; }
    }

    public class updateEventParam
    {
        public string eventDate { get; set; }
        public string eventType { get; set; }
        public string eventName { get; set; }
        public string organizerId { get; set; }
    }

}
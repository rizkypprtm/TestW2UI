using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestW2uiWeb.Model
{
    public class OrganizerModel
    {

        public List<Datum> data { get; set; }
        public Meta meta { get; set; }
        public Errors errors { get; set; }
    }

    public class Errors
    {

    }
    public class Datum
    {
        public int id { get; set; }
        public string organizerName { get; set; }
        public string imageLocation { get; set; }
    }

    public class Meta
    {
        public Pagination pagination { get; set; }
    }

    public class Pagination
    {
        public int total { get; set; }
        public int count { get; set; }
        public int per_page { get; set; }
        public int current_page { get; set; }
        public int total_pages { get; set; }
        public Links links { get; set; }
    }

    public class Links
    {
        public string next { get; set; }
    }

    public class CreateOrganizerParam
    {
        public string organizerName { get; set; }
        public string imageLocation { get; set; }
    }
}

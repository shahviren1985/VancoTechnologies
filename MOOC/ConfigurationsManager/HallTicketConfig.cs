
namespace ITM.Courses.ConfigurationsManager
{
    public class HallTicketConfig
    {
        public bool IsHeaderRequired { get; set; }
        public string LogoPath { get; set; }
        public int LogoWidth { get; set; }
        public int LogoHeight { get; set; }
        public string CollegeName { get; set; }
        public string CollegeTag1 { get; set; }
        public string CollegeTag2 { get; set; }
        public string CollegeTag3 { get; set; }
        public string TagLine { get; set; }
        public string TagLine1 { get; set; }
        public string TagLine2 { get; set; }
        public string Address { get; set; }
        public int PostHeader { get; set; }
        public int PostHallTicket { get; set; }
        public int LineBreakHeight { get; set; }
        public int PrintHallTicketsPerPage { get; set; }
    }
}

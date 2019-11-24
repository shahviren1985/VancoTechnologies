using System;

namespace AA.DAO
{
    public class CommentDetails
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public DateTime DateCreated { get; set; }

        public string ProcessedDateCreated { get; set; }
        public string CreatedBy { get; set; }

        public string UserComment { get; set; }

        public string UserName { get; set; }

        public int DocumentId { get; set; }
    }
}

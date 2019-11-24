using System;

namespace AA.DAO
{
    public class DocumentDetails
    {
        public int Id { get; set; }
        public string UniqueName { get; set; }
        public string FriendlyName { get; set; }
        public string DepartmentId { get; set; }
        public string Author { get; set; }
        public string LastModifiedBy { get; set; }
        public string DocumentStatus { get; set; }

        public bool IsScanned { get; set; }

        public string DocumentType { get; set; }

        public string ScanPath { get; set; }

        public bool IsContent { get; set; }

        public string Content { get; set; }
        public string FolderId { get; set; }

        public string AllowAccess { get; set; }
        public string TaggedUsers { get; set; }
        public string MessageHeader { get; set; }
        public DateTime DateCreated { get; set; }

        public string FileTags { get; set; }

        public string LastModified { get; set; }

        public string SerialNumber { get; set; }

        public string StoreRoomLocation { get; set; }

        public string Address { get; set; }

        public int ParentId { get; set; }

        public bool IsDeadlineMentioned { get; set; }

        public string Deadline { get; set; }

        public string IsScannedMarathi { get; set; }

        public string ScanMarathiPath { get; set; }
    }
}

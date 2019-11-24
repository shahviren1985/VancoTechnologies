using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AA.DAO
{
    public class FileDetails
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string OriginalFileName { get; set; }
        public string FileDescription { get; set; }
        public string RelativePath { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public int OwnerUserId { get; set; }
        public string UserAccess { get; set; }
        public int ParentFolderId { get; set; }
        public string Tags { get; set; }
        public string History { get; set; }
        public string OwnerName { get; set; }
        public string Permission { get; set; }
        public string  DocumentType { get; set; }
    }

    // sort by date
    public class File_SortByDateByAscendingOrder : IComparer<FileDetails>
    {
        #region IComparer<Employee> Members

        public int Compare(FileDetails x, FileDetails y)
        {
            if (x.DateCreated > y.DateCreated) return 1;
            else if (x.DateCreated < y.DateCreated) return -1;
            else return 0;
        }

        #endregion
    }

    public class File_SortByDateByDescendingOrder : IComparer<FileDetails>
    {
        #region IComparer<Employee> Members

        public int Compare(FileDetails x, FileDetails y)
        {
            if (x.DateCreated < y.DateCreated) return 1;
            else if (x.DateCreated > y.DateCreated) return -1;
            else return 0;
        }

        #endregion
    }
    //end

    //sort by name
    public class File_SortByNameDES : IComparer<FileDetails>
    {
        #region IComparer<Employee> Members

        public int Compare(FileDetails x, FileDetails y)
        {
            return string.Compare(y.OriginalFileName, x.OriginalFileName);
        }

        #endregion
    }

    public class File_SortByNameASC : IComparer<FileDetails>
    {
        #region IComparer<Employee> Members

        public int Compare(FileDetails x, FileDetails y)
        {
            return string.Compare(x.OriginalFileName, y.OriginalFileName);
        }

        #endregion
    }
    //end

    // sort by owner name
    public class File_SortByOwnerDES : IComparer<FileDetails>
    {
        #region IComparer<Employee> Members

        public int Compare(FileDetails x, FileDetails y)
        {
            return string.Compare(y.OwnerName, x.OwnerName);
        }

        #endregion
    }

    public class File_SortByOwnerASC : IComparer<FileDetails>
    {
        #region IComparer<Employee> Members

        public int Compare(FileDetails x, FileDetails y)
        {
            return string.Compare(x.OwnerName, y.OwnerName);
        }

        #endregion
    }
}

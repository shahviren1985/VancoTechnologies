using System;
using System.Collections.Generic;
using System.Reflection;
using Util;
using System.Web.UI.WebControls;

namespace AA.DAO
{
    public class FolderDetails
    {
        public int Id { get; set; }
        public string FolderName { get; set; }
        public string OwnerName { get; set; }
        public string FolderDescription { get; set; }
        public string RelativePath { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public int OwnerUserId { get; set; }
        public bool IsActive { get; set; }
        public string UserAccess { get; set; }
        public int ParentFolderId { get; set; }
        public string Tags { get; set; }
        public string History { get; set; }
        public string Alias { get; set; }
        public string Permission { get; set; }        
        public string FolderType { get; set; }
        public string Email { get; set; }
    }

    // sort by date
    public class Folder_SortByDateByAscendingOrder : IComparer<FolderDetails>
    {
        #region IComparer<Employee> Members

        public int Compare(FolderDetails x, FolderDetails y)
        {
            if (x.DateCreated > y.DateCreated) return 1;
            else if (x.DateCreated < y.DateCreated) return -1;
            else return 0;
        }

        #endregion
    }

    public class Folder_SortByDateByDescendingOrder : IComparer<FolderDetails>
    {
        #region IComparer<Employee> Members

        public int Compare(FolderDetails x, FolderDetails y)
        {
            if (x.DateCreated < y.DateCreated) return 1;
            else if (x.DateCreated > y.DateCreated) return -1;
            else return 0;
        }

        #endregion
    }
    //end

    //sort by name
    public class Folder_SortByNameDES : IComparer<FolderDetails>
    {
        #region IComparer<Employee> Members

        public int Compare(FolderDetails x, FolderDetails y)
        {
            return string.Compare(y.Alias, x.Alias);
        }

        #endregion
    }

    public class Folder_SortByNameASC : IComparer<FolderDetails>
    {
        #region IComparer<Employee> Members

        public int Compare(FolderDetails x, FolderDetails y)
        {
            return string.Compare(x.Alias, y.Alias);
        }

        #endregion
    }
    //end

    // sort by owner name
    public class Folder_SortByOwnerDES : IComparer<FolderDetails>
    {
        #region IComparer<Employee> Members

        public int Compare(FolderDetails x, FolderDetails y)
        {
            return string.Compare(y.OwnerName, x.OwnerName);
        }

        #endregion
    }

    public class Folder_SortByOwnerASC : IComparer<FolderDetails>
    {
        #region IComparer<Employee> Members

        public int Compare(FolderDetails x, FolderDetails y)
        {
            return string.Compare(x.OwnerName, y.OwnerName);
        }

        #endregion
    }
}

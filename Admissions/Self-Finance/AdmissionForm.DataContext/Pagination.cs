namespace AdmissionForm.DataContext
{ 
    /// <summary>
    /// This class is used to Define Pagination properties which will be used for entities
    /// </summary>
    /// <CreatedBy>Kaushik Patel</CreatedBy>
    /// <CreatedDate>04-Aug-2015</CreatedDate>
    /// <ModifiedBy></ModifiedBy>
    /// <ModifiedDate></ModifiedDate>
    /// <ReviewBy></ReviewBy>
    /// <ReviewDate></ReviewDate>
    public sealed class Pagination
    {
        #region Pagination Property

        /// <summary>
        /// Gets or sets Current Page No
        /// </summary>
        public int PageNo { get; set; }

        /// <summary>
        /// Gets or sets Page Size
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets Pager Size
        /// </summary>
        public int PagerSize { get; set; }

        /// <summary>
        /// Gets or sets Total Pages
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Gets or sets Total Records
        /// </summary>
        public int TotalRecords { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Next Page available
        /// </summary>
        public bool HasNextPage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Next Page available
        /// </summary>
        public bool HasPreviousPage { get; set; }

        #endregion
    }
}

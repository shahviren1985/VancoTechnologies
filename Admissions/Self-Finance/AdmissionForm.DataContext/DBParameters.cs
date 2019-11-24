namespace AdmissionForm.DataContext
{ 
    using System.Data; 

    /// <summary>
    /// This class to define default properties for Parameters for SQL Command 
    /// </summary>
    /// <CreatedBy>Kaushik Patel</CreatedBy>
    /// <CreatedDate>04-Aug-2015</CreatedDate>
    /// <ModifiedBy></ModifiedBy>
    /// <ModifiedDate></ModifiedDate>
    /// <ReviewBy></ReviewBy>
    /// <ReviewDate></ReviewDate>
    public sealed class DBParameters
    {
        #region Properties

        /// <summary>
        /// Gets or sets Parameter Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets Parameter Value
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Gets or sets Parameter DBType
        /// </summary>
        public DbType DBType { get; set; }

        #endregion
    }
}

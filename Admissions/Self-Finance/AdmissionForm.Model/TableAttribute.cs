//-----------------------------------------------------------------------
// <copyright file="TableAttribute.cs" company="karmsoft">
//     Copyright (c) karmsoft. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdmissionForm.Business.Model
{
    using System; 

    /// <summary>
    /// This class is used to Define custom attribute for Property, 
    /// which is used to define Parameter in DataAccess Context
    /// </summary>
    /// <CreatedBy>Kaushik Patel</CreatedBy>
	/// <CreatedDate>20-Aug-2016</CreatedDate>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class CustomColumnAttribute : System.Attribute
    {
        #region Variable Declaration
        /// <summary>
        /// This Property will used to identify, current property required to send as parameter or not
        /// </summary>
        private readonly bool isTableColumn;

        /// <summary>
        /// This Property will used to identify, current property required to send as parameter for Search SP
        /// </summary>
        private readonly bool isIncludeInSearchColumn;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the TableAttribute class.
        /// </summary>
        /// <param name="isTableColumn">false for not want to pass this property as parameter in DataAccessContext, else true.</param>
        /// <param name="isIncludeInSearchColumn">false for not want to pass this property as parameter in Search, else true.</param>
        public CustomColumnAttribute(bool isTableColumn, bool isIncludeInSearchColumn)
        {
            this.isTableColumn = isTableColumn;
            this.isIncludeInSearchColumn = isIncludeInSearchColumn;
        }

        /// <summary>
        /// Initializes a new instance of the TableAttribute class.
        /// </summary>
        /// <param name="isTableColumn">false for not want to pass this property as parameter in DataAccessContext, else true.</param>
        public CustomColumnAttribute(bool isTableColumn)
        {
            this.isTableColumn = isTableColumn;
        }
        #endregion

        #region Property Declaration

        /// <summary>
        /// Gets a value indicating whether the item is enabled
        /// </summary>
        public bool IsTableColumn
        {
            get { return this.isTableColumn; }
        }

        /// <summary>
        /// Gets a value indicating whether the item is enabled
        /// </summary>
        public bool IsIncludeInSearchColumn
        {
            get { return this.isIncludeInSearchColumn; }
        }

        #endregion
    }
}

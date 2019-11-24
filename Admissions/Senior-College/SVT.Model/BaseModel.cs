//-----------------------------------------------------------------------
// <copyright file="BaseModel.cs" company="karmsoft">
//     Copyright (c) karmsoft. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SVT.Business.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// This class is used to Define common Model Properties for all other model classes
    /// </summary>
    /// <CreatedBy>Kaushik Patel</CreatedBy>
	/// <CreatedDate>20-Aug-2016</CreatedDate>
    /// <ModifiedBy></ModifiedBy>
    /// <ModifiedDate></ModifiedDate>
    /// <ReviewBy></ReviewBy>
    /// <ReviewDate></ReviewDate>
    [Serializable]
    public abstract class BaseModel : ICloneable, IDisposable
    {
        #region Variable Declaration

        /// <summary>
        /// dispose Property
        /// </summary>
        private bool disposed;

        #endregion

        #region Finalizer

        /// <summary>
        /// Finalizes an instance of the <see cref="BaseModel"/> class
        /// </summary>
        ~BaseModel()
        {
            this.Dispose(false);
        }

        #endregion

        #region Property

        ///// <summary>
        ///// Gets abstract Property for TableName, which required to use Procedure Name in DataAccessContext
        ///// </summary>
        ////[NotMapped]
        ////public abstract string TableName
        ////{
        ////    get;
        ////}

        /// <summary>
        /// Gets or sets TotalRecords,  Common Property for All Entity which return Total Records for current List
        /// </summary>
        [NotMapped]
        public int TotalRecords { get; set; }

        /// <summary>
        /// Gets or sets RowIndex, Common Property for All Entity which return Total Records for current List
        /// </summary>
        [NotMapped]
        public long RowIndex { get; set; }

        #endregion

        #region Dispose Methods

        /// <summary>
        /// Clone Method
        /// </summary>
        /// <returns>return clone of current object</returns>
        object ICloneable.Clone()
        {
            return this.CloneImplementation();
        }

        /// <summary>
        /// The dispose method that implements IDisposable.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// The virtual dispose method that allows
        /// classes inherited from this one to dispose their resources.
        /// </summary>
        /// <param name="disposing">disposing value</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources here.
                }

                // Dispose unmanaged resources here.
            }

            this.disposed = true;
        }

        #endregion

        #region Clone Methods

        /// <summary>
        /// Clone Method
        /// </summary>
        /// <returns>return clone of current object</returns>
        protected object Clone()
        {
            return this.CloneImplementation();
        }

        /// <summary>
        /// Creating clone of current object
        /// </summary>
        /// <returns>return clone of current object</returns>
        protected virtual BaseModel CloneImplementation()
        {
            var copy = (BaseModel)MemberwiseClone();
            return copy;
        }

        #endregion
    }
}

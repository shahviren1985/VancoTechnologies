namespace SVT.DataContext
{ 
    using System.Collections.Generic; 

    /// <summary>
    /// Interface to define DataAccess Class with required methods 
    /// </summary>
    /// <CreatedBy>Kaushik Patel</CreatedBy>
    /// <CreatedDate>04-Aug-2015</CreatedDate>
    /// <ModifiedBy></ModifiedBy>
    /// <ModifiedDate></ModifiedDate>
    /// <ReviewBy></ReviewBy>
    /// <ReviewDate></ReviewDate>
    public interface IDBContext
    {
        /// <summary>
        /// Get a selected entity by the object primary key ID
        /// </summary>
        /// <typeparam name="T">Entity Object</typeparam>
        /// <param name="primaryKey">Primary key ID</param>
        /// <returns>entity with given key value</returns>
        T SelectObject<T>(int primaryKey);

        /// <summary> 
        /// Save entity to the Database
        /// </summary> 
        /// <typeparam name="T">Entity Object</typeparam>
        /// <param name="entity">the entity to add</param> 
        /// <returns>The saved entity identity value</returns> 
        int Save<T>(T entity);

        /// <summary> 
        /// Delete entity from the database
        /// </summary> 
        /// <typeparam name="T">Entity Object</typeparam>
        /// <param name="primaryKey">the primary key value of entity</param> 
        /// <returns>return deleted entity identity value</returns> 
        int Delete<T>(int primaryKey);

        /// <summary>
        /// Load the entities using search criteria
        /// </summary>
        /// <typeparam name="T">Entity Object</typeparam>
        /// <param name="entity">entity with filter criteria</param>
        /// <returns>returns List of entity</returns>
        IList<T> Search<T>(T entity);

        /// <summary>
        /// Load the entities using search criteria
        /// </summary>
        /// <typeparam name="T">Entity Object</typeparam>
        /// <param name="entity">entity with filter criteria</param>
        /// <param name="pageNo">Current Page no</param>
        /// <returns>returns List of entity</returns>
        IList<T> Search<T>(T entity, int? pageNo);

        /// <summary>
        /// Return the list of model for given search criteria
        /// </summary>
        /// <typeparam name="T">Entity Object</typeparam>
        /// <param name="entity">Model with Search Criteria</param>
        /// <param name="pageNo">current page no</param>
        /// <param name="sortExpression">Sort Expression</param>
        /// <param name="sortDirection">Sort Direction</param>
        /// <returns>List of Models</returns>
        IList<T> Search<T>(T entity, int? pageNo, string sortExpression, string sortDirection);
    }
}

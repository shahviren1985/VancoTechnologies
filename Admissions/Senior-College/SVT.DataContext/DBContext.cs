﻿namespace SVT.DataContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    /// <summary>
    /// This class is used to define default table methods like
    /// Insert, Update, Delete, Select, Search from Models
    /// </summary>
    /// <CreatedBy>Kaushik Patel</CreatedBy>
    /// <CreatedDate>04-Aug-2015</CreatedDate>
    /// <ModifiedBy></ModifiedBy>
    /// <ModifiedDate></ModifiedDate>
    /// <ReviewBy></ReviewBy>
    /// <ReviewDate></ReviewDate>
    public abstract class DBContext : DBExecute, IDBContext, IDisposable
    {
        /// <summary>
        /// dispose status
        /// </summary>
        private bool disposed;

        /// <summary>
        /// Finalizes an instance of the DBContext class.
        /// </summary>
        ~DBContext()
        {
            this.Dispose(false);
        }

        #region Property Declaration

        /// <summary>
        /// Gets Prefix of Stored Procedure used in Database
        /// </summary>
        public string ProcedurePrefix
        {
            get
            {
                return "Usp";
            }
        }

        #region Duplicate Check Property

        /// <summary>
        /// Gets or sets a value indicating whether check duplication require or not
        /// </summary>
        public bool CheckForDuplicate { get; set; }

        /// <summary>
        /// Gets or sets Column 1 name for duplication
        /// </summary>
        public string Col1Name { get; set; }

        /// <summary>
        /// Gets or sets Column 2 name for duplication
        /// </summary>
        public string Col2Name { get; set; }

           /// <summary>
        /// Gets or sets Column 3 name for duplication
        /// </summary>
        public string Col3Name { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether combination check required or not
        /// </summary>
        public bool CombinationCheckRequired { get; set; }

        #endregion

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Add Parameter 
        /// </summary>
        /// <typeparam name="TEntity">Model Type</typeparam>
        /// <param name="entity">entity of object</param>
        /// <param name="isForSearch">require for search</param>
        /// <returns>returns list of parameters</returns>
        public static System.Collections.ObjectModel.Collection<DBParameters> AddParameters<TEntity>(TEntity entity, bool isForSearch = false)
        {
            System.Collections.ObjectModel.Collection<DBParameters> parameters = new System.Collections.ObjectModel.Collection<DBParameters>();
            PropertyInfo[] infos = entity.GetType().GetProperties();
            foreach (PropertyInfo info in infos)
            {
                var value = info.GetValue(entity, null);

                // Verify Property Validation and than add as paramter
                if (ParameterValidation(info, value, isForSearch))
                {
                    if (info.PropertyType == typeof(string))
                    {
                        /* Added by Darshit Babariya 02 September 2013
                         To add trim functionality in All string parameters                        */
                        value = value == null ? null : value.ToString().Trim();
                    }

                    parameters.Add(new DBParameters()
                    {
                        Name = info.Name,
                        Value = value,
                        DBType = GetPropertyType(info.PropertyType)
                    });
                }
            }

            return parameters;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// The dispose method that implements IDisposable.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            /*GC.SuppressFinalize(this);        */
        }

        /// <summary>
        /// Save the Current Model
        /// </summary>
        /// <typeparam name="TEntity">Model Type</typeparam>
        /// <param name="entity">Model to Save</param>
        /// <returns>Save records id value</returns>
        public virtual int Save<TEntity>(TEntity entity)
        {
            if ((this.CheckForDuplicate && !this.HasDuplicate(entity)) || !this.CheckForDuplicate)
            {
                string procedureName = this.ProcedurePrefix + GetTableName(entity) + "Save";

                System.Collections.ObjectModel.Collection<DBParameters> parameters = AddParameters(entity);

                /*Execute Stored Procedure*/
                object primaryKeyValue = DBClient.ExecuteProcedure(procedureName, ExecuteType.ExecuteScalar, parameters, this.DatabaseConnection);

                return Convert.ToInt32(primaryKeyValue, CultureInfo.InvariantCulture);
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Save the PDF details corresponding to student Id
        /// </summary>
        /// <typeparam name="TEntity">Model Type</typeparam>
        /// <param name="entity">Save the PDF details corresponding to student Id</param>
        /// <returns>Save records id value</returns>
        public virtual int SavePDFDetails<TEntity>(TEntity entity)
        {
            if ((this.CheckForDuplicate && !this.HasDuplicate(entity)) || !this.CheckForDuplicate)
            {
                string procedureName = "UspStudentDocumentDetailsSave";

                System.Collections.ObjectModel.Collection<DBParameters> parameters = AddParameters(entity);

                /*Execute Stored Procedure*/
                object primaryKeyValue = DBClient.ExecuteProcedure(procedureName, ExecuteType.ExecuteScalar, parameters, this.DatabaseConnection);

                return Convert.ToInt32(primaryKeyValue, CultureInfo.InvariantCulture);
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Delete the matching record with primary key value
        /// </summary>
        /// <typeparam name="TEntity">Model Type</typeparam>
        /// <param name="primaryKey">primary key value of record to be delete</param>
        /// <returns>return deleted entity primary key value</returns>
        public virtual int Delete<TEntity>(int primaryKey)
        {
            TEntity entityObject = default(TEntity);
            entityObject = Activator.CreateInstance<TEntity>();
            /*define Stored Procedure Name*/
            string procedureName = this.ProcedurePrefix + GetTableName(entityObject) + "Delete";

            /*Execute Stored Procedure*/
            try
            {
                /*Add Primary Key as Parameter*/
                System.Collections.ObjectModel.Collection<DBParameters> parameters = new System.Collections.ObjectModel.Collection<DBParameters>();
                parameters.Add(new DBParameters()
                {
                    Name = GetKeyName(entityObject),
                    Value = primaryKey,
                    DBType = GetPropertyType(primaryKey.GetType())
                });
                DBClient.ExecuteProcedure(procedureName, ExecuteType.ExecuteNonQuery, parameters, this.DatabaseConnection);
                return 0;
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number == 50000 || sqlEx.Number == 547)
                {
                    return -1;
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Delete the matching record with primary key value
        /// </summary>
        /// <typeparam name="TEntity">Model Type</typeparam>
        /// <param name="primaryKey">primary key value of record to be delete</param>
        /// <returns>return deleted entity primary key value</returns>
        public virtual int Delete<TEntity>(long primaryKey)
        {
            TEntity entityObject = default(TEntity);
            entityObject = Activator.CreateInstance<TEntity>();
            /*define Stored Procedure Name*/
            string procedureName = this.ProcedurePrefix + GetTableName(entityObject) + "Delete";

            /*Execute Stored Procedure*/
            try
            {
                /*Add Primary Key as Parameter*/
                System.Collections.ObjectModel.Collection<DBParameters> parameters = new System.Collections.ObjectModel.Collection<DBParameters>();
                parameters.Add(new DBParameters()
                {
                    Name = GetKeyName(entityObject),
                    Value = primaryKey,
                    DBType = GetPropertyType(primaryKey.GetType())
                });
                DBClient.ExecuteProcedure(procedureName, ExecuteType.ExecuteNonQuery, parameters, this.DatabaseConnection);
                return 0;
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                if (sqlEx.Number == 50000 || sqlEx.Number == 547)
                {
                    return -1;
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Return the list of model for given search criteria
        /// </summary>
        /// <typeparam name="TEntity">Model Type</typeparam>
        /// <param name="entity">Model with Search Criteria</param>
        /// <returns>List of Models</returns>
        public virtual IList<TEntity> Search<TEntity>(TEntity entity)
        {
            return this.Search<TEntity>(entity, 0, string.Empty, string.Empty);
        }

        /// <summary>
        /// Return the list of model for given search criteria
        /// </summary>
        /// <typeparam name="TEntity">Model Type</typeparam>
        /// <param name="entity">Model with Search Criteria</param>
        /// <param name="pageNo">current page no</param>
        /// <returns>List of Models</returns>
        public virtual IList<TEntity> Search<TEntity>(TEntity entity, int? pageNo)
        {
            return this.Search<TEntity>(entity, pageNo, string.Empty, string.Empty);
        }

        /// <summary>
        /// Return the list of model for given search criteria
        /// </summary>
        /// <typeparam name="TEntity">Model Type</typeparam>
        /// <param name="entity">Model with Search Criteria</param>
        /// <param name="pageNo">current page no</param>
        /// <param name="sortExpression">Sort Expression</param>
        /// <param name="sortDirection">Sort Direction</param>
        /// <returns>List of Models</returns>
        public virtual IList<TEntity> Search<TEntity>(TEntity entity, int? pageNo, string sortExpression, string sortDirection)
        {
            /*define Stored Procedure Name*/
            string procedureName = this.ProcedurePrefix + GetTableName(entity) + "Search";
            /*Add Parameters*/
            System.Collections.ObjectModel.Collection<DBParameters> parameters = AddParameters(entity, true);
            if (this.StartRowIndex(pageNo) > 0 && this.EndRowIndex(pageNo) > 0)
            {
                parameters.Add(new DBParameters() { Name = "StartRowIndex", Value = this.StartRowIndex(pageNo), DBType = DbType.Int32 });
                parameters.Add(new DBParameters() { Name = "EndRowIndex", Value = this.EndRowIndex(pageNo), DBType = DbType.Int32 });
            }

            if (!string.IsNullOrEmpty(sortExpression) && !string.IsNullOrEmpty(sortDirection))
            {
                parameters.Add(new DBParameters() { Name = "SortExpression", Value = sortExpression, DBType = DbType.String });
                parameters.Add(new DBParameters() { Name = "SortDirection", Value = sortDirection, DBType = DbType.String });
            }

            /*Convert Dataset to Model List object*/
            return this.ExecuteProcedure<TEntity>(procedureName, parameters);
        }

        /// <summary>
        /// Return the Object of Model with data for given primary key value
        /// </summary>
        /// <typeparam name="TEntity">Model Type</typeparam>
        /// <param name="primaryKey">primary key value of record to be select</param>
        /// <returns>Model Object</returns>
        public virtual TEntity SelectObject<TEntity>(int primaryKey)
        {
            TEntity entityObject = default(TEntity);
            entityObject = Activator.CreateInstance<TEntity>();
            //// set primary Key value to object
            PropertyInfo info = entityObject.GetType().GetProperties().FirstOrDefault(o => o.Name.ToLower() == GetKeyName(entityObject).ToLower());
            if (info != null)
            {
                /*Set the Value to Model*/
                info.SetValue(entityObject, primaryKey, null);

                var list = Search(entityObject);
                if (list.Count() > 0)
                {
                    return list.FirstOrDefault();
                }
                else 
                {
                    entityObject = Activator.CreateInstance<TEntity>();
                }
            }

            return entityObject;
        }

        /// <summary>
        /// Return the Object of Model with data for given primary key value
        /// </summary>
        /// <typeparam name="TEntity">Model Type</typeparam>
        /// <param name="primaryKey">primary key value of record to be select</param>
        /// <returns>Model Object</returns>
        public virtual TEntity SelectObject<TEntity>(long primaryKey)
        {
            TEntity entityObject = default(TEntity);
            entityObject = Activator.CreateInstance<TEntity>();
            //// set primary Key value to object
            PropertyInfo info = entityObject.GetType().GetProperties().FirstOrDefault(o => o.Name.ToLower() == GetKeyName(entityObject).ToLower());
            if (info != null)
            {
                /*Set the Value to Model*/
                info.SetValue(entityObject, primaryKey, null);

                var list = Search(entityObject);
                if (list.Count() > 0)
                {
                    return list.FirstOrDefault();
                }
                else
                {
                    entityObject = Activator.CreateInstance<TEntity>();
                }
            }

            return entityObject;
        }

        /// <summary>
        /// Return the Object of Model with data for given primary key value
        /// </summary>
        /// <typeparam name="TEntity">Model Type</typeparam>
        /// <param name="primaryKey">primary key value of record to be select</param>
        /// <returns>Model Object</returns>
        public virtual TEntity SelectObject<TEntity>(short primaryKey)
        {
            TEntity entityObject = default(TEntity);
            entityObject = Activator.CreateInstance<TEntity>();
            //// set primary Key value to object
            PropertyInfo info = entityObject.GetType().GetProperties().FirstOrDefault(o => o.Name.ToLower() == GetKeyName(entityObject).ToLower());
            if (info != null)
            {
                /*Set the Value to Model*/
                info.SetValue(entityObject, primaryKey, null);

                var list = Search(entityObject);
                if (list.Count() > 0)
                {
                    return list.FirstOrDefault();
                }
                else
                {
                    entityObject = Activator.CreateInstance<TEntity>();
                }
            }

            return entityObject;
        }

        /// <summary>
        /// Check Duplicate Records in Database
        /// </summary>
        /// <typeparam name="TEntity">Model Type</typeparam>
        /// <param name="entity">Entity Model</param>
        /// <returns>returns entity is duplicate or not</returns>
        public bool HasDuplicate<TEntity>(TEntity entity)
        {
            System.Collections.ObjectModel.Collection<DBParameters> parameters = new System.Collections.ObjectModel.Collection<DBParameters>();
            parameters.Add(new DBParameters() { Name = "tableName", Value = GetTableName(entity), DBType = DbType.String });
            parameters.Add(new DBParameters() { Name = "columnName", Value = this.Col1Name, DBType = DbType.String });
            parameters.Add(new DBParameters() { Name = "columnNameValue", Value = GetPropertyValue(entity, this.Col1Name).Replace("'", "''"), DBType = DbType.String });
            if (!string.IsNullOrEmpty(this.Col2Name))
            {
                parameters.Add(new DBParameters() { Name = "columnName2", Value = this.Col2Name, DBType = DbType.String });

                parameters.Add(new DBParameters() { Name = "columnName2Value", Value = GetPropertyValue(entity, this.Col2Name).Replace("'", "''"), DBType = DbType.String });

                parameters.Add(new DBParameters() { Name = "IsCombinationCheck", Value = this.CombinationCheckRequired, DBType = DbType.Boolean });
            }

            if (!string.IsNullOrEmpty(this.Col3Name))
            {
                parameters.Add(new DBParameters() { Name = "columnName3", Value = this.Col3Name, DBType = DbType.String });

                parameters.Add(new DBParameters() { Name = "columnName3Value", Value = GetPropertyValue(entity, this.Col3Name).Replace("'", "''"), DBType = DbType.String });
            }

            parameters.Add(new DBParameters() { Name = "primaryKey", Value = GetKeyName(entity), DBType = DbType.String });

            parameters.Add(new DBParameters() { Name = "primaryKeyValue", Value = GetKeyValue(entity).ToString(), DBType = DbType.String });

            DataSet ds = (DataSet)DBClient.ExecuteProcedure("UspGeneralCheckDuplicate", ExecuteType.ExecuteDataSet, parameters, this.DatabaseConnection);
            return ds.Tables[0].Rows.Count > 0;
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// The virtual dispose method that allows
        /// classes inherited from this one to dispose their resources.
        /// </summary>
        /// <param name="disposing">Is Dispose</param>
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

        #region static methods

        /// <summary>
        /// Check Current Property is Primary Key or not
        /// </summary>
        /// <param name="info">Property Information</param>
        /// <returns>true or false</returns>
        private static bool IsPrimaryKey(PropertyInfo info)
        {
            var attribute = Attribute.GetCustomAttribute(info, typeof(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedAttribute));

            /* This property has a KeyAttribute*/
            return attribute != null;
        }

        /// <summary>
        /// Get Primary Key of Type T
        /// </summary>
        /// <typeparam name="TEntity">Model Type</typeparam>
        /// <param name="entity">Model object</param>
        /// <returns>Primary key value</returns>
        private static int? GetKeyValue<TEntity>(TEntity entity)
        {
            try
            {
                PropertyInfo[] infos = entity.GetType().GetProperties();
                foreach (PropertyInfo info in infos)
                {
                    /* Check for Key Attribute for Primary Key*/
                    if (IsPrimaryKey(info))
                    {
                        /* Get Property Value and return*/
                        object val = info.GetValue(entity, null);
                        return Convert.ToInt32(val, CultureInfo.InvariantCulture);
                    }
                }

                return null;
            }
            catch
            {
                Console.WriteLine("There is an error in getting primary key value.");
                throw;
            }
        }

        /// <summary>
        ///  Get Primary Key of Type TEntity
        /// </summary>
        /// <typeparam name="TEntity">Model Type</typeparam>
        /// <param name="entity">Model to Get Name</param>
        /// <returns>String : Key Name</returns>
        private static string GetKeyName<TEntity>(TEntity entity)
        {
            try
            {
                PropertyInfo[] infos = entity.GetType().GetProperties();
                foreach (PropertyInfo info in infos)
                {
                    /* Check for Key Attribute for Primary Key*/
                    if (IsPrimaryKey(info))
                    {
                        // Return Name of Key Property
                        return info.Name;
                    }
                }

                return null;
            }
            catch (Exception)
            {
                Console.WriteLine("There is an error in getting primary key Name.");
                throw;
            }
        }

        /// <summary>
        /// Get Table Name from Model
        /// </summary>
        /// <typeparam name="TEntity">Model Type</typeparam>
        /// <param name="entity">Model to Get Name</param>
        /// <returns>Table Name</returns>
        private static string GetTableName<TEntity>(TEntity entity)
        {
            var tableAttribute = Attribute.GetCustomAttribute(typeof(TEntity), typeof(System.ComponentModel.DataAnnotations.Schema.TableAttribute)) as System.ComponentModel.DataAnnotations.Schema.TableAttribute;
            if (tableAttribute != null)
            {
                return tableAttribute.Name;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Check the Property Information and verify for adding in Parameter
        /// </summary>
        /// <param name="info">Property Info</param>
        /// <param name="value">Property Value</param>
        /// <param name="isForSearch">Is validation for Search</param>
        /// <returns>true for Add as Parameter or false</returns>
        private static bool ParameterValidation(PropertyInfo info, object value, bool isForSearch = false)
        {
            var notMapped = info.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.Schema.NotMappedAttribute), true);
            var complexType = info.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.Schema.ColumnAttribute), true);

            /* Check Property is primariy key with value or not primary but has value and either Table column or complex type and with search truue */
            if ((notMapped.Count() == 0 || (complexType.Count() > 0 && isForSearch)) && ((value != null && !IsPrimaryKey(info)) || (IsPrimaryKey(info) && Convert.ToInt32(value, CultureInfo.InvariantCulture) > 0)))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Get the Command DBType from Property
        /// </summary>
        /// <param name="type">Property Type</param>
        /// <returns>appropriate DBType</returns>
        private static DbType GetPropertyType(Type type)
        {
            // Match type Name and return DB Type
            switch (type.Name.ToLower(System.Globalization.CultureInfo.CurrentCulture))
            {
                case "byte[]":
                    return DbType.Binary;
                case "int64":
                case "int64?":
                case "long":
                case "long?":
                    return DbType.Int64;
                case "boolean":
                case "bool":
                case "bool?":
                    return DbType.Boolean;
                case "char":
                case "string":
                    return DbType.String;
                case "datetime":
                case "datetime?":
                    return DbType.DateTime;
                case "decimal":
                case "decimal?":
                    return DbType.Decimal;
                case "double":
                case "double?":
                case "float":
                case "float?":
                    return DbType.Double;
                case "int":
                case "int?":
                case "int32":
                case "int32?":
                    return DbType.Int32;
                case "int16":
                case "int16?":
                    return DbType.Int16;
                case "real":
                    return DbType.Single;
                case "guid":
                    return DbType.Guid;
                case "nullable`1":
                    if (type == typeof(Nullable<int>))
                    {
                        /* || type == typeof(Nullable<Int16>) || type == typeof(Nullable<Int32>))*/
                        return DbType.Int32;
                    }
                    else if (type == typeof(Nullable<long>))
                    {
                        /*|| type == typeof(Nullable<Int64>))*/
                        return DbType.Int64;
                    }
                    else if (type == typeof(Nullable<DateTime>))
                    {
                        return DbType.DateTime;
                    }
                    else if (type == typeof(Nullable<bool>))
                    {
                        /* || type == typeof(Nullable<Boolean>))*/
                        return DbType.Boolean;
                    }
                    else if (type == typeof(Nullable<decimal>))
                    {
                        /* || type == typeof(Nullable<Decimal>))*/
                        return DbType.Decimal;
                    }
                    else if (type == typeof(Nullable<float>) || type == typeof(Nullable<double>))
                    {
                        /* || type == typeof(Nullable<Double>))*/
                        return DbType.Double;
                    }
                    else if (type == typeof(Nullable<System.TimeSpan>))
                    {
                        return DbType.Time;
                    }
                    else
                    {
                        return DbType.String;
                    }
            }

            return DbType.String;
        }

        #endregion
    }
}

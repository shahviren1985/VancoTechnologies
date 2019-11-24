using AdmissionForm.Business.Model;
using AdmissionForm.DataContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AdmissionForm.Business.Services
{
    /// <summary>
    /// This class is used to Define Database object for Save, Search, Delete
    /// </summary>
    /// <CreatedBy>Kaushik Patel</CreatedBy>
    /// <CreatedDate>06-Aug-2015</CreatedDate>
    public sealed class ServiceContext : DBContext
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ServiceContext class with database name.
        /// </summary>
        /// <param name="databaseName">database Name value</param>
        /// <param name="isFromWindowService">Is from window service</param>
        public ServiceContext()
        {
            this.PagingInformation = new Pagination() { PageSize = DefaultPageSize, PagerSize = DefaultPagerSize };
            this.CheckForDuplicate = false;
        }

        /// <summary>
        /// Initializes a new instance of the ServiceContext class for checking duplicate value for one column
        /// </summary>
        /// <param name="col1Name">column Name</param>
        public ServiceContext(string col1Name)
        {
            this.PagingInformation = new Pagination() { PageSize = DefaultPageSize, PagerSize = DefaultPagerSize };
            this.CheckForDuplicate = true;
            this.Col1Name = col1Name;
            this.CombinationCheckRequired = false;
        }

        /// <summary>
        /// Initializes a new instance of the ServiceContext class for checking duplicate value for two column with combination
        /// </summary>
        /// <param name="col1Name">first column name</param>
        /// <param name="col2Name">second column name</param>
        public ServiceContext(string col1Name, string col2Name)
        {
            this.PagingInformation = new Pagination() { PageSize = DefaultPageSize, PagerSize = DefaultPagerSize };

            this.CheckForDuplicate = true;
            this.Col1Name = col1Name;
            this.Col2Name = col2Name;
            this.CombinationCheckRequired = true;
        }

        /// <summary>
        /// Initializes a new instance of the ServiceContext class for checking duplicate value for two column with combination
        /// </summary>
        /// <param name="col1Name">first column name</param>
        /// <param name="col2Name">second column name</param>
        /// <param name="col3Name">third column name</param>
        public ServiceContext(string col1Name, string col2Name, string col3Name)
        {
            this.CheckForDuplicate = true;
            this.Col1Name = col1Name;
            this.Col2Name = col2Name;
            this.Col3Name = col3Name;
            this.CombinationCheckRequired = true;
        }

        /// <summary>
        /// Initializes a new instance of the ServiceContext class for checking duplicate value for two column
        /// </summary>
        /// <param name="col1Name">first column name</param>
        /// <param name="col2Name">second column name</param>
        /// <param name="combinationCheckRequired">Combination Check Required</param>
        public ServiceContext(string col1Name, string col2Name, bool combinationCheckRequired)
        {
            this.PagingInformation = new Pagination() { PageSize = DefaultPageSize, PagerSize = DefaultPagerSize };

            this.CheckForDuplicate = true;
            this.Col1Name = col1Name;
            this.Col2Name = col2Name;
            this.CombinationCheckRequired = combinationCheckRequired;
        }

        /// <summary>
        /// Initializes a new instance of the ServiceContext class for checking duplicate value for two column
        /// </summary>
        /// <param name="col1Name">first column name</param>
        /// <param name="col2Name">second column name</param>
        /// <param name="col3Name">second column name</param>
        /// <param name="combinationCheckRequired">Combination Check Required</param>
        public ServiceContext(string col1Name, string col2Name, string col3Name, bool combinationCheckRequired)
        {
            this.PagingInformation = new Pagination() { PageSize = DefaultPageSize, PagerSize = DefaultPagerSize };

            this.CheckForDuplicate = true;
            this.Col1Name = col1Name;
            this.Col2Name = col2Name;
            this.Col3Name = col3Name;
            this.CombinationCheckRequired = combinationCheckRequired;
        }

        #endregion

        #region Custom Methods 
        public bool CheckDuplicate(string tableName, string columnName, string columnValue, string primaryKey, string primaryKeyValue, string col2Name = "", string columnName2Value = "", bool combinationCheckRequired = false)
        {
            System.Collections.ObjectModel.Collection<DBParameters> parameters = new System.Collections.ObjectModel.Collection<DBParameters>();
            parameters.Add(new DBParameters() { Name = "tableName", Value = tableName, DBType = DbType.String });
            parameters.Add(new DBParameters() { Name = "columnName", Value = columnName, DBType = DbType.String });
            parameters.Add(new DBParameters() { Name = "columnNameValue", Value = columnValue, DBType = DbType.String });
            if (!string.IsNullOrEmpty(col2Name))
            {
                parameters.Add(new DBParameters() { Name = "columnName2", Value = col2Name, DBType = DbType.String });

                parameters.Add(new DBParameters() { Name = "columnName2Value", Value = columnName2Value, DBType = DbType.String });

                parameters.Add(new DBParameters() { Name = "IsCombinationCheck", Value = combinationCheckRequired, DBType = DbType.Boolean });
            }

            parameters.Add(new DBParameters() { Name = "primaryKey", Value = primaryKey, DBType = DbType.String });

            parameters.Add(new DBParameters() { Name = "primaryKeyValue", Value = primaryKeyValue, DBType = DbType.String });

            DataSet ds = (DataSet)this.ExecuteProcedure("UspGeneralCheckDuplicate", ExecuteType.ExecuteDataSet, parameters);
            return ds.Tables[0].Rows.Count > 0;
        }

        #endregion


        public DataTable GenerateGeneralReport()
        {
            string conn = System.Configuration.ConfigurationManager.ConnectionStrings["RCTDBConnection"].ConnectionString;
            using (SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(conn))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("GenerateGeneralReport", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 60;
                DataTable dt = new DataTable();
                dt.Locale = System.Globalization.CultureInfo.InvariantCulture;
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                sqlAdapter.Fill(dt);

                return dt;
            }
        }
    }

}

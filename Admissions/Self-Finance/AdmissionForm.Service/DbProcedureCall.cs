using System;
using System.Collections.Generic;
using System.Data;
using AdmissionForm.Business.Model;
using AdmissionForm.DataContext;

namespace AdmissionForm.Business.Service
{
    /// <summary>
    /// This class is used to standard context to do any basic operations
    /// </summary>
    /// <CreatedBy>Kaushik Patel</CreatedBy>
    /// <CreatedDate>06-Sept-2015</CreatedDate>
    public sealed class DbProcedureCall : DBContext
    {
        public DbProcedureCall()
        {
            this.PagingInformation = new Pagination() { PageSize = DefaultPageSize, PagerSize = DefaultPagerSize };
        }


        public object CallStoreProcedureWithParam(string ids,string name)
        {
            /*Add Parameters*/
            System.Collections.ObjectModel.Collection<DBParameters> parameters = new System.Collections.ObjectModel.Collection<DBParameters>();
            parameters.Add(new DBParameters() { Name = "Ids", Value = ids, DBType = DbType.String });
            parameters.Add(new DBParameters() { Name = "name", Value = name, DBType = DbType.String });
            return this.ExecuteProcedure("USPProcedureName", ExecuteType.ExecuteScalar, parameters);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace GreenNub
{
    public class Connectiondb
    {
        String strcon = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["ConnectDB"]);
        System.Data.SqlClient.SqlConnection con;

        public void DBconnection()
        {
            con = new System.Data.SqlClient.SqlConnection(strcon);
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
        }

        public DataTable Return_datatable(String strq, System.Data.SqlClient.SqlParameter[] param)
        {
            DBconnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(strq, con);

            for (int i = 0; i < param.Length; i++)
            {
                cmd.Parameters.Add(param[i]);

            }
            cmd.CommandType = CommandType.StoredProcedure;
            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            return dt;
        }

        public DataTable FetchUser(Int32 ID)
        {
            try
            {
                System.Data.SqlClient.SqlParameter[] param = new System.Data.SqlClient.SqlParameter[14];
                param[0] = new System.Data.SqlClient.SqlParameter("@ResponseTemplateId", SqlDbType.Int);
                param[0].Direction = ParameterDirection.Input;
                param[0].Value = ID;

                param[1] = new System.Data.SqlClient.SqlParameter("@IncludeComplete", SqlDbType.Bit);
                param[1].Direction = ParameterDirection.Input;
                param[1].Value = true;

                param[2] = new System.Data.SqlClient.SqlParameter("@IncludeIncomplete", SqlDbType.Bit);
                param[2].Direction = ParameterDirection.Input;
                param[2].Value = true;

                param[3] = new System.Data.SqlClient.SqlParameter("@IncludeTest", SqlDbType.Bit);
                param[3].Direction = ParameterDirection.Input;
                param[3].Value = true;

                param[4] = new System.Data.SqlClient.SqlParameter("@MinResponseCompletedDate", SqlDbType.DateTime);
                param[4].Direction = ParameterDirection.Input;
                param[4].Value = null;

                param[5] = new System.Data.SqlClient.SqlParameter("@MaxResponseCompletedDate", SqlDbType.Date);
                param[5].Direction = ParameterDirection.Input;
                param[5].Value = null;

                param[6] = new System.Data.SqlClient.SqlParameter("@PageNumber", SqlDbType.Int);
                param[6].Direction = ParameterDirection.Input;
                param[6].Value = 1;

                param[7] = new System.Data.SqlClient.SqlParameter("@ResultsPerPage", SqlDbType.Int);
                param[7].Direction = ParameterDirection.Input;
                param[7].Value = 900;

                param[8] = new System.Data.SqlClient.SqlParameter("@SortField", SqlDbType.VarChar);
                param[8].Direction = ParameterDirection.Input;
                param[8].Value = "Started";

                param[9] = new System.Data.SqlClient.SqlParameter("@SortAscending", SqlDbType.Bit);
                param[9].Direction = ParameterDirection.Input;
                param[9].Value = 1;

                param[10] = new System.Data.SqlClient.SqlParameter("@FilterField", SqlDbType.VarChar);
                param[10].Direction = ParameterDirection.Input;
                param[10].Value = "";

                param[11] = new System.Data.SqlClient.SqlParameter("@FilterValue", SqlDbType.VarChar);
                param[11].Direction = ParameterDirection.Input;
                param[11].Value = "";

                param[12] = new System.Data.SqlClient.SqlParameter("@DateFieldName", SqlDbType.VarChar);
                param[12].Direction = ParameterDirection.Input;
                param[12].Value = "";


                param[13] = new System.Data.SqlClient.SqlParameter("@ProfileFieldId", SqlDbType.Int);
                param[13].Direction = ParameterDirection.Input;
                param[13].Value = 0;
                DataTable dt = Return_datatable("ckbx_sp_ResponseUser_List_API", param);
                return dt;
            }
            catch (Exception ex)
            {

                //command.AddInParameter("ResponseTemplateId", DbType.Int32, responseTemplateID);
                //command.AddInParameter("IncludeComplete", DbType.Boolean, includeComplete);
                //command.AddInParameter("IncludeIncomplete", DbType.Boolean, includeIncomplete);
                //command.AddInParameter("IncludeTest", DbType.Boolean, includeTest);
                //command.AddInParameter("MinResponseCompletedDate", DbType.DateTime, minResponseCompletedDate);
                //command.AddInParameter("MaxResponseCompletedDate", DbType.DateTime, maxResponseCompletedDate);
                //command.AddInParameter("ProfileFieldId", DbType.Int32, profileFieldId);
                throw ex;
            }
        }
    }
}
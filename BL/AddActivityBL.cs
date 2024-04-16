using Library;
using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class AddActivityBL
    {

        public SerializeResponse<ActivityEntity> AddActivity(ActivityEntity objEntity)
        {

            InsertLog.WriteErrrorLog("AddActivityBL=>AddActivity=>Started");
            ConvertDataTable bl = new ConvertDataTable();
            SerializeResponse<ActivityEntity> objResponsemessage = new SerializeResponse<ActivityEntity>();

            DataSet ds = new DataSet();
            SqlDataProvider objSDP = new SqlDataProvider();
            string query = "Add_Activity_SP";

            try
            {

                string Con_str = Connection.GetConnectionString();
                SqlParameter prm1 = objSDP.CreateInitializedParameter("@Name", DbType.String, objEntity.Name);
                SqlParameter prm2 = objSDP.CreateInitializedParameter("@Description", DbType.String, objEntity.Description);
                SqlParameter prm3 = objSDP.CreateInitializedParameter("@EventId", DbType.Int64, objEntity.EventId);
                SqlParameter prm4 = objSDP.CreateInitializedParameter("@StartDate", DbType.String, objEntity.StartDate);
                SqlParameter prm5 = objSDP.CreateInitializedParameter("@EndDate", DbType.Int64, objEntity.EndDate);


                

                SqlParameter[] Sqlpara = { prm1, prm2, prm3, prm4, prm5 };

                ds = SqlHelper.ExecuteDataset(Con_str, query, Sqlpara);

               //it will send an message that asctivity added successfully and sometthing went wrong it will return ID as -1 
               //and if activity is already exist in same event so it will return 0 as ID and message as activity aloready exists
                if (ds?.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    objResponsemessage.Message = Convert.ToString(ds.Tables[0].Rows[0]["ResponseMessage"]);
                    objResponsemessage.ID = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"]);
                }

                else
                {
                    objResponsemessage.Message = "400|No Data Found";
                }

            }
            catch (Exception ex)
            {
                objResponsemessage.Message = "500|Exception Occurred" + ex.Message;
                InsertLog.WriteErrrorLog("UserReBL=>RegisterUSer=>Exception" + ex.Message + ex.StackTrace);
            }
            return objResponsemessage;
        }


    }
}

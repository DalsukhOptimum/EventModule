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
    public class PublishOrAddPriceBL
    {
        public SerializeResponse<EventEntity> PriceorPublis(EventEntity objEntity)
        {

            InsertLog.WriteErrrorLog("PublishOrAddPriceBL=>PriceorPublisj=>Started");
            ConvertDataTable bl = new ConvertDataTable();
            SerializeResponse<EventEntity> objResponsemessage = new SerializeResponse<EventEntity>();

            DataSet ds = new DataSet();
            SqlDataProvider objSDP = new SqlDataProvider();
            string query = "Publish_AddPrice_SP";

            try
            {

                string Con_str = Connection.GetConnectionString();
                SqlParameter prm1 = objSDP.CreateInitializedParameter("@Price", DbType.String, objEntity.Price);
                SqlParameter prm2 = objSDP.CreateInitializedParameter("@ActivityId", DbType.String, objEntity.ActivityId);
                SqlParameter prm3 = objSDP.CreateInitializedParameter("@EventId", DbType.String, objEntity.EventId);
                SqlParameter prm4 = objSDP.CreateInitializedParameter("@Flag", DbType.String, objEntity.Flag);





                SqlParameter[] Sqlpara = { prm1, prm2, prm3, prm4 };

                ds = SqlHelper.ExecuteDataset(Con_str, query, Sqlpara);

                //it will send an message that user registered succesfully or message that already email is exist 
                //and if rgistered succesfully so it will send that user data in second data
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

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
        /// <summary>
        /// Name:Nanera Dalsukh
        /// Date:04-04-2024
        /// this is for adding a price or publish an Event.
        /// </summary>
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
                SqlParameter prm1 = objSDP.CreateInitializedParameter("@Price", DbType.Int64, objEntity.Price);
                SqlParameter prm2 = objSDP.CreateInitializedParameter("@ActivityId", DbType.String, objEntity.ActivityId);
                SqlParameter prm3 = objSDP.CreateInitializedParameter("@EventId", DbType.String, objEntity.EventId);
                SqlParameter prm4 = objSDP.CreateInitializedParameter("@Flag", DbType.String, objEntity.Flag);





                SqlParameter[] Sqlpara = { prm1, prm2, prm3, prm4 };

                ds = SqlHelper.ExecuteDataset(Con_str, query, Sqlpara);

                 //publish--> it will return ID as 1 when publish successfully and 0 when event don't have any activity
                 //Add Price--> it will return 1 when price added successfulyl and 0 when not added successfully
                if (ds?.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    objResponsemessage.Message = Convert.ToString(ds.Tables[0].Rows[0]["ResponseMessage"]);
                    objResponsemessage.ID = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"]);
                }

                else
                {
                    objResponsemessage.Message = "400|No Data Found";
                    objResponsemessage.ID = 0;
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

using Library;
using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    
namespace BL
{
    public class UpdateEvent
    {
        /// <summary>
        /// Name:Nanera Dalsukh
        /// Date:04-04-2024
        /// this is for updating an Event 
        /// </summary>

        public SerializeResponse<EventEntity> Update(EventEntity objEntity)
        {

            InsertLog.WriteErrrorLog("UpdateEvent=>Update=>Started");
            ConvertDataTable bl = new ConvertDataTable();
            SerializeResponse<EventEntity> objResponsemessage = new SerializeResponse<EventEntity>();

            DataSet ds = new DataSet();
            SqlDataProvider objSDP = new SqlDataProvider();

            //checking that whether image is null or not null
            //and if not null so converting it into base64 to image 
            string imgpath="";
            if (objEntity.Image != null)
            {
                byte[] imageBytes = Convert.FromBase64String(objEntity.Image);
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Images");


                imgpath = AppDomain.CurrentDomain.BaseDirectory + "Images\\" + DateTime.Now.ToString("HH:mm:ss").Replace(":", "-") + "." + objEntity.ImageType;

                File.WriteAllBytes(imgpath, imageBytes);
            }


           //converting date formate of angular to SQL 
            objEntity.StartDate = objEntity.StartDate + " 00:00:00.000";
            objEntity.EndDate = objEntity.EndDate + " 00:00:00.000";

            string query = "SPUpdateEvent";

            try
            {

                string Con_str = Connection.GetConnectionString();
                SqlParameter prm1 = objSDP.CreateInitializedParameter("@Name", DbType.String, objEntity.Name);
                SqlParameter prm2 = objSDP.CreateInitializedParameter("@Description", DbType.String, objEntity.Description);
                SqlParameter prm3 = objSDP.CreateInitializedParameter("@StartDate", DbType.String,objEntity.StartDate);
                SqlParameter prm4 = objSDP.CreateInitializedParameter("@EndDate", DbType.String, objEntity.EndDate);
                SqlParameter prm5 = objSDP.CreateInitializedParameter("@Image", DbType.String, imgpath);
                SqlParameter prm6 = objSDP.CreateInitializedParameter("@EventId", DbType.Int64, objEntity.EventId);




                SqlParameter[] Sqlpara = { prm1, prm2, prm3, prm4, prm5, prm6 };

                ds = SqlHelper.ExecuteDataset(Con_str, query, Sqlpara);

                //it will return an message that event Updated successfully
                //or some EventId is not valid it will send not valid Event
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

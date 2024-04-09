using Library;
using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BL
{
    public class AddEventBL
    {

        public SerializeResponse<EventEntity> AddEvent(EventEntity objEntity)
        {

            InsertLog.WriteErrrorLog("AddEventBL=>AddEvent=>Started");
            ConvertDataTable bl = new ConvertDataTable();
            SerializeResponse<EventEntity> objResponsemessage = new SerializeResponse<EventEntity>();

            DataSet ds = new DataSet();
            SqlDataProvider objSDP = new SqlDataProvider();

            byte[] imageBytes = Convert.FromBase64String(objEntity.Image);
            Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Images");

            
            string imgpath = AppDomain.CurrentDomain.BaseDirectory + "Images\\" + DateTime.Now.ToString("HH:mm:ss").Replace(":","-") + "." + objEntity.ImageType;

            File.WriteAllBytes(imgpath, imageBytes);

            

            string query = "Add_Event_SP";
            
            try
            {

                string Con_str = Connection.GetConnectionString();
                SqlParameter prm1 = objSDP.CreateInitializedParameter("@Name", DbType.String, objEntity.Name);
                SqlParameter prm2 = objSDP.CreateInitializedParameter("@Description", DbType.String, objEntity.Description);
                SqlParameter prm3 = objSDP.CreateInitializedParameter("@StartDate", DbType.String, objEntity.StartDate);
                SqlParameter prm4 = objSDP.CreateInitializedParameter("@Enddate", DbType.String, objEntity.EndDate);
                SqlParameter prm5 = objSDP.CreateInitializedParameter("@Image", DbType.String, imgpath);




                SqlParameter[] Sqlpara = { prm1, prm2, prm3, prm4, prm5 };

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

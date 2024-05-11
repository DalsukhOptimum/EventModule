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
    public class OTPMatchBL
    {

        public SerializeResponse<EmailEntity> otpComapre(EmailEntity objEntity)
        {

            InsertLog.WriteErrrorLog("OTPMatchBL=>otpCompare=>Started");
            ConvertDataTable bl = new ConvertDataTable();
            SerializeResponse<EmailEntity> objResponsemessage = new SerializeResponse<EmailEntity>();

            DataSet ds = new DataSet();
            SqlDataProvider objSDP = new SqlDataProvider();
            string query = "OTP_Veryfication_SP";

            try
            {

                string Con_str = Connection.GetConnectionString();
                SqlParameter prm1 = objSDP.CreateInitializedParameter("@Email", DbType.String, objEntity.Email);
                SqlParameter prm2 = objSDP.CreateInitializedParameter("@OTP", DbType.String, objEntity.OTP);
            
                SqlParameter[] Sqlpara = { prm1, prm2 };

                ds = SqlHelper.ExecuteDataset(Con_str, query, Sqlpara);

                //it will return ID as 1 when login successfully and 0 when not valid user and -1 when something went wrong
                //and also send a message that login successfully or invalid user
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Library;
using System.Data.SqlClient;
using System.Data;
using Models;

namespace BL
{
    public class EmailVarificationBL
    {
        /// <summary>
        /// Name:Nanera Dalsukh
        /// Date:11-05-2024
        /// this is for Email Validation
        /// </summary>
        public SerializeResponse<EmailEntity> Emailverify(EmailEntity objEntity)
        {


                InsertLog.WriteErrrorLog("LoginAdimAndUserBL=>LoginUser=>Started");
                ConvertDataTable bl = new ConvertDataTable();
                SerializeResponse<EmailEntity> objResponsemessage = new SerializeResponse<EmailEntity>();

                DataSet ds = new DataSet();
                SqlDataProvider objSDP = new SqlDataProvider();
                string query = "sp_GetTemplate";
               
                string otp = "";
                Random rand = new Random();

            try
            {

                string Con_str = Connection.GetConnectionString();
                SqlParameter prm1 = objSDP.CreateInitializedParameter("@Name", DbType.String, "OTP");
                SqlParameter prm2 = objSDP.CreateInitializedParameter("@Email", DbType.String,objEntity.Email);


                SqlParameter[] Sqlpara = { prm1, prm2 };

                ds = SqlHelper.ExecuteDataset(Con_str, query, Sqlpara);


                if (ds?.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    objResponsemessage.Message = Convert.ToString(ds.Tables[0].Rows[0]["ResponseMessage"]);
                    objResponsemessage.ID = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"]);

                    if (ds?.Tables.Count > 1 && ds.Tables[0].Rows.Count > 0)
                    {
                        objResponsemessage.ArrayOfResponse = bl.ListConvertDataTable<EmailEntity>(ds.Tables[1]);
                        objResponsemessage.ArrayOfResponse[0].OTP = Convert.ToString(ds.Tables[0].Rows[0]["otp"]);
                        objResponsemessage.ArrayOfResponse[0].Email = objEntity.Email;
                        objResponsemessage.ArrayOfResponse[0].Body = objResponsemessage.ArrayOfResponse[0].Body.Replace("[OTP]", objResponsemessage.ArrayOfResponse[0].OTP);
                        SendMailBL sendMailBL = new SendMailBL();
                        sendMailBL.sendMail(objResponsemessage.ArrayOfResponse[0]);
                    }


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

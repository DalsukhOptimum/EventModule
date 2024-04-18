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
    public class LoginAdimAndUserBL
    {
        /// <summary>
        /// Name:Nanera Dalsukh
        /// Date:04-04-2024
        /// this is for login user and admin 
        /// </summary>
        public SerializeResponse<UserEntity> Login(UserEntity objEntity)
        {

            InsertLog.WriteErrrorLog("LoginAdimAndUserBL=>LoginUser=>Started");
            ConvertDataTable bl = new ConvertDataTable();
            SerializeResponse<UserEntity> objResponsemessage = new SerializeResponse<UserEntity>();

            DataSet ds = new DataSet();
            SqlDataProvider objSDP = new SqlDataProvider();
            string query = "LoginSP";

            try
            {

                string Con_str = Connection.GetConnectionString();
                SqlParameter prm1 = objSDP.CreateInitializedParameter("@Email", DbType.String, objEntity.Email);
                SqlParameter prm2 = objSDP.CreateInitializedParameter("@Password", DbType.String, objEntity.Password);
                SqlParameter prm3 = objSDP.CreateInitializedParameter("@Flag", DbType.String, objEntity.Flag);





                SqlParameter[] Sqlpara = { prm1, prm2, prm3 };

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

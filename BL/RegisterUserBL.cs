using Library;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;


namespace BL
{
    public class RegisterUserBL
    {

        public SerializeResponse<UserEntity> RegisterUSer(UserEntity objEntity)
        {

            InsertLog.WriteErrrorLog("UserReBL=>RegisterUSer=>Started");
            ConvertDataTable bl = new ConvertDataTable();
            SerializeResponse<UserEntity> objResponsemessage = new SerializeResponse<UserEntity>();

            DataSet ds = new DataSet();
            SqlDataProvider objSDP = new SqlDataProvider();
            string query = "RegisterUser_SP";

            try
            {

                string Con_str = Connection.GetConnectionString();
                SqlParameter prm1 = objSDP.CreateInitializedParameter("@Name", DbType.String, objEntity.Name);
                SqlParameter prm2 = objSDP.CreateInitializedParameter("@Email", DbType.String, objEntity.Email);
                SqlParameter prm3 = objSDP.CreateInitializedParameter("@Mobile", DbType.String, objEntity.Mobile);
                SqlParameter prm4 = objSDP.CreateInitializedParameter("@Address", DbType.String, objEntity.Address);
                SqlParameter prm5 = objSDP.CreateInitializedParameter("@Password", DbType.String, objEntity.Password);




                SqlParameter[] Sqlpara = { prm1, prm2, prm3, prm4, prm5 };

                ds = SqlHelper.ExecuteDataset(Con_str, query, Sqlpara);

                //it will send an message that user registered succesfully or message that already email is exist 
                //and if rgistered succesfully so it will send that user data in second data
                if (ds?.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    objResponsemessage.Message = Convert.ToString(ds.Tables[0].Rows[0]["Resultmessage"]);
                    objResponsemessage.ID = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"]);

                    if (ds?.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                    {
                        objResponsemessage.ArrayOfResponse = bl.ListConvertDataTable<UserEntity>(ds.Tables[1]);

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

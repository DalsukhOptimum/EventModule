﻿using Library;
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
    public class ShowEventActivity
    {

        public SerializeResponse<EventEntity> activityAndEvent(EventEntity objEntity)
        {

            InsertLog.WriteErrrorLog("ShowEventActivity=>activityAndEvent=>Started");
            ConvertDataTable bl = new ConvertDataTable();
            SerializeResponse<EventEntity> objResponsemessage = new SerializeResponse<EventEntity>();

            DataSet ds = new DataSet();
            SqlDataProvider objSDP = new SqlDataProvider();
            string query = "Showing_Event_Activity_SP";

            try
            {

                string Con_str = Connection.GetConnectionString();
                SqlParameter prm1 = objSDP.CreateInitializedParameter("@EventId", DbType.Int64, objEntity.EventId);
                SqlParameter prm2 = objSDP.CreateInitializedParameter("@Flag", DbType.String, objEntity.Flag);





                SqlParameter[] Sqlpara = { prm1, prm2 };

                ds = SqlHelper.ExecuteDataset(Con_str, query, Sqlpara);

                //it will send an message that user registered succesfully or message that already email is exist 
                //and if rgistered succesfully so it will send that user data in second data
                if (ds?.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {


                    objResponsemessage.Message = Convert.ToString(ds.Tables[0].Rows[0]["ResponseMessage"]);
                    objResponsemessage.ID = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"]);


                    if(ds?.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                    {
                        objResponsemessage.ArrayOfResponse = bl.ListConvertDataTable<EventEntity>(ds.Tables[1]);
                        if (objEntity.Flag != "ActibityShow")
                        {
                            foreach (var item in objResponsemessage.ArrayOfResponse)
                            {
                                item.ImageType = item.Image.ToString().Split('.')[1];
                                byte[] imageArray = System.IO.File.ReadAllBytes(item.Image);
                                string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                                item.Image = base64ImageRepresentation;
                               
                            }
                        }

                      
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
                objResponsemessage.ID = -1;
                InsertLog.WriteErrrorLog("UserReBL=>RegisterUSer=>Exception" + ex.Message + ex.StackTrace);
            }
            return objResponsemessage;
        }

    }
}
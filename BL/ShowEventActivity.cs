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
        /// <summary>
        /// Name:Nanera Dalsukh
        /// Date:04-04-2024
        /// this is for showing an Event or Activity
        /// while fetching an event it will convert an Event image to base64 string 
        /// </summary>
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

                // it will return activities or events based on flags and when return successfully it will return 1 as ID 
                //if no events available or activity is not available it will return 0 as ID and in errors it will return -1 as ID 
                if (ds?.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {


                    objResponsemessage.Message = Convert.ToString(ds.Tables[0].Rows[0]["ResponseMessage"]);
                    objResponsemessage.ID = Convert.ToInt32(ds.Tables[0].Rows[0]["ID"]);


                    if(ds?.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                    {
                        objResponsemessage.ArrayOfResponse = bl.ListConvertDataTable<EventEntity>(ds.Tables[1]);
                        if (objEntity.Flag != "ActibityShow" && objEntity.Flag != "AdminActibityShow" && objEntity.Flag != "Monthwise")
                        {
                            //converting an image to base64 for sedning to angular
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

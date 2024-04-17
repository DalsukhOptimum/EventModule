using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Library;
using Models;
using BL;
using EventModule.Filters;
using System.Web;


namespace EventModule.Controllers
{
    [MyAttribute]
    public class EventModuleController : ApiController
    {
        // api for  REgistration of user
        [Route("api/EventModule/RegisterUser")]
        [HttpPost]

        public HttpResponseMessage RegisterUser([FromBody] UserEntity user)
        {
            InsertLog.WriteErrrorLog("api/EventModule/RegisterUser=>API call starte");
            SerializeResponse<UserEntity> Response = new SerializeResponse<UserEntity>();
            try
            {
                RegisterUserBL userbl = new RegisterUserBL();
                Response = userbl.RegisterUSer(user);
               
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrrorLog("api/EventModule/RegisterUser" + ex.Message + ex.StackTrace);
                Response.Message = ex.Message;
                Response.ID = -1;
                
            }
            return Request.CreateResponse(HttpStatusCode.OK, Response);


        }

        //API for login for admin And User
        [Route("api/EventModule/Login")]
        [HttpPost]

        public HttpResponseMessage Login([FromBody] UserEntity user)
        {
            InsertLog.WriteErrrorLog("api/EventModule/Login=>API call starte");
            SerializeResponse<UserEntity> Response = new SerializeResponse<UserEntity>();
            try
            {
                LoginAdimAndUserBL loginuserbl = new LoginAdimAndUserBL();
                Response = loginuserbl.Login(user);
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrrorLog("api/EventModule/Login" + ex.Message + ex.StackTrace);
                Response.Message = ex.Message;
                Response.ID = -1;
               
            }
            return Request.CreateResponse(HttpStatusCode.OK, Response);

        }
        //API for adding Events
        [Route("api/EventModule/AddEvent")]
        [HttpPost]

        public HttpResponseMessage AddEvent( EventEntity Event)
        {
            InsertLog.WriteErrrorLog("api/EventModule/AddEvent=>API call started");
            SerializeResponse<EventEntity> Response = new SerializeResponse<EventEntity>();
            try
            {
                 AddEventBL addEventBL = new AddEventBL();
                Response= addEventBL.AddEvent(Event);
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrrorLog("api/EventModule/AddEvent" + ex.Message + ex.StackTrace);
                Response.Message = ex.Message;
                Response.ID = -1;
               
            }
            return Request.CreateResponse(HttpStatusCode.OK,Response);

        }

        //API for adding ACtivity
        [Route("api/EventModule/AddActivity")]
        [HttpPost]

        public HttpResponseMessage AddActivity(ActivityEntity Activity)
        {
            InsertLog.WriteErrrorLog("api/EventModule/AddActivity=>API call started");
            SerializeResponse<ActivityEntity> Response = new SerializeResponse<ActivityEntity>();
            try
            {
                AddActivityBL addActivityBL = new AddActivityBL();
                Response= addActivityBL.AddActivity(Activity);
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrrorLog("api/EventModule/AddActivity" + ex.Message + ex.StackTrace);
                Response.Message = ex.Message;
                Response.ID = -1;
              
            }
            return Request.CreateResponse(HttpStatusCode.OK, Response);

        }


        //API for showing Event or ACtivity
        [Route("api/EventModule/showEventOrActivity")]
        [HttpPost]

        public HttpResponseMessage showEventOrActivity(EventEntity Activity)
        {
            InsertLog.WriteErrrorLog("api/EventModule/showEventOrActivity=>API call started");
            SerializeResponse<EventEntity> Response = new SerializeResponse<EventEntity>();
            try
            {
               ShowEventActivity showEventActivity = new ShowEventActivity();
                Response= showEventActivity.activityAndEvent(Activity);
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrrorLog("api/EventModule/showEventOrActivity" + ex.Message + ex.StackTrace);
                Response.Message = ex.Message;
                Response.ID = -1;
              
            }
            return Request.CreateResponse(HttpStatusCode.OK, Response);

        }

        //API for publish EVent or Add price to the ACtivity
        [Route("api/EventModule/PublishOrAddPrice")]
        [HttpPost]

        public HttpResponseMessage PublishOrAddPrice(EventEntity Event)
        {
            InsertLog.WriteErrrorLog("api/EventModule/PublishOrAddPrice=>API call started");
            SerializeResponse<EventEntity> Response = new SerializeResponse<EventEntity>();

            try
            {
                PublishOrAddPriceBL publishOrAddPriceBL = new PublishOrAddPriceBL();
                Response= publishOrAddPriceBL.PriceorPublis(Event);
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrrorLog("api/EventModule/PublishOrAddPrice" + ex.Message + ex.StackTrace);
                Response.Message = ex.Message;
                Response.ID = -1;
                
            }
            return Request.CreateResponse(HttpStatusCode.OK,Response);

        }

        //this is for updating the Event
        [Route("api/EventModule/UpdateEvent")]
        [HttpPost]

        public HttpResponseMessage UpdateEvent(EventEntity Event)
        {
            InsertLog.WriteErrrorLog("api/EventModule/UpdateEvent");
            SerializeResponse<EventEntity> Response = new SerializeResponse<EventEntity>();
            try
            {
                UpdateEvent update = new UpdateEvent();
                Response= update.Update(Event);
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrrorLog("api/EventModule/UpdateEvent" + ex.Message + ex.StackTrace);
                Response.Message = ex.Message;
                Response.ID = -1;
               
            }
            return Request.CreateResponse(HttpStatusCode.OK, Response);

        }
    }
}

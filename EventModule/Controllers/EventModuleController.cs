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

        public SerializeResponse<UserEntity> RegisterUser([FromBody] UserEntity user)
        {
            InsertLog.WriteErrrorLog("api/EventModule/RegisterUser=>API call starte");
            SerializeResponse<UserEntity> Response = new SerializeResponse<UserEntity>();
            try
            {
                RegisterUserBL userbl = new RegisterUserBL();
                return userbl.RegisterUSer(user);
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrrorLog("api/EventModule/RegisterUser" + ex.Message + ex.StackTrace);
                Response.Message = ex.Message;
                Response.ID = -1;
                return Response;
            }


        }

        //API for login for admin And User
        [Route("api/EventModule/Login")]
        [HttpPost]

        public SerializeResponse<UserEntity> Login([FromBody] UserEntity user)
        {
            InsertLog.WriteErrrorLog("api/EventModule/Login=>API call starte");
            SerializeResponse<UserEntity> Response = new SerializeResponse<UserEntity>();
            try
            {
                LoginAdimAndUserBL loginuserbl = new LoginAdimAndUserBL();
                return loginuserbl.Login(user);
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrrorLog("api/EventModule/Login" + ex.Message + ex.StackTrace);
                Response.Message = ex.Message;
                Response.ID = -1;
                return Response;
            }

        }
        //API for adding Events
        [Route("api/EventModule/AddEvent")]
        [HttpPost]

        public SerializeResponse<EventEntity> AddEvent( EventEntity Event)
        {
            InsertLog.WriteErrrorLog("api/EventModule/AddEvent=>API call started");
            SerializeResponse<EventEntity> Response = new SerializeResponse<EventEntity>();
            try
            {
                 AddEventBL addEventBL = new AddEventBL();
                return addEventBL.AddEvent(Event);
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrrorLog("api/EventModule/AddEvent" + ex.Message + ex.StackTrace);
                Response.Message = ex.Message;
                Response.ID = -1;
                return Response;
            }

        }

        //API for adding ACtivity
        [Route("api/EventModule/AddActivity")]
        [HttpPost]

        public SerializeResponse<ActivityEntity> AddActivity(ActivityEntity Activity)
        {
            InsertLog.WriteErrrorLog("api/EventModule/AddActivity=>API call started");
            SerializeResponse<ActivityEntity> Response = new SerializeResponse<ActivityEntity>();
            try
            {
                AddActivityBL addActivityBL = new AddActivityBL();
                return addActivityBL.AddActivity(Activity);
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrrorLog("api/EventModule/AddActivity" + ex.Message + ex.StackTrace);
                Response.Message = ex.Message;
                Response.ID = -1;
                return Response;
            }

        }


        //API for showing Event or ACtivity
        [Route("api/EventModule/showEventOrActivity")]
        [HttpPost]

        public SerializeResponse<EventEntity> showEventOrActivity(EventEntity Activity)
        {
            InsertLog.WriteErrrorLog("api/EventModule/showEventOrActivity=>API call started");
            SerializeResponse<EventEntity> Response = new SerializeResponse<EventEntity>();
            try
            {
               ShowEventActivity showEventActivity = new ShowEventActivity();
              return  showEventActivity.activityAndEvent(Activity);
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrrorLog("api/EventModule/showEventOrActivity" + ex.Message + ex.StackTrace);
                Response.Message = ex.Message;
                Response.ID = -1;
                return Response;
            }

        }

        //API for publish EVent or Add price to the ACtivity
        [Route("api/EventModule/PublishOrAddPrice")]
        [HttpPost]

        public SerializeResponse<EventEntity> PublishOrAddPrice(EventEntity Event)
        {
            InsertLog.WriteErrrorLog("api/EventModule/PublishOrAddPrice=>API call started");
            SerializeResponse<EventEntity> Response = new SerializeResponse<EventEntity>();

            try
            {
                PublishOrAddPriceBL publishOrAddPriceBL = new PublishOrAddPriceBL();
                 return publishOrAddPriceBL.PriceorPublis(Event);
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrrorLog("api/EventModule/PublishOrAddPrice" + ex.Message + ex.StackTrace);
                Response.Message = ex.Message;
                Response.ID = -1;
                return Response;
            }

        }

        [Route("api/EventModule/UpdateEvent")]
        [HttpPost]

        public SerializeResponse<EventEntity> UpdateEvent(EventEntity Event)
        {
            InsertLog.WriteErrrorLog("api/EventModule/UpdateEvent");
            SerializeResponse<EventEntity> Response = new SerializeResponse<EventEntity>();
            try
            {
                UpdateEvent update = new UpdateEvent();
               return update.Update(Event);
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrrorLog("api/EventModule/PublishOrAddPrice" + ex.Message + ex.StackTrace);
                Response.Message = ex.Message;
                Response.ID = -1;
                return Response;
            }

        }
    }
}

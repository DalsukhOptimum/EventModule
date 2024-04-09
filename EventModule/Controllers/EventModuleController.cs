using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Library;
using Models;
using BL;


namespace EventModule.Controllers
{
    public class EventModuleController : ApiController
    {

        [Route("api/EventModule/RegisterUser")]
        [HttpPost]

        public SerializeResponse<UserEntity> RegisterUser([FromBody] UserEntity user)
        {
            InsertLog.WriteErrrorLog("api/ExpenseManager/RegisterUser=>API call starte");
            SerializeResponse<UserEntity> Response = new SerializeResponse<UserEntity>();
            try
            {
                RegisterUserBL userbl = new RegisterUserBL();
                return userbl.RegisterUSer(user);
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrrorLog("api/ExpenseManager/RegisterUser=>Exception" + ex.Message + ex.StackTrace);
                Response.Message = ex.Message;
                Response.ID = -1;
                return Response;
            }


        }

        [Route("api/EventModule/Login")]
        [HttpPost]

        public SerializeResponse<UserEntity> Login([FromBody] UserEntity user)
        {
            InsertLog.WriteErrrorLog("api/ExpenseManager/LoginUser=>API call starte");
            SerializeResponse<UserEntity> Response = new SerializeResponse<UserEntity>();
            try
            {
                LoginAdimAndUserBL loginuserbl = new LoginAdimAndUserBL();
                return loginuserbl.Login(user);
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrrorLog("api/ExpenseManager/LoginUser" + ex.Message + ex.StackTrace);
                Response.Message = ex.Message;
                Response.ID = -1;
                return Response;
            }

        }

        [Route("api/EventModule/AddEvent")]
        [HttpPost]

        public SerializeResponse<EventEntity> AddEvent( EventEntity Event)
        {
            InsertLog.WriteErrrorLog("api/ExpenseManager/AddEvent=>API call started");
            SerializeResponse<EventEntity> Response = new SerializeResponse<EventEntity>();
            try
            {
                 AddEventBL addEventBL = new AddEventBL();
                return addEventBL.AddEvent(Event);
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrrorLog("api/ExpenseManager/LoginUser" + ex.Message + ex.StackTrace);
                Response.Message = ex.Message;
                Response.ID = -1;
                return Response;
            }

        }

        [Route("api/EventModule/AddActivity")]
        [HttpPost]

        public SerializeResponse<ActivityEntity> AddActivity(ActivityEntity Activity)
        {
            InsertLog.WriteErrrorLog("api/ExpenseManager/AddEvent=>API call started");
            SerializeResponse<ActivityEntity> Response = new SerializeResponse<ActivityEntity>();
            try
            {
                AddActivityBL addActivityBL = new AddActivityBL();
                return addActivityBL.AddActivity(Activity);
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrrorLog("api/ExpenseManager/LoginUser" + ex.Message + ex.StackTrace);
                Response.Message = ex.Message;
                Response.ID = -1;
                return Response;
            }

        }


        [Route("api/EventModule/showEventOrActivity")]
        [HttpPost]

        public SerializeResponse<EventEntity> showEventOrActivity(EventEntity Activity)
        {
            InsertLog.WriteErrrorLog("api/ExpenseManager/AddEvent=>API call started");
            SerializeResponse<EventEntity> Response = new SerializeResponse<EventEntity>();
            try
            {
               ShowEventActivity showEventActivity = new ShowEventActivity();
              return  showEventActivity.activityAndEvent(Activity);
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrrorLog("api/ExpenseManager/LoginUser" + ex.Message + ex.StackTrace);
                Response.Message = ex.Message;
                Response.ID = -1;
                return Response;
            }

        }

        [Route("api/EventModule/PublishOrAddPrice")]
        [HttpPost]

        public SerializeResponse<EventEntity> PublishOrAddPrice(EventEntity Event)
        {
            InsertLog.WriteErrrorLog("api/ExpenseManager/PublishOrAddPrice=>API call started");
            SerializeResponse<EventEntity> Response = new SerializeResponse<EventEntity>();
            try
            {
                PublishOrAddPriceBL publishOrAddPriceBL = new PublishOrAddPriceBL();
                 return publishOrAddPriceBL.PriceorPublis(Event);
            }
            catch (Exception ex)
            {
                InsertLog.WriteErrrorLog("api/ExpenseManager/PublishOrAddPrice" + ex.Message + ex.StackTrace);
                Response.Message = ex.Message;
                Response.ID = -1;
                return Response;
            }

        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OpenempApiNotifications.Models;
using OpenempApiNotifications.Models.ResponseDTO;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace OpenempApiNotification.Controllers //OpenempApiNotification
{
    [Route("api/v1/Notification")]
    [ApiController] //1-Attribiute to verify the comming data for validation, and send it to actions. // 2-automatically identify the request emplacement (if it's from URL,header,body ..) 
    public class NotificationController : ControllerBase
    {
        private readonly IMapper Mapper;

        public NotificationDbContext DbContext { get; }

        public NotificationController(IMapper _Mapper, NotificationDbContext _DbContext)
        {
            this.Mapper = _Mapper;
            this.DbContext = _DbContext;
        }

        #region HttpGet
        [HttpGet]  //Attribiute
        public IActionResult GetAllNotifications([FromQuery] string NotificationReceiver, [FromQuery] string NotificationSender) //IActionResult : Interface => status
        {//[FromQuery] string SearchingString, string orderby, [FromQuery] PagingDTO paging
            var QueryNotification = DbContext.Notification.AsQueryable();
            QueryNotification = QueryNotification.Where(x => x.IsDeleted == false);
            if (!string.IsNullOrEmpty(NotificationReceiver))
            {
                QueryNotification = QueryNotification.Where(x => x.Reciever == NotificationReceiver);
            }
            if (!string.IsNullOrEmpty(NotificationSender))
            {
                QueryNotification = QueryNotification.Where(x => x.Sender == NotificationSender);
            }

            return this.Ok(QueryNotification.Select(s => Mapper.Map<NotificationResponseDTO>(s)));


            //var notifications = DbContext.Notification.Where(e => e.IsDeleted == false).FirstOrDefault();
            //Query = Query.Where(x => x.IsDeleted == false); // to get all notifications which are not deleted
            //return this.Ok(notifications);
        }

        [HttpGet("{Id}")]  //Attribiute
        public IActionResult GetNotificationById(Guid Id)
        {
            var CurrectAuthor = DbContext.Notification.Where(e => e.NotificationId.Equals(Id)).FirstOrDefault();
            if (CurrectAuthor == null)
            {
                return this.NotFound(new { ErrorCode = 404, Message = "Notification not found" });
            }
            return this.Ok(CurrectAuthor);
        }

        #endregion

        #region HttpPost
        [HttpPost]
        public IActionResult AddNotification(Notification NotificationToAdd)
        {
            if (string.IsNullOrWhiteSpace(NotificationToAdd.NotificationMessage))
            {
                return this.BadRequest(new { ErrorCode = 501, Message = "Invalid empty Notification" });
            }
            var CurrentAuthor = DbContext.Notification.Where(e => e.NotificationId == NotificationToAdd.NotificationId).SingleOrDefault();
            if (CurrentAuthor != null)
            {
                return this.Conflict(new { ErrorCode = 502, Message = "Author Id duplicated" });
            }
            DbContext.Notification.Add(NotificationToAdd);
            return this.CreatedAtAction(nameof(this.GetNotificationById), new { Id = NotificationToAdd.NotificationId }, NotificationToAdd);
        }
        #endregion

        #region HttpPut
        [HttpPut("{Id}")]
        public IActionResult UpdateNotification(Guid Id)
        {
            var CurrentNotification = DbContext.Notification.Where(x => x.NotificationId == Id).FirstOrDefault();
            if (CurrentNotification == null)
            {
                return this.NotFound(new { ErrorCode = 404, Message = "Notification not found" });
            }
            CurrentNotification.IsRead = true;
            DbContext.SaveChanges();
            return this.Ok(CurrentNotification);
        }
        #endregion


        #region HttpDelete
        [HttpDelete]
        public IActionResult DeleteNotification(Guid IdNotification)
        {
            var CurrentNotification = DbContext.Notification.Where(e => e.NotificationId == IdNotification).SingleOrDefault();
            if (CurrentNotification == null)
            {
                return this.NotFound(new { ErrorCode = 404, Message = "Author not found" });
            }
            CurrentNotification.IsDeleted = true;
            return this.NoContent();
        }
        #endregion²
    }
}
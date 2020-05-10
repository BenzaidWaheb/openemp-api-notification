using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OpenempApiNotification.Models.RequestDTO;
using OpenempApiNotifications.Models;
using OpenempApiNotifications.Models.RequestDTO;
using OpenempApiNotifications.Models.ResponseDTO;
using System;
using System.Linq;

namespace OpenempApiNotification.Controllers
{
    [Route("api/v1/Notification")]
    [ApiController]
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
        public IActionResult GetAllNotifications(string NotificationReceiver, string NotificationSender, [FromQuery]GroupingNotificationDTO GroupingDTO) //IActionResult : Interface => status
        {
            try
            {
                var QueryNotification = this.DbContext.Notification.AsQueryable();
                QueryNotification = QueryNotification.Where(x => x.IsDeleted == false);

                if (!string.IsNullOrEmpty(NotificationReceiver))
                {
                    QueryNotification = QueryNotification.Where(x => x.Reciever == NotificationReceiver);
                }
                if (!string.IsNullOrEmpty(NotificationSender))
                {
                    QueryNotification = QueryNotification.Where(x => x.Sender == NotificationSender);
                }

                QueryNotification = QueryNotification.OrderBy(x => x.CreatedOn);
                //QueryNotification = QueryNotification.Skip((GroupingDTO.GroupNumber - 1) * GroupingDTO.RowCounts)
                //                                     .Take(GroupingDTO.RowCounts);

                return this.Ok(QueryNotification.Select(s => Mapper.Map<NotificationResponseDTO>(s)));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("{Id}")]  //Attribiute
        public IActionResult GetNotificationById(Guid Id)
        {
            Notification CurrentNotification = this.DbContext.Notification.Where(e => e.NotificationId.Equals(Id)).FirstOrDefault();
            if (CurrentNotification == null)
            {
                return this.NotFound(new { ErrorCode = 404, Message = "Notification not found" });
            }
            return this.Ok(Mapper.Map<NotificationResponseDTO>(CurrentNotification));
        }

        #endregion

        #region HttpPost
        [HttpPost]
        public IActionResult AddNotification(AddRequestNotificationDTO NotificationToAdd)
        {
            if (string.IsNullOrWhiteSpace(NotificationToAdd.NotificationMessage))
            {
                return this.BadRequest(new { ErrorCode = 501, Message = "Invalid empty Notification" });
            }
            if (this.DbContext.Notification.Any(x => x.NotificationMessage == NotificationToAdd.NotificationMessage))
            {
                this.ModelState.AddModelError("NotificationMessage", "Duplicate message");
                return this.ValidationProblem();
            }
            var CurrentNotification = Mapper.Map<Notification>(NotificationToAdd);
            this.DbContext.Notification.Add(CurrentNotification);
            this.DbContext.SaveChanges();
            return this.CreatedAtAction(nameof(this.GetNotificationById), new { Id = CurrentNotification.NotificationId }, NotificationToAdd);
        }
        #endregion

        #region HttpPut
        [HttpPut("{Id}")]
        public IActionResult UpdateNotification(Guid Id, UpdateRequestNotificationDTO NotificationToUpdate)
        {
            var CurrentNotification = this.DbContext.Notification.Where(x => x.NotificationId == Id).FirstOrDefault();
            if (CurrentNotification == null)
            {
                return this.NotFound(new { ErrorCode = 404, Message = "Notification not found" });
            }
            Mapper.Map(NotificationToUpdate, CurrentNotification);
            this.DbContext.SaveChanges();
            return this.Ok(CurrentNotification);
        }
        #endregion

        #region HttpDelete
        [HttpDelete]
        public IActionResult DeleteNotification(Guid IdNotification)
        {
            var CurrentNotification = this.DbContext.Notification.Where(e => e.NotificationId == IdNotification).SingleOrDefault();
            if (CurrentNotification == null)
            {
                return this.NotFound(new { ErrorCode = 404, Message = "Author not found" });
            }
            CurrentNotification.IsDeleted = true;
            this.DbContext.SaveChanges();
            return this.NoContent();
        }
        #endregion
    }
}
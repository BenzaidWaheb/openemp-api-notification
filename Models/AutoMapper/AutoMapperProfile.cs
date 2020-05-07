using AutoMapper;
using OpenempApiNotifications.Models.RequestDTO;
using OpenempApiNotifications.Models.ResponseDTO;
using System.Linq;

namespace OpenempApiNotifications.Models
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            this.CreateMap<AddRequestNotificationDTO, Notification>()
                .ForMember(x => x.IsDeleted, x => x.MapFrom(x => false));

            this.CreateMap<UpdateRequestNotificationDTO, Notification>();

            this.CreateMap<Notification, NotificationResponseDTO>()
                .ForMember(x => x.NotificationCount, x => x.MapFrom(x => x.NotificationMessage.Count()));
        }
    }
}

using AutoMapper;
using LectureSchedule.Service.DTO;
using LectureSchedule.Domain;

namespace LectureSchedule.Service.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Lecture, LectureDTO>().ReverseMap();
            CreateMap<PublicityCampaign, PublicityCampaignDTO>().ReverseMap();
            CreateMap<Speaker, SpeakerDTO>().ReverseMap();
            CreateMap<TicketLot, TicketLotDTO>().ReverseMap();
        }
    }
}

﻿using AutoMapper;
using LectureSchedule.Service.DTO;
using LectureSchedule.Domain;
using LectureSchedule.Domain.Identity;
using LectureSchedule.Data.Pagination;

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
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserLoginDTO>().ReverseMap();
            CreateMap<User, UpdateUserDTO>().ReverseMap();
            CreateMap<UserDTO, UpdateUserDTO>().ReverseMap();
        }
    }
}

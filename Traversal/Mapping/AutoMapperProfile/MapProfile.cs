﻿using AutoMapper;
using DTOLayer.DTOs.AnnouncementDTOs;
using DTOLayer.DTOs.AppUserDTOs;
using DTOLayer.DTOs.ContactDTOs;
using EntityLayer.Concrete;

namespace Traversal.Mapping.AutoMapperProfile
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<AnnouncementAddDTO, Announcement>().ReverseMap();
            CreateMap<AnnouncementListDTO, Announcement>().ReverseMap();
            CreateMap<AppUserRegisterDTO, AppUser>().ReverseMap();
            CreateMap<AppUserLoginDTO, AppUser>().ReverseMap();
            CreateMap<SendMessageDTO, ContactUs>().ReverseMap();
        }
    }
}

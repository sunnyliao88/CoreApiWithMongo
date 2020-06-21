using AutoMapper;
using CoreApiWithMongo.Models;
using CoreApiWithMongo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApiWithMongo.Extensions;

namespace CoreApiWithMongo.AutoMap
{
    public class AppMapProfile : Profile
    {
        public AppMapProfile()
        {
            CreateMap<Employee, EmployeeCreateVM>();
            // .ForMember(dest => dest.UploadPhoto, src => src.Ignore())
            //  .ForMember(dest => dest.UploadResume, src => src.Ignore());

            CreateMap<EmployeeCreateVM, Employee>()
                .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => src.UploadPhoto.ConvertToBase64String()))
                .ForMember(dest => dest.Resume, opt => opt.MapFrom(src => src.UploadResume.ConvertToBase64String()));

            CreateMap<Employee, EmployeeEditVM>();
            CreateMap<EmployeeEditVM, Employee>()
                .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => src.UploadPhoto.ConvertToBase64String()))
                .ForMember(dest => dest.Resume, opt => opt.MapFrom(src => src.UploadResume.ConvertToBase64String()));

        }
    }
}

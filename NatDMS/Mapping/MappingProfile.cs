using AutoMapper;
using NatDMS.Models;
using Natural.Core.Models;

namespace NatDMS.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to Resource

            CreateMap<LoginModel, LoginViewModel>();
            CreateMap<StateModel, StateViewModel>();
            CreateMap<CityModel, CityViewModel>();
            CreateMap<AreaModel, AreaViewModel>();

            CreateMap<LoginModel , LoginResultModel>();
            CreateMap<DistributorModel, DistributorViewModel>();
            CreateMap<CategoryModel, CategoryViewModel>();
            CreateMap<RetailorModel, RetailorViewModel>();
            CreateMap<StateModel, StateViewModel>();
            CreateMap<CityModel, CityViewModel>();
            CreateMap<AreaModel, AreaViewModel>();
            CreateMap<ExecutiveModel, ExecutiveViewModel>();
            CreateMap<RetailorModel, SaveRetailorViewModel>();
            CreateMap<ExecutiveModel, ExecutiveViewModel>();
            CreateMap<ExecutiveModel, SaveExecutiveViewModel>();




            // Resource to Domain

            CreateMap<LoginResultModel, LoginModel>();
            CreateMap<LoginViewModel, LoginModel>();
            CreateMap<DistributorViewModel,DistributorModel>();
            CreateMap<CategoryViewModel, CategoryModel>();
            CreateMap<RetailorViewModel, RetailorModel>();
            CreateMap<EditViewModel, DistributorModel>();
            CreateMap<EditViewModel, ExecutiveModel>();
            CreateMap<SaveRetailorViewModel,RetailorModel>();
            CreateMap<ExecutiveViewModel, ExecutiveModel>();
            CreateMap<SaveExecutiveViewModel, ExecutiveModel>();


        }
    }
}

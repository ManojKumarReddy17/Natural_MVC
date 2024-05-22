using System.Collections.Generic;
using AutoMapper;
using Microsoft.CodeAnalysis.Differencing;
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
            CreateMap<ED_CreateModel, ExecutiveViewModel>();
            CreateMap<ExecutiveModel, ED_CreateViewModel>();
            CreateMap<DistributorModel , ED_CreateViewModel>();
            CreateMap<StateModel, ED_CreateViewModel>();
            CreateMap<DistributorModel, ED_EditViewModel>();
            CreateMap<ExecutiveModel, ED_EditViewModel>();
            CreateMap<RetailorModel , RetailorEditViewModel>();
            CreateMap<ExecutiveModel, EDR_DisplayViewModel>();
            CreateMap<DistributorModel,EDR_DisplayViewModel>();
            CreateMap<RetailorModel, EDR_DisplayViewModel>();
            CreateMap<SearchModel,EDR_DisplayViewModel>();
            CreateMap<DistributorToExecutive, AssignDistributorToExecutiveViewModel>();
            CreateMap<SearchModel , SearchViewModel>();
            CreateMap<ProductResponse,ProductResult>();
            CreateMap<RetailorToDistributor,AssignRetailorToDistributorViewModel>();
            CreateMap<DistributorModel, DistributorDetailsViewModel>();
            CreateMap<DSRViewModel, Dsrview>();
            CreateMap<DsrInsertProduct, DsrProduct>();
            CreateMap<DsrInsert, Dsrcreate>();
            CreateMap<DsrRetailor, DsrRetailorDrop>();
            CreateMap< DsrDistributor,DsrDistributorDrop>();
            CreateMap<Dsredit, Dsrcreate>();
            CreateMap<DistributorSalesReport, DistributorSalesReportViewModel>();
            CreateMap<DsrDistributor, DsrDistributorDrop>();
            CreateMap<DsrRetailor, DsrRetailorDrop>();
            CreateMap<DistributorNotification, DistributorNotificationViewModel>();
            CreateMap<Notification, NotificationViewmodel>();
            CreateMap<ED_CreateModel, ED_CreateViewModel>();
            CreateMap<ED_CreateModel, ED_EditViewModel>();

            CreateMap<AreaModel, AreaDisplayModel>();
            CreateMap<AreaModel,AreaCUmodel>();
            // Resource to Domain

            CreateMap<LoginResultModel, LoginModel>();
            CreateMap<LoginViewModel, LoginModel>();
            CreateMap<DistributorViewModel,DistributorModel>();
            CreateMap<CategoryViewModel, CategoryModel>();
            CreateMap<RetailorViewModel, RetailorModel>();
            CreateMap<ED_EditViewModel, DistributorModel>();
            CreateMap<ED_EditViewModel, ExecutiveModel>().ForMember(dest => dest.Area, opt => opt.Ignore());
            CreateMap<SaveRetailorViewModel,RetailorModel>();
            CreateMap<ExecutiveViewModel, ExecutiveModel>();
            CreateMap<ExecutiveViewModel, ED_CreateModel>();
            CreateMap<ED_CreateViewModel, ExecutiveModel>().ForMember(dest => dest.Area, opt => opt.Ignore());

            CreateMap<ED_CreateViewModel,DistributorModel>();
            CreateMap<RetailorEditViewModel, RetailorModel>();
            CreateMap<EDR_DisplayViewModel, ExecutiveModel>();
            CreateMap<EDR_DisplayViewModel, DistributorModel>();
            CreateMap<EDR_DisplayViewModel, RetailorModel>();
            CreateMap<EDR_DisplayViewModel, SearchModel>();
            CreateMap<AssignDistributorToExecutiveViewModel, DistributorToExecutive>();
            CreateMap<SearchViewModel,SearchModel>();
            CreateMap<AssignRetailorToDistributorViewModel, RetailorToDistributor>();
            CreateMap<DSRViewModel,DSRModel>();
            CreateMap<Product, ProductModel>();
            CreateMap<GetProduct,EditProduct>();
            CreateMap<EditProduct,ProductModel>();
            CreateMap<SearchProduct,ProductSearch>();
            CreateMap<DistributorDetailsViewModel,DistributorModel>();
            CreateMap<Dsrcreate, DsrInsert>();
            CreateMap<DsrExecutiveDrop, DsrExecutiveResourse>();
            CreateMap<DSRModel, DsrResourse>();
            CreateMap<DSRModel, DsrResourse>();
            CreateMap<DistributorSalesReportViewModel, DistributorSalesReport>();
            CreateMap<NotificationViewmodel, Notification>();
            CreateMap<NotificationGetViewModel, Dsrview>();
            CreateMap<GetExecutive, ExecutiveModel>();
            CreateMap<GetExecutive, ExecutiveViewModel>();

            CreateMap<CityViewModel, CityModel>();



            CreateMap< AreaDisplayModel, AreaModel>();
            CreateMap<AreaModel, AreaDisplayModel>();
            CreateMap<AreaCUmodel, AreaModel>();
          

            CreateMap<GetExecutive, ExecutiveModel>();
            CreateMap<GetExecutive, ExecutiveViewModel>();
            CreateMap<GetExecutive, DistributorModel>();
            CreateMap<DsrDistributorDrop, DsrDistributorResouse>();




        }
    }
}

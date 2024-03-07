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
            CreateMap<ExecutiveModel, ExecutiveViewModel>();
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
            //CreateMap<DSRModel,DSRViewModel>();
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


            // Resource to Domain

            CreateMap<LoginResultModel, LoginModel>();
            CreateMap<LoginViewModel, LoginModel>();
            CreateMap<DistributorViewModel,DistributorModel>();
            CreateMap<CategoryViewModel, CategoryModel>();
            CreateMap<RetailorViewModel, RetailorModel>();
            CreateMap<ED_EditViewModel, DistributorModel>();
            CreateMap<ED_EditViewModel, ExecutiveModel>();
            CreateMap<SaveRetailorViewModel,RetailorModel>();
            CreateMap<ExecutiveViewModel, ExecutiveModel>();
            CreateMap<ED_CreateViewModel, ExecutiveModel>();
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

            CreateMap<DistributorSalesReportViewModel, DistributorSalesReport>();


        }
    }
}

using AutoMapper;
using Bade.Admin.Model.Model.Output;
using Bade.Entity.Domain;

namespace Bade.Admin.Model.Mapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get
            {
                return "DomainToViewModelMappingProfile";
            }
        }


        protected override void Configure()
        {
            AutoMapper.Mapper.CreateMap<ApplicationConfig, ApplicationConfigViewModel>();
            //AutoMapper.Mapper.CreateMap<IEnumerable<ApplicationConfig>, IEnumerable<ApplicationConfigViewModel>>();
            //AutoMapper.Mapper.CreateMap<Location, LocationViewModel>();
            //AutoMapper.Mapper.CreateMap<ResourceActivity,ResourceActivityViewModel>()
            //    .ForMember(vm => vm.ActivityDateString, dm=> dm.MapFrom(dModel => dModel.ActivityDate.ToLongDateString()));
            //AutoMapper.Mapper.CreateMap<ApplicationUser, RegisterViewModel>();
        }
    }
}
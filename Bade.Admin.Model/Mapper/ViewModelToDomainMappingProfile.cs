using AutoMapper;
using Bade.Admin.Model.Model.Output;
using Bade.Entity.Domain;

namespace Bade.Admin.Model.Mapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get
            {
                return "ViewModelToDomainMappingProfile";
            }
        }

        protected override void Configure()
        {
            AutoMapper.Mapper.CreateMap<ApplicationConfigViewModel, ApplicationConfig>()
                .ForMember(resource => resource.Application, vm => vm.Ignore());
            //AutoMapper.Mapper.CreateMap<LocationViewModel, Location>();
            //AutoMapper.Mapper.CreateMap<ResourceActivityViewModel, ResourceActivity>();
            //AutoMapper.Mapper.CreateMap<RegisterViewModel, ApplicationUser>();
            //AutoMapper.Mapper.CreateMap<RegisterViewModel, ApplicationUser>().ForMember(user => user.UserName, vm => vm.MapFrom(rm => rm.Email));
        }
    }
}
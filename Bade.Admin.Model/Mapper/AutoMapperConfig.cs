#region



#endregion

namespace Bade.Admin.Model.Mapper
{
    public class AutoMapperConfiguration
    {
        public static void LoadConfig()
        {
            AutoMapper.Mapper.Initialize(mapper =>
            {
                mapper.AddProfile<ViewModelToDomainMappingProfile>();
                mapper.AddProfile<DomainToViewModelMappingProfile>();
            });
        }
    }
}
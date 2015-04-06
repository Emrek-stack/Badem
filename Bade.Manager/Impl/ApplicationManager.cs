#region

using System.Collections.Generic;
using AutoMapper;
using Bade.Admin.Model.Model.Output;
using Bade.Data.Contract;
using Bade.Entity.Domain;
using Bade.Infrastructure.Attribute;
using Bade.Manager.Interface;

#endregion

namespace Bade.Manager.Impl
{
    public class ApplicationManager : Manager, IApplicationManager
    {
        private readonly IApplicationRepository _applicationRepository;


        public ApplicationManager(IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }



        [Cache(CacheMinute = 10)]
        public ApplicationConfig GetKey(int applicationId, string key)
        {
            //return _applicationSettingRepository.Get(config => config.ApplicationId == 1 && config.Key == "a");
            return null;
        }

        public ApplicationConfigOutput ApplicationConfigListById(int id)
        {

            ApplicationConfigOutput applicationConfigOutput = new ApplicationConfigOutput();
            IEnumerable<ApplicationConfig> result = _applicationRepository.ApplicationConfigListById(id);
            applicationConfigOutput.ApplicationConfigList =
                Mapper.Map<IEnumerable<ApplicationConfig>, IEnumerable<ApplicationConfigViewModel>>(result);
            return applicationConfigOutput;
        }
    }
}
using Bade.Data.Contract;
using Bade.Data.Dapper;
using Bade.Infrastructure;
using Bade.Manager.Interface;

namespace Bade.Manager.Impl
{
    public abstract class Manager : IManager
    {
        private readonly IRepository _repository;

        //protected Manager(IRepository<T> repository)
        //{
        //    _repository = repository;
        //}

        #region Fields
        private bool _hasErrors;
        private string _errorMessage = string.Empty;
        protected bool IsInnerBusiness = false;
        #endregion

        //public virtual long Add(T entity)
        //{
        //    try
        //    {
        //        return _repository.Add(entity);
        //    }
        //    catch (DataAccessLayerException dce)
        //    {
        //        ThrowIfNecessary(dce, _repository);
        //    }
        //    return -1;
        //}

        //public virtual bool Update(T entity)
        //{
        //    try
        //    {
        //        return _repository.Update(entity);
        //    }
        //    catch (DataAccessLayerException dce)
        //    {
        //        ThrowIfNecessary(dce, _repository);
        //    }
        //    return false;
        //}

        //public virtual object GetById(object id)
        //{
        //    try
        //    {
        //        return _repository.GetById(id);
        //    }
        //    catch (DataAccessLayerException dce)
        //    {
        //        ThrowIfNecessary(dce, _repository);
        //    }
        //    return null;
        //}

        //public virtual bool Remove(T entity)
        //{
        //    try
        //    {
        //        return _repository.Remove(entity);
        //    }
        //    catch (DataAccessLayerException dce)
        //    {
        //        ThrowIfNecessary(dce, _repository);
        //    }
        //    return false;
        //}

        #region Exception Handling
        public bool HasErrors
        {
            get { return _hasErrors; }
        }
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                _hasErrors = (value.Length != 0);
            }
        }

        protected void ThrowIfNecessary(ServiceRuleException bre, IRepository rollerBase)
        {
            if (IsInnerBusiness) throw bre;
            //rollerBase.Rollback();
            ErrorMessage = bre.Message;
        }

        protected void ThrowIfNecessary(DataAccessLayerException be, IRepository rollerBase)
        {
            if (IsInnerBusiness) throw be;
            //rollerBase.Rollback();
            ErrorMessage = be.Message;
        }
        #endregion
    }
}
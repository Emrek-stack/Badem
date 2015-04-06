#region

using Bade.Admin.Model.Model;
using Bade.Entity.Domain;

#endregion



namespace Bade.Manager.Interface
{
    public interface IMemberManager : IManager
    {
        MemberResponse Add(MemberRequest memberRequest);
        
        MemberResponse Update(MemberRequest memberRequest);

        bool IsEmailAvaliable(string email);

        bool IsUsernameAvaliable(string username);
    }
}
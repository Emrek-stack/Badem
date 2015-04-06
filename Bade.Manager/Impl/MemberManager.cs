#region

using AutoMapper;
using Bade.Admin.Model.Model;
using Bade.Data.Contract;
using Bade.Entity.Domain;
using Bade.Manager.Interface;

#endregion

namespace Bade.Manager.Impl
{
    public class MemberManager : Manager, IMemberManager
    {
        private readonly IMemberRepository _memberRepository;

        public MemberManager(IMemberRepository memberRepository)
            
        {
            _memberRepository = memberRepository;
        }

        public MemberResponse Add(MemberRequest memberRequest)
        {
            Member member = Mapper.Map<Member>(memberRequest);
            _memberRepository.Add(member);
            return Mapper.Map<MemberResponse>(member);
        }

        public MemberResponse Update(MemberRequest memberRequest)
        {
            //Member member = Mapper.Map<Member>(memberRequest);
            //_memberRepository.Update(member);
            //return Mapper.Map<MemberResponse>(member);
            return null;
        }

        public bool IsEmailAvaliable(string email)
        {
            //return _memberRepository.Get(m => m.Email == email) != null;
            return false;
        }

        public bool IsUsernameAvaliable(string username)
        {
            //return _memberRepository.Get(m => m.Username == username) != null;
            return false;
        }
    }
}
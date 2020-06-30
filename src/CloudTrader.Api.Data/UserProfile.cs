using AutoMapper;
using CloudTrader.Api.Service.Models;

namespace CloudTrader.Api.Data
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDbModel>()
                .ReverseMap();
        }
    }
}

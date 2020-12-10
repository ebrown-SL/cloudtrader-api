using AutoMapper;
using CloudTrader.Users.Domain.Models;

namespace CloudTrader.Users.Data
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
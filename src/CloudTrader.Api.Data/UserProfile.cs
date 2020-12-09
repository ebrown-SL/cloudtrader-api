using AutoMapper;
using CloudTrader.Api.Domain.Models;

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
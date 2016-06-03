using AutoMapper;
using Adwaer.Identity.Entitites;
using Adwaer.Identity.ViewModel;

namespace Adwaer.Identity
{
    public class IdentityMapper
    {
        public static IMapper Register()
        {
            var config = new MapperConfiguration(configuration =>
            {
                configuration
                    .CreateMap<RegistrationModel, SimpleCustomerAccount>()
                    .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => $"{src.Name} {src.LastName}"))
                    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                    .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone));
                configuration.CreateProfile("identity");
            });

            return config.CreateMapper();
        }
    }
}

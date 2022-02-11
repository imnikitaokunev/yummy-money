using Application.Models.Identity;
using Application.Models.User;
using Infrastructure.Identity;
using Mapster;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure
{
    public class MappingProfile
    {
        public static void ApplyMappings()
        {
            // ApplicationUser

            TypeAdapterConfig<ApplicationUser, UserDto>
                .NewConfig()
                .Map(dst => dst.Username, src => src.UserName);

            TypeAdapterConfig<SignUpRequest, ApplicationUser>
                .NewConfig()
                .Map(dst => dst.UserName, src => src.Username);

            TypeAdapterConfig<IdentityResult, AuthenticateResponse>
                .NewConfig();
        }
    }
}

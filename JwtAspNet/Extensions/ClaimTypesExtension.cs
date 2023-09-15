using System.Security.Claims;

namespace JwtAspNet.Extensions
{
    public static class ClaimTypesExtension
    {
        public static int Id(this ClaimsPrincipal user)
        {
            try
            {
                var id = user.Claims.FirstOrDefault(x => x.Type == "id")?.Value ?? "0";
                return int.Parse(id);
            } catch 
            {
                return 0;
            }
        }

        public static string Name(this ClaimsPrincipal user)
        {
            try
            {
                var name = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value ?? "";
                return name;
            }
            catch
            {
                return "";
            }
        }

        public static string Email(this ClaimsPrincipal user)
        {
            try
            {
                var email = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value ?? "";
                return email;
            }
            catch
            {
                return "";
            }
        }

        public static string GivenName(this ClaimsPrincipal user)
        {
            try
            {
                var givenname = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value ?? "";
                return givenname;
            }
            catch
            {
                return "";
            }
        }

        public static string Image(this ClaimsPrincipal user)
        {
            try
            {
                var image = user.Claims.FirstOrDefault(x => x.Type == "image")?.Value ?? "";
                return image;
            }
            catch
            {
                return "";
            }
        }
    }
}

namespace JwtAspNet.Models
{
    public record User(int Id, string Email, string Name, string Image, string Password, string[] Roles)
    {

    }
}

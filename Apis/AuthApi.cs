public class AuthApi : IApi
{
    public void Register(WebApplication app)
    {
        app.MapGet("/login", [AllowAnonymous] async (HttpContext context,
            ITokenService tokenService, IUserRepository UserRepository) => {
            UserModel userModel = new()
            {
                UserName = context.Request.Query["username"],
                Password = context.Request.Query["password"]
            };
            var userDto = UserRepository.GetUser(userModel);
            if (userDto == null) return Results.Unauthorized();
            var token = tokenService.BuildToken(app.Configuration["Jwt:Key"],
                app.Configuration["Jwt:Issuer"], userDto);
            return Results.Ok(token);
        });

    }
}
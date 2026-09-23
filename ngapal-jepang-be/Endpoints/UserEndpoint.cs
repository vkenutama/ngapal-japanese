using ngapal_jepang_be.Data;
using ngapal_jepang_be.Dtos.User;
using System.Runtime.CompilerServices;

namespace ngapal_jepang_be.Endpoints
{
    public static class UserEndpoint
    {
        private static readonly string tags = "User";

        public static void MapUserEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/user").WithTags(tags);

            // Get User by Id
            group.MapGet("/id/{userId:guid}", async (Guid? userId, AppDbContext db) =>
            {
                var existingUser = db.Users.Find(userId);

                return existingUser == null ? Results.NotFound("User with this id not found") : Results.Ok(new GetUserDto(existingUser.Name, existingUser.Username, existingUser.PictureUrl, existingUser.CreatedAt));
            });



        }
    }
}

namespace ngapal_jepang_be.Dtos.User
{
    public record GetUserDto(
        string Name,
        string Username,
        string? pictureUrl,
        DateTime createdAt
    );
}

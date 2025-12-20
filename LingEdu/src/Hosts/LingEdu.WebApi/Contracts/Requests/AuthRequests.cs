namespace LingEdu.WebApi.Contracts.Requests
{
    public sealed record LoginRequest(string Email, string Password);

    public sealed record RegisterRequest(string Email, string UserName, string Password, int Language);
}

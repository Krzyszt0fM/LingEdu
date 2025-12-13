namespace LingEdu.Contracts.Users;

public record UserContractDto(Guid Id, string Email, string Login, string Language, bool IsPremium);

using System.Collections.Generic;
using System.Security.Claims;

namespace EHR_Application.Authentication;

public interface ICurrentUserService
{
    string? UserId { get; }
    string? UserName { get; }
    bool IsAuthenticated { get; }
    IEnumerable<Claim> Claims { get; }
}

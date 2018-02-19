using System.Collections.Generic;
using System.Security.Claims;

namespace EventSystem.Domain.Services
{
	public interface IJwtService
	{
		string GenerateToken(IEnumerable<Claim> claims);
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Core
{
    public interface IUser
    {
        string Name { get; }
        string GetUserId();
        string GetUserEmail();
        bool IsAuthenticated();
        string GetLocalIpAddress();
        string GetRemoteIpAddress();
        bool IsInRole(string role);
        IEnumerable<Claim> GetClaimsIdentity();
    }
}

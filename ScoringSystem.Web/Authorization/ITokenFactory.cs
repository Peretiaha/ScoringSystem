using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ScoringSystem.Web.Authorization
{
    public interface ITokenFactory
    {
        string Create(IList<Claim> claims);
    }
}

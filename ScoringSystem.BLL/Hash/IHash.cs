using System;
using System.Collections.Generic;
using System.Text;

namespace ScoringSystem.BLL.Hash
{
    public interface IHash
    {
        bool VerifyHashPassword(string savedPasswordHash, string password);

        string HashPassword(string password);
    }
}

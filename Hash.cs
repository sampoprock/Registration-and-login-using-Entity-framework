using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Registration
{
    public static class Hash
    {
        public static string HashPass(string value)
        {
            return Convert.ToBase64String(
                System.Security.Cryptography.SHA256.Create()
                .ComputeHash(Encoding.UTF8.GetBytes(value)));
        }
    }
}
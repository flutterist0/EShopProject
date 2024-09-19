﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helpers.Security.Hashing
{
    public static class HashingHelper
    {
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
           using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        public static bool VerifiedPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
           using var hmac = new HMACSHA512(passwordSalt);
          var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                //sfg5rte //sgfh65
                if ( computedHash[i] != passwordHash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}

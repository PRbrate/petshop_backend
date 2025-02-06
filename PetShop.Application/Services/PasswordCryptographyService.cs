using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Application.Services
{
    public static class PasswordCryptographyService
    {
        
            private const int SaltSize = 16; 
            private const int HashSize = 20; 
            private const int Iterations = 10000; 


            public static string Cryptography(string password)
            {
                byte[] salt = new byte[SaltSize];
                RandomNumberGenerator.Fill(salt);

                using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
                {
                    byte[] hash = pbkdf2.GetBytes(HashSize);

                // Combines the salt and hash in an unique array to store

                byte[] hashBytes = new byte[SaltSize + HashSize];
                    Array.Copy(salt, 0, hashBytes, 0, SaltSize);
                    Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

                // Returns the hash in Base64 format

                return Convert.ToBase64String(hashBytes);

                }
            }

            public static bool VerifyPassword(string password, string passwordHash)
            {
                // Converts the stored hash from Base64 to bytes
                byte[] hashBytes = Convert.FromBase64String(passwordHash);

                // Extracts the salt of the bytes from the hash
                byte[] salt = new byte[SaltSize];
                Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            // Generates the hash of the given password usign the extracted hash

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
                {
                    byte[] hash = pbkdf2.GetBytes(HashSize);

                // Compares the hash of the given password with the stored hash

                for (int i = 0; i < HashSize; i++)
                    {
                        if (hashBytes[i + SaltSize] != hash[i])
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
        }
    }



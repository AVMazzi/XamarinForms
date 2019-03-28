using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Mesa_RPG.Models
{
    public class CryptoSenha
    {
        public static string Encrypt(string _senha)
        {
            if (!string.IsNullOrEmpty(_senha))
            {
                StringBuilder senha = new StringBuilder();

                MD5 md5 = MD5.Create();
                byte[] entrada = Encoding.ASCII.GetBytes(_senha);
                byte[] hash = md5.ComputeHash(entrada);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    senha.Append(hash[i].ToString("X2"));
                }
                return senha.ToString();
            }
            else
            {
                return _senha;
            }

        }
    }
}

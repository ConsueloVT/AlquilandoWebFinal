using System;
using AL.Aplicacion.Interfaces;
using System.Security.Cryptography;
using System.Text;
namespace AL.Aplicacion.Servicios;

public class HashService:IHashService
{
        // Método para crear un hash y una sal a partir de una contraseña
        public (string Hash, string Salt) CreateHash(string password)
        {
            // Generar una sal aleatoria de 16 bytes
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Combinar la contraseña con la sal
            var combined = Combine(Encoding.UTF8.GetBytes(password), salt);

            // Calcular el hash SHA-256 de la combinación de contraseña y sal
            using (var sha256 = SHA256.Create())
            {
                byte[] hash = sha256.ComputeHash(combined);
                return (Convert.ToBase64String(hash), Convert.ToBase64String(salt));
            }
        }

        // Método para verificar si una contraseña coincide con un hash y una sal dados
        public bool VerifyHash(string password, string hash, string salt)
        {
            // Convertir la sal de Base64 a bytes
            byte[] saltBytes = Convert.FromBase64String(salt);

            // Combinar la contraseña con la sal almacenada
            var combined = Combine(Encoding.UTF8.GetBytes(password), saltBytes);

            // Calcular el hash SHA-256 de la combinación de contraseña y sal
            using (var sha256 = SHA256.Create())
            {
                byte[] computedHash = sha256.ComputeHash(combined);

                // Convertir el hash calculado a Base64 y compararlo con el hash almacenado
                return Convert.ToBase64String(computedHash) == hash;
            }
        }

        // Método privado para combinar dos arrays de bytes
        private byte[] Combine(byte[] first, byte[] second)
        {
            byte[] ret = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
            return ret;
        }
}

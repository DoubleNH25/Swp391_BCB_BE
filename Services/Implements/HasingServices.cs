using Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Services.Implements
{
    public class HasingServices : IHasingServices
    {
        public string encriptSHA256(string password)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            var hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }
    }
}

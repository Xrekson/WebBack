using System.Security.Cryptography;
using System.Text;

namespace WebBack.Encryption
{
    public class EncService
    {
        private byte[] _key;
        private byte[] _secret;
        private AesCryptoServiceProvider _aes;
        public EncService(string Key, string IV)
        {
            _key = Encoding.UTF8.GetBytes(Key);
            _secret = Encoding.UTF8.GetBytes(IV);
            _aes = new AesCryptoServiceProvider();
            _aes.Key = _key;
            _aes.IV = _secret;
            _aes.Mode = CipherMode.CBC;
            _aes.Padding = PaddingMode.PKCS7;
        }
        public string Encrypt(string data)
        {
            ICryptoTransform encryptor = _aes.CreateEncryptor(_aes.Key, _aes.IV);
            byte[] encryptedData = encryptor.TransformFinalBlock(Encoding.UTF8.GetBytes(data), 0, data.Length);
            return Convert.ToBase64String(encryptedData);
        }
        public string Decrypt(string encryptedData)
        {
            byte[] encryptedBytes = Convert.FromBase64String(encryptedData);
            ICryptoTransform decryptor = _aes.CreateDecryptor(_aes.Key, _aes.IV);
            byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
            return Encoding.UTF8.GetString(decryptedBytes);
        }
    }
}

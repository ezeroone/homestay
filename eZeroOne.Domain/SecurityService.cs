using System.Threading;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System;
namespace eZeroOne.Domain
{
    public class SecurityService
    {
        private static byte[] _Salt = null;
        private static string _SharedSecret = null;
        private static RijndaelManaged _CryptoService = null;

        static SecurityService()
        {
            _SharedSecret = "PasswordReset";
            _Salt = Encoding.ASCII.GetBytes("o6806642kbM7c5");
        }

        public static string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("password");
            }
            using (var md5Provider = new MD5CryptoServiceProvider())
            {
                byte[] hashedPassword = md5Provider.ComputeHash(Encoding.Default.GetBytes(password));
                var hashBuilder = new StringBuilder();
                for (int i = 0; i < hashedPassword.Length; i++)
                {
                    hashBuilder.Append(hashedPassword[i].ToString("x2"));
                }

                return hashBuilder.ToString();
            }
        }

        internal static string GenerateRecoveryKey(string email)
        {
            return email;
        }

        /* for the encryption/decryption of data we are using a symmetric algorithm to achieve the process
         * the reason for this is because we don't want to share the encrypted data with anyone, it will 
         * be returned to us and processed again */
        //public static void EncryptRecoveryToken(PasswordResetToken resetToken)
        //{
        //    var clearString = string.Format("{0};{1};{2}", resetToken.Guid, resetToken.ExpirationDate.ToString("o", Thread.CurrentThread.CurrentCulture), resetToken.Email);
        //    resetToken.EncryptedForm = EncryptString(clearString);
        //}

        //public static void DecryptRecoveryToken(PasswordResetToken resetToken)
        //{
        //    string clearString = DecryptString(resetToken.EncryptedForm);
        //    var data = clearString.Split(';');
        //    resetToken.Guid = data[0];
        //    resetToken.ExpirationDate = DateTime.ParseExact(data[1], "o", Thread.CurrentThread.CurrentCulture);
        //    resetToken.Email = data[2];
        //}

        public static string EncryptString(string clearInput)
        {
            string encrytedInput = string.Empty;
            try
            {
                using (var key = new Rfc2898DeriveBytes(_SharedSecret, _Salt))
                {
                    using (_CryptoService = new RijndaelManaged())
                    {
                        _CryptoService.Key = key.GetBytes(_CryptoService.KeySize / 8);
                        _CryptoService.IV = key.GetBytes(_CryptoService.BlockSize / 8);
                        using (ICryptoTransform cryptoTransform = _CryptoService.CreateEncryptor(_CryptoService.Key, _CryptoService.IV))
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                using (var cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write))
                                using (var streamWriter = new StreamWriter(cryptoStream))
                                {
                                    streamWriter.Write(clearInput);
                                }
                                encrytedInput = Convert.ToBase64String(memoryStream.ToArray());
                            }
                        }

                        _CryptoService.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (_CryptoService != null)
                {
                    _CryptoService.Clear();
                }
            }
            return encrytedInput;
        }

        public static string DecryptString(string encryptedInput)
        {
            string result = string.Empty;
            try
            {
                using (var key = new Rfc2898DeriveBytes(_SharedSecret, _Salt))
                {
                    using (_CryptoService = new RijndaelManaged())
                    {
                        _CryptoService.Key = key.GetBytes(_CryptoService.KeySize / 8);
                        _CryptoService.IV = key.GetBytes(_CryptoService.BlockSize / 8);
                        using (ICryptoTransform cryptoTransform = _CryptoService.CreateDecryptor(_CryptoService.Key, _CryptoService.IV))
                        {
                            byte[] buffer = Convert.FromBase64String(encryptedInput);
                            using (var memoryStream = new MemoryStream(buffer))
                            {
                                using (var cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Read))
                                using (var streamReader = new StreamReader(cryptoStream))
                                {
                                    result = streamReader.ReadToEnd();
                                }
                            }
                        }

                        _CryptoService.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (_CryptoService != null)
                {
                    _CryptoService.Clear();
                }
            }
            return result;
        }

        public static void EncryptActivationToken(EmailActivationToken activationToken)
        {
            string clearInput = string.Format("{0}||{1}", activationToken.Email, activationToken.ExpirationDate.ToString("o", Thread.CurrentThread.CurrentCulture));
            activationToken.EncryptedForm = EncryptString(clearInput);
        }

        public static void DecryptActivationToken(EmailActivationToken activationToken)
        {
            string clearString = DecryptString(activationToken.EncryptedForm);
            string[] data = clearString.Split(new string[] { "||" }, StringSplitOptions.None);
            activationToken.Email = data[0];
            activationToken.ExpirationDate = DateTime.ParseExact(data[1], "o", Thread.CurrentThread.CurrentCulture);
        }

        
    }
}
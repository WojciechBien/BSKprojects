using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace bsk_zadania
{
    public class projekt_2
    {
        public string originalFilePath;
        public string EncryptFile(string originalFile, string encryptedFile, string keyFile)
        {
            try
            {
                originalFilePath = originalFile;
                var pathForEncryptedFile = Path.GetDirectoryName(originalFile);
                // Read in the bytes from the original file:
                var originalBytes = readBytes(originalFile);
                encryptedFile = Path.Combine(pathForEncryptedFile, encryptedFile+".txt");
                var keyBytes = writeBytesToFile(originalBytes, Path.Combine(pathForEncryptedFile,keyFile+".txt"));
                // Create the one time key for encryption. This is done
                // by generating random bytes that are of the same lenght 
                // as the original bytes:


                // Encrypt the data with the Vernam-algorithm:
                byte[] encryptedBytes = new byte[originalBytes.Length];
                DoVernam(originalBytes, keyBytes, ref encryptedBytes);

                // Write the encrypted file:
                using (FileStream fs = new FileStream(encryptedFile, FileMode.Create))
                {
                    fs.Write(encryptedBytes, 0, encryptedBytes.Length);
                }
                var path = Path.GetFullPath(encryptedFile);
                var result =string.Format("File {0} has been enrypted successfully. Location of {1}: {2} \n", originalFile, Path.GetFileName(encryptedFile), path);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "Something went wrong";
            }

        }

        public string DecryptFile(string encryptedFile, string keyFile,ref string decryptedFile)
        {
            try
            {
                var pathForDecryptedFile = Path.GetDirectoryName(encryptedFile);
                decryptedFile = Path.Combine(pathForDecryptedFile, decryptedFile + ".txt");
                // Read in the encrypted bytes:
                var encryptedBytes = readBytes(encryptedFile);

                // Read in the key:

                var keyBytes = readKey(keyFile);
             
                // Decrypt the data with the Vernam-algorithm:
                byte[] decryptedBytes = new byte[encryptedBytes.Length];
                DoVernam(encryptedBytes, keyBytes, ref decryptedBytes);

                // Write the decrypted file:
                using (FileStream fs = new FileStream(decryptedFile, FileMode.Create))
                {
                    fs.Write(decryptedBytes, 0, decryptedBytes.Length);
                }
                var path = Path.GetFullPath(decryptedFile);
                var result = string.Format("\n File {0} has been decrypted successfully. Location of {1}: {2}", encryptedFile, Path.GetFileName(decryptedFile), path);
                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// CompareFiles compare file before encryption and after encryption and decryption
        /// </summary>
        /// <param name="originalFile"></param>
        /// <param name="decryptedFile"></param>
        /// <returns></returns>
        public string CompareFiles(string originalFile, string decryptedFile)
        {
            var areEquals = System.IO.File.ReadLines(originalFile).SequenceEqual(
                    System.IO.File.ReadLines(decryptedFile));

            if (areEquals)
            {
               return "\n Files were compared and original file is the same as encrypted->decrypted file. Operation was successful.";
            }
            else
            {
                return "\n Files were compared and original file is different than encrypted->decrypted file. Something went wrong";
            }
        }

        private byte[] readBytes(string directory)
        {
            byte[] originalBytes;
            using (FileStream fs = new FileStream(directory, FileMode.Open))
            {
                originalBytes = new byte[fs.Length];
                fs.Read(originalBytes, 0, originalBytes.Length);
            }
            return originalBytes;
        }

        private byte[] readKey(string keyFile)
        {
            byte[] keyBytes;
            using (FileStream fs = new FileStream(keyFile, FileMode.Open))
            {
                keyBytes = new byte[fs.Length];
                fs.Read(keyBytes, 0, keyBytes.Length);
            }
            return keyBytes;
        }

        private byte[] writeBytesToFile(byte[] originalBytes, string keyFile)
        {
            byte[] keyBytes = new byte[originalBytes.Length];
            Random random = new Random();
            random.NextBytes(keyBytes);

            // Write the key to the file:
            using (FileStream fs = new FileStream(keyFile, FileMode.Create))
            {
                fs.Write(keyBytes, 0, keyBytes.Length);
            }
            return keyBytes;
        }
        private void DoVernam(byte[] inBytes, byte[] keyBytes, ref byte[] outBytes)
        {
            // Check arguments:
            if ((inBytes.Length != keyBytes.Length) ||
                (keyBytes.Length != outBytes.Length))
                throw new ArgumentException("Byte-arrays are not of the same length");

            // Encrypt/decrypt by XOR:
            for (int i = 0; i < inBytes.Length; i++)
                outBytes[i] = (byte)(inBytes[i] ^ keyBytes[i]);
        }
    }
}

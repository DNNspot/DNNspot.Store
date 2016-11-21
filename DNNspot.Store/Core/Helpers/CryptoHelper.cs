/*
* This software is licensed under the GNU General Public License, version 2
* You may copy, distribute and modify the software as long as you track changes/dates of in source files and keep all modifications under GPL. You can distribute your application using a GPL library commercially, but you must also provide the source code.

* DNNspot Software (http://www.dnnspot.com)
* Copyright (C) 2013 Atriage Software LLC
* Authors: Kevin Southworth, Matthew Hall, Ryan Doom

* This program is free software; you can redistribute it and/or
* modify it under the terms of the GNU General Public License
* as published by the Free Software Foundation; either version 2
* of the License, or (at your option) any later version.

* This program is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
* GNU General Public License for more details.

* You should have received a copy of the GNU General Public License
* along with this program; if not, write to the Free Software
* Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

* Full license viewable here: http://www.gnu.org/licenses/gpl-2.0.txt
*/

using System;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace DNNspot.Store
{
        public static class CryptoHelper
        {
            public class CryptoArgs
            {
                public string PassPhrase = "";        // can be any string
                public string SaltValue = "r@nDom$aLtV*luE";        // can be any string

                string hashAlgorithm = "SHA1";             // can be "MD5"
                int passwordIterations = 2;                  // can be any number (1 or 2 is good)
                string initVector = "@1G6c2D4e9F6g7K3"; // must be 16 bytes
                int keySize = 256;                // can be 192 or 128     

                public string InitVector
                {
                    get { return initVector; }
                }

                public string HashAlgorithm
                {
                    get { return hashAlgorithm; }
                }

                public int PasswordIterations
                {
                    get { return passwordIterations; }
                }

                public int KeySize
                {
                    get { return keySize; }
                }
            }

            public static string EncryptString(string plainText, string passPhrase)
            {
                return EncryptString(plainText, new CryptoArgs() { PassPhrase = passPhrase });
            }

            public static string EncryptString(string plainText, CryptoArgs args)
            {
                return RijndaelSimple.Encrypt(plainText,
                    args.PassPhrase,
                    args.SaltValue,
                    args.HashAlgorithm,
                    args.PasswordIterations,
                    args.InitVector,
                    args.KeySize);
            }

            public static string DecryptString(string encryptedText, string passPhrase)
            {
                return DecryptString(encryptedText, new CryptoArgs() { PassPhrase = passPhrase });
            }

            public static string DecryptString(string encryptedText, CryptoArgs args)
            {
                return RijndaelSimple.Decrypt(encryptedText,
                    args.PassPhrase,
                    args.SaltValue,
                    args.HashAlgorithm,
                    args.PasswordIterations,
                    args.InitVector,
                    args.KeySize);
            }

            ///////////////////////////////////////////////////////////////////////////////
            // SAMPLE: Symmetric key encryption and decryption using Rijndael algorithm.
            // 
            // To run this sample, create a new Visual C# project using the Console
            // Application template and replace the contents of the Class1.cs file with
            // the code below.
            //
            // THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
            // EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
            // WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
            // 
            // Copyright (C) 2002 Obviex(TM). All rights reserved.
            // 

            /// <summary>
            /// This class uses a symmetric key algorithm (Rijndael/AES) to encrypt and 
            /// decrypt data. As long as encryption and decryption routines use the same
            /// parameters to generate the keys, the keys are guaranteed to be the same.
            /// The class uses static functions with duplicate code to make it easier to
            /// demonstrate encryption and decryption logic. In a real-life application, 
            /// this may not be the most efficient way of handling encryption, so - as
            /// soon as you feel comfortable with it - you may want to redesign this class.
            /// </summary>
            public class RijndaelSimple
            {
                /// <summary>
                /// Encrypts specified plaintext using Rijndael symmetric key algorithm
                /// and returns a base64-encoded result.
                /// </summary>
                /// <param name="plainText">
                /// Plaintext value to be encrypted.
                /// </param>
                /// <param name="passPhrase">
                /// Passphrase from which a pseudo-random password will be derived. The
                /// derived password will be used to generate the encryption key.
                /// Passphrase can be any string. In this example we assume that this
                /// passphrase is an ASCII string.
                /// </param>
                /// <param name="saltValue">
                /// Salt value used along with passphrase to generate password. Salt can
                /// be any string. In this example we assume that salt is an ASCII string.
                /// </param>
                /// <param name="hashAlgorithm">
                /// Hash algorithm used to generate password. Allowed values are: "MD5" and
                /// "SHA1". SHA1 hashes are a bit slower, but more secure than MD5 hashes.
                /// </param>
                /// <param name="passwordIterations">
                /// Number of iterations used to generate password. One or two iterations
                /// should be enough.
                /// </param>
                /// <param name="initVector">
                /// Initialization vector (or IV). This value is required to encrypt the
                /// first block of plaintext data. For RijndaelManaged class IV must be 
                /// exactly 16 ASCII characters long.
                /// </param>
                /// <param name="keySize">
                /// Size of encryption key in bits. Allowed values are: 128, 192, and 256. 
                /// Longer keys are more secure than shorter keys.
                /// </param>
                /// <returns>
                /// Encrypted value formatted as a base64-encoded string.
                /// </returns>
                public static string Encrypt(string plainText,
                                             string passPhrase,
                                             string saltValue,
                                             string hashAlgorithm,
                                             int passwordIterations,
                                             string initVector,
                                             int keySize)
                {
                    // Convert strings into byte arrays.
                    // Let us assume that strings only contain ASCII codes.
                    // If strings include Unicode characters, use Unicode, UTF7, or UTF8 
                    // encoding.
                    byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
                    byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

                    // Convert our plaintext into a byte array.
                    // Let us assume that plaintext contains UTF8-encoded characters.
                    byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

                    // First, we must create a password, from which the key will be derived.
                    // This password will be generated from the specified passphrase and 
                    // salt value. The password will be created using the specified hash 
                    // algorithm. Password creation can be done in several iterations.
                    PasswordDeriveBytes password = new PasswordDeriveBytes(
                                                                    passPhrase,
                                                                    saltValueBytes,
                                                                    hashAlgorithm,
                                                                    passwordIterations);

                    // Use the password to generate pseudo-random bytes for the encryption
                    // key. Specify the size of the key in bytes (instead of bits).
                    byte[] keyBytes = password.GetBytes(keySize / 8);

                    // Create uninitialized Rijndael encryption object.
                    RijndaelManaged symmetricKey = new RijndaelManaged();

                    // It is reasonable to set encryption mode to Cipher Block Chaining
                    // (CBC). Use default options for other symmetric key parameters.
                    symmetricKey.Mode = CipherMode.CBC;

                    // Generate encryptor from the existing key bytes and initialization 
                    // vector. Key size will be defined based on the number of the key 
                    // bytes.
                    ICryptoTransform encryptor = symmetricKey.CreateEncryptor(
                                                                     keyBytes,
                                                                     initVectorBytes);

                    // Define memory stream which will be used to hold encrypted data.
                    MemoryStream memoryStream = new MemoryStream();

                    // Define cryptographic stream (always use Write mode for encryption).
                    CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                                 encryptor,
                                                                 CryptoStreamMode.Write);
                    // Start encrypting.
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

                    // Finish encrypting.
                    cryptoStream.FlushFinalBlock();

                    // Convert our encrypted data from a memory stream into a byte array.
                    byte[] cipherTextBytes = memoryStream.ToArray();

                    // Close both streams.
                    memoryStream.Close();
                    cryptoStream.Close();

                    // Convert encrypted data into a base64-encoded string.
                    string cipherText = Convert.ToBase64String(cipherTextBytes);

                    // Return encrypted string.
                    return cipherText;
                }

                /// <summary>
                /// Decrypts specified ciphertext using Rijndael symmetric key algorithm.
                /// </summary>
                /// <param name="cipherText">
                /// Base64-formatted ciphertext value.
                /// </param>
                /// <param name="passPhrase">
                /// Passphrase from which a pseudo-random password will be derived. The
                /// derived password will be used to generate the encryption key.
                /// Passphrase can be any string. In this example we assume that this
                /// passphrase is an ASCII string.
                /// </param>
                /// <param name="saltValue">
                /// Salt value used along with passphrase to generate password. Salt can
                /// be any string. In this example we assume that salt is an ASCII string.
                /// </param>
                /// <param name="hashAlgorithm">
                /// Hash algorithm used to generate password. Allowed values are: "MD5" and
                /// "SHA1". SHA1 hashes are a bit slower, but more secure than MD5 hashes.
                /// </param>
                /// <param name="passwordIterations">
                /// Number of iterations used to generate password. One or two iterations
                /// should be enough.
                /// </param>
                /// <param name="initVector">
                /// Initialization vector (or IV). This value is required to encrypt the
                /// first block of plaintext data. For RijndaelManaged class IV must be
                /// exactly 16 ASCII characters long.
                /// </param>
                /// <param name="keySize">
                /// Size of encryption key in bits. Allowed values are: 128, 192, and 256.
                /// Longer keys are more secure than shorter keys.
                /// </param>
                /// <returns>
                /// Decrypted string value.
                /// </returns>
                /// <remarks>
                /// Most of the logic in this function is similar to the Encrypt
                /// logic. In order for decryption to work, all parameters of this function
                /// - except cipherText value - must match the corresponding parameters of
                /// the Encrypt function which was called to generate the
                /// ciphertext.
                /// </remarks>
                public static string Decrypt(string cipherText,
                                             string passPhrase,
                                             string saltValue,
                                             string hashAlgorithm,
                                             int passwordIterations,
                                             string initVector,
                                             int keySize)
                {
                    // Convert strings defining encryption key characteristics into byte
                    // arrays. Let us assume that strings only contain ASCII codes.
                    // If strings include Unicode characters, use Unicode, UTF7, or UTF8
                    // encoding.
                    byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
                    byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

                    // Convert our ciphertext into a byte array.
                    byte[] cipherTextBytes = Convert.FromBase64String(cipherText);

                    // First, we must create a password, from which the key will be 
                    // derived. This password will be generated from the specified 
                    // passphrase and salt value. The password will be created using
                    // the specified hash algorithm. Password creation can be done in
                    // several iterations.            
                    PasswordDeriveBytes password = new PasswordDeriveBytes(
                                                                    passPhrase,
                                                                    saltValueBytes,
                                                                    hashAlgorithm,
                                                                    passwordIterations);

                    // Use the password to generate pseudo-random bytes for the encryption
                    // key. Specify the size of the key in bytes (instead of bits).
                    byte[] keyBytes = password.GetBytes(keySize / 8);

                    // Create uninitialized Rijndael encryption object.
                    RijndaelManaged symmetricKey = new RijndaelManaged();

                    // It is reasonable to set encryption mode to Cipher Block Chaining
                    // (CBC). Use default options for other symmetric key parameters.
                    symmetricKey.Mode = CipherMode.CBC;

                    // Generate decryptor from the existing key bytes and initialization 
                    // vector. Key size will be defined based on the number of the key 
                    // bytes.
                    ICryptoTransform decryptor = symmetricKey.CreateDecryptor(
                                                                     keyBytes,
                                                                     initVectorBytes);

                    // Define memory stream which will be used to hold encrypted data.
                    MemoryStream memoryStream = new MemoryStream(cipherTextBytes);

                    // Define cryptographic stream (always use Read mode for encryption).
                    CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                                  decryptor,
                                                                  CryptoStreamMode.Read);

                    // Since at this point we don't know what the size of decrypted data
                    // will be, allocate the buffer long enough to hold ciphertext;
                    // plaintext is never longer than ciphertext.
                    byte[] plainTextBytes = new byte[cipherTextBytes.Length];

                    // Start decrypting.
                    int decryptedByteCount = cryptoStream.Read(plainTextBytes,
                                                               0,
                                                               plainTextBytes.Length);

                    // Close both streams.
                    memoryStream.Close();
                    cryptoStream.Close();

                    // Convert decrypted data into a string. 
                    // Let us assume that the original plaintext string was UTF8-encoded.
                    string plainText = Encoding.UTF8.GetString(plainTextBytes,
                                                               0,
                                                               decryptedByteCount);

                    // Return decrypted string.   
                    return plainText;
                }
            }
        }

        //public class RijndaelSimpleTest
        //{
        //    /// <summary>
        //    /// The main entry point for the application.
        //    /// </summary>
        //    [STAThread]
        //    static void Main(string[] args)
        //    {
        //        string plainText = "Hello, World!";    // original plaintext

        //        string passPhrase = "Pas5pr@se";        // can be any string
        //        string saltValue = "s@1tValue";        // can be any string
        //        string hashAlgorithm = "SHA1";             // can be "MD5"
        //        int passwordIterations = 2;                  // can be any number
        //        string initVector = "@1B2c3D4e5F6g7H8"; // must be 16 bytes
        //        int keySize = 256;                // can be 192 or 128

        //        Console.WriteLine(String.Format("Plaintext : {0}", plainText));

        //        string cipherText = RijndaelSimple.Encrypt(plainText,
        //                                                    passPhrase,
        //                                                    saltValue,
        //                                                    hashAlgorithm,
        //                                                    passwordIterations,
        //                                                    initVector,
        //                                                    keySize);

        //        Console.WriteLine(String.Format("Encrypted : {0}", cipherText));

        //        plainText = RijndaelSimple.Decrypt(cipherText,
        //                                                    passPhrase,
        //                                                    saltValue,
        //                                                    hashAlgorithm,
        //                                                    passwordIterations,
        //                                                    initVector,
        //                                                    keySize);

        //        Console.WriteLine(String.Format("Decrypted : {0}", plainText));
        //    }
        //}

    
}

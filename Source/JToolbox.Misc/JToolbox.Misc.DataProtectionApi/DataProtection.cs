﻿using Integrative.Encryption;
using System;
using System.Security.Cryptography;
using System.Text;

namespace JToolbox.Misc.DataProtectionApi
{
    public static class DataProtection
    {
        public static string Decrypt(string encryptedText,
            string entropy = null,
            bool localMachineScope = false)
        {
            if (encryptedText == null) { return encryptedText; }

            byte[] data = Convert.FromBase64String(encryptedText);
            var entropyBytes = string.IsNullOrEmpty(entropy)
                ? null
                : Encoding.UTF8.GetBytes(entropy);
            var scope = localMachineScope ? DataProtectionScope.LocalMachine : DataProtectionScope.CurrentUser;

            var decryptedData = CrossProtect.Unprotect(data, entropyBytes, scope);

            return Encoding.UTF8.GetString(decryptedData);
        }

        public static string Encrypt(string textToEncrypt,
            string entropy = null,
            bool localMachineScope = false)
        {
            if (textToEncrypt == null) { return textToEncrypt; }

            var textBytes = Encoding.UTF8.GetBytes(textToEncrypt);
            var entropyBytes = string.IsNullOrEmpty(entropy)
                ? null
                : Encoding.UTF8.GetBytes(entropy);
            var scope = localMachineScope ? DataProtectionScope.LocalMachine : DataProtectionScope.CurrentUser;

            var encrypted = CrossProtect.Protect(textBytes, entropyBytes, scope);

            return Convert.ToBase64String(encrypted);
        }
    }
}
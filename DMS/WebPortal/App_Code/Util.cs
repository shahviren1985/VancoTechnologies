using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;

public static class UtilityManager
{
    public static string BASE_URL = string.Empty;

    static UtilityManager()
    {
        BASE_URL = ConfigurationManager.AppSettings["BASE_URL"];
    }

    public static bool CreateInstituteFolders(string instituteId)
    {
        try
        {
            var Mappingpath = System.Web.HttpContext.Current.Server.MapPath("institutes");

            string parentFolder = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["BASE_PATH"]), "institutes");

            if (!System.IO.Directory.Exists(parentFolder))
            {
                Directory.CreateDirectory(parentFolder);
            }

            string subPath = parentFolder + "/" + instituteId;

            bool IsExists = System.IO.Directory.Exists(subPath);

            if (!IsExists)
            {
                System.IO.Directory.CreateDirectory(subPath);

                System.IO.Directory.CreateDirectory(subPath + "/logo");
                System.IO.Directory.CreateDirectory(subPath + "/mail-attachments");
                System.IO.Directory.CreateDirectory(subPath + "/certificates");
                System.IO.Directory.CreateDirectory(subPath + "/photogallery");
            }
        }
        catch (Exception ex)
        {
            return false;
        }

        return true;
    }

    public static DataTable ConvertToDataTable<T>(IList<T> data)
    {
        PropertyDescriptorCollection properties =
           TypeDescriptor.GetProperties(typeof(T));
        DataTable table = new DataTable();
        foreach (PropertyDescriptor prop in properties)
            table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
        foreach (T item in data)
        {
            DataRow row = table.NewRow();
            foreach (PropertyDescriptor prop in properties)
                row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
            table.Rows.Add(row);
        }
        return table;
    }

    public static void DeleteDirectory(string target_dir)
    {
        try
        {
            //target_dir = target_dir.Remove(target_dir.LastIndexOf('/'));

            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);

                try
                {
                    File.Delete(file);
                }
                catch (Exception ex)
                {

                }
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            try
            {
                Directory.Delete(target_dir, false);
            }
            catch
            { }
        }
        catch (Exception ex)
        {
            //throw ex;
        }
    }


    public static string GetMACString()
    {
        NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
        String sMacAddress = string.Empty;
        foreach (NetworkInterface adapter in nics)
        {
            if (sMacAddress == String.Empty)// only return MAC Address from first card  
            {
                IPInterfaceProperties properties = adapter.GetIPProperties();
                sMacAddress = adapter.GetPhysicalAddress().ToString();
            }
        }

        return sMacAddress + " ||| " + DateTime.Now + " ||| " + Environment.MachineName;
    }

    public static PhysicalAddress GetMacAddress()
    {
        foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
        {
            if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet &&
                nic.OperationalStatus == OperationalStatus.Up)
            {
                return nic.GetPhysicalAddress();
            }
        }
        return null;
    }

    public static string ReadLicensedFromFile()
    {
        try
        {
            string filePath = ConfigurationManager.AppSettings["LicFile"];

            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    String line = sr.ReadToEnd();
                    return line;
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        return string.Empty;
    }

    public static bool IsLicenseValid(string macString, string license)
    {
        if (string.IsNullOrEmpty(macString) || string.IsNullOrEmpty(license))
        {
            return false; // any of parameter is empty
        }

        try
        {
            //macString = UserDetailsDAO.GetActualPassword(macString);
            //license = UserDetailsDAO.GetActualPassword(license);          

            license = Decrypt(license, "Pas5pr@se", "s@1tValue", "MD5", 5, "@1B2c3D4e5F6g7H8", 256);

            string[] macStr = macString.Split(new string[] { "|||" }, StringSplitOptions.None);
            string[] lic = license.Split(new string[] { "|||" }, StringSplitOptions.None);

            if (macStr.Length < 3 || lic.Length < 4)
            {
                return false;
            }

            DateTime macDate = Convert.ToDateTime(macStr[1]);

            DateTime licCreateDate = Convert.ToDateTime(lic[1]);
            DateTime licExpDate = Convert.ToDateTime(lic[2]);

            if (macStr[0].ToLower() == lic[0].ToLower() && macDate <= licExpDate && macDate >= licCreateDate && macStr[2].ToLower() == lic[3].ToLower())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static string Decrypt(string cipherText, string passPhrase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector, int keySize)
    {
        try
        {
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);
            byte[] keyBytes = password.GetBytes(keySize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            string plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
            return plainText;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


}
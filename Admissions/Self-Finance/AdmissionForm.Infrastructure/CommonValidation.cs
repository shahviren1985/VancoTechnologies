//-----------------------------------------------------------------------
// <copyright file="CommonValidation.cs" company="KarmSoft">
//     Copyright (c) KarmSoft. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdmissionForm.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq; 

    /// <summary>
    ///  Contains common validations
    /// </summary>
    /// <CreatedBy>Kaushik Patel</CreatedBy>    
    public static class CommonValidation
    {
        /// <summary>
        /// The byte arrays found below (called magic numbers) represent the first several bytes of the associated 
        /// file type. These leading bytes are universal and can be accurately and reliably used to determine the 
        /// type of file, even if the visible extension has changed.
        /// Maintenance: If the FileExtension enum is changed, the magic numbers list must be updated accordingly.
        ///     The ValidateFileType method will throw an exception if any FileExtension passed in is not in the
        ///     list of magic numbers. When adding a new file extension, get the magic number byte array for the 
        ///     added extension here: https://en.wikipedia.org/wiki/List_of_file_signatures
        /// </summary>        
        [SuppressMessage("Microsoft.StyleCop.CSharp.SpacingRules", "*", Justification = "Risky to change manually")]        
        private static List<KeyValuePair<FileExtension, byte[]>> fileTypes = new List<KeyValuePair<FileExtension, byte[]>>
        {
            new KeyValuePair<FileExtension, byte[]> (FileExtension.gif, new byte[] { 0x47, 0x49, 0x46, 0x38, 0x39, 0x61 }),
            new KeyValuePair<FileExtension, byte[]> (FileExtension.gif, new byte[] { 0x47, 0x49, 0x46, 0x38, 0x37, 0x61 }),
            new KeyValuePair<FileExtension, byte[]> (FileExtension.jpg, new byte[] { 0xFF, 0xD8 }),
            new KeyValuePair<FileExtension, byte[]> (FileExtension.jpeg, new byte[] { 0xFF, 0xD8 }),
            new KeyValuePair<FileExtension, byte[]> (FileExtension.png, new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A }),
            new KeyValuePair<FileExtension, byte[]> (FileExtension.pdf, new byte[] { 0x25, 0x50, 0x44, 0x46 }),
            new KeyValuePair<FileExtension, byte[]> (FileExtension.tiff, new byte[] { 0x49,0x49, 0x2A, 0x00 }),
            new KeyValuePair<FileExtension, byte[]> (FileExtension.tif, new byte[] { 0x49,0x49, 0x2A, 0x00 }),
            new KeyValuePair<FileExtension, byte[]> (FileExtension.mtiff, new byte[] { 0x49,0x49, 0x2A, 0x00 }),
            new KeyValuePair<FileExtension, byte[]> (FileExtension.tiff, new byte[] { 0x4D, 0x4D, 0x00, 0x2A }),
            new KeyValuePair<FileExtension, byte[]> (FileExtension.tif, new byte[] { 0x4D, 0x4D, 0x00, 0x2A }),
            new KeyValuePair<FileExtension, byte[]> (FileExtension.mtiff, new byte[] { 0x4D, 0x4D, 0x00, 0x2A }),
            new KeyValuePair<FileExtension, byte[]> (FileExtension.xml, new byte[] { 0x3C, 0x3F, 0x78, 0x6D, 0x6C, 0x20 })
        };

        /// <summary>
        /// New file extensions must match up to an item in the MagicNumbers list below. When adding an enum value
        /// here, see comments on the MagicNumber list on adding the associated magic number as well.
        /// </summary>
        public enum FileExtension
        {
            gif,
            jpg,
            jpeg,
            png,
            pdf,
            tiff,
            tif,
            mtiff,
            xml,
            unknown
        }
        

        /// <summary>
        /// returns true if file type is valid and underlying byte array matches file type
        /// </summary>
        /// <param name="fileName">fileName value</param>
        /// <param name="fileContent">file Content value</param>
        /// <param name="maxFileSize">maxFileSize value</param>
        /// <param name="validExtensions">validExtensions value</param>
        /// <returns>return boolean</returns>
        public static bool ValidateFileType(string fileName, byte[] fileContent, int maxFileSize, params FileExtension[] validExtensions)
        {
            var name = Path.GetFileNameWithoutExtension(fileName);
            var extension = Path.GetExtension(fileName).TrimStart('.').ToLower();

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(extension))
            {
                throw new Exception("File Name must have a value and an extension");
            }

            if (fileContent == null || !fileContent.Any())
            {
                throw new Exception("File Content cannot be null or empty");
            }

            ////Get a list of items in valid extensions list that have no matching magic number
            var unimplementedExtensions = validExtensions.Select(ext => ext.ToString()).Except(fileTypes.Select(num => num.Key.ToString()));

            ////If any values in valid extensions list does not have an associated magic number, throw and error
            if (unimplementedExtensions.Any())
            {
                throw new Exception("Magic number not found for file types(s): " + string.Join(",", unimplementedExtensions));
            }

            if (fileContent.Length > maxFileSize)
            {
                return false;
            }

            ////Get magic numbers for the given valid extensions
            var magicNumbersLst = fileTypes.Where(num => validExtensions.Any(ext => num.Key == ext)).ToList();

            ////If the file type is not in the valid list it is always invalid
            if (validExtensions.All(ext => ext.ToString() != extension))
            {
                return false;
            }

            foreach (var magicNumber in magicNumbersLst)
            {
                var index = 0;
                ////If the key to be checked is longer than the file content the content cannot match the key; continue to next magicNumber
                if (magicNumber.Value.Length > fileContent.Length)
                {
                    continue;
                }

                foreach (var b in magicNumber.Value)
                {
                    ////If bytes with matching indeces don't match, move on to the next magicNumber
                    if (b != fileContent[index])
                    {
                        break;
                    }

                    index++;
                }

                ////If index reaches the length of the magic number, it has been matched
                if (index >= magicNumber.Value.Length)
                {
                    return true;
                }
            }

            ////If it still hasn't matched a file type, the type is unsupported
            return false;
        }
    }
}

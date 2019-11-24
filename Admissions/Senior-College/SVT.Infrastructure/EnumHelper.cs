namespace SVT.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    

    /// <summary>
    /// System Enum values
    /// </summary>
    /// <CreatedBy>Kaushik Patel</CreatedBy>
    public static class EnumHelper
    {        

        //#region Methods

        ///// <summary>
        ///// Get the SystemEnumList from given Enum type
        ///// </summary>
        ///// <param name="enumType">Enum Type like typeof(EnumType)</param>
        ///// <returns>List of Enum with Name Value pair</returns>
        //public static IEnumerable<SelectListItem> GetItems(this System.Type enumType)
        //{
        //    if (!typeof(Enum).IsAssignableFrom(enumType))
        //    {
        //        throw new ArgumentException("Type must be enum");
        //    }

        //    var names = Enum.GetNames(enumType);
        //    var values = Enum.GetValues(enumType).Cast<int>();

        //    var items = names.Zip(values,
        //        (name, value) => new SelectListItem { Text = GetName(enumType, name), Value = value.ToString() });

        //    return items;
        //}

        ///// <summary>
        ///// Get Enum Name 
        ///// </summary>
        ///// <param name="enumType">enum Type</param>
        ///// <param name="name">Enum Name Value</param>
        ///// <returns>Return Enum name</returns>
        //public static string GetName(Type enumType, string name)
        //{
        //    var result = name;

        //    var attribute = enumType.GetField(name).GetCustomAttributes(inherit: false).OfType<DisplayAttribute>()
        //        .FirstOrDefault();
        //    if (attribute != null)
        //    {
        //        result = attribute.GetName();
        //    }

        //    return result;
        //}

        ///// <summary>
        ///// Get Enum Name 
        ///// </summary>
        ///// <param name="enumType">enum Type</param>
        ///// <param name="name">Enum Name Value</param>
        ///// <returns>Return Enum name</returns>
        //public static string GetDescription(Type enumType, string name)
        //{
        //    var result = name;

        //    var attribute = enumType.GetField(name).GetCustomAttributes(inherit: false).OfType<DescriptionAttribute>()
        //        .FirstOrDefault();
        //    if (attribute != null)
        //    {
        //        result = attribute.Description;
        //    }

        //    return result;
        //}

        //#endregion

        
    }
}

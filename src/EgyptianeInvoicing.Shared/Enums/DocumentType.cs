using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace EgyptianeInvoicing.Shared.Enums
{
    public enum DocumentType
    {
        [Display(Name = "Invoice", Description = "I")]
        Invoice,

        [Display(Name = "CreditNote", Description = "C")]
        CreditNote,

        [Display(Name = "DebitNote", Description = "D")]
        DebitNote
    }
    public static class EnumExtensions
    {
        public static TEnum ParseEnumFromDescription<TEnum>(string description)
            where TEnum : Enum
        {
            var enumType = typeof(TEnum);
            foreach (var field in enumType.GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                var displayAttribute = field.GetCustomAttributes(typeof(DisplayAttribute), false)
                                           .OfType<DisplayAttribute>()
                                           .FirstOrDefault();
                if (displayAttribute != null && displayAttribute.Description == description)
                {
                    return (TEnum)field.GetValue(null);
                }
            }
            throw new ArgumentException($"Enum value with description '{description}' not found.");
        }
    }
}

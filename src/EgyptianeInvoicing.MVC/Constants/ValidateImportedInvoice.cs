using EgyptianeInvoicing.Shared.Dtos;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;

public class ValidateImportedInvoice
{
    private readonly IStringLocalizer<ValidateImportedInvoice> _localizer;

    public ValidateImportedInvoice(IStringLocalizer<ValidateImportedInvoice> localizer)
    {
        _localizer = localizer;
    }

    public List<string> IsValidInvoice(ImportedInvoiceDto invoice)
    {
        var errors = new List<string>();

        if (string.IsNullOrEmpty(invoice.SerialNumber))
        {
            errors.Add(_localizer["SerialNumberRequired"]);
        }

        if (invoice.IssueDate.Date != DateTime.Today)
        {
            errors.Add(_localizer["IssueDateToday", DateTime.Today]);
        }

        List<string> validInvoiceTypes = new List<string> { "فاتورة", "اشعار دائن", "اشعار مدين" };
        if (!validInvoiceTypes.Contains(invoice.InvoiceType))
        {
            errors.Add(_localizer["InvalidInvoiceType"]);
        }

        List<string> validCustomerTypes = new List<string> { "شركة", "أجنبي", "فرد" };
        if (!validCustomerTypes.Contains(invoice.CustomerType))
        {
            errors.Add(_localizer["InvalidCustomerType"]);
        }

        if (string.IsNullOrEmpty(invoice.Name))
        {
            errors.Add(_localizer["NameRequired"]);
        }

        if (invoice.CustomerType == "شركة")
        {
            if (string.IsNullOrEmpty(invoice.RegisterNumber))
            {
                errors.Add(_localizer["RegisterNumberRequiredForCompany"]);
            }
            else if (invoice.RegisterNumber.Length != 9)
            {
                errors.Add(_localizer["RegisterNumberLength"]);
            }
        }
        else
        {
            if (string.IsNullOrEmpty(invoice.RegisterNumber))
            {
                errors.Add(_localizer["RegisterNumberRequired"]);
            }
        }

        if (string.IsNullOrEmpty(invoice.Country))
        {
            errors.Add(_localizer["CountryRequired"]);
        }

        if (string.IsNullOrEmpty(invoice.Governorate))
        {
            errors.Add(_localizer["GovernorateRequired"]);
        }

        if (string.IsNullOrEmpty(invoice.Street))
        {
            errors.Add(_localizer["StreetRequired"]);
        }

        return errors;
    }

    public List<string> IsValidInvoiceProduct(ImportedInvoiceItemDto item)
    {
        var errors = new List<string>();

        if (string.IsNullOrEmpty(item.ProductName))
        {
            errors.Add(_localizer["ProductNameRequired"]);
        }

        if (item.CodeType != "GS1" && item.CodeType != "EGs")
        {
            errors.Add(_localizer["InvalidCodeType"]);
        }

        if (item.CodeType == "GS1" && string.IsNullOrEmpty(item.InternationalProductCode))
        {
            errors.Add(_localizer["InternationalProductCodeRequired"]);
        }

        if (item.CodeType == "EGs" && string.IsNullOrEmpty(item.InternalProductCode))
        {
            errors.Add(_localizer["InternalProductCodeRequired"]);
        }

        if (string.IsNullOrEmpty(item.Unit))
        {
            errors.Add(_localizer["UnitRequired"]);
        }

        if (item.Quantity <= 0)
        {
            errors.Add(_localizer["QuantityGreaterThanZero"]);
        }

        if (item.UnitPrice <= 0)
        {
            errors.Add(_localizer["UnitPriceGreaterThanZero"]);
        }

        if (string.IsNullOrEmpty(item.Currency))
        {
            errors.Add(_localizer["CurrencyRequired"]);
        }

        if (item.Currency != "جنيه مصري" && (item.CurrencyConvert == null || item.CurrencyConvert <= 0))
        {
            errors.Add(_localizer["CurrencyConvertRequired"]);
        }

        return errors;
    }
}

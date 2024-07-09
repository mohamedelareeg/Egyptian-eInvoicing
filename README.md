# Egyptian eInvoicing Integration

Welcome to the repository for Egyptian eInvoicing integration, designed to automate document processing and facilitate seamless interaction with the Egyptian Tax Authority's eInvoicing and eReceipt systems.

## Overview

This project serves as a bridge between ERP and POS systems and the Egyptian Tax Authority's eInvoicing platform. It offers robust APIs for managing document lifecycle events, ensuring compliance, and enhancing operational efficiency for taxpayers.

### Key Features

- **Document Management**: Submit, retrieve, cancel, and manage various types of invoices and related documents effortlessly.
  
- **Authentication and Authorization**: Secure endpoints with OAuth-based authentication and manage user access tokens dynamically.

- **Notification Services**: Receive real-time updates on document statuses and events, ensuring timely action and compliance with regulatory requirements.

- **Document Packaging**: Efficiently bundle documents into packages for streamlined retrieval and processing.

## Technology Stack

The project leverages modern technologies to ensure reliability, security, and scalability:

- **Backend**: Developed using .NET Core, providing a robust and scalable foundation for APIs.
  
- **Frontend**: Includes ASP.NET MVC and WPF applications for intuitive user interfaces and seamless integration.

- **Logging**: Serilog is used for structured logging, enabling detailed monitoring and troubleshooting capabilities.

- **SDK Integration**: Integrated with the [Egyptian eInvoicing SDK](https://sdk.invoicing.eta.gov.eg/) to leverage official APIs and ensure compliance with Egyptian Tax Authority standards.

## Integration with Egyptian eInvoicing SDK

This project integrates with the [Egyptian eInvoicing SDK](https://sdk.invoicing.eta.gov.eg/) to facilitate seamless communication with the Egyptian Tax Authority's systems. The SDK provides:

- **API Documentation**: Detailed documentation on API endpoints, request formats, and response structures.
  
- **Authentication Mechanisms**: OAuth-based authentication for secure access to eInvoicing services.

- **Event Notifications**: Real-time notifications for document status changes and compliance updates.

### Screenshots

Below are screenshots demonstrating various aspects of the application:

- **Companies**: Manage company details and configurations.
  
  ![Companies](/screenshots/companies.PNG)

- **Create Companies**: Create new company profiles and integrate them with eInvoicing.

  ![Create Companies](/screenshots/create-componies.PNG)

- **Import Excel**: Bulk import invoices and related data from Excel sheets.

  ![Import Excel 1](/screenshots/import-excel.PNG)

  ![Import Excel 2](/screenshots/import-excel-2.PNG)

- **Invoices**: View and manage submitted invoices and related documents.

  ![Invoices](/screenshots/invoices.PNG)

- **Recent Invoices**: Retrieve and filter recently submitted invoices for quick access.

  ![Recent Invoices](/screenshots/recent-invoices.PNG)

# Integration APIs Documentation

## Authentication API

- **Name:** `IAuthenticationClient`
- **Type:** Authentication
- **Description:** Provides methods to authenticate and obtain access tokens for API interactions.
```csharp
// Example usage of AuthenticationClient
var authenticationClient = new AuthenticationClient(httpClientFactory, companyRepository, configuration);

// Example call to login and get access token
var accessToken = await authenticationClient.LoginAndGetAccessTokenAsync(
    companyId,
    "clientId",
    "clientSecret",
    "registrationNumber"
);
Console.WriteLine($"Access Token: {accessToken}");
```
## Code Management API

- **Name:** `ICodeManagementClient`
- **Type:** Code Management
- **Description:** Manages EGS (Electronic Governance Services) code usage, requests, and search operations.
```csharp
// Example usage of CodeManagementClient
var codeManagementClient = new CodeManagementClient(httpClientFactory, companyRepository);

// Example call to create EGS code usage
var request = new List<CreateEGSCodeUsageItemDto> { /* populate request */ };
var response = await codeManagementClient.CreateEGSCodeUsageAsync(companyId, request);
Console.WriteLine($"Create EGS Code Usage Response: {response.StatusCode}");

// Example call to search code usage requests
var searchResult = await codeManagementClient.SearchCodeUsageRequestsAsync(companyId, "true", "Approved", "10", "1", "Descending");
foreach (var result in searchResult)
{
    Console.WriteLine($"Code Usage Request: {result.RequestId}, Status: {result.Status}");
}

```
## Document Types API

- **Name:** `IDocumentTypesClient`
- **Type:** Document Types
- **Description:** Retrieves information about document types and their versions.
```csharp
// Example usage of DocumentTypesClient
var documentTypesClient = new DocumentTypesClient(httpClientFactory, companyRepository);

// Example call to get document types
var documentTypes = await documentTypesClient.GetDocumentTypesAsync(companyId);
foreach (var type in documentTypes)
{
    Console.WriteLine($"Document Type: {type.Id}, Name: {type.Name}");
}

// Example call to get document type details
var documentType = await documentTypesClient.GetDocumentTypeAsync(companyId, documentTypeId);
Console.WriteLine($"Document Type: {documentType.Id}, Name: {documentType.Name}");

```
## Notifications API

- **Name:** `INotificationsClient`
- **Type:** Notifications
- **Description:** Retrieves notifications based on various filtering criteria such as date, type, and status.
```csharp
// Example usage of NotificationsClient
var notificationsClient = new NotificationsClient(httpClientFactory, companyRepository);

// Example call to get notifications
var notifications = await notificationsClient.GetNotificationsAsync(companyId);
foreach (var notification in notifications)
{
    Console.WriteLine($"Notification: {notification.Id}, Message: {notification.Message}");
}

```
## Document Handling API

- **Name:** `IDocumentHandlingClient`
- **Type:** Document Handling
- **Description:** Handles operations related to declining or canceling documents.
```csharp
// Example usage of DocumentHandlingClient to decline or cancel a document

try
{
    var client = new DocumentHandlingClient(httpClientFactory, companyRepository);

    // Decline or cancel document example
    Guid companyId = new Guid("your-company-id");
    string documentUUID = "document-uuid";
    string declineReason = "Reason for declining/canceling the document";

    var response = await client.DeclineCancelDocumentAsync(companyId, documentUUID, declineReason);

    if (response.IsSuccessStatusCode)
    {
        Console.WriteLine("Document declined/canceled successfully.");
    }
    else
    {
        Console.WriteLine($"Failed to decline/cancel document. Status code: {response.StatusCode}");
        var errorMessage = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Error message: {errorMessage}");
    }
}
catch (HttpRequestException ex)
{
    Console.WriteLine($"HTTP Request Exception: {ex.Message}");
}


```
## Document Notification API

- **Name:** `IDocumentNotificationClient`
- **Type:** Document Notification
- **Description:** Manages receipt of notifications related to document deliveries and packages.
```csharp
// Example usage of DocumentNotificationClient to receive document notifications

try
{
    var client = new DocumentNotificationClient(httpClientFactory, companyRepository);

    // Receive document notifications example
    Guid companyId = new Guid("your-company-id");
    string deliveryId = "delivery-id";
    string packageId = "package-id";

    var response = await client.ReceiveDocumentPackageNotificationAsync(companyId, deliveryId, packageId);

    if (response.IsSuccessStatusCode)
    {
        Console.WriteLine("Document package notification received successfully.");
    }
    else
    {
        Console.WriteLine($"Failed to receive document package notification. Status code: {response.StatusCode}");
        var errorMessage = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Error message: {errorMessage}");
    }
}
catch (HttpRequestException ex)
{
    Console.WriteLine($"HTTP Request Exception: {ex.Message}");
}


```
## Document Operations API

- **Name:** `IDocumentOperationsClient`
- **Type:** Document Operations
- **Description:** Performs operations on documents such as cancellation, rejection, and retrieval based on various criteria.
```csharp
// Example usage of DocumentOperationsClient to cancel or reject a document

try
{
    var client = new DocumentOperationsClient(httpClientFactory, companyRepository);

    // Cancel document example
    Guid companyId = new Guid("your-company-id");
    string documentUUID = "document-uuid";
    string reason = "Reason for cancellation/rejection";

    var response = await client.CancelDocumentAsync(companyId, documentUUID, reason);

    if (response.IsSuccessStatusCode)
    {
        Console.WriteLine("Document canceled/rejected successfully.");
    }
    else
    {
        Console.WriteLine($"Failed to cancel/reject document. Status code: {response.StatusCode}");
        var errorMessage = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Error message: {errorMessage}");
    }
}
catch (HttpRequestException ex)
{
    Console.WriteLine($"HTTP Request Exception: {ex.Message}");
}

```
## Document Package API

- **Name:** `IDocumentPackageClient`
- **Type:** Document Package
- **Description:** Handles requests and retrieval of document packages, including metadata and content.
```csharp
// Assuming you have initialized necessary dependencies (like IHttpClientFactory and ICompanyRepository)
var documentPackageClient = new DocumentPackageClient(httpClientFactory, companyRepository);

// Example usage to request a document package
var companyId = Guid.NewGuid(); // Replace with actual companyId
var requestDto = new DocumentPackageRequestDto
{
    // Populate with necessary request details
};
try
{
    var packageResponse = await documentPackageClient.RequestDocumentPackageAsync(companyId, requestDto);
    
    if (packageResponse.IsReady)
    {
        // Package is ready for download
        Console.WriteLine($"Package is ready. Content Type: {packageResponse.ContentType}, Content Length: {packageResponse.ContentLength}");

        // Example: Save packageData to file or process as needed
        File.WriteAllBytes("document_package.zip", packageResponse.PackageData);
    }
    else
    {
        // Package is not yet ready
        Console.WriteLine("Package is not ready for download.");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error requesting document package: {ex.Message}");
}

```
## Document Retrieval API

- **Name:** `IDocumentRetrievalClient`
- **Type:** Document Retrieval
- **Description:** Retrieves specific documents, their details, and PDF representations.
```csharp
// Assuming you have initialized necessary dependencies (like IHttpClientFactory and ICompanyRepository)
var documentRetrievalClient = new DocumentRetrievalClient(httpClientFactory, companyRepository);

// Example usage to retrieve document details
var companyId = Guid.NewGuid(); // Replace with actual companyId
var documentUUID = "123456"; // Replace with actual document UUID

try
{
    var documentDetails = await documentRetrievalClient.GetDocumentDetailsAsync(companyId, documentUUID);
    
    // Example: Access document details
    Console.WriteLine($"Document Details - ID: {documentDetails.DocumentId}, Type: {documentDetails.DocumentType}, Date: {documentDetails.DocumentDate}");

    // Example usage to retrieve PDF of the document
    var pdfBytes = await documentRetrievalClient.GetDocumentPdfAsync(companyId, documentUUID);
    
    // Example: Save PDF to file or process as needed
    File.WriteAllBytes("document.pdf", pdfBytes);
}
catch (Exception ex)
{
    Console.WriteLine($"Error retrieving document: {ex.Message}");
}

```
## Invoice Submission API

- **Name:** `IInvoiceSubmissionClient`
- **Type:** Invoice Submission
- **Description:** Submits various types of invoices (Regular, Debit Note, Credit Note, Export) for processing.
```csharp
// Example usage of IInvoiceSubmissionClient
var companyDto = new CompanyDto
{
   Id = company.CommercialRegistrationNo,
   Name = company.Name,
   Type = company.Type.ToString(),
   Address = new AddressDto
   {

       Country = "EG",//company.Address.Country,
       BranchID = company.Address.BranchId.ToString(),
       PostalCode = company.Address.PostalCode,
       RegionCity = company.Address.RegionCity,
       BuildingNumber = company.Address.BuildingNumber,
       Floor = company.Address.Floor,
       Governate = company.Address.Governorate,
       Landmark = company.Address.Landmark,
       Room = company.Address.Room,
       Street = company.Address.Street,
       AdditionalInformation = company.Address.AdditionalInformation,
   }
};
var invoicesDto = new List<EInvoiceDto>();
foreach (var invoice in request.Request)
{
   var invoiceDto = new EInvoiceDto();
   invoiceDto.Issuer = companyDto;
   #region Customer Information
   var customerType = CompanyTypes.FromName(invoice.CustomerType);
   if (customerType == null)
   {
       _logger.LogError($"Invalid customer type: {invoice.CustomerType}");
       return Result.Failure<SubmissionResponseDto>("SubmitInvoiceCommand", $"Invalid customer type: {invoice.CustomerType}");
   }

   var country = CountryCodes.Codes.FirstOrDefault(c => c.ArabicDescription == invoice.Country);
   if (country == null)
   {
       _logger.LogError($"Invalid country: {invoice.Country}");
       return Result.Failure<SubmissionResponseDto>("SubmitInvoiceCommand", $"Invalid country: {invoice.Country}");
   }

   invoiceDto.Receiver = new CompanyDto
   {
       Id = invoice.RegisterNumber,
       Name = invoice.Name,
       Type = customerType.Code,
       Address = new AddressDto
       {
           Country = country.Code,
           Governate = invoice.Governorate,
           Street = invoice.Street,
           BuildingNumber = invoice.PropertyNumber,
           RegionCity = invoice.District
       }
   };
   #endregion
   var invoiceType = InvoiceType.FromName(invoice.InvoiceType);
   if (invoiceType == null)
   {
       _logger.LogError($"Invalid invoice type: {invoice.InvoiceType}");
       return Result.Failure<SubmissionResponseDto>("SubmitInvoiceCommand", $"Invalid invoice type: {invoice.InvoiceType}");
   }
   invoiceDto.DocumentType = invoiceType.Code;
   invoiceDto.DocumentTypeVersion = "1.0";
   invoiceDto.DateTimeIssued = invoice.IssueDate.ToString("yyyy-MM-ddTHH:mm:ssZ");
   invoiceDto.TaxpayerActivityCode = company.ActivityCode;
   invoiceDto.InternalID = invoice.SerialNumber;
   var invoiceLines = new List<InvoiceLineDto>();
   double totalSalesAmount = 0, totalDiscountAmount = 0, vatTotal = 0, totalItemsDiscountAmount = 0;

   foreach (var item in invoice.Items)
   {
       var unit = UnitTypes.Codes.FirstOrDefault(u => u.ArabicDescription == item.Unit);
       if (unit == null)
       {
           _logger.LogError($"Invalid unit '{item.Unit}' in invoice serial '{invoice.SerialNumber}'.");
           return Result.Failure<SubmissionResponseDto>("SubmitInvoiceCommand", $"Invalid unit '{item.Unit}' in invoice serial '{invoice.SerialNumber}'.");
       }

       var invoiceLine = new InvoiceLineDto
       {
           Description = item.ProductName,
           ItemType = item.CodeType,
           ItemCode = item.CodeType switch
           {
               "GS1" => item.InternationalProductCode,
               "EGS" => item.InternalProductCode,
               _ => null
           },
           UnitType = unit.Code,
           Quantity = Math.Round(item.Quantity, 5),
           ItemsDiscount = Math.Round(item.UnitDiscount, 5),
           Discount = new DiscountDto { Amount = Math.Round(item.UnitDiscount, 5) },
           SalesTotal = Math.Round(item.UnitPrice * item.Quantity, 5),
           UnitValue = new UnitValueDto
           {
               CurrencySold = Currencies.Codes.FirstOrDefault(c => c.ArabicDescription == item.Currency)?.Code,
               AmountEGP = Math.Round(item.UnitPrice, 5)
           }
       };

       // Calculate line totals
       double subtotal = Math.Round((double)((item.UnitPrice * invoiceLine.Quantity) - item.UnitDiscount), 5);
       double lineTotal = subtotal;

       // Calculate VAT and other taxes
       var taxableItems = new List<TaxableItemDto>();
       var vatCode = TaxSubtypes.Codes.FirstOrDefault(c => c.ArabicDescription == item.VATCode);
       if (vatCode != null)
       {
           double vatValue = Math.Round(subtotal * (item.VATPercentage / 100), 5);
           vatTotal += vatValue;
           lineTotal += vatValue;
           taxableItems.Add(new TaxableItemDto { TaxType = vatCode.TaxTypeReference, SubType = vatCode.Code, Rate = item.VATPercentage, Amount = vatValue });
       }

       // Calculate discount taxes
       var discountTaxCode = TaxSubtypes.Codes.FirstOrDefault(c => c.ArabicDescription == item.DiscountTaxCode);
       if (discountTaxCode != null)
       {
           double discountValue = Math.Round(subtotal * (item.DiscountTaxPercentage / 100), 5);
           totalDiscountAmount += discountValue;
           lineTotal -= discountValue;
           taxableItems.Add(new TaxableItemDto { TaxType = discountTaxCode.TaxTypeReference, SubType = discountTaxCode.Code, Rate = item.DiscountTaxPercentage, Amount = discountValue });
       }

       // Summarize totals
       invoiceLine.Total = Math.Round(lineTotal, 5);
       invoiceLine.NetTotal = Math.Round(subtotal, 5);
       invoiceLine.TaxableItems = taxableItems;

       totalSalesAmount += (double)invoiceLine.SalesTotal;
       totalItemsDiscountAmount += item.UnitDiscount;

       invoiceLines.Add(invoiceLine);
   }


   // Calculate invoice totals
   invoiceDto.InvoiceLines = invoiceLines;
   invoiceDto.TotalSalesAmount = Math.Round(totalSalesAmount, 5);
   invoiceDto.TotalDiscountAmount = Math.Round(totalItemsDiscountAmount, 5);
   invoiceDto.NetAmount = Math.Round(totalSalesAmount - totalItemsDiscountAmount, 5);
   invoiceDto.TotalAmount = Math.Round((double)invoiceDto.NetAmount + vatTotal - totalDiscountAmount, 5);
   invoiceDto.TaxTotals = new List<TaxTotalDto>
   {
       new TaxTotalDto { TaxType = "T1", Amount = Math.Round(vatTotal, 5) },
       new TaxTotalDto { TaxType = "T4", Amount = Math.Round(totalDiscountAmount, 5) }
   };
   invoiceDto.TotalItemsDiscountAmount = Math.Round(totalItemsDiscountAmount, 5);

   // Sign invoice
   var signatures = new List<SignatureDto>();
   var signatureValue = _tokenSigner.SignDocuments(SerializeToJson(invoiceDto), company.Credentials.TokenPin, company.Credentials.Certificate , request.certificate , request.certForSigning);
   if (signatureValue.IsFailure)
   {
       _logger.LogError(signatureValue.Error.Message);
       return Result.Failure<SubmissionResponseDto>("SubmitInvoiceCommand", $"'{signatureValue.Error.Message}' in invoice serial '{invoice.SerialNumber}'.");
   }
   signatures.Add(new SignatureDto { SignatureType = "I", Value = signatureValue.Value });
   invoiceDto.Signatures = signatures;

   invoicesDto.Add(invoiceDto);
}

var response = await _invoiceSubmissionClient.SubmitRegularInvoiceAsync(request.CompanyId, invoicesDto);
```

### Signature Reading from USB Token (ePass 2003)

To read the digital signature from a USB token like ePass 2003, follow these steps within your application:

1. **Loading PKCS#11 Library**: Load the PKCS#11 library (`dllPath`) provided by the USB token manufacturer to access cryptographic functions.
```csharp
private Result<IPkcs11Library> LoadPkcs11Library(string dllLibPath)
{
    try
    {
        Pkcs11InteropFactories factories = new Pkcs11InteropFactories();
        IPkcs11Library pkcs11Library = factories.Pkcs11LibraryFactory.LoadPkcs11Library(factories, dllLibPath, AppType.MultiThreaded);
        return Result.Success(pkcs11Library);
    }
    catch (Exception ex)
    {
        return Result.Failure<IPkcs11Library>(new Error("TokenSigningService.LoadPkcs11Library", ex.Message));
    }
}
```
2. **Finding the Appropriate Slot**: Identify and connect to the slot where the USB token is inserted using the loaded library.
```csharp
private Result<ISlot> GetFirstAvailableSlot(IPkcs11Library pkcs11Library)
{
    try
    {
        ISlot slot = pkcs11Library.GetSlotList(SlotsType.WithTokenPresent).FirstOrDefault();
        if (slot == null)
            return Result.Failure<ISlot>(new Error("TokenSigningService.GetFirstAvailableSlot", "No slots found"));

        return Result.Success(slot);
    }
    catch (Exception ex)
    {
        return Result.Failure<ISlot>(new Error("TokenSigningService.GetFirstAvailableSlot", ex.Message));
    }
}
```
3. **Opening a Session**: Open a session to interact with the USB token, enabling cryptographic operations.
```csharp
private Result<ISession> OpenSession(ISlot slot)
{
    try
    {
        ISession session = slot.OpenSession(SessionType.ReadWrite);
        return Result.Success(session);
    }
    catch (Exception ex)
    {
        return Result.Failure<ISession>(new Error("TokenSigningService.OpenSession", ex.Message));
    }
}
```
4. **Logging In**: Authenticate by logging in with the user's PIN (`tokenPin`) to access the token's private key.
```csharp
private Result<Unit> Login(ISession session, string tokenPin)
{
    try
    {
        CKS sessionState = session.GetSessionInfo().State;
        if (sessionState == CKS.CKS_RO_USER_FUNCTIONS || sessionState == CKS.CKS_RW_USER_FUNCTIONS)
        {
            _logger.LogInformation("Session is already logged in.");
            return Result.Success(Unit.Value);
        }
        session.Login(CKU.CKU_USER, Encoding.UTF8.GetBytes(tokenPin));
        return Result.Success(Unit.Value);
    }
    catch (Exception ex)
    {
        return Result.Failure<Unit>(new Error("TokenSigningService.Login", ex.Message));
    }
}
```
5. **Finding the Certificate**: Locate the certificate stored on the USB token, typically used for digital signing operations.
```csharp
private Result<IObjectHandle> FindCertificate(ISession session)
{
    try
    {
        var certificateSearchAttributes = new List<IObjectAttribute>()
        {
            session.Factories.ObjectAttributeFactory.Create(CKA.CKA_CLASS, CKO.CKO_CERTIFICATE),
            session.Factories.ObjectAttributeFactory.Create(CKA.CKA_TOKEN, true),
            session.Factories.ObjectAttributeFactory.Create(CKA.CKA_CERTIFICATE_TYPE, CKC.CKC_X_509)
        };

        IObjectHandle certificate = session.FindAllObjects(certificateSearchAttributes).FirstOrDefault();
        if (certificate == null)
            return Result.Failure<IObjectHandle>(new Error("TokenSigningService.FindCertificate", "Certificate not found"));

        return Result.Success(certificate);
    }
    catch (Exception ex)
    {
        return Result.Failure<IObjectHandle>(new Error("TokenSigningService.FindCertificate", ex.Message));
    }
}
```
6. **Signing the Data**: Utilize the cryptographic functions provided by the token to generate a digital signature for the data.
```csharp
public Result<string> UseSignature(string serializedJson, IObjectHandle certificate, X509Certificate2 certForSigning)
{
    if (string.IsNullOrEmpty(serializedJson))
        return Result.Failure<string>(new Error("TokenSigningService.ValidateInputs", "Serialized JSON is null or empty"));

    byte[] data = Encoding.UTF8.GetBytes(serializedJson);
    Result<SignedCms> cmsResult = CreateSignedCms(data, certificate, certForSigning);
    if (cmsResult.IsFailure)
        return Result.Failure<string>(cmsResult.Error);

    Result<byte[]> encodedCmsResult = ComputeSignature(cmsResult.Value);
    if (encodedCmsResult.IsFailure)
        return Result.Failure<string>(encodedCmsResult.Error);

    return Result.Success(Convert.ToBase64String(encodedCmsResult.Value));
}
```
7. **Verifying the Signature**: Optionally, verify the integrity and authenticity of the generated signature to ensure data security.

These steps enable secure access to cryptographic materials stored on the USB token (e.g., ePass 2003), facilitating digital signing operations essential for eInvoicing and secure document transactions.


## Setup Instructions

To run the project locally, follow these steps:

1. **Clone the Repository**: `git clone https://github.com/mohamedelareeg/Egyptian-eInvoicing`
   
2. **Configure Dependencies**: Update and configure dependencies as specified in the respective `*.csproj` files.

3. **Environment Setup**: Configure connection strings, API keys, and token settings based on your development or production environment.

4. **Build and Run**: Build the solution and start the application. Ensure all necessary services (e.g., database, external API endpoints) are accessible.


## Contributing

We welcome contributions to enhance and expand this project. To contribute, follow these steps:

1. **Fork the Repository**: Start your contributions by forking this repository to your GitHub account.

2. **Create a Pull Request**: Submit your changes via a pull request, ensuring you follow the existing coding standards and documentation guidelines.

3. **Collaborate**: Engage with the community by reviewing and discussing proposed changes to improve the project further.

## License

This project is licensed under the [MIT License](LICENSE), ensuring open collaboration and flexibility for commercial and non-commercial use.


using BuildingBlocks.Results;
using EgyptianeInvoicing.Signer.Services.Abstractions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Text;

namespace EgyptianeInvoicing.Signer.Services
{
    internal class JsonSerializationService : ISerializationService
    {
        private readonly ILogger<JsonSerializationService> _logger;

        public JsonSerializationService(ILogger<JsonSerializationService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Result<string> Serialize(JObject request)
        {
            if (request == null)
            {
                _logger.LogError("Request object is null");
                return Result.Failure<string>(new Error("JsonSerializationService.Serialize", "Request object is null"));
            }

            try
            {
                StringBuilder serialized = new StringBuilder();
                SerializeToken(request, serialized);
                return Result.Success(serialized.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to serialize JSON");
                return Result.Failure<string>(new Error("JsonSerializationService.Serialize", "Failed to serialize JSON"));
            }
        }

        private void SerializeToken(JToken request, StringBuilder serialized)
        {
            if (request.Parent is null)
            {
                SerializeToken(request.First, serialized);
            }
            else
            {
                if (request.Type == JTokenType.Property)
                {
                    string name = ((JProperty)request).Name.ToUpper();
                    serialized.Append("\"" + name + "\"");
                    foreach (var property in request)
                    {
                        if (property.Type == JTokenType.Object)
                        {
                            SerializeToken(property, serialized);
                        }
                        if (property.Type == JTokenType.Boolean || property.Type == JTokenType.Integer || property.Type == JTokenType.Float || property.Type == JTokenType.Date)
                        {
                            serialized.Append("\"" + property.Value<string>() + "\"");
                        }
                        if (property.Type == JTokenType.String)
                        {
                            serialized.Append(JsonConvert.ToString(property.Value<string>()));
                        }
                        if (property.Type == JTokenType.Array)
                        {
                            foreach (var item in property.Children())
                            {
                                serialized.Append("\"" + ((JProperty)request).Name.ToUpper() + "\"");
                                SerializeToken(item, serialized);
                            }
                        }
                    }
                }
                if (request.Type == JTokenType.String)
                {
                    serialized.Append(JsonConvert.ToString(request.Value<string>()));
                }
            }

            if (request.Type == JTokenType.Object)
            {
                foreach (var property in request.Children())
                {
                    if (property.Type == JTokenType.Object || property.Type == JTokenType.Property)
                    {
                        SerializeToken(property, serialized);
                    }
                }
            }
        }
    }
}

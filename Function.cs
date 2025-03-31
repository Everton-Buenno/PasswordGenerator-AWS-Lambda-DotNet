using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using PasswordGeneratorFunction.Models;
using PasswordGeneratorFunction.Services;
using System.Text.Json;
using static PasswordGeneratorFunction.Services.PasswordGeneratorFunctionService;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace PasswordGeneratorFunction;

public class Function
{
    private readonly PasswordGeneratorService _passwordService;

    public Function( )
    {
        _passwordService = new PasswordGeneratorService();
    }

    public  async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
    {

        //Cors
        var headers = new Dictionary<string, string>
        {
            { "Access-Control-Allow-Origin", "*" },//Em produção substituir * pelo domínio do site
            { "Access-Control-Allow-Headers", "Content-Type,X-Amz-Date,Authorization,X-Api-Key" },
            { "Access-Control-Allow-Methods", "OPTIONS,POST" },
            { "Content-Type", "application/json" }
        };

        if(request.HttpMethod == "OPTIONS")
        {
            return new APIGatewayProxyResponse
            {
                StatusCode = 200,
                Headers = headers,
                Body = string.Empty
            };
        }
        try
        {
            var passwordRequest = JsonSerializer.Deserialize<PasswordRequest>(request.Body);

            var password = _passwordService.GeneratePassword(passwordRequest);

            var strength = _passwordService.CalculatePasswordStrength(password);

            var response = new PasswordResponse
            {
                Password = password,
                Strength = strength,
                GeneratedAt = DateTime.UtcNow
            };

            return new APIGatewayProxyResponse
            {
                StatusCode = 200,
                Headers = headers,
                Body = JsonSerializer.Serialize(response)
            };

        }
        catch (Exception ex)
        {

            context.Logger.LogError($"Error: {ex.Message}");

            return new APIGatewayProxyResponse
            {
                StatusCode = 500,
                Headers = headers,
                Body = JsonSerializer.Serialize(new { error = "Failed to generate password" })
            };
        }

    }
}

# Password Generator Lambda

Uma função AWS Lambda escrita em .NET Core que gera senhas seguras.

## Funcionalidades

- Geração de senhas com comprimento personalizável
- Opções para incluir letras maiúsculas/minúsculas, números e símbolos
- Cálculo de força da senha (1-5)
- API REST via AWS Lambda Function URL

## Tecnologias

- .NET 8
- AWS Lambda
- AWS Lambda Function URL

## Como usar

1. Clone o repositório
2. Configure suas credenciais AWS
3. Execute `dotnet lambda deploy-function PasswordGenerator`
4. Configure a Function URL no console AWS

## Exemplo de requisição

```json
{
  "length": 16,
  "includeUppercase": true,
  "includeLowercase": true,
  "includeNumbers": true,
  "includeSpecialChars": true
}

# Projeto de Enfileiramento e Leitura de Fila no RabbitMQ utilizando Worker

Este é um projeto de exemplo que demonstra como criar uma aplicação de console em .NET Core que adiciona itens a uma fila no RabbitMQ.

## Pré-requisitos

Certifique-se de ter o seguinte instalado em seu ambiente de desenvolvimento:

- .NET Core SDK
- RabbitMQ Server

## Configuração

No arquivo `appsettings.json`, verifique se as configurações do RabbitMQ estão corretas:

```json
{
  "RabbitMQ": {
    "Hostname": "localhost",
    "Port": 5672,
    "Username": "guest",
    "Password": "guest"
  }
}
```

Certifique-se de ajustar as configurações de acordo com o seu ambiente.

##  Executando a aplicação
1. Abra um terminal ou prompt de comando.
2. Navegue até o diretório do projeto.
3. Execute o comando `dotnet run` para iniciar a aplicação.

A aplicação irá adicionar um item à fila no RabbitMQ e exibirá uma mensagem de confirmação de sucesso.

## Contribuição

Contribuições são bem-vindas! Se você encontrar algum problema ou tiver alguma sugestão, por favor, abra uma issue ou envie um pull request.

## Licença

Este projeto está licenciado sob a [Licença MIT](LICENSE). Consulte o arquivo de licença para obter mais detalhes.

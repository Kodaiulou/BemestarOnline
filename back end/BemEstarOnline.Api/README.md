# Bem Estar Online API

Backend em **C# com ASP.NET Core Web API** para a farmácia virtual Bem Estar Online.

## Como executar

1. Instale o SDK .NET 8.
2. Entre na pasta do projeto:

```bash
cd backend/BemEstarOnline.Api
```

3. Restaure os pacotes e execute:

```bash
dotnet restore
dotnet run
```

A API será iniciada em `http://localhost:5088` e a documentação Swagger ficará em `http://localhost:5088/swagger`.

## Banco de dados

Por padrão, o arquivo `appsettings.json` está com `UseInMemoryDatabase: true`, permitindo testar a API sem instalar MySQL. Para usar o MySQL, execute o script `database/db_bemestar_melhorado.sql`, altere a string `DefaultConnection` e defina `UseInMemoryDatabase` como `false`.

## Credenciais de teste

| Perfil | E-mail | Senha |
|---|---|---|
| Administrador | admin@bemestaronline.com | Admin@123 |
| Cliente | cliente@bemestaronline.com | Cliente@123 |

## Principais rotas

| Método | Rota | Finalidade |
|---|---|---|
| GET | `/api/health` | Verificar se a API está online |
| GET | `/api/produtos` | Listar produtos |
| POST | `/api/auth/login` | Login do usuário |
| POST | `/api/auth/registrar` | Cadastro de usuário |
| GET | `/api/carrinho/usuario/{id}` | Consultar carrinho |
| POST | `/api/carrinho/item` | Adicionar item ao carrinho |
| POST | `/api/pedidos` | Finalizar pedido |
| POST | `/api/cupons/validar` | Validar cupom |
| POST | `/api/delivery` | Solicitar delivery expresso |
| POST | `/api/assinaturas` | Criar assinatura |

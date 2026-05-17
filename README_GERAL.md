# Bem Estar Online — Pacote Final

Este pacote contém a entrega funcional do projeto **Bem Estar Online**, uma farmácia virtual com frontend ajustado, backend em C#, banco de dados MySQL melhorado, documentação técnica em Word e identidade visual.

## Estrutura

| Pasta | Conteúdo |
|---|---|
| `frontend/` | Site estático recebido, ajustado para tentar consumir a API C# e manter fallback local |
| `backend/BemEstarOnline.Api/` | Backend em C# com ASP.NET Core Web API |
| `database/` | Script SQL MySQL melhorado (`db_bemestar_melhorado.sql`) |
| `documentacao/` | Relatório Word de 40 páginas, diagramas de arquitetura e fluxo |
| `logo/` | Logo principal, versão vertical, header e ícones |

## Como executar o backend

1. Instale o SDK .NET 8.
2. Acesse a pasta do backend:

```bash
cd backend/BemEstarOnline.Api
```

3. Restaure e execute:

```bash
dotnet restore
dotnet run
```

A API será iniciada em `http://localhost:5088`. A documentação Swagger ficará em `http://localhost:5088/swagger`.

## Banco de dados

Por padrão, o backend está configurado para funcionar com banco em memória, o que facilita a apresentação sem instalar MySQL. Para usar MySQL, execute o arquivo `database/db_bemestar_melhorado.sql`, altere a string `DefaultConnection` em `appsettings.json` e defina `UseInMemoryDatabase` como `false`.

## Credenciais de teste

| Perfil | E-mail | Senha |
|---|---|---|
| Administrador | `admin@bemestaronline.com` | `Admin@123` |
| Cliente | `cliente@bemestaronline.com` | `Cliente@123` |

## Rotas principais

| Método | Rota | Finalidade |
|---|---|---|
| GET | `/api/health` | Verificar se a API está online |
| GET | `/api/produtos` | Listar produtos |
| POST | `/api/auth/login` | Login |
| POST | `/api/auth/registrar` | Cadastro |
| POST | `/api/pedidos` | Finalização de pedido |
| POST | `/api/cupons/validar` | Validação de cupom |

## Observação

O projeto foi validado com compilação do backend, testes HTTP básicos nos endpoints principais e verificação de sintaxe do JavaScript ajustado.

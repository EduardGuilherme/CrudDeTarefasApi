# CrudTarefas API

API desenvolvida em **.NET 6** para gerenciamento de tarefas, utilizando **Entity Framework Core** e **SQL Server 2022**.
O backend foi desenvolvido no **Visual Studio 2022**, containerizado com **Docker** para facilitar o deploy e gerenciamento do banco de dados.

---

---

## ⚙ Tecnologias utilizadas

- .NET 6
- C#
- Entity Framework Core
- SQL Server 2022
- Docker e Docker Compose
- Swagger (para documentação de API)
- Visual Studio 2022 (IDE principal)

---

## 🐳 Executando o projeto com Docker

1. Clone o repositório:

```bash
git clone <seu-repositorio>
cd CrudTarefasApi
```

2. Execute o Docker Compose:

```bash
docker-compose up -d --build
```

3. Verifique os containers:

```bash
docker ps
```

- `sqlserver` → SQL Server 2022, porta `1433`
- `crudtarefas-api` → API .NET, portas `7042` (HTTP) e `5003` (HTTPS)

---

## 🗄 Banco de Dados

- **Nome do banco:** `DbTarefas`
- **Usuário:** `sa`
- **Senha:** `SenhaForte123!` (respeitando a política de complexidade do SQL Server)
- O banco e as tabelas são criados automaticamente através do **Entity Framework Core** ao iniciar a aplicação.

### Migration automática

No `Program.cs`:

```csharp
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}
```

> Isso garante que todas as migrations sejam aplicadas ao iniciar o container.

---

## 🔗 Configuração de Conexão

No `docker-compose.yml`:

```yaml
ConnectionStrings__Database: "Server=sqlserver,1433;Database=DbTarefas;User Id=sa;Password=SenhaForte123!;TrustServerCertificate=True;"
```

No `appsettings.json`:

```json
"ConnectionStrings": {
  "Database": "Server=sqlserver,1433;Database=DbTarefas;User Id=sa;Password=SenhaForte123!;TrustServerCertificate=True;"
}
```

---

## 🚀 Endpoints da API

A API possui **CRUD completo** para tarefas:

| Método | Endpoint            | Descrição                | Exemplo Body (POST/PUT)                  |
|--------|-------------------|------------------------|----------------------------------------|
| GET    | `/api/tarefas`      | Listar todas as tarefas | -                                      |
| GET    | `/api/tarefas/{id}` | Buscar tarefa por ID    | -                                      |
| POST   | `/api/tarefas`      | Criar nova tarefa       | `{ "Titulo": "Estudar", "Descricao": "Estudar .NET 7", "Concluida": false }` |
| PUT    | `/api/tarefas/{id}` | Atualizar tarefa        | `{ "Titulo": "Estudar", "Descricao": "Estudar EF Core", "Concluida": true }` |
| DELETE | `/api/tarefas/{id}` | Deletar tarefa          | -                                      |

### Exemplo de requisição via `curl`:

```bash
curl -X POST https://localhost:7043/api/tarefas \
-H "Content-Type: application/json" \
```

### Exemplo de resposta:

```json
 {
    "id": 1,
    "name": "Tareta01",
    "description": "Este é um teste ",
    "status": 3,
    "createdAt": "2025-09-12T23:53:05.5918994",
    "usuarioId": 1,
    "usuario": {
      "id": 1,
      "name": "Eduardo Guilherme",
      "email": "Edu@teste.com",
      "senha": "1234"
    }
  },
```

> Documentação interativa via **Swagger**:  
> `https://localhost:7043/swagger/index.html`

---

## 💻 Executando no Visual Studio

1. Abra a solução `crudtarefas.sln` no **Visual Studio 2022**.
2. Configure o **Docker Compose** como projeto de inicialização.
3. Pressione **F5** para iniciar a aplicação.
4. Acesse o Swagger para testar os endpoints.

---

## 💡 Observações importantes

- A senha do SQL Server precisa atender à **política de complexidade** (mínimo 8 caracteres, letras maiúsculas, minúsculas, números e símbolos).
- Se o backend iniciar antes do SQL Server, o container aguardará até que o serviço fique saudável (graças ao `depends_on` no Docker Compose).
- Todas as migrations são aplicadas automaticamente, então não é necessário criar tabelas manualmente.

## Resultado no Swagger
<img width="1864" height="945" alt="Captura de tela 2025-09-12 213123" src="https://github.com/user-attachments/assets/34dde1ee-a9ff-4067-9b9d-e6983484c31d" />



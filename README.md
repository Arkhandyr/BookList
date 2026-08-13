<div align="center">

<img src="./client/src/assets/logotext.png" alt="BookList" width="360">

# 📚 BookList

### Plataforma social para organização e compartilhamento de leituras

![Status](https://img.shields.io/badge/status-concluído-2ea44f?style=for-the-badge)
![TCC](https://img.shields.io/badge/projeto-TCC-d98a24?style=for-the-badge)
![Licença](https://img.shields.io/badge/licença-não%20informada-lightgrey?style=for-the-badge)

</div>

## 🎓 Sobre o projeto

O **BookList** foi desenvolvido como Trabalho de Conclusão de Curso (TCC) da minha graduação em Ciência da Computação no Instituto Federal de Ciência e Tecnologia (IFSC), Campus Lages. A aplicação funciona como uma rede social literária na qual usuários podem organizar sua estante virtual, descobrir obras, avaliar livros e interagir com outros leitores.

O projeto está finalizado e este repositório permanece disponível como registro acadêmico e portfólio técnico.

## ✨ Funcionalidades

- 🔐 Cadastro, login e logout de usuários
- 👤 Perfis públicos com informações e estatísticas de leitura
- 📚 Estante virtual dividida em **quero ler**, **lendo** e **lidos**
- 🔥 Catálogo de livros em alta e ranking das obras mais lidas
- 🔎 Busca paginada por livros, autores e usuários
- 📖 Páginas individuais para livros e autores
- ⭐ Avaliações e resenhas de obras
- ❤️ Curtidas em resenhas
- 🤝 Sistema para seguir e deixar de seguir usuários
- 🏅 Conquistas e badges associados ao perfil
- ➕ Cadastro de novas obras no catálogo

## 🛠️ Tecnologias

### 🎨 Front-end

- **Angular 14**
- **TypeScript 4.7**
- **Angular Router** e route guards
- **Angular Forms**
- **Angular Material**
- **Bootstrap 5** e ng-bootstrap
- **RxJS**
- **SCSS**
- **ngx-toastr**
- **Nginx** para servir a aplicação em produção

### ⚙️ Back-end

- **.NET 7**
- **ASP.NET Core Minimal APIs**
- **C#**
- **MongoDB Driver 2.20**
- **JWT** para autenticação
- **BCrypt** para hash de senhas
- **Swagger / OpenAPI** para documentação da API

### 🗄️ Infraestrutura

- **MongoDB**
- **Mongo Express**
- **Docker** e **Docker Compose**
- **Fly.io**
- Contêineres Linux

## 🏗️ Arquitetura

O sistema é dividido em cliente e servidor:

```text
BookList/
├── client/                         # SPA em Angular
│   ├── src/app/pages/              # Telas da aplicação
│   ├── src/app/services/           # Comunicação com a API
│   ├── src/app/guards/             # Proteção das rotas
│   ├── src/app/interfaces/         # Contratos TypeScript
│   └── src/environments/           # URLs por ambiente
│
└── server/
    └── BookList/                   # API em ASP.NET Core
        ├── Model/                  # Entidades e DTOs
        ├── Repository/             # Acesso ao MongoDB
        ├── Service/                # Regras de negócio
        ├── Helpers/                # Geração e validação de JWT
        ├── DB/                     # Contexto do banco
        └── Program.cs              # Configuração e endpoints
```

O back-end adota separação em camadas de **endpoints**, **serviços** e **repositórios**. O front-end consome a API por serviços Angular e protege as páginas autenticadas com um `AuthGuard`.

## 🔐 Autenticação e segurança

- As senhas são armazenadas com hash gerado pelo **BCrypt**.
- O servidor gera um **JSON Web Token (JWT)** após o login.
- O token é enviado em um cookie marcado como **HTTP-only**.
- A API possui política de **CORS** específica para os ambientes de desenvolvimento e produção.
- As rotas protegidas no cliente utilizam um guard de autenticação.

## 🗃️ Modelo de dados

Os dados são persistidos no banco `BookList`, no MongoDB. As coleções utilizadas pela aplicação são:

| Coleção | Responsabilidade |
|:--------|:-----------------|
| `Books` | Catálogo e informações das obras |
| `Authors` | Informações dos autores |
| `Users` | Contas e perfis dos usuários |
| `Users_Books` | Relação entre usuários, livros e listas de leitura |
| `Reviews` | Avaliações e resenhas |
| `Follows` | Relacionamentos entre usuários |
| `Badges` | Conquistas disponíveis |
| `Users_Badges` | Conquistas obtidas pelos usuários |

## 🚀 Execução local

### 📋 Pré-requisitos

- [Node.js](https://nodejs.org/) e npm
- [Angular CLI](https://angular.dev/tools/cli)
- [.NET SDK 7](https://dotnet.microsoft.com/download/dotnet/7.0)
- [MongoDB](https://www.mongodb.com/docs/manual/installation/)

### 1️⃣ Banco de dados

Inicie uma instância do MongoDB na porta padrão `27017`. Por padrão, a API utiliza:

```text
mongodb://localhost:27017
```

Para usar outra instância, defina a variável de ambiente `MongoConnectionString`.

### 2️⃣ API

```bash
cd server/BookList
dotnet restore
dotnet run
```

No perfil HTTP de desenvolvimento, a API fica disponível em `http://localhost:7838`. A interface do Swagger é habilitada no ambiente de desenvolvimento.

### 3️⃣ Aplicação web

Em outro terminal:

```bash
cd client
npm install
npm start
```

Acesse `http://localhost:4200` no navegador.

> ⚠️ O repositório não inclui um processo automático para popular o banco. Para utilizar catálogos e autores, é necessário fornecer dados compatíveis nas coleções do MongoDB ou cadastrá-los pela aplicação/API.

## 🐳 Contêineres

O projeto contém arquivos de build para:

- API .NET em uma imagem Linux;
- aplicação Angular compilada e servida pelo Nginx;
- MongoDB e Mongo Express via Docker Compose.

Os arquivos de contêiner foram preservados como parte da infraestrutura original do TCC e podem exigir ajustes de rede, variáveis de ambiente e imagens locais antes de uma nova implantação.

## 🌐 Endpoints principais

| Método | Rota | Descrição |
|:------:|:-----|:----------|
| `POST` | `/register` | Cadastra um usuário |
| `POST` | `/login` | Autentica e cria a sessão |
| `GET` | `/catalog/trending/{page}` | Lista livros em alta |
| `GET` | `/catalog/top/{page}` | Lista os livros mais lidos |
| `GET` | `/book/{id}` | Retorna os detalhes de uma obra |
| `POST` | `/addToList` | Adiciona uma obra à estante |
| `POST` | `/books/addReview` | Publica uma resenha |
| `GET` | `/profile/{username}` | Retorna um perfil público |
| `GET` | `/searchBooks/{filter}/{page}` | Pesquisa livros |
| `GET` | `/searchAuthors/{filter}/{page}` | Pesquisa autores |
| `GET` | `/searchUsers/{filter}/{page}` | Pesquisa usuários |
| `POST` | `/follow` | Segue outro usuário |

## 📌 Situação do projeto

✅ **Concluído** — projeto desenvolvido e entregue como Trabalho de Conclusão de Curso.

Como se trata de um registro acadêmico, versões das dependências e configurações de implantação refletem o período em que o sistema foi desenvolvido.

## 👨‍💻 Autor

Desenvolvido por **[Arkhandyr](https://github.com/Arkhandyr)**.

---

<div align="center">

Feito com 💻, 📚 e muito ☕

[⬆️ Voltar ao início](#-booklist)

</div>

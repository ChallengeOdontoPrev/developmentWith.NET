# Projeto de Blog de Atualizações da OdontoPrev

## Visão Geral do Projeto

Este projeto é parte de uma solução maior da OdontoPrev para reduzir sinistros em procedimentos odontológicos. A solução principal consiste em um aplicativo mobile que utiliza inteligência artificial para validar consultas odontológicas através da análise de imagens antes e depois dos procedimentos.

O componente específico abordado neste repositório é uma aplicação web desenvolvida em .NET MVC. Esta aplicação serve como um blog para publicar atualizações, novidades e informações importantes relacionadas ao aplicativo principal de validação de consultas.

O objetivo é manter usuários e profissionais que trabalham com a OdontoPrev informados sobre todas as atualizações e melhorias do sistema principal, garantindo transparência e facilitando a adoção de novas funcionalidades.

## Arquitetura do Projeto

Este projeto segue os princípios da Clean Architecture para manter o código desacoplado e facilitar a manutenção e escalabilidade. A estrutura do projeto é dividida nas seguintes camadas:

[Clique aqui para acessar o desenho da arquitetura!](https://whimsical.com/odontoprev-BCdqprBhTvpmQRitEzJCn2)

### Páginas Implementadas

1. **Página Inicial (Index)**
   - Hero section com banner destacando o propósito do sistema
   - Seção de features principais
   - Grid de cards com as últimas atualizações do blog
   - Design responsivo e interativo

2. **Página de Post Individual**
   - Visualização detalhada de cada post
   - Imagem de capa
   - Conteúdo formatado em parágrafos
   - Data de publicação
   - Navegação para retornar à página inicial

3. **Página de Login**
   - Formulário de autenticação para autores
   - Validação de campos
   - Mensagens de erro/sucesso
   - Layout responsivo com imagem ilustrativa

4. **Dashboard do Autor**
   - Interface para gerenciamento de posts
   - Grid de cards com todos os posts do autor
   - Modal para criação de novo post
   - Modal para edição de posts existentes
   - Confirmação para exclusão de posts
   - Sistema de notificações toast
   - Ações rápidas em cada card (editar/excluir)

### Camadas da Aplicação

1. **Apresentação**
   - Controllers: AuthController, PostController
   - Views/Pages: Index, Post, Login, Author/Dashboard

2. **Aplicação**
   - Services: AuthorService, BlogService
   - ViewModels: AuthorViewModel, PostViewModel, LoginViewModel

3. **Domínio**
   - Models: Author, Post

4. **Infraestrutura**
   - Data: BlogDbContext
   - Repositories: AuthorRepository, PostRepository

## Definição do Projeto

### Objetivo do Projeto
O objetivo deste projeto é criar uma plataforma de blog para publicar atualizações sobre o aplicativo mobile da OdontoPrev. Este blog servirá como um canal de comunicação para manter usuários e profissionais informados sobre as últimas novidades e melhorias no aplicativo de validação de consultas odontológicas.

### Escopo
Este projeto abrange o desenvolvimento de uma aplicação web utilizando .NET MVC para gerenciar e exibir posts de blog. O escopo inclui:

1. Sistema de autenticação para autores do blog
2. Interface de administração para criar, editar e excluir posts
3. Página pública para visualização dos posts de atualização
4. API para possível integração futura com o aplicativo mobile

### Requisitos Funcionais
1. Autenticação de autores
   - Login seguro para autores do blog
   - Gerenciamento de perfis de autor

2. Gerenciamento de Posts
   - Criação de novos posts
   - Edição de posts existentes
   - Exclusão de posts
   - Visualização de todos os posts (para autores)

3. Visualização Pública
   - Listagem de posts de atualização em ordem cronológica
   - Visualização detalhada de posts individuais
   - Funcionalidade de busca por palavras-chave

4. API RESTful
   - Endpoints para listar posts
   - Endpoint para recuperar detalhes de um post específico

### Requisitos Não Funcionais
1. Segurança
   - Utilização de HTTPS para todas as comunicações
   - Armazenamento seguro de senhas (hashing)
   - Proteção contra ataques comuns (SQL Injection, XSS, CSRF)

2. Desempenho
   - Tempo de carregamento da página inicial inferior a 2 segundos
   - Capacidade de lidar com pelo menos 1000 usuários simultâneos

3. Usabilidade
   - Interface responsiva para acesso via desktop e dispositivos móveis
   - Design intuitivo e acessível

4. Manutenibilidade
   - Código bem documentado
   - Utilização de padrões de projeto para facilitar futuras expansões

5. Compatibilidade
   - Suporte aos principais navegadores (Chrome, Firefox, Safari, Edge)

6. Escalabilidade
   - Arquitetura que permita fácil escalabilidade horizontal

## Detalhes das Classes Principais

### Author
Propriedades:
- Id (int): Identificador único do autor
- Name (string): Nome completo do autor
- Email (string): Email do autor
- Username (string): Nome de usuário para login
- PasswordHash (string): Hash da senha do autor
- Posts (ICollection<Post>): Coleção de posts do autor

### Post
Propriedades:
- Id (int): Identificador único do post
- Title (string): Título do post
- Content (string): Conteúdo do post
- CreatedAt (DateTime): Data e hora de criação
- AuthorId (int): ID do autor do post
- Author (Author): Referência ao autor do post

### Controllers

#### AuthController
Endpoints:
- POST /api/auth/login: Autenticação de usuários
- GET /api/auth/authors: Listagem de autores (protegido)

#### PostController
Endpoints:
- GET /api/post: Lista todos os posts
- GET /api/post/{id}: Obtém um post específico
- POST /api/post: Cria novo post (protegido)
- PUT /api/post/{id}: Atualiza um post (protegido)
- DELETE /api/post/{id}: Remove um post (protegido)

## Fluxo de Funcionamento

1. **Usuário Não Autenticado**
   - Acessa a página inicial
   - Visualiza lista de posts
   - Acessa posts individuais
   - Pode fazer login como autor

2. **Autor Autenticado**
   - Acessa o dashboard
   - Gerencia seus posts (CRUD)
   - Visualiza estatísticas
   - Pode fazer logout

## Tecnologias Utilizadas

### Backend
- .NET 8.0
- ASP.NET Core MVC
- Entity Framework Core
- Oracle Database
- Cookie Authentication

### Frontend
- Bootstrap 5
- Font Awesome
- Bootstrap Icons
- jQuery
- Marked.js
- CSS customizado
- JavaScript moderno

### Ferramentas de Desenvolvimento
- Visual Studio 2022
- Git para controle de versão
- Oracle SQL Developer

## INTEGRANTES

### Gustavo Araújo Maia
### Kauã Almeida Silveira
### Rafael Vida Fernandes

### Turmas: 2TDSPS
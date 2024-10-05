# Projeto de Blog de Atualizações da OdontoPrev

## Visão Geral do Projeto

Este projeto é parte de uma solução maior da OdontoPrev para reduzir sinistros em procedimentos odontológicos. A solução principal consiste em um aplicativo mobile que utiliza inteligência artificial para validar consultas odontológicas através da análise de imagens antes e depois dos procedimentos.

O componente específico abordado neste repositório é uma aplicação web desenvolvida em .NET MVC. Esta aplicação serve como um blog para publicar atualizações, novidades e informações importantes relacionadas ao aplicativo principal de validação de consultas.

O objetivo é manter usuários e profissionais que trabalham com a OdontoPrev informados sobre todas as atualizações e melhorias do sistema principal, garantindo transparência e facilitando a adoção de novas funcionalidades.

## Arquitetura do Projeto

Este projeto segue os princípios da Clean Architecture para manter o código desacoplado e facilitar a manutenção e escalabilidade. A estrutura do projeto é dividida nas seguintes camadas:

[Clique aqui para acessar o desenho da arquitetura! ](https://whimsical.com/odontoprev-BCdqprBhTvpmQRitEzJCn2)

### Camadas da Aplicação

1. **Apresentação**
   - Controllers: AuthController, PostController
   - Views (não implementadas neste projeto MVC API)

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
- Propriedades:
  - ID
  - Name
  - Email
  - Senha
  

### Post
- Propriedades:
  - ID
  - Título
  - Conteúdo
  - Data
  - Hora

### AuthorController
- Métodos:
  - Put: Login
  - Get: ID

### PostController
- Métodos:
  - Get: Feed
  - Post: Criar Post
  - Get: Busca Post
  - PUT: Editar
  - Delete: Apagar

## Fluxo de Funcionamento

1. **Usuário**
   - Acessa a aplicação
   - Visualiza a tela inicial
   - Acessa os blogs

2. **Autor**
   - Acessa a aplicação
   - Visualiza o feed
   - Pode acessar blogs ou a página de postagens
   - Se não estiver logado, é redirecionado para a página de login
   - Após o login bem-sucedido, pode:
     - Adicionar nova postagem (se for o autor)
     - Editar ou apagar postagens existentes (se for o autor)

## Tecnologias Utilizadas
- .NET 8.0 ou superior
- ASP.NET Core MVC
- Entity Framework Core
- Banco de dados Oracle
- JWT para autenticação


# INTEGRANTE

### Gustavo Araújo Maia
### Kauã Almeida Silveira
### Rafael Vida Fernandes

### Turmas: 2TDSPS




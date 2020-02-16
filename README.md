# AirplaneServices DESCRIÇÃO :

 O objetivo principal é criar uma aplicação contendo o CRUD completo da tabela Airplane:

# Campos:
    ⦁ Código do Avião
    ⦁ Modelo
    ⦁ Quantidade de passageiros
    ⦁ Data de criação do registro

# Tecnologias e patterns obrigatórios:
    ⦁ FRONT-END – Angular 7
    ⦁ BACK-END - .Net Core / Entity Framework Core / RESTful Api
    ⦁ PATTERNS – DDD e Injeção de Dependência
    ⦁ *Você pode utilizar frameworks de mercado para os patterns.
    ⦁ O código fonte deverá ser armazenado em um repositório público do github, e enviar o link para análise.
    ⦁ Desejável: Manual de Instalação da aplicação

# Manual de Instalação da aplicação

    ## para executar o projeto backend

        - crie uma variável de ambiente no windows com a connectionstring do seu banco conforme abaixo
            Variable Name = LocalConnection
            Variable value = Data Source=DESKTOP-OUM5KHF\SQLEXPRESS;Initial Catalog=AirServicesDB;Integrated Security=True

        - executar a api 

        - Api utiliza Swagger para  documentação e utilização 
                https://localhost:5000/swagger

    ## para executar o projeto frontend

        - entre no diretório do projeto
        - execute 'npm install' para instalar as dependências
        - execute 'ng serve --open' no console da IDE para abrir o projeto
 
 
# AirplaneServices Tecnologias utilizadas:

    ⦁ FRONT-END – Angular 7
    ⦁ BACK-END - .Net Core 3.1 / Entity Framework Core / RESTful Api
    ⦁ PATTERNS – DDD e Injeção de Dependência

# AirplaneServices Layers:

    - BackEnd

        AirplaneServices.Application
        AirplaneServices.Domain
        AirplaneServices.Infra
        AirplaneServices.IoC
        AirplaneServices.WebAPI
        AirplaneServices.WebAPI.Tests.Unit
        
    - FrontEnd

        AirplaneServices.Web

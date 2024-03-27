## Financial Processing
  Este é um aplicativo de processamento financeiro que inclui um servidor TCP para receber dados de mercado, processá-los e enviá-los de volta ao cliente. Ele também inclui testes unitários e é contêinerizado com Docker.

## Pré-requisitos
  * .NET Core SDK instalado
  * Docker Desktop (opcional, apenas se você quiser usar contêineres Docker)

## Como executar o projeto
  1. Clonar Repositório
  2. Navegar para o diretório do projeto
  3. Executar o aplicativo localmente
    ```
    dotnet run --project FinancialProcessing
    ```
  5. Executar os testes unitários
     ```
     dotnet test
     ```

## Como usar o docker (opcional)
  1. Construir a imagem Docker
     ```
     docker build -t financial-processing
     ```
  2. Executar o contêiner Docker
     ```
     docker run -d -p 8888:8888 financial-processing
     ```

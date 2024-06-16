#  Sistema de Gestão de Portfólio de Investimentos

## Descrição
O projeto consiste em gerenciar ativos de investimentos possíbilitando a criação de transações em nome de clientes podendo ser compra ou venda dos ativos, essa api disponibiliza a consulta desses produtos, criar transações, consultar a transação, consultar produtos próximos de vencimentos e enviar email para administradores.

## Índice
1. [Descrição](#descrição)
2. [Tecnologias](#tecnologias)
3. [Pré-requisitos](#pré-requisitos)
4. [Instalação](#instalação)
5. [Rodando o Projeto](#rodando-o-projeto)
6. [Uso](#uso)
7. [Contribuição](#contribuição)
8. [Licença](#licença)
9. [Contato](#contato)

## Tecnologias
Lista das principais tecnologias e ferramentas usadas no projeto:
- [.NET Core 8.0](https://dotnet.microsoft.com/pt-br/)
- [MondoDB](https://www.mongodb.com/)
- [Redis](https://redis.io/)
- [Docker](https://www.docker.com/)
- [Nginx](https://nginx.org/en/)

## Pré-requisitos
Lista de pré-requisitos necessários para rodar o projeto:
- [Docker](https://www.docker.com/) ou instalar todas as demais tecnologias citadas acima e realizar o passo de [Sem Docker](###Sem-Docker)
 

## Instalação
Passos para instalar o projeto:

1. Clone o repositório:
    ```sh
    git clone https://github.com/Matheus216/asset_management.git
    ```
2. Navegue até a pasta em que está o docker-compose.yaml:
    ```sh
    cd asset_management
    ```

## Rodando o Projeto
Instruções para rodar o projeto localmente:

### Usando Docker
* Garanta que esteja instalado o [docker-compose](https://docs.docker.com/compose/) em sua máquina

1. Construa a imagem Docker:
    ```sh
    docker-compose build / docker compose build
    ```
2. Rode o contêiner:
    ```sh
    docker-compose up
    ```

### Sem Docker
1. Instale o mongodb seguindo passo a passo da documentação e inicie um servidor na porta 27017:

2. Instale redis localmente seguindo a documentação e inicie na porta 6379

3. Troque as connections string ./API/Asset.Management.API/appsettings.json tanto referente ao mongodb quanto referente ao redis apontando para suas respectivas portas

4. Navegue até a pasta onde está o csproj do projeto de inicialização
```
cd ./API/Asset.Management.API
``` 

5. Rode o comando de execuçãos
```
dotnet run 
```

## Uso
Instruções de uso ou exemplos de como utilizar a aplicação:

#### GET http://localhost:8080/api/produtos
Retorna uma lista de itens.

- **URL**: `/api/produtos`
- **Método HTTP**: `GET`
- **Parâmetros de Consulta**: Nenhum
- **Resposta**:
    - **Código**: 200 OK
    - **Corpo**:
    ```json
    {
      "messageError": [], 
      "success": true,
      "data": [
      {
         "id": "666ed4e2abd6247377a26a13",
         "description": " 3R PETROLEUM",
         "code": "RRRP3",
         "expirationDate": "2023-07-01T00:00:00Z",
         "price": 12,
         "availableQuantity": 99999
      },
      {
         "id": "666ed4e2abd6247377a26a14",
         "description": " ALLOS",
         "code": "ALOS3",
         "expirationDate": "2023-07-02T00:00:00Z",
         "price": 13,
         "availableQuantity": 99999
      }]
    }
    ```

#### GET http://localhost:8080/api/produtos/{id}
Retorna um único produto pelo id .

- **URL**: `/api/produtos/{id}`
- **Método HTTP**: `GET`
- **Parâmetro da rota**:
   - `id` (string) ID do produto
- **Resposta**:
    - **Código**: 200 OK
    - **Corpo**:
    ```json
    {
      "messageError": [], 
      "success": true,
      "data": 
      {
         "id": "666ed4e2abd6247377a26a13",
         "description": " 3R PETROLEUM",
         "code": "RRRP3",
         "expirationDate": "2023-07-01T00:00:00Z",
         "price": 12,
         "availableQuantity": 99999
      }
    }
    ```
#### GET http://localhost:8080/api/produtos/{daysToExpiration}
Retorna uma lista de prdutos próximo ao vencimento.

- **URL**: `/api/produtos/{daysToExpiration}`
- **Método HTTP**: `GET`
- **Parâmetro da rota**:
   - `daysToExpiration` (string) dias a frente para expirar que deseja buscar
- **Resposta**:
    - **Código**: 200 OK
    - **Corpo**:
    ```json
    {
      "messageError": [], 
      "success": true,
      "data": [
      {
         "id": "666ed4e2abd6247377a26a13",
         "description": " 3R PETROLEUM",
         "code": "RRRP3",
         "expirationDate": "2023-07-01T00:00:00Z",
         "price": 12,
         "availableQuantity": 99999
      },
      {
         "id": "666ed4e2abd6247377a26a14",
         "description": " ALLOS",
         "code": "ALOS3",
         "expirationDate": "2023-07-02T00:00:00Z",
         "price": 13,
         "availableQuantity": 99999
      }]
    }
    ```

#### POST http://localhost:8080/api/transaction
Cria uma nova transação.

- **URL**: `/api/transaction`
- **Método HTTP**: `POST`
- **Corpo da Requisição**:
    ```json
   {
      "productId": "666ed4e2abd6247377a26a14",
      "quantity": 23,
      "clientId": "123456",
      "typeTransaction": 0 //0 - compra / 1 - venda
   }
    ```
- **Resposta**:
    - **Código**: 201 Created
    - **Corpo**:
    ```json
    {
      "data": {
         "id": "666ee27d3b62566fe38f981d",
         "productId": "666ed4e2abd6247377a26a14",
         "quantity": 6,
         "unitValue": 12,
         "totalValue": 72,
         "transactionType": 0,
         "createdDate": "2024-06-16T13:02:52.9815983+00:00",
         "clientId": "03849384"
      },
      "success": true,
      "messagesError": []
      }
    ```

#### GET http://localhost:8080/api/transaction/{id}
Retorna uma única transação pelo id.

- **URL**: `/api/transaction/{id}`
- **Método HTTP**: `GET`
- **Parâmetro da rota**:
   - `id` (string) ID da transação 
- **Resposta**:
    - **Código**: 200 OK
    - **Corpo**:
    ```json
    {
      "messageError": [], 
      "success": true,
      "data": 
      {
         "id": "666ee27d3b62566fe38f981d",
         "productId": "666ed4e2abd6247377a26a14",
         "quantity": 6,
         "unitValue": 12,
         "totalValue": 72,
         "transactionType": 0,
         "createdDate": "2024-06-16T13:02:52.9815983+00:00",
         "clientId": "03849384"
      }
    }
    ```

#### POST http://localhost:8080/api/email/{daysToExpiration}
Envia um email para os administradores informando as transações próximas do vencimento, informar somente os dias a frente para validação e o sistema vai localizar quem está como administrador do sistema e realizar um envio com um template pré criado.<br /> 
PS: como é somente uma prova de conceito o método não foi implementado

- **URL**: `/api/email/{daysToExpiration}`
- **Método HTTP**: `POST`
- **Resposta**:
    - **Código**: 200 Success
    - **Corpo**:
    ```json
    {
      "data": {
         "Email enviado"
      },
      "success": true,
      "messagesError": []
      }
    ```
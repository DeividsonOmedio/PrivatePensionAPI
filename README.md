# PrivatePensionAPI

## Descrição
Uma API REST C# para gerenciamento, cadastro, compra e consulta de produtos de uma previdência privada. onde  

## Requisitos
.NET SDK (versão 5.0 ou superior)
Banco de dados SQL Server
Visual Studio ou Visual Studio Code
Postman ou outra ferramenta para testar API(Pode ser testado também o Swagger)

## Configuração do Projeto
Passo 1: Clonar o Repositório

git clone https://github.com/DeividsonOmedio/PrivatePensionAPI.git

Passo 2: Configurar o Banco de Dados
Atualize a string de conexão do banco de dados no arquivo appsettings.json:


{
  "ConnectionStrings": {
    "DefaultConnection": "Server=seu_servidor;Database=sua_base_de_dados;User Id=seu_usuario;Password=sua_senha;"
  }
}

Passo 3: Aplicar Migrações
Aplique as migrações para criar as tabelas necessárias no banco de dados:
dotnet ef migrations add InitialCreate ou Add-Migration NomeDaMigracao
dotnet ef database update ou Update-Database

Passo 4: Restaurar Dependências
Restaure as dependências do projeto:
dotnet restore

Executando o Projeto
Para iniciar o projeto, use o seguinte comando:

No Visual studio clique no botão HTTPS
O projeto estará disponível em https://localhost:7109/.
ou dotnet watch run (os 2 comandos abre o swagger no navegador.

# Testando a Autenticação

## Uso da Aplicação
O Admistrador faz o login, cadastra os usuários, aprova as compras, adicona saldo a carteira dos clientes e visualiza edita ou deleta usuarios, produtos, compras e contribuições.
o Cliente faz o login, realiza compras, deleta compras que ainda não foram aprovadas, realiza apostes, visualiza produtos disponiveis que ainda naõ foram adquiridos e visualiza suas comprs e aportes realizados. 

### Detalhes
Todos os endpoints são protegidos, o que implica que você precisará passar o token em todas as requisições
Somente o Administrador Adiciona, Edita, Altera ou Deleta usuários, tendo um endpoint especifico para adicionar saldo a carteira do cliente.
Detalhe: Ao alterar um cliente, se não fornecer uma nova senha, a antiga será mantida, então não precisa ser passada a senha se não quiser. Lembrando que todas as senhas são criptografadas antes de serem salvas no banco.

Cliente:
O endpoint "Get" api/Product/GetProductsPurchasedByUser mostra ao cliente os produtos já comprados por ele,
já o endpont "Get" /api/Product/GetProductsNotPurchasedByUser mostra ao cliente os produtos que ainda não foram adquiridos por ele,
No endpoint "Post" /api/Purchase o cliente e só o cliente pode fazer uma compra, desde que seja um produto ainda não adquirido por ele e tenha saldo suficiente para a compra na carteira.
No endpoint "Delete" /api/Purchase/ o cliente pode cancelar uma compra, mas somente se essa compra ainda não tiver sido aprovada pelo administrador
Todas as compras precisam da aprovação do administrador.
Após a compra ser aprovado, o cliente pode realizar um aporte desde que tenha saldo suficiente. Endpoint: /api/Contribution.

Administrador:
O administrador não pode realizar compras ou aportes
No endpoint "Post" /api/User o administrador realiza o cadastro de um novo usuário, O usuario deve ter a "role"= 0 se for cliente ou 1 caso seja adminstrador, o administrador sem sempre sua carteira de saldo nula no sistema.
No endpoint "Patch"api/User/UpdateWalletBalance/Id?value={valor}' o administrador adiciona um valor a carteira do cliente
No endpoint "Put" /api/User o administrador edita o usuario, sendo esse endpoint somente para editar nome, email e senha. Detalhe: se não quiser alterar a senha é só não passar ela na requisição.
Todos os endpoints api/User/ são de acesso somente do administrador
No endpoint "Get" /api/Purchase/GetByApproved/notApproved o administrador visualiza tos as compras que não foram aprovadas
No endpoint "Get"/api/Purchase/GetByClient/{id} visualiza as compras efetuadas por um usuario especifico

Login do Usuário
Envie uma requisição POST para /api/auth/login com o corpo:

{
	"email": "admin@admin.com",
    	"password": "Admin@123"
}
ou
{
	"email": "client1@client.com",
    	"password": "Client@123"
}

Você receberá um token JWT no response. Use esse token para autenticar requisições subsequentes.


## Endpoint de AUTENTICAÇÃO

HttpPost /api/auth/login
Realizar login e gerar um token JWT.

## Todos os endpoints de USUÁRIOS:

HttpPost /api/User
Adicionar um novo usuário.

HttpPut /api/User
Atualizar um usuário existente.

HttpPatch /api/User/UpdateWalletBalance/{id}
Atualizar o saldo da carteira de um usuário pelo ID.

HttpDelete /api/User/{id}
Deletar um usuário pelo ID.

HttpGet /api/User
Buscar todos os usuários.

HttpGet /api/User/{id}
Buscar um usuário pelo ID.

HttpGet /api/User/email/{email}
Buscar um usuário pelo email.

## Todos os endpoints de PRODUTOS:

HttpPost /api/Product
Adicionar um novo produto.

HttpPut /api/Product/{id}
Atualizar um produto pelo ID.

HttpDelete /api/Product/{id}
Deletar um produto pelo ID.

HttpGet /api/Product
Buscar todos os produtos.

HttpGet /api/Product/{id}
Buscar um produto pelo ID.

HttpGet /api/Product/GetProductsPurchasedByUser
Buscar produtos comprados por um usuário específico (ID do usuário passado como parâmetro na query).

HttpGet /api/Product/GetProductsNotPurchasedByUser
Buscar produtos não comprados por um usuário específico (ID do usuário passado como parâmetro na query).

HttpGet /api/Product/GetByName/{name}
Buscar um produto pelo nome.

HttpGet /api/Product/GetByAvailable/true
Buscar produtos disponíveis.

HttpGet /api/Product/GetByAvailable/false
Buscar produtos indisponíveis.


## Todos os endpoints de COMPRAS:

HttpPost /api/Purchase
Criar uma nova compra.

HttpPatch /approve/{id}
Aprovar uma compra pelo ID.

HttpDelete /api/Purchase/{id}
Deletar uma compra pelo ID.

HttpGet /api/Purchase
Buscar todas as compras.

HttpGet /api/Purchase/{id}
Buscar uma compra pelo ID.

HttpGet /api/Purchase/GetByApproved/isApproved
Buscar todas as compras aprovadas.

HttpGet /api/Purchase/GetByApproved/notApproved
Buscar todas as compras não aprovadas.

HttpGet /api/Purchase/GetByClient/{clientId}
Buscar todas as compras de um cliente específico pelo ID do cliente.

HttpGet /api/Purchase/GetByDate/{date}
Buscar todas as compras por uma data específica.

HttpGet /api/Purchase/GetByProduct/{productId}
Buscar todas as compras por um ID de produto específico.


## Todos os endpoints de APORTES:

HttpPost /api/Contribution
Adicionar uma nova contribuição.

HttpPut /api/Contribution/{id}
Atualizar uma contribuição pelo ID.

HttpDelete /api/Contribution/{id}
Deletar uma contribuição pelo ID.

HttpGet /api/Contribution
Buscar todas as contribuições.

HttpGet /api/Contribution/{id}
Buscar uma contribuição pelo ID.

HttpGet /api/Contribution/GetByContributionDate/{contributionDate}
Buscar contribuições por uma data específica.

HttpGet /api/Contribution/GetByContributionDateByUser/{contributionDate}/{userId}
Buscar contribuições por data específica e ID de usuário.

HttpGet /api/Contribution/GetByPurchaseId/{purchaseId}
Buscar contribuições pelo ID da compra.

HttpGet /api/Contribution/GetByUser/{userId}
Buscar contribuições pelo ID do usuário.



Contato
Para mais informações, entre em contato pelo email: deividsonomedio@gmail.com


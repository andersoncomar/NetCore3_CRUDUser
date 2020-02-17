<div>
<img src="https://docs.microsoft.com/en-us/media/microsoft-logo-dark.png" height="50" alt="Swagger Logo" style="margin-right:20px;">
<img src="https://upload.wikimedia.org/wikipedia/commons/thumb/e/ee/.NET_Core_Logo.svg/1024px-.NET_Core_Logo.svg.png" height="100" alt=".NET Core" >
<img src="https://static1.smartbear.co/swagger/media/assets/images/swagger_logo.svg" height="50" alt="Swagger Logo">
</ div>

# Api CRUD de Usuários

API desenvolvida em .Net Core 3 para operações: Create, Read, Update e Delete persistindo em banco NoSql MongoDB.

- Utilizado Padrão Repository e Service para separação das responsabilidades.
- Foram criados 2 Projetos Class Libraries: Um contendo projeto de Data responsavél pela conexão e manipulação do Repositórios e, 
outro projeto de Business responsavél pelos Models, Notifications, Services e Interfaces.

- Utilizei o [AutoMapper](https://automapper.org) para auxílio de mapeamento entre as Models e as ViewModels. 

- Foi utilizado o [Swagger](https://swagger.io/) para documentação da API.

Publicado no Azure link [Projeto Api - Documentação](https://apirestcomar.azurewebsites.net/swagger/)

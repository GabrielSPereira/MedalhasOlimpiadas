# Medalhas Olímpicas 🏅
[<img src="https://i.imgur.com/0u0ZmMR.png" target="_blank">](https://medalhasolimpicas.azurewebsites.net/)
##  🏋️‍♀️Sobre
- **Medalhas Olímpicas** é uma plataforma de consulta e registro de medalhas e atletas olímpicos. Nela é possível cadastrar atletas especificando sua categoria e seu país, e então compará-los em um ranking por quantidades de medalhas.
- A classificação do ranking não é por número absoluto de medalhas, e sim por seu valor. Dessa forma, um país que tenha uma medalha de ouro e uma de prata está acima de um país que tenha uma de ouro e duas de bronze.
- Ao clicar em um país, é exibido o quadro de medalhas por modalidade.
## ## 💻Run this App
```
#clonando o repositório
$ git clone https://github.com/GabrielSPereira/MedalhasOlimpiadas.git
```
Primeiro precisamos rodar o medalhas.sql na IDE SQL EXPRESS, abrir o projeto pelo Visual Studio e acessar o arquivo BLL/BLLBase.cs para alterar a string de conexão. Após isso podemos executar o projeto através da tecla F5.
## 💻 Tecnologias
-   [C#](https://docs.microsoft.com/pt-br/dotnet/csharp/)
-   [ASP.NET MVC 5](https://docs.microsoft.com/pt-br/aspnet/mvc/overview/getting-started/introduction/getting-started)
-   [BootStrap](https://getbootstrap.com/)
-   [JQuery](https://api.jquery.com/jquery.ajax/)
-   [restcountries](https://restcountries.eu/)


#  Doc API MedalhasOlimpiadas


## Endpoints
### Home:
```
GET
https://medalhasolimpicas.azurewebsites.net/Atleta
```
Essa requisição retornará um XML da página principal


```
POST
https://medalhasolimpicas.azurewebsites.net/HomeFiltro
```
Espera os seguintes dados:
ddlModalidade : String
ddlIsMulher : String

Essa requisição retornará, de acordo com os filtros, um XML com o quadro de medalhas em ranking de países
ㅤ
```
GET
https://restcountries.eu/rest/v2/alpha/:sigla
```
Espera os seguintes dados:
sigla : Obrigatório

- Retorno em JSON, com os dados de cada país
ㅤ

**Detalhes**
```
GET
https://medalhasolimpicas.azurewebsites.net/Detalhes/:id
```
Espera os seguintes dados:
id : int : Obrigatório

Essa requisição retornará, de acordo com o id, um XML com o quadro de medalhas em ranking de modalidades de um país

- Chamando esse método, ele faz mais duas requisições 
ㅤ
```
POST
https://medalhasolimpicas.azurewebsites.net/DetalhesPopula/:id
```
Espera os seguintes dados:
id : int : Obrigatório
ㅤ
```
GET
https://restcountries.eu/rest/v2/alpha/:sigla
```
Espera os seguintes dados:
sigla : Obrigatório

- Retorno em JSON, com os dados de cada país
ㅤ
### Atleta:

```
GET
https://medalhasolimpicas.azurewebsites.net/Atleta
```
Essa requisição retornará um XML da página de Atleta
ㅤ
```
POST
https://medalhasolimpicas.azurewebsites.net/AtletaFiltro
```
Espera os seguintes dados:
txtNome : String 
ddlModalidade : String
ddlPais : String
ddlAtivo : String

Essa requisição retornará, de acordo com os filtros, um XML com a grid da tela de Atleta
ㅤ
```
POST
https://medalhasolimpicas.azurewebsites.net/AtletaAjaxForm/:id
```
Espera os seguintes dados:
id: int : Obrigatório

Essa requisição retornará, de acordo com o id, um XML com o form da tela de Atleta
ㅤ
```
POST
https://medalhasolimpicas.azurewebsites.net/AtletaSalvar
```
Espera os seguintes dados:
hdnIdAtleta: String
txtNome: String : Obrigatório
ddlModalidade : String : Obrigatório
ddlPais : String : Obrigatório

Essa requisição irá salvar o Atleta, de acordo com os dados, e retornará para a tela de Atleta
ㅤ
```
POST
https://medalhasolimpicas.azurewebsites.net/AtletaAjaxRemover/:id
```
Espera os seguintes dados:
id: int : Obrigatório

Essa requisição removerá o atleta através de seu id e retornará para tela de Atleta

ㅤ
### Medalhas:

```
GET
https://medalhasolimpicas.azurewebsites.net/Medalha
```
Essa requisição retornará um XML da página de atleta
ㅤ
```
POST
https://medalhasolimpicas.azurewebsites.net/AtletaFiltro
```
Espera os seguintes dados:
ddlTipoMedalha : String 
ddlAtleta : String
ddlAtivo : String

Essa requisição retornará, de acordo com os filtros, um XML com a grid da tela de Medalha
ㅤ
```
POST
https://medalhasolimpicas.azurewebsites.net/MedalhaAjaxForm/:id
```
Espera os seguintes dados:
id: int : Obrigatório

Essa requisição retornará, de acordo com o id, um XML com o form da tela de Medalha
ㅤ
```
POST
https://medalhasolimpicas.azurewebsites.net/MedalhaSalvar
```
Espera os seguintes dados:
hdnIdMedalha : String
dllAtleta : String : Obrigatório
dllTipoMedalha : String : Obrigatório

Essa requisição irá salvar a Medalha, de acordo com os dados, e retornará para a tela de Medalha
ㅤ
```
POST
https://medalhasolimpicas.azurewebsites.net/MedalhaAjaxRemover/:id
```
Espera os seguintes dados:
id: int : Obrigatório

Essa requisição removerá o atleta através de seu id e retornará para tela de Medalha

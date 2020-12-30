## Arquitetura de Automação utilizada no Projeto REST API

- Arquitetura Projeto
	- Linguagem		- [CSharp](https://docs.microsoft.com/pt-br/dotnet/csharp/ "CSharp")
	- Orquestrador de testes - [NUnit 3.12](https://github.com/nunit/nunit "NUnit 3.12")
	- Relatório de testes automatizados - [ExtentReports.Core 1.0.3](https://www.nuget.org/packages/ExtentReports.Core/)
	- Framework interação com API - [Rest Sharp 106.6.10](http://restsharp.org/ "RestSharp 106.6.10") 
	- Framework de desenvolvimento - [.Net Core](https://dotnet.microsoft.com/download/dotnet-core/3.1)
  - Framework Jenkins - [Jenkins](https://get.jenkins.io/war-stable/2.263.1/ "Jenkins 2.263.1")

**Arquitetura padrão By: Base2 Tecnologia**

Para facilitar o entendimento da arquitetura do projeto de testes automatizados, o template segue a padronização utilizada para Testes API pensado pelos arquitetos da base 2

![alt text](https://i.imgur.com/EXC4keZ.png)

  - Bases ("contem as bases para requisições REST e SOAP alem da base para os testes")
  - DBSteps ("Contem exemplo de uso de queries")
  - Helpers ("Contem metodos auxiliares para os projetos inclusive serializacao e deserializacao de jsons, entre outros")
  - Jsons ("Diretorio para armazenar os jsons utilizados nas requisições do projeto")
  - Queries ("Diretorio para armazenar as queries utilizadas no projeto")
  - Requests ("Diretorio para armazenar as requisições do projeto")
  - Tests ("Diretorio para armazenar os testes do projeto")

## Desafio Automacao REST API : Regras

- [x] 1) Implementar 50 scripts de testes que manipulem uma aplicação cuja interface é uma API
REST.

![alt text](https://i.imgur.com/BSGLaAz.png)

- [x] 2) Alguns scripts devem ler dados de uma planilha Excel para implementar Data-Driven.

![alt text](https://i.imgur.com/1BuWlxA.png) ![alt text](https://i.imgur.com/t0djf3K.png)

- [x] 3) Notem que 50 scripts podem cobrir mais de 50 casos de testes se usarmos Data-Driven. Em
outras palavras, implementar 50 CTs usando data-driven não é a mesma coisa que
implementar 50 scripts.

![alt text](https://i.imgur.com/PBcQea6.png)

- [x] 4) O projeto deve tratar autenticação. Exemplo: OAuth2.
As chamadas da API Mantis Bug Tracker devem ser autenticadas criando um token de API para o usuário que faz as chamadas e, em seguida, passando o token de API no cabeçalho 'Autorização'.

![alt text](https://i.imgur.com/N6NWBgt.png) ![alt text](https://i.imgur.com/w17EZ7A.png)

- [x] 5) Pelo menos um teste deve fazer a validação usando REGEX (Expressões Regulares).

![alt text](https://i.imgur.com/plPh0YT.png)

- [x] 6) Pelo menos um script deve usar código Groovy / Node.js ou outra linguagem para fazer
scripts.
![alt text]()

- [x] 7) O projeto deverá gerar um relatório de testes automaticamente.

![alt text](https://i.imgur.com/gI9wd0f.png)

- [x] 8) Implementar pelo menos dois ambientes (desenvolvimento / homologação)

![alt text](https://i.imgur.com/M2pOKwi.png)

- [x] 9) A massa de testes deve ser preparada neste projeto, seja com scripts carregando massa
nova no BD ou com restore de banco de dados. Os fluxos de teste foram desenvolvidos sob à logica do acrônimo comumente utilizado para as quatro operações básicas usadas em Banco de Dados Relacionais.
A ideia foi sempre manter ou criar dados para durante os testes.

![alt text](https://i.imgur.com/BC9c3Bh.png)

- [x] 10) Executar testes em paralelo. Pelo menos duas threads (25 testes cada).
Para a execução dos testes em paralelo pode ser adicionado junto a tag de "Test" a tag [Parallelizable]. E na classe AssemblyInfo.cs dentro de Properties foi adicionado a tag [assembly: LevelOfParallelism(2)] para dividir a quantidade de testes duranta a execução.

![alt text](https://i.imgur.com/icECO7B.png) ![alt text](https://i.imgur.com/x28pgIV.png)

- [x] 11) Testes deverão ser agendados pelo Azure DevOps, Jenkins, CircleCI, TFS ou outra
ferramenta de CI que preferir.
Os testes foram agendados pelo Jenkins, seguindo a configuração evidenciada neste repositorio com exemplo garimpado na web: 
[clique aqui](https://github.com/williammelquiades/jenkinsCICDStepByStep).
# SciensaAPI
1. PRE-REQUISITOS

Sistema Operacional
- Windows 7 SP 2+ ou superior
- Preferencialmente utilizar o Windows 10

IDE

- Visual Studio 2015 ou 2017
- atualização de ferramentas azure Cloud
- atualização do Service Fabric e Dot.net core

SDK

- Service Fabric SDK and tools

2.INSTALAÇÂO

- Instalar o Visual Studio 2017
- Executar o update para azure cloud
- Habilitar a execução de scripts do PowerShell
- Instalar o Service Fabric SDK
- Executar o PowerShell como administrador e rodar a linha de comando abaixo:
- Set-ExecutionPolicy -ExecutionPolicy Unrestricted -Force -Scope CurrentUser

3.PROJETO

- Baixar o projeto de https://github.com/DanielDaddato/SciensaAPI
- Executar o Visual Studio como Administrador
- Abrir a Solution SciensaAPI
- Verificar se o projeto SciensaAPI está como projeto Inicial
- Executar a solution(botão start ou tecla F5)
- O projeto vai criar um novo cluster no service fabric e instanciar a aplicação.
- A aplicação está iniciando em http://localhost:8080/ 
- Para testar as funcionalidades do projeto acessar http://localhost:8080/swagger/ 

4. REFERENCIAS
https://azure.microsoft.com/pt-br/services/service-fabric/


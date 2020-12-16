#   Newsletter
Programa responsável pelo controle e envio de email marketing para uma lista de emails cadastrados no banco de dados.

## Começando
Essas instruções farão com que você tenha uma cópia do projeto em execução na sua máquina local para fins de desenvolvimento e teste.
### Pré-requisitos
O que você vai precisar para a execução desse software:

* [Microsoft Visual Studio](https://www.visualstudio.microsoft.com)
* [Microsoft SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### Instalando os programas necessários
Para instalar o Microsoft Visual Studio, [clique aqui](https://docs.microsoft.com/en-us/visualstudio/install/install-visual-studio?view=vs-2019).

Para instalar o Microsoft SQL Server, [clique aqui](https://docs.microsoft.com/en-us/sql/database-engine/install-windows/install-sql-server?view=sql-server-ver15).

Para instalar o SQL Server Management Studio (SSMS), [clique aqui](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15).

## Configurações iniciais

### No SQL Management Studio
* Execute o SSMS, e realize o login com o usuário e senha do SQL Server.
* Clique no botão **Nova Consulta** e digite os comandos abaixo:
```SQL
CREATE DATABASE SUPLEMENTOS
GO
USE SUPLEMENTOS
CREATE TABLE tbl_Newsletter(
Id INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
Situacao VARCHAR(7) NOT NULL,
Nome VARCHAR(50) NOT NULL,
Email VARCHAR(50) NOT NULL UNIQUE,
)
```
* Clique no botão executar.
### No Visual Studio
* Execute o Visual Studio.
* Vá em Arquivo -> Abrir -> Projeto/Solução ou CTRL + SHIFT + O.
* Abra o arquivo solução **Newsletter_System_2.0.sln**
* Na Solution Explorer (Gerenciador de Soluções) no projeto **DAO**, abra o arquivo **Connection.cs**

Encontre esse trecho de código:
```c#

public string strConn = @"server = localhost; database = SUPLEMENTOS; user = MEU USUARIO; password = MINHA SENHA; MultipleActiveResultSets=True";

```
 * Altere **MEU USUARIO** e **MINHA SENHA** para o usuário e senha do SQL Server.
## Configurando o envio de email
 * No projeto **Newsletter_System_2.0**, abra o arquivo **NewsletterForm.cs**

Encontre esse trecho de código:
```c#
        txtEmail.Text = "EMAIL";
        txtSenha.Text = "SENHA";
```
 * Altere **EMAIL** para o email que será utilizado no envio da mensagem.
 * Altere **SENHA** para a senha do email.
 
Por padrão, o envio da mensagem está configurada para emails microsoft.

Se o **seu email não for microsoft**, siga esses passos:
* Encontre esse trecho de código:
```c#
        smtp.Host = "smtp.live.com";
        smtp.Port = 587;
```
Verifique o domínio do servidor do seu email:
host | porta
---------|--------
smtp.gmail.com | 587 ou 465
smtp.mail.yahoo.com | 995 ou 465

* Altere o **smtp.live.com** e **587** para o host e a porta do seu email.

Após essas configurações iniciais, **salve as alterações e execute o projeto**.

## Desdobramento e desenvolvimento
Esse programa controla e envia as mensagens para divulgação de marketing para os emails cadastrados no banco de dados.

## Desenvolvido com
* [Microsoft Visual Studio Enterprise 2019](https://visualstudio.microsoft.com/pt-br/vs/enterprise/)
* [Microsoft SQL Server Enterprise 2017](https://www.microsoft.com/en-us/sql-server/sql-server-2016)
* [.NET Framework](https://dotnet.microsoft.com)

### Versão
2.0

### Autor
* **Angelo Barroso**

### Licença
Este projeto está licenciado sob a licença MIT - Veja o arquivo [LICENSE](LICENSE) para detalhes.

CREATE TABLE [dbo].[ManualRegistration]
(
	[Id] INT NOT NULL IDENTITY (1, 1) PRIMARY KEY, 
    [Name ] NVARCHAR(50) NULL, 
    [Address] NVARCHAR(MAX) NULL, 
    [Phone] INT NULL
)

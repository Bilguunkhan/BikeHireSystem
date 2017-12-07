CREATE TABLE [dbo].[UsersAndBikes]
(
	[Id] INT NOT NULL PRIMARY KEY, 
	[Name] NVARCHAR(50) NULL, 
	[BikeModel] NVARCHAR(50) NULL, 
	[BikeId] INT NULL, 
	[StardDate] DATE NULL, 
	[EndDate] DATE NULL
)

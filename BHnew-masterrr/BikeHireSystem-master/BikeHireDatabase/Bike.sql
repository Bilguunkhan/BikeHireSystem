CREATE TABLE [dbo].[Bikes]
(
	[Bike Id] INT IDENTITY (1, 1) NOT NULL, 
    [Bike Model] NVARCHAR(50) NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    [Cost] MONEY NULL, 
    [Status] CHAR(10) NULL,
	PRIMARY KEY CLUSTERED ([Bike Id] ASC)
)


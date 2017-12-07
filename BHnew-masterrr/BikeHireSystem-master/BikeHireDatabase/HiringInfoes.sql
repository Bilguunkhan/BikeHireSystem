CREATE TABLE [dbo].[HiringInfoes] (
	[fullName]    NVARCHAR (50) NULL,
	[sDate]    NVARCHAR (50) NULL,
	[eDate]    NVARCHAR (50) NULL,
	[CardInfo] INT           NULL,
	[FbikeId]  INT           NOT NULL,
	CONSTRAINT [PK_HiringInfoes] PRIMARY KEY CLUSTERED ([FbikeId] ASC),
	CONSTRAINT [FK_dbo.HiringInfoes_dbo.Bikes] FOREIGN KEY ([FbikeId]) REFERENCES [dbo].[Bikes] ([Bike Id]) ON DELETE CASCADE
);
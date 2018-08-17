CREATE TABLE [dbo].[Income]
(
	
	[IncomeId] BIGINT IDENTITY(1,1) NOT NULL,	
	[ProductId] BIGINT not NULL,
	[SectorId]  BIGINT NOT NULL,
	[Amount]    FLOAT NOT NULL,
	[IncomeDate]   DATETIME NOT NULL,
	[BranchId]  BIGINT NOT NULL,
	[WeightSold]  FLOAT NOT NULL,
	[Deleted]	[bit] NULL,
	[CreatedBy] [nvarchar](128) NULL, 
    [UpdatedBy] [nvarchar](128) NULL,     
    [DeletedBy] [nvarchar](128) NULL,
	[CreatedOn]	[datetime] NULL,
	[TimeStamp]	[datetime] NOT NULL DEFAULT GETDATE(),	
	[DeletedOn]	[datetime] NULL,

    CONSTRAINT [PK_dbo.Income] PRIMARY KEY CLUSTERED 
(
	[IncomeId] ASC
),
CONSTRAINT [FK_Income_ProductId] FOREIGN KEY([ProductId]) REFERENCES [dbo].[Product](ProductId),
CONSTRAINT [FK_Income_BranchId] FOREIGN KEY([BranchId]) REFERENCES [dbo].[Branch](BranchId),
CONSTRAINT [FK_Income_SectorId] FOREIGN KEY([SectorId]) REFERENCES [dbo].[Sector](SectorId),
CONSTRAINT [FK_Income_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
CONSTRAINT [FK_Income_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
CONSTRAINT [FK_Income_DeletedBy] FOREIGN KEY ([DeletedBy]) REFERENCES [dbo].[AspNetUsers](Id),
)ON [PRIMARY]



CREATE TABLE [dbo].[Stock]
(
	
	[StockId] BIGINT IDENTITY(1,1) NOT NULL,	
	[SectorId] BIGINT not NULL,
	[BatchId] BIGINT NOT NULL,
	[BranchId]  BIGINT NOT NULL,
	[ProductId] BIGINT NOT NULL,
	[StoreId] BIGINT NOT NULL,
	[InOrOut]   BIT NOT NULL,
	[Deleted]	[bit] NULL,
	[CreatedBy] [nvarchar](128) NULL, 
    [UpdatedBy] [nvarchar](128) NULL,     
    [DeletedBy] [nvarchar](128) NULL,
	[CreatedOn]	[datetime] NULL,
	[TimeStamp]	[datetime] NOT NULL DEFAULT GETDATE(),	
	[DeletedOn]	[datetime] NULL,

    CONSTRAINT [PK_dbo.Stock] PRIMARY KEY CLUSTERED 
(
	[StockId] ASC
),
CONSTRAINT [FK_Stock_BranchId] FOREIGN KEY([BranchId]) REFERENCES [dbo].[Branch](BranchId),
CONSTRAINT [FK_Stock_BatchId] FOREIGN KEY([BatchId]) REFERENCES [dbo].[Batch](BatchId),
CONSTRAINT [FK_Stock_SectorId] FOREIGN KEY([SectorId]) REFERENCES [dbo].[Sector](SectorId),
CONSTRAINT [FK_Stock_ProductId] FOREIGN KEY([ProductId]) REFERENCES [dbo].[Product](ProductId),
CONSTRAINT [FK_Stock_StoreId] FOREIGN KEY([StoreId]) REFERENCES [dbo].[Store](StoreId),
CONSTRAINT [FK_Stock_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
CONSTRAINT [FK_Stock_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
CONSTRAINT [FK_Stock_DeletedBy] FOREIGN KEY ([DeletedBy]) REFERENCES [dbo].[AspNetUsers](Id),
)ON [PRIMARY]



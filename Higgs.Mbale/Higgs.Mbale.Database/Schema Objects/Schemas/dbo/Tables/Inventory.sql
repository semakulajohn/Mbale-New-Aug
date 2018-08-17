CREATE TABLE [dbo].[Inventory]
(
	[InventoryId] BIGINT IDENTITY(1,1) NOT NULL,	
	[ItemName] [nvarchar](128) not NULL,
	[Amount] FLOAT NOT NULL,
	[Description]    [nvarchar](max) NOT NULL,
	[BranchId]   BIGINT NOT NULL,
	[SectorId] BIGINT NOT NULL,
	[StoreId] BIGINT NOT NULL,
	[TransactionSubTypeId] BIGINT NOT NULL,
	[PurchaseDate]  [datetime] NOT NULL,
	[Deleted]	[bit] NULL,
	[CreatedBy] [nvarchar](128) NULL, 
    [UpdatedBy] [nvarchar](128) NULL,     
    [DeletedBy] [nvarchar](128) NULL,
	[CreatedOn]	[datetime] NULL,
	[TimeStamp]	[datetime] NOT NULL DEFAULT GETDATE(),	
	[DeletedOn]	[datetime] NULL,

    CONSTRAINT [PK_dbo.Inventory] PRIMARY KEY CLUSTERED 
(
	[InventoryId] ASC
),
CONSTRAINT [FK_Inventory_SectorId] FOREIGN KEY ([SectorId]) REFERENCES [dbo].[Sector](SectorId),
CONSTRAINT [FK_Inventory_BranchId] FOREIGN KEY ([BranchId]) REFERENCES [dbo].[Branch](BranchId),
CONSTRAINT [FK_Inventory_TransactionSubTypeId] FOREIGN KEY ([TransactionSubTypeId]) REFERENCES [dbo].[TransactionSubType](TransactionSubTypeId),
CONSTRAINT [FK_Inventory_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
CONSTRAINT [FK_Inventory_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
CONSTRAINT [FK_Inventory_DeletedBy] FOREIGN KEY ([DeletedBy]) REFERENCES [dbo].[AspNetUsers](Id),
)ON [PRIMARY]


CREATE TABLE [dbo].[StoreStock]
(
	[StoreStockId]  BIGINT IDENTITY(1,1) NOT NULL,	
	[StockId] BIGINT NOT NULL,
	[ProductId] BIGINT NOT NULL,
	[StartStock] FLOAT NOT NULL,
	[Quantity]   FLOAT NOT NULL,
	[StockBalance] FLOAT NOT NULL,
	[StoreId]	BIGINT NOT NULL,
	[BranchId] BIGINT NOT NULL,
	[SectorId] BIGINT NOT NULL,
	[TimeStamp] DATETIME NOT NULL,
 CONSTRAINT [PK_dbo.StoreStock] PRIMARY KEY CLUSTERED 
(
	[StoreStockId] ASC
),
CONSTRAINT [FK_StoreStock_BranchId] FOREIGN KEY([BranchId]) REFERENCES [dbo].[Branch](BranchId),
CONSTRAINT [FK_StoreStock_StoreId] FOREIGN KEY([StoreId]) REFERENCES [dbo].[Store](StoreId),
CONSTRAINT [Fk_StoreStock_StockId] FOREIGN KEY([StockId]) REFERENCES [dbo].[Stock](StockId),
CONSTRAINT [FK_StoreStock_SectorId] FOREIGN KEY([SectorId]) REFERENCES [dbo].[Sector](SectorId),
CONSTRAINT [FK_StoreStock_ProductId] FOREIGN KEY([ProductId]) REFERENCES [dbo].[Product](ProductId),

)ON [PRIMARY]



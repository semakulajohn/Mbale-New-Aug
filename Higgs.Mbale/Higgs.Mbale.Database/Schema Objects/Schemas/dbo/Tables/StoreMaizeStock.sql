CREATE TABLE [dbo].[StoreMaizeStock]
(
	[StoreMaizeStockId]  BIGINT IDENTITY(1,1) NOT NULL,	
	[StockId] BIGINT NOT NULL,
	[SupplyId] BIGINT NOT NULL,
	[StartStock] FLOAT NOT NULL,
	[Quantity]   FLOAT NOT NULL,
	[StockBalance] FLOAT NOT NULL,
	[StoreId]	BIGINT NOT NULL,
	[BranchId] BIGINT NOT NULL,
	[SectorId] BIGINT NOT NULL,
	[TimeStamp] DATETIME NOT NULL,
 CONSTRAINT [PK_dbo.StoreMaizeStock] PRIMARY KEY CLUSTERED 
(
	[StoreMaizeStockId] ASC
),
CONSTRAINT [FK_StoreMaizeStock_BranchId] FOREIGN KEY([BranchId]) REFERENCES [dbo].[Branch](BranchId),
CONSTRAINT [FK_StoreMaizeStock_StoreId] FOREIGN KEY([StoreId]) REFERENCES [dbo].[Store](StoreId),
CONSTRAINT [Fk_StoreMaizeStock_StockId] FOREIGN KEY([StockId]) REFERENCES [dbo].[Stock](StockId),
CONSTRAINT [FK_StoreMaizeStock_SectorId] FOREIGN KEY([SectorId]) REFERENCES [dbo].[Sector](SectorId),
CONSTRAINT [FK_StoreMaizeStock_SupplyId] FOREIGN KEY([SupplyId]) REFERENCES [dbo].[Supply](SupplyId),

)ON [PRIMARY]



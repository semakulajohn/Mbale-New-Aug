CREATE TABLE [dbo].[StockSupply]
(
	[StockId] BIGINT NOT NULL,
	[SupplyId] BIGINT NOT NULL,
	[Quantity]   FLOAT NOT NULL,
	[TimeStamp] DATETIME NOT NULL,
 CONSTRAINT [PK_dbo.StockSupply] PRIMARY KEY CLUSTERED 
(
	[StockId] ASC,
	[SupplyId] ASC
) 
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[StockSupply]  ADD  CONSTRAINT [FK_dbo.StockSupply_dbo.Stock_StockId] FOREIGN KEY([StockId])
REFERENCES [dbo].[Stock] ([StockId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[StockSupply] CHECK CONSTRAINT [FK_dbo.StockSupply_dbo.Stock_StockId]
GO

ALTER TABLE [dbo].[StockSupply]  ADD  CONSTRAINT [FK_dbo.StockSupply_dbo.Supply_SupplyId] FOREIGN KEY([SupplyId])
REFERENCES  [dbo].[Supply] ([SupplyId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[StockSupply] CHECK CONSTRAINT [FK_dbo.StockSupply_dbo.Supply_SupplyId]
GO

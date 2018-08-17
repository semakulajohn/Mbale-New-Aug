CREATE TABLE [dbo].[StockProduct]
(
	[StockId] BIGINT NOT NULL,
	[ProductId] BIGINT NOT NULL,
	[Quantity]   FLOAT NOT NULL,
	[TimeStamp] DATETIME NOT NULL,
 CONSTRAINT [PK_dbo.StockProduct] PRIMARY KEY CLUSTERED 
(
	[StockId] ASC,
	[ProductId] ASC
) 
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[StockProduct]  ADD  CONSTRAINT [FK_dbo.StockProduct_dbo.Stock_StockId] FOREIGN KEY([StockId])
REFERENCES [dbo].[Stock] ([StockId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[StockProduct] CHECK CONSTRAINT [FK_dbo.StockProduct_dbo.Stock_StockId]
GO

ALTER TABLE [dbo].[StockProduct]  ADD  CONSTRAINT [FK_dbo.StockProduct_dbo.Product_ProductId] FOREIGN KEY([ProductId])
REFERENCES  [dbo].[Product] ([ProductId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[StockProduct] CHECK CONSTRAINT [FK_dbo.StockProduct_dbo.Product_ProductId]
GO

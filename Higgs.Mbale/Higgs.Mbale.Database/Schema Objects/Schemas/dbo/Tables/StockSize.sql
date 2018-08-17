CREATE TABLE [dbo].[StockSize]
(
	[StockId] BIGINT NOT NULL,
	[SizeId] BIGINT NOT NULL,
 CONSTRAINT [PK_dbo.StockSize] PRIMARY KEY CLUSTERED 
(
	[StockId] ASC,
	[SizeId] ASC
) 
) ON [PRIMARY]

GO


ALTER TABLE [dbo].[StockSize]  ADD  CONSTRAINT [FK_dbo.StockSize_dbo.Size_SizeId] FOREIGN KEY([SizeId])
REFERENCES  [dbo].[Size] ([SizeId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[StockSize] CHECK CONSTRAINT [FK_dbo.StockSize_dbo.Size_SizeId]
GO

ALTER TABLE [dbo].[StockSize]  ADD  CONSTRAINT [FK_dbo.StockSize_dbo.Stock_StockId] FOREIGN KEY([StockId])
REFERENCES [dbo].[Stock] ([StockId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[StockSize] CHECK CONSTRAINT [FK_dbo.StockSize_dbo.Stock_StockId]
GO




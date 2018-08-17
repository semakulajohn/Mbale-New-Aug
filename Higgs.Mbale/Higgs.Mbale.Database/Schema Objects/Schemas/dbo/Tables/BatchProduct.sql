CREATE TABLE [dbo].[BatchProduct]
(
	[BatchId] BIGINT NOT NULL,
	[ProductId] BIGINT NOT NULL,
	[OutPut]   FLOAT NOT NULL,
	[TimeStamp] DATETIME NOT NULL,
 CONSTRAINT [PK_dbo.BatchProduct] PRIMARY KEY CLUSTERED 
(
	[BatchId] ASC,
	[ProductId] ASC
) 
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[BatchProduct]  ADD  CONSTRAINT [FK_dbo.BatchProduct_dbo.Batch_BatchId] FOREIGN KEY([BatchId])
REFERENCES [dbo].[Batch] ([BatchId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[BatchProduct] CHECK CONSTRAINT [FK_dbo.BatchProduct_dbo.Batch_BatchId]
GO

ALTER TABLE [dbo].[BatchProduct]  ADD  CONSTRAINT [FK_dbo.BatchProduct_dbo.Product_ProductId] FOREIGN KEY([ProductId])
REFERENCES  [dbo].[Product] ([ProductId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[BatchProduct] CHECK CONSTRAINT [FK_dbo.BatchProduct_dbo.Product_ProductId]
GO
CREATE TABLE [dbo].[OrderSize]
(

	[OrderId] BIGINT NOT NULL,
	[SizeId] BIGINT NOT NULL,
	[Quantity] FLOAT NOT NULL,
	[TimeStamp] DATETIME NOT NULL,
 CONSTRAINT [PK_dbo.OrderSize] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC,
	[SizeId] ASC
) 
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[OrderSize]  ADD  CONSTRAINT [FK_dbo.OrderSize_dbo.Order_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([OrderId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[OrderSize] CHECK CONSTRAINT [FK_dbo.OrderSize_dbo.Order_OrderId]
GO

ALTER TABLE [dbo].[OrderSize]  ADD  CONSTRAINT [FK_dbo.OrderSize_dbo.Size_SizeId] FOREIGN KEY([SizeId])
REFERENCES  [dbo].[Size] ([SizeId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[OrderSize] CHECK CONSTRAINT [FK_dbo.OrderSize_dbo.Size_SizeId]
GO

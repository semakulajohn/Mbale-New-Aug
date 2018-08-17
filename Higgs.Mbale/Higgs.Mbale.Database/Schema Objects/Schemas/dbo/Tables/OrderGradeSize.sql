CREATE TABLE [dbo].[OrderGradeSize]
(
	[OrderId] BIGINT NOT NULL,
	[GradeId] BIGINT NOT NULL,
	[SizeId] BIGINT NOT NULL,
	[Quantity] FLOAT NOT NULL,
	[TimeStamp] DATETIME NOT NULL,
 CONSTRAINT [PK_dbo.OrderGradeSize] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC,
	[GradeId] ASC,
	[SizeId] ASC
) 
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[OrderGradeSize]  ADD  CONSTRAINT [FK_dbo.OrderGradeSize_dbo.Order_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([OrderId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[OrderGradeSize] CHECK CONSTRAINT [FK_dbo.OrderGradeSize_dbo.Order_OrderId]
GO

ALTER TABLE [dbo].[OrderGradeSize]  ADD  CONSTRAINT [FK_dbo.OrderGradeSize_dbo.Size_SizeId] FOREIGN KEY([SizeId])
REFERENCES  [dbo].[Size] ([SizeId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[OrderGradeSize] CHECK CONSTRAINT [FK_dbo.OrderGradeSize_dbo.Size_SizeId]
GO


ALTER TABLE [dbo].[OrderGradeSize]  ADD  CONSTRAINT [FK_dbo.OrderGradeSize_dbo.Grade_GradeId] FOREIGN KEY([GradeId])
REFERENCES  [dbo].[Grade] ([GradeId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[OrderGradeSize] CHECK CONSTRAINT [FK_dbo.OrderGradeSize_dbo.Grade_GradeId]
GO


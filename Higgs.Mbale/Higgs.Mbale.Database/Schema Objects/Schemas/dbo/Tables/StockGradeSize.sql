CREATE TABLE [dbo].[StockGradeSize]
(
	[StockId] BIGINT NOT NULL,
	[GradeId] BIGINT NOT NULL,
	[SizeId] BIGINT NOT NULL,
	[Quantity] FLOAT NOT NULL,
	[TimeStamp] DATETIME NOT NULL,
 CONSTRAINT [PK_dbo.StockGradeSize] PRIMARY KEY CLUSTERED 
(
	[StockId] ASC,
	[GradeId] ASC,
	[SizeId] ASC
) 
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[StockGradeSize]  ADD  CONSTRAINT [FK_dbo.StockGradeSize_dbo.Stock_StockId] FOREIGN KEY([StockId])
REFERENCES [dbo].[Stock] ([StockId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[StockGradeSize] CHECK CONSTRAINT [FK_dbo.StockGradeSize_dbo.Stock_StockId]
GO

ALTER TABLE [dbo].[StockGradeSize]  ADD  CONSTRAINT [FK_dbo.StockGradeSize_dbo.Grade_GradeId] FOREIGN KEY([GradeId])
REFERENCES  [dbo].[Grade] ([GradeId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[StockGradeSize] CHECK CONSTRAINT [FK_dbo.StockGradeSize_dbo.Grade_GradeId]
GO

ALTER TABLE [dbo].[StockGradeSize]  ADD  CONSTRAINT [FK_dbo.StockGradeSize_dbo.Size_SizeId] FOREIGN KEY([SizeId])
REFERENCES  [dbo].[Size] ([SizeId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[StockGradeSize] CHECK CONSTRAINT [FK_dbo.StockGradeSize_dbo.Size_SizeId]
GO

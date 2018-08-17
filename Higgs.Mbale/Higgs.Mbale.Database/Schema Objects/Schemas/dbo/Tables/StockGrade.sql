CREATE TABLE [dbo].[StockGrade]
(
	[StockId] BIGINT NOT NULL,
	[GradeId] BIGINT NOT NULL,
	[TimeStamp] DATETIME NOT NULL,
 CONSTRAINT [PK_dbo.StockGrade] PRIMARY KEY CLUSTERED 
(
	[StockId] ASC,
	[GradeId] ASC
) 
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[StockGrade]  ADD  CONSTRAINT [FK_dbo.StockGrade_dbo.Stock_StockId] FOREIGN KEY([StockId])
REFERENCES [dbo].[Stock] ([StockId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[StockGrade] CHECK CONSTRAINT [FK_dbo.StockGrade_dbo.Stock_StockId]
GO

ALTER TABLE [dbo].[StockGrade]  ADD  CONSTRAINT [FK_dbo.StockGrade_dbo.Grade_GradeId] FOREIGN KEY([GradeId])
REFERENCES  [dbo].[Grade] ([GradeId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[StockGrade] CHECK CONSTRAINT [FK_dbo.StockGrade_dbo.Grade_GradeId]
GO

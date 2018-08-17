CREATE TABLE [dbo].[StoreGradeSize]
(
	[GradeId] BIGINT NOT NULL,
	[StoreId] BIGINT NOT NULL,
	[SizeId] BIGINT NOT NULL,
	[Quantity] FLOAT NOT NULL,
	[TimeStamp] DATETIME NOT NULL,
 CONSTRAINT [PK_dbo.StoreStockGradeSize] PRIMARY KEY CLUSTERED 
(
	
	[GradeId] ASC,
	[SizeId] ASC
) 
) ON [PRIMARY]

GO


ALTER TABLE [dbo].[StoreGradeSize]  ADD  CONSTRAINT [FK_dbo.StoreGradeSize_dbo.Grade_GradeId] FOREIGN KEY([GradeId])
REFERENCES  [dbo].[Grade] ([GradeId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[StoreGradeSize] CHECK CONSTRAINT [FK_dbo.StoreGradeSize_dbo.Grade_GradeId]
GO

ALTER TABLE [dbo].[StoreGradeSize]  ADD  CONSTRAINT [FK_dbo.StoreGradeSize_dbo.Size_SizeId] FOREIGN KEY([SizeId])
REFERENCES  [dbo].[Size] ([SizeId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[StoreGradeSize] CHECK CONSTRAINT [FK_dbo.StoreGradeSize_dbo.Size_SizeId]
GO


ALTER TABLE [dbo].[StoreGradeSize]  ADD  CONSTRAINT [FK_dbo.StoreGradeSize_dbo.Store_StoreId] FOREIGN KEY([StoreId])
REFERENCES  [dbo].[Store] ([StoreId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[StoreGradeSize] CHECK CONSTRAINT [FK_dbo.StoreGradeSize_dbo.Size_SizeId]
GO
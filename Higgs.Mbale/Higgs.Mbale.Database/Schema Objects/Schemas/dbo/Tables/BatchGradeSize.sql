CREATE TABLE [dbo].[BatchGradeSize]
(
	[BatchOutPutId] BIGINT NOT NULL,
	[GradeId] BIGINT NOT NULL,
	[SizeId] BIGINT NOT NULL,
	[Quantity] FLOAT NOT NULL,
	[TimeStamp] DATETIME NOT NULL,
 CONSTRAINT [PK_dbo.BatchGrade] PRIMARY KEY CLUSTERED 
(
	[BatchOutPutId] ASC,
	[GradeId] ASC,
	[SizeId] ASC
) 
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[BatchGradeSize]  ADD  CONSTRAINT [FK_dbo.BatchGradeSize_dbo.BatchOutPut_BatchId] FOREIGN KEY([BatchOutPutId])
REFERENCES [dbo].[BatchOutPut] ([BatchOutPutId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[BatchGradeSize] CHECK CONSTRAINT [FK_dbo.BatchGradeSize_dbo.BatchOutPut_BatchId]
GO

ALTER TABLE [dbo].[BatchGradeSize]  ADD  CONSTRAINT [FK_dbo.BatchGradeSize_dbo.Grade_GradeId] FOREIGN KEY([GradeId])
REFERENCES  [dbo].[Grade] ([GradeId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[BatchGradeSize] CHECK CONSTRAINT [FK_dbo.BatchGradeSize_dbo.Grade_GradeId]
GO

ALTER TABLE [dbo].[BatchGradeSize]  ADD  CONSTRAINT [FK_dbo.BatchGradeSize_dbo.Size_SizeId] FOREIGN KEY([SizeId])
REFERENCES  [dbo].[Size] ([SizeId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[BatchGradeSize] CHECK CONSTRAINT [FK_dbo.BatchGradeSize_dbo.Size_SizeId]
GO
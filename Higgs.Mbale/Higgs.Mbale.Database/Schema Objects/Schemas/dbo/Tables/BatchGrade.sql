CREATE TABLE [dbo].[BatchGrade]
(
	[BatchId] BIGINT NOT NULL,
	[GradeId] BIGINT NOT NULL,
	[TimeStamp] DATETIME NOT NULL,
 CONSTRAINT [PK_dbo.BatchxGrade] PRIMARY KEY CLUSTERED 
(
	[BatchId] ASC,
	[GradeId] ASC
) 
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[BatchGrade]  ADD  CONSTRAINT [FK_dbo.BatchGrade_dbo.Batch_BatchId] FOREIGN KEY([BatchId])
REFERENCES [dbo].[Batch] ([BatchId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[BatchGrade] CHECK CONSTRAINT [FK_dbo.BatchGrade_dbo.Batch_BatchId]
GO

ALTER TABLE [dbo].[BatchGrade]  ADD  CONSTRAINT [FK_dbo.BatchGrade_dbo.Grade_GradeId] FOREIGN KEY([GradeId])
REFERENCES  [dbo].[Grade] ([GradeId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[BatchGrade] CHECK CONSTRAINT [FK_dbo.BatchGrade_dbo.Grade_GradeId]
GO
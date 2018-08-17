CREATE TABLE [dbo].[OrderGrade]
(
	[OrderId] BIGINT NOT NULL,
	[GradeId] BIGINT NOT NULL,
	[TimeStamp] DATETIME NOT NULL,
 CONSTRAINT [PK_dbo.OrderGrade] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC,
	[GradeId] ASC
) 
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[OrderGrade]  ADD  CONSTRAINT [FK_dbo.OrderGrade_dbo.Order_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([OrderId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[OrderGrade] CHECK CONSTRAINT [FK_dbo.OrderGrade_dbo.Order_OrderId]
GO

ALTER TABLE [dbo].[OrderGrade]  ADD  CONSTRAINT [FK_dbo.OrderGrade_dbo.Grade_GradeId] FOREIGN KEY([GradeId])
REFERENCES  [dbo].[Grade] ([GradeId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[OrderGrade] CHECK CONSTRAINT [FK_dbo.OrderGrade_dbo.Grade_GradeId]
GO

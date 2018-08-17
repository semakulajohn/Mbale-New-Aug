CREATE TABLE [dbo].[BatchSupply]
(
	[BatchId] BIGINT NOT NULL,
	[SupplyId] BIGINT NOT NULL,
	[Quantity]   [float]   NOT NULL,
	[CreatedOn]  [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.BatchSupply] PRIMARY KEY CLUSTERED 
(
	[BatchId] ASC,
	[SupplyId] ASC
) 
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[BatchSupply]  ADD  CONSTRAINT [FK_dbo.BatchSupply_dbo.Batch_BatchId] FOREIGN KEY([BatchId])
REFERENCES [dbo].[Batch] ([BatchId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[BatchSupply] CHECK CONSTRAINT [FK_dbo.BatchSupply_dbo.Batch_BatchId]
GO

ALTER TABLE [dbo].[BatchSupply]  ADD  CONSTRAINT [FK_dbo.BatchSupply_dbo.Supply_SupplyId] FOREIGN KEY([SupplyId])
REFERENCES  [dbo].[Supply] ([SupplyId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[BatchSupply] CHECK CONSTRAINT [FK_dbo.BatchSupply_dbo.Supply_SupplyId]
GO

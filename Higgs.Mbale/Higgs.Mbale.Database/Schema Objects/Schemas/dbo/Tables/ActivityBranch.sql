CREATE TABLE [dbo].[ActivityBranch]
(
	[ActivityId] BIGINT NOT NULL,
	[BranchId] BIGINT NOT NULL,
	[TimeStamp] datetime NOT NULL DEFAULT GETDATE(),

 CONSTRAINT [PK_dbo.ActivityBranch] PRIMARY KEY CLUSTERED 
(
	[BranchId] ASC,
	[ActivityId] ASC
) 
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ActivityBranch]  ADD  CONSTRAINT [FK_dbo.ActivityBranch_dbo.Branch_BranchId] FOREIGN KEY([BranchId])
REFERENCES [dbo].[Branch] ([BranchId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[ActivityBranch] CHECK CONSTRAINT [FK_dbo.ActivityBranch_dbo.Branch_BranchId]
GO

ALTER TABLE [dbo].[ActivityBranch]  ADD  CONSTRAINT [FK_dbo.ActivityBranch_dbo.Activity_ActivityId] FOREIGN KEY([ActivityId])
REFERENCES  [dbo].[Activity] ([ActivityId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[ActivityBranch] CHECK CONSTRAINT [FK_dbo.ActivityBranch_dbo.Activity_ActivityId]
GO


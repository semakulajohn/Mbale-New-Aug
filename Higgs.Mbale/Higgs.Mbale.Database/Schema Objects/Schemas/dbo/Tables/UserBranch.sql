CREATE TABLE [dbo].[UserBranch]
(
	[UserId] [nvarchar](128) NOT NULL,
	[BranchId] BIGINT NOT NULL,
	[TimeStamp] datetime not null,
 CONSTRAINT [PK_dbo.UserBranch] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[BranchId] ASC
) 
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[UserBranch]  ADD  CONSTRAINT [FK_dbo.UserBranch_dbo.Branch_BranchId] FOREIGN KEY([BranchId])
REFERENCES [dbo].[Branch] ([BranchId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[UserBranch] CHECK CONSTRAINT [FK_dbo.UserBranch_dbo.Branch_BranchId]
GO

ALTER TABLE [dbo].[UserBranch]  ADD  CONSTRAINT [FK_dbo.UserBranch_dbo.AspNetUser_Id] FOREIGN KEY([UserId])
REFERENCES  [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[UserBranch] CHECK CONSTRAINT [FK_dbo.UserBranch_dbo.AspNetUser_Id]
GO
CREATE TABLE [dbo].[UserBranchManager]
(
	[BranchId] BIGINT NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[TimeStamp] DATETIME NOT NULL,
 CONSTRAINT [PK_dbo.UserBranchManager] PRIMARY KEY CLUSTERED 
(
	[BranchId] ASC,
	[UserId] ASC
) 
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[UserBranchManager]  ADD  CONSTRAINT [FK_dbo.UserBranchManager_dbo.Branch_BranchId] FOREIGN KEY([BranchId])
REFERENCES [dbo].[Branch] ([BranchId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[UserBranchManager] CHECK CONSTRAINT [FK_dbo.UserBranchManager_dbo.Branch_BranchId]
GO

ALTER TABLE [dbo].[UserBranchManager]  ADD  CONSTRAINT [FK_dbo.UserBranchManager_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES  [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[UserBranchManager] CHECK CONSTRAINT [FK_dbo.UserBranchManager_dbo.AspNetUsers_UserId]
GO
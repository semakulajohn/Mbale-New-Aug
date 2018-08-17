CREATE TABLE [dbo].[BranchSector]
(
	[BranchId] BIGINT NOT NULL,
	[SectorId] BIGINT NOT NULL,
 CONSTRAINT [PK_dbo.BranchSector] PRIMARY KEY CLUSTERED 
(
	[BranchId] ASC,
	[SectorId] ASC
) 
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[BranchSector]  ADD  CONSTRAINT [FK_dbo.BranchSector_dbo.Branch_BranchId] FOREIGN KEY([BranchId])
REFERENCES [dbo].[Branch] ([BranchId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[BranchSector] CHECK CONSTRAINT [FK_dbo.BranchSector_dbo.Branch_BranchId]
GO

ALTER TABLE [dbo].[BranchSector]  ADD  CONSTRAINT [FK_dbo.BranchSector_dbo.Sector_SectorId] FOREIGN KEY([SectorId])
REFERENCES  [dbo].[Sector] ([SectorId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[BranchSector] CHECK CONSTRAINT [FK_dbo.BranchSector_dbo.Sector_SectorId]
GO


CREATE TABLE [dbo].[Creditor]
(
	[CreditorId] BIGINT IDENTITY(1,1) NOT NULL,	
	[AspNetUserId] [nvarchar](128)  NULL,
	[CasualWorkerId] BIGINT NULL,
	[Amount] FLOAT NOT NULL,
	[Action]     BIT NOT NULL,
	[BranchId]  BIGINT NOT NULL,
	[SectorId] BIGINT NOT NULL,
	[Deleted]	[bit] NULL,
	[CreatedBy] [nvarchar](128) NULL, 
    [UpdatedBy] [nvarchar](128) NULL,     
    [DeletedBy] [nvarchar](128) NULL,
	[CreatedOn]	[datetime] NOT NULL,
	[TimeStamp]	[datetime] NOT NULL DEFAULT GETDATE(),	
	[DeletedOn]	[datetime] NULL,

    CONSTRAINT [PK_dbo.Creditor] PRIMARY KEY CLUSTERED 
(
	[CreditorId] ASC
),
CONSTRAINT [FK_Creditor_BranchId] FOREIGN KEY([BranchId]) REFERENCES [dbo].[Branch](BranchId),
CONSTRAINT [FK_Creditor_SectorId] FOREIGN KEY([SectorId]) REFERENCES [dbo].[Sector](SectorId),
CONSTRAINT [FK_Creditor_AspNetUserId] FOREIGN KEY([AspNetUserId]) REFERENCES [dbo].[AspNetUsers](Id),
CONSTRAINT [FK_Creditor_CasualWorkerId] FOREIGN KEY([CasualWorkerId]) REFERENCES [dbo].[CasualWorker](CasualWorkerId),
CONSTRAINT [FK_Creditor_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
CONSTRAINT [FK_Creditor_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
CONSTRAINT [FK_Creditor_DeletedBy] FOREIGN KEY ([DeletedBy]) REFERENCES [dbo].[AspNetUsers](Id),
)ON [PRIMARY]

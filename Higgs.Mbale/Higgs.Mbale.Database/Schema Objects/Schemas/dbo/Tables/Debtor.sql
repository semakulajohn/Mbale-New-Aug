CREATE TABLE [dbo].[Debtor]
(
	
	[DebtorId] BIGINT IDENTITY(1,1) NOT NULL,	
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

    CONSTRAINT [PK_dbo.Debtor] PRIMARY KEY CLUSTERED 
(
	[DebtorId] ASC
),
CONSTRAINT [FK_Debtor_BranchId] FOREIGN KEY([BranchId]) REFERENCES [dbo].[Branch](BranchId),
CONSTRAINT [FK_Debtor_SectorId] FOREIGN KEY([SectorId]) REFERENCES [dbo].[Sector](SectorId),
CONSTRAINT [FK_Debtor_AspNetUserId] FOREIGN KEY([AspNetUserId]) REFERENCES [dbo].[AspNetUsers](Id),
CONSTRAINT [FK_Debtor_CasualWorkerId] FOREIGN KEY([CasualWorkerId]) REFERENCES [dbo].[CasualWorker](CasualWorkerId),
CONSTRAINT [FK_Debtor_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
CONSTRAINT [FK_Debtor_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
CONSTRAINT [FK_Debtor_DeletedBy] FOREIGN KEY ([DeletedBy]) REFERENCES [dbo].[AspNetUsers](Id),
)ON [PRIMARY]

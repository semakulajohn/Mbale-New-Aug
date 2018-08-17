CREATE TABLE [dbo].[MachineRepair]
(
	[MachineRepairId] BIGINT IDENTITY(1,1) NOT NULL,	
	[NameOfRepair] [nvarchar](128) not NULL,
	[Amount] FLOAT NOT NULL,
	[DateRepaired]	[datetime] NOT NULL,
	[TransactionSubTypeId] BIGINT NOT NULL,
	[SectorId] BIGINT NOT NULL,
	[BatchId] BIGINT NOT NULL,
	[Description] [nvarchar](MAX) NULL,
	[BranchId]   BIGINT NOT NULL,
	[Deleted]	[bit] NULL,
	[CreatedBy] [nvarchar](128) NULL, 
    [UpdatedBy] [nvarchar](128) NULL,     
    [DeletedBy] [nvarchar](128) NULL,
	[CreatedOn]	[datetime] NULL,
	[TimeStamp]	[datetime] NOT NULL DEFAULT GETDATE(),	
	[DeletedOn]	[datetime] NULL,

    CONSTRAINT [PK_dbo.MachineRepair] PRIMARY KEY CLUSTERED 
(
	[MachineRepairId] ASC
),

CONSTRAINT [FK_MachineRepair_BranchId] FOREIGN KEY([BranchId]) REFERENCES [dbo].[Branch](BranchId),
CONSTRAINT [FK_MachineRepair_SectorId] FOREIGN KEY([SectorId]) REFERENCES [dbo].[Sector](SectorId),
CONSTRAINT [FK_MachineRepair_BatchId] FOREIGN KEY([BatchId]) REFERENCES [dbo].[Batch](BatchId),
CONSTRAINT [FK_MachineRepair_TransactionSubTypeId] FOREIGN KEY([TransactionSubTypeId]) REFERENCES [dbo].[TransactionSubType](TransactionSubTypeId),
CONSTRAINT [FK_MachineRepair_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
CONSTRAINT [FK_MachineRepair_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
CONSTRAINT [FK_MachineRepair_DeletedBy] FOREIGN KEY ([DeletedBy]) REFERENCES [dbo].[AspNetUsers](Id),
)ON [PRIMARY]


CREATE TABLE [dbo].[Transaction]
(
	[TransactionId] BIGINT IDENTITY(1,1) NOT NULL,	
	[BranchId]  BIGINT NOT NULL,
	[SectorId] BIGINT NOT NULL,
	[Amount] FLOAT NOT NULL,
	[TransactionTypeId] BIGINT NOT NULL,
	[TransactionSubTypeId] BIGINT NOT NULL,
	[Deleted]	[bit] NULL,
	[CreatedBy] [nvarchar](128) NULL, 
    [UpdatedBy] [nvarchar](128) NULL,     
    [DeletedBy] [nvarchar](128) NULL,
	[CreatedOn]	[datetime] NOT NULL,
	[TimeStamp]	[datetime] NOT NULL DEFAULT GETDATE(),	
	[DeletedOn]	[datetime] NULL,

    CONSTRAINT [PK_dbo.Transaction] PRIMARY KEY CLUSTERED 
(
	[TransactionId] ASC
),
CONSTRAINT [FK_Transaction_BranchId] FOREIGN KEY([BranchId]) REFERENCES [dbo].[Branch](BranchId),
CONSTRAINT [FK_Transaction_TransactionTypeId] FOREIGN KEY([TransactionTypeId]) REFERENCES [dbo].[TransactionType](TransactionTypeId),
CONSTRAINT [FK_Transaction_TransactionSubTypeId] FOREIGN KEY([TransactionSubTypeId]) REFERENCES [dbo].[TransactionSubType](TransactionSubTypeId),
CONSTRAINT [FK_Transaction_SectorId] FOREIGN KEY([SectorId]) REFERENCES [dbo].[Sector](SectorId),
CONSTRAINT [FK_Transaction_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
CONSTRAINT [FK_Transaction_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
CONSTRAINT [FK_Transaction_DeletedBy] FOREIGN KEY ([DeletedBy]) REFERENCES [dbo].[AspNetUsers](Id),
)ON [PRIMARY]



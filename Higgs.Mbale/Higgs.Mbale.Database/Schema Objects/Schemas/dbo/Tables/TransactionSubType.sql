CREATE TABLE [dbo].[TransactionSubType]
(
	[TransactionSubTypeId] BIGINT IDENTITY(1,1) NOT NULL,	
	[TransactionTypeId]  BIGINT NOT NULL,
	[Name] [nvarchar](max) not null,
	[Deleted]	[bit] NULL,
	[CreatedBy] [nvarchar](128) NULL, 
    [UpdatedBy] [nvarchar](128) NULL,     
    [DeletedBy] [nvarchar](128) NULL,
	[CreatedOn]	[datetime] NULL,
	[TimeStamp]	[datetime] NOT NULL DEFAULT GETDATE(),	
	[DeletedOn]	[datetime] NULL,

    CONSTRAINT [PK_dbo.TransactionSubType] PRIMARY KEY CLUSTERED 
(
	[TransactionSubTypeId] ASC
),
CONSTRAINT [FK_TransactionSubType_TransactionTypeId] FOREIGN KEY([TransactionTypeId]) REFERENCES [dbo].[TransactionType](TransactionTypeId),
CONSTRAINT [FK_TransactionSubType_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
CONSTRAINT [FK_TransactionSubType_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
CONSTRAINT [FK_TransactionSubType_DeletedBy] FOREIGN KEY ([DeletedBy]) REFERENCES [dbo].[AspNetUsers](Id),
)ON [PRIMARY]



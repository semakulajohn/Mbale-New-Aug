CREATE TABLE [dbo].[CasualWorker]
(
	[CasualWorkerId] BIGINT IDENTITY(1,1) NOT NULL,	
	[FirstName] [nvarchar](128) not NULL,
	[LastName] [nvarchar](128) NOT NULL,
	[BranchId]   BIGINT NOT NULL,
	[Address]   [nvarchar](128) NOT NULL,
	[PhoneNumber]  [nvarchar](128) NOT NULL,
	[NINNumber]  [nvarchar](max) NOT NULL,
	[NextOfKeen] [nvarchar](50) NOT NULL,
	[EmailAddress] [nvarchar](max) NULL,
	[UniqueNumber] [nvarchar](max) NOT NULL,
	[Deleted]	[bit] NULL,
	[CreatedBy] [nvarchar](128) NULL, 
    [UpdatedBy] [nvarchar](128) NULL,     
    [DeletedBy] [nvarchar](128) NULL,
	[CreatedOn]	[datetime] NULL,
	[TimeStamp]	[datetime] NOT NULL DEFAULT GETDATE(),	
	[DeletedOn]	[datetime] NULL,

    CONSTRAINT [PK_dbo.CasualWorker] PRIMARY KEY CLUSTERED 
(
	[CasualWorkerId] ASC
),

CONSTRAINT [FK_CasualWorker_BranchId] FOREIGN KEY([BranchId]) REFERENCES [dbo].[Branch](BranchId),
CONSTRAINT [FK_CasualWorker_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
CONSTRAINT [FK_CasualWorker_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
CONSTRAINT [FK_CasualWorker_DeletedBy] FOREIGN KEY ([DeletedBy]) REFERENCES [dbo].[AspNetUsers](Id),
)ON [PRIMARY]

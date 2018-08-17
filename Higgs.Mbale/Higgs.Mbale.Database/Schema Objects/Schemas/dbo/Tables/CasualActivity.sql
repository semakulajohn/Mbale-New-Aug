CREATE TABLE [dbo].[CasualActivity]
(
[CasualActivityId] BIGINT IDENTITY(1,1) NOT NULL,
	[BatchId] BIGINT NOT NULL,
	[CasualWorkerId] BIGINT NOT NULL,
	[Quantity]  FLOAT NOT NULL,
	[BranchId]  BIGINT NOT NULL,
	[SectorId]  BIGINT NOT NULL,
	[Notes]   [nvarchar](max),
	[Amount]   FLOAT NOT NULL,
	[Deleted]   BIT NOT NULL,
	[CreatedBy] [nvarchar](128) NULL, 
    [UpdatedBy] [nvarchar](128) NULL,     
    [DeletedBy] [nvarchar](128) NULL,
	[CreatedOn]	[datetime] NULL,
	[TimeStamp]	[datetime] NOT NULL DEFAULT GETDATE(),	
	[DeletedOn]	[datetime] NULL,
	[ActivityId] BIGINT NOT NULL,
 CONSTRAINT [PK_dbo.CasualActivity] PRIMARY KEY CLUSTERED 
(
	[CasualActivityId] ASC
) ,

CONSTRAINT [FK_CasualActivity_ActivityId] FOREIGN KEY ([ActivityId]) REFERENCES [dbo].[Activity](ActivityId),
CONSTRAINT [FK_CasualActivity_BranchId] FOREIGN KEY([BranchId]) REFERENCES [dbo].[Branch](BranchId),
CONSTRAINT [FK_CasualActivity_BatchId] FOREIGN KEY([BatchId]) REFERENCES [dbo].[Batch](BatchId),
CONSTRAINT [FK_CasualActivity_SectorId] FOREIGN KEY([SectorId]) REFERENCES [dbo].[Sector](SectorId),
CONSTRAINT [FK_CasualActivity_CasualWorkerId] FOREIGN KEY([CasualWorkerId]) REFERENCES [dbo].[CasualWorker](CasualWorkerId),
CONSTRAINT [FK_CasualActivity_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
CONSTRAINT [FK_CasualActivity_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
CONSTRAINT [FK_CasualActivity_DeletedBy] FOREIGN KEY ([DeletedBy]) REFERENCES [dbo].[AspNetUsers](Id),
)ON [PRIMARY]
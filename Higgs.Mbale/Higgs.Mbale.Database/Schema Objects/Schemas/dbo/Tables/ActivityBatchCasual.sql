CREATE TABLE [dbo].[ActivityBatchCasual]
(
	[ActivityId]		BIGINT NOT NULL,
	[CasualWorkerId]	BIGINT NOT NULL, 
	[BatchId]			BIGINT NOT NULL,
	[Amount]			FLOAT NOT NULL,
	[Timestamp]		DATETIME NOT NULL,
	[Deleted]		bit NOT NULL,
	[DeletedBy]		nvarchar (128) null,
	[DeletedOn]		DATETIME NULL,
	[CreatedBy] [nvarchar](128) NULL, 
    [UpdatedBy] [nvarchar](128) NULL, 
	

	CONSTRAINT [PK_ActivityBatchCasual] PRIMARY KEY CLUSTERED ([ActivityId],[CasualWorkerId],[BatchId] ASC) ,
	CONSTRAINT [FK_dbo_ActivityBatchCasual_ActivityId] FOREIGN KEY ([ActivityId]) REFERENCES [dbo].[Activity](ActivityId),
	CONSTRAINT [FK_dbo_ActivityBatchCasual_CasualWorkerId] FOREIGN KEY ([CasualWorkerId]) REFERENCES [dbo].[CasualWorker](CasualWorkerId),
	CONSTRAINT [FK_dbo_ActivityBatchCasual_BatchId] FOREIGN KEY ([BatchId]) REFERENCES [dbo].[Batch](BatchId),
	
CONSTRAINT [FK_ActivityBatchCasual_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
CONSTRAINT [FK_ActivityBatchCasual_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
CONSTRAINT [FK_ActivityBatchCasual_DeletedBy] FOREIGN KEY ([DeletedBy]) REFERENCES [dbo].[AspNetUsers](Id),

)
GO
ALTER TABLE [dbo].[ActivityBatchCasual] ADD  CONSTRAINT [DF_dbo_ActivityBatchCasual_Deleted]  DEFAULT ((0)) FOR [Deleted]
GO


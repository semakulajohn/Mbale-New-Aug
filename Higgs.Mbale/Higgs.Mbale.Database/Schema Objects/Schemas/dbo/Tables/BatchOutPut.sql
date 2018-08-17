CREATE TABLE [dbo].[BatchOutPut]
(
	[BatchOutPutId] BIGINT IDENTITY(1,1) NOT NULL,	
	[BatchId]   BIGINT NOT NULL,
	[FlourOutPut] FLOAT NOT NULL,
	[LossPercentage] FLOAT NOT NULL,
	[FlourPercentage] FLOAT NOT NULL,
	[BrandPercentage]  FLOAT NOT NULL,
	[BrandOutPut] FLOAT NOT NULL,
	[BranchId]   BIGINT NOT NULL,
	[SectorId]  BIGINT NOT NULL,
	[Loss]    FLOAT  NULL,
	[Deleted]	[bit] NULL,
	[CreatedBy] [nvarchar](128) NULL, 
    [UpdatedBy] [nvarchar](128) NULL,     
    [DeletedBy] [nvarchar](128) NULL,
	[CreatedOn]	[datetime] NULL,
	[TimeStamp]	[datetime] NOT NULL DEFAULT GETDATE(),	
	[DeletedOn]	[datetime] NULL,

    CONSTRAINT [PK_dbo.BatchOutPut] PRIMARY KEY CLUSTERED 
(
	[BatchOutPutId] ASC
),
CONSTRAINT [FK_BatchOutPut_BatchId] FOREIGN KEY([BatchId]) REFERENCES [dbo].[Batch](BatchId),
CONSTRAINT [FK_BatchOutPut_BranchId] FOREIGN KEY([BranchId]) REFERENCES [dbo].[Branch](BranchId),
CONSTRAINT [FK_BatchOutPut_SectorId] FOREIGN KEY([SectorId]) REFERENCES [dbo].[Sector](SectorId),
CONSTRAINT [FK_BatchOutPut_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
CONSTRAINT [FK_BatchOutPut_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
CONSTRAINT [FK_BatchOutPut_DeletedBy] FOREIGN KEY ([DeletedBy]) REFERENCES [dbo].[AspNetUsers](Id),
)ON [PRIMARY]


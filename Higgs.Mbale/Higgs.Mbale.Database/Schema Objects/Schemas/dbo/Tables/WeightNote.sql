CREATE TABLE [dbo].[WeightNote]
(
	[WeightNoteId] BIGINT IDENTITY(1,1) NOT NULL,	
	[SupplierId] [nvarchar](128) not NULL,
	[TruckNumber]   [nvarchar](128) NOT NULL,
	[MoistureContent]    FLOAT NOT NULL,
	[RejectedBags] INT NOT NULL,
	[WeightNoteNumber]    INT NOT NULL,
	[NumberOfBags]    INT NOT NULL,
	[GrossWeight]   FLOAT NOT NULL,
	[BranchId]  BIGINT NOT NULL,
	[BatchId]  BIGINT NOT NULL,
	[Deleted]	[bit] NULL,
	[CreatedBy] [nvarchar](128) NULL, 
    [UpdatedBy] [nvarchar](128) NULL,     
    [DeletedBy] [nvarchar](128) NULL,
	[CreatedOn]	[datetime] NULL,
	[TimeStamp]	[datetime] NOT NULL DEFAULT GETDATE(),	
	[DeletedOn]	[datetime] NULL,

    CONSTRAINT [PK_dbo.WeightNote] PRIMARY KEY CLUSTERED 
(
	[WeightNoteId] ASC
),
CONSTRAINT [FK_WeightNote_SupplierId] FOREIGN KEY([SupplierId]) REFERENCES [dbo].[AspNetUsers](Id),
CONSTRAINT [FK_WeightNote_BranchId] FOREIGN KEY([BranchId]) REFERENCES [dbo].[Branch](BranchId),
CONSTRAINT [FK_WeightNote_BatchId] FOREIGN KEY([BatchId]) REFERENCES [dbo].[Batch](BatchId),
CONSTRAINT [FK_WeightNote_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
CONSTRAINT [FK_WeightNote_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
CONSTRAINT [FK_WeightNote_DeletedBy] FOREIGN KEY ([DeletedBy]) REFERENCES [dbo].[AspNetUsers](Id),
)ON [PRIMARY]



CREATE TABLE [dbo].[LabourCost]
(
	[LabourCostId] BIGINT IDENTITY(1,1) NOT NULL,	
	[Quantity]   FLOAT NOT NULL,
	[Amount]     FLOAT NOT NULL,
	[Rate]		FLOAT NOT NULL,
	[BatchId] BIGINT NOT NULL,
	[ActivityId] BIGINT NOT NULL,
	[SectorId] BIGINT NOT NULL,
	[BranchId] BIGINT NOT NULL,
	[Deleted]	[bit] NULL,
	[CreatedBy] [nvarchar](128) NULL, 
    [UpdatedBy] [nvarchar](128) NULL,     
    [DeletedBy] [nvarchar](128) NULL,
	[CreatedOn]	[datetime] NULL,
	[TimeStamp]	[datetime] NOT NULL DEFAULT GETDATE(),	
	[DeletedOn]	[datetime] NULL,

    CONSTRAINT [PK_dbo.LabourCost] PRIMARY KEY CLUSTERED 
(
	[LabourCostId] ASC
),
CONSTRAINT [FK_LabourCost_Activity] FOREIGN KEY ([ActivityId]) REFERENCES [dbo].[Activity](ActivityId),
CONSTRAINT [FK_LabourCost_Batch] FOREIGN KEY ([BatchId]) REFERENCES [dbo].[Batch](BatchId),
CONSTRAINT [FK_LabourCost_Branch] FOREIGN KEY ([BranchId]) REFERENCES [dbo].[Branch](BranchId),
CONSTRAINT [FK_LabourCost_Sector] FOREIGN KEY ([SectorId]) REFERENCES [dbo].[Sector](SectorId),
CONSTRAINT [FK_LabourCost_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
CONSTRAINT [FK_LabourCost_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
CONSTRAINT [FK_LabourCost_DeletedBy] FOREIGN KEY ([DeletedBy]) REFERENCES [dbo].[AspNetUsers](Id),
)ON [PRIMARY]
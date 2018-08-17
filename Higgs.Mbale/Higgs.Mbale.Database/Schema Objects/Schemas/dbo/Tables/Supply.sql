CREATE TABLE [dbo].[Supply]
(
	
	[SupplyId] BIGINT IDENTITY(1,1) NOT NULL,	
	[BranchId]  BIGINT NOT NULL,
	[Amount] FLOAT NOT NULL,
	[Used]    BIT NOT NULL,
	[TruckNumber] [nvarchar](max) NOT NULL,
	[Quantity] FLOAT NOT NULL,
	[Price]    FLOAT NOT NULL,
	[WeightNoteNumber] [nvarchar](max) NOT NULL,
	[MoistureContent]  FLOAT NULL,
	[BagsOfStones]   FLOAT NOT NULL,
	[NormalBags]     FLOAT NOT NULL,
	[StatusId]		BIGINT NOT NULL,
	[SupplyNumber] BIGINT NOT NULL,
	[SupplierId] [nvarchar](128) NOT NULL,
	[SupplyDate]  datetime NOT NULL,
	[Deleted]	[bit] NULL,
	[CreatedBy] [nvarchar](128) NULL, 
    [UpdatedBy] [nvarchar](128) NULL,     
    [DeletedBy] [nvarchar](128) NULL,
	[CreatedOn]	[datetime] NULL,
	[TimeStamp]	[datetime] NOT NULL DEFAULT GETDATE(),	
	[DeletedOn]	[datetime] NULL,

    CONSTRAINT [PK_dbo.Supply] PRIMARY KEY CLUSTERED 
(
	[SupplyId] ASC
),
CONSTRAINT [FK_Supply_SupplierId] FOREIGN KEY ([SupplierId]) REFERENCES [dbo].[AspNetUsers](Id),

CONSTRAINT [FK_Supply_BranchId] FOREIGN KEY([BranchId]) REFERENCES [dbo].[Branch](BranchId),
CONSTRAINT [FK_Supply_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
CONSTRAINT [FK_Supply_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
CONSTRAINT [FK_Supply_DeletedBy] FOREIGN KEY ([DeletedBy]) REFERENCES [dbo].[AspNetUsers](Id),
CONSTRAINT [FK_Supply_StatusId]  FOREIGN KEY ([StatusId]) REFERENCES [dbo].[Status](StatusId),
)ON [PRIMARY]


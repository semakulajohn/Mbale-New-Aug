CREATE TABLE [dbo].[WeightNoteValue]
(
	[WeightNoteValueId] BIGINT IDENTITY(1,1) NOT NULL,	
	[WeightNoteId] BIGINT not NULL,
	[Value]  INT NOT NULL,
	[Deleted]	[bit] NULL,
	[CreatedBy] [nvarchar](128) NULL, 
    [UpdatedBy] [nvarchar](128) NULL,     
    [DeletedBy] [nvarchar](128) NULL,
	[CreatedOn]	[datetime] NULL,
	[TimeStamp]	[datetime] NOT NULL DEFAULT GETDATE(),	
	[DeletedOn]	[datetime] NULL,

    CONSTRAINT [PK_dbo.WeightNoteValue] PRIMARY KEY CLUSTERED 
(
	[WeightNoteValueId] ASC
),
CONSTRAINT [FK_WeightNoteValue_BranchId] FOREIGN KEY([WeightNoteId]) REFERENCES [dbo].[WeightNote](WeightNoteId),
CONSTRAINT [FK_WeightNoteValue_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
CONSTRAINT [FK_WeightNoteValue_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
CONSTRAINT [FK_WeightNoteValue_DeletedBy] FOREIGN KEY ([DeletedBy]) REFERENCES [dbo].[AspNetUsers](Id),
)ON [PRIMARY]



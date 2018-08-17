CREATE TABLE [dbo].[Branch]
(
	[BranchId] BIGINT IDENTITY(1,1) NOT NULL,	
	[Name] [nvarchar](max) NULL,
	[Location] [nvarchar](max) NULL,
	[MillingChargeRate]  FLOAT NOT NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[Deleted]	[bit] NULL,
	[CreatedBy] [nvarchar](128) NULL, 
    [UpdatedBy] [nvarchar](128) NULL,     
    [DeletedBy] [nvarchar](128) NULL,
	[CreatedOn]	[datetime] NULL,
	[TimeStamp]	[datetime] NOT NULL DEFAULT GETDATE(),	
	[DeletedOn]	[datetime] NULL,

    CONSTRAINT [PK_dbo.Branch] PRIMARY KEY CLUSTERED 
(
	[BranchId] ASC
),
CONSTRAINT [FK_Branch_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
CONSTRAINT [FK_Branch_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
CONSTRAINT [FK_Branch_DeletedBy] FOREIGN KEY ([DeletedBy]) REFERENCES [dbo].[AspNetUsers](Id),
)ON [PRIMARY]

CREATE TABLE [dbo].[Requistion]
(
	[RequistionId] BIGINT IDENTITY(1,1) NOT NULL,	
	[StatusId] BIGINT NOT NULL,
	[BranchId] BIGINT NOT NULL,
	[ApprovedById]  [nvarchar](128) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Response]   [nvarchar](max) NULL,
	[RequistionNumber] [nvarchar](128) NOT NULL,
	[Deleted]	[bit] NULL,
	[CreatedBy] [nvarchar](128) NULL, 
    [UpdatedBy] [nvarchar](128) NULL,     
    [DeletedBy] [nvarchar](128) NULL,
	[CreatedOn]	[datetime] NULL,
	[TimeStamp]	[datetime] NOT NULL DEFAULT GETDATE(),	
	[DeletedOn]	[datetime] NULL,

    CONSTRAINT [PK_dbo.Requistion] PRIMARY KEY CLUSTERED 
(
	[RequistionId] ASC
),
CONSTRAINT [FK_Requistion_StatusId] FOREIGN KEY([StatusId]) REFERENCES [dbo].[Status](StatusId),
CONSTRAINT [FK_Requistion_BranchId] FOREIGN KEY([BranchId]) REFERENCES [dbo].[Branch](BranchId),
CONSTRAINT [FK_Requistion_ApprovedById] FOREIGN KEY ([ApprovedById]) REFERENCES [dbo].[AspNetUsers](Id),
CONSTRAINT [FK_Requistion_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
CONSTRAINT [FK_Requistion_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
CONSTRAINT [FK_Requistion_DeletedBy] FOREIGN KEY ([DeletedBy]) REFERENCES [dbo].[AspNetUsers](Id),
)ON [PRIMARY]


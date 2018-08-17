CREATE TABLE [dbo].[Expense]
(
	[ExpenseId] BIGINT IDENTITY(1,1) NOT NULL,	
	[Description] [nvarchar](max) not NULL,
	[SectorId]  BIGINT NOT NULL,
	[Amount]    FLOAT NOT NULL,
	[ExpenseDate]   DATETIME NOT NULL,
	[BranchId]  BIGINT NOT NULL,
	[Deleted]	[bit] NULL,
	[CreatedBy] [nvarchar](128) NULL, 
    [UpdatedBy] [nvarchar](128) NULL,     
    [DeletedBy] [nvarchar](128) NULL,
	[CreatedOn]	[datetime] NULL,
	[TimeStamp]	[datetime] NOT NULL DEFAULT GETDATE(),	
	[DeletedOn]	[datetime] NULL,

    CONSTRAINT [PK_dbo.Expense] PRIMARY KEY CLUSTERED 
(
	[ExpenseId] ASC
),

CONSTRAINT [FK_Expense_BranchId] FOREIGN KEY([BranchId]) REFERENCES [dbo].[Branch](BranchId),
CONSTRAINT [FK_Expense_SectorId] FOREIGN KEY([SectorId]) REFERENCES [dbo].[Sector](SectorId),
CONSTRAINT [FK_Expense_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
CONSTRAINT [FK_Expense_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
CONSTRAINT [FK_Expense_DeletedBy] FOREIGN KEY ([DeletedBy]) REFERENCES [dbo].[AspNetUsers](Id),
)ON [PRIMARY]




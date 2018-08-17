CREATE TABLE [dbo].[PaymentMode]
(
	[PaymentModeId] BIGINT IDENTITY(1,1) NOT NULL,	
	[Name] [nvarchar](max) NULL,
	[Deleted]	[bit] NULL,
	[CreatedBy] [nvarchar](128) NULL,    
    [DeletedBy] [nvarchar](128) NULL,
	[CreatedOn]	[datetime] NULL,
	[TimeStamp]	[datetime] NOT NULL DEFAULT GETDATE(),	
	[DeletedOn]	[datetime] NULL,

    CONSTRAINT [PK_dbo.PaymentMode] PRIMARY KEY CLUSTERED 
(
	[PaymentModeId] ASC
),
CONSTRAINT [FK_PaymentMode_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
CONSTRAINT [FK_PaymentMode_DeletedBy] FOREIGN KEY ([DeletedBy]) REFERENCES [dbo].[AspNetUsers](Id),
)ON [PRIMARY]

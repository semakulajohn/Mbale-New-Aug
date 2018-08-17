CREATE TABLE [dbo].[TransactionType]
(
	[TransactionTypeId] BIGINT IDENTITY(1,1) NOT NULL,	
	[Name]  [nvarchar](128) NOT NULL,
	
	[CreatedOn]	[datetime] NULL,


    CONSTRAINT [PK_dbo.TransactionType] PRIMARY KEY CLUSTERED 
(
	[TransactionTypeId] ASC
),

)ON [PRIMARY]



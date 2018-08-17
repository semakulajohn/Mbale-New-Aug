CREATE TABLE [dbo].[Order]
(
	[OrderId] BIGINT IDENTITY(1,1) NOT NULL,	
	[Name] [nvarchar](128) not NULL,
	[Amount] FLOAT NOT NULL,
	[StatusId] BIGINT NOT NULL,
	[CustomerId] [nvarchar](128) NOT NULL,
	[ProductId]  BIGINT NOT NULL,
	[BranchId]   BIGINT NOT NULL,
	[Deleted]	[bit] NULL,
	[CreatedBy] [nvarchar](128) NULL, 
    [UpdatedBy] [nvarchar](128) NULL,     
    [DeletedBy] [nvarchar](128) NULL,
	[CreatedOn]	[datetime] NULL,
	[TimeStamp]	[datetime] NOT NULL DEFAULT GETDATE(),	
	[DeletedOn]	[datetime] NULL,

    CONSTRAINT [PK_dbo.Order] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
),
CONSTRAINT [FK_Order_StatusId] FOREIGN KEY([StatusId]) REFERENCES [dbo].[Status](StatusId),
CONSTRAINT [FK_Order_CustomerId] FOREIGN KEY([CustomerId]) REFERENCES [dbo].[AspNetUsers](Id),
CONSTRAINT [FK_Order_ProductId] FOREIGN KEY([ProductId]) REFERENCES [dbo].[Product](ProductId),
CONSTRAINT [FK_Order_BranchId] FOREIGN KEY([BranchId]) REFERENCES [dbo].[Branch](BranchId),
CONSTRAINT [FK_Order_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
CONSTRAINT [FK_Order_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[AspNetUsers](Id),
CONSTRAINT [FK_Order_DeletedBy] FOREIGN KEY ([DeletedBy]) REFERENCES [dbo].[AspNetUsers](Id),
)ON [PRIMARY]

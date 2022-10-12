USE [cashier_db]
GO


CREATE TABLE [dbo].[OrderDetailsHistory](
	[Id] [bigint]  NULL,
	[OrderId] [bigint]  NULL,
	[ArticleId] [bigint]  NULL,
	[Count] [decimal](18, 2)  NULL,
	[Price] [decimal](18, 2)  NULL,
	[ModifiedBy] [nvarchar](max)  NULL,
	[ModifiedOn] [datetime2](7)  NULL,
	[Operation] [nvarchar] (max) NULL,
)


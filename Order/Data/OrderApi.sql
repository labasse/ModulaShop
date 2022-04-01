CREATE TABLE [dbo].Orders
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [State] INT NULL, 
    [ShippingFees] MONEY NULL, 
    [Paid] MONEY NULL, 
    [Validated] DATETIME2 NULL, 
    [ClientName] NCHAR(50) NULL
);


CREATE TABLE [dbo].[OrderActions]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1), 
	[OrderId] INT NOT NULL,
    [Type] INT NOT NULL,
    [Created] DATETIME2 NULL DEFAULT NULL, 
    [ShippingAddress] NCHAR(255) NULL DEFAULT NULL, 
    [ShippingFees] MONEY NULL DEFAULT NULL, 
    [TransactionNumber] NCHAR(10) NULL DEFAULT NULL, 
    [Amount] MONEY NULL DEFAULT NULL, 
    [TrackingNumber] NCHAR(30) NULL DEFAULT NULL, 
    CONSTRAINT [FK_OrderActions_Orders] FOREIGN KEY ([OrderId]) REFERENCES [Orders]([Id]) 
);

CREATE TABLE [dbo].OrderLines
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [OrderId] INT NOT NULL, 
    [ProductId] INT NOT NULL, 
    [Name] NCHAR(50) NOT NULL, 
    [Price] MONEY NOT NULL, 
    [Qty] INT NOT NULL, 
    CONSTRAINT [FK_OrderLines_Orders] FOREIGN KEY ([OrderId]) REFERENCES [Orders]([Id])
);

SET IDENTITY_INSERT [dbo].[Orders] ON
INSERT INTO [dbo].[Orders] ([Id], [State], [ShippingFees], [Paid], [Validated], [ClientName]) VALUES (1, 0, NULL, NULL, NULL, N'Client1                                           ')
INSERT INTO [dbo].[Orders] ([Id], [State], [ShippingFees], [Paid], [Validated], [ClientName]) VALUES (2, 1, CAST(2.5000 AS Money), NULL, NULL, N'Client2                                           ')
INSERT INTO [dbo].[Orders] ([Id], [State], [ShippingFees], [Paid], [Validated], [ClientName]) VALUES (3, 2, CAST(1.5000 AS Money), CAST(10.0000 AS Money), NULL, N'Client1                   Client 1                ')
INSERT INTO [dbo].[Orders] ([Id], [State], [ShippingFees], [Paid], [Validated], [ClientName]) VALUES (4, 3, CAST(1.5000 AS Money), CAST(25.0000 AS Money), N'2022-03-23 00:00:00', N'Client2                                           ')
SET IDENTITY_INSERT [dbo].[Orders] OFF

SET IDENTITY_INSERT [dbo].[OrderLines] ON
INSERT INTO [dbo].[OrderLines] ([Id], [OrderId], [ProductId], [Name], [Price], [Qty]) VALUES (1, 1, 1, N'Foo                                               ', CAST(10.0000 AS Money), 2)
INSERT INTO [dbo].[OrderLines] ([Id], [OrderId], [ProductId], [Name], [Price], [Qty]) VALUES (2, 1, 2, N'Bar                                               ', CAST(5.0000 AS Money), 1)
INSERT INTO [dbo].[OrderLines] ([Id], [OrderId], [ProductId], [Name], [Price], [Qty]) VALUES (3, 2, 2, N'Bar                                               ', CAST(5.5000 AS Money), 2)
INSERT INTO [dbo].[OrderLines] ([Id], [OrderId], [ProductId], [Name], [Price], [Qty]) VALUES (4, 3, 2, N'Bar                                               ', CAST(5.0000 AS Money), 2)
INSERT INTO [dbo].[OrderLines] ([Id], [OrderId], [ProductId], [Name], [Price], [Qty]) VALUES (5, 3, 1, N'Foo                                               ', CAST(10.0000 AS Money), 1)
INSERT INTO [dbo].[OrderLines] ([Id], [OrderId], [ProductId], [Name], [Price], [Qty]) VALUES (6, 4, 2, N'Bar                                               ', CAST(5.0000 AS Money), 5)
SET IDENTITY_INSERT [dbo].[OrderLines] OFF


SET IDENTITY_INSERT [dbo].[OrderActions] ON
INSERT INTO [dbo].[OrderActions] ([Id], [OrderId], [Type], [Created], [ShippingAddress], [ShippingFees], [TransactionNumber], [Amount], [TrackingNumber]) VALUES (1, 2, 0, N'2022-03-10 00:00:00', N'5 av de la République 33000 Bordeaux                                                                                                                                                                                                                           ', CAST(2.5000 AS Money), NULL, NULL, NULL)
INSERT INTO [dbo].[OrderActions] ([Id], [OrderId], [Type], [Created], [ShippingAddress], [ShippingFees], [TransactionNumber], [Amount], [TrackingNumber]) VALUES (2, 3, 0, N'2022-03-09 00:00:00', N'10 rue des Roses - 33520 Bruges                                                                                                                                                                                                                                ', CAST(1.5000 AS Money), NULL, NULL, NULL)
INSERT INTO [dbo].[OrderActions] ([Id], [OrderId], [Type], [Created], [ShippingAddress], [ShippingFees], [TransactionNumber], [Amount], [TrackingNumber]) VALUES (3, 3, 1, N'2022-03-15 00:00:00', NULL, NULL, N'CF2154585 ', CAST(5.0000 AS Money), NULL)
INSERT INTO [dbo].[OrderActions] ([Id], [OrderId], [Type], [Created], [ShippingAddress], [ShippingFees], [TransactionNumber], [Amount], [TrackingNumber]) VALUES (4, 3, 1, N'2022-03-15 01:00:00', NULL, NULL, N'CF2154678 ', CAST(5.0000 AS Money), NULL)
INSERT INTO [dbo].[OrderActions] ([Id], [OrderId], [Type], [Created], [ShippingAddress], [ShippingFees], [TransactionNumber], [Amount], [TrackingNumber]) VALUES (5, 4, 0, N'2022-03-05 00:00:00', N'5 av de la République 33000 Bordeaux                                                                                                                                                                                                                           ', CAST(1.5000 AS Money), NULL, NULL, NULL)
INSERT INTO [dbo].[OrderActions] ([Id], [OrderId], [Type], [Created], [ShippingAddress], [ShippingFees], [TransactionNumber], [Amount], [TrackingNumber]) VALUES (6, 4, 1, N'2022-03-06 00:00:00', NULL, NULL, N'FR545897  ', CAST(10.0000 AS Money), NULL)
INSERT INTO [dbo].[OrderActions] ([Id], [OrderId], [Type], [Created], [ShippingAddress], [ShippingFees], [TransactionNumber], [Amount], [TrackingNumber]) VALUES (7, 4, 1, N'2022-03-07 00:00:00', NULL, NULL, N'FR548913  ', CAST(10.0000 AS Money), NULL)
INSERT INTO [dbo].[OrderActions] ([Id], [OrderId], [Type], [Created], [ShippingAddress], [ShippingFees], [TransactionNumber], [Amount], [TrackingNumber]) VALUES (8, 4, 1, N'2022-03-08 00:00:00', NULL, NULL, N'FR551489  ', CAST(6.5000 AS Money), NULL)
INSERT INTO [dbo].[OrderActions] ([Id], [OrderId], [Type], [Created], [ShippingAddress], [ShippingFees], [TransactionNumber], [Amount], [TrackingNumber]) VALUES (9, 4, 2, N'2022-03-09 00:00:00', NULL, NULL, NULL, NULL, N'TR-345665-DR5                 ')
SET IDENTITY_INSERT [dbo].[OrderActions] OFF

 
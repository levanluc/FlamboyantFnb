CREATE TABLE [dbo].[Merchant] (
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Code] NVARCHAR(255) NOT NULL,
    [FullName] NVARCHAR(255) NOT NULL,
    [CreatedDate] DATETIME2 NOT NULL,
    [ModifiedDate] DATETIME2 NULL,
    [IsDeleted] BIT NOT NULL
);

CREATE TABLE [dbo].[User] (
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [FullName] NVARCHAR(255) NOT NULL,
    [Avatar] NVARCHAR(255) NULL,
    [PhoneNumber] NVARCHAR(50) NULL,
    [UserName] NVARCHAR(100) NOT NULL,
    [Password] NVARCHAR(255) NOT NULL,
    [MerchantId] INT NOT NULL,
    [CreatedDate] DATETIME2 NOT NULL,
    [ModifiedDate] DATETIME2 NULL,
    [IsDeleted] BIT NOT NULL,
    CONSTRAINT [FK_User_Merchant] FOREIGN KEY ([MerchantId]) REFERENCES [dbo].[Merchant]([Id])
);

CREATE TABLE [dbo].[Categories] (
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Name] NVARCHAR(255) NOT NULL,
    [MerchantId] INT NOT NULL,
    [CreatedDate] DATETIME2 NOT NULL,
    [ModifiedDate] DATETIME2 NULL,
    [IsDeleted] BIT NOT NULL,
    CONSTRAINT [FK_Category_Merchant] FOREIGN KEY ([MerchantId]) REFERENCES [dbo].[Merchants]([Id])
);





CREATE TABLE [dbo].[Products] (
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Name] NVARCHAR(255) NOT NULL,
    [Code] NVARCHAR(100) NOT NULL,
    [Unit] NVARCHAR(50) NULL,
    [IsMaster] BIT NOT NULL,
    [Type] TINYINT NOT NULL,
    [Description] NVARCHAR(MAX) NULL,
    [BasePrice] DECIMAL(18,2) NOT NULL,
    [Price] DECIMAL(18,2) NOT NULL,
    [OnHand] DECIMAL(18,2) NOT NULL,
    [Image] NVARCHAR(255) NULL,
    [CategoryId] INT NOT NULL,
    [MerchantId] INT NOT NULL,
    [CreatedDate] DATETIME2 NOT NULL,
    [ModifiedDate] DATETIME2 NULL,
    [IsDeleted] BIT NOT NULL,
    CONSTRAINT [FK_Product_Category] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Categories]([Id]),
    CONSTRAINT [FK_Product_Merchant] FOREIGN KEY ([MerchantId]) REFERENCES [dbo].[Merchants]([Id])
);
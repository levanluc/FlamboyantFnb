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
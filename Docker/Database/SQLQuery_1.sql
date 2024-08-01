-- Create the database
IF NOT EXISTS ( SELECT name FROM sys.databases WHERE name = N'ECommerce' ) CREATE DATABASE [ECommerce];
GO

-- Use the database
USE ECommerce;
GO

-- Create User table
CREATE TABLE [dbo].[User] (
    [UserID]   INT           IDENTITY (1, 1) NOT NULL,
    [Username] VARCHAR (50)  NOT NULL,
    [Password] VARCHAR (MAX) NOT NULL,
    [ChatID]   VARCHAR (50)  NOT NULL,
    [CreateAt] DATETIME      CONSTRAINT [DEFAULT_User_CreateAt] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([UserID] ASC)
);
GO

-- Create Shop table
CREATE TABLE [dbo].[Shop] (
    [ShopID]      INT             IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (50)    NOT NULL,
    [Description] VARCHAR (MAX)   NULL,
    [Image]       VARBINARY (MAX) NULL,
    [CreateAt]    DATETIME        CONSTRAINT [DEFAULT_Shop_CreateAt] DEFAULT (getdate()) NULL,
    [UserID]      INT             NOT NULL,
    CONSTRAINT [PK_Shop] PRIMARY KEY CLUSTERED ([ShopID] ASC),
    CONSTRAINT [UserID] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([UserID])
);
GO

-- Create Product table
CREATE TABLE [dbo].[Product] (
    [ProductID]   INT             IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (50)    NOT NULL,
    [Description] VARCHAR (MAX)   NULL,
    [Image]       VARBINARY (MAX) NULL,
    [Price]       FLOAT (53)      NOT NULL,
    [Stock]       INT             NOT NULL,
    [CreateAt]    DATETIME        CONSTRAINT [DEFAULT_Product_CreateAt] DEFAULT (getdate()) NULL,
    [ShopID]      INT             NOT NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([ProductID] ASC),
    CONSTRAINT [ShopID] FOREIGN KEY ([ShopID]) REFERENCES [dbo].[Shop] ([ShopID])
);
GO

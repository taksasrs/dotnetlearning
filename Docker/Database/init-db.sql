USE master;
GO

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

-- Insert mock data into User table
INSERT INTO [dbo].[User] ([Username], [Password], [ChatID])
VALUES
('alice_w', 'P@ssw0rd123', 'alice123'),
('bob_m', 'S3cur3P@ss!', 'bob456'),
('charlie_h', 'Pa$$w0rd!', 'charlie789'),
('diana_p', 'Diana2024!', 'diana001'),
('edward_k', 'Edw@rdP@ss', 'edward002'),
('frank_t', 'Frank!2023', 'frank003'),
('grace_y', 'Gr@c3R0cks', 'grace004'),
('hannah_j', 'H@nn@h2024', 'hannah005'),
('ian_v', 'Ian_1234', 'ian006'),
('julia_s', 'JuliaSecure!', 'julia007');
GO

-- Insert mock data into Shop table
INSERT INTO [dbo].[Shop] ([Name], [Description], [Image], [UserID])
VALUES
('Tech Haven', 'A place for all your tech needs', NULL, 1),
('Book Nook', 'Your cozy corner for books', NULL, 2),
('Fashion Fiesta', 'Trendy fashion apparel for all', NULL, 3),
('Gadget Galaxy', 'Latest gadgets and gizmos', NULL, 4),
('Artisan Alley', 'Handmade goods and crafts', NULL, 5),
('Sports Central', 'All the gear for sports enthusiasts', NULL, 6),
('Music Mart', 'Instruments and music accessories', NULL, 7),
('Toy Town', 'Toys and games for kids of all ages', NULL, 8),
('Home Haven', 'Furniture and decor for your home', NULL, 9),
('Pet Paradise', 'Everything for your furry friends', NULL, 10);
GO
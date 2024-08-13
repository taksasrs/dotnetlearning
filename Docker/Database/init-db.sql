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
    [Username] VARCHAR (50)  NOT NULL,
    [Password] VARBINARY(256) NOT NULL,
    [PasswordSalt] VARBINARY(256) NOT NULL,
    [ChatID]   VARCHAR (50)  NOT NULL,
    [CreateAt] DATETIME      CONSTRAINT [DEFAULT_User_CreateAt] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Username] ASC)
    ON DELETE CASCADE
);
GO

-- Create Shop table
CREATE TABLE [dbo].[Shop] (
    [ShopID]      INT             IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (50)    NOT NULL,
    [Description] VARCHAR (MAX)   NULL,
    [Image]       VARBINARY (MAX) NULL,
    [CreateAt]    DATETIME        CONSTRAINT [DEFAULT_Shop_CreateAt] DEFAULT (getdate()) NULL,
    [Username]    VARCHAR (50)    NOT NULL,
    [ImageName]   VARCHAR (50)    NULL,
    CONSTRAINT [PK_Shop] PRIMARY KEY CLUSTERED ([ShopID] ASC),
    CONSTRAINT [FK_Shop_Username] FOREIGN KEY ([Username]) REFERENCES [dbo].[User] ([Username])
    ON DELETE CASCADE
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
    [ImageName]   VARCHAR (50)    NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([ProductID] ASC),
    CONSTRAINT [FK_Product_Shop] FOREIGN KEY ([ShopID]) REFERENCES [dbo].[Shop] ([ShopID])
    ON DELETE CASCADE
);
GO

CREATE TABLE [dbo].[Token] (
    [Id] INT PRIMARY KEY IDENTITY(1,1),
    [Username] VARCHAR(50) NOT NULL,
    [RefreshToken] NVARCHAR(MAX) NOT NULL,
    [RefreshTokenExpiryTime] DATETIME NOT NULL,
    CONSTRAINT [FK_Token_Username] FOREIGN KEY ([Username]) REFERENCES [dbo].[User] ([Username])
);
GO

CREATE TABLE [dbo].[UserRole] (
    [Id] INT PRIMARY KEY IDENTITY(1,1),
    [Username] VARCHAR(50) NOT NULL,
    [Role] VARCHAR(30) NOT NULL,
    CONSTRAINT [FK_UserRole_Username] FOREIGN KEY ([Username]) REFERENCES [dbo].[User] ([Username])
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
INSERT INTO [dbo].[Shop] ([Name], [Description], [Image], [Username], [ImageName])
VALUES
('Tech Haven', 'A place for all your tech needs', NULL, 'alice_w', NULL),
('Book Nook', 'Your cozy corner for books', NULL, 'bob_m', NULL),
('Fashion Fiesta', 'Trendy fashion apparel for all', NULL, 'charlie_h', NULL),
('Gadget Galaxy', 'Latest gadgets and gizmos', NULL, 'diana_p', NULL),
('Artisan Alley', 'Handmade goods and crafts', NULL, 'edward_k', NULL),
('Sports Central', 'All the gear for sports enthusiasts', NULL, 'frank_t', NULL),
('Music Mart', 'Instruments and music accessories', NULL, 'grace_y', NULL),
('Toy Town', 'Toys and games for kids of all ages', NULL, 'hannah_j', NULL),
('Home Haven', 'Furniture and decor for your home', NULL, 'ian_v', NULL),
('Pet Paradise', 'Everything for your furry friends', NULL, 'julia_s', NULL);
GO

-- Insert mock data into the Product table
INSERT INTO [dbo].[Product] ([Name], [Description], [Image], [Price], [Stock], [ShopID], [ImageName])
VALUES 
('Wireless Mouse', 'Ergonomic wireless mouse with adjustable DPI and programmable buttons', NULL, 25.99, 150, 1, NULL),
('Mechanical Keyboard', 'RGB backlit mechanical keyboard with blue switches and detachable wrist rest', NULL, 89.99, 80, 1, NULL),
('Gaming Headset', 'Over-ear gaming headset with surround sound and noise-cancelling microphone', NULL, 59.99, 120, 2, NULL),
('4K Monitor', '27-inch 4K UHD monitor with HDR and ultra-thin bezel', NULL, 349.99, 50, 2, NULL),
('External Hard Drive', '1TB portable external hard drive with USB 3.0 connectivity', NULL, 69.99, 200, 3, NULL),
('Smartphone', 'Latest model smartphone with 128GB storage and 5G connectivity', NULL, 699.99, 30, 3, NULL),
('Bluetooth Speaker', 'Portable Bluetooth speaker with waterproof design and long battery life', NULL, 39.99, 300, 1, NULL),
('Fitness Tracker', 'Water-resistant fitness tracker with heart rate monitor and GPS', NULL, 49.99, 250, 2, NULL),
('Smartwatch', 'Smartwatch with customizable watch faces and health tracking features', NULL, 199.99, 100, 3, NULL),
('Laptop Stand', 'Adjustable laptop stand with cooling fan and USB hub', NULL, 29.99, 400, 1, NULL);
GO

CREATE DATABASE WFW;
GO

USE WFW;
GO

CREATE TABLE Accounts (
   AccountID VARCHAR(100) PRIMARY KEY ,
   FullName NVARCHAR(100),
  Phone NVARCHAR(20),
   Email NVARCHAR(100),
   Password NVARCHAR(100)
);
GO

CREATE TABLE FirewallRules (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IpAddress NVARCHAR(255) NOT NULL,
    IsEnabled BIT NOT NULL
);
GO

CREATE TABLE AccessLogs (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IpAddress NVARCHAR(255),
    AccessTime DATETIME NOT NULL
);
GO

select * from Products;
select * from AccessLogs;

INSERT INTO Accounts (AccountID, FullName, Phone, Email, Password)
VALUES ('Hen','Hang Nguyen', '0123456789', 'hang@gmail.com', 'hen12345');

INSERT INTO Accounts (AccountID, FullName, Phone, Email, Password)
VALUES ('Dung','Dung Huynh', '0234567891', 'dung@gmail.com', 'dung12345');

INSERT INTO Accounts (AccountID, FullName, Phone, Email, Password)
VALUES ('Quyen','Quyen Ly', '0345678912', 'Quyen@gmail.com', 'quyen12345');

DROP TABLE Accounts;
GO

DROP TABLE FirewallRules;
GO

DROP TABLE AccessLogs;
GO
SELECT * FROM INFORMATION_SCHEMA.TABLES;

SELECT name, state_desc FROM sys.databases WHERE name = 'WFW';

CREATE TABLE [dbo].[Categories] (
    [CatID]        int NOT NULL,
    [CatName]      NVARCHAR (50)  NULL,
    [Description]  NVARCHAR (MAX) NULL,
  
    [Thumb]        NVARCHAR (50)  NULL,

    PRIMARY KEY CLUSTERED ([CatID] ASC)
);

CREATE TABLE [dbo].[Products] (
    [ProductID]    int  NOT NULL,
    [ProductName]  NVARCHAR (255)  NULL,
    [CatName]      NVARCHAR (50)   NULL,
    
    [CatID]        int  NULL,
    [Price]        DECIMAL (10, 3) NULL,
    
    [Thumb]        NVARCHAR (255)  NULL,
    
    PRIMARY KEY CLUSTERED ([ProductID] ASC),
    FOREIGN KEY ([CatID]) REFERENCES [dbo].[Categories] ([CatID])
);

-- Chèn dữ liệu vào bảng Categories
INSERT INTO [dbo].[Categories] (CatID, CatName, Description, Thumb)
VALUES
(1, 'Electronics', 'Devices and gadgets', 'electronics_thumb.png'),
(2, 'Clothing', 'Apparel and accessories', 'clothing_thumb.png'),
(3, 'Books', 'Books and magazines', 'books_thumb.png'),
(4, 'Home Appliances', 'Appliances for home use', 'home_appliances_thumb.png');

-- Chèn dữ liệu vào bảng Products
INSERT INTO [dbo].[Products] (ProductID, ProductName, CatName, CatID, Price, Thumb)
VALUES
(1, 'Smartphone', 'Electronics', 1, 299.99, 'smartphone_thumb.png'),
(2, 'Laptop', 'Electronics', 1, 899.99, 'laptop_thumb.png'),
(3, 'T-shirt', 'Clothing', 2, 19.99, 'tshirt_thumb.png'),
(4, 'Jeans', 'Clothing', 2, 49.99, 'jeans_thumb.png'),
(5, 'Fiction Book', 'Books', 3, 14.99, 'fiction_book_thumb.png'),
(6, 'Cookbook', 'Books', 3, 29.99, 'cookbook_thumb.png'),
(7, 'Refrigerator', 'Home Appliances', 4, 499.99, 'refrigerator_thumb.png'),
(8, 'Microwave Oven', 'Home Appliances', 4, 89.99, 'microwave_oven_thumb.png'),
(9, 'Tablet', 'Electronics', 1, 199.99, 'tablet_thumb.png'),
(10, 'Sneakers', 'Clothing', 2, 59.99, 'sneakers_thumb.png');





INSERT INTO Products (CatName) 
VALUES ('Books');

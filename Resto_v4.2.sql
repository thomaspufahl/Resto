USE [master]
GO
/****** Object:  Database [Resto]    Script Date: 30/11/2023 13:23:59 ******/
CREATE DATABASE [Resto]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Resto', FILENAME = N'/var/opt/mssql/data/Resto.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Resto_log', FILENAME = N'/var/opt/mssql/data/Resto_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Resto] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Resto].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Resto] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Resto] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Resto] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Resto] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Resto] SET ARITHABORT OFF 
GO
ALTER DATABASE [Resto] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [Resto] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Resto] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Resto] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Resto] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Resto] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Resto] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Resto] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Resto] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Resto] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Resto] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Resto] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Resto] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Resto] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Resto] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Resto] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Resto] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Resto] SET RECOVERY FULL 
GO
ALTER DATABASE [Resto] SET  MULTI_USER 
GO
ALTER DATABASE [Resto] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Resto] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Resto] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Resto] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Resto] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Resto] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Resto] SET QUERY_STORE = OFF
GO
USE [Resto]
GO
/****** Object:  UserDefinedFunction [dbo].[existsRole]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[existsRole](@roleId int)
RETURNS bit
AS
BEGIN
	DECLARE @existsRole bit = 0;

	IF EXISTS(SELECT 1 FROM [Role] WHERE roleId = @roleId) 
		SET @existsRole = 1

	RETURN @existsRole
END
GO
/****** Object:  UserDefinedFunction [dbo].[isManager]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[isManager] (@employeeId int)
RETURNS BIT
AS
BEGIN
	DECLARE @isManager bit = 0

	IF EXISTS
	(
	SELECT 
	1 
	FROM [Employee] e
	INNER JOIN [Role] r
	ON e.roleId= r.roleId
	WHERE e.employeeId = @employeeId AND (LOWER(r.roleName) like 'manager')
	) SET @isManager = 1

	RETURN @isManager
END
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[employeeId] [int] IDENTITY(1,1) NOT NULL,
	[employeeNumber] [varchar](15) NOT NULL,
	[firstName] [varchar](50) NOT NULL,
	[lastName] [varchar](50) NOT NULL,
	[roleId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[employeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_Employee_employeeNumber] UNIQUE NONCLUSTERED 
(
	[employeeNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[orderNumber] [bigint] IDENTITY(1000,1) NOT NULL,
	[employeeId] [int] NOT NULL,
	[tableNumber] [tinyint] NOT NULL,
	[orderDate] [datetime] NOT NULL,
	[orderStatusId] [int] NOT NULL,
	[updatedAt] [datetime] NOT NULL,
	[isActive] [bit] NOT NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[orderNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderItem]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderItem](
	[orderItemId] [bigint] IDENTITY(1,1) NOT NULL,
	[orderNumber] [bigint] NOT NULL,
	[productId] [int] NOT NULL,
	[quantity] [tinyint] NOT NULL,
	[isActive] [bit] NOT NULL,
 CONSTRAINT [PK_OrderItem] PRIMARY KEY CLUSTERED 
(
	[orderItemId] ASC,
	[orderNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderStatus]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderStatus](
	[orderStatusId] [int] IDENTITY(1,1) NOT NULL,
	[statusCode] [int] NOT NULL,
	[orderStatusName] [varchar](50) NOT NULL,
	[orderStatusDescription] [varchar](255) NULL,
	[isActive] [bit] NOT NULL,
 CONSTRAINT [PK_OrderStatus] PRIMARY KEY CLUSTERED 
(
	[orderStatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_OrderStatus_orderStatusName] UNIQUE NONCLUSTERED 
(
	[orderStatusName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_OrderStatus_statusCode] UNIQUE NONCLUSTERED 
(
	[statusCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[productId] [int] IDENTITY(1,1) NOT NULL,
	[productName] [varchar](50) NOT NULL,
	[productDescription] [varchar](255) NULL,
	[productCategoryId] [int] NOT NULL,
	[stock] [int] NOT NULL,
	[minStockLevel] [int] NOT NULL,
	[unitPrice] [money] NOT NULL,
	[isActive] [bit] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[productId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_Product_productName] UNIQUE NONCLUSTERED 
(
	[productName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductCategory]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductCategory](
	[productCategoryId] [int] IDENTITY(1,1) NOT NULL,
	[productCategoryName] [varchar](50) NOT NULL,
	[isActive] [bit] NOT NULL,
 CONSTRAINT [PK_ProductCategory] PRIMARY KEY CLUSTERED 
(
	[productCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_ProductCategory_productCategoryName] UNIQUE NONCLUSTERED 
(
	[productCategoryName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RestoTable]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RestoTable](
	[tableNumber] [tinyint] NOT NULL,
	[orderNumber] [bigint] NULL,
	[isEnabled] [bit] NOT NULL,
	[isActive] [bit] NOT NULL,
 CONSTRAINT [PK_RestoTable] PRIMARY KEY CLUSTERED 
(
	[tableNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[roleId] [int] IDENTITY(1,1) NOT NULL,
	[roleName] [varchar](50) NOT NULL,
	[roleDescription] [varchar](255) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[roleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_Role_roleName] UNIQUE NONCLUSTERED 
(
	[roleName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Employee] ADD  CONSTRAINT [DF_Employee_isActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Order] ADD  CONSTRAINT [DF_Order_orderDate]  DEFAULT (getdate()) FOR [orderDate]
GO
ALTER TABLE [dbo].[Order] ADD  CONSTRAINT [DF_Order_updatedAt]  DEFAULT (getdate()) FOR [updatedAt]
GO
ALTER TABLE [dbo].[Order] ADD  CONSTRAINT [DF_Order_isActive]  DEFAULT ((1)) FOR [isActive]
GO
ALTER TABLE [dbo].[OrderItem] ADD  CONSTRAINT [DF_OrderItem_isActive]  DEFAULT ((1)) FOR [isActive]
GO
ALTER TABLE [dbo].[OrderStatus] ADD  CONSTRAINT [DF_OrderStatus_isActive]  DEFAULT ((1)) FOR [isActive]
GO
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_isActive]  DEFAULT ((1)) FOR [isActive]
GO
ALTER TABLE [dbo].[ProductCategory] ADD  CONSTRAINT [DF_ProductCategory_isActive]  DEFAULT ((1)) FOR [isActive]
GO
ALTER TABLE [dbo].[RestoTable] ADD  CONSTRAINT [DF_RestoTable_isEnabled]  DEFAULT ((1)) FOR [isEnabled]
GO
ALTER TABLE [dbo].[RestoTable] ADD  CONSTRAINT [DF_RestoTable_isActive]  DEFAULT ((1)) FOR [isActive]
GO
ALTER TABLE [dbo].[Role] ADD  CONSTRAINT [DF_Role_isActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_roleId] FOREIGN KEY([roleId])
REFERENCES [dbo].[Role] ([roleId])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_roleId]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_employeeId] FOREIGN KEY([employeeId])
REFERENCES [dbo].[Employee] ([employeeId])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_employeeId]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_orderStatusId] FOREIGN KEY([orderStatusId])
REFERENCES [dbo].[OrderStatus] ([orderStatusId])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_orderStatusId]
GO
ALTER TABLE [dbo].[OrderItem]  WITH CHECK ADD  CONSTRAINT [FK_OrderItem_orderNumber] FOREIGN KEY([orderNumber])
REFERENCES [dbo].[Order] ([orderNumber])
GO
ALTER TABLE [dbo].[OrderItem] CHECK CONSTRAINT [FK_OrderItem_orderNumber]
GO
ALTER TABLE [dbo].[OrderItem]  WITH CHECK ADD  CONSTRAINT [FK_OrderItem_productId] FOREIGN KEY([productId])
REFERENCES [dbo].[Product] ([productId])
GO
ALTER TABLE [dbo].[OrderItem] CHECK CONSTRAINT [FK_OrderItem_productId]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_productCategoryId] FOREIGN KEY([productCategoryId])
REFERENCES [dbo].[ProductCategory] ([productCategoryId])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_productCategoryId]
GO
ALTER TABLE [dbo].[RestoTable]  WITH CHECK ADD  CONSTRAINT [FK_RestoTable_orderNumber] FOREIGN KEY([orderNumber])
REFERENCES [dbo].[Order] ([orderNumber])
GO
ALTER TABLE [dbo].[RestoTable] CHECK CONSTRAINT [FK_RestoTable_orderNumber]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [CK_MinStockLessThanStock] CHECK  (([minStockLevel]<=[stock]))
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [CK_MinStockLessThanStock]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [CK_MinStockNonNegative] CHECK  (([minStockLevel]>=(0)))
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [CK_MinStockNonNegative]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [CK_StockNonNegative] CHECK  (([stock]>=(0)))
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [CK_StockNonNegative]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [CK_UnitPriceNonNegative] CHECK  (([unitPrice]>=(0)))
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [CK_UnitPriceNonNegative]
GO
/****** Object:  StoredProcedure [dbo].[activateEmployee]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[activateEmployee]
	@employeeId   int
AS
BEGIN
	IF EXISTS(SELECT 1 FROM Employee WHERE employeeId = @employeeId AND isActive = 1) RETURN;

	UPDATE Employee
	SET
	isActive = 1
	WHERE employeeId = @employeeId

	SELECT @@ROWCOUNT
END
GO
/****** Object:  StoredProcedure [dbo].[activateOrder]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[activateOrder]
	@orderNumber bigint
AS
BEGIN
	IF EXISTS(SELECT 1 FROM [Order] WHERE orderNumber = @orderNumber AND isActive = 1) RETURN;

	UPDATE [Order]
	SET
	isActive = 1
	WHERE orderNumber = @orderNumber

	SELECT @@ROWCOUNT
END
GO
/****** Object:  StoredProcedure [dbo].[activateOrderItem]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[activateOrderItem]
	@orderItemId bigint
AS
BEGIN
	IF EXISTS(SELECT 1 FROM OrderItem WHERE orderItemId = @orderItemId AND isActive = 1) RETURN;

	UPDATE OrderItem
	SET
	isActive = 1
	WHERE orderItemId = @orderItemId

	SELECT @@ROWCOUNT
END
GO
/****** Object:  StoredProcedure [dbo].[activateOrderStatus]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[activateOrderStatus]
	@orderStatusId   int
AS
BEGIN
	IF EXISTS(SELECT 1 FROM OrderStatus WHERE orderStatusId = @orderStatusId AND isActive = 1) RETURN;

	UPDATE OrderStatus
	SET
	isActive = 1
	WHERE orderStatusId = @orderStatusId

	SELECT @@ROWCOUNT
END
GO
/****** Object:  StoredProcedure [dbo].[activateProduct]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[activateProduct]
	@productId   int
AS
BEGIN
	IF EXISTS(SELECT 1 FROM Product WHERE productId = @productId AND isActive = 1) RETURN;

	UPDATE Product
	SET
	isActive = 1
	WHERE productId = @productId

	SELECT @@ROWCOUNT
END
GO
/****** Object:  StoredProcedure [dbo].[activateProductCategory]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[activateProductCategory]
	@productCategoryId   int
AS
BEGIN
	IF EXISTS(SELECT 1 FROM ProductCategory WHERE productCategoryId = @productCategoryId AND isActive = 1) RETURN;

	UPDATE ProductCategory
	SET
	isActive = 1
	WHERE productCategoryId = @productCategoryId

	SELECT @@ROWCOUNT
END
GO
/****** Object:  StoredProcedure [dbo].[activateRestoTable]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[activateRestoTable]
	@tableNumber tinyint
AS
BEGIN
	IF EXISTS(SELECT 1 FROM RestoTable WHERE tableNumber = @tableNumber AND isActive = 1) RETURN;

	UPDATE RestoTable
	SET
	isActive = 1
	WHERE tableNumber = @tableNumber

	SELECT @@ROWCOUNT
END
GO
/****** Object:  StoredProcedure [dbo].[activateRole]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[activateRole]
	@roleId   int
AS
BEGIN
	IF EXISTS(SELECT 1 FROM [Role] WHERE roleId = @roleId AND isActive = 1) RETURN;

	UPDATE [Role]
	SET
	isActive = 1
	WHERE roleId = @roleId

	SELECT @@ROWCOUNT
END
GO
/****** Object:  StoredProcedure [dbo].[deactivateEmployee]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[deactivateEmployee]
	@employeeId   int
AS
BEGIN
	IF EXISTS(SELECT 1 FROM Employee WHERE employeeId = @employeeId AND isActive = 0) RETURN;

	UPDATE Employee
	SET
	isActive = 0
	WHERE employeeId = @employeeId

	SELECT @@ROWCOUNT
END
GO
/****** Object:  StoredProcedure [dbo].[deactivateOrder]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[deactivateOrder]
	@orderNumber bigint
AS
BEGIN
	IF EXISTS(SELECT 1 FROM [Order] WHERE orderNumber = @orderNumber AND isActive = 0) RETURN;

	UPDATE [Order]
	SET
	isActive = 0
	WHERE orderNumber = @orderNumber

	SELECT @@ROWCOUNT
END
GO
/****** Object:  StoredProcedure [dbo].[deactivateOrderItem]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[deactivateOrderItem]
	@orderItemId bigint
AS
BEGIN
	IF EXISTS(SELECT 1 FROM OrderItem WHERE orderItemId = @orderItemId AND isActive = 0) RETURN;

	UPDATE OrderItem
	SET
	isActive = 0
	WHERE orderItemId = @orderItemId

	SELECT @@ROWCOUNT
END
GO
/****** Object:  StoredProcedure [dbo].[deactivateOrderStatus]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[deactivateOrderStatus]
	@orderStatusId   int
AS
BEGIN
	IF EXISTS(SELECT 1 FROM OrderStatus WHERE orderStatusId = @orderStatusId AND isActive = 0) RETURN;

	UPDATE OrderStatus
	SET
	isActive = 0
	WHERE orderStatusId = @orderStatusId

	SELECT @@ROWCOUNT
END
GO
/****** Object:  StoredProcedure [dbo].[deactivateProduct]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[deactivateProduct]
	@productId   int
AS
BEGIN
	IF EXISTS(SELECT 1 FROM Product WHERE productId = @productId AND isActive = 0) RETURN;

	UPDATE Product
	SET
	isActive = 0
	WHERE productId = @productId

	SELECT @@ROWCOUNT
END
GO
/****** Object:  StoredProcedure [dbo].[deactivateProductCategory]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[deactivateProductCategory]
	@productCategoryId   int
AS
BEGIN
	IF EXISTS(SELECT 1 FROM ProductCategory WHERE productCategoryId = @productCategoryId AND isActive = 0) RETURN;

	UPDATE ProductCategory
	SET
	isActive = 0
	WHERE productCategoryId = @productCategoryId

	SELECT @@ROWCOUNT
END
GO
/****** Object:  StoredProcedure [dbo].[deactivateRestoTable]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[deactivateRestoTable]
	@tableNumber tinyint
AS
BEGIN
	IF EXISTS(SELECT 1 FROM RestoTable WHERE tableNumber = @tableNumber AND isActive = 0) RETURN;

	UPDATE RestoTable
	SET
	isActive = 0
	WHERE tableNumber = @tableNumber

	SELECT @@ROWCOUNT
END
GO
/****** Object:  StoredProcedure [dbo].[deactivateRole]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[deactivateRole]
	@roleId   int
AS
BEGIN
	IF EXISTS(SELECT 1 FROM [Role] WHERE roleId = @roleId AND isActive = 0) RETURN;

	UPDATE [Role]
	SET
	isActive = 0
	WHERE roleId = @roleId

	SELECT @@ROWCOUNT
END
GO
/****** Object:  StoredProcedure [dbo].[delEmployee]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[delEmployee]
	@employeeId int
AS
BEGIN
	DELETE FROM Employee
	WHERE employeeId = @employeeId

	SELECT @@ROWCOUNT
END
GO
/****** Object:  StoredProcedure [dbo].[delOrder]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[delOrder]
	@orderNumber bigint
AS
BEGIN
	
	DELETE FROM [Order] WHERE orderNumber = @orderNumber

	SELECT @@ROWCOUNT
END
GO
/****** Object:  StoredProcedure [dbo].[delOrderItem]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[delOrderItem]
	@orderItemId bigint
AS
BEGIN
	
	DELETE FROM OrderItem WHERE orderItemId = @orderItemId

	SELECT @@ROWCOUNT
END
GO
/****** Object:  StoredProcedure [dbo].[delOrderStatus]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[delOrderStatus]
	@orderStatusId   int
AS
BEGIN
	
	DELETE FROM OrderStatus WHERE orderStatusId = @orderStatusId

	SELECT @@ROWCOUNT
END
GO
/****** Object:  StoredProcedure [dbo].[delProduct]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[delProduct]
	@productId   int
AS
BEGIN
	
	DELETE FROM Product WHERE productId = @productId

	SELECT @@ROWCOUNT
END
GO
/****** Object:  StoredProcedure [dbo].[delProductCategory]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[delProductCategory]
	@productCategoryId   int
AS
BEGIN
	
	DELETE FROM ProductCategory WHERE productCategoryId = @productCategoryId

	SELECT @@ROWCOUNT
END
GO
/****** Object:  StoredProcedure [dbo].[delRestoTable]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[delRestoTable]
	@tableNumber tinyint
AS
BEGIN
	DELETE FROM RestoTable WHERE tableNumber = @tableNumber

	SELECT @@ROWCOUNT
END
GO
/****** Object:  StoredProcedure [dbo].[disableRestoTable]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[disableRestoTable]
	@tableNumber tinyint
AS
BEGIN
	IF EXISTS(SELECT 1 FROM RestoTable WHERE tableNumber = @tableNumber AND isEnabled = 0 AND isActive = 1) RETURN;

	UPDATE RestoTable
	SET
	isEnabled = 0
	WHERE tableNumber = @tableNumber

	SELECT @@ROWCOUNT
END
GO
/****** Object:  StoredProcedure [dbo].[enableRestoTable]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[enableRestoTable]
	@tableNumber tinyint
AS
BEGIN
	IF EXISTS(SELECT 1 FROM RestoTable WHERE tableNumber = @tableNumber AND isEnabled = 1 AND isActive = 1) RETURN;

	UPDATE RestoTable
	SET
	isEnabled = 1
	WHERE tableNumber = @tableNumber

	SELECT @@ROWCOUNT
END
GO
/****** Object:  StoredProcedure [dbo].[getEmployee]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[getEmployee]
AS
BEGIN
	SELECT
	employeeId, 
	employeeNumber, 
	firstName, 
	lastName, 
	roleId,
	isActive 
	FROM Employee
END
GO
/****** Object:  StoredProcedure [dbo].[getEmployeeByEmployeeNumber]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[getEmployeeByEmployeeNumber]
	@employeeNumber varchar(15)
AS
BEGIN
	SELECT
	employeeId, 
	employeeNumber, 
	firstName, 
	lastName, 
	roleId,
	isActive 
	FROM Employee
	WHERE employeeNumber = @employeeNumber
END
GO
/****** Object:  StoredProcedure [dbo].[getEmployeeById]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[getEmployeeById]
	@employeeId int
AS
BEGIN
	SELECT
	employeeId, 
	employeeNumber, 
	firstName, 
	lastName, 
	roleId,
	isActive 
	FROM Employee
	WHERE employeeId = @employeeId
END
GO
/****** Object:  StoredProcedure [dbo].[getOrder]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[getOrder]
AS
BEGIN
	SELECT
	O.orderNumber,
	O.employeeId,
	O.tableNumber,
	O.orderDate,
	O.orderStatusId,
	O.updatedAt,
	O.isActive
	FROM [Order] O
END
GO
/****** Object:  StoredProcedure [dbo].[getOrderByOrderNumber]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[getOrderByOrderNumber]
	@orderNumber bigint
AS
BEGIN
	SELECT
	O.orderNumber,
	O.employeeId,
	O.tableNumber,
	O.orderDate,
	O.orderStatusId,
	O.updatedAt,
	O.isActive
	FROM [Order] O
	WHERE O.orderNumber = @orderNumber
END
GO
/****** Object:  StoredProcedure [dbo].[getOrderItem]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[getOrderItem]
AS
BEGIN
	SELECT
	O.orderItemId,
	O.orderNumber,
	O.productId,
	O.quantity,
	O.isActive
	FROM OrderItem O
END
GO
/****** Object:  StoredProcedure [dbo].[getOrderItemById]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[getOrderItemById]
	@orderItemId bigint
AS
BEGIN
	SELECT
	O.orderItemId,
	O.orderNumber,
	O.productId,
	O.quantity,
	O.isActive
	FROM OrderItem O
	WHERE O.orderItemId = @orderItemId
END
GO
/****** Object:  StoredProcedure [dbo].[getOrderItemByOrderNumber]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[getOrderItemByOrderNumber]
	@orderNumber bigint
AS
BEGIN
	SELECT
	O.orderItemId,
	O.orderNumber,
	O.productId,
	O.quantity,
	O.isActive
	FROM OrderItem O
	WHERE O.orderNumber = @orderNumber
END
GO
/****** Object:  StoredProcedure [dbo].[getOrderStatus]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[getOrderStatus]
AS
BEGIN
	SELECT
	O.orderStatusId,
	O.statusCode,
	O.orderStatusName,
	COALESCE(O.orderStatusDescription, '') as orderStatusDescription,
	O.isActive
	FROM OrderStatus O
END
GO
/****** Object:  StoredProcedure [dbo].[getOrderStatusByCode]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[getOrderStatusByCode]
	@statusCode int
AS
BEGIN
	SELECT
	O.orderStatusId,
	O.statusCode,
	O.orderStatusName,
	COALESCE(O.orderStatusDescription, '') as orderStatusDescription,
	O.isActive
	FROM OrderStatus O
	WHERE O.statusCode = @statusCode
END
GO
/****** Object:  StoredProcedure [dbo].[getOrderStatusById]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[getOrderStatusById]
	@orderStatusId int
AS
BEGIN
	SELECT
	O.orderStatusId,
	O.statusCode,
	O.orderStatusName,
	COALESCE(O.orderStatusDescription, '') as orderStatusDescription,
	O.isActive
	FROM OrderStatus O
	WHERE O.orderStatusId = @orderStatusId
END
GO
/****** Object:  StoredProcedure [dbo].[getProduct]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[getProduct]
AS
BEGIN
	SELECT
	productId,
	productName,
	COALESCE(productDescription, '') as productDescription,
	productCategoryId,
	stock,
	minStockLevel,
	unitPrice,
	isActive
	FROM Product
END
GO
/****** Object:  StoredProcedure [dbo].[getProductById]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[getProductById]
	@productId int
AS
BEGIN
	SELECT
	productId,
	productName,
	COALESCE(productDescription, '') as productDescription,
	productCategoryId,
	stock,
	minStockLevel,
	unitPrice,
	isActive
	FROM Product
	WHERE productId = @productId
END
GO
/****** Object:  StoredProcedure [dbo].[getProductCategory]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[getProductCategory]
AS
BEGIN
	SELECT
	productCategoryId,
	productCategoryName,
	isActive
	FROM ProductCategory
END
GO
/****** Object:  StoredProcedure [dbo].[getProductCategoryById]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[getProductCategoryById]
	@productCategoryId int
AS
BEGIN
	SELECT
	productCategoryId,
	productCategoryName,
	isActive
	FROM ProductCategory
	WHERE productCategoryId = @productCategoryId
END
GO
/****** Object:  StoredProcedure [dbo].[getRestoTable]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[getRestoTable]
AS
BEGIN
	SELECT
	R.tableNumber,
	COALESCE(R.orderNumber, -1) as orderNumber,
	R.isEnabled,
	R.isActive
	FROM RestoTable R
END
GO
/****** Object:  StoredProcedure [dbo].[getRestoTableByTableNumber]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[getRestoTableByTableNumber]
	@tableNumber tinyint
AS
BEGIN
	SELECT
	R.tableNumber,
	COALESCE(R.orderNumber, -1) as orderNumber,
	R.isEnabled,
	R.isActive
	FROM RestoTable R
	WHERE R.tableNumber = @tableNumber
END
GO
/****** Object:  StoredProcedure [dbo].[getRole]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[getRole]
AS
BEGIN
	SELECT
	roleId,
	roleName,
	COALESCE(roleDescription, '') as roleDescription,
	isActive
	FROM [Role]
END
GO
/****** Object:  StoredProcedure [dbo].[getRoleById]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[getRoleById]
	@roleId int
AS
BEGIN
	SELECT
	roleId,
	roleName,
	COALESCE(roleDescription, '') as roleDescription,
	isActive
	FROM [Role]
	WHERE roleId = @roleId
END
GO
/****** Object:  StoredProcedure [dbo].[insEmployee]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[insEmployee]
	@employeeNumber varchar(15),
	@firstName		varchar(50),
	@lastName		varchar(50),
	@roleId			int
AS
BEGIN
	INSERT INTO Employee
	(employeeNumber, firstName, lastName, roleId)
	VALUES
	(@employeeNumber, @firstName, @lastName, @roleId)

	SELECT SCOPE_IDENTITY()
END
GO
/****** Object:  StoredProcedure [dbo].[insOrder]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[insOrder]
	@employeeId    int,
	@tableNumber   tinyint,
	@orderStatusId int
AS
BEGIN
	INSERT INTO [Order] (employeeId, tableNumber, orderStatusId)
	VALUES
	(@employeeId, @tableNumber, @orderStatusId)

	SELECT SCOPE_IDENTITY()
END
GO
/****** Object:  StoredProcedure [dbo].[insOrderItem]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[insOrderItem]
	@orderNumber   bigint,
	@productId     int,
	@quantity      tinyint
AS
BEGIN
	INSERT INTO OrderItem (orderNumber, productId, quantity)
	VALUES
	(@orderNumber, @productId, @quantity)
	
	SELECT SCOPE_IDENTITY()
END
GO
/****** Object:  StoredProcedure [dbo].[insOrderStatus]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[insOrderStatus]
	@statusCode				int,
	@orderStatusName		varchar(50),
	@orderStatusDescription varchar(255) = NULL
AS
BEGIN
	INSERT INTO OrderStatus (statusCode, orderStatusName, orderStatusDescription)
	VALUES
	(@statusCode, @orderStatusName, @orderStatusDescription)

	SELECT SCOPE_IDENTITY()
END
GO
/****** Object:  StoredProcedure [dbo].[insProduct]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[insProduct]
	@productName		varchar(50),
	@productCategoryId  int,
	@stock				int,
	@minStockLevel		int,
	@unitPrice			money,
	@productDescription varchar(255) = NULL
AS
BEGIN
	INSERT INTO Product (productName, productDescription, productCategoryId, stock, minStockLevel, unitPrice)
	VALUES
	(
		@productName,
		@productDescription,
		@productCategoryId,
		@stock,
		@minStockLevel,
		@unitPrice
	)

	SELECT SCOPE_IDENTITY()
END
GO
/****** Object:  StoredProcedure [dbo].[insProductCategory]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[insProductCategory]
	@productCategoryName varchar(50)
AS
BEGIN
	INSERT INTO ProductCategory (productCategoryName)
	VALUES
	(@productCategoryName)

	SELECT SCOPE_IDENTITY()
END
GO
/****** Object:  StoredProcedure [dbo].[insRestoTable]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[insRestoTable]
	@tableNumber tinyint
AS
BEGIN
	INSERT INTO RestoTable (tableNumber, orderNumber)
	VALUES
	(@tableNumber, NULL)

	IF EXISTS(SELECT 1 FROM RestoTable WHERE tableNumber = @tableNumber)
		BEGIN
			SELECT @tableNumber		
		END
	ELSE
		BEGIN
			SELECT -1
		END
END
GO
/****** Object:  StoredProcedure [dbo].[updEmployee]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[updEmployee]
	@employeeId int,
	@firstName  varchar(50),
	@lastName   varchar(50),
	@roleId	    int
AS
BEGIN
	UPDATE Employee 
	SET
	firstName = @firstName,
	lastName = @lastName,
	roleId = @roleId
	WHERE employeeId = @employeeId

	SELECT @@ROWCOUNT
END
GO
/****** Object:  StoredProcedure [dbo].[updOrder]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[updOrder]
	@orderNumber   bigint,
	@orderStatusId int
AS
BEGIN
	UPDATE [Order]
	SET
	orderStatusId = @orderStatusId
	WHERE orderNumber = @orderNumber

	SELECT @@ROWCOUNT
END
GO
/****** Object:  StoredProcedure [dbo].[updOrderItem]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[updOrderItem]
	@orderItemId bigint,
	@productId   int,
	@quantity    tinyint
AS
BEGIN
	UPDATE OrderItem
	SET
	productId = @productId,
	quantity = @quantity
	WHERE orderItemId = @orderItemId

	SELECT @@ROWCOUNT
END
GO
/****** Object:  StoredProcedure [dbo].[updOrderStatus]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[updOrderStatus]
	@orderStatusId			int,
	@statusCode				int,
	@orderStatusName		varchar(50),
	@orderStatusDescription varchar(255)
AS
BEGIN
	UPDATE OrderStatus
	SET
	statusCode = @statusCode,
	orderStatusName = @orderStatusName,
	orderStatusDescription = @orderStatusDescription
	WHERE orderStatusId = @orderStatusId

	SELECT @@ROWCOUNT
END
GO
/****** Object:  StoredProcedure [dbo].[updProduct]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[updProduct]
	@productId			int,
	@productName		varchar(50),
	@productCategoryId  int,
	@stock				int,
	@minStockLevel		int,
	@unitPrice			money,
	@productDescription varchar(255) = NULL
AS
BEGIN
	UPDATE Product
	SET
	productName = @productName,
	productDescription = @productDescription,
	productCategoryId = @productCategoryId,
	stock = @stock,
	minStockLevel = @minStockLevel,
	unitPrice = @unitPrice
	WHERE productId = @productId

	SELECT @@ROWCOUNT
END
GO
/****** Object:  StoredProcedure [dbo].[updProductCategory]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[updProductCategory]
	@productCategoryId   int,
	@productCategoryName varchar(50)
AS
BEGIN
	UPDATE ProductCategory
	SET
	productCategoryName = @productCategoryName
	WHERE productCategoryId = @productCategoryId

	SELECT @@ROWCOUNT
END
GO
/****** Object:  StoredProcedure [dbo].[updRestoTable]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[updRestoTable]
	@tableNumber	tinyint,
	@orderNumber	bigint
AS
BEGIN
	UPDATE RestoTable
	SET
	orderNumber = @orderNumber
	WHERE tableNumber = @tableNumber

	SELECT @@ROWCOUNT
END
GO
/****** Object:  Trigger [dbo].[onUpdOrder]    Script Date: 30/11/2023 13:23:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER [dbo].[onUpdOrder]
ON [dbo].[Order]
FOR UPDATE
AS
	UPDATE [Order]
	SET
	updatedAt = GETDATE()
	WHERE orderNumber = (SELECT orderNumber FROM inserted)
GO
ALTER TABLE [dbo].[Order] ENABLE TRIGGER [onUpdOrder]
GO
USE [master]
GO
ALTER DATABASE [Resto] SET  READ_WRITE 
GO

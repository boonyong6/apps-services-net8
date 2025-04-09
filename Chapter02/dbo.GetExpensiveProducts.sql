USE [Northwind]
GO

/****** Object: SqlProcedure [dbo].[GetExpensiveProducts] Script Date: 4/9/2025 10:30:02 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetExpensiveProducts]
	@price money,
	@count int OUT
AS
	-- Raise the `InfoMessage` event.
	PRINT 'Getting expensive products: ' + TRIM(CAST(@price AS NVARCHAR(10)))
	
	SELECT @count = COUNT(*)
	FROM Products
	WHERE UnitPrice >= @price

	SELECT *
	FROM Products
	WHERE UnitPrice >= @price
RETURN 0

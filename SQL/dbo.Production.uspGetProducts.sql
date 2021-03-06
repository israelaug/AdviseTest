USE [AdventureWorks2012]
GO
/****** Object:  StoredProcedure [Production].[uspGetProducts]    Script Date: 09/04/2018 18:31:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [Production].[uspGetProducts]

	-- parametros: data de inicio de venda, pagina e quantidade de itens por pagina
	@pSellStartDate date = NULL,
	@pPage int = 1,
	@pRowsPage int = 20,
	@pOrderByName bit = false,
	@pOrderLastFirst bit = false
AS
BEGIN

    SELECT 
		pro.Name AS productName, 
		pro.ProductNumber AS productNumber, 
		promod.Name AS productModel, 
		pro.SellStartDate AS SellStartDate
	FROM Production.Product pro


	INNER JOIN Production.ProductModel promod
	ON promod.ProductModelID = pro.ProductModelID

	-- filtros
    WHERE @pSellStartDate IS NULL OR pro.SellStartDate = @pSellStartDate

	-- ordem alfabetica
	ORDER BY
		CASE WHEN @pOrderByName = 'true' THEN pro.Name END ASC,
		CASE WHEN @pOrderLastFirst = 'true' THEN pro.ProductID END ASC

	-- paginacao
	OFFSET ((@pPage - 1) * @pRowsPage) ROWS
	FETCH NEXT @pRowsPage ROWS ONLY;

END;

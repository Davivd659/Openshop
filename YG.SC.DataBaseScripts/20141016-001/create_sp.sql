
CREATE PROC [dbo].[SP_GetOrderNumberSeed]
AS
BEGIN
	DECLARE @result VARCHAR(14),@insdt VARCHAR(10),@count VARCHAR(10),@OrderNumber VARCHAR(10)
	SET @result=CONVERT(varchar(100), GETDATE(), 12)
	SET @insdt =CONVERT(varchar(100), GETDATE(), 112)
	SET @count = (SELECT COUNT(*) FROM OrderNumberSeeds WHERE InsDt = @insdt)+1
	SET @count = RIGHT('0000'+@count, 4)
	SET @OrderNumber = (@result + @count)
	
	INSERT OrderNumberSeeds(OrderNumber, InsDt) VALUES (@OrderNumber, @insdt)
	
	SELECT TOP 1 Id,OrderNumber,InsDt FROM OrderNumberSeeds Order By Id DESC
END
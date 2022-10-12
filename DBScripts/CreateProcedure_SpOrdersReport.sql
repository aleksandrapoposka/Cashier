CREATE PROCEDURE spGetOrdersReport 
@DateFrom smalldatetime = NULL,
@DateTo smalldatetime = NULL,
@ModifiedBy nvarchar(MAX) = NULL

AS 

BEGIN 

DECLARE @SSQL AS NVARCHAR(MAX) 

SET @SSQL = 'Select  O.Id , OD.ArticleId , A.Name ,
A.Description, A.Manufacturer ,
O.UserId,U.FirstName, U.LastName,
OD.Price, OD.Count , OD.Price * OD.Count TotalValue
From Orders O 
Inner Join OrderDetails OD on O.Id = OD.OrderId 
Inner Join Articles A on OD.ArticleID = A.Id 
Inner Join AspNetUsers U on O.UserId = U.Id
Where 1 = 1 '

If @ModifiedBy IS NOT NULL
SET @SSQL = @SSQL + 'AND O.ModifiedBy = ''' + @ModifiedBy  +''''

If  @DateFrom IS NOT NULL
SET @SSQL = @SSQL + 'AND O.ModifiedOn >= ''' + CONVERT(nvarchar(30),@DateFrom)   + ''''

If @DateTo IS NOT NULL
SET @SSQL = @SSQL + 'AND O.ModifiedOn <= ''' + CONVERT(nvarchar(30),@DateTo) + ''''

print(@SSQL)
exec (@SSQL) 
END 

--exec spGetOrdersReport 
--@ModifiedBy = 'filip.c',
--@DateFrom = '2022-11-09',
--@DateTo = '2022-11-11'
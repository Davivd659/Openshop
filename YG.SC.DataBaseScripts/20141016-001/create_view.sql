CREATE VIEW Scp_GetAllGoods_View
AS
SELECT     goods.Id, goods.GoodsName, goods.GoodsPinYin, goods.Price, goods.UnitCd, goods.ImageName, sctFirst.CategoryName AS ClassifyName, sctFirst.Id AS ClassifyId, 
                      sctSecond.Id AS ClassifySecondId, sctSecond.CategoryName AS ClassifySecondName, brand.BrandName, goods.Specification, goods.Producer, goods.StorageCd, 
                      goods.InsDt, goods.IsElite, goods.Recsts
FROM         dbo.SctSkuGoods AS goods INNER JOIN
                      dbo.SccSkuGoodsInSkuBrand ON goods.Id = dbo.SccSkuGoodsInSkuBrand.GoodsId INNER JOIN
                      dbo.SctSkuBrand AS brand ON dbo.SccSkuGoodsInSkuBrand.BrandId = brand.Id INNER JOIN
                      dbo.SccSkuGoodsInSkuCategorySecond ON goods.Id = dbo.SccSkuGoodsInSkuCategorySecond.GoodsId INNER JOIN
                      dbo.SctSkuCategorySecond AS sctSecond ON dbo.SccSkuGoodsInSkuCategorySecond.CategorySecondId = sctSecond.Id INNER JOIN
                      dbo.SctSkuCategoryFirst AS sctFirst ON sctSecond.CategoryFirstId = sctFirst.Id
GO

CREATE VIEW Scp_GetClassifty_View
AS
SELECT     s.Id AS SecondId, f.Id AS FirstId, f.CategoryName AS FirstName, f.CategoryImg AS FirstImg, s.Id, s.CategoryName AS SecondCategoryName
FROM         dbo.SctSkuCategoryFirst AS f INNER JOIN
                      dbo.SctSkuCategorySecond AS s ON f.Id = s.CategoryFirstId

GO

CREATE VIEW Scp_GetGoodsBySecond_View
AS
SELECT     f.Id AS FirstId, f.CategoryName AS FirstCategoryName, s.Id AS SecondId, s.CategoryName AS SecondCategoryName, g.Id AS GoodsId, g.GoodsName, g.Price, 
                      g.UnitCd, g.ImageName, b.BrandName, g.Producer, g.Specification, g.StorageCd, g.Recsts
FROM         dbo.SctSkuCategoryFirst AS f INNER JOIN
                      dbo.SctSkuCategorySecond AS s ON f.Id = s.CategoryFirstId INNER JOIN
                      dbo.SccSkuGoodsInSkuCategorySecond AS sg ON s.Id = sg.CategorySecondId INNER JOIN
                      dbo.SctSkuGoods AS g ON sg.GoodsId = g.Id INNER JOIN
                      dbo.SccSkuGoodsInSkuBrand AS sb ON g.Id = sb.GoodsId INNER JOIN
                      dbo.SctSkuBrand AS b ON sb.BrandId = b.Id
GO

CREATE VIEW Scp_OrderDetail_View
AS
SELECT     o.OrderNo, o.OrderCd, o.InsDt, o.CustomerName, o.CustomerPhone, o.DeliveryAddress, o.CustomerDesc, o.OrderAmount, o.InvoiceCd, o.InvoiceName, d.GoodsId, 
                      d.GoodsName, d.GoodsPrice, d.GoodsTotal, c.SDesc AS OrserStatusName
FROM         dbo.SctSkuOrder AS o INNER JOIN
                      dbo.SctSkuOrderDetail AS d ON d.OrderId = o.Id INNER JOIN
                      dbo.SysRefCd AS c ON o.OrderCd = c.RefCd
GO

CREATE VIEW Scp_OrderList_View
AS
SELECT     dbo.SctSkuOrder.Id, dbo.SctUserOAuth.OtherId, dbo.ScmUser.Id AS UserId, dbo.SctSkuOrder.OrderNo, dbo.SctSkuOrder.OrderCd, SRC.SDesc AS OrderStatusName, 
                      dbo.SctSkuOrder.InsDt
FROM         dbo.SctSkuOrder INNER JOIN
                          (SELECT     RefGrpId, RefCd, SDesc
                            FROM          dbo.SysRefCd
                            WHERE      (RefGrpId = 4)) AS SRC ON SRC.RefCd = dbo.SctSkuOrder.OrderCd INNER JOIN
                      dbo.ScmUser ON dbo.SctSkuOrder.UserId = dbo.ScmUser.Id INNER JOIN
                      dbo.SctUserOAuth ON dbo.SctUserOAuth.UserId = dbo.ScmUser.Id
GO

CREATE VIEW Scp_OrderStatistics_View
AS
SELECT     SCM.Id AS UserId, SCU.CustomerName, SCU.CustomerPhone, SCU.Gender, STO.OrderId, STO.OrderAmount, STO.InsDt
FROM         (SELECT     Id, UserName, UserPhone
                       FROM          ScmUser WITH (NOLOCK)
                       WHERE      UserCd = 'NORMAL') AS SCM INNER JOIN
                          (SELECT     Id AS OrderId, UserId, OrderAmount, OrderCd, InsDt, ROW_NUMBER() OVER (ORDER BY InsDt DESC) AS RowNum
FROM         SctSkuOrder WITH (NOLOCK)
WHERE     OrderCd IN ('GENERATE', 'RECEIVE', 'SUCCESS', 'FAIL')) AS STO ON STO.USERID = SCM.ID INNER JOIN
    (SELECT     UserId, CustomerName, CustomerPhone, Gender
      FROM          SctUserAddress
      WHERE      Recsts = '3') AS SCU ON STO.UserId = SCU.UserId
GO



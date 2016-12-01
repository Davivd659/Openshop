
----设置菜单目录
--INSERT SctApplication(ApplicationName,InsBy,InsDt,OrdSeq,ParentId) VALUES ('商品管理','SYSTEM',GETDATE(),1000,0);
--INSERT SctApplication(ApplicationName,InsBy,InsDt,OrdSeq,ParentId) VALUES ('订单管理','SYSTEM',GETDATE(),1100,0);
--INSERT SctApplication(ApplicationName,InsBy,InsDt,OrdSeq,ParentId) VALUES ('客户管理','SYSTEM',GETDATE(),1200,0);
--INSERT SctApplication(ApplicationName,InsBy,InsDt,OrdSeq,ParentId) VALUES ('帐号管理','SYSTEM',GETDATE(),1300,0);
--INSERT SctApplication(ApplicationName,InsBy,InsDt,OrdSeq,ParentId) VALUES ('首页Branner','SYSTEM',GETDATE(),1400,0)

--INSERT SctApplication(ApplicationName,InsBy,InsDt,OrdSeq,ParentId) VALUES ('一级分类管理','SYSTEM',GETDATE(),1000,1)
--INSERT SctApplication(ApplicationName,InsBy,InsDt,OrdSeq,ParentId) VALUES ('二级分类管理','SYSTEM',GETDATE(),1100,1)
--INSERT SctApplication(ApplicationName,InsBy,InsDt,OrdSeq,ParentId) VALUES ('品牌管理','SYSTEM',GETDATE(),1200,1)
--INSERT SctApplication(ApplicationName,InsBy,InsDt,OrdSeq,ParentId) VALUES ('商品管理','SYSTEM',GETDATE(),1300,1)

--INSERT SctApplication(ApplicationName,InsBy,InsDt,OrdSeq,ParentId) VALUES ('订单列表','SYSTEM',GETDATE(),1000,2)
--INSERT SctApplication(ApplicationName,InsBy,InsDt,OrdSeq,ParentId) VALUES ('订单统计','SYSTEM',GETDATE(),1100,2)

--INSERT SctApplication(ApplicationName,InsBy,InsDt,OrdSeq,ParentId) VALUES ('客户信息审核','SYSTEM',GETDATE(),1000,3)

--INSERT SctApplication(ApplicationName,InsBy,InsDt,OrdSeq,ParentId) VALUES ('帐号列表','SYSTEM',GETDATE(),1000,4)

--INSERT SctApplication(ApplicationName,InsBy,InsDt,OrdSeq,ParentId) VALUES ('Branner管理','SYSTEM',GETDATE(),1000,5)


----商品管理
--INSERT SctPath(ApplicationId,PathUrl,PathName,InsBy,InsDt,OrdSeq) VALUES (6, '/SkuCategoryFirst/Index', '一级分类管理', 'SYSTEM', GETDATE(), 1000)
--INSERT SctPath(ApplicationId,PathUrl,PathName,InsBy,InsDt,OrdSeq) VALUES (6, '/SkuCategoryFirst/Edit', '一级分类修改', 'SYSTEM', GETDATE(), 1100)
--INSERT SctPath(ApplicationId,PathUrl,PathName,InsBy,InsDt,OrdSeq) VALUES (6, '/SkuCategoryFirst/Add', '一级分类添加', 'SYSTEM', GETDATE(), 1200)

--INSERT SctPath(ApplicationId,PathUrl,PathName,InsBy,InsDt,OrdSeq) VALUES (7, '/SkuCategorySecond/Index', '二级分类管理', 'SYSTEM', GETDATE(), 1000)
--INSERT SctPath(ApplicationId,PathUrl,PathName,InsBy,InsDt,OrdSeq) VALUES (7,  '/SkuCategorySecond/Edit', '二级分类修改', 'SYSTEM', GETDATE(), 1100)
--INSERT SctPath(ApplicationId,PathUrl,PathName,InsBy,InsDt,OrdSeq) VALUES (7, '/SkuCategorySecond/Add', '二级分类添加', 'SYSTEM', GETDATE(), 1200)


--INSERT SctPath(ApplicationId,PathUrl,PathName,InsBy,InsDt,OrdSeq) VALUES (8,  '/SkuBrand/Index', '品牌管理', 'SYSTEM', GETDATE(), 1000)
--INSERT SctPath(ApplicationId,PathUrl,PathName,InsBy,InsDt,OrdSeq) VALUES (8,  '/SkuBrand/Edit', '品牌修改', 'SYSTEM', GETDATE(), 1100)
--INSERT SctPath(ApplicationId,PathUrl,PathName,InsBy,InsDt,OrdSeq) VALUES (8,  '/SkuBrand/Add', '品牌添加', 'SYSTEM', GETDATE(), 1300)


--INSERT SctPath(ApplicationId,PathUrl,PathName,InsBy,InsDt,OrdSeq) VALUES (9, '/SkuGoods/Index', '商品管理', 'SYSTEM', GETDATE(), 1000)
--INSERT SctPath(ApplicationId,PathUrl,PathName,InsBy,InsDt,OrdSeq) VALUES (9, '/SkuGoods/Edit', '商品修改', 'SYSTEM', GETDATE(), 1100)
--INSERT SctPath(ApplicationId,PathUrl,PathName,InsBy,InsDt,OrdSeq) VALUES (9, '/SkuGoods/Add', '商品添加', 'SYSTEM', GETDATE(), 1300)


----订单管理
--INSERT SctPath(ApplicationId,PathUrl,PathName,InsBy,InsDt,OrdSeq) VALUES (10,'/SkuOrder/Index', '订单管理', 'SYSTEM', GETDATE(), 1000)
--INSERT SctPath(ApplicationId,PathUrl,PathName,InsBy,InsDt,OrdSeq) VALUES (10, '/SkuOrder/Edit', '订单处理', 'SYSTEM', GETDATE(), 1100)

--INSERT SctPath(ApplicationId,PathUrl,PathName,InsBy,InsDt,OrdSeq) VALUES (11, '/SkuOrder/OrderStatistics', '订单统计', 'SYSTEM', GETDATE(), 1000)

----客户管理
--INSERT SctPath(ApplicationId,PathUrl,PathName,InsBy,InsDt,OrdSeq) VALUES (12, '/SkuUser/Index', '客户信息管理', 'SYSTEM', GETDATE(), 1000)
--INSERT SctPath(ApplicationId,PathUrl,PathName,InsBy,InsDt,OrdSeq) VALUES (12, '/SkuUser/Index', '客户信息修改', 'SYSTEM', GETDATE(), 1100)

----帐号管理
--INSERT SctPath(ApplicationId,PathUrl,PathName,InsBy,InsDt,OrdSeq) VALUES (13,'/UserSecurity/Index', '帐号管理', 'SYSTEM', GETDATE(), 1000)
--INSERT SctPath(ApplicationId,PathUrl,PathName,InsBy,InsDt,OrdSeq) VALUES (13,'/UserSecurity/Edit', '设置权限', 'SYSTEM', GETDATE(), 1100)
--INSERT SctPath(ApplicationId,PathUrl,PathName,InsBy,InsDt,OrdSeq) VALUES (13, '/UserSecurity/Add', '添加帐号', 'SYSTEM', GETDATE(), 1200)

----banner管理
--INSERT SctPath(ApplicationId,PathUrl,PathName,InsBy,InsDt,OrdSeq) VALUES (14,'/AdPicture/Index', 'banner管理', 'SYSTEM', GETDATE(), 1000)
--INSERT SctPath(ApplicationId,PathUrl,PathName,InsBy,InsDt,OrdSeq) VALUES (14,'/AdPicture/Edit', '修改banner', 'SYSTEM', GETDATE(), 1100)
--INSERT SctPath(ApplicationId,PathUrl,PathName,InsBy,InsDt,OrdSeq) VALUES (14, '/AdPicture/Add', '添加banner', 'SYSTEM', GETDATE(), 1200)



----一级分类动作
--INSERT SccAction(PathId, ActionName) VALUES (1,'查看')
--INSERT SccAction(PathId, ActionName) VALUES (2,'修改')
--INSERT SccAction(PathId, ActionName) VALUES (3,'添加')

----二级分类动作
--INSERT SccAction(PathId, ActionName) VALUES (4,'查看')
--INSERT SccAction(PathId, ActionName) VALUES (5,'修改')
--INSERT SccAction(PathId, ActionName) VALUES (6,'添加')

----品牌动作
--INSERT SccAction(PathId, ActionName) VALUES (7,'查看')
--INSERT SccAction(PathId, ActionName) VALUES (8,'修改')
--INSERT SccAction(PathId, ActionName) VALUES (9,'添加')

----商品动作
--INSERT SccAction(PathId, ActionName) VALUES (10,'查看')
--INSERT SccAction(PathId, ActionName) VALUES (11,'修改')
--INSERT SccAction(PathId, ActionName) VALUES (12,'添加')

----订单动作
--INSERT SccAction(PathId, ActionName) VALUES (13,'查看')
--INSERT SccAction(PathId, ActionName) VALUES (14,'修改')
--INSERT SccAction(PathId, ActionName) VALUES (15,'统计')

----客户动作
--INSERT SccAction(PathId, ActionName) VALUES (16,'查看')
--INSERT SccAction(PathId, ActionName) VALUES (17,'修改')

----帐号动作
--INSERT SccAction(PathId, ActionName) VALUES (18,'查看')
--INSERT SccAction(PathId, ActionName) VALUES (19,'设置权限')
--INSERT SccAction(PathId, ActionName) VALUES (20,'添加帐号')

----banner动作
--INSERT SccAction(PathId, ActionName) VALUES (21,'查看')
--INSERT SccAction(PathId, ActionName) VALUES (22,'修改')
--INSERT SccAction(PathId, ActionName) VALUES (23,'添加')

------初始账户
--INSERT ScmUser(UserName,LoginName,Password,UserPhone,UserCd,SourceCd,InsBy,InsDt,UpdBy,UpdDt,Recsts) VALUES('test123用户','test123','ygtest123','','SENIOR','WEB','SYSTEM',GETDATE(),'SYSTEM',GETDATE(),'0')
--INSERT ScmUser(UserName,LoginName,Password,UserPhone,UserCd,SourceCd,InsBy,InsDt,UpdBy,UpdDt,Recsts)  VALUES('admin用户','admin123','ygadmin123','','SENIOR','WEB','SYSTEM',GETDATE(),'SYSTEM',GETDATE(),'0')

-----测试数据普通用户
--INSERT ScmUser(UserName,LoginName,Password,UserPhone,UserCd,SourceCd,InsBy,InsDt,UpdBy,UpdDt,Recsts) VALUES('user1用户','user1','123456','13264232563','NORMAL','WEB','SYSTEM',GETDATE(),'SYSTEM',GETDATE(),'0')
--INSERT ScmUser(UserName,LoginName,Password,UserPhone,UserCd,SourceCd,InsBy,InsDt,UpdBy,UpdDt,Recsts) VALUES('user2用户','user2','123456','13264232563','NORMAL','WEB','SYSTEM',GETDATE(),'SYSTEM',GETDATE(),'0')


--INSERT SccRole(RoleName,SDesc) VALUES ('普通管理员','普通管理员')
--INSERT SccRole(RoleName,SDesc) VALUES ('超级管理员','超级管理员')

--INSERT SccUserInRole(UserId, RoleId) VALUES (1,1)
--INSERT SccUserInRole(UserId, RoleId) VALUES (2,2)



-------普通用户权限
----一级分类
--INSERT SccActionInRole(ActionId,RoleId) VALUES (1,1)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (2,1)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (3,1)
----二级分类
--INSERT SccActionInRole(ActionId,RoleId) VALUES (4,1)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (5,1)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (6,1)
----品牌管理
--INSERT SccActionInRole(ActionId,RoleId) VALUES (7,1)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (8,1)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (9,1)
----商品管理
--INSERT SccActionInRole(ActionId,RoleId) VALUES (10,1)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (11,1)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (12,1)
----订单管理
--INSERT SccActionInRole(ActionId,RoleId) VALUES (13,1)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (14,1)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (15,1)
----客户管理
--INSERT SccActionInRole(ActionId,RoleId) VALUES (16,1)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (17,1)
----banner管理
--INSERT SccActionInRole(ActionId,RoleId) VALUES (21,1)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (22,1)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (23,1)

-------管理员用户权限
----一级分类
--INSERT SccActionInRole(ActionId,RoleId) VALUES (1,2)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (2,2)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (3,2)
----二级分类
--INSERT SccActionInRole(ActionId,RoleId) VALUES (4,2)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (5,2)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (6,2)
----品牌管理
--INSERT SccActionInRole(ActionId,RoleId) VALUES (7,2)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (8,2)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (9,2)
----商品管理
--INSERT SccActionInRole(ActionId,RoleId) VALUES (10,2)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (11,2)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (12,2)
----订单管理
--INSERT SccActionInRole(ActionId,RoleId) VALUES (13,2)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (14,2)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (15,2)
----客户管理
--INSERT SccActionInRole(ActionId,RoleId) VALUES (16,2)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (17,2)
----账户管理
--INSERT SccActionInRole(ActionId,RoleId) VALUES (18,2)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (19,2)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (20,2)

----banner管理
--INSERT SccActionInRole(ActionId,RoleId) VALUES (21,2)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (22,2)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (23,2)










--INSERT SctAppVersion(SourceCd,Version,IsMandatory,UrlAddress,SDesc,Recsts,InsBy,InsDt,VersionCode) VALUES ('ANDROID','1.0',0,'http://www.chaoshirukou.cn/download/SuperMarketEnter1.2.2.apk?sukey=fea97b85a98d7f78868b7321f9861c6eddc29eebc94103b3461196f575258584cb130b9bd27573ac52ea0ce46285c950','更新内容：'+CHAR(10)+CHAR(13)+'1.这里是更新的内容1 '+CHAR(10)+CHAR(13)+' 2.这里是更新的内容2','0','SYSTEM',GETDATE(),1)
--INSERT SctAppVersion(SourceCd,Version,IsMandatory,UrlAddress,SDesc,Recsts,InsBy,InsDt,VersionCode) VALUES ('IPHONE','1.0',0,'http://www.chaoshirukou.cn/download/SuperMarketEnter1.2.2.apk?sukey=fea97b85a98d7f78868b7321f9861c6eddc29eebc94103b3461196f575258584cb130b9bd27573ac52ea0ce46285c950','更新内容：'+CHAR(10)+CHAR(13)+'1.这里是更新的内容1 '+CHAR(10)+CHAR(13)+' 2.这里是更新的内容2','0','SYSTEM',GETDATE(),1)
--INSERT SctUserAddress(UserId,CustomerName,CustomerPhone,Gender,ProvinceId,CityId,CountyId,AddressDetial,InsBy,InsDt,Recsts) 
-- VALUES (3,'地址姓名','13264232563','1',15,147,1384,'十字路镇孟家黄庄村','SYSTEM',GETDATE(),'0')


-------------订单生成存储过程
--CREATE PROC SP_GetOrderNumberSeed
--AS
--BEGIN
--	DECLARE @result VARCHAR(14),@insdt VARCHAR(10),@count VARCHAR(10),@OrderNumber VARCHAR(10)
--	SET @result=CONVERT(varchar(100), GETDATE(), 12)
--	SET @insdt =CONVERT(varchar(100), GETDATE(), 112)
--	SET @count = (SELECT COUNT(*) FROM OrderNumberSeeds WHERE InsDt = @insdt)+1
--	SET @count = RIGHT('0000'+@count, 4)
--	SET @OrderNumber = (@result + @count)
	
--	INSERT OrderNumberSeeds(OrderNumber, InsDt) VALUES (@OrderNumber, @insdt)
	
--	SELECT TOP 1 Id,OrderNumber,InsDt FROM OrderNumberSeeds Order By Id DESC
--END

--调用存储过程

--EXEC SP_GetOrderNumberSeed 

--SELECT TOP 10 * FROM OrderNumberSeeds

----SELECT TOP 10 * FROM SctPath
----SELECT * FROM SccAction
----SELECT * FROM SctApplication where  ParentId=4

--SELECT * FROM SysRefGrp
--SELECT * FROM SysRefCd

--SELECT * FROM SccRole

--SELECT TOP 10 * FROM ScmUser

--SELECT TOP 10 * FROM ScmUser WHERE Id=5

--SELECT * FROM SccUserInRole

--INSERT SccUserInRole(UserId,RoleId) VALUES (5,1)

--SELECT * FROM SctPath

--SELECT * FROM SctUserAddress


--SELECT * FROM SccRole

--SELECT  * FROM SctSkuGoods

--SELECT TOP 10 * FROM SccUserInRole
--SELECT TOP 10 * FROM SccRole

--SELECT  * FROM SCCACTIONINROLE 
----ROLEID 1 普通 2超级
--SELECT A.ID,A.ACTIONNAME,P.PathName FROM 
--(SELECT * FROM SCCAction ) A LEFT JOIN (SELECT * FROM SctPath) P ON A.PATHID = P.ID

--SELECT TOP 10 * FROM SctPath WHERE ApplicationId=8

--SELECT A.ActionName,P.ApplicationId,P.PathUrl,P.PathName,P.Id,A.Id AS ActionId,P.OrdSeq,AN.ApplicationName,AN.OrdSeq FROM 
--(SELECT Id FROM ScmUser WHERE UserName='mengqizhou') M
--INNER JOIN (SELECT UserId,RoleId FROM SccUserInRole) CR ON CR.UserId = M.id
--INNER JOIN (SELECT RoleId,ActionId FROM SccActionInRole) CA ON CR.RoleId = CA.RoleId
--INNER JOIN (SELECT Id,ActionName,PathId FROM SccAction) AS A On A.Id = CA.ActionId 
--INNER JOIN (SELECT Id,ApplicationId,PathUrl,PathName,OrdSeq FROM SctPath) AS P ON P.Id = A.PathId
--INNER JOIN (SELECT Id,ApplicationName,OrdSeq FROM SctApplication ) AS AN ON P.ApplicationId = AN.Id

--SELECT TOP 10 * FROM SctApplication WHERE ID=1
--SELECT TOP 10 * FROM SctApplication WHERE Id=5


--SELECT * FROM SccRole

--SELECT TOP 10 * FROM SctSkuCategorySecond

--SELECT * FROM SctPath



----SkuGoods

----UPDATE SctPath SET PathUrl = REPLACE(PathUrl,'SctPath','SkuGoods') WHERE Id IN(10,11,12)
--SELECT * FROM SccAction

--SELECT TOP 10 * FROM SccActionRestriction


--SELECT TOP 10 * FROM SctSkuGoods WHERE ID = 13



--SELECT  * FROM SccActionInRole

-----订单统计
--SELECT SMU.Id,SMU.UserName,SMU.UserPhone,STO.Id AS OrderId FROM ScmUser AS SMU LEFT JOIN 
--(SELECT Id,UserId FROM SctSkuOrder) AS STO ON STO.UserId = SMU.Id

--SELECT TOP 10 * FROM ScmUser
----1	1	用户来源组
----2	1	支付方式组
----3	1	支付状态组
----4	1	订单状态组
----5	1	发票类型组
----6	1	订单短信发送状态组
----7	1	商品单位分组
----8	1	用户类型组
--SELECT TOP 10 * FROM SysRefCd WHERE RefGrpId=8



--SELECT TOP 10 * FROM SctUserAddress

--SELECT TOP 10 * FROM SctSkuOrder

----INSERT SctSkuOrder(UserId,OrderNo,SourceCd,UserAddressId,CustomerName,CustomerPhone,SendCd,PaymentMethodCd,PaymentStatusCd,OrderAmount,
---- OrderCd,DeliveryDt,DeliveryAddress,InvoiceCd,InvoiceName,InsBy,InsDt,CustomerDesc,FAQDesc)
---- VALUES (4,'100046252','WEB',1,'孟祺宙','13264232563','UNSENT','COD','NOTPAID',130,'GENERATE',GETDATE(),
---- '山东省临沂市莒南县十字路镇孟家黄庄村','PERSON','','SYSTEM',GETDATE(),'快点','')

--SELECT * FROM ScmUser

----创建订单统计视图
------DROP VIEW Scp_OrderStatistics_View

----CREATE VIEW Scp_OrderStatistics_View
----AS 
----SELECT SCM.Id AS UserId, SCM.UserName, SCM.UserPhone, STO.OrderId, STO.OrderAmount, STO.InsDt FROM (
----SELECT Id, UserName, UserPhone FROM ScmUser WITH(NOLOCK) WHERE UserCd = 'NORMAL') AS SCM
----INNER JOIN 
----(SELECT Id AS OrderId,UserId,OrderAmount,OrderCd,InsDt, ROW_NUMBER() OVER (ORDER BY InsDt DESC) AS RowNum 
---- FROM SctSkuOrder WITH(NOLOCK) WHERE OrderCd IN ('GENERATE','RECEIVE','SUCCESS','FAIL') ) AS STO ON STO.USERID = SCM.ID

--SELECT * FROM Scp_OrderStatistics_View

--SELECT * FROM SctSkuOrder

--SELECT * FROM SctSkuOrder

--SELECT * FROM SysRefCd WHERE RefGrpId=9
----NORMAL

--SELECT * FROM SctSkuGoods

--SELECT * FROM SctUserOAuth


--SELECT * FROM OrderNumberSeeds order By Id desc

--SELECT * FROM SctUserAddress

--SELECT TOP 10 * FROM SctSkuOrder

--SELECT * FROM SysRefCd
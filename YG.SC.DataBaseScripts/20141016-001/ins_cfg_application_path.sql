
----���ò˵�Ŀ¼
--INSERT SctApplication(ApplicationName,InsBy,InsDt,OrdSeq,ParentId) VALUES ('��Ʒ����','SYSTEM',GETDATE(),1000,0);
--INSERT SctApplication(ApplicationName,InsBy,InsDt,OrdSeq,ParentId) VALUES ('��������','SYSTEM',GETDATE(),1100,0);
--INSERT SctApplication(ApplicationName,InsBy,InsDt,OrdSeq,ParentId) VALUES ('�ͻ�����','SYSTEM',GETDATE(),1200,0);
--INSERT SctApplication(ApplicationName,InsBy,InsDt,OrdSeq,ParentId) VALUES ('�ʺŹ���','SYSTEM',GETDATE(),1300,0);
--INSERT SctApplication(ApplicationName,InsBy,InsDt,OrdSeq,ParentId) VALUES ('��ҳBranner','SYSTEM',GETDATE(),1400,0)

--INSERT SctApplication(ApplicationName,InsBy,InsDt,OrdSeq,ParentId) VALUES ('һ���������','SYSTEM',GETDATE(),1000,1)
--INSERT SctApplication(ApplicationName,InsBy,InsDt,OrdSeq,ParentId) VALUES ('�����������','SYSTEM',GETDATE(),1100,1)
--INSERT SctApplication(ApplicationName,InsBy,InsDt,OrdSeq,ParentId) VALUES ('Ʒ�ƹ���','SYSTEM',GETDATE(),1200,1)
--INSERT SctApplication(ApplicationName,InsBy,InsDt,OrdSeq,ParentId) VALUES ('��Ʒ����','SYSTEM',GETDATE(),1300,1)

--INSERT SctApplication(ApplicationName,InsBy,InsDt,OrdSeq,ParentId) VALUES ('�����б�','SYSTEM',GETDATE(),1000,2)
--INSERT SctApplication(ApplicationName,InsBy,InsDt,OrdSeq,ParentId) VALUES ('����ͳ��','SYSTEM',GETDATE(),1100,2)

--INSERT SctApplication(ApplicationName,InsBy,InsDt,OrdSeq,ParentId) VALUES ('�ͻ���Ϣ���','SYSTEM',GETDATE(),1000,3)

--INSERT SctApplication(ApplicationName,InsBy,InsDt,OrdSeq,ParentId) VALUES ('�ʺ��б�','SYSTEM',GETDATE(),1000,4)

--INSERT SctApplication(ApplicationName,InsBy,InsDt,OrdSeq,ParentId) VALUES ('Branner����','SYSTEM',GETDATE(),1000,5)


----��Ʒ����
--INSERT SctPath(ApplicationId,PathUrl,PathName,InsBy,InsDt,OrdSeq) VALUES (6, '/SkuCategoryFirst/Index', 'һ���������', 'SYSTEM', GETDATE(), 1000)
--INSERT SctPath(ApplicationId,PathUrl,PathName,InsBy,InsDt,OrdSeq) VALUES (6, '/SkuCategoryFirst/Edit', 'һ�������޸�', 'SYSTEM', GETDATE(), 1100)
--INSERT SctPath(ApplicationId,PathUrl,PathName,InsBy,InsDt,OrdSeq) VALUES (6, '/SkuCategoryFirst/Add', 'һ���������', 'SYSTEM', GETDATE(), 1200)

--INSERT SctPath(ApplicationId,PathUrl,PathName,InsBy,InsDt,OrdSeq) VALUES (7, '/SkuCategorySecond/Index', '�����������', 'SYSTEM', GETDATE(), 1000)
--INSERT SctPath(ApplicationId,PathUrl,PathName,InsBy,InsDt,OrdSeq) VALUES (7,  '/SkuCategorySecond/Edit', '���������޸�', 'SYSTEM', GETDATE(), 1100)
--INSERT SctPath(ApplicationId,PathUrl,PathName,InsBy,InsDt,OrdSeq) VALUES (7, '/SkuCategorySecond/Add', '�����������', 'SYSTEM', GETDATE(), 1200)


--INSERT SctPath(ApplicationId,PathUrl,PathName,InsBy,InsDt,OrdSeq) VALUES (8,  '/SkuBrand/Index', 'Ʒ�ƹ���', 'SYSTEM', GETDATE(), 1000)
--INSERT SctPath(ApplicationId,PathUrl,PathName,InsBy,InsDt,OrdSeq) VALUES (8,  '/SkuBrand/Edit', 'Ʒ���޸�', 'SYSTEM', GETDATE(), 1100)
--INSERT SctPath(ApplicationId,PathUrl,PathName,InsBy,InsDt,OrdSeq) VALUES (8,  '/SkuBrand/Add', 'Ʒ�����', 'SYSTEM', GETDATE(), 1300)


--INSERT SctPath(ApplicationId,PathUrl,PathName,InsBy,InsDt,OrdSeq) VALUES (9, '/SkuGoods/Index', '��Ʒ����', 'SYSTEM', GETDATE(), 1000)
--INSERT SctPath(ApplicationId,PathUrl,PathName,InsBy,InsDt,OrdSeq) VALUES (9, '/SkuGoods/Edit', '��Ʒ�޸�', 'SYSTEM', GETDATE(), 1100)
--INSERT SctPath(ApplicationId,PathUrl,PathName,InsBy,InsDt,OrdSeq) VALUES (9, '/SkuGoods/Add', '��Ʒ���', 'SYSTEM', GETDATE(), 1300)


----��������
--INSERT SctPath(ApplicationId,PathUrl,PathName,InsBy,InsDt,OrdSeq) VALUES (10,'/SkuOrder/Index', '��������', 'SYSTEM', GETDATE(), 1000)
--INSERT SctPath(ApplicationId,PathUrl,PathName,InsBy,InsDt,OrdSeq) VALUES (10, '/SkuOrder/Edit', '��������', 'SYSTEM', GETDATE(), 1100)

--INSERT SctPath(ApplicationId,PathUrl,PathName,InsBy,InsDt,OrdSeq) VALUES (11, '/SkuOrder/OrderStatistics', '����ͳ��', 'SYSTEM', GETDATE(), 1000)

----�ͻ�����
--INSERT SctPath(ApplicationId,PathUrl,PathName,InsBy,InsDt,OrdSeq) VALUES (12, '/SkuUser/Index', '�ͻ���Ϣ����', 'SYSTEM', GETDATE(), 1000)
--INSERT SctPath(ApplicationId,PathUrl,PathName,InsBy,InsDt,OrdSeq) VALUES (12, '/SkuUser/Index', '�ͻ���Ϣ�޸�', 'SYSTEM', GETDATE(), 1100)

----�ʺŹ���
--INSERT SctPath(ApplicationId,PathUrl,PathName,InsBy,InsDt,OrdSeq) VALUES (13,'/UserSecurity/Index', '�ʺŹ���', 'SYSTEM', GETDATE(), 1000)
--INSERT SctPath(ApplicationId,PathUrl,PathName,InsBy,InsDt,OrdSeq) VALUES (13,'/UserSecurity/Edit', '����Ȩ��', 'SYSTEM', GETDATE(), 1100)
--INSERT SctPath(ApplicationId,PathUrl,PathName,InsBy,InsDt,OrdSeq) VALUES (13, '/UserSecurity/Add', '����ʺ�', 'SYSTEM', GETDATE(), 1200)

----banner����
--INSERT SctPath(ApplicationId,PathUrl,PathName,InsBy,InsDt,OrdSeq) VALUES (14,'/AdPicture/Index', 'banner����', 'SYSTEM', GETDATE(), 1000)
--INSERT SctPath(ApplicationId,PathUrl,PathName,InsBy,InsDt,OrdSeq) VALUES (14,'/AdPicture/Edit', '�޸�banner', 'SYSTEM', GETDATE(), 1100)
--INSERT SctPath(ApplicationId,PathUrl,PathName,InsBy,InsDt,OrdSeq) VALUES (14, '/AdPicture/Add', '���banner', 'SYSTEM', GETDATE(), 1200)



----һ�����ද��
--INSERT SccAction(PathId, ActionName) VALUES (1,'�鿴')
--INSERT SccAction(PathId, ActionName) VALUES (2,'�޸�')
--INSERT SccAction(PathId, ActionName) VALUES (3,'���')

----�������ද��
--INSERT SccAction(PathId, ActionName) VALUES (4,'�鿴')
--INSERT SccAction(PathId, ActionName) VALUES (5,'�޸�')
--INSERT SccAction(PathId, ActionName) VALUES (6,'���')

----Ʒ�ƶ���
--INSERT SccAction(PathId, ActionName) VALUES (7,'�鿴')
--INSERT SccAction(PathId, ActionName) VALUES (8,'�޸�')
--INSERT SccAction(PathId, ActionName) VALUES (9,'���')

----��Ʒ����
--INSERT SccAction(PathId, ActionName) VALUES (10,'�鿴')
--INSERT SccAction(PathId, ActionName) VALUES (11,'�޸�')
--INSERT SccAction(PathId, ActionName) VALUES (12,'���')

----��������
--INSERT SccAction(PathId, ActionName) VALUES (13,'�鿴')
--INSERT SccAction(PathId, ActionName) VALUES (14,'�޸�')
--INSERT SccAction(PathId, ActionName) VALUES (15,'ͳ��')

----�ͻ�����
--INSERT SccAction(PathId, ActionName) VALUES (16,'�鿴')
--INSERT SccAction(PathId, ActionName) VALUES (17,'�޸�')

----�ʺŶ���
--INSERT SccAction(PathId, ActionName) VALUES (18,'�鿴')
--INSERT SccAction(PathId, ActionName) VALUES (19,'����Ȩ��')
--INSERT SccAction(PathId, ActionName) VALUES (20,'����ʺ�')

----banner����
--INSERT SccAction(PathId, ActionName) VALUES (21,'�鿴')
--INSERT SccAction(PathId, ActionName) VALUES (22,'�޸�')
--INSERT SccAction(PathId, ActionName) VALUES (23,'���')

------��ʼ�˻�
--INSERT ScmUser(UserName,LoginName,Password,UserPhone,UserCd,SourceCd,InsBy,InsDt,UpdBy,UpdDt,Recsts) VALUES('test123�û�','test123','ygtest123','','SENIOR','WEB','SYSTEM',GETDATE(),'SYSTEM',GETDATE(),'0')
--INSERT ScmUser(UserName,LoginName,Password,UserPhone,UserCd,SourceCd,InsBy,InsDt,UpdBy,UpdDt,Recsts)  VALUES('admin�û�','admin123','ygadmin123','','SENIOR','WEB','SYSTEM',GETDATE(),'SYSTEM',GETDATE(),'0')

-----����������ͨ�û�
--INSERT ScmUser(UserName,LoginName,Password,UserPhone,UserCd,SourceCd,InsBy,InsDt,UpdBy,UpdDt,Recsts) VALUES('user1�û�','user1','123456','13264232563','NORMAL','WEB','SYSTEM',GETDATE(),'SYSTEM',GETDATE(),'0')
--INSERT ScmUser(UserName,LoginName,Password,UserPhone,UserCd,SourceCd,InsBy,InsDt,UpdBy,UpdDt,Recsts) VALUES('user2�û�','user2','123456','13264232563','NORMAL','WEB','SYSTEM',GETDATE(),'SYSTEM',GETDATE(),'0')


--INSERT SccRole(RoleName,SDesc) VALUES ('��ͨ����Ա','��ͨ����Ա')
--INSERT SccRole(RoleName,SDesc) VALUES ('��������Ա','��������Ա')

--INSERT SccUserInRole(UserId, RoleId) VALUES (1,1)
--INSERT SccUserInRole(UserId, RoleId) VALUES (2,2)



-------��ͨ�û�Ȩ��
----һ������
--INSERT SccActionInRole(ActionId,RoleId) VALUES (1,1)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (2,1)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (3,1)
----��������
--INSERT SccActionInRole(ActionId,RoleId) VALUES (4,1)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (5,1)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (6,1)
----Ʒ�ƹ���
--INSERT SccActionInRole(ActionId,RoleId) VALUES (7,1)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (8,1)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (9,1)
----��Ʒ����
--INSERT SccActionInRole(ActionId,RoleId) VALUES (10,1)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (11,1)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (12,1)
----��������
--INSERT SccActionInRole(ActionId,RoleId) VALUES (13,1)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (14,1)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (15,1)
----�ͻ�����
--INSERT SccActionInRole(ActionId,RoleId) VALUES (16,1)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (17,1)
----banner����
--INSERT SccActionInRole(ActionId,RoleId) VALUES (21,1)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (22,1)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (23,1)

-------����Ա�û�Ȩ��
----һ������
--INSERT SccActionInRole(ActionId,RoleId) VALUES (1,2)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (2,2)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (3,2)
----��������
--INSERT SccActionInRole(ActionId,RoleId) VALUES (4,2)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (5,2)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (6,2)
----Ʒ�ƹ���
--INSERT SccActionInRole(ActionId,RoleId) VALUES (7,2)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (8,2)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (9,2)
----��Ʒ����
--INSERT SccActionInRole(ActionId,RoleId) VALUES (10,2)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (11,2)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (12,2)
----��������
--INSERT SccActionInRole(ActionId,RoleId) VALUES (13,2)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (14,2)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (15,2)
----�ͻ�����
--INSERT SccActionInRole(ActionId,RoleId) VALUES (16,2)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (17,2)
----�˻�����
--INSERT SccActionInRole(ActionId,RoleId) VALUES (18,2)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (19,2)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (20,2)

----banner����
--INSERT SccActionInRole(ActionId,RoleId) VALUES (21,2)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (22,2)
--INSERT SccActionInRole(ActionId,RoleId) VALUES (23,2)










--INSERT SctAppVersion(SourceCd,Version,IsMandatory,UrlAddress,SDesc,Recsts,InsBy,InsDt,VersionCode) VALUES ('ANDROID','1.0',0,'http://www.chaoshirukou.cn/download/SuperMarketEnter1.2.2.apk?sukey=fea97b85a98d7f78868b7321f9861c6eddc29eebc94103b3461196f575258584cb130b9bd27573ac52ea0ce46285c950','�������ݣ�'+CHAR(10)+CHAR(13)+'1.�����Ǹ��µ�����1 '+CHAR(10)+CHAR(13)+' 2.�����Ǹ��µ�����2','0','SYSTEM',GETDATE(),1)
--INSERT SctAppVersion(SourceCd,Version,IsMandatory,UrlAddress,SDesc,Recsts,InsBy,InsDt,VersionCode) VALUES ('IPHONE','1.0',0,'http://www.chaoshirukou.cn/download/SuperMarketEnter1.2.2.apk?sukey=fea97b85a98d7f78868b7321f9861c6eddc29eebc94103b3461196f575258584cb130b9bd27573ac52ea0ce46285c950','�������ݣ�'+CHAR(10)+CHAR(13)+'1.�����Ǹ��µ�����1 '+CHAR(10)+CHAR(13)+' 2.�����Ǹ��µ�����2','0','SYSTEM',GETDATE(),1)
--INSERT SctUserAddress(UserId,CustomerName,CustomerPhone,Gender,ProvinceId,CityId,CountyId,AddressDetial,InsBy,InsDt,Recsts) 
-- VALUES (3,'��ַ����','13264232563','1',15,147,1384,'ʮ��·���ϼһ�ׯ��','SYSTEM',GETDATE(),'0')


-------------�������ɴ洢����
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

--���ô洢����

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
----ROLEID 1 ��ͨ 2����
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

-----����ͳ��
--SELECT SMU.Id,SMU.UserName,SMU.UserPhone,STO.Id AS OrderId FROM ScmUser AS SMU LEFT JOIN 
--(SELECT Id,UserId FROM SctSkuOrder) AS STO ON STO.UserId = SMU.Id

--SELECT TOP 10 * FROM ScmUser
----1	1	�û���Դ��
----2	1	֧����ʽ��
----3	1	֧��״̬��
----4	1	����״̬��
----5	1	��Ʊ������
----6	1	�������ŷ���״̬��
----7	1	��Ʒ��λ����
----8	1	�û�������
--SELECT TOP 10 * FROM SysRefCd WHERE RefGrpId=8



--SELECT TOP 10 * FROM SctUserAddress

--SELECT TOP 10 * FROM SctSkuOrder

----INSERT SctSkuOrder(UserId,OrderNo,SourceCd,UserAddressId,CustomerName,CustomerPhone,SendCd,PaymentMethodCd,PaymentStatusCd,OrderAmount,
---- OrderCd,DeliveryDt,DeliveryAddress,InvoiceCd,InvoiceName,InsBy,InsDt,CustomerDesc,FAQDesc)
---- VALUES (4,'100046252','WEB',1,'������','13264232563','UNSENT','COD','NOTPAID',130,'GENERATE',GETDATE(),
---- 'ɽ��ʡ������������ʮ��·���ϼһ�ׯ��','PERSON','','SYSTEM',GETDATE(),'���','')

--SELECT * FROM ScmUser

----��������ͳ����ͼ
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
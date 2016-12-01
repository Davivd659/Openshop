
--配置模块
INSERT SysMod(ModNm,ModCd,InsBy,InsDt,UpdBy,UpdDt,Owner,SDesc) VALUES ('食材联盟', 'SC', 'SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '盐光', '盐光 食材联盟系统')


--配置组
INSERT SysRefGrp(ModId,RefGrpNm,InsBy,InsDt,UpdBy,UpdDt) VALUES ('1', '用户来源组', 'SYSTEM', GETDATE(), 'SYSTEM', GETDATE())
INSERT SysRefGrp(ModId,RefGrpNm,InsBy,InsDt,UpdBy,UpdDt) VALUES ('1', '支付方式组', 'SYSTEM', GETDATE(), 'SYSTEM', GETDATE())
INSERT SysRefGrp(ModId,RefGrpNm,InsBy,InsDt,UpdBy,UpdDt) VALUES ('1', '支付状态组', 'SYSTEM', GETDATE(), 'SYSTEM', GETDATE())
INSERT SysRefGrp(ModId,RefGrpNm,InsBy,InsDt,UpdBy,UpdDt) VALUES ('1', '订单状态组', 'SYSTEM', GETDATE(), 'SYSTEM', GETDATE())
INSERT SysRefGrp(ModId,RefGrpNm,InsBy,InsDt,UpdBy,UpdDt) VALUES ('1', '发票类型组', 'SYSTEM', GETDATE(), 'SYSTEM', GETDATE())
INSERT SysRefGrp(ModId,RefGrpNm,InsBy,InsDt,UpdBy,UpdDt) VALUES ('1', '订单短信发送状态组', 'SYSTEM', GETDATE(), 'SYSTEM', GETDATE())
INSERT SysRefGrp(ModId,RefGrpNm,InsBy,InsDt,UpdBy,UpdDt) VALUES ('1', '商品单位分组', 'SYSTEM', GETDATE(), 'SYSTEM', GETDATE())
INSERT SysRefGrp(ModId,RefGrpNm,InsBy,InsDt,UpdBy,UpdDt) VALUES ('1', '用户类型组', 'SYSTEM', GETDATE(), 'SYSTEM', GETDATE())
INSERT SysRefGrp(ModId,RefGrpNm,InsBy,InsDt,UpdBy,UpdDt) VALUES ('1', '储存方式分组', 'SYSTEM', GETDATE(), 'SYSTEM', GETDATE())


------ 配置引用 
--用户来源组
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (1,'WEB','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '网站来源', '', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (1,'ANDROID','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '安卓', '', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (1,'IPHONE','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '苹果', '', '', '')
--支付方式组
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (2,'COD','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '货到付款', '', '', '')
--支付状态组
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (3,'NOTPAID','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '未支付', '', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (3,'PAID','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '已支付', '', '', '')
--订单状态组
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (4,'GENERATE','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '订单生成中', '', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (4,'RECEIVE','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '订单接受', '', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (4,'SUCCESS','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '订单完成', '', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (4,'FAIL','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '订单失败', '', '', '')
--发票类型组
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (5,'PERSON','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '个人', '', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (5,'COMPANY','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '公司', '', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (5,'NONE','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '无', '', '', '')
--订单短信发送状态组
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (6,'UNSENT','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '未发送', '', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (6,'SUCCESS','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '发送成功', '', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (6,'FAIL','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '发送失败', '', '', '')
--商品单位分组
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (7,'PING','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '瓶', '1000', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (7,'XIANG','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '箱', '1100', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (7,'GE','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '个', '1200', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (7,'JIAN','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '件', '1300', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (7,'DAI','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '袋', '1400', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (7,'HE','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '盒', '1500', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (7,'TONG','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '桶', '1600', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (7,'BAO','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '包', '1700', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (7,'TING','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '听', '1800', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (7,'KE','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '克', '1900', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (7,'QIANKE','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '千克', '2000', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (7,'JIN','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '斤', '2100', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (7,'ZHI','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '只', '2200', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (7,'TIAO','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '条', '2300', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (7,'KUAI','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '块', '2400', '', '')


--用户类型组
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (8,'NORMAL','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '普通用户', '1000', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (8,'SENIOR','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '管理员用户（只是标识不做权限的判断）', '1100', '', '')
--储存方式分组
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (9,'NORMAL','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '常温', '1000', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (9,'REFRIGERATE','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '冷藏', '1100', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (9,'FREEZE','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '冷冻', '1200', '', '')
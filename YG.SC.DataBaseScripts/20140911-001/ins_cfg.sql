
--����ģ��
INSERT SysMod(ModNm,ModCd,InsBy,InsDt,UpdBy,UpdDt,Owner,SDesc) VALUES ('ʳ������', 'SC', 'SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '�ι�', '�ι� ʳ������ϵͳ')


--������
INSERT SysRefGrp(ModId,RefGrpNm,InsBy,InsDt,UpdBy,UpdDt) VALUES ('1', '�û���Դ��', 'SYSTEM', GETDATE(), 'SYSTEM', GETDATE())
INSERT SysRefGrp(ModId,RefGrpNm,InsBy,InsDt,UpdBy,UpdDt) VALUES ('1', '֧����ʽ��', 'SYSTEM', GETDATE(), 'SYSTEM', GETDATE())
INSERT SysRefGrp(ModId,RefGrpNm,InsBy,InsDt,UpdBy,UpdDt) VALUES ('1', '֧��״̬��', 'SYSTEM', GETDATE(), 'SYSTEM', GETDATE())
INSERT SysRefGrp(ModId,RefGrpNm,InsBy,InsDt,UpdBy,UpdDt) VALUES ('1', '����״̬��', 'SYSTEM', GETDATE(), 'SYSTEM', GETDATE())
INSERT SysRefGrp(ModId,RefGrpNm,InsBy,InsDt,UpdBy,UpdDt) VALUES ('1', '��Ʊ������', 'SYSTEM', GETDATE(), 'SYSTEM', GETDATE())
INSERT SysRefGrp(ModId,RefGrpNm,InsBy,InsDt,UpdBy,UpdDt) VALUES ('1', '�������ŷ���״̬��', 'SYSTEM', GETDATE(), 'SYSTEM', GETDATE())
INSERT SysRefGrp(ModId,RefGrpNm,InsBy,InsDt,UpdBy,UpdDt) VALUES ('1', '��Ʒ��λ����', 'SYSTEM', GETDATE(), 'SYSTEM', GETDATE())
INSERT SysRefGrp(ModId,RefGrpNm,InsBy,InsDt,UpdBy,UpdDt) VALUES ('1', '�û�������', 'SYSTEM', GETDATE(), 'SYSTEM', GETDATE())
INSERT SysRefGrp(ModId,RefGrpNm,InsBy,InsDt,UpdBy,UpdDt) VALUES ('1', '���淽ʽ����', 'SYSTEM', GETDATE(), 'SYSTEM', GETDATE())


------ �������� 
--�û���Դ��
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (1,'WEB','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '��վ��Դ', '', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (1,'ANDROID','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '��׿', '', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (1,'IPHONE','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', 'ƻ��', '', '', '')
--֧����ʽ��
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (2,'COD','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '��������', '', '', '')
--֧��״̬��
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (3,'NOTPAID','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', 'δ֧��', '', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (3,'PAID','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '��֧��', '', '', '')
--����״̬��
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (4,'GENERATE','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '����������', '', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (4,'RECEIVE','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '��������', '', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (4,'SUCCESS','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '�������', '', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (4,'FAIL','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '����ʧ��', '', '', '')
--��Ʊ������
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (5,'PERSON','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '����', '', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (5,'COMPANY','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '��˾', '', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (5,'NONE','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '��', '', '', '')
--�������ŷ���״̬��
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (6,'UNSENT','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', 'δ����', '', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (6,'SUCCESS','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '���ͳɹ�', '', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (6,'FAIL','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '����ʧ��', '', '', '')
--��Ʒ��λ����
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (7,'PING','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', 'ƿ', '1000', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (7,'XIANG','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '��', '1100', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (7,'GE','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '��', '1200', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (7,'JIAN','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '��', '1300', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (7,'DAI','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '��', '1400', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (7,'HE','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '��', '1500', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (7,'TONG','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', 'Ͱ', '1600', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (7,'BAO','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '��', '1700', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (7,'TING','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '��', '1800', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (7,'KE','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '��', '1900', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (7,'QIANKE','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', 'ǧ��', '2000', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (7,'JIN','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '��', '2100', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (7,'ZHI','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', 'ֻ', '2200', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (7,'TIAO','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '��', '2300', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (7,'KUAI','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '��', '2400', '', '')


--�û�������
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (8,'NORMAL','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '��ͨ�û�', '1000', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (8,'SENIOR','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '����Ա�û���ֻ�Ǳ�ʶ����Ȩ�޵��жϣ�', '1100', '', '')
--���淽ʽ����
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (9,'NORMAL','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '����', '1000', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (9,'REFRIGERATE','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '���', '1100', '', '')
INSERT SysRefCd(RefGrpId, RefCd, InsBy, InsDt, UpdBy, UpdDt, Recsts, SDesc, Var1, Var2, Var3) VALUES (9,'FREEZE','SYSTEM', GETDATE(), 'SYSTEM', GETDATE(), '0', '�䶳', '1200', '', '')
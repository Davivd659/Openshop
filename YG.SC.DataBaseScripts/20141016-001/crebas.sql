/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2005                    */
/* Created on:     2014/10/16 19:18:05                          */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SccAction') and o.name = 'FK_SccAction_PathId_On_SctPath')
alter table SccAction
   drop constraint FK_SccAction_PathId_On_SctPath
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SccActionInRole') and o.name = 'FK_SccActionInRole_ActionId_On_SccAction')
alter table SccActionInRole
   drop constraint FK_SccActionInRole_ActionId_On_SccAction
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SccActionInRole') and o.name = 'FK_SccActionInRole_RoleId_On_SccRole')
alter table SccActionInRole
   drop constraint FK_SccActionInRole_RoleId_On_SccRole
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SccActionRestriction') and o.name = 'FK_SccActionRestriction_ActionId_On_SccAction')
alter table SccActionRestriction
   drop constraint FK_SccActionRestriction_ActionId_On_SccAction
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SccActionRestriction') and o.name = 'FK_SccActionRestriction_UserId_On_ScmUser')
alter table SccActionRestriction
   drop constraint FK_SccActionRestriction_UserId_On_ScmUser
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SccSkuGoodsInSkuBrand') and o.name = 'FK_SctSkuGoodsInSkuBrand_BrandId_On_SctSkuBrand')
alter table SccSkuGoodsInSkuBrand
   drop constraint FK_SctSkuGoodsInSkuBrand_BrandId_On_SctSkuBrand
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SccSkuGoodsInSkuBrand') and o.name = 'FK_SctSkuGoodsInSkuBrand_GoodsId_On_SctSkuGoods')
alter table SccSkuGoodsInSkuBrand
   drop constraint FK_SctSkuGoodsInSkuBrand_GoodsId_On_SctSkuGoods
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SccSkuGoodsInSkuCategorySecond') and o.name = 'FK_SccSkuGoodsInSkuCategorySecond_CategorySecondId_On_SctSkuCategorySecond')
alter table SccSkuGoodsInSkuCategorySecond
   drop constraint FK_SccSkuGoodsInSkuCategorySecond_CategorySecondId_On_SctSkuCategorySecond
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SccSkuGoodsInSkuCategorySecond') and o.name = 'FK_SccSkuGoodsInSkuCategorySecond_GoodsId_On_SctSkuGoods')
alter table SccSkuGoodsInSkuCategorySecond
   drop constraint FK_SccSkuGoodsInSkuCategorySecond_GoodsId_On_SctSkuGoods
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SccUserInRole') and o.name = 'FK_SCCUSERI_FK_SCCUSE_SCCROLE')
alter table SccUserInRole
   drop constraint FK_SCCUSERI_FK_SCCUSE_SCCROLE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SccUserInRole') and o.name = 'FK_SccUserInRole_UserId_On_ScmUser')
alter table SccUserInRole
   drop constraint FK_SccUserInRole_UserId_On_ScmUser
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SctPath') and o.name = 'FK_SctPath_ApplicationId_On_SctApplication')
alter table SctPath
   drop constraint FK_SctPath_ApplicationId_On_SctApplication
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SctSkuCategorySecond') and o.name = 'FK_SctSkuCategorySecond_CategoryFristId_On_SkuCategoryFrist')
alter table SctSkuCategorySecond
   drop constraint FK_SctSkuCategorySecond_CategoryFristId_On_SkuCategoryFrist
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SctSkuOrder') and o.name = 'FK_SctSkuOrder_UserId_On_ScmUser')
alter table SctSkuOrder
   drop constraint FK_SctSkuOrder_UserId_On_ScmUser
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SctSkuOrderDetail') and o.name = 'FK_SctSkuOrderDetail_OrderId_On_SctSkuOrder')
alter table SctSkuOrderDetail
   drop constraint FK_SctSkuOrderDetail_OrderId_On_SctSkuOrder
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SctSkuOrderLog') and o.name = 'FK_SctSkuOrderLog_OrderId_On_SctSkuOrder')
alter table SctSkuOrderLog
   drop constraint FK_SctSkuOrderLog_OrderId_On_SctSkuOrder
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SctSkuOrderSms') and o.name = 'FK_SctSkuOrderSms_OrderId_On_SctSkuOrder')
alter table SctSkuOrderSms
   drop constraint FK_SctSkuOrderSms_OrderId_On_SctSkuOrder
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SctUserActivity') and o.name = 'FK_SctUserActivity_UserId_On_ScmUser')
alter table SctUserActivity
   drop constraint FK_SctUserActivity_UserId_On_ScmUser
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SctUserAddress') and o.name = 'FK_SctUserAddress_UserId_On_ScmUser')
alter table SctUserAddress
   drop constraint FK_SctUserAddress_UserId_On_ScmUser
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SctUserOAuth') and o.name = 'FK_SctUserOAuth_UserId_On_ScmUser')
alter table SctUserOAuth
   drop constraint FK_SctUserOAuth_UserId_On_ScmUser
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SysRefCd') and o.name = 'FK_SysRefCd_RefGrpId_On_SysRefGrp')
alter table SysRefCd
   drop constraint FK_SysRefCd_RefGrpId_On_SysRefGrp
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SysRefGrp') and o.name = 'FK_SysRefGrp_ModId_On_SysMod')
alter table SysRefGrp
   drop constraint FK_SysRefGrp_ModId_On_SysMod
go

alter table HelloWorld
   drop constraint PK_HELLOWORLD
go

if exists (select 1
            from  sysobjects
           where  id = object_id('HelloWorld')
            and   type = 'U')
   drop table HelloWorld
go

alter table OrderNumberSeeds
   drop constraint PK_ORDERNUMBERSEEDS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('OrderNumberSeeds')
            and   type = 'U')
   drop table OrderNumberSeeds
go

alter table SccAction
   drop constraint PK_SCCACTION
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SccAction')
            and   type = 'U')
   drop table SccAction
go

alter table SccActionInRole
   drop constraint PK_SCCACTIONINROLE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SccActionInRole')
            and   type = 'U')
   drop table SccActionInRole
go

alter table SccActionRestriction
   drop constraint PK_SCCACTIONRESTRICTION
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SccActionRestriction')
            and   type = 'U')
   drop table SccActionRestriction
go

alter table SccRole
   drop constraint PK_SCCROLE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SccRole')
            and   type = 'U')
   drop table SccRole
go

alter table SccSkuGoodsInSkuBrand
   drop constraint PK_SCCSKUGOODSINSKUBRAND
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SccSkuGoodsInSkuBrand')
            and   type = 'U')
   drop table SccSkuGoodsInSkuBrand
go

alter table SccSkuGoodsInSkuCategorySecond
   drop constraint PK_SCCSKUGOODSINSKUCATEGORYSEC
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SccSkuGoodsInSkuCategorySecond')
            and   type = 'U')
   drop table SccSkuGoodsInSkuCategorySecond
go

alter table SccUserInRole
   drop constraint PK_SCCUSERINROLE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SccUserInRole')
            and   type = 'U')
   drop table SccUserInRole
go

alter table ScmUser
   drop constraint AK_UQ_LOGINNAME_SCMUSER
go

alter table ScmUser
   drop constraint PK_SCMUSER
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ScmUser')
            and   type = 'U')
   drop table ScmUser
go

alter table SctAdPicture
   drop constraint PK_SCTADPICTURE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SctAdPicture')
            and   type = 'U')
   drop table SctAdPicture
go

alter table SctAppVersion
   drop constraint PK_SCTAPPVERSION
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SctAppVersion')
            and   type = 'U')
   drop table SctAppVersion
go

alter table SctApplication
   drop constraint PK_SCTAPPLICATION
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SctApplication')
            and   type = 'U')
   drop table SctApplication
go

alter table SctPath
   drop constraint PK_SCTPATH
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SctPath')
            and   type = 'U')
   drop table SctPath
go

alter table SctSkuBrand
   drop constraint PK_SCTSKUBRAND
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SctSkuBrand')
            and   type = 'U')
   drop table SctSkuBrand
go

alter table SctSkuCategoryFirst
   drop constraint PK_SCTSKUCATEGORYFIRST
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SctSkuCategoryFirst')
            and   type = 'U')
   drop table SctSkuCategoryFirst
go

alter table SctSkuCategorySecond
   drop constraint PK_SCTSKUCATEGORYSECOND
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SctSkuCategorySecond')
            and   type = 'U')
   drop table SctSkuCategorySecond
go

alter table SctSkuGoods
   drop constraint PK_SCTSKUGOODS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SctSkuGoods')
            and   type = 'U')
   drop table SctSkuGoods
go

alter table SctSkuGoodsSearchActivity
   drop constraint PK_SCTSKUGOODSSEARCHACTIVITY
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SctSkuGoodsSearchActivity')
            and   type = 'U')
   drop table SctSkuGoodsSearchActivity
go

alter table SctSkuOrder
   drop constraint AK_UQ_ORDERNO_SCTSKUOR
go

alter table SctSkuOrder
   drop constraint PK_SCTSKUORDER
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SctSkuOrder')
            and   type = 'U')
   drop table SctSkuOrder
go

alter table SctSkuOrderDetail
   drop constraint PK_SCTSKUORDERDETAIL
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SctSkuOrderDetail')
            and   type = 'U')
   drop table SctSkuOrderDetail
go

alter table SctSkuOrderLog
   drop constraint PK_SCTSKUORDERLOG
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SctSkuOrderLog')
            and   type = 'U')
   drop table SctSkuOrderLog
go

alter table SctSkuOrderSms
   drop constraint PK_SCTSKUORDERSMS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SctSkuOrderSms')
            and   type = 'U')
   drop table SctSkuOrderSms
go

alter table SctUserActivity
   drop constraint PK_SCTUSERACTIVITY
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SctUserActivity')
            and   type = 'U')
   drop table SctUserActivity
go

alter table SctUserAddress
   drop constraint PK_SCTUSERADDRESS
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SctUserAddress')
            and   type = 'U')
   drop table SctUserAddress
go

alter table SctUserOAuth
   drop constraint PK_SCTUSEROAUTH
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SctUserOAuth')
            and   type = 'U')
   drop table SctUserOAuth
go

alter table SysMod
   drop constraint PK_SYSMOD
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SysMod')
            and   type = 'U')
   drop table SysMod
go

alter table SysRefCd
   drop constraint PK_SYSREFCD
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SysRefCd')
            and   type = 'U')
   drop table SysRefCd
go

alter table SysRefGrp
   drop constraint PK_SYSREFGRP
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SysRefGrp')
            and   type = 'U')
   drop table SysRefGrp
go

/*==============================================================*/
/* Table: HelloWorld                                            */
/*==============================================================*/
create table HelloWorld (
   Id                   int                  identity,
   UserName             varchar(36)          not null,
   Age                  int                  not null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '测试表',
   'user', @CurrentUser, 'table', 'HelloWorld'
go

alter table HelloWorld
   add constraint PK_HELLOWORLD primary key (Id)
go

/*==============================================================*/
/* Table: OrderNumberSeeds                                      */
/*==============================================================*/
create table OrderNumberSeeds (
   Id                   int                  identity,
   OrderNumber          varchar(10)          not null,
   InsDt                varchar(10)          not null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '订单种子表',
   'user', @CurrentUser, 'table', 'OrderNumberSeeds'
go

alter table OrderNumberSeeds
   add constraint PK_ORDERNUMBERSEEDS primary key (Id)
go

/*==============================================================*/
/* Table: SccAction                                             */
/*==============================================================*/
create table SccAction (
   Id                   int                  identity,
   PathId               int                  not null,
   ActionName           varchar(30)          not null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '操作名称',
   'user', @CurrentUser, 'table', 'SccAction'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '动作名称',
   'user', @CurrentUser, 'table', 'SccAction', 'column', 'ActionName'
go

alter table SccAction
   add constraint PK_SCCACTION primary key (Id)
go

/*==============================================================*/
/* Table: SccActionInRole                                       */
/*==============================================================*/
create table SccActionInRole (
   Id                   int                  identity,
   ActionId             int                  not null,
   RoleId               int                  not null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '动作角色关联表',
   'user', @CurrentUser, 'table', 'SccActionInRole'
go

alter table SccActionInRole
   add constraint PK_SCCACTIONINROLE primary key (Id)
go

/*==============================================================*/
/* Table: SccActionRestriction                                  */
/*==============================================================*/
create table SccActionRestriction (
   Id                   int                  identity,
   ActionId             int                  not null,
   UserId               int                  not null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '权限限制表',
   'user', @CurrentUser, 'table', 'SccActionRestriction'
go

alter table SccActionRestriction
   add constraint PK_SCCACTIONRESTRICTION primary key (Id)
go

/*==============================================================*/
/* Table: SccRole                                               */
/*==============================================================*/
create table SccRole (
   Id                   int                  identity,
   RoleName             varchar(36)          not null,
   SDesc                nvarchar(50)         not null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '角色表',
   'user', @CurrentUser, 'table', 'SccRole'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '角色名称',
   'user', @CurrentUser, 'table', 'SccRole', 'column', 'RoleName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '描述',
   'user', @CurrentUser, 'table', 'SccRole', 'column', 'SDesc'
go

alter table SccRole
   add constraint PK_SCCROLE primary key (Id)
go

/*==============================================================*/
/* Table: SccSkuGoodsInSkuBrand                                 */
/*==============================================================*/
create table SccSkuGoodsInSkuBrand (
   Id                   int                  identity,
   GoodsId              int                  not null,
   BrandId              int                  not null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '商品到品牌的关联表',
   'user', @CurrentUser, 'table', 'SccSkuGoodsInSkuBrand'
go

alter table SccSkuGoodsInSkuBrand
   add constraint PK_SCCSKUGOODSINSKUBRAND primary key (Id)
go

/*==============================================================*/
/* Table: SccSkuGoodsInSkuCategorySecond                        */
/*==============================================================*/
create table SccSkuGoodsInSkuCategorySecond (
   Id                   int                  identity,
   GoodsId              int                  not null,
   CategorySecondId     int                  not null,
   InsBy                varchar(36)          not null,
   InsDt                datetime             not null,
   OrdSeq               decimal(10,2)        not null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '商品二级分类关联表',
   'user', @CurrentUser, 'table', 'SccSkuGoodsInSkuCategorySecond'
go

alter table SccSkuGoodsInSkuCategorySecond
   add constraint PK_SCCSKUGOODSINSKUCATEGORYSEC primary key (Id)
go

/*==============================================================*/
/* Table: SccUserInRole                                         */
/*==============================================================*/
create table SccUserInRole (
   Id                   int                  identity,
   UserId               int                  not null,
   RoleId               int                  not null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '用户角色关联表',
   'user', @CurrentUser, 'table', 'SccUserInRole'
go

alter table SccUserInRole
   add constraint PK_SCCUSERINROLE primary key (Id)
go

/*==============================================================*/
/* Table: ScmUser                                               */
/*==============================================================*/
create table ScmUser (
   Id                   int                  identity,
   UserName             varchar(36)          not null,
   LoginName            varchar(36)          not null,
   UserPhone            varchar(11)          null,
   Password             varchar(50)          not null,
   UserCd               varchar(36)          not null,
   SourceCd             varchar(36)          not null,
   Company              nvarchar(200)        null,
   InsBy                varchar(36)          not null,
   InsDt                datetime             not null,
   UpdBy                varchar(36)          not null,
   UpdDt                datetime             not null,
   Recsts               char(1)              not null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '会员表',
   'user', @CurrentUser, 'table', 'ScmUser'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '用户姓名',
   'user', @CurrentUser, 'table', 'ScmUser', 'column', 'UserName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '用户登录名',
   'user', @CurrentUser, 'table', 'ScmUser', 'column', 'LoginName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '用户手机',
   'user', @CurrentUser, 'table', 'ScmUser', 'column', 'UserPhone'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '用户类型(普通用户、管理员)',
   'user', @CurrentUser, 'table', 'ScmUser', 'column', 'UserCd'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '用户归属',
   'user', @CurrentUser, 'table', 'ScmUser', 'column', 'SourceCd'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '用户公司名',
   'user', @CurrentUser, 'table', 'ScmUser', 'column', 'Company'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '0审核成功 1审核中 2审核失败 ',
   'user', @CurrentUser, 'table', 'ScmUser', 'column', 'Recsts'
go

alter table ScmUser
   add constraint PK_SCMUSER primary key (Id)
go

alter table ScmUser
   add constraint AK_UQ_LOGINNAME_SCMUSER unique (LoginName)
go

/*==============================================================*/
/* Table: SctAdPicture                                          */
/*==============================================================*/
create table SctAdPicture (
   Id                   int                  identity,
   ImageName            varchar(50)          not null,
   Url                  varchar(100)         null,
   InsBy                varchar(36)          not null,
   InsDt                datetime             not null,
   Recsts               char(1)              not null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '广告图片',
   'user', @CurrentUser, 'table', 'SctAdPicture'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '图片名称',
   'user', @CurrentUser, 'table', 'SctAdPicture', 'column', 'ImageName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '连接地址',
   'user', @CurrentUser, 'table', 'SctAdPicture', 'column', 'Url'
go

alter table SctAdPicture
   add constraint PK_SCTADPICTURE primary key (Id)
go

/*==============================================================*/
/* Table: SctAppVersion                                         */
/*==============================================================*/
create table SctAppVersion (
   Id                   int                  identity,
   SourceCd             varchar(36)          not null,
   Version              varchar(36)          not null,
   VersionCode          int                  not null,
   IsMandatory          char(1)              not null,
   UrlAddress           varchar(200)         not null,
   SDesc                nvarchar(200)        null,
   Recsts               char(1)              not null,
   InsBy                varchar(36)          not null,
   InsDt                datetime             not null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'app版本库',
   'user', @CurrentUser, 'table', 'SctAppVersion'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '来源',
   'user', @CurrentUser, 'table', 'SctAppVersion', 'column', 'SourceCd'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '版本',
   'user', @CurrentUser, 'table', 'SctAppVersion', 'column', 'Version'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '0 默认 1强制更新',
   'user', @CurrentUser, 'table', 'SctAppVersion', 'column', 'IsMandatory'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新地址',
   'user', @CurrentUser, 'table', 'SctAppVersion', 'column', 'UrlAddress'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '更新内容',
   'user', @CurrentUser, 'table', 'SctAppVersion', 'column', 'SDesc'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '状态位',
   'user', @CurrentUser, 'table', 'SctAppVersion', 'column', 'Recsts'
go

alter table SctAppVersion
   add constraint PK_SCTAPPVERSION primary key (Id)
go

/*==============================================================*/
/* Table: SctApplication                                        */
/*==============================================================*/
create table SctApplication (
   Id                   int                  identity,
   ApplicationName      nvarchar(30)         not null,
   InsBy                varchar(36)          not null,
   InsDt                datetime             not null,
   OrdSeq               decimal(10,2)        not null,
   ParentId             int                  not null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '应用程序',
   'user', @CurrentUser, 'table', 'SctApplication'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '菜单名称',
   'user', @CurrentUser, 'table', 'SctApplication', 'column', 'ApplicationName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '父级Id',
   'user', @CurrentUser, 'table', 'SctApplication', 'column', 'ParentId'
go

alter table SctApplication
   add constraint PK_SCTAPPLICATION primary key (Id)
go

/*==============================================================*/
/* Table: SctPath                                               */
/*==============================================================*/
create table SctPath (
   Id                   int                  identity,
   ApplicationId        int                  not null,
   PathUrl              varchar(100)         not null,
   PathName             varchar(30)          not null,
   InsBy                varchar(36)          not null,
   InsDt                datetime             not null,
   OrdSeq               decimal(10,2)        not null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '功能路径',
   'user', @CurrentUser, 'table', 'SctPath'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '路径',
   'user', @CurrentUser, 'table', 'SctPath', 'column', 'PathUrl'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '路径描述名称',
   'user', @CurrentUser, 'table', 'SctPath', 'column', 'PathName'
go

alter table SctPath
   add constraint PK_SCTPATH primary key (Id)
go

/*==============================================================*/
/* Table: SctSkuBrand                                           */
/*==============================================================*/
create table SctSkuBrand (
   Id                   int                  identity,
   BrandName            nvarchar(30)         not null,
   InsBy                varchar(36)          not null,
   InsDt                datetime             not null,
   Recsts               char(1)              not null,
   OrdSeq               decimal(10,2)        not null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '商品品牌',
   'user', @CurrentUser, 'table', 'SctSkuBrand'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '品牌名称',
   'user', @CurrentUser, 'table', 'SctSkuBrand', 'column', 'BrandName'
go

alter table SctSkuBrand
   add constraint PK_SCTSKUBRAND primary key (Id)
go

/*==============================================================*/
/* Table: SctSkuCategoryFirst                                   */
/*==============================================================*/
create table SctSkuCategoryFirst (
   Id                   int                  identity,
   CategoryName         nvarchar(30)         not null,
   CategoryImg          varchar(50)          not null,
   InsBy                varchar(36)          not null,
   InsDt                datetime             not null,
   Recsts               char(1)              not null,
   OrdSeq               decimal(10,2)        not null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '商品一级分类',
   'user', @CurrentUser, 'table', 'SctSkuCategoryFirst'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '分类名称',
   'user', @CurrentUser, 'table', 'SctSkuCategoryFirst', 'column', 'CategoryName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '分类图标',
   'user', @CurrentUser, 'table', 'SctSkuCategoryFirst', 'column', 'CategoryImg'
go

alter table SctSkuCategoryFirst
   add constraint PK_SCTSKUCATEGORYFIRST primary key (Id)
go

/*==============================================================*/
/* Table: SctSkuCategorySecond                                  */
/*==============================================================*/
create table SctSkuCategorySecond (
   Id                   int                  identity,
   CategoryFirstId      int                  not null,
   CategoryName         nvarchar(30)         not null,
   InsBy                varchar(36)          not null,
   InsDt                datetime             not null,
   Recsts               char(1)              not null,
   OrdSeq               decimal(10,2)        not null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '商品二级分类',
   'user', @CurrentUser, 'table', 'SctSkuCategorySecond'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '二级分类名称',
   'user', @CurrentUser, 'table', 'SctSkuCategorySecond', 'column', 'CategoryName'
go

alter table SctSkuCategorySecond
   add constraint PK_SCTSKUCATEGORYSECOND primary key (Id)
go

/*==============================================================*/
/* Table: SctSkuGoods                                           */
/*==============================================================*/
create table SctSkuGoods (
   Id                   int                  identity,
   GoodsName            nvarchar(50)         not null,
   GoodsPinYin          varchar(200)         null,
   Specification        nvarchar(50)         not null,
   Price                decimal(10,2)        not null,
   ImageName            varchar(50)          not null,
   IsElite              char(1)              not null,
   InsBy                varchar(36)          not null,
   InsDt                datetime             not null,
   UpdBy                varchar(36)          not null,
   UpdDt                datetime             not null,
   Recsts               char(1)              not null,
   OrdSeq               decimal(10,2)        not null,
   UnitCd               varchar(36)          not null,
   Producer             nvarchar(50)         null,
   StorageCd            varchar(36)          null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '商品库存',
   'user', @CurrentUser, 'table', 'SctSkuGoods'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '商品名称',
   'user', @CurrentUser, 'table', 'SctSkuGoods', 'column', 'GoodsName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '拼音',
   'user', @CurrentUser, 'table', 'SctSkuGoods', 'column', 'GoodsPinYin'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '商品规格',
   'user', @CurrentUser, 'table', 'SctSkuGoods', 'column', 'Specification'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '商品价格',
   'user', @CurrentUser, 'table', 'SctSkuGoods', 'column', 'Price'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '图片名称',
   'user', @CurrentUser, 'table', 'SctSkuGoods', 'column', 'ImageName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否推荐',
   'user', @CurrentUser, 'table', 'SctSkuGoods', 'column', 'IsElite'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '控制是否有效(0有效 1无效)',
   'user', @CurrentUser, 'table', 'SctSkuGoods', 'column', 'Recsts'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '单位类型',
   'user', @CurrentUser, 'table', 'SctSkuGoods', 'column', 'UnitCd'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '生产商',
   'user', @CurrentUser, 'table', 'SctSkuGoods', 'column', 'Producer'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '储存code',
   'user', @CurrentUser, 'table', 'SctSkuGoods', 'column', 'StorageCd'
go

alter table SctSkuGoods
   add constraint PK_SCTSKUGOODS primary key (Id)
go

/*==============================================================*/
/* Table: SctSkuGoodsSearchActivity                             */
/*==============================================================*/
create table SctSkuGoodsSearchActivity (
   Id                   int                  identity,
   Keyword              nvarchar(50)         not null,
   ResultCnt            int                  not null,
   InsBy                varchar(36)          not null,
   InsDt                datetime             not null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '搜索记录表',
   'user', @CurrentUser, 'table', 'SctSkuGoodsSearchActivity'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '搜索关键字',
   'user', @CurrentUser, 'table', 'SctSkuGoodsSearchActivity', 'column', 'Keyword'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '返回结果',
   'user', @CurrentUser, 'table', 'SctSkuGoodsSearchActivity', 'column', 'ResultCnt'
go

alter table SctSkuGoodsSearchActivity
   add constraint PK_SCTSKUGOODSSEARCHACTIVITY primary key (Id)
go

/*==============================================================*/
/* Table: SctSkuOrder                                           */
/*==============================================================*/
create table SctSkuOrder (
   Id                   int                  identity,
   UserId               int                  not null,
   OrderNo              varchar(36)          not null,
   SourceCd             varchar(36)          not null,
   UserAddressId        int                  not null,
   CustomerName         nvarchar(30)         not null,
   CustomerPhone        varchar(30)          not null,
   SendCd               varchar(36)          not null,
   PaymentMethodCd      varchar(36)          not null,
   PaymentStatusCd      varchar(36)          not null,
   OrderAmount          decimal(10,2)        not null,
   OrderCd              varchar(36)          not null,
   DeliveryDt           datetime             not null,
   DeliveryAddress      nvarchar(300)        not null,
   InvoiceCd            varchar(36)          not null,
   InvoiceName          nvarchar(100)        not null,
   InsBy                varchar(36)          not null,
   InsDt                datetime             not null,
   CustomerDesc         nvarchar(200)        not null,
   FAQDesc              nvarchar(200)        null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '订单表',
   'user', @CurrentUser, 'table', 'SctSkuOrder'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '订单号',
   'user', @CurrentUser, 'table', 'SctSkuOrder', 'column', 'OrderNo'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '订单来源',
   'user', @CurrentUser, 'table', 'SctSkuOrder', 'column', 'SourceCd'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '收货人姓名',
   'user', @CurrentUser, 'table', 'SctSkuOrder', 'column', 'CustomerName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '收货电话',
   'user', @CurrentUser, 'table', 'SctSkuOrder', 'column', 'CustomerPhone'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '发送状态',
   'user', @CurrentUser, 'table', 'SctSkuOrder', 'column', 'SendCd'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '付款方式',
   'user', @CurrentUser, 'table', 'SctSkuOrder', 'column', 'PaymentMethodCd'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '支付状态',
   'user', @CurrentUser, 'table', 'SctSkuOrder', 'column', 'PaymentStatusCd'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '订单金额',
   'user', @CurrentUser, 'table', 'SctSkuOrder', 'column', 'OrderAmount'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '订单状态',
   'user', @CurrentUser, 'table', 'SctSkuOrder', 'column', 'OrderCd'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '要求送达时间',
   'user', @CurrentUser, 'table', 'SctSkuOrder', 'column', 'DeliveryDt'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '送货地址',
   'user', @CurrentUser, 'table', 'SctSkuOrder', 'column', 'DeliveryAddress'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '发票类型',
   'user', @CurrentUser, 'table', 'SctSkuOrder', 'column', 'InvoiceCd'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '发票抬头',
   'user', @CurrentUser, 'table', 'SctSkuOrder', 'column', 'InvoiceName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '用户备注',
   'user', @CurrentUser, 'table', 'SctSkuOrder', 'column', 'CustomerDesc'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '客服备注',
   'user', @CurrentUser, 'table', 'SctSkuOrder', 'column', 'FAQDesc'
go

alter table SctSkuOrder
   add constraint PK_SCTSKUORDER primary key (Id)
go

alter table SctSkuOrder
   add constraint AK_UQ_ORDERNO_SCTSKUOR unique (OrderNo)
go

/*==============================================================*/
/* Table: SctSkuOrderDetail                                     */
/*==============================================================*/
create table SctSkuOrderDetail (
   Id                   int                  identity,
   OrderId              int                  not null,
   GoodsId              int                  not null,
   GoodsTotal           int                  not null,
   GoodsPrice           decimal(10,2)        not null,
   GoodsTotalPrice      decimal(10,2)        not null,
   GoodsName            nvarchar(50)         not null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '订单详细',
   'user', @CurrentUser, 'table', 'SctSkuOrderDetail'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '商品ID',
   'user', @CurrentUser, 'table', 'SctSkuOrderDetail', 'column', 'GoodsId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '商品数量',
   'user', @CurrentUser, 'table', 'SctSkuOrderDetail', 'column', 'GoodsTotal'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '商品单价',
   'user', @CurrentUser, 'table', 'SctSkuOrderDetail', 'column', 'GoodsPrice'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '商品总价',
   'user', @CurrentUser, 'table', 'SctSkuOrderDetail', 'column', 'GoodsTotalPrice'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '商品名称',
   'user', @CurrentUser, 'table', 'SctSkuOrderDetail', 'column', 'GoodsName'
go

alter table SctSkuOrderDetail
   add constraint PK_SCTSKUORDERDETAIL primary key (Id)
go

/*==============================================================*/
/* Table: SctSkuOrderLog                                        */
/*==============================================================*/
create table SctSkuOrderLog (
   Id                   int                  identity,
   OrderId              int                  not null,
   UserId               int                  not null,
   CloumnCd             varchar(36)          not null,
   OldCd                varchar(36)          not null,
   NewCd                varchar(36)          not null,
   InsBy                varchar(36)          not null,
   InsDt                datetime             not null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '订单操作记录',
   'user', @CurrentUser, 'table', 'SctSkuOrderLog'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '列的名字',
   'user', @CurrentUser, 'table', 'SctSkuOrderLog', 'column', 'CloumnCd'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '旧的RefCd',
   'user', @CurrentUser, 'table', 'SctSkuOrderLog', 'column', 'OldCd'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '新的RefCd',
   'user', @CurrentUser, 'table', 'SctSkuOrderLog', 'column', 'NewCd'
go

alter table SctSkuOrderLog
   add constraint PK_SCTSKUORDERLOG primary key (Id)
go

/*==============================================================*/
/* Table: SctSkuOrderSms                                        */
/*==============================================================*/
create table SctSkuOrderSms (
   Id                   int                  identity,
   OrderId              int                  not null,
   CustomerName         nvarchar(30)         not null,
   CustomerPhone        varchar(30)          null,
   SmsDesc              nvarchar(200)        not null,
   SendCd               varchar(36)          not null,
   InsBy                varchar(36)          not null,
   InsDt                datetime             not null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '订单短信发送',
   'user', @CurrentUser, 'table', 'SctSkuOrderSms'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '发送状态',
   'user', @CurrentUser, 'table', 'SctSkuOrderSms', 'column', 'SendCd'
go

alter table SctSkuOrderSms
   add constraint PK_SCTSKUORDERSMS primary key (Id)
go

/*==============================================================*/
/* Table: SctUserActivity                                       */
/*==============================================================*/
create table SctUserActivity (
   Id                   int                  identity,
   UserId               int                  not null,
   UserName             nvarchar(50)         not null,
   BrowserInfo          varchar(50)          not null,
   IPAddress            varchar(30)          not null,
   InsBy                varchar(36)          not null,
   InsDt                datetime             not null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '用户活动记录表',
   'user', @CurrentUser, 'table', 'SctUserActivity'
go

alter table SctUserActivity
   add constraint PK_SCTUSERACTIVITY primary key (Id)
go

/*==============================================================*/
/* Table: SctUserAddress                                        */
/*==============================================================*/
create table SctUserAddress (
   Id                   int                  identity,
   UserId               int                  not null,
   CustomerName         nvarchar(30)         not null,
   CustomerPhone        varchar(30)          not null,
   Gender               char(1)              not null,
   ProvinceId           int                  not null,
   CityId               int                  not null,
   CountyId             int                  not null,
   AddressDetial        nvarchar(200)        not null,
   InsBy                varchar(36)          not null,
   InsDt                datetime             not null,
   Recsts               char(1)              not null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '用户地址',
   'user', @CurrentUser, 'table', 'SctUserAddress'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '收货姓名',
   'user', @CurrentUser, 'table', 'SctUserAddress', 'column', 'CustomerName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '收货电话',
   'user', @CurrentUser, 'table', 'SctUserAddress', 'column', 'CustomerPhone'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '性别',
   'user', @CurrentUser, 'table', 'SctUserAddress', 'column', 'Gender'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '省',
   'user', @CurrentUser, 'table', 'SctUserAddress', 'column', 'ProvinceId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '市',
   'user', @CurrentUser, 'table', 'SctUserAddress', 'column', 'CityId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '区',
   'user', @CurrentUser, 'table', 'SctUserAddress', 'column', 'CountyId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '详细地址',
   'user', @CurrentUser, 'table', 'SctUserAddress', 'column', 'AddressDetial'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '状态(0 有效1无效2默认 3用户持有)',
   'user', @CurrentUser, 'table', 'SctUserAddress', 'column', 'Recsts'
go

alter table SctUserAddress
   add constraint PK_SCTUSERADDRESS primary key (Id)
go

/*==============================================================*/
/* Table: SctUserOAuth                                          */
/*==============================================================*/
create table SctUserOAuth (
   Id                   int                  identity,
   UserId               int                  not null,
   OtherId              varchar(50)          not null,
   SourceCd             varchar(36)          not null,
   InsBy                varchar(36)          not null,
   InsDt                datetime             not null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '会员外部映射表',
   'user', @CurrentUser, 'table', 'SctUserOAuth'
go

alter table SctUserOAuth
   add constraint PK_SCTUSEROAUTH primary key (Id)
go

/*==============================================================*/
/* Table: SysMod                                                */
/*==============================================================*/
create table SysMod (
   Id                   int                  identity,
   ModNm                varchar(36)          not null,
   ModCd                varchar(36)          not null,
   InsBy                varchar(36)          not null,
   InsDt                datetime             not null,
   UpdBy                varchar(36)          not null,
   UpdDt                datetime             not null,
   Owner                nvarchar(50)         not null,
   SDesc                nvarchar(200)        not null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '系统模块表',
   'user', @CurrentUser, 'table', 'SysMod'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '标识列',
   'user', @CurrentUser, 'table', 'SysMod', 'column', 'Id'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '模块名字',
   'user', @CurrentUser, 'table', 'SysMod', 'column', 'ModNm'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '食材CODE',
   'user', @CurrentUser, 'table', 'SysMod', 'column', 'ModCd'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '模块所有者',
   'user', @CurrentUser, 'table', 'SysMod', 'column', 'Owner'
go

alter table SysMod
   add constraint PK_SYSMOD primary key (Id)
go

/*==============================================================*/
/* Table: SysRefCd                                              */
/*==============================================================*/
create table SysRefCd (
   Id                   int                  identity,
   RefGrpId             int                  not null,
   RefCd                varchar(30)          not null,
   InsBy                varchar(36)          not null,
   InsDt                datetime             not null,
   UpdBy                varchar(36)          not null,
   UpdDt                datetime             not null,
   Recsts               char(1)              not null,
   SDesc                varchar(36)          not null,
   Var1                 varchar(36)          null,
   Var2                 nvarchar(50)         null,
   Var3                 nvarchar(150)        null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '引用Code表',
   'user', @CurrentUser, 'table', 'SysRefCd'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '模块Code',
   'user', @CurrentUser, 'table', 'SysRefCd', 'column', 'RefCd'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '模块描述',
   'user', @CurrentUser, 'table', 'SysRefCd', 'column', 'SDesc'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '预留',
   'user', @CurrentUser, 'table', 'SysRefCd', 'column', 'Var1'
go

alter table SysRefCd
   add constraint PK_SYSREFCD primary key (Id)
go

/*==============================================================*/
/* Table: SysRefGrp                                             */
/*==============================================================*/
create table SysRefGrp (
   Id                   int                  identity,
   ModId                int                  not null,
   RefGrpNm             nvarchar(50)         not null,
   InsBy                varchar(36)          not null,
   InsDt                datetime             not null,
   UpdBy                varchar(36)          not null,
   UpdDt                datetime             not null
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '组',
   'user', @CurrentUser, 'table', 'SysRefGrp'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'SysMod外键',
   'user', @CurrentUser, 'table', 'SysRefGrp', 'column', 'ModId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '组名',
   'user', @CurrentUser, 'table', 'SysRefGrp', 'column', 'RefGrpNm'
go

alter table SysRefGrp
   add constraint PK_SYSREFGRP primary key (Id)
go

alter table SccAction
   add constraint FK_SccAction_PathId_On_SctPath foreign key (PathId)
      references SctPath (Id)
go

alter table SccActionInRole
   add constraint FK_SccActionInRole_ActionId_On_SccAction foreign key (ActionId)
      references SccAction (Id)
go

alter table SccActionInRole
   add constraint FK_SccActionInRole_RoleId_On_SccRole foreign key (RoleId)
      references SccRole (Id)
go

alter table SccActionRestriction
   add constraint FK_SccActionRestriction_ActionId_On_SccAction foreign key (ActionId)
      references SccAction (Id)
go

alter table SccActionRestriction
   add constraint FK_SccActionRestriction_UserId_On_ScmUser foreign key (UserId)
      references ScmUser (Id)
go

alter table SccSkuGoodsInSkuBrand
   add constraint FK_SctSkuGoodsInSkuBrand_BrandId_On_SctSkuBrand foreign key (BrandId)
      references SctSkuBrand (Id)
go

alter table SccSkuGoodsInSkuBrand
   add constraint FK_SctSkuGoodsInSkuBrand_GoodsId_On_SctSkuGoods foreign key (GoodsId)
      references SctSkuGoods (Id)
go

alter table SccSkuGoodsInSkuCategorySecond
   add constraint FK_SccSkuGoodsInSkuCategorySecond_CategorySecondId_On_SctSkuCategorySecond foreign key (CategorySecondId)
      references SctSkuCategorySecond (Id)
go

alter table SccSkuGoodsInSkuCategorySecond
   add constraint FK_SccSkuGoodsInSkuCategorySecond_GoodsId_On_SctSkuGoods foreign key (GoodsId)
      references SctSkuGoods (Id)
go

alter table SccUserInRole
   add constraint FK_SCCUSERI_FK_SCCUSE_SCCROLE foreign key (RoleId)
      references SccRole (Id)
go

alter table SccUserInRole
   add constraint FK_SccUserInRole_UserId_On_ScmUser foreign key (UserId)
      references ScmUser (Id)
go

alter table SctPath
   add constraint FK_SctPath_ApplicationId_On_SctApplication foreign key (ApplicationId)
      references SctApplication (Id)
go

alter table SctSkuCategorySecond
   add constraint FK_SctSkuCategorySecond_CategoryFristId_On_SkuCategoryFrist foreign key (CategoryFirstId)
      references SctSkuCategoryFirst (Id)
go

alter table SctSkuOrder
   add constraint FK_SctSkuOrder_UserId_On_ScmUser foreign key (UserId)
      references ScmUser (Id)
go

alter table SctSkuOrderDetail
   add constraint FK_SctSkuOrderDetail_OrderId_On_SctSkuOrder foreign key (OrderId)
      references SctSkuOrder (Id)
go

alter table SctSkuOrderLog
   add constraint FK_SctSkuOrderLog_OrderId_On_SctSkuOrder foreign key (OrderId)
      references SctSkuOrder (Id)
go

alter table SctSkuOrderSms
   add constraint FK_SctSkuOrderSms_OrderId_On_SctSkuOrder foreign key (OrderId)
      references SctSkuOrder (Id)
go

alter table SctUserActivity
   add constraint FK_SctUserActivity_UserId_On_ScmUser foreign key (UserId)
      references ScmUser (Id)
go

alter table SctUserAddress
   add constraint FK_SctUserAddress_UserId_On_ScmUser foreign key (UserId)
      references ScmUser (Id)
go

alter table SctUserOAuth
   add constraint FK_SctUserOAuth_UserId_On_ScmUser foreign key (UserId)
      references ScmUser (Id)
go

alter table SysRefCd
   add constraint FK_SysRefCd_RefGrpId_On_SysRefGrp foreign key (RefGrpId)
      references SysRefGrp (Id)
go

alter table SysRefGrp
   add constraint FK_SysRefGrp_ModId_On_SysMod foreign key (ModId)
      references SysMod (Id)
go


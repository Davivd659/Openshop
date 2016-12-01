/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2005                    */
/* Created on:     2014/9/29 11:17:43                           */
/*==============================================================*/


alter table SctSkuGoodsSearchActivity
   drop constraint PK_SCTSKUGOODSSEARCHACTIVITY
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SctSkuGoodsSearchActivity')
            and   type = 'U')
   drop table SctSkuGoodsSearchActivity
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


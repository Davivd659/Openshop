/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2005                    */
/* Created on:     2014/10/13 12:11:10                          */
/*==============================================================*/


alter table SctAppVersion
   drop constraint PK_SCTAPPVERSION
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SctAppVersion')
            and   type = 'U')
   drop table SctAppVersion
go

/*==============================================================*/
/* Table: SctAppVersion                                         */
/*==============================================================*/
create table SctAppVersion (
   Id                   int                  identity,
   SourceCd             varchar(36)          not null,
   Version              varchar(36)          not null,
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
   'app�汾��',
   'user', @CurrentUser, 'table', 'SctAppVersion'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��Դ',
   'user', @CurrentUser, 'table', 'SctAppVersion', 'column', 'SourceCd'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '�汾',
   'user', @CurrentUser, 'table', 'SctAppVersion', 'column', 'Version'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '0 Ĭ�� 1ǿ�Ƹ���',
   'user', @CurrentUser, 'table', 'SctAppVersion', 'column', 'IsMandatory'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '���µ�ַ',
   'user', @CurrentUser, 'table', 'SctAppVersion', 'column', 'UrlAddress'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '��������',
   'user', @CurrentUser, 'table', 'SctAppVersion', 'column', 'SDesc'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '״̬λ',
   'user', @CurrentUser, 'table', 'SctAppVersion', 'column', 'Recsts'
go

alter table SctAppVersion
   add constraint PK_SCTAPPVERSION primary key (Id)
go


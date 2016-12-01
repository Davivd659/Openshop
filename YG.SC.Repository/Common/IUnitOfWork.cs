﻿
namespace YG.SC.Repository
{
    using System;

    /// <summary>
    /// 接口名称：IUnitOfWork
    /// 命名空间：YG.SC.IRepository
    /// 接口功能：
    /// </summary>
    /// 创建者：孟祺宙
    /// 创建日期：2014/9/11 11:19
    /// 修改者：
    /// 修改时间：
    /// ----------------------------------------------------------------------------------------
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// 事物开始
        /// </summary>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/11 11:21
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        void StartTransaction();

        /// <summary>
        /// 提交事物
        /// </summary>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/11 11:21
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        void CommitTransaction();

    }
}
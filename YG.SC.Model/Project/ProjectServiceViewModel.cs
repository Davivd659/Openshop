using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace YG.SC.Model.Project
{
    /// <summary>
    ///项目服务团队
    /// </summary>
    public class ProjectServiceViewModel
    {
        public int Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [DisplayName("姓名")]
        public string Name { get; set; }
        [DisplayName("所属项目")]
        public string ProjectListId { get; set;  }
        /// <summary>
        /// 所属项目
        /// </summary>
        [DisplayName("所属项目集合")]
        public List<SelectListItem> ProjectList { get; set; }
            /// <summary>
        /// 图片路径
        /// </summary>
        [DisplayName("上传头像")]
        public string PicUrl { get; set; }
        /// <summary>
        /// 是否领导
        /// </summary>
        [DisplayName("是否团队经理")]
        public bool IsVip { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }

        [DisplayName("职位")]
        public string Job { get; set; }
    }

    /// <summary>
    /// 活动申请
    /// </summary>
    public class ApplyActivityModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string  Name { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 类型（个人，公司）
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 申请项目Id
        /// </summary>
        public int ProjectId { get; set; }
    }
}

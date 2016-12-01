using System.Configuration;
using YG.US.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace YG.US.Common
{
    public  class UploadImgPath
    {

        public UploadImgPath(string fileNane)
        {
            FolderName = fileNane ;
        }

        public string FolderName { get; set; }  
        //public  string FolderName
        //{
        //    get { return "SupplierPhoto"; }
        //}


        public string physicalPath;
        /// <summary>
        /// 文件物理路径
        /// </summary>
        public  string PhysicalPath
        {
            get
            {
                if (physicalPath == null)
                {
                    physicalPath = Path.Combine(ConfigurationManager.AppSettings["FileUploadPath"], this.FolderName);
                    if (!System.IO.Directory.Exists(physicalPath))
                    {
                        System.IO.Directory.CreateDirectory(physicalPath);
                    }
                }
                return physicalPath;
            }
        }

        /// <summary>
        /// 文件相对路径
        /// </summary>
        public  string RelativePath
        {
            get { return Path.Combine("/", Path.GetFileName(ConfigurationManager.AppSettings["FileUploadPath"]), this.FolderName).Replace('\\', '/'); }
        }
    }
}
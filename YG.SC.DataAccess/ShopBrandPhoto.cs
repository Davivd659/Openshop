
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------


namespace YG.SC.DataAccess
{

using System;
    using System.Collections.Generic;
    
public partial class ShopBrandPhoto
{

    public int Id { get; set; }

    public int BrandId { get; set; }

    public string Photo { get; set; }

    public string PhotoSmall { get; set; }

    public string PhotoSquare { get; set; }

    public string PhotoRectangle { get; set; }

    public int Recsts { get; set; }

    public string Rmark { get; set; }

    public string title { get; set; }



    public virtual ShopBrand ShopBrand { get; set; }

}

}

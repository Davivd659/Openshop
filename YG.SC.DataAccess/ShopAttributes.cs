
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
    
public partial class ShopAttributes
{

    public ShopAttributes()
    {

        this.ShopBrandAttributeValues = new HashSet<ShopBrandAttributeValues>();

        this.ShopProjectAttributesValues = new HashSet<ShopProjectAttributesValues>();

        this.OpenShopAttributeValues = new HashSet<OpenShopAttributeValues>();

    }


    public int Id { get; set; }

    public string AttributeName { get; set; }

    public int DisplaySequence { get; set; }

    public int TypeId { get; set; }

    public int UsageMode { get; set; }

    public bool UseAttributeImage { get; set; }

    public string ImageUrl { get; set; }



    public virtual ICollection<ShopBrandAttributeValues> ShopBrandAttributeValues { get; set; }

    public virtual ICollection<ShopProjectAttributesValues> ShopProjectAttributesValues { get; set; }

    public virtual ICollection<OpenShopAttributeValues> OpenShopAttributeValues { get; set; }

}

}

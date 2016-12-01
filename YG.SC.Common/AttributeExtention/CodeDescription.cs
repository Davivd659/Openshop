using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YG.SC.Common.AttributeExtention
{
    public class CodeDescription
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime EffectiveStartDate { get; set; }
        public DateTime EffectiveEndDate { get; set; }

        public CodeDescription(string code, string description, string category)
        {
            Code = code;
            Description = description;
            Category = category;
            Id = Guid.NewGuid().ToString();
            Code = code;
            Description = description;
            Category = category;
            EffectiveStartDate = DateTime.MinValue;
            EffectiveEndDate = DateTime.MaxValue;
        }
    }
}

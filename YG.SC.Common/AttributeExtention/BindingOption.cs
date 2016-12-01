using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YG.SC.Common.AttributeExtention
{
    public class BindingOption
    {
        public string OptionalLabel { get; set; }
        public string TextTemplate { get; set; }
        public string ValueTemplate { get; set; }
        public BindingOption()
        {
            OptionalLabel = null;
            TextTemplate = "{Description}";
            ValueTemplate = "{Code}";

        }
    }
}

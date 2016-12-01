using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace YG.SC.Common.AttributeExtention
{
    public static class CodeManager
    {
        public static CodeDescription[] codes;

        public static Collection<CodeDescription> GetCodes(string category)
        {
            Collection<CodeDescription> codeCollection = new Collection<CodeDescription>();
            foreach (var code in codes.Where(code => code.Category == category))
            {
                codeCollection.Add(code);
            }
            return codeCollection;

        }

    }
    public class CodeCollection : Collection<CodeDescription>
    {
        public IEnumerable<CodeDescription> GetEffectiveCodes()
        {
            return this.Where(code => code.EffectiveStartDate <= DateTime.Today && code.EffectiveEndDate >= DateTime.Today).ToList();
        }
    }
}

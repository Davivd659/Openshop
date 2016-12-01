using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YG.SC.Model.Project
{
   public class CustomerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public Nullable<bool> Online { get; set; }
        public string LoginName { get; set; }
        public string Email { get; set; }
        public bool Property { get; set; }
        public Nullable<int> AttentionId { get; set; }
    }
}

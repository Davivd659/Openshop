using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YG.SC.OpenShop
{
    public class EndDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime? dt = null;
                try
                {
                    dt = Convert.ToDateTime(value);
                }
                catch (Exception ex)
                {
                }
                if (dt.HasValue)
                {
                    if (dt.Value < DateTime.Now)
                    {
                        return new ValidationResult(validationContext.DisplayName + "不得早于今天。");
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}
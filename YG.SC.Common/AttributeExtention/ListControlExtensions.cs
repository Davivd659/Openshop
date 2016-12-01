using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace YG.SC.Common.AttributeExtention
{
    public static class ListControlExtensions
    {
        public static MvcHtmlString RadioButtonList(this HtmlHelper htmlHelper, string name, string codeCategory,object htmlAttributes=null,
            RepeatDirection repeatDirection = RepeatDirection.Horizontal)

        {
            var codes = CodeManager.GetCodes(codeCategory);
            return ListControlUtil.GenerateHtml(name, codes, repeatDirection, "radio", htmlAttributes, null);
        }

        public static MvcHtmlString RadioButtonListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, string codeCategory, object htmlAttributes = null,
            RepeatDirection repeatDirection = RepeatDirection.Horizontal)
        {
            var codes = CodeManager.GetCodes(codeCategory);
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression,
                htmlHelper.ViewData);

            string name = ExpressionHelper.GetExpressionText(expression);

            string fullHtmlFieldName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);

            return ListControlUtil.GenerateHtml(fullHtmlFieldName, codes, repeatDirection, "radio", metadata.Model,HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));

        }

        public static MvcHtmlString CheckBoxList(this HtmlHelper htmlHelper, string name, string codeCategory, object htmlAttributes = null,
            RepeatDirection repeatDirection = RepeatDirection.Horizontal)
        {
            var codes = CodeManager.GetCodes(codeCategory);
            return ListControlUtil.GenerateHtml(name, codes, repeatDirection, "checkbox",htmlAttributes, null);
        }

        public static MvcHtmlString CheckBoxListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, string codeCategory, object htmlAttributes = null,
            RepeatDirection repeatDirection = RepeatDirection.Horizontal)
        {
            var codes = CodeManager.GetCodes(codeCategory);
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression,
                htmlHelper.ViewData);
            string name = ExpressionHelper.GetExpressionText(expression);
            string fullHtmlFieldName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
            return ListControlUtil.GenerateHtml(fullHtmlFieldName, codes, repeatDirection, "checkbox", metadata.Model,
                HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }
    }
}

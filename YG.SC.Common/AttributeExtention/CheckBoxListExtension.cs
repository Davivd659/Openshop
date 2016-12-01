using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace YG.SC.Common.AttributeExtention
{
    public class InputListItem
    {

        public MvcHtmlString Button { get; set; }

        public string Text { get; set; }

    }

    public static class CheckBoxListExtension
    {
        #region CheckBoxList

        public static MvcHtmlString CheckBoxListFor<TModel, TProperty>(

            this HtmlHelper<TModel> html,

            Expression<Func<TModel, TProperty>> expression,

            IEnumerable<SelectListItem> selectList,

            Func<dynamic, object> format,

            object htmlAttributes)
        {

            return CheckBoxListFor(html, expression, selectList, format, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));

        }

        public static MvcHtmlString CheckBoxListFor<TModel, TProperty>(

            this HtmlHelper<TModel> html,

            Expression<Func<TModel, TProperty>> expression,

            IEnumerable<SelectListItem> selectList,

            Func<dynamic, object> format = null,

            IDictionary<string, object> htmlAttributes = null)
        {

            return CheckBoxList(html, GetName(expression), selectList, format, htmlAttributes);

        }

        public static MvcHtmlString CheckBoxList(

          this HtmlHelper html,

          string name,

          IEnumerable<SelectListItem> selectList,

          Func<dynamic, object> format,

          object htmlAttributes)
        {

            return CheckBoxList(html, name, selectList, format, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));

        }

        public static MvcHtmlString CheckBoxList(

           this HtmlHelper html,

           string name,

           IEnumerable<SelectListItem> selectList,

           Func<dynamic, object> format = null,

           IDictionary<string, object> htmlAttributes = null)
        {

            return InputListInternal(html, name, selectList, true, format, htmlAttributes);

        }

        #endregion

        #region RadioButtonList

        public static MvcHtmlString RadioButtonList(

         this HtmlHelper html,

         string name,

         IEnumerable<SelectListItem> selectList,

         Func<dynamic, object> format,

         object htmlAttributes)
        {

            return RadioButtonList(html, name, selectList, format, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));

        }

        public static MvcHtmlString RadioButtonList(

         this HtmlHelper html,

         string name,

         IEnumerable<SelectListItem> selectList,

         Func<dynamic, object> format = null,

         IDictionary<string, object> htmlAttributes = null)
        {

            return InputListInternal(html, name, selectList, false, format, htmlAttributes);

        }

        public static MvcHtmlString RadioButtonListFor<TModel, TProperty>(

          this HtmlHelper<TModel> html,

          Expression<Func<TModel, TProperty>> expression,

          IEnumerable<SelectListItem> selectList,

          Func<dynamic, object> format,

          object htmlAttributes)
        {

            return RadioButtonList(html, GetName(expression), selectList, format, htmlAttributes);

        }

        public static MvcHtmlString RadioButtonListFor<TModel, TProperty>(

            this HtmlHelper<TModel> html,

            Expression<Func<TModel, TProperty>> expression,

            IEnumerable<SelectListItem> selectList,

            Func<dynamic, object> format = null,

            IDictionary<string, object> htmlAttributes = null)
        {

            return RadioButtonList(html, GetName(expression), selectList, format, htmlAttributes);

        }

        #endregion

        /*-------------------------------------

         * Core Function

         --------------------------------------*/

        private static MvcHtmlString InputListInternal(

            this HtmlHelper html,

            string name,

            IEnumerable<SelectListItem> selectList,

            bool allowMultiple,

            Func<dynamic, object> format,

            IDictionary<string, object> htmlAttributes

           )
        {

            string fullHtmlFieldName = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);

            if (string.IsNullOrEmpty(fullHtmlFieldName))
            {

                throw new ArgumentException("filed can't be null or empty !", "name");

            }

            if (format == null)

                format = i => i.Button + i.Text + "<br/>";

            StringBuilder strBuilder = new StringBuilder();

            TagBuilder tagBuilder = new TagBuilder("input");

            foreach (var item in selectList)
            {   //Clear first

                tagBuilder.InnerHtml = string.Empty;

                if (allowMultiple)
                {

                    tagBuilder.MergeAttribute("type", "checkbox", true);

                }

                else
                {

                    tagBuilder.MergeAttribute("type", "radio", true);

                }

                tagBuilder.MergeAttribute("value", item.Value, true);

                if (item.Selected)

                    tagBuilder.MergeAttribute("selected", "selected", true);

                tagBuilder.MergeAttributes<string, object>(htmlAttributes);

                tagBuilder.MergeAttribute("name", fullHtmlFieldName, true);

                var btnHtmlString = new MvcHtmlString(tagBuilder.ToString());

                var inputItem = new InputListItem { Button = btnHtmlString, Text = item.Text };

                var s = format(inputItem).ToString();

                strBuilder.Append(s);

            }

            return new MvcHtmlString(strBuilder.ToString());

        }

        private static string GetName(LambdaExpression expression)
        {

            if (expression == null)
            {

                throw new ArgumentNullException("expression");

            }

            return ExpressionHelper.GetExpressionText(expression);

        }

    }
}

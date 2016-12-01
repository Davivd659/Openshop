using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Web.WebPages;

namespace YG.SC.Common.AttributeExtention
{
    public static class ControlsExtention
    {


        #region private enum

        /// <summary>

        /// InputType enum

        /// </summary>

        private enum InputType
        {

            /// <summary>

            /// Html5 input attribute

            /// </summary>

            Color, Date, DateTime, DateTime_Local, Email, Month, Number, Range, Search, Tel, Text, Time, Url, Week

        }

        #endregion



        #region Public static method

        /// <summary>

        /// Colors the specified helper.

        /// </summary>

        /// <param name="helper">The helper.</param>

        /// <param name="name">The name.</param>

        /// <param name="value">The value.</param>

        /// <param name="attributes">The attributes.</param>

        /// <returns>IHtmlString</returns>

        public static IHtmlString Color(this HtmlHelper helper, string name, object value = null, object attributes = null)
        {

            if (name.IsEmpty())
            {

                throw new ArgumentException("Value cannot be null or an empty string.", "name");

            }

            return BuildInputTag(name, InputType.Color, value, attributes);

        }



        /// <summary>

        /// Dates the time.

        /// </summary>

        /// <param name="helper">The helper.</param>

        /// <param name="name">The name.</param>

        /// <param name="value">The value.</param>

        /// <param name="attributes">The attributes.</param>

        /// <returns>IHtmlString</returns>

        public static IHtmlString DateTime(this HtmlHelper helper, string name, object value = null, object attributes = null)
        {
            if (name.IsEmpty())
            {
                throw new ArgumentException("Value cannot be null or an empty string.", "name");
            }
            return BuildInputTag(name, InputType.DateTime, value, attributes);
        }

        public static IHtmlString DateTimeFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null,
            RepeatDirection repeatDirection = RepeatDirection.Horizontal)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression,
                htmlHelper.ViewData);

            string name = ExpressionHelper.GetExpressionText(expression);
            if (name.IsEmpty())
            {
                throw new ArgumentException("Value cannot be null or an empty string.", "name");
            }
            return BuildInputTag(name, InputType.DateTime, null, htmlAttributes);
        }


        /// <summary>

        /// Dates the time local.

        /// </summary>

        /// <param name="helper">The helper.</param>

        /// <param name="name">The name.</param>

        /// <param name="value">The value.</param>

        /// <param name="attributes">The attributes.</param>

        /// <returns>IHtmlString</returns>

        public static IHtmlString DateTimeLocal(this HtmlHelper helper, string name, object value = null, object attributes = null)
        {

            if (name.IsEmpty())
            {

                throw new ArgumentException("Value cannot be null or an empty string.", "name");

            }

            return BuildInputTag(name, InputType.DateTime_Local, value, attributes);

        }



        /// <summary>

        /// Emails the specified helper.

        /// </summary>

        /// <param name="helper">The helper.</param>

        /// <param name="name">The name.</param>

        /// <param name="value">The value.</param>

        /// <param name="attributes">The attributes.</param>

        /// <returns>IHtmlString</returns>

        public static IHtmlString Email(this HtmlHelper helper, string name, object value = null, object attributes = null)
        {

            if (name.IsEmpty())
            {

                throw new ArgumentException("Value cannot be null or an empty string.", "name");

            }

            return BuildInputTag(name, InputType.Email, value, attributes);

        }



        /// <summary>

        /// Monthes the specified helper.

        /// </summary>

        /// <param name="helper">The helper.</param>

        /// <param name="name">The name.</param>

        /// <param name="value">The value.</param>

        /// <param name="attributes">The attributes.</param>

        /// <returns>IHtmlString</returns>

        public static IHtmlString Month(this HtmlHelper helper, string name, object value = null, object attributes = null)
        {

            if (name.IsEmpty())
            {

                throw new ArgumentException("Value cannot be null or an empty string.", "name");

            }

            return BuildInputTag(name, InputType.Month, value, attributes);

        }



        /// <summary>

        /// Numbers the specified helper.

        /// </summary>

        /// <param name="helper">The helper.</param>

        /// <param name="name">The name.</param>

        /// <param name="value">The value.</param>

        /// <param name="attributes">The attributes.</param>

        /// <returns>IHtmlString</returns>

        public static IHtmlString Number(this HtmlHelper helper, string name, object value = null, object attributes = null)
        {

            if (name.IsEmpty())
            {

                throw new ArgumentException("Value cannot be null or an empty string.", "name");

            }

            return BuildInputTag(name, InputType.Number, value, attributes);

        }



        /// <summary>

        /// Ranges the specified helper.

        /// </summary>

        /// <param name="helper">The helper.</param>

        /// <param name="name">The name.</param>

        /// <param name="value">The value.</param>

        /// <param name="attributes">The attributes.</param>

        /// <returns>IHtmlString</returns>

        public static IHtmlString Range(this HtmlHelper helper, string name, object value = null, object attributes = null)
        {

            if (name.IsEmpty())
            {

                throw new ArgumentException("Value cannot be null or an empty string.", "name");

            }

            return BuildInputTag(name, InputType.Range, value, attributes);

        }



        /// <summary>

        /// Searches the specified helper.

        /// </summary>

        /// <param name="helper">The helper.</param>

        /// <param name="name">The name.</param>

        /// <param name="value">The value.</param>

        /// <param name="attributes">The attributes.</param>

        /// <returns>IHtmlString</returns>

        public static IHtmlString Search(this HtmlHelper helper, string name, object value = null, object attributes = null)
        {

            if (name.IsEmpty())
            {

                throw new ArgumentException("Value cannot be null or an empty string.", "name");

            }

            return BuildInputTag(name, InputType.Search, value, attributes);

        }



        /// <summary>

        /// Tels the specified helper.

        /// </summary>

        /// <param name="helper">The helper.</param>

        /// <param name="name">The name.</param>

        /// <param name="value">The value.</param>

        /// <param name="attributes">The attributes.</param>

        /// <returns>IHtmlString</returns>

        public static IHtmlString Tel(this HtmlHelper helper, string name, object value = null, object attributes = null)
        {

            if (name.IsEmpty())
            {

                throw new ArgumentException("Value cannot be null or an empty string.", "name");

            }

            return BuildInputTag(name, InputType.Tel, value, attributes);

        }



        /// <summary>

        /// Times the specified helper.

        /// </summary>

        /// <param name="helper">The helper.</param>

        /// <param name="name">The name.</param>

        /// <param name="value">The value.</param>

        /// <param name="attributes">The attributes.</param>

        /// <returns>IHtmlString</returns>

        public static IHtmlString Time(this HtmlHelper helper, string name, object value = null, object attributes = null)
        {

            if (name.IsEmpty())
            {

                throw new ArgumentException("Value cannot be null or an empty string.", "name");

            }

            return BuildInputTag(name, InputType.Time, value, attributes);

        }



        /// <summary>

        /// URLs the specified helper.

        /// </summary>

        /// <param name="helper">The helper.</param>

        /// <param name="name">The name.</param>

        /// <param name="value">The value.</param>

        /// <param name="attributes">The attributes.</param>

        /// <returns>IHtmlString</returns>

        public static IHtmlString Url(this HtmlHelper helper, string name, object value = null, object attributes = null)
        {

            if (name.IsEmpty())
            {

                throw new ArgumentException("Value cannot be null or an empty string.", "name");

            }

            return BuildInputTag(name, InputType.Url, value, attributes);

        }



        /// <summary>

        /// Weeks the specified helper.

        /// </summary>

        /// <param name="helper">The helper.</param>

        /// <param name="name">The name.</param>

        /// <param name="value">The value.</param>

        /// <param name="attributes">The attributes.</param>

        /// <returns>IHtmlString</returns>

        public static IHtmlString Week(this HtmlHelper helper, string name, object value = null, object attributes = null)
        {

            if (name.IsEmpty())
            {

                throw new ArgumentException("Value cannot be null or an empty string.", "name");

            }

            return BuildInputTag(name, InputType.Week, value, attributes);

        }

        #endregion



        #region private static method

        /// <summary>

        /// Builds the input tag.

        /// </summary>

        /// <param name="name">The name.</param>

        /// <param name="inputType">Type of the input.</param>

        /// <param name="value">The value.</param>

        /// <param name="attributes">The attributes.</param>

        /// <returns>IHtmlString</returns>

        private static IHtmlString BuildInputTag(string name, InputType inputType, object value = null, object attributes = null)
        {



            TagBuilder tag = new TagBuilder("input");

            tag.MergeAttribute("type", GetInputTypeString(inputType));



            tag.MergeAttribute("name", name, replaceExisting: true);

            tag.GenerateId(name);

            if (value != null || HttpContext.Current.Request.Form[name] != null)
            {

                value = value != null ? value : HttpContext.Current.Request.Form[name];

                tag.MergeAttribute("value", value.ToString());

            }



            if (attributes != null)
            {

                var dictionary = attributes.GetType()

                 .GetProperties()

                 .ToDictionary(prop => prop.Name, prop => prop.GetValue(attributes, null));

                tag.MergeAttributes(dictionary, replaceExisting: true);

            }

            return new HtmlString(tag.ToString(TagRenderMode.SelfClosing));



        }



        /// <summary>

        /// Gets the input type string.

        /// </summary>

        /// <param name="inputType">Type of the input.</param>

        /// <returns>string</returns>

        private static string GetInputTypeString(InputType inputType)
        {

            if (!Enum.IsDefined(typeof(InputType), inputType))
            {

                inputType = InputType.Text;

            }

            return inputType.ToString().Replace('_', '-').ToLowerInvariant();

        }

        #endregion


    }
}

using System;
using System.ComponentModel.DataAnnotations;
using YG.SC.Common;

namespace YG.SC.Model
{
	/// <summary>
	/// 任务首页发布任务。
	/// </summary>
	public class Mission
	{
		/// <summary>
		/// 姓名。
		/// </summary>
		[Required(ErrorMessage = @"<img src=""/Images/icon/new_red_k.png"" class=""litimg""><span class=""red"">请输入您的姓名</span>")]
		public string PublisherContact { get; set; }

		/// <summary>
		/// 手机。
		/// </summary>
		[Required(ErrorMessage = @"<img src=""/Images/icon/new_red_k.png"" class=""litimg""><span class=""red"">请您填写手机</span>")]
		[RegularExpression(@"1\d{10}", ErrorMessage = @"<img src=""/Images/icon/new_red_k.png"" class=""litimg""><span class=""red"">您填写的手机号码格式有误，请重新填写。</span>")]
		public string PublisherMobile { get; set; }

		/// <summary>
		/// 截止日期。
		/// </summary>
		[Required(ErrorMessage = @"<img src=""/Images/icon/new_red_k.png"" class=""litimg""><span class=""red"">请您填写截止日期。</span>")]
		[RegularExpression(@"\d{4}-\d{1,2}-\d{1,2}", ErrorMessage = @"<img src=""/Images/icon/new_red_k.png"" class=""litimg""><span class=""red"">您填写的截止日期格式有误，请重新填写，正确的格式为“2015-06-25”。</span>")]
		public string LimitDate { get; set; }

		/// <summary>
		/// 任务类型。
		/// </summary>
		[Required(ErrorMessage = @"<img src=""/Images/icon/new_red_k.png"" class=""litimg""><span class=""red"">请选择任务类型。</span>")]
		public int MissionType { get; set; }

		/// <summary>
		/// 赏金。
		/// </summary>
		[Required(ErrorMessage = @"<img src=""/Images/icon/new_red_k.png"" class=""litimg""><span class=""red"">请您填写赏金。</span>")]
		[Range(0, 99999999.99, ErrorMessage = @"<img src=""/Images/icon/new_red_k.png"" class=""litimg""><span class=""red"">赏金太高了，少点吧。</span>")]
		public decimal? TotalPrice { get; set; }

		/// <summary>
		/// 标题。
		/// </summary>
		[Required(ErrorMessage = @"<img src=""/Images/icon/new_red_k.png"" class=""litimg""><span class=""red"">请您输入标题。</span>")]
		public string Title { get; set; }

		/// <summary>
		/// 详情。
		/// </summary>
		[Required(ErrorMessage = @"<img src=""/Images/icon/new_red_k.png"" class=""litimg""><span class=""red"">请您输入任务详情。</span>")]
		public string Description { get; set; }
	}
}

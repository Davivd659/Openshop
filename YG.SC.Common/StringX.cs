using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YG.SC.Common
{
	public static class StringX
	{
		/// <summary>
		/// 截取特定长度。
		/// </summary>
		/// <param name="source"></param>
		/// <param name="targetLenght"></param>
		/// <returns></returns>
		public static string Cut(this string source,int targetLenght)
		{
			if (targetLenght<1)
			{
				throw new Exception("目标长度不能小于1.");
			}
			if (source.Length>targetLenght)
			{
				return source.Substring(0, targetLenght);
			}
			return source;
		}

		/// <summary>
		/// 转日期（必填）。
		/// </summary>
		/// <param name="source"></param>
		/// <returns></returns>
		public static DateTime ToDateTime(this string source)
		{
			DateTime? dt = source.ToDateTimeNullable();
			if (dt==null)
			{
				throw new Exception("转不了日期。");
			}
			return dt.Value;
		}

		/// <summary>
		/// 转日期（可空）。
		/// </summary>
		/// <param name="source"></param>
		/// <returns></returns>
		public static DateTime? ToDateTimeNullable(this string source)
		{
			try
			{
				return DateTime.Parse(source);
			}
			catch (Exception ex)
			{
				return null;
			}
		}
	}
}

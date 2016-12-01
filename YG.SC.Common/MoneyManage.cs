using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Text.RegularExpressions;

namespace YG.SC.Common
{
    /// <summary>
    /// Summary description for MoneyLib.
    /// </summary>
    public class MoneyManage
    {
        public MoneyManage()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// 检查一个字符串是否Decimal,如果不是返回0.00
        /// </summary>
        /// <param name="strInput"></param>
        /// <returns></returns>
        public static string SetNullDecimal(string strInput)
        {
            if (!strInput.Trim().Equals("") && IsNumeric(strInput))
                return strInput;
            return "0.00";
        }

        public static decimal ConvertToDecimal(string strInput)
        {
            return Convert.ToDecimal(SetNullDecimal(strInput));
        }
        /// <summary>
        /// 是否是无符号数字
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsUnsign(string value)
        {
            return Regex.IsMatch(value, @"^\d*[.]?\d*$");
        }
        /// <summary>
        /// 是否是数字型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNumeric(string value)
        {
            return Regex.IsMatch(value, @"^[+-]?\d*[.]?\d*$");
        }

        /// <summary>
        /// 是否是整数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsInt(string value)
        {
            return Regex.IsMatch(value, @"^[+-]?\d*$");
        }

        /// <summary>
        /// 将decimal转为int，并四舍五入
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int DecimalToInt(decimal val)
        {
            int i = Convert.ToInt32(val);
            if ((Convert.ToDouble(val) - i) >= 0.5)
            {
                return i + 1;
            }
            return i;

        }
        #region 将decimal转为string，保留两位小数并四舍五入
        /// <summary>
        /// 将decimal转为string，保留两位小数并四舍五入
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string DecimalToString(decimal val)
        {
            //return decimal.Parse(string.Format("{0:F2}",val)).ToString();
            return string.Format("{0:F2}", val);
        }
        #endregion
        /// <summary>
        /// 根据汇率转化货币
        /// </summary>
        /// <param name="BaseCurrency">金额</param>
        /// <param name="ExchangeRate">汇率</param>
        /// <returns></returns>
        public static decimal ToCurrency(decimal BaseCurrency, decimal ExchangeRate)
        {
            return Math.Round(BaseCurrency * ExchangeRate);
        }


        /// <summary>
        /// 把金额转化为标准的两位小数的字符串
        /// </summary>
        /// <param name="Money">符合double格式的字符串</param>
        /// <returns>有两位小数的数字字符串</returns>
        public static string ToSTDMoney(string Money)
        {
            if (IsNumeric(Money))
            {
                return double.Parse(Money).ToString("n2").Replace(",", "");
            }
            else
            {
                throw new Exception("金额格式不正确！");
            }
        }

        /// <summary> 
        /// 转换数字为人民币大写金额
        /// </summary> 
        /// <param name="Money">金额</param> 
        /// <returns>返回输入金额的大写形式</returns> 
        public static string ToCHNMoney(double Money)
        {
            string str1 = "零壹贰叁肆伍陆柒捌玖";            //0-9所对应的汉字 
            string str2 = "万仟佰拾亿仟佰拾万仟佰拾元角分"; //数字位所对应的汉字 
            string str3 = "";    //从原Money值中取出的值 
            string str4 = "";    //数字的字符串形式 
            string str5 = "";  //人民币大写金额形式 
            int i;    //循环变量 
            int j;    //Money的值乘以100的字符串长度 
            string ch1 = "";    //数字的汉语读法 
            string ch2 = "";    //数字位的汉字读法 
            int nzero = 0;  //用来计算连续的零值是几个 
            int temp;            //从原Money值中取出的值 

            Money = Math.Round(Math.Abs(Money), 2);    //将Money取绝对值并四舍五入取2位小数 
            str4 = ((long)(Money * 100)).ToString();        //将Money乘100并转换成字符串形式 
            j = str4.Length;      //找出最高位 
            if (j > 15) { return "溢出"; }
            str2 = str2.Substring(15 - j);   //取出对应位数的str2的值。如：200.55,j为5所以str2=佰拾元角分 
            // 6 5000 0000.0 
            //循环取出每一位需要转换的值 
            for (i = 0; i < j; i++)
            {
                str3 = str4.Substring(i, 1);          //取出需转换的某一位的值 
                temp = Convert.ToInt32(str3);      //转换为数字 
                if (i != (j - 3) && i != (j - 7) && i != (j - 11) && i != (j - 15))
                {
                    //当所取位数不为元、万、亿、万亿上的数字时 
                    if (str3 == "0")
                    {
                        ch1 = "";
                        ch2 = "";
                        nzero = nzero + 1;
                    }
                    else
                    {
                        if (str3 != "0" && nzero != 0)
                        {
                            ch1 = "零" + str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                    }
                }
                else
                {
                    //该位是万亿，亿，万，元位等关键位 
                    if (str3 != "0" && nzero != 0)
                    {
                        ch1 = "零" + str1.Substring(temp * 1, 1);
                        ch2 = str2.Substring(i, 1);
                        nzero = 0;
                    }
                    else
                    {
                        if (str3 != "0" && nzero == 0)
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            if (str3 == "0" && nzero >= 3)
                            {
                                ch1 = "";
                                ch2 = "";
                                nzero = nzero + 1;
                            }
                            else
                            {
                                if (j >= 11)
                                {
                                    ch1 = "";
                                    nzero = nzero + 1;
                                }
                                else
                                {
                                    ch1 = "";
                                    ch2 = str2.Substring(i, 1);
                                    nzero = nzero + 1;
                                }
                            }
                        }
                    }
                }
                if (i == (j - 11) || i == (j - 3))
                {
                    //如果该位是亿位或元位，则必须写上 
                    ch2 = str2.Substring(i, 1);
                }
                str5 = str5 + ch1 + ch2;

                if (i == j - 1 && str3 == "0")
                {
                    //最后一位（分）为0时，加上“整” 
                    str5 = str5 + '整';
                }
            }
            if (Money == 0)
            {
                str5 = "零元整";
            }
            return str5;
        }

        /// <summary> 
        /// 转换数字为人民币大写金额
        /// </summary> 
        /// <param name="Money">金额</param> 
        /// <returns>返回输入金额的大写形式</returns> 
        public static string ToCHSimpleMoney(double Money)
        {
            string str4 = "";    //数字的字符串形式
            string str5 = "";  //数字的简写形式
            int j;    //Money的值乘以100的字符串长度

            Money = Math.Round(Math.Abs(Money), 2);    //将Money取绝对值并四舍五入取2位小数 
            str4 = ((long)(Money * 100)).ToString();   //将Money乘100并转换成字符串形式 
            j = str4.Length;      //找出最高位 
            if (j > 15) { return "溢出"; } // 最高位 千亿 

            if (j < 7)
            {
                // 千计算
                str5 = Money.ToString();
            }
            else if (j < 11)
            {
                // 万为单位
                double tempMoney = Money * 0.0001;
                double d2 = Math.Round(Math.Abs(tempMoney), 2);
                str5 = d2.ToString() + "万";
            }
            else
            {
                // 亿为单位
                double tempMoney = Money * 0.00000001;
                double d2 = Math.Round(Math.Abs(tempMoney), 2);
                str5 = d2.ToString() + "亿";
            }
            return str5;
        }
        public static string ToCHSimpleMoney(string Money)
        {
            if (IsNumeric(Money))
            {
                return ToCHSimpleMoney(double.Parse(Money));
            }
            else
            {
                throw new Exception("金额格式不正确！");
            }
        }

        /// <summary> 
        /// 转换数字为人民币大写金额
        /// </summary> 
        /// <param name="Money">符合double格式的字符串</param> 
        /// <returns>返回输入金额的大写形式</returns> 
        public static string ToCHNMoney(string Money)
        {
            if (IsNumeric(Money))
            {
                return ToCHNMoney(double.Parse(Money));
            }
            else
            {
                throw new Exception("金额格式不正确！");
            }
        }

        /// <summary>
        /// 将unicode货币字符串转换为货币符号
        /// </summary>
        /// <param name="UTFStr">unicode货币字符串</param>
        /// <returns>货币符号（输入不正确则返回空串）</returns>
        public static string ToCurrencySymbol(string UTFStr)
        {
            switch (UTFStr.Trim())
            {
                case "&#xFFE5;":
                    return "￥";
                case "HK&#36;":
                    return "HK$";
                case "P":
                    return "P";
                case "&#36;":
                    return "$";
                default:
                    return "";
            }
        }
    }
}

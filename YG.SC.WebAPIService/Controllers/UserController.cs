
using System.Text.RegularExpressions;

namespace YG.SC.WebAPIService.Controllers
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Net.Http;
    using System.Web.Http;
    using YG.SC.Common;
    using YG.SC.DataAccess;
    using YG.SC.Service;
    using YG.SC.Service.IService;
    using YG.SC.WebAPIService.Filters;
    using YG.SC.WebAPIService.Models;


    /// <summary>
    /// 类名称：UserController
    /// 命名空间：YG.SC.WebAPIService.Controllers
    /// 类功能：用户服务
    /// </summary>
    /// 创建者：孟祺宙
    /// 创建日期：2014/9/24 18:51
    /// 修改者：
    /// 修改时间：
    /// ----------------------------------------------------------------------------------------
    public class UserController : WebApiBaseController
    {
        /// <summary>
        /// 地址默认
        /// </summary>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/8 15:13
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        private const string AddressDefaultRecsts = "2";

        /// <summary>
        /// 字段_userOAuthService
        /// </summary>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/24 19:17
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        private readonly IUserOAuthService _userOAuthService;
        /// <summary>
        /// 字段_userService
        /// </summary>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/24 18:51
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        private readonly IUserService _userService;

        /// <summary>
        /// 字段_smsService
        /// </summary>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/24 19:49
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        private readonly ISmsService _smsService;

        /// <summary>
        /// 字段_addressService
        /// </summary>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/8 14:51
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        private readonly IAddressService _addressService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController" /> class.
        /// </summary>
        /// <param name="userOAuthService">The userOAuthService</param>
        /// <param name="userService">The userService</param>
        /// <param name="smsService">The smsService</param>
        /// <param name="addressService">The addressService</param>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/24 18:51
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public UserController(IUserOAuthService userOAuthService, IUserService userService, ISmsService smsService, IAddressService addressService)
        {
            this._userOAuthService = userOAuthService;
            this._userService = userService;
            this._smsService = smsService;
            this._addressService = addressService;
        }

        /// <summary>
        /// 释放对象使用的非托管资源，并有选择性地释放托管资源。
        /// </summary>
        /// <param name="disposing">若为 true，则同时释放托管资源和非托管资源；若为 false，则仅释放非托管资源。</param>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/24 18:52
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        protected override void Dispose(bool disposing)
        {
            this._userOAuthService.Dispose();
            this._userService.Dispose();
            this._smsService.Dispose();
            this._addressService.Dispose();
            base.Dispose(disposing);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userLoginParameter">The userLoginParameter</param>
        /// <returns>
        /// The HttpResponseMessage
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/24 18:52
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        [ScApiFilter]
        [HttpPost]
        public HttpResponseMessage Login([FromBody] UserLoginParameter userLoginParameter)
        {
            if (userLoginParameter == null || !IsValidPhoneNumber(userLoginParameter.Phone))
            {
                return new HttpResponseMessage
                {
                    Content = new WebApiResponseModel<string>
                    {
                        Result = ApiStatusCode.SystemResult.Fail.ToString(),
                        StatusMsg = "请求参数无效！",
                        StatusCode = (int)ApiStatusCode.Validate.InvalidResquest
                    }.Transform()
                };

            }
            //审核账号
            if (userLoginParameter.Phone.Trim() == "13264232563" && userLoginParameter.Code.Trim() == "232563")
            {

            }
            else if (this._userOAuthService.GetUserByPhone(userLoginParameter.Phone.Trim())!=null)
            {

            }
            else
            {
                var key = string.Format("{0}_{1}", SourceCd, userLoginParameter.Phone);

                if (!WebApiApplication.MockCacheDictionary.ContainsKey(key))
                {
                    return new HttpResponseMessage
                    {
                        Content = new WebApiResponseModel<string>
                        {
                            Result = ApiStatusCode.SystemResult.Fail.ToString(),
                            StatusMsg = "请先发送验证码",
                            StatusCode = (int)ApiStatusCode.Validate.InvalidResquest
                        }.Transform()
                    };
                }
                var code = WebApiApplication.MockCacheDictionary[key];
                if (!string.Equals(code, userLoginParameter.Code, StringComparison.CurrentCultureIgnoreCase))
                {
                    return new HttpResponseMessage
                    {
                        Content = new WebApiResponseModel<string>
                        {
                            Result = ApiStatusCode.SystemResult.Fail.ToString(),
                            StatusMsg = "验证码错误",
                            StatusCode = (int)ApiStatusCode.Validate.ValidateError
                        }.Transform()
                    };
                }
                WebApiApplication.MockCacheDictionary.Remove(key);
            }

            var usertoken = this._userOAuthService.Register(SourceCd, userLoginParameter.Phone);


            #region redis cache
            //if (!CacheUtility.RedisInstance.Exists(key))
            //{
            //    return new HttpResponseMessage
            //    {
            //        Content = new WebApiResponseModel<string>
            //        {
            //            Result = ApiStatusCode.SystemResult.Fail.ToString(),
            //            StatusMsg = "请先发送验证码",
            //            StatusCode = (int)ApiStatusCode.Validate.InvalidResquest
            //        }.Transform()
            //    };
            //}
            //var code = CacheUtility.RedisInstance.Get(key) as string;
            //if (!string.Equals(code, userLoginParameter.Code, StringComparison.CurrentCultureIgnoreCase))
            //{
            //    return new HttpResponseMessage
            //    {
            //        Content = new WebApiResponseModel<string>
            //        {
            //            Result = ApiStatusCode.SystemResult.Fail.ToString(),
            //            StatusMsg = "验证码错误",
            //            StatusCode = (int)ApiStatusCode.Validate.ValidateError
            //        }.Transform()
            //    };
            //}
            //var usertoken = this._userOAuthService.Register(SourceCd, userLoginParameter.Phone);
            //CacheUtility.RedisInstance.Delete(key);
            #endregion

            var userId = this._userOAuthService.GetUserId(SourceCd, usertoken);
            var tuple = this._userService.UserPerfectStatus(userId);
            var userLoginModel = new UserLoginModel
            {
                UserToken = usertoken,
                IsPerfect = tuple.Item1,
                UserStatus = tuple.Item2 == "0" ? "审核成功" : (tuple.Item2 == "1" ? "审核中" : "审核失败"),
                UserStatusCode = Int32.Parse(tuple.Item2)
            };
            return new HttpResponseMessage
            {
                Content = new WebApiResponseModel<UserLoginModel>
                {
                    Result = userLoginModel
                }.Transform()
            };
        }



        /// <summary>
        /// 验证手机号
        /// </summary>
        /// <param name="phoneNumber">The phoneNumber</param>
        /// <returns></returns>
        /// 创建者：边亮
        /// 创建日期：2014/11/27 15:06
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return false;
            }
            return Regex.IsMatch(phoneNumber, @"^(13|14|15|16|18|19)\d{9}$");
        }
        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="userLoginParameter">The userLoginParameter</param>
        /// <returns>
        /// HttpResponseMessage
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/24 19:49
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        [ScApiFilter]
        [HttpPost]
        public HttpResponseMessage SendCode([FromBody] UserLoginParameter userLoginParameter)
        {
            if (userLoginParameter == null || string.IsNullOrEmpty(userLoginParameter.Phone))
            {
                return new HttpResponseMessage
                {
                    Content = new WebApiResponseModel<string>
                    {
                        Result = ApiStatusCode.SystemResult.Fail.ToString(),
                        StatusMsg = "请求参数不能为空！",
                        StatusCode = (int)ApiStatusCode.Validate.InvalidResquest
                    }.Transform()
                };

            }
            var key = string.Format("{0}_{1}", SourceCd, userLoginParameter.Phone);
            string code = new RandomUtility().Generate();

            if (WebApiApplication.MockCacheDictionary.ContainsKey(key))
            {
                WebApiApplication.MockCacheDictionary.Remove(key);

            }

            WebApiApplication.MockCacheDictionary.Add(key, code);

            #region
            //CacheUtility.RedisInstance.Add(key, code, DateTime.Now.AddMinutes(3));
            //if (CacheUtility.RedisInstance.Exists(key))
            //{
            //    code = CacheUtility.RedisInstance.Get(key) as string;
            //}
            //else
            //{
            //    code = new RandomUtility().Generate();
            //    CacheUtility.RedisInstance.Add(key, code, DateTime.Now.AddMinutes(3));
            //}
            #endregion
            var content = string.Format(CommonContorllers.SmsSendUserVerificationCode, code);
            var result = this._smsService.SendRegisterCode(userLoginParameter.Phone, content);
            return new HttpResponseMessage
            {
                Content = new WebApiResponseModel<string>
                {
                    Result = code,
                    StatusMsg = result
                }.Transform()
            };
        }

        /// <summary>
        /// 完善信息
        /// </summary>
        /// <param name="userPerfectParameter">The userPerfectParameter</param>
        /// <returns>
        /// The HttpResponseMessage
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/25 14:09
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        [ScApiFilter]
        [HttpPost]
        public HttpResponseMessage Perfect([FromBody] UserPerfectParameter userPerfectParameter)
        {
            if (userPerfectParameter == null || string.IsNullOrEmpty(userPerfectParameter.UserToken))
            {
                return new HttpResponseMessage
                {
                    Content = new WebApiResponseModel<string>
                    {
                        Result = ApiStatusCode.SystemResult.Fail.ToString(),
                        StatusMsg = "请求参数(UserToken)不能为空！",
                        StatusCode = (int)ApiStatusCode.Validate.InvalidResquest
                    }.Transform()
                };

            }
            var userId = this._userOAuthService.GetUserId(SourceCd, userPerfectParameter.UserToken);
            var userAddress = this._userService.Perfect(userId, userPerfectParameter.Company, new SctUserAddress
               {
                   UserId = userId,
                   Id = userPerfectParameter.Id,
                   CustomerName = userPerfectParameter.Name,
                   CustomerPhone = userPerfectParameter.Phone,
                   Gender = userPerfectParameter.Gender.ToString(CultureInfo.InvariantCulture),
                   ProvinceId = userPerfectParameter.ProvinceId,
                   CityId = userPerfectParameter.CityId,
                   CountyId = userPerfectParameter.CountyId,
                   AddressDetial = userPerfectParameter.AddressDetails,
                   InsBy = "SYSTEM",
                   InsDt = DateTime.Now,
                   Recsts = userPerfectParameter.IsDefault ? "2" : "0"//2默认地址 0有效地址 3用户地址
               });

            var provincesDic = this._addressService.GetProvincesDictionary();
            var citesDic = this._addressService.GetChinaCitiesDictionary(new int[] { userAddress.CityId });
            var countiesDic = this._addressService.GetChinaCountiesDictionary(new int[] { userAddress.CountyId });
            var userAddressModel = new UserAddressModel
            {
                Id = userAddress.Id,
                ProvinceId = userAddress.ProvinceId,
                CityId = userAddress.CityId,
                CountyId = userAddress.CountyId,
                Name = userAddress.CustomerName,
                Phone = userAddress.CustomerPhone,
                AddressDetail = userAddress.AddressDetial,
                CustomerAddress = provincesDic[userAddress.ProvinceId].Name + citesDic[userAddress.CityId].Name + countiesDic[userAddress.CountyId].Name + userAddress.AddressDetial,
                IsDefault = userAddress.Recsts == AddressDefaultRecsts
            };
            return new HttpResponseMessage
            {
                Content = new WebApiResponseModel<UserAddressModel>
                {
                    Result = userAddressModel
                }.Transform()
            };
        }

        /// <summary>
        /// 添加地址
        /// </summary>
        /// <param name="userPerfectParameter">The userPerfectParameter</param>
        /// <returns>
        /// HttpResponseMessage
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/25 14:27
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        [ScApiFilter]
        [HttpPost]
        public HttpResponseMessage AddAddress([FromBody] UserPerfectParameter userPerfectParameter)
        {

            if (userPerfectParameter == null || string.IsNullOrEmpty(userPerfectParameter.UserToken))
            {
                return new HttpResponseMessage
                {
                    Content = new WebApiResponseModel<string>
                    {
                        Result = ApiStatusCode.SystemResult.Fail.ToString(),
                        StatusMsg = "请求参数(UserToken)不能为空！",
                        StatusCode = (int)ApiStatusCode.Validate.InvalidResquest
                    }.Transform()
                };

            }
            var userId = this._userOAuthService.GetUserId(SourceCd, userPerfectParameter.UserToken);
            var userAddress = this._userService.AddAddress(new SctUserAddress
             {
                 UserId = userId,
                 CustomerName = userPerfectParameter.Name,
                 CustomerPhone = userPerfectParameter.Phone,
                 Gender = userPerfectParameter.Gender.ToString(CultureInfo.InvariantCulture),
                 ProvinceId = userPerfectParameter.ProvinceId,
                 CityId = userPerfectParameter.CityId,
                 CountyId = userPerfectParameter.CountyId,
                 AddressDetial = userPerfectParameter.AddressDetails,
                 InsBy = "SYSTEM",
                 InsDt = DateTime.Now,
                 Recsts = userPerfectParameter.IsDefault ? "2" : "0"//2默认地址 0有效地址
             });

            var provincesDic = this._addressService.GetProvincesDictionary();
            var citesDic = this._addressService.GetChinaCitiesDictionary(new int[] { userAddress.CityId });
            var countiesDic = this._addressService.GetChinaCountiesDictionary(new int[] { userAddress.CountyId });
            var userAddressModel = new UserAddressModel
            {
                Id = userAddress.Id,
                ProvinceId = userAddress.ProvinceId,
                CityId = userAddress.CityId,
                CountyId = userAddress.CountyId,
                Name = userAddress.CustomerName,
                Phone = userAddress.CustomerPhone,
                AddressDetail = userAddress.AddressDetial,
                CustomerAddress = provincesDic[userAddress.ProvinceId].Name + citesDic[userAddress.CityId].Name + countiesDic[userAddress.CountyId].Name + userAddress.AddressDetial,
                IsDefault = userAddress.Recsts == AddressDefaultRecsts
            };
            return new HttpResponseMessage
            {
                Content = new WebApiResponseModel<UserAddressModel>
                {
                    Result = userAddressModel
                }.Transform()
            };
        }

        /// <summary>
        /// 编辑地址
        /// </summary>
        /// <param name="parameter">The parameter</param>
        /// <returns>
        /// HttpResponseMessage
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/10/8 14:58
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        [ScApiFilter]
        [HttpPost]
        public HttpResponseMessage EditAddress([FromBody]UserEditAddressParameter parameter)
        {

            if (parameter == null || string.IsNullOrEmpty(parameter.UserToken))
            {
                return new HttpResponseMessage
                {
                    Content = new WebApiResponseModel<string>
                    {
                        Result = ApiStatusCode.SystemResult.Fail.ToString(),
                        StatusMsg = "请求参数(UserToken)不能为空！",
                        StatusCode = (int)ApiStatusCode.Validate.InvalidResquest
                    }.Transform()
                };

            }
            var userId = this._userOAuthService.GetUserId(SourceCd, parameter.UserToken);

            var userAddress = this._userService.EditAddress(new SctUserAddress
            {
                Id = parameter.Id,
                UserId = userId,
                CustomerName = parameter.Name,
                CustomerPhone = parameter.Phone,
                Gender = parameter.Gender.ToString(),
                ProvinceId = parameter.ProvinceId,
                CityId = parameter.CityId,
                CountyId = parameter.CountyId,
                AddressDetial = parameter.AddressDetails,
                Recsts = parameter.IsDefault ? "2" : "0"//2默认地址 0有效地址
            });
            var provincesDic = this._addressService.GetProvincesDictionary();
            var citesDic = this._addressService.GetChinaCitiesDictionary(new int[] { userAddress.CityId });
            var countiesDic = this._addressService.GetChinaCountiesDictionary(new int[] { userAddress.CountyId });
            var userAddressModel = new UserAddressModel
            {
                Id = userAddress.Id,
                ProvinceId = userAddress.ProvinceId,
                CityId = userAddress.CityId,
                CountyId = userAddress.CountyId,
                Name = userAddress.CustomerName,
                Phone = userAddress.CustomerPhone,
                AddressDetail = userAddress.AddressDetial,
                CustomerAddress = provincesDic[userAddress.ProvinceId].Name + citesDic[userAddress.CityId].Name + countiesDic[userAddress.CountyId].Name + userAddress.AddressDetial,
                IsDefault = userAddress.Recsts == AddressDefaultRecsts
            };
            return new HttpResponseMessage
            {
                Content = new WebApiResponseModel<UserAddressModel>
                {
                    Result = userAddressModel
                }.Transform()
            };
        }

        /// <summary>
        /// 根据usertoken获取用户地址列表
        /// </summary>
        /// <param name="userToken">The userToken</param>
        /// <returns>
        /// HttpResponseMessage
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/24 19:01
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        [ScApiFilter]
        [HttpGet]
        public HttpResponseMessage GetAddress([FromUri] string userToken)
        {
            if (string.IsNullOrEmpty(userToken))
            {
                return new HttpResponseMessage
                {
                    Content = new WebApiResponseModel<string>
                    {
                        Result = ApiStatusCode.SystemResult.Fail.ToString(),
                        StatusMsg = "请求参数(UserToken)不能为空！",
                        StatusCode = (int)ApiStatusCode.Validate.InvalidResquest
                    }.Transform()
                };

            }
            var userId = this._userOAuthService.GetUserId(SourceCd, userToken);
            var userAddressArray = this._userService.GetUserAddress(userId).OrderByDescending(item => item.InsDt);

            var userAddressArrayModel = (from p in userAddressArray
                                         orderby p.InsDt descending
                                         select new UserAddressModel
                                             {
                                                 Id = p.Id,
                                                 ProvinceId = p.ProvinceId,
                                                 CityId = p.CityId,
                                                 CountyId = p.CountyId,
                                                 Name = p.CustomerName,
                                                 Phone = p.CustomerPhone,
                                                 AddressDetail = p.AddressDetial,
                                                 CustomerAddress = p.ProvinceName + p.CityName + p.CountyName + p.AddressDetial,
                                                 IsDefault = p.Recsts == AddressDefaultRecsts
                                             }).ToArray();

            return new HttpResponseMessage
            {
                Content = new WebApiResponseModel<UserAddressModel[]>
                {
                    Result = userAddressArrayModel
                }.Transform()
            };
        }

        /// <summary>
        /// 删除地址
        /// </summary>
        /// <param name="userAddressParameter">The userAddressParameter</param>
        /// <returns>
        /// HttpResponseMessage
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/25 14:41
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        [ScApiFilter]
        [HttpPost]
        public HttpResponseMessage DelAddress([FromBody] UserAddressParameter userAddressParameter)
        {
            if (userAddressParameter == null || userAddressParameter.AddressId == 0)
            {
                return new HttpResponseMessage
                {
                    Content = new WebApiResponseModel<string>
                    {
                        Result = ApiStatusCode.SystemResult.Fail.ToString(),
                        StatusMsg = "请求参数(UserToken)不能为空！",
                        StatusCode = (int)ApiStatusCode.Validate.InvalidResquest
                    }.Transform()
                };

            }
            this._userService.DelAddress(userAddressParameter.AddressId);
            return new HttpResponseMessage
            {
                Content = new WebApiResponseModel<string>
                {
                    Result = ApiStatusCode.SystemResult.Success.ToString()
                }.Transform()
            };
        }

        /// <summary>
        /// 获取用户状态
        /// </summary>
        /// <param name="userParameter">The userParameter</param>
        /// <returns>
        /// HttpResponseMessage
        /// </returns>
        /// 创建者：孟祺宙
        /// 创建日期：2014/9/29 16:37
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        [ScApiFilter]
        [HttpPost]
        public HttpResponseMessage Status([FromBody] UserParameter userParameter)
        {
            if (userParameter == null || string.IsNullOrEmpty(userParameter.UserToken))
            {
                return new HttpResponseMessage
                {
                    Content = new WebApiResponseModel<string>
                    {
                        StatusCode = (int)ApiStatusCode.Validate.InvalidResquest,
                        StatusMsg = "请求参数错误",
                        Result = ApiStatusCode.SystemResult.Fail.ToString()
                    }.Transform()
                };
            }
            var userId = this._userOAuthService.GetUserId(SourceCd, userParameter.UserToken);
            if (userId == -1)
            {
                return new HttpResponseMessage
                {
                    Content = new WebApiResponseModel<string>
                    {
                        StatusCode = (int)ApiStatusCode.OAuth.UnauthorizedClient,
                        StatusMsg = "登陆超时，请重新登录",
                        Result = ApiStatusCode.SystemResult.Fail.ToString()
                    }.Transform()
                };
            }

            var tuple = this._userService.UserPerfectStatus(userId);
            var userLoginModel = new UserLoginModel
            {
                UserToken = userParameter.UserToken,
                IsPerfect = tuple.Item1,
                UserStatus = tuple.Item2 == "0" ? "审核成功" : (tuple.Item2 == "1" ? "审核中" : "审核失败"),
                UserStatusCode = Int32.Parse(tuple.Item2)
            };
            //0审核成功 1审核中 2审核失败 
            return new HttpResponseMessage
            {
                Content = new WebApiResponseModel<UserLoginModel>
                {
                    Result = userLoginModel
                }.Transform()
            };
        }

        /// <summary>
        ///获取用户信息
        /// </summary>
        /// <param name="userToken">The userToken</param>
        /// <returns>
        /// The HttpResponseMessage
        /// </returns>
        /// 创建者：边亮
        /// 创建日期：2014/10/16 10:31
        /// 修改者：
        /// 修改时间：
        /// ----------------------------------------------------------------------------------------
        [ScApiFilter]
        [HttpGet]
        public HttpResponseMessage Info([FromUri] string userToken)
        {
            if (string.IsNullOrEmpty(userToken))
            {
                return new HttpResponseMessage
                {
                    Content = new WebApiResponseModel<string>
                    {
                        StatusCode = (int)ApiStatusCode.Validate.InvalidRequiredParameter,
                        StatusMsg = "请求参数不能为空",
                        Result = ApiStatusCode.SystemResult.Fail.ToString()
                    }.Transform()
                };
            }

            var userId = this._userOAuthService.GetUserId(SourceCd, userToken);
            var user = this._userService.GetUserById(userId);
            var userAddress = user.SctUserAddress.FirstOrDefault(item => item.Recsts == "3");//获取地址详情

            var userInfoModel = new UserInfoModel
            {
                UserToken = userToken,
                Company = user.Company,
                IsPerfect = userAddress != null,
                UserStatus = user.Recsts == "0" ? "审核成功" : (user.Recsts == "1" ? "审核中" : "审核失败"),
                UserStatusCode = Int32.Parse(user.Recsts),
                CustomerName = "",
                CustomerPhone = "",
                Gender = "",
                ProvinceId = 0,
                CityId = 0,
                CountyId = 0,
                AddressDetail = "",
                CustomerAddress = ""
            };


            if (userAddress == null)
                return new HttpResponseMessage
                {
                    Content = new WebApiResponseModel<UserInfoModel>
                    {
                        Result = userInfoModel
                    }.Transform()
                };

            var provincesDic = this._addressService.GetProvincesDictionary();
            var citesDic = this._addressService.GetChinaCitiesDictionary(new[] { userAddress.CityId });
            var countiesDic = this._addressService.GetChinaCountiesDictionary(new[] { userAddress.CountyId });

            userInfoModel.CustomerName = userAddress.CustomerName;
            userInfoModel.CustomerPhone = userAddress.CustomerPhone;
            userInfoModel.Gender = userAddress.Gender;
            userInfoModel.ProvinceId = userAddress.ProvinceId;
            userInfoModel.CityId = userAddress.CityId;
            userInfoModel.CountyId = userAddress.CountyId;
            userInfoModel.AddressDetail = userAddress.AddressDetial;
            userInfoModel.CustomerAddress = provincesDic[userAddress.ProvinceId].Name +
                                            citesDic[userAddress.CityId].Name +
                                            countiesDic[userAddress.CountyId].Name + userAddress.AddressDetial;

            return new HttpResponseMessage
            {
                Content = new WebApiResponseModel<UserInfoModel>
                {
                    Result = userInfoModel
                }.Transform()
            };
        }

    }

}

#-*- coding : utf-8 -*-
require 'net/http'
require 'json'

class UserServiceTest 

	def initialize(args)
		@host = args
	end
	
	
	# 获取验证
	def user_sendcode_test
		uri = URI(@host + 'User/SendCode')
		
		res = Net::HTTP.post_form(uri,'Phone' => '18701632199')
		json_string = res.body if res.is_a?(Net::HTTPSuccess)

		json_obj = JSON.parse(json_string)

		# puts json_string
		puts json_obj['StatusCode'] == 200
		puts json_obj['Result']
		
	end
	#登陆
	def user_login_test
		uri = URI(@host + 'User/Login')
		
		res = Net::HTTP.post_form(uri,'Phone' => '18701632199','code'=>'514883')
		json_string = res.body if res.is_a?(Net::HTTPSuccess)
		puts json_string
		json_obj = JSON.parse(json_string)

		#puts json_string
		# puts json_obj['StatusCode'] == 200
		# puts json_obj['Result']
		
	end
	# 添加地址
	def user_AddAddress_test
		uri = URI(@host + 'user/AddAddress')

		post_json = { :UserToken => 'f7cd318bf121432aa87ab7b2efc64221',
					:Name => '边亮', 
					:Phone => '15510116605', :AddressDetails => '华腾北塘',
					:ProvinceId => '1',:CityId=>'2',:CountyId=>'5',
					:IsDefault=>'true'
					}.to_json

		json_headers = {"Content-Type" => "application/json",
		                "Accept" => "application/json"}

		http = Net::HTTP.new(uri.host, uri.port)

		res = http.post(uri.path, post_json, json_headers)

		response_string = res.body
		puts response_string
		#response_json = JSON.parse(response_string)
		#puts response_json["StatusCode"]
		#puts response_json['Result']['Id']
	end
	# 编辑地址
	def user_EditAddress_test
		uri = URI(@host + 'user/AddAddress')

		post_json = { :UserToken => 'f7cd318bf121432aa87ab7b2efc64221',
					:Id=>'60',:Name => '边亮', 
					:Phone => '15510116605', :AddressDetails => '华腾北塘商务大厦',
					:ProvinceId => '13',:CityId=>'13',:CountyId=>'13',
					:IsDefault=>'true',:GenDer=>'1'
					}.to_json

		json_headers = {"Content-Type" => "application/json",
		                "Accept" => "application/json"}

		http = Net::HTTP.new(uri.host, uri.port)

		res = http.post(uri.path, post_json, json_headers)

		response_string = res.body
		puts response_string
		#response_json = JSON.parse(response_string)
		#puts response_json["StatusCode"]
		#puts response_json['Result']['Id']
	end
	# 获取地址列表
	def user_getaddress_test
		uri = URI(@host + 'User/GetAddress')
		params = { :UserToken => 'f7cd318bf121432aa87ab7b2efc64221' }
		uri.query = URI.encode_www_form(params)

		res = Net::HTTP.get_response(uri)
		json_string = res.body if res.is_a?(Net::HTTPSuccess)

		json_obj = JSON.parse(json_string)

		puts json_string
		#puts json_obj['StatusCode'] == 200
		#puts json_obj['Result'][0]['Phone'] == '1409260210'

	end

		#删除地址
	def user_DelAddress_test
		uri = URI(@host + 'User/DelAddress')
		
		res = Net::HTTP.post_form(uri,'UserToken' => 'f7cd318bf121432aa87ab7b2efc64221','AddressId'=>'60')
		json_string = res.body if res.is_a?(Net::HTTPSuccess)

		json_obj = JSON.parse(json_string)

		# puts json_string
		puts json_obj['StatusCode'] == 200
		puts json_obj['Result']
		
	end

	# 完善信息
	def user_Perfect_test
		uri = URI(@host + 'user/Perfect')

		post_json = { :UserToken => '0cd18eebadfc43d29e672b4364a4b566',
					:Name => '高伟伟', 
					:Phone => '15510116605', :AddressDetails => '华腾北塘商务大大大厦',
					:ProvinceId => '1',:CityId=>'1',:CountyId=>'1',
					:IsDefault=>'true',:Company=>'暖化器',:GenDer=>'1'
					}.to_json

		json_headers = {"Content-Type" => "application/json",
		                "Accept" => "application/json"}

		http = Net::HTTP.new(uri.host, uri.port)

		res = http.post(uri.path, post_json, json_headers)

		response_string = res.body
		 # puts response_string
		response_json = JSON.parse(response_string)
		puts response_json["StatusCode"]
		puts response_json['Result']
	end
	#获取用户状态
	def user_Status_test
		uri = URI(@host + 'User/Status')
		
		res = Net::HTTP.post_form(uri,'UserToken' => 'f7cd318bf121432aa87ab7b2efc64221')
		json_string = res.body if res.is_a?(Net::HTTPSuccess)

		json_obj = JSON.parse(json_string)

		puts json_string
		puts json_obj['StatusCode'] == 200
		puts json_obj['Result']
		
	end


end

ost = UserServiceTest.new('http://localhost:5886/api/ANDROID/')
#ost = UserServiceTest.new('http://shicai.api.chaoshirukou.cn/api/ANDROID/')

#ost = UserServiceTest.new('http://10.8.8.253:9001/api/ANDROID/')
#ost.user_sendcode_test
#ost.user_Perfect_test
#ost.user_Status_test
#ost.user_getaddress_test
ost.user_login_test




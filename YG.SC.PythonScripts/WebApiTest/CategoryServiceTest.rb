require 'net/http'
require 'json'

class CategoryServiceTest
	def initialize(args)
		@host=args
	end

	#获取分类信息
	def category_GetClassifty_test 

		uri=URI(@host+'Category/GetClassifty')
		json_string = Net::HTTP.get(uri)
		puts json_string
	end

	#获取商品列表
	def category_GetGoodsBySecond_test 
		uri=URI(@host+'Category/GetGoodsBySecond')
		params={:SecondId=>'1'}
		uri.query = URI.encode_www_form(params)

		res = Net::HTTP.get_response(uri)
		json_string = res.body if res.is_a?(Net::HTTPSuccess)

		json_obj = JSON.parse(json_string)

		# puts json_string
		puts json_obj['StatusCode']
		puts json_obj['Result']
	end
	
end


ost = CategoryServiceTest.new('http://localhost:5886/api/ANDROID/')
ost.category_GetGoodsBySecond_test
require 'net/http'
require 'json'

# 	class GoodsServiceTest 
# 	def initialize(args)
# 		@host = args
# 	end

# 	def goods_CheckGoods_test
# 		uri = URI(@host + 'goods/CheckGoods')
# 		puts uri
# 		res=Net::HTTP.post_form(uri, 'goodsId' => ['1', '2'])
# 		puts res.body
# 		# response_string = res.body
# 		#  # puts response_string
# 		# response_json = JSON.parse(response_string)
# 		# puts response_json["StatusCode"]
# 		# puts response_json['Result']
# 	end
# ost = GoodsServiceTest.new('http://localhost:5886/api/ANDROID/')
# ost.goods_CheckGoods_test

uri = URI('http://localhost:5886/api/ANDROID/goods/CheckGoods')
res = Net::HTTP.post_form(uri,'goodsId' => ['1', '2','3'])
puts res.body

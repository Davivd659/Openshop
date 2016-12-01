require 'net/http'
require 'json'

class OrderServiceTest 

	@@json_headers = {"Content-Type" => "application/json", "Accept" => "application/json"}

	def initialize(host)
		@host = host
	end
	
	def order_add_test
		uri = URI(@host + 'order/add')

		# my_hash = { :UserToken => 'f7cd318bf121432aa87ab7b2efc64221', :AddressId => 6, 
		# 			:Invoice => 'NONE', :Company => '发票公司名称', :Remark => '备注',
		# 			:MaterialPart => [ 
		# 				{ :Id=>10, :Number=>10 },
		# 				{ :Id=>9, :Number=>2 } 
		# 			]
		# 		}

		my_hash = {
		    :UserToken => "df1043798ea84cc78dc423f44f87eaef",
		    :AddressId => 2,
		    :Invoice => "NONE",
		    :Company => "",
		    :MaterialPart => [ {:Id=>5,:Number=>1},{:Id=>6,:Number=>1},{:Id=>7,:Number=>1} ],
		    :Remark => ""
		}
		post_json = JSON.generate(my_hash)
		#{"UserToken":"633cc385af58489bb40672a8d4bd00b7","AddressId":104,"Invoice":"NONE","Company":"","MaterialPart":"[{\"Id\":1,\"Number\":1},{\"Id\":2,\"Number\":1},{\"Id\":3,\"Number\":1}]","Remark":""}
		#{"UserToken":"633cc385af58489bb40672a8d4bd00b7","AddressId":104,"Invoice":"NONE","Company":"","MaterialPart":[{"Id":1,"Number":1},{"Id":2,"Number":1},{"Id":3,"Number":1}],"Remark":""}
		puts post_json

		http = Net::HTTP.new(uri.host, uri.port)
		res = http.post(uri.path, post_json, @@json_headers)

		response_string = res.body
		response_json = JSON.parse(response_string)
		puts response_json["StatusCode"] == 200
		puts response_json['Result']['OrderNumber']
		puts response_json['Result']['TotalPrice'] == 22.00
		puts response_json['Result']['OrderStatusName'] == '订单生成中'
	end
	

	def order_detail_test
		order_number = '1410110013'
		user_token = 'f7cd318bf121432aa87ab7b2efc64221'

		uri = URI(@host + 'Order/Detail')
		params = { :UserToken => user_token, :OrderNumber => order_number }
		uri.query = URI.encode_www_form(params)

		res = Net::HTTP.get_response(uri)
		json_string = res.body if res.is_a?(Net::HTTPSuccess)
		
		if json_string.nil?
			puts 'nil'
		else
			json_obj = JSON.parse(json_string)

			puts json_string
			puts json_obj['StatusCode'] == 200
			puts json_obj['Result']['OrderNumber'] == order_number
			puts res['content-type']
		end

	end

	def order_list_test
		user_token = 'f7cd318bf121432aa87ab7b2efc64221'
		uri = URI(@host + 'Order/List?userToken=' + user_token)
		json_string = Net::HTTP.get(uri) 
		json_obj = JSON.parse(json_string)
		puts json_obj['Result'].length
	end

	def order_affirm
		uri = URI(@host + 'order/affirm')
		post_json = { :UserToken => 'f7cd318bf121432aa87ab7b2efc64221', :OrderNumber => 1410110013 }.to_json
		http = Net::HTTP.new(uri.host, uri.port)

		res = http.post(uri.path, post_json, @@json_headers)
		response_string = res.body
		puts response_string
	end


end

ost = OrderServiceTest.new('http://localhost:5886/api/ANDROID/')
#ost = OrderServiceTest.new('http://localhost:5886/api/ANDROID/')
#ost.order_list_test
ost.order_add_test
#ost.order_detail_test
#ost.order_list_test
#ost.order_affirm
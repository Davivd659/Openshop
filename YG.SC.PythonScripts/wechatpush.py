#coding:utf-8

import urllib, httplib,hashlib,time, random


def makeRequestUrl():
    token = 'YG.SC.IPHONE'
    
    timestamp = str(int(time.time()))
    nonce = str(random.randint(1000,9999))

    arr = [token, timestamp, nonce]
    arr.sort()
    signature = hashlib.sha1(''.join(arr)).hexdigest()
    
    return [signature, timestamp, nonce]

def get(arr):
    signature, timestamp, nonce = arr

    con = httplib.HTTPConnection('localhost:5886')
    con.request('GET', '/api/ANDROID/User/GetAddress?userToken=651ca9b7b0e94663a6cdfdc3d91b3047',
                headers ={ "Content-type": "application/x-www-form-urlencoded", "Accept": "text/plain",
                           'YgSc':'signature=%s&timestamp=%s&nonce=%s' %(signature, timestamp, nonce)
                           })
    
    #['dbb3210196494545ac5b31244accb4483efec05d', '1398152071', '8228', 'success123']
    r1 = con.getresponse()
    #print r1.status, r1.reason

    data1 = r1.read()
    con.close()
    print data1

def post(arr):
    signature, timestamp, nonce = arr
    
    params = urllib.urlencode({'Phone':'13264232563','Code':983045})

    headers ={ "Content-type": "application/x-www-form-urlencoded",
               'YgSc':'signature=%s&timestamp=%s&nonce=%s' %(signature, timestamp, nonce),
               "Accept": "text/plain"}

    con = httplib.HTTPConnection('10.8.8.253:9001')
    #con = httplib.HTTPConnection('localhost:5886')
    con.request('POST', '/api/ANDROID/User/Login' , params, headers)


    r1 = con.getresponse()
    #print r1.status, r1.reason

    data1 = r1.read()
    con.close()
    print data1

def postAddOrder(arr):
    signature, timestamp, nonce = arr
    
    params = urllib.urlencode({
        'UserToken':'651ca9b7b0e94663a6cdfdc3d91b3047',
        'AddressId':42,
        'Invoice':'NONE',
        'MaterialPart':[{"Id":20,"Number":2,"Name":'',"Price":0.0}],
        'Remark' : '测试'
        })

    headers ={ "Content-type": "application/x-www-form-urlencoded",
               'YgSc':'signature=%s&timestamp=%s&nonce=%s' %(signature, timestamp, nonce),
               "Accept": "text/plain"}

    #httplib.HTTPConnection('10.8.8.253:9001')
    con = httplib.HTTPConnection('localhost:5886')
    con.request('POST', '/api/ANDROID/Order/Add' , params, headers)


    r1 = con.getresponse()
    #print r1.status, r1.reason

    data1 = r1.read()
    con.close()
    print data1

def postUserAddAddress(arr):
    signature, timestamp, nonce = arr
    
    params = urllib.urlencode({
    'UserToken':'651ca9b7b0e94663a6cdfdc3d91b3047',
    "Id":24,
    'Name':'Wohao ',
    'Phone':11111111111,
    'AddressDetails':'Hhhhhhqqq',
    'ProvinceId':1,
    'CityId':1,
    'CountyId':2,
    'IsDefault':'true',
    'Gender':1
    })

    headers ={ "Content-type": "application/x-www-form-urlencoded",
               'YgSc':'signature=%s&timestamp=%s&nonce=%s' %(signature, timestamp, nonce),
               "Accept": "text/plain"}

    con = httplib.HTTPConnection('10.8.8.253:9001')
    #con = httplib.HTTPConnection('localhost:5886')
    con.request('POST', '/api/ANDROID/User/AddAddress' , params, headers)


    r1 = con.getresponse()
    #print r1.status, r1.reason

    data1 = r1.read()
    con.close()
    print data1
    
if __name__ == '__main__':
    arr = makeRequestUrl()
    #get(arr)
    #postUserAddAddress(arr)
    postAddOrder(arr)

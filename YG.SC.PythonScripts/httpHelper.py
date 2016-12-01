#coding:utf-8

import urllib, httplib

def post(header, host, page, param):
    _headers = {"Content-type": "application/x-www-form-urlencoded", "Accept": "text/plain"}
    headers = dict(_headers, **header)
    con = httplib.HTTPConnection(host)
    con.request('POST', page, param, headers)
    
    r1 = con.getresponse()
    data1 = r1.read()
    con.close()
    print data1

def get(host, page):
    con = httplib.HTTPConnection(host)
    con.request('GET', page)
    r1 = con.getresponse()
    data1 = r1.read()
    con.close()
    return data1
    
    

if __name__ == '__main__':
    pass
    #post( header={'a':123}, host='www.baidu.com', page='', param= "a")
    #print get('www.baidu.com', '/index')
webExpress.data.china.getProvinces = function () {
    return webExpress.data.china.provinces;
}

webExpress.data.china.getCities = function (provinceId) {
    var result = webExpress.data.china.cities[provinceId];
    if (!result) {
        return [];
    }
    return result;
}

webExpress.data.china.getCounties = function (cityId) {
    var result = webExpress.data.china.counties[cityId];
    if (!result) {
        return [];
    }
    return result;
}

webExpress.data.china.getAddress = function (provinceId, cityId, countyId) {
    var address = "";
    for (var i = 0; i < webExpress.data.china.provinces.length; i++) {
        if (webExpress.data.china.provinces[i].Value == provinceId) {
            address += webExpress.data.china.provinces[i].Text;
            break;
        }
    }
    var cities = webExpress.data.china.getCities(provinceId);
    for (var i = 0; i < cities.length; i++) {
        if (cities[i].Value == cityId) {
            address += cities[i].Text;
            break;
        }
    }
    var counties = webExpress.data.china.getCounties(cityId);
    for (var i = 0; i < counties.length; i++) {
        if (counties[i].Value == countyId) {
            address += counties[i].Text;
            break;
        }
    }
    return address;
}

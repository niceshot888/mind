
function getUrlVar(name) {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars[name];
}


Date.prototype.format = function (fmt) {   
    var o = {
        "M+": this.getMonth() + 1,                 //月份   
        "d+": this.getDate(),                    //日   
        "h+": this.getHours(),                   //小时   
        "m+": this.getMinutes(),                 //分   
        "s+": this.getSeconds(),                 //秒   
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度   
        "S": this.getMilliseconds()             //毫秒   
    };
    if (/(y+)/.test(fmt))
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt))
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}

function convertUndefinedToEmptyNum(val) {
    return val == undefined ? "" : keepTwoDecimal(val);
}

function convertNullToDecimal(val) {
    if (val == null || val == "null")
        return null;
    return val;
}

function convertDecimalToYiYuan(val) {
    var new_val = convertNullToDecimal(val);
    if (new_val == null)
        return null;
    return keepTwoDecimal(val/100000000);
}

function convertDecimalToWanGu(val) {
    var new_val = convertNullToDecimal(val);
    if (new_val == null)
        return null;
    return keepTwoDecimal(val / 10000);
}


function keepTwoDecimal(num) {
    var result = parseFloat(num);
    if (isNaN(result)) {
        return null;
    }
    result = Math.round(num * 100) / 100;
    return result;
};

function changeDateFormat(cellval) {
    var date = new Date(parseInt(cellval.replace("/Date(", "").replace(")/", ""), 10));
    var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
    var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
    return date.getFullYear() + "-" + month + "-" + currentDate;
}

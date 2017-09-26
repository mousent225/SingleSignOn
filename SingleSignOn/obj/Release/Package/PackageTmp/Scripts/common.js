function showCalendar(year, month, date){
  var oDateOfCurrent;
  if(year != null && month != null && date != null){
    oDateOfCurrent = new Date(year, month, date);
  }else{
    oDateOfCurrent = new Date(); 
  }

  var currentYear = oDateOfCurrent.getFullYear();  
  var currentMonth = oDateOfCurrent.getMonth();  
  var currentDate = oDateOfCurrent.getDate();  

  var oDateTemp;
  var tmpeYear;
  var tempMonth;
  var tempDate = 1;
  var tempDay;


  var calendar = "	<div class=\"ui-datepicker-header ui-widget-header ui-helper-clearfix ui-corner-all\">\n" +
          "			<a class=\"ui-datepicker-prev ui-corner-all\" onclick=\"javascript:showCalendar(" + currentYear + "," + (currentMonth-1) + "," + currentDate + ")\" title=\"Prev\">" +
          "			<span class=\"ui-icon ui-icon-circle-triangle-w\">Prev</span></a>" +
          "			<a class=\"ui-datepicker-next ui-corner-all\" onclick=\"javascript:showCalendar(" + currentYear + "," + (currentMonth+1) + "," + currentDate + ")\" title=\"Next\">" +
          "				<span class=\"ui-icon ui-icon-circle-triangle-e\">Next</span></a>\n " +
          "			<div class=\"ui-datepicker-title\">\n" +
          "			<span class=\"ui-datepicker-year\">" + currentYear + "</span>&nbsp;" +
          "			<span class=\"ui-datepicker-month\">" + (currentMonth + 1) + "</span>\n" +
          "			</div>\n" +
          "	</div>\n" +
          "<table class=\"ui-datepicker-calendar\" id=\"calendar\" >\n" +
          "	<thead>\n" +
          "		<tr>\n" +
          "				<th class=\"ui-datepicker-week-end\"><span title=\"Sunday\">Sun</span></th> " +
          "		    <th><span title=\"Monday\">Mon</span></th> " +
          "   		<th><span title=\"Tuesday\">Tue</span></th> " +
          "   		<th><span title=\"Wednesday\">Wed</span></th> " +
          "   		<th><span title=\"Thursday\">Thu</span></th> " +
          "   		<th><span title=\"Friday\">Fri</span></th> " +
          "   		<th class=\"ui-datepicker-week-end\"><span title=\"Saturday\">Sat</span></th> " +
          "	</tr>\n" +
          "	</thead>\n";

  calendar += "	<tbody>\n";

  var RealOfCurrent = new Date();//현재 값 가져오기
  var RealCurrentMonth = RealOfCurrent.getMonth(); //현재 월
  for(var row = 0; row < 6; row++){
    calendar += "	<tr>\n";

    for(var col = 0; col < 7; col++){
      oDateTemp = new Date(currentYear, currentMonth, tempDate); // 달력 시작 년,월,일
      tempDay = oDateTemp.getDay();
      tempMonth = oDateTemp.getMonth();
      if(tempDay <= col && tempMonth == currentMonth){ // 해당 월, 요일과 맞는 일자만출력
        tempYear = oDateTemp.getYear();

        if(tempDate == currentDate &&  tempMonth == RealCurrentMonth){
          calendar += "		<td class=\" ui-datepicker-week-end ui-datepicker-days-cell-over  ui-datepicker-current-day ui-datepicker-today\">"; // 선택 일자 일때는 굵게 표시
          calendar += "<a class=\"ui-state-default ui-state-highlight ui-state-active\" href=\"javascript:;\">" + (tempDate++) + "</a>";
        }
		else if(col==0 || col==6)
		{
		calendar += "		<td class=\"  \">";
          calendar += "<a class=\"ui-state-default\" href=\"javascript:;\" style=\"color:red\">" + (tempDate++) + "</a>";
		}
		else{
          calendar += "		<td class=\"  \">";
          calendar += "<a class=\"ui-state-default\" href=\"javascript:;\">" + (tempDate++) + "</a>";
        }

        // 작성 함수 위치

      }else{
        calendar += "		<td class=\" \">";
      }
      calendar += "</td>\n";
    }

    calendar += "	</tr>\n";
  }
  calendar += "	</tbody>\n";
  calendar += "</table>\n";

  document.all['oCalendar'].innerHTML = calendar;
  
  //var d = new Date();
  //  var weekday = new Array(7);
  //  weekday[0] = "Sun";
  //  weekday[1] = "Mon";
  //  weekday[2] = "Tue";
  //  weekday[3] = "Wed";
  //  weekday[4] = "Thu";
  //  weekday[5] = "Fri";
  //  weekday[6] = "Sa";
  //  var n = weekday[d.getDay()];
  //  document.getElementById("date").innerHTML = currentYear+'.'+ currentMonth+'.'+currentDate+' '+ n.toUpperCase();
}


  
//window.onload = function showCalendar(){
//  var oDateOfCurrent;
//  oDateOfCurrent = new Date(); 
//  var currentYear = oDateOfCurrent.getFullYear();  
//  var currentMonth = oDateOfCurrent.getMonth() + 1;  
//  var currentDate = oDateOfCurrent.getDate(); 
  
//  var d = new Date();
//    var weekday = new Array(7);
//    weekday[0] = "Sunday";
//    weekday[1] = "Monday";
//    weekday[2] = "Tuesday";
//    weekday[3] = "Wednesday";
//    weekday[4] = "Thursday";
//    weekday[5] = "Friday";
//    weekday[6] = "Saturday";
//    var n = weekday[d.getDay()];
//	document.getElementById("date").innerHTML = 
//	currentYear + '.' + (currentMonth.toString().length > 1 ? currentMonth : '0' + currentMonth.toString()) + '.' + (currentDate.toString().length > 1 ? currentDate : '0' + currentDate.toString()) + ' ' + n;
//}


function fncEraseSpace(val) {
  var space = /\s+/g;
  return val.replace(space, "");
}

// 공백 여부확인
function fncIsEmpty(val) {
  if (val == null || fncEraseSpace(val) == "") return true;
  return false;
}

// 좌측 공백 제거
function fncLTrim(val) {
  var space = /^\s*/;
  return val.replace(space,"");
}

// 우측 공백 제거
function fncRTrim(val) {
  var space = /\s*$/;
  return val.replace(space,"");
}

// 좌,우측 공백 제거
function fncTrim(val) {
  return fncRTrim(fncLTrim(val));
}

// alert
function fncCallAlert(msg) {
  if (!msg) msg = "";
  alert(msg.replace("\\n","\n"));
}

// s 가 값이 없을경우 d 로 대체
function fncNvl(s, d) {
  return fncIsEmpty(s) ? (d == null ? "" : d) : s;
}

// 해당 id의 오브젝트 반환
function fncGetObjById(id) {
  return document.getElementById(id);
}

//해당 name의 오브젝트 반환
function fncGetObjByName(name) {
  return document.getElementsByName(name);
}

// 이동
function fncGoTo(url) {
  location.href(url);
}

// 로그인으로 이동
function fncGoToLogin() {
  fncGoTo("/common/nl/LoginController/login.dev");
}

// 메인으로 이동
function fncGoToMain() {
  fncGoTo("/common/na/IndexController/index.dev");
}

// 일반팝업 호출
function fncOpenPopup(param) {
  defaultSettings = {
    centerBrowser:0, // center window over browser window? {1 (YES) or 0 (NO)}. overrides top and left
    centerScreen:0, // center window over entire screen? {1 (YES) or 0 (NO)}. overrides top and left
    height:500, // sets the height in pixels of the window.
    left:0, // left position when the window appears.
    location:0, // determines whether the address bar is displayed {1 (YES) or 0 (NO)}.
    menubar:0, // determines whether the menu bar is displayed {1 (YES) or 0 (NO)}.
    resizable:0, // whether the window can be resized {1 (YES) or 0 (NO)}. Can also be overloaded using resizable.
    scrollbars:0, // determines whether scrollbars appear on the window {1 (YES) or 0 (NO)}.
    status:0, // whether a status line appears at the bottom of the window {1 (YES) or 0 (NO)}.
    width:500, // sets the width in pixels of the window.
    windowName:null, // name of window set from the name attribute of the element that invokes the click
    windowURL:null, // url used for the popup
    top:0, // top position when the window appears.
    toolbar:0 // determines whether a toolbar (includes the forward and back buttons) is displayed {1 (YES) or 0 (NO)}.
  };

  settings = jQuery.extend({}, defaultSettings, param || {});

  var windowFeatures =    'height=' + settings.height +
  ',width=' + settings.width +
  ',toolbar=' + settings.toolbar +
  ',scrollbars=' + settings.scrollbars +
  ',status=' + settings.status +
  ',resizable=' + settings.resizable +
  ',location=' + settings.location +
  ',menuBar=' + settings.menubar;

  settings.windowName =  settings.windowName || this.name;
  settings.windowURL = settings.windowURL || this.href ;
  var centeredY,centeredX;

  if (settings.centerBrowser){

    if ($.browser.msie) {//hacked together for IE browsers
      centeredY = (window.screenTop - 120) + ((((document.documentElement.clientHeight + 120)/2) - (settings.height/2)));
      centeredX = window.screenLeft + ((((document.body.offsetWidth + 20)/2) - (settings.width/2)));
    } else {
      centeredY = window.screenY + (((window.outerHeight/2) - (settings.height/2)));
      centeredX = window.screenX + (((window.outerWidth/2) - (settings.width/2)));
    }
    window.open(settings.windowURL, settings.windowName, windowFeatures+',left=' + centeredX +',top=' + centeredY).focus();
  } else if (settings.centerScreen){
    centeredY = (screen.height - settings.height)/2;
    centeredX = (screen.width - settings.width)/2;
    window.open(settings.windowURL, settings.windowName, windowFeatures+',left=' + centeredX +',top=' + centeredY).focus();
  } else {
    window.open(settings.windowURL, settings.windowName, windowFeatures+',left=' + settings.left +',top=' + settings.top).focus();
  }
  return false;
}

// modal 팝업
function fncOpenModalPopup(param) {
  defaultSettings = {
      windowURL:"", // url used for the popup
      height:500, // sets the height in pixels of the window.
      width:500, // sets the width in pixels of the window.
      resizable:'no',
      scroll:'no',
      args:[]
  };

  settings = jQuery.extend({}, defaultSettings, param || {});
  settings.windowURL = this.href || settings.windowURL;

  var options = 'dialogWidth:' + settings.width + 'px; dialogHeight:' + settings.height + 'px;';
  options +=  ' center:yes; help:no; status:no; ';
  options +=  ' resizable:' + settings.resizable + '; scroll:' + settings.scroll + '; ';

  return window.showModalDialog(settings.windowURL, settings.args, options);
}


function fncCustomerSearchPopup(aryParam) {

  var rtnVal = fncOpenModalPopup({
      height:500
    , width:800
    , windowURL:"/common/CommonController/retrieveCustomer.dev"
    , args:aryParam
  });

  return rtnVal;
}

function fncNationSearchPopup(aryParam) {

  var rtnVal = fncOpenModalPopup({
    height:500
    , width:690
    , windowURL:"/common/CommonController/retrieveNation.dev"
    , args:aryParam
  });

  return rtnVal;
}

function fncCompetitorSearchPopup(aryParam) {

  var rtnVal = fncOpenModalPopup({
    height:500
    , width:690
    , windowURL:"/common/CommonController/retrieveCompetitor.dev"
    , args:aryParam
  });

  return rtnVal;
}

function fncOrganizationSearchPopup(aryParam) {

  var rtnVal = fncOpenModalPopup({
    height:500
    , width:620
    , windowURL:"/common/CommonController/retrieveOrganization.dev"
    , args:aryParam
  });

  return rtnVal;
}

// 법인간거래시 조직popup
function fncOrganizationSearchPopupLiverdeal(aryParam) {

	  var rtnVal = fncOpenModalPopup({
	    height:500
	    , width:620
	    , windowURL:"/common/CommonController/retrieveLiverdealOrganization.dev"
	    , args:aryParam
	  });

	  return rtnVal;
	}


function fncOrganizationAllSearchPopup(aryParam) {

  var rtnVal = fncOpenModalPopup({
      height:500
      , width:620
      , windowURL:"/common/CommonController/retrieveAllOrganization.dev"
      , args:aryParam
  });

  return rtnVal;
}

function fncOrganizationCommonSearchPopup(aryParam) {
	var rtnVal = fncOpenModalPopup({
					      height:500
					      , width:680
					      , windowURL:"/common/CommonController/retrieveCommonOrganization.dev"
					      , args:aryParam
					  });

	return rtnVal;
}

function fncUserSearchPopup(aryParam) {

  if (aryParam && (aryParam.length == 5 || aryParam.length == 6 )) {

    var rtnVal = fncOpenModalPopup({
      height:500
      , width:900
      , windowURL:"/common/CommonController/retrieveUser.dev"
      , args:aryParam
    });

    return rtnVal;
  } else {
    return "";
  }
}

function fncUserSearchPopup2(aryParam) {

	  if (aryParam && (aryParam.length == 5 || aryParam.length == 6 )) {

	    var rtnVal = fncOpenModalPopup({
	      height:500
	      , width:900
	      , windowURL:"/common/CommonController/retrieveUser2.dev"
	      , args:aryParam
	    });

	    return rtnVal;
	  } else {
	    return "";
	  }
	}


function fncPowerMapSearchPopup(aryParam) {

    var rtnVal = fncOpenModalPopup({
      height:500
      , width:800
      , windowURL:"/common/CommonController/retrievePowerMap.dev"
      , args:aryParam
    });

    return rtnVal;
  }

  function fncAgentSearchPopup(aryParam) {

    var rtnVal = fncOpenModalPopup({
      height:500
      , width:800
      , windowURL:"/common/CommonController/retrieveAgentPop.dev"
      , args:aryParam
    });

    return rtnVal;
  }

function fncCalendar(id) {
  $(function() {

    $('#'+id).datepicker({
      changeMonth: true,	changeYear: true,
      yearRange: '2009:2020',
      showOn: 'button', buttonImage: '/images/icon_cal.gif',
      buttonImageOnly: true,
      //buttonText:'달력',
      altField: '#'+id, altFormat: 'yy.mm.dd',
      defaultDate: '+0m+0d'
    });

  });
}

function fncProjectSearchPopup(aryParam) {

  // 일반 윈도우 팝업
  var rtnVal = fncOpenModalPopup({
      height:400
    , width:830
    , windowURL:"/project/ProjectController/retrieveProjectPop.dev"
    , args:aryParam
  });
  return rtnVal;
}

function fncKunnrSearchPopup(aryParam) {

	  // 일반 윈도우 팝업
	  var rtnVal = fncOpenModalPopup({
	      height:500
	    , width:830
	    , windowURL:"/activity/reqfac/RequestFactoryController/requestFactoryKunnrPop.dev"
	    , args:aryParam
	  });
	  return rtnVal;
	}

function fncKunnr2SearchPopup(aryParam) {

	  // 일반 윈도우 팝업
	  var rtnVal = fncOpenModalPopup({
	      height:500
	    , width:1000
	    , windowURL:"/activity/reqfac/RequestFactoryController/requestFactoryKunnr2Pop.dev"
	    , args:aryParam
	  });
	  return rtnVal;
	}

function fncApprovalSetupSearchPopup(aryParam) {

	  // 일반 윈도우 팝업
	  var rtnVal = fncOpenModalPopup({
	      height:500
	    , width:1000
	    , windowURL:"/activity/reqfac/RequestFactoryController/requestFactoryApprovalSetupPop.dev"
	    , args:aryParam
	  });
	  return rtnVal;
	}

function fncZtermSearchPopup(aryParam) {

	  // 일반 윈도우 팝업
	  var rtnVal = fncOpenModalPopup({
	      height:450
	    , width:630
	    , windowURL:"/activity/reqfac/RequestFactoryController/requestFactoryKunnr3.dev"
	    , args:aryParam
	  });
	  return rtnVal;
	}

function fncBillingSearchPopup(aryParam) {

	  // 일반 윈도우 팝업
	  var rtnVal = fncOpenModalPopup({
	      height:450
	    , width:1200
	    , windowURL:"/activity/reqfac/RequestFactoryController/requestFactoryBillingPop.dev"
	    , args:aryParam
	  });
	  return rtnVal;
	}


function fncStockSearchPopupList(vkorg, spart) {

	var arr = new Array();

	var rtnVal = fncOpenModalPopup({
        height:600
      , width:1200
      , windowURL:"/activity/reqfac/RequestFactoryController/stockStatusPop.dev?page_id=requestFactorySave&p_vkorg=" + vkorg + "&p_spart=" +spart
      ,args:arr
    });

    return rtnVal;
}

function fncStockSearchPopupUpdList(vkorg, user_id, lang_code) {

	var arr = new Array();

	var rtnVal = fncOpenModalPopup({
        height:600
      , width:1200
      , windowURL:"/activity/reqfac/RequestFactoryController/stockStatusPop.dev?page_id=detailUpd&p_vkorg=" + vkorg + "&user_id=" + user_id + "&lang_code=" + lang_code
      ,args:arr
    });

    return rtnVal;
}

function fncLogrtSearchPopup(aryParam) {

	  // 일반 윈도우 팝업
	  var rtnVal = fncOpenModalPopup({
	      height:150
	    , width:300
	    , windowURL:"/activity/reqfac/RequestFactoryController/requestFactoryLogrtPop.dev"
	    , args:aryParam
	  });
	  return rtnVal;
	}

function fncMatnrFixtSearchPopup(aryParam) {

	  // 일반 윈도우 팝업
	  var rtnVal = fncOpenModalPopup({
	      height:430
	    , width:300
	    , windowURL:"/activity/reqfac/RequestFactoryController/requestFactoryMatnrFixPop.dev"
	    , args:aryParam
	  });
	  return rtnVal;
	}


function fncRdPreview(frmRpt) {
    /*var sParam = "REPORT_FILE=" +fncGetObjById("REPORT_FILE").value;
    sParam += "&REPORT_PARAM=" +fncGetObjById("REPORT_PARAM").value;
    sParam += "&REPORT_NAME=" +fncGetObjById("REPORT_NAME").value;

    var val = fncOpenPopup({
          height:400
        , width:800
        , windowName:"_rd_pop"
        , windowURL:"<c:url value="/common/CommonController/rdPreview.dev"/>?" +sParam
      });*/
    //rptFrm.target = val;
    //rptFrm.action = "<c:url value="/common/CommonController/rdPreview.dev"/>";
    //rptFrm.submit();

    //var strOption = 'width=800, height =600, scrollbars=no, toolbars=yes, resizable=yes, left=100, top=50';
    //window.open( "", "_rd_pop", strOption);
    //rptFrm.target = "_rd_pop";
    //rptFrm.action = "<c:url value="/common/CommonController/rdPreview.dev"/>";
    //rptFrm.submit();

    var rdWin = fncOpenPopup({
        height:700
      , width:1000
      , windowName:"_rd_pop"
      , windowURL:"about:blank"
      , centerScreen:1
      , resizable:1
    });
    //rdWin.focus();
    frmRpt.target = "_rd_pop";
    frmRpt.action = "/common/CommonController/rdPreview.dev";
    frmRpt.submit();

    return rdWin;
}


/**
 * 입력한 날짜가 날짜형식에맞는지 확인한다.
 * @작성자 김경미
 * @class  fncIsDate : 입력한 날짜가 날짜형식에맞는지 확인한다.
 * @param  val 현재입력받은 객체의 Object Value
 * @return True / False
 */
function fncIsDate(val){
    var strDate  = "";
    var strYear  = "";
    var strMonth = "";
    var strDay   = "";

    strDate = val.replace(/\./gi,'');

    if (strDate.length < 8) return false;

    if (!Number(strDate)) return false;

    strYear  = strDate.substring(0,4);
    strMonth = strDate.substring(4,6);
    strDay   = strDate.substring(6,8);

    if (strYear.length == 4 && strMonth.length == 2 && strDay.length == 2){

        if (strYear < 1900 ) return false;
        if (strMonth < 1 || strMonth > 12)   return false;

        if (strDay > fncGetEndDayOfMonth(strYear, strMonth) || strDay < 1 ) return false;
    }

    return true;
}

/**
 * 월별 마지막 날짜를 리턴한다.
 * @작성자 김경미
 * @class  fncGetEndDayOfMonth : 월별 마지막 날짜를 리턴한다.
 * @param  strYear  년도
 * @param  strMonth 월
 * @return 월별 마지막날짜
 */
function fncGetEndDayOfMonth(strYear, strMonth) {

    var saMon = new Array(31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31);
    if(((strYear % 4 == 0) && (strYear % 100 != 0)) || (strYear % 400 == 0))
        saMon[1] = "29";
    return saMon[strMonth-1];
}

/**
 * 숫자형식의 포맷문자열을 반환한다.
 * @작성자 김경미
 * @class  fncToFormatCurrency : 숫자형식의 포맷문자열을 반환한다.
 * @param  num  포맷팅할 문자열
 * @return 포맷팅된 문자열(숫자형이 부적절하면 "" 반환)
 */
function fncToFormatCurrency(num) {
    var numData = "";
    var startRealNum = 0;
    var sign = "";//양수[],음수[-]
    numData = num;

    numData = numData.toString().replace(/\$|\,/g, '');

    if (isNaN(numData)) {
      numData = "";
      return numData;
    }
    if (numData.substring(0, 1) == "-") {
      sign = "-";
      numData = numData.substring(1);
    }

    //소숫점 및 "000.." 제거
    for ( var i = 0; i < numData.length; i++) {
      if (numData.charAt(i) != '0') {
        break;
      }
      startRealNum++;
    }

    if (numData.length != 1 && startRealNum > 0) {
      if (numData.charAt(startRealNum) == '.') {
        numData = numData.substring(startRealNum - 1);
      } else {
        numData = numData.substring(startRealNum);
      }
    }

    //소숫점 제거
    if (numData.charAt(0) == ".") {
      numData = "0." + numData.substring(1);
    }

    tmpNum = numData.split('.');
    if (tmpNum.length == 1) {
      numData = tmpNum[0];
      cents = "";
    } else if (tmpNum.length == 2) {
      numData = tmpNum[0];
      cents = tmpNum[1];
    } else {
      return "";
    }

    for ( var i = 0; i < Math.floor((numData.length - (1 + i)) / 3); i++)
      numData = numData.substring(0, numData.length - (4 * i + 3)) + ','
          + numData.substring(numData.length - (4 * i + 3));

    if (cents == "") {
      return sign + numData;
    } else {
      return sign + (numData + "." + cents);
    }
}

/**
* 날짜형식의 포맷문자열을 반환한다.
* @작성자 김경미
* @class  fncToFormatDate : 날짜형식의 포맷문자열을 반환한다.
* @param  num  포맷팅할 문자열
* @return 포맷팅된 문자열(날짜형이 부적절하면 "" 반환)
*/
function fncToFormatDate(date) {
    var dateData = "";
    dateData = date;
    //dateData = _replace(dateData.toString(),".","");
    dateData = dateData.replace(/\./gi, '');

    if (dateData == "") {
      return "";
    }
    if (isNaN(dateData) || dateData.length != 8) {
      dateData = "";
      return dateData;
    }
    if (!fncIsDate(dateData)) {
      return "";
    }
    dateData = dateData.substring(0, 4) + "." + dateData.substring(4, 6)
        + "." + dateData.substring(6, 8);
    return dateData;
}

/**
 * Null 여부를 Check 한다..
 * @작성자 강병준
 * @class  fncIsNull : Null Check
 * @param  value : OBJ
 */
function fncIsNull(value) {
  if (value == null || (typeof(value) == "string" && value.trim() == "") ) {
    return true;
  }
  return false;
}

/**
 * delete key를 누르면 해당 obj(NM, CD)를 CLEAR 한다..
 * @작성자 강병준
 * @class  fncClearObjValue : OBJ CLEAR
 * @param  nmObj  name OBJ
 * @param  cdObj  code OBJ
 */
function fncClearObjValue(nmObj, cdObj){

  if(window.event.keyCode == 46) {//DELETE
      if (!fncIsNull(nmObj)) {
        nmObj.value = "";
      }
      if (!fncIsNull(cdObj)) {
        cdObj.value = "";
      }
  }
  else if(window.event.keyCode == 8) {//BACK SPACE
      if (!fncIsNull(cdObj)) {
        cdObj.value = "";
      }
  }
}

/**

 */
function fncDisable(obj) {
  if (fncIsNull(obj)) {
    return;
  }

  if (obj.length != null) {
    for (var i = 0; i < obj.length; i++) {
      fncProcessChildElement(obj[i], fncDisableElement);
    }
  } else {
    fncProcessChildElement(obj, fncDisableElement);
  }
}

/**

 */
function fncDisableElement(oElement, argArr) {
  switch (fncGetElementType(oElement)) {
    case "BUTTON" :
      oElement.disabled = true;

      if (oElement.className != null &&
          (oElement.className.substr(0, 7) == "btnGrid" ||
           oElement.className.substr(0, 7) == "btnIcon" ) &&
          oElement.currentStyle.backgroundImage.substr(oElement.currentStyle.backgroundImage.length - 15, 9) != "_disabled"
         ) {
          oElement.style.backgroundImage =
            oElement.currentStyle.backgroundImage.substr(0, oElement.currentStyle.backgroundImage.length - 6) + "_disabled" +
            oElement.currentStyle.backgroundImage.substr(oElement.currentStyle.backgroundImage.length - 6);
      }

      break;

    case "CHECKBOX" :
    case "RADIO" :
    case "RESET" :
    case "SELECT" :
    case "SUBMIT" :
      oElement.disabled = true;
      oElement.style.color = "#4E4E4E";
      oElement.style.backgroundColor = "#e8e8e8";
      break;

    case "FILE" :
    case "PASSWORD" :
    case "TEXT" :
    case "TEXTAREA" :
      oElement.readOnly = true;
      //oElement.style.color = "#454648";
      //oElement.style.color = "#808080";
      oElement.style.color = "#4E4E4E";
      //oElement.style.backgroundColor = "#DEDEDE";"#e8e8e8"
      oElement.style.backgroundColor = "#e8e8e8";

      break;

    case "GE" :
//      oElement.ReadOnly = true;
//      oElement.ReadOnlyBackColor = "#DEDEDE";
//      oElement.ReadOnlyForeColor = "#454648";
      oElement.Enable = false;
      oElement.DisabledBackColor = "#DEDEDE";
      break;

    case "GCC" :
      oElement.Enable = false;
      break;

    case "GIF" :
    case "GRDO" :
    case "GTA" :
      oElement.Enable = false;
      break;

    default :
      break;
  }
}

/**

 */
function fncEnable(obj) {
  if (fncIsNull(obj)) {
    return;
  }

  if (obj.length != null) {
    for (var i = 0; i < obj.length; i++) {
      fncProcessChildElement(obj[i], fncEnableElement);
    }
  } else {
    fncProcessChildElement(obj, fncEnableElement);
  }
}

function fncEnableElement(oElement, argArr) {
  switch (fncGetElementType(oElement)) {
    case "BUTTON" :
      oElement.disabled = false;

      if (oElement.className != null &&
          (oElement.className.substr(0, 7) == "btnGrid" ||
           oElement.className.substr(0, 7) == "btnIcon"
          )
         ) {
          if (oElement.currentStyle.backgroundImage.substr(oElement.currentStyle.backgroundImage.length - 15, 9) == "_disabled") {
            oElement.style.backgroundImage =
              oElement.currentStyle.backgroundImage.cut(oElement.currentStyle.backgroundImage.length - 15, 9);
        }
      }

      break;

    case "CHECKBOX" :
    case "RADIO" :
    case "RESET" :
    case "SELECT" :
    case "SUBMIT" :
      oElement.disabled = false;
      break;

    case "FILE" :
    case "PASSWORD" :
    case "TEXT" :
    case "TEXTAREA" :
      oElement.readOnly = false;
      oElement.style.color = "#454648";
      oElement.style.backgroundColor = "";
      break;

    case "GE" :
//      oElement.ReadOnly = false;
      oElement.Enable = true;
      break;

    case "GCC" :
      oElement.Enable = true;
      break;

    case "GIF" :
    case "GRDO" :
    case "GTA" :
      oElement.Enable = true;
      break;

    default :
      break;
  }
}


function fncProcessChildElement(parentObj, fnc, argArr) {
    fnc(parentObj, argArr);

    if (parentObj.all == null) {
      return;
    }

    for (var i = 0; i < parentObj.all.length; i++) {
      fnc(parentObj.all[i], argArr);
    }
}

function fncGetElementType(oElement) {
    if (oElement == null) {
      return null;
    }

    switch (oElement.tagName) {
      case "INPUT":
        switch (oElement.type) {
          case "button" :
            return "BUTTON";
          case "checkbox" :
            return "CHECKBOX";
          case "file" :
            return "FILE";
          case "hidden" :
            return "HIDDEN";
          case "image" :
            return "IMAGE";
          case "password" :
            return "PASSWORD";
          case "radio" :
            return "RADIO";
          case "reset" :
            return "RESET";
          case "submit" :
            return "SUBMIT";
          case "text" :
            return "TEXT";
          default :
            return null;
        }
      case "IMG":
        return "IMG";
      case "SELECT":
        return "SELECT";
      case "TEXTAREA":
        return "TEXTAREA";
      default :
        return null;
    }
}

/**
 * delete key를 누르면 해당 obj(NM, CD)를 CLEAR 한다..
 * @작성자 강병준
 * @class  fncIsNull : Null Check
 * @param  value : OBJ
 */
function fncIsNull(value) {
  if (value == null || (typeof(value) == "string" && value.trim() == "") ) {
    return true;
  }
  return false;
}

/**
 * delete key를 누르면 해당 obj(NM, CD)를 CLEAR 한다..
 * @작성자 강병준
 * @class  fncClearObjValue : OBJ CLEAR
 * @param  nmObj  name OBJ
 * @param  cdObj  code OBJ
 */
function fncClearObjValue(nmObj, cdObj){

  if(window.event.keyCode == 46) {//DELETE
      if (!fncIsNull(nmObj)) {
        nmObj.value = "";
      }
      if (!fncIsNull(cdObj)) {
        cdObj.value = "";
      }
  }
  else if(window.event.keyCode == 8) {//BACK SPACE
      if (!fncIsNull(cdObj)) {
        cdObj.value = "";
      }
  }
}

/**

 */
function fncDisable(obj) {
  if (fncIsNull(obj)) {
    return;
  }

  if (obj.length != null) {
    for (var i = 0; i < obj.length; i++) {
      fncProcessChildElement(obj[i], fncDisableElement);
    }
  } else {
    fncProcessChildElement(obj, fncDisableElement);
  }
}

/**

 */
function fncDisableElement(oElement, argArr) {
  switch (fncGetElementType(oElement)) {
    case "BUTTON" :
      oElement.disabled = true;

      if (oElement.className != null &&
          (oElement.className.substr(0, 7) == "btnGrid" ||
           oElement.className.substr(0, 7) == "btnIcon" ) &&
          oElement.currentStyle.backgroundImage.substr(oElement.currentStyle.backgroundImage.length - 15, 9) != "_disabled"
         ) {
          oElement.style.backgroundImage =
            oElement.currentStyle.backgroundImage.substr(0, oElement.currentStyle.backgroundImage.length - 6) + "_disabled" +
            oElement.currentStyle.backgroundImage.substr(oElement.currentStyle.backgroundImage.length - 6);
      }

      break;

    case "CHECKBOX" :
    case "RADIO" :
    case "RESET" :
    case "SELECT" :
    case "SUBMIT" :
      oElement.disabled = true;
      oElement.style.color = "#000000";
      oElement.style.backgroundColor = "#e8e8e8";
      break;

    case "FILE" :
    case "PASSWORD" :
    case "TEXT" :
    case "TEXTAREA" :
      oElement.readOnly = true;
      //oElement.style.color = "#454648";
      //oElement.style.color = "#808080";
      oElement.style.color = "#4E4E4E";
      //oElement.style.backgroundColor = "#DEDEDE";"#e8e8e8"
      oElement.style.backgroundColor = "#e8e8e8";

      break;

    case "GE" :
//      oElement.ReadOnly = true;
//      oElement.ReadOnlyBackColor = "#DEDEDE";
//      oElement.ReadOnlyForeColor = "#454648";
      oElement.Enable = false;
      oElement.DisabledBackColor = "#DEDEDE";
      break;

    case "GCC" :
      oElement.Enable = false;
      break;

    case "GIF" :
    case "GRDO" :
    case "GTA" :
      oElement.Enable = false;
      break;

    default :
      break;
  }
}

/**

 */
function fncEnable(obj) {
  if (fncIsNull(obj)) {
    return;
  }

  if (obj.length != null) {
    for (var i = 0; i < obj.length; i++) {
      fncProcessChildElement(obj[i], fncEnableElement);
    }
  } else {
    fncProcessChildElement(obj, fncEnableElement);
  }
}

function fncEnableElement(oElement, argArr) {
  switch (fncGetElementType(oElement)) {
    case "BUTTON" :
      oElement.disabled = false;

      if (oElement.className != null &&
          (oElement.className.substr(0, 7) == "btnGrid" ||
           oElement.className.substr(0, 7) == "btnIcon"
          )
         ) {
          if (oElement.currentStyle.backgroundImage.substr(oElement.currentStyle.backgroundImage.length - 15, 9) == "_disabled") {
            oElement.style.backgroundImage =
              oElement.currentStyle.backgroundImage.cut(oElement.currentStyle.backgroundImage.length - 15, 9);
        }
      }

      break;

    case "CHECKBOX" :
    case "RADIO" :
    case "RESET" :
    case "SELECT" :
    case "SUBMIT" :
      oElement.disabled = false;
      oElement.style.color = "#454648";
      oElement.style.backgroundColor = "#ffffff";
      break;

    case "FILE" :
    case "PASSWORD" :
    case "TEXT" :
    case "TEXTAREA" :
      oElement.readOnly = false;
      oElement.style.color = "#454648";
      oElement.style.backgroundColor = "#ffffff";
      break;

    case "GE" :
//      oElement.ReadOnly = false;
      oElement.Enable = true;
      break;

    case "GCC" :
      oElement.Enable = true;
      break;

    case "GIF" :
    case "GRDO" :
    case "GTA" :
      oElement.Enable = true;
      break;

    default :
      break;
  }
}


function fncProcessChildElement(parentObj, fnc, argArr) {
    fnc(parentObj, argArr);

    if (parentObj.all == null) {
      return;
    }

    for (var i = 0; i < parentObj.all.length; i++) {
      fnc(parentObj.all[i], argArr);
    }
}

function fncGetElementType(oElement) {
    if (oElement == null) {
      return null;
    }

    switch (oElement.tagName) {
      case "INPUT":
        switch (oElement.type) {
          case "button" :
            return "BUTTON";
          case "checkbox" :
            return "CHECKBOX";
          case "file" :
            return "FILE";
          case "hidden" :
            return "HIDDEN";
          case "image" :
            return "IMAGE";
          case "password" :
            return "PASSWORD";
          case "radio" :
            return "RADIO";
          case "reset" :
            return "RESET";
          case "submit" :
            return "SUBMIT";
          case "text" :
            return "TEXT";
          default :
            return null;
        }
      case "IMG":
        return "IMG";
      case "SELECT":
        return "SELECT";
      case "TEXTAREA":
        return "TEXTAREA";
      default :
        return null;
    }
}
var windowHeight = $(window).height();
function SetHeight(element, value) {
    element.css({
        height: value,
        "max-height": value,
        overflow: "auto"
    });
}

function CreateMenuTreeForDropCommon(idTree, idHid, isFilter) {
	var data = new kendo.data.HierarchicalDataSource({
		transport: {
			read: {
				url: isFilter ? "../../Dept/GetDeptTreeViewFilter" : "../../Dept/GetDeptTreeView",
				contentType: "application/json"
			}
		},
		schema: {
			model: {
				id: "Id",
				children: "Children",
				hasChildren: "HasChildren"
			}
		}
	});

	function onParentSelect(e) {
		var data = this.dataItem(e.node);
		//$(idHid).val(GetIdParent(data.Id));
		$(idHid).val(data.Id);
	}

	$(idTree).kendoTreeView({
		Name: "MenuTreeDrop",
		dataSource: data,
		loadOnDemand: true,
		dataTextField: "EnName",
		select: onParentSelect
	});
}
function CreateDropdownCommon(idDtop, idTree) {
	var dropdown = $(idDtop).kendoDropDownList({
		dataSource: [{ text: "", value: "" }],
		dataTextField: "text",
		dataValueField: "value",
		open: function (e) {
			// If the treeview is not visible, then make it visible.
			if (!$treeviewRootElem.hasClass("k-custom-visible")) {
				$treeviewRootElem.slideToggle('fast', function () {
					dropdown.close();
					$treeviewRootElem.addClass("k-custom-visible");
				});
			}
		}
	}).data("kendoDropDownList");
	var $dropdownRootElem = $(dropdown.element).closest("span.k-dropdown");
	var treeview = $(idTree).kendoTreeView({
		select: function (e) {
			// When a node is selected, display the text for the node in the dropdown and hide the treeview.
			$dropdownRootElem.find("span.k-input").text($(e.node).children("div").text());
			$treeviewRootElem.slideToggle('fast', function () {
				$treeviewRootElem.removeClass("k-custom-visible");
			});
		}
	}).data("kendoTreeView");
	var $treeviewRootElem = $(treeview.element).closest("div.k-treeview");

	// Hide the treeview.
	$treeviewRootElem
		.width($dropdownRootElem.width())
		.css({ "border": "1px solid grey", "display": "none", "position": "absolute", "top": "50% !important" });

	// Position the treeview so that it is below the dropdown.
	$treeviewRootElem
		.css({ "top": $dropdownRootElem.position().top + $dropdownRootElem.height(), "left": $dropdownRootElem.position().left });

	$(document).click(function (e) {
		// Ignore clicks on the treetriew.
		if ($(e.target).closest("div.k-treeview").length == 0) {
			// If visible, then close the treeview.
			if ($treeviewRootElem.hasClass("k-custom-visible")) {
				$treeviewRootElem.slideToggle('fast', function () {
					$treeviewRootElem.removeClass("k-custom-visible");
				});
			}
		}
	});
}

function GetIdParent(id) {
	if (id === "") return "";
	var str = id + ",";
	$.ajax({
		url: "../../Dept/GetId",
		type: "POST",
		data: JSON.stringify({ id: id }),
		dataType: "json",
		async: false,
		contentType: "application/json; charset=utf-8",
		success: function (data) {
			str += data;
			if (str.split(",").length === 2 && (str.split(",")[0] === "31" || str.split(",")[0] === "202")) {
				str = "Not";
				return false;
			}
		},
		error: function (rs) {
			console.log();
		}
	});
	return str;
}

//Cookie
function createCookie(name, value, days) {
	if (days) {
		var date = new Date();
		date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
		var expires = "; expires=" + date.toGMTString();
	}
	else var expires = "";
	document.cookie = name + "=" + value + expires + "; path=/";
}

function readCookie(name) {
	var nameEQ = name + "=";
	var ca = document.cookie.split(';');
	for (var i = 0; i < ca.length; i++) {
		var c = ca[i];
		while (c.charAt(0) == ' ') c = c.substring(1, c.length);
		if (c.indexOf(nameEQ) == 0)
			return c.substring(nameEQ.length, c.length);
	}
	if (name == "H_UserID")
		window.parent.location.href = '../Login.aspx';
	return "";
}

function eraseCookie(name) {
	createCookie(name, "", -1);
}
//************************************************************************************
// weeklycalendar 
// Copyright (C) 2006, Massimo Beatini
//
// This software is provided "as-is", without any express or implied warranty. In 
// no event will the authors be held liable for any damages arising from the use 
// of this software.
//
// Permission is granted to anyone to use this software for any purpose, including 
// commercial applications, and to alter it and redistribute it freely, subject to 
// the following restrictions:
//
// 1. The origin of this software must not be misrepresented; you must not claim 
//    that you wrote the original software. If you use this software in a product, 
//    an acknowledgment in the product documentation would be appreciated but is 
//    not required.
//
// 2. Altered source versions must be plainly marked as such, and must not be 
//    misrepresented as being the original software.
//
// 3. This notice may not be removed or altered from any source distribution.
//
//************************************************************************************

// variable declarations
var w_d = new Date()
var w_monthname=new Array("01-Jan","02-Feb","03-Mar","04-Apr","05-May","06-Jun","07-Jul","08-Aug","09-Sep","10-Oct","11-Nov","12-Dec");
var w_dayname = new Array("Sun","Mon","Tue","Wed","Thu","Fri","Sat");
var w_StartOfWeek = 1; // Monday

// image title
var prev_month_title = "Previous month";
var next_month_title = "Next month";
var close_title = "Close";


// set the default position
// for the week end days (Saturday,Sunday)
var weekend_pos = new Array(6,0);


var w_min_year = 1906;
var w_max_year = 2060;

var gx = 0;
var gy = 0;

var w_linkedInputText_1;
var w_linkedInputText_2;


var HideWeekCol = false;


//
//
//
function w_funMouseMove(evnt) 
{   
	gx = evnt.pageX;
	gy = evnt.pageY;

	return true;   
}

//
// handle mousemove
//
if ((navigator.appName.indexOf("Netscape") != -1) || (navigator.appName.indexOf("Opera") != -1))
{
    if (document.onmousemove == undefined)
	    document.onmousemove = w_funMouseMove;
}

//
//
//
function w_changeMonth(id)
{
    var box = document.getElementById(id);
    var mm = box.options[box.selectedIndex].value;    
    w_d.setMonth(mm);
    w_renderCalendar(0);
}

//
//
//
function w_changeYear(id)
{
    var box = document.getElementById(id);
    var yy = box.options[box.selectedIndex].value;    
    w_d.setFullYear(yy);
    w_renderCalendar(0);
}

//
//
//
function w_getWeek(year,month,day){
    //Find JulianDay 
    month += 1; //use 1-12
    var a = Math.floor((14-(month))/12);
    var y = year+4800-a;
    var m = (month)+(12*a)-3;

    var jd = day + Math.floor(((153*m)+2)/5) + 
                 (365*y) + Math.floor(y/4) - Math.floor(y/100) + 
                 Math.floor(y/400) - 32045;      // (gregorian calendar)

/*
    var jd = (day+1)+Math.round(((153*m)+2)/5)+(365+y) + 
                     Math.round(y/4)-32083;    // (julian calendar)
*/
    
    //now calc weeknumber according to JD
    var d4 = (jd+31741-(jd%7))%146097%36524%1461;
    var L = Math.floor(d4/1460);
    var d1 = ((d4-L)%365)+L;
    NumberOfWeek = Math.floor(d1/7) + 1;
    return NumberOfWeek;        
}



//
//
//
function w_writeDayNumber(d)
{
	var dd = new Date(d);
	var temp = new Date(d);
	
	// reset data array
	for (i=0;i<6;i++)
	{
 		for (j=0;j<7;j++)
 		{
			document.getElementById("w_c" +i+""+j).innerHTML= "";
			document.getElementById("w_c" +i+""+j).className = "day_out";
 	    }
        document.getElementById("week_" +i).innerHTML = '';
	}
	
	// set to the first day of the month
	dd.setDate(1);
	
	// fill data array
	i = 0;
	j = 0;

    // previous month's days
    j = dd.getDay() - w_StartOfWeek;
    if (j<0)
        j = 7 + j;
    
    if (j > 0)
    {
        temp.setDate(dd.getDate()-1);
        for (k=j-1; k>=0; k--)
        {
			document.getElementById("w_c" +i+""+k).innerHTML= temp.getDate();
		    if ((weekend_pos[0] == k) || (weekend_pos[1] == k))
		        document.getElementById("w_c" +i+""+k).className = "weekends_out";
		    else
			    document.getElementById("w_c" +i+""+k).className = "day_out";

            temp.setDate(temp.getDate()-1);
        }
    }

	var week = -1;
    var iStartWeek = -1;
    var iEndWeek = -1;
    var weekEl;
    var dayval;

	do 
	{
	    // get the position according to
	    // StartOfWeek
	    j = dd.getDay() - w_StartOfWeek;
	    if (j<0)
	        j = 7 + j;
	
	    if (iStartWeek==-1)
	        iStartWeek = j;
        iEndWeek = j;
        	        
		dayval = dd.getDate();



	    // get the week number
	    if (week < 0)
	        week = w_getWeek(w_d.getFullYear(), w_d.getMonth(), dayval);
	        
		document.getElementById("w_c" +i+""+j).innerHTML= dayval;
		
		// set week ends layout
		if ((weekend_pos[0] == j) || (weekend_pos[1] == j))
		    document.getElementById("w_c" +i+""+j).className = "weekends";
		else
		    document.getElementById("w_c" +i+""+j).className = "day";
        {
            // set today layout 
            var today = new Date();
			
			if ((today.getDate() ==  dayval) && (today.getMonth() == w_d.getMonth()) && (today.getFullYear() == w_d.getFullYear()) )
				document.getElementById("w_c" +i+""+j).className = "today";
        }

        ////////
        // dynamically set the onclick event
        // only on the day of the selected month
        ////////

        var object = document.getElementById("w_c" +i+""+j);
        object.rownumber = i;
        object.colnumber = j;

        if(window.addEventListener){ // Mozilla, Netscape, Firefox
	        object.addEventListener('click', w_setDate, false);
        } else { // IE
	        object.attachEvent('onclick', w_setDate);
        }

        ///////
        
        
	    if (week < 0)
	        document.getElementById("week_" +i).innerHTML = '';
	    else if (w_StartOfWeek != 1)
	        document.getElementById("week_" +i).innerHTML = "&gt;";
	    else
	        document.getElementById("week_" +i).innerHTML = week;

        weekEl = document.getElementById("week_" +i);

        // if HideWeekCol change the class
        // hide the week col and its header
        if (HideWeekCol) 
        {
            weekEl.className = "weekhidden";
            document.getElementById("weekHeader").className = "weekhidden";
        }
        else
        {
            document.getElementById("weekHeader").className = "week";
            weekEl.className = "weeksel";
        }
        
		if (j == 6) 
		{
            ////////
            // dynamically set the onclick event
            // on the week number
            ////////
            weekEl.startweek = iStartWeek;
            weekEl.endweek = iEndWeek;
            weekEl.rowweek = i;

            if(window.addEventListener){ // Mozilla, Netscape, Firefox
                weekEl.addEventListener('click', w_SetWeekDate, false);
            } else { // IE
                weekEl.attachEvent('onclick', w_SetWeekDate);
            }
            ///////

		    week = -1;
		    iStartWeek = -1;
		    iEndWeek = -1;
		    i++;
		}
	
		dd.setDate(dd.getDate() + 1);
	
	} while (dd.getDate() != 1);
	
	if ((iStartWeek!=-1) && (iEndWeek!=-1))
	{
        weekEl = document.getElementById("week_" +i);
        
        ////////
        // dynamically set the onclick event
        // on the week number
        ////////
        weekEl.startweek = iStartWeek;
        weekEl.endweek = iEndWeek;
        weekEl.rowweek = i;

        if(window.addEventListener){ // Mozilla, Netscape, Firefox
            weekEl.addEventListener('click', w_SetWeekDate, false);
        } else { // IE
            weekEl.attachEvent('onclick', w_SetWeekDate);
        }
        ///////
    }
    
    // next month's days
    if ((j < 7))
    {
        temp = dd;
        for (k=j+1; k<7; k++)
        {
			document.getElementById("w_c" +i+""+k).innerHTML= temp.getDate();
	        if ((weekend_pos[0] == k) || (weekend_pos[1] == k))
	            document.getElementById("w_c" +i+""+k).className = "weekends_out";
	        else
			    document.getElementById("w_c" +i+""+k).className = "day_out";
            temp.setDate(temp.getDate()+1);
        }
    }


}


// 
// render calendar according to the selected
// month (k)
//
function w_renderCalendar(k) 
{
    var monthsel_html='';

	w_d.setMonth(w_d.getMonth() + k);
    
    monthsel_html += '<select class="nav" id="w_sel_month" onchange="w_changeMonth(\'w_sel_month\')">';
    for (im=0; im < 12; im++)
    {
        monthsel_html += '<option value="' + im + '" ' + ((im == w_d.getMonth())?'selected ':'')+ '>'+ w_monthname[im] + '</option>';
    }
    monthsel_html += '</select>';
    monthsel_html += ' ';


    monthsel_html += '<select class="nav" id="w_sel_year" onchange="w_changeYear(\'w_sel_year\')">';
    for (im = w_min_year; im <= w_max_year; im++)
    {
        monthsel_html += '<option value="' + im + '" ' + ((im == w_d.getFullYear())?'selected ':'')+ '>'+ im + '</option>';
    }
    monthsel_html += '</select>';
    monthsel_html += ' ';

	document.getElementById('w_month_year').innerHTML = monthsel_html;

    // write days number
    w_writeDayNumber(w_d);
 }
//
// set clicked date
//
function w_setDate(evt)
{
	var m="";
	var g="";
	var mMonth;
	var mDay;
	var i,j;
	

	var e_out;
	var ie_var = "srcElement";
	var moz_var = "target";
	var prop_var = "rownumber";

	// "target" for Mozilla, Netscape, Firefox et al. ; "srcElement" for IE
	evt[moz_var] ? e_out = evt[moz_var][prop_var] : e_out = evt[ie_var][prop_var];
	i = e_out;
	prop_var = "colnumber";
	evt[moz_var] ? e_out = evt[moz_var][prop_var] : e_out = evt[ie_var][prop_var];
	j = e_out;

		mMonth = (w_d.getMonth()+1);
		mDay = document.getElementById("w_c"+i+j).innerHTML;  	
		
		if(mMonth<10)
			m = "0" + mMonth
		else
			m = mMonth
	
	    ///////Sanjay
	    m = w_monthname[mMonth -1].toString().substr(3,4); 
	    
	    ///////
		if (mDay<10)
			g = "0" + mDay
		else
			g = mDay	
		
        // set the selected date
        try
        {        
		document.getElementById(w_linkedInputText_1).value = g + "-" + m + "-" + w_d.getFullYear();
        document.getElementById(w_linkedInputText_2).value = '';
 		}
 		catch(e){}

	    w_hiddenCalendar();
		document.getElementById(w_linkedInputText_1).focus();
}

//
// set week start and end date
// in the selected month
//
function w_SetWeekDate(evt)
{
	var m="";
	var g="";
	var mMonth;
	var mDay;
	var result = '';
	var startW = '';
	var endW = '';


	var e_out;
	var ie_var = "srcElement";
	var moz_var = "target";
	var prop_var = "startweek";
    var istartWeek, iendWeek;
    var rowWeek;
    
	// "target" for Mozilla, Netscape, Firefox et al. ; "srcElement" for IE
	evt[moz_var] ? e_out = evt[moz_var][prop_var] : e_out = evt[ie_var][prop_var];
	istartWeek = e_out;
	prop_var = "endweek";
	evt[moz_var] ? e_out = evt[moz_var][prop_var] : e_out = evt[ie_var][prop_var];
	iendWeek = e_out;

	prop_var = "rowweek";
	evt[moz_var] ? e_out = evt[moz_var][prop_var] : e_out = evt[ie_var][prop_var];
	rowWeek = e_out;


    mMonth = (w_d.getMonth()+1);

    if(mMonth<10)
        m = "0" + mMonth
    else
        m = mMonth

    mDay = document.getElementById("w_c"+rowWeek+istartWeek).innerHTML;  	
    if (mDay<10)
	    g = "0" + mDay
    else
	    g = mDay	
    		
    startW = g + "/" + m + "/" + w_d.getFullYear();

    mDay = document.getElementById("w_c"+rowWeek+iendWeek).innerHTML;  	
    if (mDay<10)
	    g = "0" + mDay
    else
	    g = mDay	

    endW = g + "/" + m + "/" + w_d.getFullYear();
    
    // set the selected date
    try
    {
    document.getElementById(w_linkedInputText_1).value = startW;
    document.getElementById(w_linkedInputText_2).value = endW;
    }
    catch(e)
    {};
	
    w_hiddenCalendar();
	document.getElementById(w_linkedInputText_2).focus();
}

//
// display date picker
// hide the col week
//
function w_displayDatePicker(linkedId1)
{
	w_linkedInputText_1 = linkedId1;
	w_linkedInputText_2 = null;

    HideWeekCol = true;	
    w_displayCal();
}

//
// display calendar
//
function w_displayCalendar(linkedId1, linkedId2) 
{
	w_linkedInputText_1 = linkedId1;
	w_linkedInputText_2 = linkedId2;

    HideWeekCol = false;	
    w_displayCal();
}


function w_displayCal() {
    w_renderCalendar(0);
	if(navigator.userAgent.indexOf("MSIE") != -1) {
	    document.getElementById('weeklyCalendar').style.left = window.event.clientX + document.body.scrollLeft + "px";
//dal added to account for long scroll

	    if (document.body.scrollTop > 0) {
	        //weeklyCalendar.style.top = window.event.y;
	        document.getElementById('weeklyCalendar').style.top = window.event.clientY + "px";
	    }
	    else {

//	        var IE = document.all ? true : false;
//	        if (IE) {
//	            //  alert('ss')
//	            tempX = event.clientX + document.body.scrollLeft
//	            tempY = event.clientY + document.body.scrollTop
//}
	            var d = (document.documentElement && document.documentElement.scrollLeft != null) ? document.documentElement : document.body;
	            document.getElementById('weeklyCalendar').style.top = event.clientY + d.scrollTop + "px";
	            //weeklyCalendar.style.top=window.event.y+39;
	       
	    }
	} 
	else if ((navigator.appName.indexOf("Netscape") != -1) || (navigator.appName.indexOf("Opera") != -1))
	{
		document.getElementById('weeklyCalendar').style.left=gx + 5 + "px";
		document.getElementById('weeklyCalendar').style.top=gy + 5 + "px";
	}
	document.getElementById('weeklyCalendar').style.visibility = "visible";
	document.getElementById('weeklyCalendar').style.position = "absolute";
}

//
// hidden calendar
//
function w_hiddenCalendar() 
{
	document.getElementById('weeklyCalendar').style.visibility='hidden';

	
    // remove the attached events	
    var i, j;
    var week;
    var daycol;
    
    for (i = 0; i < 7; i++)
    {
        // detach event from week element
        try
        {
            week = document.getElementById("week_" +i);
            if(window.removeEventListener()){ // Mozilla, Netscape, Firefox
	            week.removeEventListener('click', w_SetWeekDate, false);
            } else { // IE
	            week.detachEvent('onclick', w_SetWeekDate);
            }
        }
        catch(e){};

        // detach event from each day col
        try
        {
            for (j=0; j <7; j++)
            {
                daycol = document.getElementById("w_c" +i+""+j);
                if(window.removeEventListener()){ // Mozilla, Netscape, Firefox
	                daycol.removeEventListener('click', w_setDate, false);
                } else { // IE
	                daycol.detachEvent('onclick', w_setDate);
                }
            }
        }
        catch(e){};
    }
	
}


function w_writeDayname()
{
    var mDay;
    document.write('<tr>');

    document.write('<td class="week" id="weekHeader">Week</td>');

    for(wd =0; wd < 7; wd++)
    {
        mDay = wd + w_StartOfWeek;

        if (mDay > 6)
            mDay = mDay-7;
        document.write('<td class="wd">' + w_dayname[mDay] + '</td>');
        
        // set week ends postion
        if (w_dayname[mDay] == 'Sat')
            weekend_pos[0] = wd;
        if (w_dayname[mDay] == 'Sun')
            weekend_pos[1] = wd;

    }

    document.write('</tr>');

}

///
///
///
function buildWeeklyCalendar(WeekStart, Position) {
    if (WeekStart != undefined)
        w_StartOfWeek = WeekStart;

    document.write('<div id="weeklyCalendar" class="calendar">');
    document.write('<table class="calendar" >');
    document.write('<tr><td colspan="8">');
    // header table
    document.write('<table width="100%" cellpading="0" cellspacing="0">');
    var pathArray = location.pathname.split('/');
    pathArray = pathArray[0];
    //Jayant 04-03-2008-Begin
    if (WeekStart == 0 && Position == 0) {
        document.write('<tr class="firstrow"><td width="8px" onClick="w_renderCalendar(-1);" align="right" ><img src="' + pathArray + '../images/arrow_left.gif" title="' + prev_month_title + '" border="0"></td>');
        document.write('<td width="8px" onClick="w_renderCalendar(1);" align="left" ><img src="'+pathArray+'../images/arrow_right.gif" title="' + next_month_title + '" border="0"></td>');
    }
    else {
        document.write('<tr class="firstrow"><td width="8px" onClick="w_renderCalendar(-1);" align="right" ><img src="/' + pathArray + '../images/arrow_left.gif" title="' + prev_month_title + '" border="0"></td>');
        document.write('<td width="8px" onClick="w_renderCalendar(1);" align="left" ><img src="' + pathArray + '../images/arrow_right.gif" title="' + next_month_title + '" border="0"></td>');
    }
    //End

    document.write('<td colspan="4" id="w_month_year" align="center">');
    document.write('<select id="w_sel_month">');

    for (im = 0; im < 12; im++) {
        document.write('<option value="' + im + '" ' + ((im == w_d.getMonth()) ? 'selected ' : '') + '>' + w_monthname[im] + '</option>');
    }
    document.write('</select>');
    document.write(' ');
    document.write('<select id="w_sel_year">');

    for (im = w_min_year; im <= w_max_year; im++) {
        document.write('<option value="' + im + '" ' + ((im == w_d.getFullYear()) ? 'selected ' : '') + '>' + im + '</option>');
    }
    document.write('</select>');

    document.write('</td>');
    //Jayant 04-03-2008-Begin
    if (WeekStart == 0 && Position == 0) { document.write('<td align="center" onClick="w_hiddenCalendar()"><img src="' + pathArray + '../images/close.gif" title="' + close_title + '" border="0"></td>'); }
    else { document.write('<td align="center" onClick="w_hiddenCalendar()"><img src="' + pathArray + '../images/close.gif" title="' + close_title + '" border="0"></td>'); }
    //End

    document.write('</tr>');

    document.write('</table>');
    // end header table

    document.write('</td></tr>');

    w_writeDayname();

    // init day/week number
    for (i = 0; i < 6; i++) {
        document.write('  <tr>');

        //	    document.write('<td class="weeksel" id="week_'+ i + '" onClick="w_SetWeekDate(' + i + ')">&nbsp;Select&nbsp;</td>');
        document.write('<td class="weeksel" id="week_' + i + '">&nbsp;Select&nbsp;</td>');

        for (j = 0; j < 7; j++) {
            //	            document.write('<td onClick="w_setDate('+i+','+j+')" class="day_out" id="w_c' + i + j + '">&nbsp;</td>');
            document.write('<td  class="day_out" onmouseover="return escape(\'This is area 1\')" id="w_c' + i + j + '">&nbsp;</td>');
        }
        document.write('  </tr>');
    }
    document.write('</table></div>');

}




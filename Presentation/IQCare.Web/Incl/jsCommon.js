//// JScript File
////Global Variable

//var dt1 = "";
//var dt2 = "";

//function getMessage(txt1, txt2, txt3, txt4, txt5, txt6, txt7, txt8, txt9, txt10, txt11, txt12, chk1,chk2, rdo1) 
//{ 
//var objtxt1 = document.getElementById(txt1);
//var objtxt2 = document.getElementById(txt2);
//var objtxt3 = document.getElementById(txt3);
//var objtxt4 = document.getElementById(txt4);
//var objtxt5 = document.getElementById(txt5);
//var objtxt6 = document.getElementById(txt6);
//var objtxt7 = document.getElementById(txt7);
//var objtxt8 = document.getElementById(txt8);
//var objtxt9 = document.getElementById(txt9);
//var objtxt10 = document.getElementById(txt10);
//var objtxt11 = document.getElementById(txt11);
//var objtxt12 = document.getElementById(txt12);
//var objchk1 = document.getElementById(chk1);
//objchk1.type == "checkbox";
//var objchk2 = document.getElementById(chk2);
//objchk2.type == "checkbox";
//objrdo1 = document.getElementById(rdo1);

//if (objtxt1.value != "" || objtxt2.value != "" || objtxt3.value != "" || objtxt4.value != "" || objtxt5.value != "" || objtxt6.value != "" || objtxt7.value != "" || objtxt8.value != "" || objtxt9.value != "" || objtxt10.value != "" || objtxt11.value != "" || objtxt12.value != "" || objchk1.checked==false)
//{
//    var ans; 
//    ans=window.confirm('All fields in previous exposure will be cleared.....?'); 
//    if (ans==true) 
//    { 
//    objtxt1.value = "";
//    objtxt2.value = "";
//    objtxt3.value = "";
//    objtxt4.value = "";
//    objtxt5.value = "";
//    objtxt6.value = "";
//    objtxt7.value = "";
//    objtxt8.value = "";
//    objtxt9.value = "";
//    objtxt10.value = "";
//    objtxt11.value = "";
//    objtxt12.value = "";
//        if(objchk1.type == "checkbox")
//        {
//        objchk1.checked=false;
//        }
//        if(objchk2.type == "checkbox")
//        {
//        objchk2.checked=false;
//        }
//   }
//else 
// { 
//   if (objrdo1.type == "radio")
//   {
//    objrdo1.checked=true;
//   }
//  } 
// } 
//} 

//function AddBoundary(Control,MinRange,MaxRange)
//{
//   var objtxt = document.getElementById(Control);
//     
//   var len = objtxt.value.toString().length;
//   if ( len > 0)
//   {
//  
//   if(Number(objtxt.value) < Number(MinRange) && (MinRange.toString().length) < len)
//   {
//         objtxt.value = objtxt.value.toString().substr(0,objtxt.value.length - 1);
//         objtxt.focus();
//   }
//       
//   if(Number(objtxt.value) > Number(MaxRange))
//      {
//         objtxt.value = objtxt.value.toString().substr(0,objtxt.value.length - 1);
//         objtxt.focus();
//      }
//   }
//}

//function CheckValue(Control, MinRange)
//{
//   var objtxt = document.getElementById(Control);
//   var len = objtxt.value.toString().length;
//   if (len>0)
//   {
//       if(Number(objtxt.value) < Number(MinRange))
//       {
//           objtxt.value = "";
//           objtxt.focus();
//       }
//   }
//}

//function validate(InitEval)
//{
//if (document.InitEval.physKarnofskyScore.value.length > 3 || document.InitEval.physKarnofskyScore.value > 100  || document.InitEval.physKarnofskyScore .value < 1 )
//	{
//		alert("Please enter a score between 1 and 100.");
//		document.InitEval.physKarnofskyScore.focus();
//		return false;
//	}
//		
//	else
//	{
//	return true;
//    }
//}

//// Function for checking value lies in specified range
//// obj-->Object
//// Disp--> Display name for error alert
//// lo---> Lower value
//// hi---> Higher Value

//function isBetween (obj, Disp, lo, hi) { 

//var objtxt = document.getElementById(obj);
//val = document.getElementById(obj).value
//val = parseInt(val);
//lo = parseInt(lo);
//hi = parseInt(hi);
//if ((val < lo) || (val > hi)) 
//{
//	
//	alert(Disp+" should be in the range of " +lo+ " and " + hi)
//	objtxt.value = "";
//	objtxt.focus();
//	objtxt.select();
//	return(false);
//} 
//else 
//	{
//		return(true);
//	} 
//    
//} 
////Function to check valid date - Date Format or Future Date
//function isCheckValidDate(sysdate, frmdate, obj)
//{
//    var objtxt = document.getElementById(obj);
//    var frmdatetxt=(frmdd + "-"+ fmm + "-" + frmyr);
//    //System Date
//	var sysdatetxt;
//	var sysdd = sysdate.toString().substr(0,2);
//    var sysmm = sysdate.toString().substr(3,3);
//    var sysyr = sysdate.toString().substr(7,4);
//    var smm;
//	    switch(sysmm.toLowerCase()){
//		case "jan": smm = "01";
//		break;
//		case "feb": smm = "02";
//		break;
//		case "mar": smm = "03";
//		break;
//		case "apr": smm = "04";		
//		break;
//		case "may": smm = "05";
//		break;
//		case "jun": smm = "06";
//		break;
//		case "jul": smm = "07";	
//		break;
//		case "aug": smm = "08";
//		break;
//		case "sep": smm = "09";
//		break;
//		case "oct": smm = "10";
//		break;
//		case "nov": smm = "11";	
//		break;
//		case "dec": smm = "12";	
//		break;
//	}
//    var sysdatetxt=(sysdd + "-"+ smm + "-" + sysyr);
//    
//    //frmdate
//    var frmdatetxt = document.getElementById(frmdate).value;
//    var frmdd = document.getElementById(frmdate).value.toString().substr(0,2);
//    var frmmm = document.getElementById(frmdate).value.toString().substr(3,3);
//    var frmyr = document.getElementById(frmdate).value.toString().substr(7,4);
//    var fmm;
//	    switch(frmmm.toLowerCase()){
//		case "jan": fmm = "01";
//		break;
//		case "feb": fmm = "02";
//		break;
//		case "mar": fmm = "03";
//		break;
//		case "apr": fmm = "04";		
//		break;
//		case "may": fmm = "05";
//		break;
//		case "jun": fmm = "06";
//		break;
//		case "jul": fmm = "07";	
//		break;
//		case "aug": fmm = "08";
//		break;
//		case "sep": fmm = "09";
//		break;
//		case "oct": fmm = "10";
//		break;
//		case "nov": fmm = "11";	
//		break;
//		case "dec": fmm = "12";	
//		break;
//	}
//	var frmdatetxt=(frmdd + "-"+ fmm + "-" + frmyr);
//  	if(frmdatetxt <= sysdatetxt)
//  	return true;
//	alert("Invalid date\nCheck DateFormat or\nCheck Future Date");
//	objtxt.value = "";
//	objtxt.focus();
//	objtxt.select();
//	return false;
//}
////HIV related History 

//function isCheckValidDateHIVrelated(HIVdate, frmdate, obj)
//{
//    var objtxt = document.getElementById(obj);
//    
//    //HIV related Date
//	var HIVdatetxt;
//    var HIVdatetxt = document.getElementById(HIVdate).value;
//    var HIVdd = document.getElementById(HIVdate).value.toString().substr(0,2);
//    var HIVmm = document.getElementById(HIVdate).value.toString().substr(3,3);
//    var HIVyr = document.getElementById(HIVdate).value.toString().substr(7,4);
//    var hmm;
//	    switch(HIVmm.toLowerCase()){
//		case "jan": hmm = "01";
//		break;
//		case "feb": hmm = "02";
//		break;
//		case "mar": hmm = "03";
//		break;
//		case "apr": hmm = "04";		
//		break;
//		case "may": hmm = "05";
//		break;
//		case "jun": hmm = "06";
//		break;
//		case "jul": hmm = "07";	
//		break;
//		case "aug": hmm = "08";
//		break;
//		case "sep": hmm = "09";
//		break;
//		case "oct": hmm = "10";
//		break;
//		case "nov": hmm = "11";	
//		break;
//		case "dec": hmm = "12";	
//		break;
//	}
//	var HIVdatetxt=(HIVdd + "-"+ hmm + "-" + HIVyr);
//    //frmdate
//    var frmdatetxt = document.getElementById(frmdate).value;
//    var frmdd = document.getElementById(frmdate).value.toString().substr(0,2);
//    var frmmm = document.getElementById(frmdate).value.toString().substr(3,3);
//    var frmyr = document.getElementById(frmdate).value.toString().substr(7,4);
//    var fmm;
//	    switch(frmmm.toLowerCase()){
//		case "jan": fmm = "01";
//		break;
//		case "feb": fmm = "02";
//		break;
//		case "mar": fmm = "03";
//		break;
//		case "apr": fmm = "04";		
//		break;
//		case "may": fmm = "05";
//		break;
//		case "jun": fmm = "06";
//		break;
//		case "jul": fmm = "07";	
//		break;
//		case "aug": fmm = "08";
//		break;
//		case "sep": fmm = "09";
//		break;
//		case "oct": fmm = "10";
//		break;
//		case "nov": fmm = "11";	
//		break;
//		case "dec": fmm = "12";	
//		break;
//	}
//	var frmdatetxt=(frmdd + "-"+ fmm + "-" + frmyr);
//	
//  	if(frmdatetxt <= HIVdatetxt)
//  	return true;
//	alert("Invalid date\nCheck DateFormat or\nCheck Visit Date");
//	objtxt.value = "";
//	objtxt.focus();
//	objtxt.select();
//	return false;
//}

////Function to check/uncheck list

//function display_chklist(ChkboxId, Id)
//{
//    e = document.getElementById(ChkboxId);
//    if(e.type == "checkbox")
//    {
//        if (e.checked==false)
//        {
//          d = document.getElementById(Id);
//          d.style.display = '';
//        }
//        else if (e.checked = true)
//        {
//            d = document.getElementById(Id);
//            d.style.display = "none";
//        }
//    }
//}

////function to uncheckListItems
//function disableListItems(checkBoxListId, numOfItems)
//{
//    var objChkID = document.getElementById(checkBoxListId).value;
//    objChkID = parseInt(objChkID);
//        
//   // Does the checkboxlist not exist?
//    if(objChkID == null)
//    {
//       return;
//    }
//    var objItem;
//    var i = 0;

//    // Loop through the checkboxes in the list.
//    for(i = 0; i < numOfItems; i++)
//    {
// 
//        objItem = document.getElementById(checkBoxListId + '_' + i);
//        //alert(objItem);
//        if (objItem.checked == true)
//        {
//           // objItem.checked = false;
//            objItem.click();
//        }
// 
//    }       
//}
////function to uncheck Checkboxlist items through radio button

//function disableChkboxList(Id, numOfItems)
//{
//    TotItems = numOfItems;
//    for (i=0; i<TotItems; i++)
//    {
//      ChkListID = document.getElementById(Id + '_' + i);
//      if(ChkListID.type == "checkbox")
//      {
//      ChkListID.checked=false;
//      }
//    }
//}

////function to uncheck griditems

//function disableGridItems(GridId, numOfItems)
//{
//    TotItems = numOfItems+1;
//    for(i=2; i < TotItems; i++)
//    {
//      if (i<=9)
//      {
//      grdId = document.getElementById(GridId + '_' + 'ctl0' + i + '_' + 'ChkBox');
//      if(grdId.type == "checkbox")
//        {
//          grdId.checked=false;
//        }
//      }
//     else
//     {
//      grdId = document.getElementById(GridId + '_' + 'ctl' + i + '_' + 'ChkBox');
//      if(grdId.type == "checkbox")
//        {
//          grdId.checked=false;
//        }
//      }
//    }
//}

//function disableCheckbox(Id)
//{
//    ChkId = document.getElementById(Id);
//    if(ChkId.type == "checkbox")
//    {
//    ChkId.checked=false;
//    }
//}

//function disableRadioNone(Id )
//{
//    rdoId = document.getElementById(Id);
//    if(rdoId.type == "radio")
//    {
//    rdoId.checked=false;
//    }
//}

//function disableRadioNotDocumented(Id )
//{
//    rdoId = document.getElementById(Id);
//    if(rdoId.type == "radio")
//    {
//    rdoId.checked=false;
//    }
//}

//////For Field Level Integer Validation////////////
//function IsInteger(IntString)
////  check for valid numeric strings	
//{ 
//   var IntValidChars = "0123456789";
//   var IntChar;
//   var blnResult = true;
//   if (IntString.length == 0) return false;
//   //test strString consists of valid characters listed above
//   for (i = 0; i < IntString.length && blnResult == true; i++)
//      {
//      IntChar = IntString.charAt(i);
//      //alert(strChar);
//      if (IntValidChars.indexOf(IntChar) == -1)
//         {
//         blnResult = false;
//         }
//      }
//   return blnResult;
//}

//function chkInteger(TextID)
//{
// var objtxt = document.getElementById(TextID);
// if (IsInteger(objtxt.value) == false) 
//  {
//  objtxt.value = objtxt.value.toString().substr(0,objtxt.value.length - 1);
//  objtxt.focus();
//  }
//}

////////////////   For Field Level Numeric Validation ////////////////////
//function IsNumeric(strString)
////  check for valid numeric strings	
//{ 
//   var strValidChars = "0123456789.-";
//   var strChar;
//   var blnResult = true;
//   if (strString.length == 0) return false;
//   //test strString consists of valid characters listed above
//   for (i = 0; i < strString.length && blnResult == true; i++)
//      {
//      strChar = strString.charAt(i);
//      //alert(strChar);
//      if (strValidChars.indexOf(strChar) == -1)
//         {
//         blnResult = false;
//         }
//      }
//   return blnResult;
//}

//function chkNumeric(TextID)
//{
// var objtxt = document.getElementById(TextID);
// if (IsNumeric(objtxt.value) == false) 
//  {
//  objtxt.value = objtxt.value.toString().substr(0,objtxt.value.length - 1);
//  objtxt.focus();
//  }
//}
//function IsDecimal(strString)
////  check for valid numeric strings	
//{ 
//   var strValidChars = "0123456789.";
//   var strChar;
//   var blnResult = true;
//   if (strString.length == 0) return false;
//   //test strString consists of valid characters listed above
//   for (i = 0; i < strString.length && blnResult == true; i++)
//      {
//      strChar = strString.charAt(i);
//      //alert(strChar);
//      if (strValidChars.indexOf(strChar) == -1)
//         {
//         blnResult = false;
//         }
//      }
//   return blnResult;
//}

//function chkDecimal(TextID)
//{
// var objtxt = document.getElementById(TextID);
// if (IsDecimal(objtxt.value) == false) 
//  {
//  objtxt.value = objtxt.value.toString().substr(0,objtxt.value.length - 1);
//  objtxt.focus();
//  }
//}
/////////////////////////////////////////////////////////////

////////////////   For Field Level String Validation ////////////////////
//function IsString(strString)
////  check for valid numeric strings	
//{ 
//   var strValidChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ-/";
//   var strChar;
//   var blnResult = true;
//   if (strString.length == 0) return false;
//   //test strString consists of valid characters listed above
//   for (i = 0; i < strString.length && blnResult == true; i++)
//      {
//      strChar = strString.charAt(i);
//      //alert(strChar);
//      if (strValidChars.indexOf(strChar) == -1)
//         {
//         blnResult = false;
//         }
//      }
//   return blnResult;
//}

//function chkString(TextID)
//{
// var objtxt = document.getElementById(TextID);
// if (IsString(objtxt.value) == false) 
//  {
//  objtxt.value = objtxt.value.toString().substr(0,objtxt.value.length - 1);
//  objtxt.focus();
//  }
//}
/////////////////////////////////////////////////////////////
/////////////////Copied from scheduler//////////////////////
////////////////////////////////////////////////////////////
//function SetCheckBoxes(FormName, FieldName, CheckValue)
//{
//    var objCheckBoxes;
//  	if(!document.forms[FormName])
//  	return;
//	
//	var objCheckBoxes = document.forms[FormName].elements[FieldName];
//									
//	if(!objCheckBoxes)
//		return;
//	var countCheckBoxes = objCheckBoxes.length;
//	if(!countCheckBoxes)
//	
//	if (objCheckBoxes.checked == true)
//	    {
//		objCheckBoxes.checked = CheckValue;
//		}
//	else
//		// set the check value for all check boxes
//		for(var i = 0; i < countCheckBoxes; i++)
//			objCheckBoxes[i].checked = CheckValue;
//}

////Generic function to toggle elements
//function toggle(divId)
//{
//var Id = divId;
//if (document.layers)
//  {
//    if(document.layers[Id].visibility == "hide")
//    { show(Id);
//      return;
//    }
//    else if (document.layers[Id].visibility == "show")
//    { hide(Id);
//      return;
//    }
//  }
//if (document.all)
//  { if(document.all[Id].style.display == "none")
//      { show(Id);
//        return;
//      }
//    else if (document.all[Id].style.display == "inline")
//      { hide(Id);
//        return;
//      }
//  }
//else if (document.getElementById)
//  {
//    if(document.getElementById(Id).style.display == "none")
//      { show(Id);
//        return;
//      }
//    else if (document.getElementById(Id).style.display == "inline")
//      { 
//       hide(Id);
//       return;
//      }
//  }
//}


////hides div
//function hide(divId) {
//if (document.layers) document.layers[divId].visibility = 'hide';
//else if (document.all) document.all[divId].style.display = 'none';
//else if (document.getElementById) document.getElementById(divId).style.display = 'none';
//}

////shows div
//function show(divId) {
//if (document.layers) document.layers[divId].visibility = 'show';
//else if (document.all) document.all[divId].style.display = 'inline';
//else if (document.getElementById) document.getElementById(divId).style.display = 'inline';
//}

////ARV Therapy Validations
//function Therapy(Id, value)
//{
//    var cdivId = 'otherarvTherapyChangeCode';
//    var sdivId = 'otherarvTherapyStopCode';
//   
//    if (Id == 'Other' && value == 1)
//    {
//   
//    hide(sdivId);
//    show(cdivId);
//    }
//    if(Id == 'Other' && value == 2)
//    {
//    
//    hide(cdivId);
//    show(sdivId);
//    }
//    if (Id != 'Other')
//    {
//    
//    hide(sdivId);
//    hide(cdivId);
//    }
//}


//function specify(id)
//{
//var selectId = id;
//var sdivId = 'other' + selectId;
//sdivId = document.getElementById('Other' + id);

//if (document.getElementById(selectId)[document.getElementById(selectId).selectedIndex].value != Other)
//{
//hide (sdivId);
//}
//else 
//{
//show (sdivId);
//}
//}


//function specifyCounsellorName(CounsellorID, CName)
//{
//document.getElementById(CName).value = CounsellorID;
//}

//function specifyChangeStop(id)
//{
//var selectId = id;
//var sdivId = 'arvTherapyStop';
//var cdivId = 'arvTherapyChange';
//var asdivId = 'Adherance_counsellor_signature';
//var ChangeRegimen = 281;
//var Stoptreatment = 282;
//var AdheranceSign = 3;
//var PatientSign = 2;
//var NoSign = 1;
//if (document.getElementById(selectId)[document.getElementById(selectId).selectedIndex].value == ChangeRegimen)
//{
//show (cdivId);
//hide (sdivId);
//}
//else if(document.getElementById(selectId)[document.getElementById(selectId).selectedIndex].value == Stoptreatment)
//{
//show (sdivId);
//hide (cdivId);
//}
//else if(document.getElementById(selectId)[document.getElementById(selectId).selectedIndex].value == AdheranceSign)
//{
//show (asdivId);
////hide (asdivId);
//}
//else if
//(document.getElementById(selectId)[document.getElementById(selectId).selectedIndex].value == PatientSign)
//{
////show (asdivId);
// hide (asdivId);
//}
//else if
//(document.getElementById(selectId)[document.getElementById(selectId).selectedIndex].value == NoSign)
//{
////show (asdivId);
// hide (asdivId);
//}
//else
//{ 
////hide (otherId);
//hide (sdivId);
//hide (cdivId);
//}
//}

//function dispense(id)
//{
//var selectId = id;
//var sdivId = 'pharmReportedbyShow';

//if (document.getElementById(selectId).value != '')
//{
//show(sdivId);
//}
//}

////To check forms mandatory validations

//var form = "";
//var submitted = false;
//var error = false;
//var error_message = "";

//function check_input(field_name, message) {
//  if (form.elements[field_name] && (form.elements[field_name].type != "hidden")) 
//    {
//    var field_value = form.elements[field_name].value;

//    if (field_value == '') {
//      error_message = error_message + "* " + message + "\n";
//      error = true;
//    }
//  }
//}


//function check_form(form_name) {
//  if (submitted == true) {
//    alert("This form has already been submitted. Please press Ok and wait for this process to be completed.");
//    return false;
//  }

//  error = false;
//  form = form_name;
//  error_message = "Errors have occured during the process of your form.\n\nPlease make the following corrections:\n\n";
//  check_input("firstname", "First Name Cannot be blank");
//  
//  if (error == true) {
//    alert(error_message);
//    return false;
//  } else {
//    submitted = true;
//    return true;
//  }

//////////////////////////////////////////////////////////////////////
/////Clinicalheaderfooter
////To trim right 0's from EnrolmentID
//function unique_Enrolment(Enrol_ID)
//{
//var Enrol_no = document.getElementById(Enrol_ID).value;
//Enrol_no = (Enrol_no/10)*10;
//document.getElementById(Enrol_ID).value = Enrol_no;
//}

////Function to find Date Difference
//function isDate(p_Expression){
//	return !isNaN(new Date(p_Expression));		// <<--- this needs checking
//}

//function dateDiff(p_Interval, p_Date1, p_Date2, p_firstdayofweek, p_firstweekofyear){
//	if(!isDate(p_Date1)){return "invalid date: '" + p_Date1 + "'";}
//	if(!isDate(p_Date2)){return "invalid date: '" + p_Date2 + "'";}
//	var dt1 = new Date(p_Date1);
//	var dt2 = new Date(p_Date2);

//	// get ms between dates (UTC) and make into "difference" date
//	var iDiffMS = dt2.valueOf() - dt1.valueOf();
//	var dtDiff = new Date(iDiffMS);

//	// calc various diffs
//	var nYears  = dt2.getUTCFullYear() - dt1.getUTCFullYear();
//	var nMonths = dt2.getUTCMonth() - dt1.getUTCMonth() + (nYears!=0 ? nYears*12 : 0);
//	var nQuarters = parseInt(nMonths/3);	//<<-- different than VBScript, which watches rollover not completion
//	
//	var nMilliseconds = iDiffMS;
//	var nSeconds = parseInt(iDiffMS/1000);
//	var nMinutes = parseInt(nSeconds/60);
//	var nHours = parseInt(nMinutes/60);
//	var nDays  = parseInt(nHours/24);
//	var nWeeks = parseInt(nDays/7);

//	// return requested difference
//	var iDiff = 0;		
//	switch(p_Interval.toLowerCase()){
//		case "yyyy": return nYears;
//		case "q": return nQuarters;
//		case "m": return nMonths;
//		case "y": 		// day of year
//		case "d": return nDays;
//		case "w": return nDays;
//		case "ww":return nWeeks;		// week of year	// <-- inaccurate, WW should count calendar weeks (# of sundays) between
//		case "h": return nHours;
//		case "n": return nMinutes;
//		case "s": return nSeconds;
//		case "ms":return nMilliseconds;	// millisecond	// <-- extension for JS, NOT available in VBScript
//		default: return "invalid interval: '" + p_Interval + "'";
//	}
//}
////Function to calculate date
//function CalculateDate(Date1, Date2)
//{
//        var yr1 = Date1.substr(7,4);
//        var yr2 = Date2.substr(7,4);
//        var mm1 = Date1.substr(3,3);
//        var mm2 = Date2.substr(3,3);
//        var dd1 = Date1.substr(0,2);
//        var dd2 = Date2.substr(0,2);

//    var nmm1;
//	switch(mm1.toLowerCase()){
//		case "jan": nmm1= "01";
//		break;
//		case "feb": nmm1= "02";
//		break;
//		case "mar": nmm1= "03";
//		break;
//		case "apr": nmm1= "04";		
//		break;
//		case "may": nmm1= "05";
//		break;
//		case "jun": nmm1= "06";
//		break;
//		case "jul": nmm1= "07";	
//		break;
//		case "aug": nmm1= "08";
//		break;
//		case "sep": nmm1= "09";
//		break;
//		case "oct": nmm1= "10";
//		break;
//		case "nov": nmm1= "11";	
//		break;
//		case "dec": nmm1= "12";	
//		break;
//	}
//	var nmm2;
//	switch(mm2.toLowerCase()){
//		case "jan": nmm2= "01";
//		break;
//		case "feb": nmm2= "02";
//		break;
//		case "mar": nmm2= "03";
//		break;
//		case "apr": nmm2= "04";		
//		break;
//		case "may": nmm2= "05";
//		break;
//		case "jun": nmm2= "06";
//		break;
//		case "jul": nmm2= "07";	
//		break;
//		case "aug": nmm2= "08";
//		break;
//		case "sep": nmm2= "09";
//		break;
//		case "oct": nmm2= "10";
//		break;
//		case "nov": nmm2= "11";	
//		break;
//		case "dec": nmm2= "12";	
//		break;
//	}
//     dt1 = nmm1 + "/" + dd1 + "/" + yr1;
//     dt2 = nmm2 + "/" + dd2 + "/" + yr2;
//     return;
//}

////Function to Calculate Age for Height in ART Followup Form
//function CalculateAgeHeight(Height,txtheight, txtDT1,txtDT2)
//{
//   var txtdT = document.getElementById(txtDT2).value;
//   CalculateDate(txtDT1, txtdT);
//   var val1 = dateDiff("d",dt1,dt2,"","")/365;
//   if (val1 > 18)
//   {if (Height != 0)
//   {document.getElementById(txtheight).value = Height}
//   else {document.getElementById(txtheight).value = "";}
//   }
//   return false;
//}

//////Function to Calculate Age for Enrolment Form
//function CalcualteAge(txtAge,txtmonth,txtDT1,txtDT2)
//{
//    var txtDT1 = document.getElementById(txtDT1).value;
//    var txtDT2 = document.getElementById(txtDT2).value;
//    if (txtDT1 == "" || txtDT2 == "")
//	{
//	 document.getElementById(txtmonth).value ="";
//	 document.getElementById(txtAge).value="";
//	 return true;
//	}
//    CalculateDate(txtDT1, txtDT2);
//    var val1 = dateDiff("d",dt1,dt2,"","")/365;
//    var val2 = Math.round((dateDiff("d",dt1,dt2,"","")/365));
//    if (val2 > val1 )
//    {
//    document.getElementById(txtAge).value = Math.round((dateDiff("d",dt1,dt2,"","")/365))-1;  
//    var yr= Math.round(dateDiff("d",dt1,dt2,"","")/365)-1;
//    document.getElementById(txtmonth).value = Math.round((dateDiff("d",dt1,dt2,"","") -  (365*yr))/30);  
//    }
//    else
//    {
//     document.getElementById(txtAge).value = Math.round((dateDiff("d",dt1,dt2,"","")/365));  
//     var yr= Math.round(dateDiff("d",dt1,dt2,"","")/365);
//     document.getElementById(txtmonth).value = Math.round((dateDiff("d",dt1,dt2,"","") -  (365*yr))/30);  
//    }
//}


////ARV Message
//function getMessage(txt1, txt2, txt3, txt4, txt5, txt6, txt7, txt8, txt9, txt10, txt11, txt12, chk1,chk2, rdo1) 
//{ 
//var objtxt1 = document.getElementById(txt1); var objtxt2 = document.getElementById(txt2); var objtxt3 = document.getElementById(txt3);
//var objtxt4 = document.getElementById(txt4); var objtxt5 = document.getElementById(txt5); var objtxt6 = document.getElementById(txt6);
//var objtxt7 = document.getElementById(txt7); var objtxt8 = document.getElementById(txt8); var objtxt9 = document.getElementById(txt9);
//var objtxt10 = document.getElementById(txt10); var objtxt11 = document.getElementById(txt11); var objtxt12 = document.getElementById(txt12);
//objchk1 = document.getElementById(chk1); objchk1.type == "checkbox"
//objchk2 = document.getElementById(chk2); objchk2.type == "checkbox"
//objrdo1 = document.getElementById(rdo1); objrdo1.type == "radio"
// if (objtxt1.value != "" || objtxt2.value != "" || objtxt3.value != "" || objtxt4.value != "" || objtxt5.value != "" || objtxt6.value != "" || objtxt7.value != "" || objtxt8.value != "" || objtxt9.value != "" || objtxt10.value != "" || objtxt11.value != "" || objtxt12.value != "" || objchk1.checked==true || objchk2.checked==true)
// {
// var ans; 
// ans=window.confirm('All fields in previous exposure will be cleared.....?'); 
//    if (ans==true) 
//    { 
//     objtxt1.value = ""; objtxt2.value = ""; objtxt3.value = ""; objtxt4.value = ""; objtxt5.value = "";
//     objtxt6.value = ""; objtxt7.value = ""; objtxt8.value = ""; objtxt9.value = ""; objtxt10.value = "";
//     objtxt11.value = ""; objtxt12.value = ""; objchk1.checked=false; objchk2.checked=false;
//     hide('prevexpdiv');
//     return;
//    }
//    else 
//    {  
//    objrdo1.checked=true;
//    }
//  } 
//  if ((objtxt1.value == "") && (objtxt2.value == "") && (objtxt3.value == "") && (objtxt4.value == "") && (objtxt5.value == "") && (objtxt6.value == "") && (objtxt7.value == "") && (objtxt8.value == "") && (objtxt9.value == "") && (objtxt10.value == "") && (objtxt11.value == "") && (objtxt12.value == "") && (objchk1.checked==false) && (objchk2.checked==false))
//  {
//    hide('prevexpdiv');
//    return;
//  }
//} 


////function AddBoundary(Control,MinRange,MaxRange)
////{
////   var objtxt = document.getElementById(Control);
////     
////   var len = objtxt.value.toString().length;
////   if ( len > 0)
////   {
////  
////   if(Number(objtxt.value) < Number(MinRange) && (MinRange.toString().length) < len)
////   {
////         objtxt.value = objtxt.value.toString().substr(0,objtxt.value.length - 1);
////         objtxt.focus();
////   }
////       
////   if(Number(objtxt.value) > Number(MaxRange))
////      {
////         objtxt.value = objtxt.value.toString().substr(0,objtxt.value.length - 1);
////         objtxt.focus();
////      }
////   }
////}

////function CheckValue(Control, MinRange)
////{
////   var objtxt = document.getElementById(Control);
////   var len = objtxt.value.toString().length;
////   if (len>0)
////   {
////       if(Number(objtxt.value) < Number(MinRange))
////       {
////           objtxt.value = "";
////           objtxt.focus();
////       }
////   }
////}

////function validate(InitEval)
////{
////if (document.InitEval.physKarnofskyScore.value.length > 3 || document.InitEval.physKarnofskyScore.value > 100  || document.InitEval.physKarnofskyScore .value < 1 )
////	{
////		alert("Please enter a score between 1 and 100.");
////		document.InitEval.physKarnofskyScore.focus();
////		return false;
////	}
////	else
////	{
////	return true;
////    }
////}

//// Function for checking value lies in specified range
//// obj-->Object
//// Disp--> Display name for error alert
//// lo---> Lower value
//// hi---> Higher Value

////function isBetween (obj, Disp, lo, hi) { 

////var objtxt = document.getElementById(obj);
////val = document.getElementById(obj).value
////val = parseInt(val);
////lo = parseInt(lo);
////hi = parseInt(hi);
////if ((val < lo) || (val > hi)) 
////{
////	
////	alert(Disp+" should be in the range of " +lo+ " and " + hi)
////	objtxt.value = "";
////	objtxt.focus();
////	objtxt.select();
////	return(false);
////} 
////else 
////	{
////		return(true);
////	} 
////    
////} 

////Function to check valid date - Date Format or Future Date
//function isCheckValidDate(sysdate, frmdate, obj)
//{
//    var objtxt = document.getElementById(obj);
//    //form date
//    var frmdatetxt = document.getElementById(frmdate).value;
//    if (frmdatetxt == "")
//	{
//	 return true;
//	}
//    var frmdatetxt=(frmdd + "-"+ fmm + "-" + frmyr);
//    //System Date
//	var sysdatetxt;
//	var sysdd = sysdate.toString().substr(0,2);
//    var sysmm = sysdate.toString().substr(3,3);
//    var sysyr = sysdate.toString().substr(7,4);
//    var smm;
//	    switch(sysmm.toLowerCase()){
//		case "jan": smm = "01";
//		break;
//		case "feb": smm = "02";
//		break;
//		case "mar": smm = "03";
//		break;
//		case "apr": smm = "04";		
//		break;
//		case "may": smm = "05";
//		break;
//		case "jun": smm = "06";
//		break;
//		case "jul": smm = "07";	
//		break;
//		case "aug": smm = "08";
//		break;
//		case "sep": smm = "09";
//		break;
//		case "oct": smm = "10";
//		break;
//		case "nov": smm = "11";	
//		break;
//		case "dec": smm = "12";	
//		break;
//	}
//    var sysdatetxt=(sysyr+smm+sysdd);
//    //frmdate
//    var frmdatetxt = document.getElementById(frmdate).value;
//    var frmdd = document.getElementById(frmdate).value.toString().substr(0,2);
//    var frmmm = document.getElementById(frmdate).value.toString().substr(3,3);
//    var frmyr = document.getElementById(frmdate).value.toString().substr(7,4);
//    var fmm;
//	    switch(frmmm.toLowerCase()){
//		case "jan": fmm = "01";
//		break;
//		case "feb": fmm = "02";
//		break;
//		case "mar": fmm = "03";
//		break;
//		case "apr": fmm = "04";		
//		break;
//		case "may": fmm = "05";
//		break;
//		case "jun": fmm = "06";
//		break;
//		case "jul": fmm = "07";	
//		break;
//		case "aug": fmm = "08";
//		break;
//		case "sep": fmm = "09";
//		break;
//		case "oct": fmm = "10";
//		break;
//		case "nov": fmm = "11";	
//		break;
//		case "dec": fmm = "12";	
//		break;
//	}
//	
//	var frmdatetxt=(frmyr+fmm+frmdd);
//		
//  	if(frmdatetxt <= sysdatetxt)
//  	return true;
//	alert("Invalid date\nCheck DateFormat or\nCheck Future Date");
//	objtxt.value = "";
//	objtxt.focus();
//	objtxt.select();
//	return false;
//}

////Comparsion of Other Dates with Visit Date 
//function isCheckValidDateHIVrelated(frmvisitdate, frmotherdate, Disp, obj)
//{
//    
//    var objtxt = document.getElementById(obj);
//    //Other Date
//	var frmothertxt = document.getElementById(frmotherdate).value;
//	if (frmothertxt == "")
//	{
//	 return true;
//	}

//	//Visit Date
//	var frmvisitdatetxt = document.getElementById(frmvisitdate).value;
//	if (frmvisitdatetxt == "")
//	{
//	    alert("Please Enter Visit Date First");
//	    objtxt.value = "";
//	    objtxt.focus();
//	    objtxt.select();
//	    return false;
//	}
//    var frmvisitdatetxt = document.getElementById(frmvisitdate).value;
//    var frmvisitdd = document.getElementById(frmvisitdate).value.toString().substr(0,2);
//    var frmvisitmm = document.getElementById(frmvisitdate).value.toString().substr(3,3);
//    var frmvisityr = document.getElementById(frmvisitdate).value.toString().substr(7,4);
//    var hmm;
//	    switch(frmvisitmm.toLowerCase()){
//		case "jan": hmm = "01";
//		break;
//		case "feb": hmm = "02";
//		break;
//		case "mar": hmm = "03";
//		break;
//		case "apr": hmm = "04";		
//		break;
//		case "may": hmm = "05";
//		break;
//		case "jun": hmm = "06";
//		break;
//		case "jul": hmm = "07";	
//		break;
//		case "aug": hmm = "08";
//		break;
//		case "sep": hmm = "09";
//		break;
//		case "oct": hmm = "10";
//		break;
//		case "nov": hmm = "11";	
//		break;
//		case "dec": hmm = "12";	
//		break;
//	}
//	var frmvisitdatetxt=(frmvisityr+hmm+frmvisitdd);

//    //form Other dates
//    var frmothertxt = document.getElementById(frmotherdate).value;
//    var frmotherdd = document.getElementById(frmotherdate).value.toString().substr(0,2);
//    var frmothermm = document.getElementById(frmotherdate).value.toString().substr(3,3);
//    var frmotheryr = document.getElementById(frmotherdate).value.toString().substr(7,4);
//    var fmm;
//	    switch(frmothermm.toLowerCase()){
//		case "jan": fmm = "01";
//		break;
//		case "feb": fmm = "02";
//		break;
//		case "mar": fmm = "03";
//		break;
//		case "apr": fmm = "04";		
//		break;
//		case "may": fmm = "05";
//		break;
//		case "jun": fmm = "06";
//		break;
//		case "jul": fmm = "07";	
//		break;
//		case "aug": fmm = "08";
//		break;
//		case "sep": fmm = "09";
//		break;
//		case "oct": fmm = "10";
//		break;
//		case "nov": fmm = "11";	
//		break;
//		case "dec": fmm = "12";	
//		break;
//	}
//	var frmothertxt=(frmotheryr+fmm+frmotherdd);
//  	if(frmothertxt <= frmvisitdatetxt)
//  	return true;
//  	alert(Disp+" date should be before or equal to visit date");
//	objtxt.value = "";
//	objtxt.focus();
//	objtxt.select();
//	return false;
//}

////Comparsion of Other Dates(MMM-YYYYY) with Visit Date 
//function isCheckValidDate_MMM_YR(frmvisitdate, frmotherdate, Disp, obj)
//{
//    var objtxt = document.getElementById(obj);
//    //Other Date
//	var frmothertxt = document.getElementById(frmotherdate).value;
//	if (frmothertxt == "")
//	{
//	 return true;
//	}
//   
//	//Visit Date
//	var frmvisitdatetxt = document.getElementById(frmvisitdate).value;
//	
//	if (frmvisitdatetxt == "")
//	{
//	    alert("Please Enter Visit Date First");
//	    objtxt.value = "";
//	    objtxt.focus();
//	    objtxt.select();
//	    return false;
//	}
//    var frmvisitdatetxt = document.getElementById(frmvisitdate).value;
//    var frmvisitdd = document.getElementById(frmvisitdate).value.toString().substr(0,2);
//    var frmvisitmm = document.getElementById(frmvisitdate).value.toString().substr(3,3);
//    var frmvisityr = document.getElementById(frmvisitdate).value.toString().substr(7,4);
//    var hmm;
//	    switch(frmvisitmm.toLowerCase()){
//		case "jan": hmm = "01";
//		break;
//		case "feb": hmm = "02";
//		break;
//		case "mar": hmm = "03";
//		break;
//		case "apr": hmm = "04";		
//		break;
//		case "may": hmm = "05";
//		break;
//		case "jun": hmm = "06";
//		break;
//		case "jul": hmm = "07";	
//		break;
//		case "aug": hmm = "08";
//		break;
//		case "sep": hmm = "09";
//		break;
//		case "oct": hmm = "10";
//		break;
//		case "nov": hmm = "11";	
//		break;
//		case "dec": hmm = "12";	
//		break;
//	}
//	var frmvisitdatetxt=(frmvisityr+hmm);

//    //form Other dates
//    var frmothertxt = document.getElementById(frmotherdate).value;
//    var frmothermm = document.getElementById(frmotherdate).value.toString().substr(0,3);
//    var frmotheryr = document.getElementById(frmotherdate).value.toString().substr(4,4);
//        
//    var fmm;
//	    switch(frmothermm.toLowerCase()){
//		case "jan": fmm = "01";
//		break;
//		case "feb": fmm = "02";
//		break;
//		case "mar": fmm = "03";
//		break;
//		case "apr": fmm = "04";		
//		break;
//		case "may": fmm = "05";
//		break;
//		case "jun": fmm = "06";
//		break;
//		case "jul": fmm = "07";	
//		break;
//		case "aug": fmm = "08";
//		break;
//		case "sep": fmm = "09";
//		break;
//		case "oct": fmm = "10";
//		break;
//		case "nov": fmm = "11";	
//		break;
//		case "dec": fmm = "12";	
//		break;
//	}

//	var frmothertxt=(frmotheryr+fmm);
//  	if(frmothertxt <= frmvisitdatetxt)
//  	return true;
//  	alert(Disp+" date should be before or equal to the Month & Year of visit date");
//	objtxt.value = "";
//	objtxt.focus();
//	objtxt.select();
//	return false;
//}
//////////////////////////////////////////////

////Function to check/uncheck list
//function display_chkddl(textBoxID, Id)
//{
//    e = document.getElementById(textBoxID);
//    if(e.type == "textbox")
//    {
//        if (e.text = "")
//        {
//          d = document.getElementById(Id);
//          d.style.display = '';
//        }
//        else if (e.text != "")
//        {
//            d = document.getElementById(Id);
//            d.style.display = "none";
//        }
//    }
//}

/////////////////////////////////////////////////

////Function to check/uncheck list
////function display_chklist(ChkboxId, Id)
////{
////    e = document.getElementById(ChkboxId);
////    if(e.type == "checkbox")
////    {
////        if (e.checked==false)
////        {
////          d = document.getElementById(Id);
////          d.style.display = '';
////        }
////        else if (e.checked = true)
////        {
////            d = document.getElementById(Id);
////            d.style.display = "none";
////        }
////    }
////}

////function to uncheckListItems
////function disableListItems(checkBoxListId, numOfItems)
////{
////    var objChkID = document.getElementById(checkBoxListId).value;
////    objChkID = parseInt(objChkID);
////        
////   // Does the checkboxlist not exist?
////    if(objChkID == null)
////    {
////       return;
////    }
////    var objItem;
////    var i = 0;

////    // Loop through the checkboxes in the list.
////    for(i = 0; i < numOfItems; i++)
////    {
////         objItem = document.getElementById(checkBoxListId + '_' + i);
////        //alert(objItem);
////        if (objItem.checked == true)
////        {    
////            objItem.checked = false;
////            objItem.style.backcolor = 'red';
////            objItem.style.forecolor = 'black';
////            objItem.nextSibling.style.color ='black';
////            
//////            alert(objItem.style.backcolor);
//////            alert(objItem.style.forecolor);
////            //objItem.nextSibling.style.color='red';
////             //objItem.style.bgcolor= 'white';
////          //objItem.setcolor= 'white';

////          //objItem.style.background= 'white';
////        }
//// 
////    }       
////}
////function to uncheck Checkboxlist items through radio button
////function disableChkboxList(Id, numOfItems)
////{
////    TotItems = numOfItems;
////    for (i=0; i<TotItems; i++)
////    {
////      ChkListID = document.getElementById(Id + '_' + i);
////      if(ChkListID.type == "checkbox")
////      {
////          ChkListID.checked=false;
////          ChkListID.style.backcolor= 'red';
////          ChkListID.style.forecolor = 'black';
////          ChkListID.nextSibling.style.color = 'black';
//////          alert(ChkListID.style.backcolor);
//////          alert(ChkListID.style.forecolor);
////          //ChkListID.setcolor= 'white';
////          //ChkListID.nextSibling.style.color='red';
////          // ChkListID.style.bgcolor= 'white';
////          //ChkListID.style.background= 'white';
////      }
////    }
////}

////function to uncheck griditems
////function disableGridItems(GridId, numOfItems)
////{
////    TotItems = numOfItems+1;
////    for(i=2; i < TotItems; i++)
////    {
////      if (i<=9)
////      {
////      grdId = document.getElementById(GridId + '_' + 'ctl0' + i + '_' + 'ChkBox');
////      if(grdId.type == "checkbox")
////        {
////          grdId.checked=false;
////        }
////      }
////     else
////     {
////      grdId = document.getElementById(GridId + '_' + 'ctl' + i + '_' + 'ChkBox');
////      if(grdId.type == "checkbox")
////        {
////          grdId.checked=false;
////        }
////      }
////    }
////}

////function disableCheckbox(Id)
////{
////    ChkId = document.getElementById(Id);
////    if(ChkId.type == "checkbox")
////    {
////    ChkId.checked=false;
////    }
////}

////function disableRadioNone(Id )
////{
////    rdoId = document.getElementById(Id);
////    if(rdoId.type == "radio")
////    {
////    rdoId.checked=false;
////    }
////}

////function disableRadioNotDocumented(Id )
////{
////    rdoId = document.getElementById(Id);
////    if(rdoId.type == "radio")
////    {
////    rdoId.checked=false;
////    }
////}

//////For Field Level Integer Validation////////////
////function IsInteger(IntString)
//////  check for valid numeric strings	
////{ 
////   var IntValidChars = "0123456789";
////   var IntChar;
////   var blnResult = true;
////   if (IntString.length == 0) return false;
////   //test strString consists of valid characters listed above
////   for (i = 0; i < IntString.length && blnResult == true; i++)
////      {
////      IntChar = IntString.charAt(i);
////      //alert(strChar);
////      if (IntValidChars.indexOf(IntChar) == -1)
////         {
////         blnResult = false;
////         }
////      }
////   return blnResult;
////}

////function chkInteger(TextID)
////{
//// var objtxt = document.getElementById(TextID);
//// if (IsInteger(objtxt.value) == false) 
////  {
////  objtxt.value = objtxt.value.toString().substr(0,objtxt.value.length - 1);
////  objtxt.focus();
////  }
////}

////////////////   For Field Level Numeric Validation ////////////////////
////function IsNumeric(strString)
//////  check for valid numeric strings	
////{ 
////   var strValidChars = "0123456789-";
////   var strChar;
////   var blnResult = true;
////   if (strString.length == 0) return false;
////   //test strString consists of valid characters listed above
////   for (i = 0; i < strString.length && blnResult == true; i++)
////      {
////      strChar = strString.charAt(i);
////      //alert(strChar);
////      if (strValidChars.indexOf(strChar) == -1)
////         {
////         blnResult = false;
////         }
////      }
////   return blnResult;
////}

////function chkNumeric(TextID)
////{
//// var objtxt = document.getElementById(TextID);
//// if (IsNumeric(objtxt.value) == false) 
////  {
////  objtxt.value = objtxt.value.toString().substr(0,objtxt.value.length - 1);
////  objtxt.focus();
////  }
////}
////function IsDecimal(strString)
//////  check for valid numeric strings	
////{ 
////   var strValidChars = "0123456789.";
////   var strChar;
////   var blnResult = true;
////   if (strString.length == 0) return false;
////   //test strString consists of valid characters listed above
////   for (i = 0; i < strString.length && blnResult == true; i++)
////      {
////      strChar = strString.charAt(i);
////      //alert(strChar);
////      if (strValidChars.indexOf(strChar) == -1)
////         {
////         blnResult = false;
////         }
////      }
////   return blnResult;
////}

////function chkDecimal(TextID)
////{
//// var objtxt = document.getElementById(TextID);
//// if (IsDecimal(objtxt.value) == false) 
////  {
////  objtxt.value = objtxt.value.toString().substr(0,objtxt.value.length - 1);
////  objtxt.focus();
////  }
////}
/////////////////////////////////////////////////////////////

////////////////   For Field Level String Validation ////////////////////
////function IsString(strString)
//////  check for valid numeric strings	
////{ 
////   var strValidChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ-/";
////   var strChar;
////   var blnResult = true;
////   if (strString.length == 0) return false;
////   //test strString consists of valid characters listed above
////   for (i = 0; i < strString.length && blnResult == true; i++)
////      {
////      strChar = strString.charAt(i);
////      //alert(strChar);
////      if (strValidChars.indexOf(strChar) == -1)
////         {
////         blnResult = false;
////         }
////      }
////   return blnResult;
////}

////function chkString(TextID)
////{
//// var objtxt = document.getElementById(TextID);
//// if (IsString(objtxt.value) == false) 
////  {
////  objtxt.value = objtxt.value.toString().substr(0,objtxt.value.length - 1);
////  objtxt.focus();
////  }
////}

////ARV Therapy Validations - for IE and Follow Up
////function Therapy(Id, value)
////{
////    var cdivId = 'otherarvTherapyChangeCode';
////    var sdivId = 'otherarvTherapyStopCode';
////   
////    if (Id == 'Other' && value == 1)
////    {
////   
////    hide(sdivId);
////    show(cdivId);
////    }
////    if(Id == 'Other' && value == 2)
////    {
////    
////    hide(cdivId);
////    show(sdivId);
////    }
////    if (Id != 'Other')
////    {
////    
////    hide(sdivId);
////    hide(cdivId);
////    }
////}

////Function for Death Reason
//function DeathReason(Id, value)
//{
//    var deathdivId = 'specifyDeathReason';
//    var dropreason = 'dropOther';
//   
//    if (Id != 'Unknown' && value == 1)
//    {
//       show(deathdivId);
//    }
//    
//    if(Id == 'Other' && value == 2)
//    {
//    show(dropreason);
//    }
//    
//    if (Id == 'Unknown' && value == 1)
//    {
//    hide(deathdivId);
//    }
//    
//    if (Id != 'Other' && value == 2)
//    {
//    hide(dropreason);
//    } 
//   
//}

////function specifyCounsellorName(CounsellorID, CName)
////{
////document.getElementById(CName).value = CounsellorID;
////}

//function specifyChangeStop(id)
//{
//var selectId = id;
//var sdivId = 'arvTherapyStop';
//var cdivId = 'arvTherapyChange';
//var asdivId = 'Adherance_counsellor_signature';
//var ChangeRegimen = 98;
//var Stoptreatment = 99;
//var AdheranceSign = 3;
//var PatientSign = 2;
//var NoSign = 1;
//if (document.getElementById(selectId)[document.getElementById(selectId).selectedIndex].value == ChangeRegimen)
//{
//show (cdivId);
//hide (sdivId);
//}
//else if(document.getElementById(selectId)[document.getElementById(selectId).selectedIndex].value == Stoptreatment)
//{
//show (sdivId);
//hide (cdivId);
//}
//else if(document.getElementById(selectId)[document.getElementById(selectId).selectedIndex].value == AdheranceSign)
//{
//show (asdivId);
////hide (asdivId);
//}
//else if(document.getElementById(selectId)[document.getElementById(selectId).selectedIndex].value == PatientSign)
//{
////show (asdivId);
// hide (asdivId);
//}
//else if(document.getElementById(selectId)[document.getElementById(selectId).selectedIndex].value == NoSign)
//{
////show (asdivId);
// hide (asdivId);
//}
//else
//{ 
////hide (otherId);
//hide (sdivId);
//hide (cdivId);
//}
//}


////function dispense(id)
////{
////    var selectId = id;
////    var sdivId = 'pharmReportedbyShow';

////    if (document.getElementById(selectId).value != '')
////    {
////    show(sdivId);
////    }
////}


////Function for hide and unhide weeks in HomeVisit. on schedular header footer
//    
//function GetWeeks(txtNumofWeeks)
//{
//   var VisitWeekDiv   = 'VisitPerWeekShow';
//   var VisitWeekShow1 = 'VisitPerWeekShow1';
//   var VisitWeekShow2 = 'VisitPerWeekShow2';
//   var VisitWeekShow3 = 'VisitPerWeekShow3';
//   var VisitWeekShow4 = 'VisitPerWeekShow4';
//   
//   var val =document.getElementById(txtNumofWeeks).value;
//   alert('sanjay');   
//   if(val == 1)
//   {
//        
//        show(VisitWeekDiv);	
//        show(VisitWeekShow1);
//        alert('sanjay');   
//        hide(VisitWeekShow2);
//        hide(VisitWeekShow3);
//        hide(VisitWeekShow4);
//   }
//   else if(val == 2)
//   {
//        show(VisitWeekDiv);
//        show(VisitWeekShow1);
//        show(VisitWeekShow2);
//        hide(VisitWeekShow3);
//        hide(VisitWeekShow4);
//   }
//   else if(val == 3)
//   {
//        show(VisitWeekDiv);
//        show(VisitWeekShow1);
//        show(VisitWeekShow2);
//        show(VisitWeekShow3);
//        hide(VisitWeekShow4);
//   }
//   else if(val == 4)
//   {
//        show(VisitWeekDiv);
//        show(VisitWeekShow1);
//        show(VisitWeekShow2);
//        show(VisitWeekShow3);
//        show(VisitWeekShow4);
//   }
//}

////shows div
//function show1(divId) 
//{
//    document.layers[divId].visibility = 'show';
//    document.getElementById(divId).style.display = 'inline';
//}

//function To_Change_Color(lblId)
//{
//    if (document.all) 
//    {
//       document.all[lblId].style.color = 'Red';
//    }
//    else if (document.getElementById) 
//    {
//      document.getElementById(lblId).style.color = 'Red';
//    }
//}

//////////////////////ClinicalHeaderFooter and clinicalmasterheaderfooter////////////
////Global Variable
// var dt1 = "";
// var dt2 = "";
// 
////To trim right 0's from EnrolmentID
////function unique_Enrolment(Enrol_ID)
////{
////var Enrol_no = document.getElementById(Enrol_ID).value;
////Enrol_no = (Enrol_no/10)*10;
////document.getElementById(Enrol_ID).value = Enrol_no;
////}

////Function to find Date Difference
////function isDate(p_Expression){
////	return !isNaN(new Date(p_Expression));		// <<--- this needs checking
////}

////function dateDiff(p_Interval, p_Date1, p_Date2, p_firstdayofweek, p_firstweekofyear){
////	if(!isDate(p_Date1)){return "invalid date: '" + p_Date1 + "'";}
////	if(!isDate(p_Date2)){return "invalid date: '" + p_Date2 + "'";}
////	var dt1 = new Date(p_Date1);
////	var dt2 = new Date(p_Date2);

////	// get ms between dates (UTC) and make into "difference" date
////	var iDiffMS = dt2.valueOf() - dt1.valueOf();
////	var dtDiff = new Date(iDiffMS);

////	// calc various diffs
////	var nYears  = dt2.getUTCFullYear() - dt1.getUTCFullYear();
////	var nMonths = dt2.getUTCMonth() - dt1.getUTCMonth() + (nYears!=0 ? nYears*12 : 0);
////	var nQuarters = parseInt(nMonths/3);	//<<-- different than VBScript, which watches rollover not completion
////	
////	var nMilliseconds = iDiffMS;
////	var nSeconds = parseInt(iDiffMS/1000);
////	var nMinutes = parseInt(nSeconds/60);
////	var nHours = parseInt(nMinutes/60);
////	var nDays  = parseInt(nHours/24);
////	var nWeeks = parseInt(nDays/7);

////	// return requested difference
////	var iDiff = 0;		
////	switch(p_Interval.toLowerCase()){
////		case "yyyy": return nYears;
////		case "q": return nQuarters;
////		case "m": return nMonths;
////		case "y": 		// day of year
////		case "d": return nDays;
////		case "w": return nDays;
////		case "ww":return nWeeks;		// week of year	// <-- inaccurate, WW should count calendar weeks (# of sundays) 
////between
////		case "h": return nHours;
////		case "n": return nMinutes;
////		case "s": return nSeconds;
////		case "ms":return nMilliseconds;	// millisecond	// <-- extension for JS, NOT available in VBScript
////		default: return "invalid interval: '" + p_Interval + "'";
////	}
////}
////Function to calculate date
////function CalculateDate(Date1, Date2)
////{
////        var yr1 = Date1.substr(7,4);
////        var yr2 = Date2.substr(7,4);
////        var mm1 = Date1.substr(3,3);
////        var mm2 = Date2.substr(3,3);
////        var dd1 = Date1.substr(0,2);
////        var dd2 = Date2.substr(0,2);

////    var nmm1;
////	switch(mm1.toLowerCase()){
////		case "jan": nmm1= "01";
////		break;
////		case "feb": nmm1= "02";
////		break;
////		case "mar": nmm1= "03";
////		break;
////		case "apr": nmm1= "04";		
////		break;
////		case "may": nmm1= "05";
////		break;
////		case "jun": nmm1= "06";
////		break;
////		case "jul": nmm1= "07";	
////		break;
////		case "aug": nmm1= "08";
////		break;
////		case "sep": nmm1= "09";
////		break;
////		case "oct": nmm1= "10";
////		break;
////		case "nov": nmm1= "11";	
////		break;
////		case "dec": nmm1= "12";	
////		break;
////	}
////	var nmm2;
////	switch(mm2.toLowerCase()){
////		case "jan": nmm2= "01";
////		break;
////		case "feb": nmm2= "02";
////		break;
////		case "mar": nmm2= "03";
////		break;
////		case "apr": nmm2= "04";		
////		break;
////		case "may": nmm2= "05";
////		break;
////		case "jun": nmm2= "06";
////		break;
////		case "jul": nmm2= "07";	
////		break;
////		case "aug": nmm2= "08";
////		break;
////		case "sep": nmm2= "09";
////		break;
////		case "oct": nmm2= "10";
////		break;
////		case "nov": nmm2= "11";	
////		break;
////		case "dec": nmm2= "12";	
////		break;
////	}
////     dt1 = nmm1 + "/" + dd1 + "/" + yr1;
////     dt2 = nmm2 + "/" + dd2 + "/" + yr2;
////     return;
////}

////Function to Calculate Age for Height in ART Followup Form
////function CalculateAgeHeight(Height,txtheight, txtDT1,txtDT2)
////{
////   var txtdT = document.getElementById(txtDT2).value;
////   CalculateDate(txtDT1, txtdT);
////   var val1 = dateDiff("d",dt1,dt2,"","")/365;
////   if (val1 > 18)
////   {if (Height != 0)
////   {document.getElementById(txtheight).value = Height}
////   else {document.getElementById(txtheight).value = "";}
////   }
////   return false;
////}

////Function to Calculate Age for Enrolment Form
////function CalcualteAge(txtAge,txtmonth,txtDT1,txtDT2)
////{
////    var txtDT1 = document.getElementById(txtDT1).value;
////    var txtDT2 = document.getElementById(txtDT2).value;
////    if (txtDT1 == "" || txtDT2 == "")
////	{
////	 document.getElementById(txtmonth).value ="";
////	 document.getElementById(txtAge).value="";
////	 return true;
////	}
////    CalculateDate(txtDT1, txtDT2);
////    var val1 = dateDiff("d",dt1,dt2,"","")/365;
////    var val2 = Math.round((dateDiff("d",dt1,dt2,"","")/365));
////    if (val2 > val1 )
////    {
////    document.getElementById(txtAge).value = Math.round((dateDiff("d",dt1,dt2,"","")/365))-1;  
////    var yr= Math.round(dateDiff("d",dt1,dt2,"","")/365)-1;
////    document.getElementById(txtmonth).value = Math.round((dateDiff("d",dt1,dt2,"","") -  (365*yr))/30);  
////    }
////    else
////    {
////     document.getElementById(txtAge).value = Math.round((dateDiff("d",dt1,dt2,"","")/365));  
////     var yr= Math.round(dateDiff("d",dt1,dt2,"","")/365);
////     document.getElementById(txtmonth).value = Math.round((dateDiff("d",dt1,dt2,"","") -  (365*yr))/30);  
////    }
////}


////ARV Message
//function getMessage(txt1, txt2, txt3, txt4, txt5, txt6, txt7, txt8, txt9, txt10, txt11, txt12, chk1,chk2, rdo1) 
//{ 
//var objtxt1 = document.getElementById(txt1); var objtxt2 = document.getElementById(txt2); var objtxt3 = document.getElementById(txt3);
//var objtxt4 = document.getElementById(txt4); var objtxt5 = document.getElementById(txt5); var objtxt6 = document.getElementById(txt6);
//var objtxt7 = document.getElementById(txt7); var objtxt8 = document.getElementById(txt8); var objtxt9 = document.getElementById(txt9);
//var objtxt10 = document.getElementById(txt10); var objtxt11 = document.getElementById(txt11); var objtxt12 = document.getElementById(txt12);
//objchk1 = document.getElementById(chk1); objchk1.type == "checkbox"
//objchk2 = document.getElementById(chk2); objchk2.type == "checkbox"
//objrdo1 = document.getElementById(rdo1); objrdo1.type == "radio"
// if (objtxt1.value != "" || objtxt2.value != "" || objtxt3.value != "" || objtxt4.value != "" || objtxt5.value != "" || objtxt6.value != "" || 
//objtxt7.value != "" || objtxt8.value != "" || objtxt9.value != "" || objtxt10.value != "" || objtxt11.value != "" || objtxt12.value != "" || 
//objchk1.checked==true || objchk2.checked==true)
// {
// var ans; 
// ans=window.confirm('All fields in previous exposure will be cleared.....?'); 
//    if (ans==true) 
//    { 
//     objtxt1.value = ""; objtxt2.value = ""; objtxt3.value = ""; objtxt4.value = ""; objtxt5.value = "";
//     objtxt6.value = ""; objtxt7.value = ""; objtxt8.value = ""; objtxt9.value = ""; objtxt10.value = "";
//     objtxt11.value = ""; objtxt12.value = ""; objchk1.checked=false; objchk2.checked=false;
//     hide('prevexpdiv');
//     return;
//    }
//    else 
//    {  
//    objrdo1.checked=true;
//    }
//  } 
//  if ((objtxt1.value == "") && (objtxt2.value == "") && (objtxt3.value == "") && (objtxt4.value == "") && (objtxt5.value == "") && 
//(objtxt6.value == "") && (objtxt7.value == "") && (objtxt8.value == "") && (objtxt9.value == "") && (objtxt10.value == "") && (objtxt11.value 
//== "") && (objtxt12.value == "") && (objchk1.checked==false) && (objchk2.checked==false))
//  {
//    hide('prevexpdiv');
//    return;
//  }
//} 


////function AddBoundary(Control,MinRange,MaxRange)
////{
////   var objtxt = document.getElementById(Control);
////     
////   var len = objtxt.value.toString().length;
////   if ( len > 0)
////   {
////  
////   if(Number(objtxt.value) < Number(MinRange) && (MinRange.toString().length) < len)
////   {
////         objtxt.value = objtxt.value.toString().substr(0,objtxt.value.length - 1);
////         objtxt.focus();
////   }
////       
////   if(Number(objtxt.value) > Number(MaxRange))
////      {
////         objtxt.value = objtxt.value.toString().substr(0,objtxt.value.length - 1);
////         objtxt.focus();
////      }
////   }
////}

////function CheckValue(Control, MinRange)
////{
////   var objtxt = document.getElementById(Control);
////   var len = objtxt.value.toString().length;
////   if (len>0)
////   {
////       if(Number(objtxt.value) < Number(MinRange))
////       {
////           objtxt.value = "";
////           objtxt.focus();
////       }
////   }
//}

////function validate(InitEval)
////{
////if (document.InitEval.physKarnofskyScore.value.length > 3 || document.InitEval.physKarnofskyScore.value > 100  || 
////document.InitEval.physKarnofskyScore .value < 1 )
////	{
////		alert("Please enter a score between 1 and 100.");
////		document.InitEval.physKarnofskyScore.focus();
////		return false;
////	}
////	else
////	{
////	return true;
////    }
////}

//// Function for checking value lies in specified range
//// obj-->Object
//// Disp--> Display name for error alert
//// lo---> Lower value
//// hi---> Higher Value

////function isBetween (obj, Disp, lo, hi) { 

////var objtxt = document.getElementById(obj);
////val = document.getElementById(obj).value
////val = parseInt(val);
////lo = parseInt(lo);
////hi = parseInt(hi);
////if ((val < lo) || (val > hi)) 
////{
////	
////	alert(Disp+" should be in the range of " +lo+ " and " + hi)
////	objtxt.value = "";
////	objtxt.focus();
////	objtxt.select();
////	return(false);
////} 
////else 
////	{
////		return(true);
////	} 
////    
////} 

////Function to check valid date - Date Format or Future Date
//function isCheckValidDate(sysdate, frmdate, obj)
//{
//    var objtxt = document.getElementById(obj);
//    //form date
//    var frmdatetxt = document.getElementById(frmdate).value;
//    if (frmdatetxt == "")
//	{
//	 return true;
//	}
//    var frmdatetxt=(frmdd + "-"+ fmm + "-" + frmyr);
//    //System Date
//	var sysdatetxt;
//	var sysdd = sysdate.toString().substr(0,2);
//    var sysmm = sysdate.toString().substr(3,3);
//    var sysyr = sysdate.toString().substr(7,4);
//    var smm;
//	    switch(sysmm.toLowerCase()){
//		case "jan": smm = "01";
//		break;
//		case "feb": smm = "02";
//		break;
//		case "mar": smm = "03";
//		break;
//		case "apr": smm = "04";		
//		break;
//		case "may": smm = "05";
//		break;
//		case "jun": smm = "06";
//		break;
//		case "jul": smm = "07";	
//		break;
//		case "aug": smm = "08";
//		break;
//		case "sep": smm = "09";
//		break;
//		case "oct": smm = "10";
//		break;
//		case "nov": smm = "11";	
//		break;
//		case "dec": smm = "12";	
//		break;
//	}
//    var sysdatetxt=(sysyr+smm+sysdd);
//    //frmdate
//    var frmdatetxt = document.getElementById(frmdate).value;
//    var frmdd = document.getElementById(frmdate).value.toString().substr(0,2);
//    var frmmm = document.getElementById(frmdate).value.toString().substr(3,3);
//    var frmyr = document.getElementById(frmdate).value.toString().substr(7,4);
//    var fmm;
//	    switch(frmmm.toLowerCase()){
//		case "jan": fmm = "01";
//		break;
//		case "feb": fmm = "02";
//		break;
//		case "mar": fmm = "03";
//		break;
//		case "apr": fmm = "04";		
//		break;
//		case "may": fmm = "05";
//		break;
//		case "jun": fmm = "06";
//		break;
//		case "jul": fmm = "07";	
//		break;
//		case "aug": fmm = "08";
//		break;
//		case "sep": fmm = "09";
//		break;
//		case "oct": fmm = "10";
//		break;
//		case "nov": fmm = "11";	
//		break;
//		case "dec": fmm = "12";	
//		break;
//	}
//	
//	var frmdatetxt=(frmyr+fmm+frmdd);
//		
//  	if(frmdatetxt <= sysdatetxt)
//  	return true;
//	alert("Invalid date\nCheck DateFormat or\nCheck Future Date");
//	objtxt.value = "";
//	objtxt.focus();
//	objtxt.select();
//	return false;
//}

////Comparsion of Other Dates with Visit Date 
////function isCheckValidDateHIVrelated(frmvisitdate, frmotherdate, Disp, obj)
////{
////    
////    var objtxt = document.getElementById(obj);
////    //Other Date
////	var frmothertxt = document.getElementById(frmotherdate).value;
////	if (frmothertxt == "")
////	{
////	 return true;
////	}

////	//Visit Date
////	var frmvisitdatetxt = document.getElementById(frmvisitdate).value;
////	if (frmvisitdatetxt == "")
////	{
////	    alert("Please Enter Visit Date First");
////	    objtxt.value = "";
////	    objtxt.focus();
////	    objtxt.select();
////	    return false;
////	}
////    var frmvisitdatetxt = document.getElementById(frmvisitdate).value;
////    var frmvisitdd = document.getElementById(frmvisitdate).value.toString().substr(0,2);
////    var frmvisitmm = document.getElementById(frmvisitdate).value.toString().substr(3,3);
////    var frmvisityr = document.getElementById(frmvisitdate).value.toString().substr(7,4);
////    var hmm;
////	    switch(frmvisitmm.toLowerCase()){
////		case "jan": hmm = "01";
////		break;
////		case "feb": hmm = "02";
////		break;
////		case "mar": hmm = "03";
////		break;
////		case "apr": hmm = "04";		
////		break;
////		case "may": hmm = "05";
////		break;
////		case "jun": hmm = "06";
////		break;
////		case "jul": hmm = "07";	
////		break;
////		case "aug": hmm = "08";
////		break;
////		case "sep": hmm = "09";
////		break;
////		case "oct": hmm = "10";
////		break;
////		case "nov": hmm = "11";	
////		break;
////		case "dec": hmm = "12";	
////		break;
////	}
////	var frmvisitdatetxt=(frmvisityr+hmm+frmvisitdd);

////    //form Other dates
////    var frmothertxt = document.getElementById(frmotherdate).value;
////    var frmotherdd = document.getElementById(frmotherdate).value.toString().substr(0,2);
////    var frmothermm = document.getElementById(frmotherdate).value.toString().substr(3,3);
////    var frmotheryr = document.getElementById(frmotherdate).value.toString().substr(7,4);
////    var fmm;
////	    switch(frmothermm.toLowerCase()){
////		case "jan": fmm = "01";
////		break;
////		case "feb": fmm = "02";
////		break;
////		case "mar": fmm = "03";
////		break;
////		case "apr": fmm = "04";		
////		break;
////		case "may": fmm = "05";
////		break;
////		case "jun": fmm = "06";
////		break;
////		case "jul": fmm = "07";	
////		break;
////		case "aug": fmm = "08";
////		break;
////		case "sep": fmm = "09";
////		break;
////		case "oct": fmm = "10";
////		break;
////		case "nov": fmm = "11";	
////		break;
////		case "dec": fmm = "12";	
////		break;
////	}
////	var frmothertxt=(frmotheryr+fmm+frmotherdd);
////  	if(frmothertxt <= frmvisitdatetxt)
////  	return true;
////  	alert(Disp+" date should be before or equal to visit date");
////	objtxt.value = "";
////	objtxt.focus();
////	objtxt.select();
////	return false;
////}

////Comparsion of Other Dates(MMM-YYYYY) with Visit Date 
////function isCheckValidDate_MMM_YR(frmvisitdate, frmotherdate, Disp, obj)
////{
////    var objtxt = document.getElementById(obj);
////    //Other Date
////	var frmothertxt = document.getElementById(frmotherdate).value;
////	if (frmothertxt == "")
////	{
////	 return true;
////	}
////   
////	//Visit Date
////	var frmvisitdatetxt = document.getElementById(frmvisitdate).value;
////	
////	if (frmvisitdatetxt == "")
////	{
////	    alert("Please Enter Visit Date First");
////	    objtxt.value = "";
////	    objtxt.focus();
////	    objtxt.select();
////	    return false;
////	}
////    var frmvisitdatetxt = document.getElementById(frmvisitdate).value;
////    var frmvisitdd = document.getElementById(frmvisitdate).value.toString().substr(0,2);
////    var frmvisitmm = document.getElementById(frmvisitdate).value.toString().substr(3,3);
////    var frmvisityr = document.getElementById(frmvisitdate).value.toString().substr(7,4);
////    var hmm;
////	    switch(frmvisitmm.toLowerCase()){
////		case "jan": hmm = "01";
////		break;
////		case "feb": hmm = "02";
////		break;
////		case "mar": hmm = "03";
////		break;
////		case "apr": hmm = "04";		
////		break;
////		case "may": hmm = "05";
////		break;
////		case "jun": hmm = "06";
////		break;
////		case "jul": hmm = "07";	
////		break;
////		case "aug": hmm = "08";
////		break;
////		case "sep": hmm = "09";
////		break;
////		case "oct": hmm = "10";
////		break;
////		case "nov": hmm = "11";	
////		break;
////		case "dec": hmm = "12";	
////		break;
////	}
////	var frmvisitdatetxt=(frmvisityr+hmm);

////    //form Other dates
////    var frmothertxt = document.getElementById(frmotherdate).value;
////    var frmothermm = document.getElementById(frmotherdate).value.toString().substr(0,3);
////    var frmotheryr = document.getElementById(frmotherdate).value.toString().substr(4,4);
////        
////    var fmm;
////	    switch(frmothermm.toLowerCase()){
////		case "jan": fmm = "01";
////		break;
////		case "feb": fmm = "02";
////		break;
////		case "mar": fmm = "03";
////		break;
////		case "apr": fmm = "04";		
////		break;
////		case "may": fmm = "05";
////		break;
////		case "jun": fmm = "06";
////		break;
////		case "jul": fmm = "07";	
////		break;
////		case "aug": fmm = "08";
////		break;
////		case "sep": fmm = "09";
////		break;
////		case "oct": fmm = "10";
////		break;
////		case "nov": fmm = "11";	
////		break;
////		case "dec": fmm = "12";	
////		break;
////	}

////	var frmothertxt=(frmotheryr+fmm);
////  	if(frmothertxt <= frmvisitdatetxt)
////  	return true;
////  	alert(Disp+" date should be before or equal to the Month & Year of visit date");
////	objtxt.value = "";
////	objtxt.focus();
////	objtxt.select();
////	return false;
////}
//////////////////////////////////////////////

////Function to check/uncheck list
////function display_chkddl(textBoxID, Id)
////{
////    e = document.getElementById(textBoxID);
////    if(e.type == "textbox")
////    {
////        if (e.text = "")
////        {
////          d = document.getElementById(Id);
////          d.style.display = '';
////        }
////        else if (e.text != "")
////        {
////            d = document.getElementById(Id);
////            d.style.display = "none";
////        }
////    }
////}

/////////////////////////////////////////////////

////Function to check/uncheck list
////function display_chklist(ChkboxId, Id)
////{
////    e = document.getElementById(ChkboxId);
////    if(e.type == "checkbox")
////    {
////        if (e.checked==false)
////        {
////          d = document.getElementById(Id);
////          d.style.display = '';
////        }
////        else if (e.checked = true)
////        {
////            d = document.getElementById(Id);
////            d.style.display = "none";
////        }
////    }
////}

////function to uncheckListItems
////function disableListItems(checkBoxListId, numOfItems)
////{
////    var objChkID = document.getElementById(checkBoxListId).value;
////    objChkID = parseInt(objChkID);
////        
////   // Does the checkboxlist not exist?
////    if(objChkID == null)
////    {
////       return;
////    }
////    var objItem;
////    var i = 0;

////    // Loop through the checkboxes in the list.
////    for(i = 0; i < numOfItems; i++)
////    {
////         objItem = document.getElementById(checkBoxListId + '_' + i);
////        //alert(objItem);
////        if (objItem.checked == true)
////        {    
////            objItem.click();
////        }
//// 
////    }       
////}

////function to uncheck Checkboxlist items through radio button
////function disableChkboxList(Id, numOfItems)
////{
////    TotItems = numOfItems;
////    for (i=0; i<TotItems; i++)
////    {
////      ChkListID = document.getElementById(Id + '_' + i);
////      if(ChkListID.type == "checkbox")
////      {
////        ChkListID.click();
////      }
////    }
////}

////function to uncheck griditems
////function disableGridItems(GridId, numOfItems)
////{
////    TotItems = numOfItems+1;
////    for(i=2; i < TotItems; i++)
////    {
////      if (i<=9)
////      {
////      grdId = document.getElementById(GridId + '_' + 'ctl0' + i + '_' + 'ChkBox');
////      if(grdId.type == "checkbox")
////        {
////          grdId.checked=false;
////        }
////      }
////     else
////     {
////      grdId = document.getElementById(GridId + '_' + 'ctl' + i + '_' + 'ChkBox');
////      if(grdId.type == "checkbox")
////        {
////          grdId.checked=false;
////        }
////      }
////    }
////}


////function disableCheckbox(Id)
////{
////    ChkId = document.getElementById(Id);
////    if(ChkId.type == "checkbox")
////    {
////    ChkId.checked=false;
////    }
////}

////function disableRadioNone(Id )
////{
////    rdoId = document.getElementById(Id);
////    if(rdoId.type == "radio")
////    {
////    rdoId.checked=false;
////    }
////}

////function disableRadioNotDocumented(Id )
////{
////    rdoId = document.getElementById(Id);
////    if(rdoId.type == "radio")
////    {
////    rdoId.checked=false;
////    }
////}

//////For Field Level Integer Validation////////////
////function IsInteger(IntString)
//////  check for valid numeric strings	
////{ 
////   var IntValidChars = "0123456789";
////   var IntChar;
////   var blnResult = true;
////   if (IntString.length == 0) return false;
////   //test strString consists of valid characters listed above
////   for (i = 0; i < IntString.length && blnResult == true; i++)
////      {
////      IntChar = IntString.charAt(i);
////      //alert(strChar);
////      if (IntValidChars.indexOf(IntChar) == -1)
////         {
////         blnResult = false;
////         }
////      }
////   return blnResult;
////}

////function chkInteger(TextID)
////{
//// var objtxt = document.getElementById(TextID);
//// if (IsInteger(objtxt.value) == false) 
////  {
////  objtxt.value = objtxt.value.toString().substr(0,objtxt.value.length - 1);
////  objtxt.focus();
////  }
////}

////////////////   For Field Level Numeric Validation ////////////////////
////function IsNumeric(strString)
//////  check for valid numeric strings	
////{ 
////   var strValidChars = "0123456789-";
////   var strChar;
////   var blnResult = true;
////   if (strString.length == 0) return false;
////   //test strString consists of valid characters listed above
////   for (i = 0; i < strString.length && blnResult == true; i++)
////      {
////      strChar = strString.charAt(i);
////      //alert(strChar);
////      if (strValidChars.indexOf(strChar) == -1)
////         {
////         blnResult = false;
////         }
////      }
////   return blnResult;
////}

////function chkNumeric(TextID)
////{
//// var objtxt = document.getElementById(TextID);
//// if (IsNumeric(objtxt.value) == false) 
////  {
////  objtxt.value = objtxt.value.toString().substr(0,objtxt.value.length - 1);
////  objtxt.focus();
////  }
////}
////function IsDecimal(strString)
//////  check for valid numeric strings	
////{ 
////   var strValidChars = "0123456789.";
////   var strChar;
////   var blnResult = true;
////   if (strString.length == 0) return false;
////   //test strString consists of valid characters listed above
////   for (i = 0; i < strString.length && blnResult == true; i++)
////      {
////      strChar = strString.charAt(i);
////      //alert(strChar);
////      if (strValidChars.indexOf(strChar) == -1)
////         {
////         blnResult = false;
////         }
////      }
////   return blnResult;
////}

////function chkDecimal(TextID)
////{
//// var objtxt = document.getElementById(TextID);
//// if (IsDecimal(objtxt.value) == false) 
////  {
////  objtxt.value = objtxt.value.toString().substr(0,objtxt.value.length - 1);
////  objtxt.focus();
////  }
////}
/////////////////////////////////////////////////////////////

////////////////   For Field Level String Validation ////////////////////
////function IsString(strString)
//////  check for valid numeric strings	
////{ 
////   var strValidChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ-/";
////   var strChar;
////   var blnResult = true;
////   if (strString.length == 0) return false;
////   //test strString consists of valid characters listed above
////   for (i = 0; i < strString.length && blnResult == true; i++)
////      {
////      strChar = strString.charAt(i);
////      //alert(strChar);
////      if (strValidChars.indexOf(strChar) == -1)
////         {
////         blnResult = false;
////         }
////      }
////   return blnResult;
////}

////function chkString(TextID)
////{
//// var objtxt = document.getElementById(TextID);
//// if (IsString(objtxt.value) == false) 
////  {
////  objtxt.value = objtxt.value.toString().substr(0,objtxt.value.length - 1);
////  objtxt.focus();
////  }
////}
/////////////////////////////////////////////////////////////
////function SetCheckBoxes(FormName, FieldName, CheckValue)
////{
////    var objCheckBoxes;
////  	if(!document.forms[FormName])
////  	return;
////	
////	var objCheckBoxes = document.forms[FormName].elements[FieldName];
////									
////	if(!objCheckBoxes)
////		return;
////	var countCheckBoxes = objCheckBoxes.length;
////	if(!countCheckBoxes)
////	
////	if (objCheckBoxes.checked == true)
////	    {
////		objCheckBoxes.checked = CheckValue;
////		}
////	else
////		// set the check value for all check boxes
////		for(var i = 0; i < countCheckBoxes; i++)
////			objCheckBoxes[i].checked = CheckValue;
////}

////Generic function to toggle elements
////function toggle(divId)
////{
////var Id = divId;
////if (document.layers)
////  {
////    if(document.layers[Id].visibility == "hide")
////    { show(Id);
////      return;
////    }
////    else if (document.layers[Id].visibility == "show")
////    { hide(Id);
////      return;
////    }
////  }
////if (document.all)
////  { if(document.all[Id].style.display == "none")
////      { show(Id);
////        return;
////      }
////    else if (document.all[Id].style.display == "inline")
////      { hide(Id);
////        return;
////      }
////  }
////else if (document.getElementById)
////  {
////    if(document.getElementById(Id).style.display == "none")
////      { show(Id);
////        return;
////      }
////    else if (document.getElementById(Id).style.display == "inline")
////      { 
////       hide(Id);
////       return;
////      }
////  }
////}


//////hides div
////function hide(divId) {
////if (document.layers) document.layers[divId].visibility = 'hide';
////else if (document.all) document.all[divId].style.display = 'none';
////else if (document.getElementById) document.getElementById(divId).style.display = 'none';
////}

//////shows div
////function show(divId) {
////if (document.layers) document.layers[divId].visibility = 'show';
////else if (document.all) document.all[divId].style.display = 'inline';
////else if (document.getElementById) document.getElementById(divId).style.display = 'inline';

////}



////ARV Therapy Validations - for IE and Follow Up
////function Therapy(Id, value)
////{
////    var cdivId = 'otherarvTherapyChangeCode';
////    var sdivId = 'otherarvTherapyStopCode';
////   
////    if (Id == 'Other' && value == 1)
////    {
////   
////    hide(sdivId);
////    show(cdivId);
////    }
////    if(Id == 'Other' && value == 2)
////    {
////    
////    hide(cdivId);
////    show(sdivId);
////    }
////    if (Id != 'Other')
////    {
////    
////    hide(sdivId);
////    hide(cdivId);
////    }
////}

////Function for Death Reason
////function DeathReason(Id, value)
////{
////    var deathdivId = 'specifyDeathReason';
////    var dropreason = 'dropOther';
////   
////    if (Id != 'Unknown' && value == 1)
////    {
////       show(deathdivId);
////    }
////    
////    if(Id == 'Other' && value == 2)
////    {
////    show(dropreason);
////    }
////    
////    if (Id == 'Unknown' && value == 1)
////    {
////    hide(deathdivId);
////    }
////    
////    if (Id != 'Other' && value == 2)
////    {
////    hide(dropreason);
////    } 
////   
////}




////function specify(id)
////{
////var selectId = id;
////var sdivId = 'other' + selectId;
////sdivId = document.getElementById('Other' + id);

////if (document.getElementById(selectId)[document.getElementById(selectId).selectedIndex].value != Other)
////{
////hide (sdivId);
////}
////else 
////{
////show (sdivId);
////}
////}


////function specifyCounsellorName(CounsellorID, CName)
////{
////document.getElementById(CName).value = CounsellorID;
////}

////function specifyChangeStop(id)
////{
////var selectId = id;
////var sdivId = 'arvTherapyStop';
////var cdivId = 'arvTherapyChange';
////var asdivId = 'Adherance_counsellor_signature';
////var ChangeRegimen = 98;
////var Stoptreatment = 99;
////var AdheranceSign = 3;
////var PatientSign = 2;
////var NoSign = 1;
////if (document.getElementById(selectId)[document.getElementById(selectId).selectedIndex].value == ChangeRegimen)
////{
////show (cdivId);
////hide (sdivId);
////}
////else if(document.getElementById(selectId)[document.getElementById(selectId).selectedIndex].value == Stoptreatment)
////{
////show (sdivId);
////hide (cdivId);
////}
////else if(document.getElementById(selectId)[document.getElementById(selectId).selectedIndex].value == AdheranceSign)
////{
////show (asdivId);
//////hide (asdivId);
////}
////else if
////(document.getElementById(selectId)[document.getElementById(selectId).selectedIndex].value == PatientSign)
////{
//////show (asdivId);
//// hide (asdivId);
////}
////else if
////(document.getElementById(selectId)[document.getElementById(selectId).selectedIndex].value == NoSign)
////{
//////show (asdivId);
//// hide (asdivId);
////}
////else
////{ 
//////hide (otherId);
////hide (sdivId);
////hide (cdivId);
////}
////}


////function dispense(id)
////{
////var selectId = id;
////var sdivId = 'pharmReportedbyShow';

////if (document.getElementById(selectId).value != '')
////{
////show(sdivId);
////}
////}


////Function for hide and unhide weeks in HomeVisit. on schedular header footer
//    
////function GetWeeks(txtNumofWeeks)
////{
////   var VisitWeekDiv   = 'VisitPerWeekShow';
////   var VisitWeekShow1 = 'VisitPerWeekShow1';
////   var VisitWeekShow2 = 'VisitPerWeekShow2';
////   var VisitWeekShow3 = 'VisitPerWeekShow3';
////   var VisitWeekShow4 = 'VisitPerWeekShow4';
////   
////   var val =document.getElementById(txtNumofWeeks).value
////      
////   if(val == 1)
////   {
////        show(VisitWeekDiv);	
////        show(VisitWeekShow1);
////        hide(VisitWeekShow2);
////        hide(VisitWeekShow3);
////        hide(VisitWeekShow4);
////   }
////   else if(val == 2)
////   {
////        show(VisitWeekDiv);
////        show(VisitWeekShow1);
////        show(VisitWeekShow2);
////        hide(VisitWeekShow3);
////        hide(VisitWeekShow4);
////   }
////   else if(val == 3)
////   {
////        show(VisitWeekDiv);
////        show(VisitWeekShow1);
////        show(VisitWeekShow2);
////        show(VisitWeekShow3);
////        hide(VisitWeekShow4);
////   }
////   else if(val == 4)
////   {
////        show(VisitWeekDiv);
////        show(VisitWeekShow1);
////        show(VisitWeekShow2);
////        show(VisitWeekShow3);
////        show(VisitWeekShow4);
////   }
////}

////shows div
////function show1(divId) {

////    
////    document.layers[divId].visibility = 'show';
////    document.getElementById(divId).style.display = 'inline';

////}



//function GetWeeks1(txtNumofWeeks)
//{
//   var VisitWeekDiv   = 'VisitPerWeekShow';
//   var VisitWeekShow1 = 'VisitPerWeekShow1';
//   var VisitWeekShow2 = 'VisitPerWeekShow2';
//   var VisitWeekShow3 = 'VisitPerWeekShow3';
//   var VisitWeekShow4 = 'VisitPerWeekShow4';
//   
// 
//  
//   if(txtNumofWeeks == 1)
//   {
//        show(VisitWeekDiv);	
//        show(VisitWeekShow1);
//        hide(VisitWeekShow2);
//        hide(VisitWeekShow3);
//        hide(VisitWeekShow4);
//   }
//   else if(txtNumofWeeks == 2)
//   {
//        show1(VisitWeekDiv);	
//        show1(VisitWeekShow1);
//        //hide(VisitWeekShow3);
//        //hide(VisitWeekShow4);
//   }
//   else if(txtNumofWeeks == 3)
//   {
//        show(VisitWeekDiv);
//        show(VisitWeekShow1);
//        show(VisitWeekShow2);
//        show(VisitWeekShow3);
//        hide(VisitWeekShow4);
//   }
//   else if(txtNumofWeeks == 4)
//   {
//        show(VisitWeekDiv);
//        show(VisitWeekShow1);
//        show(VisitWeekShow2);
//        show(VisitWeekShow3);
//        show(VisitWeekShow4);
//   }
//}

////function To_Change_Color(lblId)
////{
////if (document.all) document.all[lblId].style.color = 'Red';
////else if (document.getElementById) document.getElementById(lblId).style.color = 'Red';
////}

/////////////////////adminheaderfooter//////////////////////////
//function checkval(SelectedVal) {
//  
//  if( SelectedVal == "ARV" )
//  {
//   show('abbrevation');
//  }
//  else
//  {
//  hide('abbrevation');
//  }
//}

//function specifyChange(id)
//{
//   // alert(id);
//    var drugTypeID = "37";
//    var arvdivId = 'arvShow';
//    var arvdivId1 = 'arvShow1';
//    var nonARVdivId1 = 'nonARVShow1';
//    if(id == drugTypeID)
//    {
//       
//       show (arvdivId);
//       show (arvdivId1);
//       hide (nonARVdivId1);
//    }

//    else
//    {
//       
//       show (nonARVdivId1);
//       hide (arvdivId);
//       hide (arvdivId1);
//    }
//}

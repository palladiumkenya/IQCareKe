// JScript File
//Global Variable
var dt1 = "";
var dt2 = "";

//ARV Message for Regimen in IE form
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

function AddBoundary(Control,MinRange,MaxRange)
{
   var objtxt = document.getElementById(Control);
   if (objtxt != null) {
       var len = objtxt.value.toString().length;
       if (len > 0) {

           if (Number(objtxt.value) < Number(MinRange) && (MinRange.toString().length) < len) {
               objtxt.value = objtxt.value.toString().substr(0, objtxt.value.length - 1);
               objtxt.focus();
           }

           if (Number(objtxt.value) > Number(MaxRange)) {
               objtxt.value = objtxt.value.toString().substr(0, objtxt.value.length - 1);
               objtxt.focus();
           }
       }
   }
}

function CheckValue(Control, MinRange)
{
    var objtxt = document.getElementById(Control);
    if (objtxt != null)
    {
   var len = objtxt.value.toString().length;
   if (len > 0) {
       //if length is 1 and only . is there then also return false
       //Ajay Kumar
       if (objtxt.value == ".")
       { objtxt.value = ""; }

       if (Number(objtxt.value) < Number(MinRange)) {
           objtxt.value = "";
           objtxt.focus();
       }
   }
   }
}
function CheckValue2(Control ,MaxRange )
{
    var objtxt = document.getElementById(Control);
    if (objtxt != null) {

        var len = objtxt.value.toString().length;
        if (len > 0) {
            //if length is 1 and only . is there then also return false
            //Ajay Kumar
            if (objtxt.value == ".")
            { objtxt.value = ""; }

            if (Number(objtxt.value) > Number(MaxRange)) {
                objtxt.value = "";
                objtxt.focus();
            }
        }
    }
}

function validate(InitEval)
{
if (document.InitEval.physKarnofskyScore.value.length > 3 || document.InitEval.physKarnofskyScore.value > 100  || document.InitEval.physKarnofskyScore .value < 1 )
	{
		alert("Please enter a score between 1 and 100.");
		document.InitEval.physKarnofskyScore.focus();
		return false;
	}
		
	else
	{
   	    return true;
    }
}

// Function for checking value lies in specified range
// obj-->Object
// Disp--> Display name for error alert
// lo---> Lower value
// hi---> Higher Value

function flagAbnormalValue(obj, Disp, lo, hi) {
    var objtxt = document.getElementById(obj);
    val = document.getElementById(obj).value
    val = parseInt(val);
    lo = parseInt(lo);
    hi = parseInt(hi);
    if ((val < lo) || (val > hi)) {

        alert("Normal range should be in the range of " + lo + " and " + hi);
        objtxt.style.color = "red";
        //objtxt.value = "";
        //objtxt.focus();
        //objtxt.select();
        return (true);
    }
    /*else {
        return (true);
    }*/
}


function isBetween (obj, Disp, lo, hi) 
{ 
    var objtxt = document.getElementById(obj);
    val = document.getElementById(obj).value
    val = parseInt(val);
    lo = parseInt(lo);
    hi = parseInt(hi);
    if ((val < lo) || (val > hi)) 
    {
	
	    alert(Disp+" should be in the range of " +lo+ " and " + hi)
	    objtxt.value = "";
	    objtxt.focus();
	    objtxt.select();
	    return(false);
    } 
    else 
	{
		return(true);
    }
}

function isCheckNormalonLoad(theID, theValue, MinVal, MaxVal, MinNormal, MaxNormal) {




}

function isCheckNormal(obj, Disp, lo, hi, MinNorVal, MaxNorVal) {
//    debugger;
//    alert(Disp)
    var objtxt2 = document.getElementById(obj);
    DecimalVal =objtxt2.value
//    alert(DecimalVal)
    var objtxt = document.getElementById(obj); 
    val = document.getElementById(obj).value
//    val = parseInt(val);
//    lo = parseInt(lo);
    //    hi = parseInt(hi);
    val = parseFloat(val);
    lo = parseFloat(lo);
    hi = parseFloat(hi);
//    alert("Hi")
    MinNorVal = parseInt(MinNorVal);
    MaxNorVal = parseInt(MaxNorVal);
//    alert(val)
//    alert(hi)
//    alert(MaxNorVal)
//    alert(MinNorVal)
//    alert(lo)
    objtxt.style.color = "black";
    if (Disp == "DECIMAL") {
       
//        alert(DecimalVal)
        if ((DecimalVal < lo) || (DecimalVal > hi)) {

            alert(Disp + " should be in the range of " + lo + " and " + hi)
            objtxt.value = "";
            objtxt.focus();
            objtxt.select();
            return (false);
        }
        else if ((DecimalVal <= hi) && (DecimalVal > MaxNorVal)) {
//            alert(Disp + "Max should be in Abnormal "); //the range of " + lo + " and " + hi)
            //        alert(Disp + " should be in the range of " + lo + " and " + hi)
            objtxt.style.color = "red";
            //objtxt.value = "";
            //objtxt.focus();
            //objtxt.select();
            return (true);
        }
        else if ((DecimalVal < MinNorVal) && (DecimalVal >= lo)) {
//            alert(Disp + "Min should be in Abnormal "); //the range of " + lo + " and " + hi)
            //        alert(Disp + " should be in the range of " + lo + " and " + hi)
            objtxt.style.color = "red";
            //objtxt.value = "";
            //objtxt.focus();
            //objtxt.select();
            return (true);
        }

    }

    else {
        if ((val < lo) || (val > hi)) {

            alert(Disp + " should be in the range of " + lo + " and " + hi)
            objtxt.value = "";
            objtxt.focus();
            objtxt.select();
            return (false);
        }
        else if ((val <= hi) && (val > MaxNorVal)) {
//            alert(Disp + "Max should be in Abnormal "); //the range of " + lo + " and " + hi)
            //        alert(Disp + " should be in the range of " + lo + " and " + hi)
            objtxt.style.color = "red";
            //objtxt.value = "";
            //objtxt.focus();
            //objtxt.select();
            return (true);
        }
        else if ((val < MinNorVal) && (val >= lo)) {
//            alert(Disp + "Min should be in Abnormal "); //the range of " + lo + " and " + hi)
            //        alert(Disp + " should be in the range of " + lo + " and " + hi)
            objtxt.style.color = "red";
            //objtxt.value = "";
            //objtxt.focus();
            //objtxt.select();
            return (true);
        }
    }
//    else {
//        return (true);
//    }
}
function checkMin(obj, Disp, lo) {
    var objtxt = document.getElementById(obj);
    val = document.getElementById(obj).value
    val = parseInt(val);
    lo = parseInt(lo);
    if (val < lo){

        alert(Disp + " should be greater than " + lo)
        objtxt.value = "";
        objtxt.focus();
        objtxt.select();
        return (false);
    }
    else {
        return (true);
    }
} 




function checkMax (obj, Disp, hi) 
{ 
    var objtxt = document.getElementById(obj);
    val = document.getElementById(obj).value
    val = parseInt(val);
    hi = parseInt(hi);
    if (val > hi) 
    {
	
	    alert(Disp+" should be less than " + hi)
	    objtxt.value = "";
	    objtxt.focus();
	    objtxt.select();
	    return(false);
    } 
    else 
	{
		return(true);
    }
} 

//HIV related History 
function isCheckValidDateHIVrelated(HIVdate, frmdate, obj)
{
    var objtxt = document.getElementById(obj);
    
    //HIV related Date
	var HIVdatetxt;
    var HIVdatetxt = document.getElementById(HIVdate).value;
    var HIVdd = document.getElementById(HIVdate).value.toString().substr(0,2);
    var HIVmm = document.getElementById(HIVdate).value.toString().substr(3,3);
    var HIVyr = document.getElementById(HIVdate).value.toString().substr(7,4);
    var hmm;
	switch(HIVmm.toLowerCase())
	{
		case "jan": hmm = "01";
		break;
		case "feb": hmm = "02";
		break;
		case "mar": hmm = "03";
		break;
		case "apr": hmm = "04";		
		break;
		case "may": hmm = "05";
		break;
		case "jun": hmm = "06";
		break;
		case "jul": hmm = "07";	
		break;
		case "aug": hmm = "08";
		break;
		case "sep": hmm = "09";
		break;
		case "oct": hmm = "10";
		break;
		case "nov": hmm = "11";	
		break;
		case "dec": hmm = "12";	
		break;
    }
    var HIVdatetxt=(HIVdd + "-"+ hmm + "-" + HIVyr);
    //frmdate
    var frmdatetxt = document.getElementById(frmdate).value;
    var frmdd = document.getElementById(frmdate).value.toString().substr(0,2);
    var frmmm = document.getElementById(frmdate).value.toString().substr(3,3);
    var frmyr = document.getElementById(frmdate).value.toString().substr(7,4);
    var fmm;
	switch(frmmm.toLowerCase())
	{
		case "jan": fmm = "01";
		break;
		case "feb": fmm = "02";
		break;
		case "mar": fmm = "03";
		break;
		case "apr": fmm = "04";		
		break;
		case "may": fmm = "05";
		break;
		case "jun": fmm = "06";
		break;
		case "jul": fmm = "07";	
		break;
		case "aug": fmm = "08";
		break;
		case "sep": fmm = "09";
		break;
		case "oct": fmm = "10";
		break;
		case "nov": fmm = "11";	
		break;
		case "dec": fmm = "12";	
		break;
	}
	
	var frmdatetxt=(frmdd + "-"+ fmm + "-" + frmyr);
  	if(frmdatetxt <= HIVdatetxt)
  	{
  	  	return true;
  	}
  	else
  	{
	    alert("Invalid date\nCheck DateFormat or\nCheck Visit Date");
	    objtxt.value = "";
	    objtxt.focus();
	    objtxt.select();
	    return false;
	}
}

//Function to check/uncheck list
function display_chklist(ChkboxId, Id)
{
    e = document.getElementById(ChkboxId);
    if(e.type == "checkbox")
    {
        if (e.checked==false)
        {
          d = document.getElementById(Id);
          d.style.display = '';
        }
        else if (e.checked = true)
        {
            d = document.getElementById(Id);
            d.style.display = "none";
        }
    }
}

//function to uncheckListItems
function disableListItems(checkBoxListId, numOfItems)
{
    var objChkID = document.getElementById(checkBoxListId).value;
    objChkID = parseInt(objChkID);
        
   // Does the checkboxlist not exist?
    if(objChkID == null)
    {
       return;
    }
    var objItem;
    var i = 0;

    // Loop through the checkboxes in the list.
    for(i = 0; i < numOfItems; i++)
    {
 
        objItem = document.getElementById(checkBoxListId + '_' + i);
        if (objItem.checked == true)
        {
           // objItem.checked = false;
            objItem.click();
        }
 
    }       
}


//Function to check/Uncheck Checkbox and Radio Buttion list - left (Spl. for OIAIDs illness)
function disableChkRdoListItems_left(chkrdoListId, numOfItems)
{
    var i = 0;
    // Loop through the checkboxes in the list.
    for(i=0; i<numOfItems; i++)
    {
        objItem = document.getElementById(chkrdoListId + i);
        if (objItem.checked == true)
        {
          objItem.checked = false;
        }
    }       
}

function disableChkRdoListItems_left_IE(chkrdoListId, numOfItems)
{
    var i = 0;
    // Loop through the checkboxes in the list.
    objItem = document.getElementById(chkrdoListId + i);
    if (objItem.checked == true)
    {
     objItem.checked = false;
    }
    for(i=3; i<numOfItems; i++)
    {
        objItem = document.getElementById(chkrdoListId + i);
        if (objItem.checked == true)
        {
        objItem.checked = false;
        }
    }       
}

//Function to check/Uncheck Checkbox and Radio Buttion list - right (Spl. for OIAIDs illness)
function disableChkRdoListItems_right(chkrdoListId, numOfItems)
{
    var i = 0;
    // Loop through the checkboxes in the list.
    for(i=0; i<numOfItems; i++)
    {
        objItem = document.getElementById(chkrdoListId + i);
        if (objItem.checked == true)
        {
          objItem.checked = false;
        }
    }       
}


//function to uncheck Checkboxlist items through radio button
function disableChkboxList(Id, numOfItems)
{
    TotItems = numOfItems;
    for (i=0; i<TotItems; i++)
    {
      ChkListID = document.getElementById(Id + '_' + i);
      
      if(ChkListID.type == "checkbox")
      {
       ChkListID.checked=false;
      }
    }
}

//function to uncheck griditems
function disableGridItems(GridId, numOfItems) {
    TotItems = parseInt(numOfItems) + parseInt(1);

    for (i = 2; i <= TotItems; i++)
    {
      if (i<=9)
      {
         grdId = document.getElementById(GridId + '_' + 'ctl0' + i + '_' + 'ChkBox');
      
         if(grdId.type == "checkbox")
         {
             grdId.checked=false;
         }
      }
      else
      {
         grdId = document.getElementById(GridId + '_' + 'ctl' + i + '_' + 'ChkBox');
         if(grdId.type == "checkbox")
         {
            grdId.checked=false;
         }
      }
    }
}

function disableCheckbox(Id)
{
    ChkId = document.getElementById(Id);
    if(ChkId.type == "checkbox")
    {
       ChkId.checked=false;
    }
}

function disableRadioNone(Id)
{
    rdoId = document.getElementById(Id);
    if(rdoId.type == "radio")
    {
       rdoId.checked=false;
    }
}

function disableRadioNotDocumented(Id)
{
    rdoId = document.getElementById(Id);
    if(rdoId.type == "radio")
    {
       rdoId.checked=false;
    }
}

function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode != 46 && charCode > 31
            && (charCode < 48 || charCode > 57))
        return false;

    return true;
}
////For Field Level Integer Validation////////////
function IsInteger(IntString)
//  check for valid numeric strings	
{ 
   var IntValidChars = "0123456789-";
   var IntChar;
   var blnResult = true;
   if (IntString.length == 0) return false;
   
   //test strString consists of valid characters listed above
   for (i = 0; i < IntString.length && blnResult == true; i++)
   {
         IntChar = IntString.charAt(i);
         if (IntValidChars.indexOf(IntChar) == -1)
         {
            blnResult = false;
         }
   }
   return blnResult;
}
function chkInteger(TextID)
{
    var objtxt = document.getElementById(TextID);
   if (objtxt != null) {
       if (IsInteger(objtxt.value) == false) {
           objtxt.value = objtxt.value.toString().substr(0, objtxt.value.length - 1);
           objtxt.focus();
       }
   }
}


function IsNaturalNumber(IntString)
//  check for valid numeric strings	
{ 
   var IntValidChars = "123456789";
   var IntChar;
   var blnResult = true;
   if (IntString.length == 0) return false;
   
   //test strString consists of valid characters listed above
   for (i = 0; i < IntString.length && blnResult == true; i++)
   {
         IntChar = IntString.charAt(i);
         if (IntValidChars.indexOf(IntChar) == -1)
         {
            blnResult = false;
         }
   }
   return blnResult;
}

function chkNaturalNumber(TextID)
{
   var objtxt = document.getElementById(TextID);
   if (IsNaturalNumber(objtxt.value) == false) 
   {
      objtxt.value = objtxt.value.toString().substr(0,objtxt.value.length - 1);
      objtxt.focus();
   }
}

function IsPositiveInteger(IntString) {

    
   var IntValidChars = "0123456789";
   var IntChar;
   var blnResult = true;
   if (IntString.length == 0) return false;
   
   //test strString consists of valid characters listed above
   for (i = 0; i < IntString.length && blnResult == true; i++)
   {
       IntChar = IntString.charAt(i);
       
         if (IntValidChars.indexOf(IntChar) == -1)
         {
            blnResult = false;
         }
   }
   return blnResult;
}

function chkPostiveInteger(TextID)
{
   var objtxt = document.getElementById(TextID);
   if (IsPositiveInteger(objtxt.value) == false) 
   {
      objtxt.value = objtxt.value.toString().substr(0,objtxt.value.length - 1);
      objtxt.focus();
   }
}



//////////////   For Field Level Numeric Validation ////////////////////
function IsNumeric(strString)
//  check for valid numeric strings	
{ 
   var strValidChars = "0123456789";
   var strChar;
   var blnResult = true;
   if (strString.length == 0) return false;
   //test strString consists of valid characters listed above
   for (i = 0; i < strString.length && blnResult == true; i++)
   {
      strChar = strString.charAt(i);
      if (strValidChars.indexOf(strChar) == -1)
      {
         blnResult = false;
      }
   }
   return blnResult;
}

function chkNumeric(TextID)
{
   var objtxt = document.getElementById(TextID);
   if (IsNumeric(objtxt.value) == false) 
   {
      objtxt.value = objtxt.value.toString().substr(0,objtxt.value.length - 1);
      objtxt.focus();
   }
}

function IsDecimal(strString)
//  check for valid numeric strings	
{ 
   var strValidChars = "0123456789.";
   var strChar;
   var blnResult = true;
   if (strString.length == 0) return false;
   
       
   //test strString consists of valid characters listed above
   for (i = 0; i < strString.length && blnResult == true; i++)
   {
      strChar = strString.charAt(i);
      //alert(strChar);
      if (strValidChars.indexOf(strChar) == -1)
      {
         blnResult = false;
      }
   }
   return blnResult;
}

function chkDecimal(TextID)
{
    var objtxt = document.getElementById(TextID);    
    if (objtxt != null) {
        if (IsDecimal(objtxt.value) == false) {
            objtxt.value = objtxt.value.toString().substr(0, objtxt.value.length - 1);
            objtxt.focus();
        }
    }
}


function IsNumber(strString)
//  check for valid numeric strings	
{ 
   var strValidChars = "0123456789";
   var strChar;
   var blnResult = true;
   if (strString.length == 0) return false;
   //test strString consists of valid characters listed above
   for (i = 0; i < strString.length && blnResult == true; i++)
   {
      strChar = strString.charAt(i);
      if (strValidChars.indexOf(strChar) == -1)
      {
         blnResult = false;
      }
   }
   return blnResult;
}

function chkNumber(TextID)
{
   var objtxt = document.getElementById(TextID);
   if (IsNumber(objtxt.value) == false) 
   {
      objtxt.value = objtxt.value.toString().substr(0,objtxt.value.length - 1);
      objtxt.focus();
   }
}




///////////////////////////////////////////////////////////

//////////////   For Field Level String Validation ////////////////////
function IsAlphaNumericString(strString)
//  check for valid numeric strings	
{
    var strValidChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ-/0123456789";
    var strChar;
    var blnResult = true;
    if (strString.length == 0) return false;
    //test strString consists of valid characters listed above
    for (i = 0; i < strString.length && blnResult == true; i++) {
        strChar = strString.charAt(i);
        if (strValidChars.indexOf(strChar) == -1) {
            blnResult = false;
        }
    }
    return blnResult;
}

function chkAlphaNumericString(TextID) {
    var objtxt = document.getElementById(TextID);
    if (IsAlphaNumericString(objtxt.value) == false) {
        objtxt.value = objtxt.value.toString().substr(0, objtxt.value.length - 1);
        objtxt.focus();
    }
}

function IsString(strString)
//  check for valid numeric strings	
{ 
   var strValidChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ-/0123456789";
   var strChar;
   var blnResult = true;
   if (strString.length == 0) return false;
   //test strString consists of valid characters listed above
   for (i = 0; i < strString.length && blnResult == true; i++)
   {
      strChar = strString.charAt(i);
      if (strValidChars.indexOf(strChar) == -1)
      {
         blnResult = false;
      }
   }
   return blnResult;
}

function chkString(TextID)
{
   var objtxt = document.getElementById(TextID);
   if (IsString(objtxt.value) == false) 
   {
     objtxt.value = objtxt.value.toString().substr(0,objtxt.value.length - 1);
     objtxt.focus();
   }
}

function IsNameString(strString)
//  check for valid numeric strings	
{ 
   var strValidChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ-/ ";
   var strChar;
   var blnResult = true;
   if (strString.length == 0) return false;
   //test strString consists of valid characters listed above
   for (i = 0; i < strString.length && blnResult == true; i++)
   {
      strChar = strString.charAt(i);
      if (strValidChars.indexOf(strChar) == -1)
      {
         blnResult = false;
      }
   }
   return blnResult;
}

function chkNameString(TextID)
{
   var objtxt = document.getElementById(TextID);
   if (IsNameString(objtxt.value) == false) 
   {
     objtxt.value = objtxt.value.toString().substr(0,objtxt.value.length - 1);
     objtxt.focus();
   }
}
  
   
///////////////////////////////////////////////////////////
///////////////Copied from scheduler//////////////////////
//////////////////////////////////////////////////////////
function SetCheckBoxes(FormName, FieldName, CheckValue)
{
    var objCheckBoxes;
  	if(!document.forms[FormName])
  	 	return;
	
	var objCheckBoxes = document.forms[FormName].elements[FieldName];
									
	if(!objCheckBoxes)
		return;
	var countCheckBoxes = objCheckBoxes.length;
	if(!countCheckBoxes)
	{
	    if (objCheckBoxes.checked == true)
	    {
	    	objCheckBoxes.checked = CheckValue;
		}
   	    else
   	    {
		// set the check value for all check boxes
		   for(var i = 0; i < countCheckBoxes; i++)
		   {
		      objCheckBoxes[i].checked = CheckValue;
		   }
		}  
	}
}
function ChkHideUnhide(divId, chkboxId) {
    var Id = divId;
    var chkId = document.getElementById(chkboxId);
    if (document.getElementById(chkboxId).checked == true) {
        if (document.layers) {
            if (document.layers[Id].visibility == "hide") {
                show(Id);
                return;
            }

        }
        if (document.all) {
            if (document.all[Id].style.display == "none") {
                show(Id);
                return;
            }

        }
        else if (document.getElementById) {
            if (document.getElementById(Id).style.display == "none") {
                show(Id);
                return;
            }

        }


    }
    else {
        if (document.layers) {
            if (document.layers[Id].visibility == "show") {
                hide(Id);
                return;
            }
        }
        if (document.all) {
            if (document.all[Id].style.display == "inline") {
                hide(Id);
                return;
            }
        }
        else if (document.getElementById) {
            if (document.getElementById(Id).style.display == "inline") {
                hide(Id);
                return;
            }
        }
    }
}

//Generic function to toggle elements
function toggle(divId)
{
    var Id = divId;
    if (document.layers)
    {
        if(document.layers[Id].visibility == "hide")
        {
           show(Id);
           return;
        }
        else if (document.layers[Id].visibility == "show")
        { 
           hide(Id);
           return;
        }
    }
    if (document.all)
    { 
        if(document.all[Id].style.display == "none")
        { 
           show(Id);
           return;
        }
        else if (document.all[Id].style.display == "inline")
        { 
           hide(Id);
           return;
        }
    }
    else if (document.getElementById)
    {
       if(document.getElementById(Id).style.display == "none")
       { 
          show(Id);
          return;
       }
       else if (document.getElementById(Id).style.display == "inline")
       { 
           hide(Id);
           return;
       }
    }
}


//hides div
function hide(divId) 
{
   if (document.layers) document.layers[divId].visibility = 'hide';
   //else if (document.all) document.all[divId].style.display = 'none';
   else if (document.getElementById) document.getElementById(divId).style.display = 'none';
}
//hides div and Unchecked & Clear Elements
function hideandclear(divId) 
{
   if (document.layers) document.layers[divId].visibility = 'hide';
   //else if (document.all) document.all[divId].style.display = 'none';
   else if (document.getElementById) document.getElementById(divId).style.display = 'none';
   var div = document.getElementById(divId).innerHTML;
   
   for (var i=0;i<document.div.childNodes.length;i++) 
   { 
   //alert(div.childNodes[i]);
   var type = document.div.childNodes[i].type;
        if (type=="checkbox" && div.childNodes[i].checked){
            //alert("Form element in position " + i + " is of type checkbox and is checked.");
            document.div.childNodes[i].checked=false;
        }
        else if (type=="radio" && div.childNodes[i].checked) {
            //alert("Form element in position " + i + " is of type checkbox and is not checked.");
            div.childNodes[i].checked=false;
        }
        else if (type=="input") {
            //alert("Form element in position " + i + " is of type checkbox and is not checked.");
            div.childNodes[i].value=='';}
        else {
        }
 
   }
}

//shows div
function show(divId) 
{
  //---- Rupesh 19-Nov-08 ------
//   if (document.layers) document.layers[divId].visibility = 'show';
//   else if (document.all)document.all[divId].style.display  ='inline';
//   else if (document.getElementById) document.getElementById(divId).style.display = 'inline';

    if (document.layers) { document.layers[divId].visibility = 'show'; }

    //else if (document.all) document.all[divId].style.display = 'inline';
    else if (document.getElementById(divId)) { document.getElementById(divId).style.display = 'inline'; }
      
}

//ARV Therapy Validations
function Therapy(Id, value)
{
    var cdivId = 'otherarvTherapyChangeCode';
    var sdivId = 'otherarvTherapyStopCode';
   
    if (Id == 'Other' && value == 1)
    {
       hide(sdivId);
       show(cdivId);
    }
    if(Id == 'Other' && value == 2)
    {
       hide(cdivId);
       show(sdivId);
    }
    if (Id != 'Other')
    {
       hide(sdivId);
       hide(cdivId);
    }
}
//ARV BlueCard Therapy Validations
function TherapyBlueCard(Id, value) {
    var cdivId = 'otherarvTherapyChangeCode';
    var sdivId = 'otherarvTherapyStopCode';

    if (Id.indexOf("Other") > -1 && value == 1) {
        hide(sdivId);
        show(cdivId);
    }
    if (Id.indexOf("Other") > -1 && value == 2) {
        hide(cdivId);
        show(sdivId);
    }

    if (Id.indexOf("Other") == -1) {
        hide(sdivId);
        hide(cdivId);
    }
}

//HIVcareArtTBstatus
function TBstatusBRule(Id) {
    var cdivId = 'divTBStatusTBRX';
    if (Id == 'TB Rx') {
        show(cdivId);
    }
    else {
        hide(cdivId);
    }


}


//HIVcareArt ARVDrugsPoorFair
function HIVcareArtOtherBRule(text, tobebecheck, divname) {
  
        var cdivId = divname;
//        if (text == tobebecheck) {
        if (text.toString().indexOf(tobebecheck) > -1) {
            show(cdivId);
        }
        else {
            hide(cdivId);
        }
   
}

//HIVcareArt ARVDrugsPoorFair
function HIVcareArtPostPartumBRule(text, chkname) {
    var objchk1 = document.getElementById(chkname);
    objchk1.type = "checkbox"
    if (text !='') {
        objchk1.checked = true;
    }
    else {
        objchk1.checked = false;
    }
}

//HIVcareArt HIVcareArtPMTCTBRule
function HIVcareArtPMTCTBRule(chkname,divName) {
 //   var objchk1 = document.getElementById(chkname);

    if (chkname.value=='Y') {
            show(divName)
        }
        else {
            hide(divName)
        }
   
}



function specify(id)
{
   var selectId = id;
   var sdivId = 'other' + selectId;
   sdivId = document.getElementById('Other' + id);

   if (document.getElementById(selectId)[document.getElementById(selectId).selectedIndex].value != Other)
   {
      hide (sdivId);
   }
   else 
   {
      show (sdivId);
   }
}


function specifyCounsellorName(CounsellorID, CName)
{
    document.getElementById(CName).value = CounsellorID;
}
//Change/Stop function for Therapy Plan
function specifyChangeStop(id)
{
   var selectId = id;
   var sdivId = 'arvTherapyStop';
   var cdivId = 'arvTherapyChange';
   var asdivId = 'Adherance_counsellor_signature';
   var ChangeRegimen = 98;
   var Stoptreatment = 99;
   var AdheranceSign = 3;
   var PatientSign = 2;
   var NoSign = 1;
   if (document.getElementById(selectId)[document.getElementById(selectId).selectedIndex].value == ChangeRegimen)
   {
      show (cdivId);
      hide (sdivId);
   }
   else if(document.getElementById(selectId)[document.getElementById(selectId).selectedIndex].value == Stoptreatment)
   {
      show (sdivId);
      hide (cdivId);
   }
   else if(document.getElementById(selectId)[document.getElementById(selectId).selectedIndex].value == AdheranceSign)
   {
      show (asdivId);
      //hide (asdivId);
   }
   else if(document.getElementById(selectId)[document.getElementById(selectId).selectedIndex].value == PatientSign)
   {
      //show (asdivId);
      hide (asdivId);
   }
   else if(document.getElementById(selectId)[document.getElementById(selectId).selectedIndex].value == NoSign)
   {
      //show (asdivId);
      hide (asdivId);
   }
   else
   { 
      //hide (otherId);
      hide (sdivId);
      hide (cdivId);
   }
}

function dispense(id)
{
   var selectId = id;
   var sdivId = 'pharmReportedbyShow';

   if (document.getElementById(selectId).value != '')
   {
      show(selectId);
   }
}

//To check forms mandatory validations
var form = "";
var submitted = false;
var error = false;
var error_message = "";

function check_input(field_name, message) 
{
   if (form.elements[field_name] && (form.elements[field_name].type != "hidden")) 
   {
      var field_value = form.elements[field_name].value;

      if (field_value == '') 
      {
         error_message = error_message + "* " + message + "\n";
         error = true;
       }
   }
}


function check_form(form_name) 
{
    if (submitted == true) 
    {
       alert("This form has already been submitted. Please press Ok and wait for this process to be completed.");
       return false;
    }
    error = false;
    form = form_name;
    error_message = "Errors have occured during the process of your form.\n\nPlease make the following corrections:\n\n";
    check_input("firstname", "First Name Cannot be blank");
  
    if (error == true) 
    {
       alert(error_message);
       return false;
    } 
    else 
    {
       submitted = true;
       return true;
    }
}
////////////////////////////////////////////////////////////////////
///Clinicalheaderfooter
//To trim right 0's from EnrolmentID
function unique_Enrolment(Enrol_ID)
{
   var Enrol_no = document.getElementById(Enrol_ID).value;
   Enrol_no = (Enrol_no/10)*10;
   if (Enrol_no == '0')
   {
   document.getElementById(Enrol_ID).value = "";
   }
   else{ document.getElementById(Enrol_ID).value = Enrol_no;}
}

//Function to find Date Difference
function isDate(p_Expression)
{
	return !isNaN(new Date(p_Expression));		// <<--- this needs checking
}

function dateDiff(p_Interval, p_Date1, p_Date2, p_firstdayofweek, p_firstweekofyear)
{
	if(!isDate(p_Date1)){return "invalid date: '" + p_Date1 + "'";}
	if(!isDate(p_Date2)){return "invalid date: '" + p_Date2 + "'";}
	var dt1 = new Date(p_Date1);
	var dt2 = new Date(p_Date2);

	// get ms between dates (UTC) and make into "difference" date
	var iDiffMS = dt2.valueOf() - dt1.valueOf();
	var dtDiff = new Date(iDiffMS);

	// calc various diffs
	var nYears  = dt2.getUTCFullYear() - dt1.getUTCFullYear();
	var nMonths = dt2.getUTCMonth() - dt1.getUTCMonth() + (nYears!=0 ? nYears*12 : 0);
	var nQuarters = parseInt(nMonths/3);	//<<-- different than VBScript, which watches rollover not completion
	
	var nMilliseconds = iDiffMS;
	var nSeconds = parseInt(iDiffMS/1000);
	var nMinutes = parseInt(nSeconds/60);
	var nHours = parseInt(nMinutes/60);
	var nDays  = parseInt(nHours/24);
	var nWeeks = parseInt(nDays/7);

	// return requested difference
	var iDiff = 0;		
	switch(p_Interval.toLowerCase())
	{
		case "yyyy": return nYears;
		case "q": return nQuarters;
		case "m": return nMonths;
		case "y": 		// day of year
		case "d": return nDays;
		case "w": return nDays;
		case "ww":return nWeeks;		// week of year	// <-- inaccurate, WW should count calendar weeks (# of sundays) between
		case "h": return nHours;
		case "n": return nMinutes;
		case "s": return nSeconds;
		case "ms":return nMilliseconds;	// millisecond	// <-- extension for JS, NOT available in VBScript
		default: return "invalid interval: '" + p_Interval + "'";
	}
}
//Function to calculate date
function CalculateDate(Date1, Date2)
{
        var yr1 = Date1.substr(7,4);
        var yr2 = Date2.substr(7,4);
        var mm1 = Date1.substr(3,3);
        var mm2 = Date2.substr(3,3);
        var dd1 = Date1.substr(0,2);
        var dd2 = Date2.substr(0,2);

    var nmm1;
	switch(mm1.toLowerCase())
	{
		case "jan": nmm1= "01";
		break;
		case "feb": nmm1= "02";
		break;
		case "mar": nmm1= "03";
		break;
		case "apr": nmm1= "04";		
		break;
		case "may": nmm1= "05";
		break;
		case "jun": nmm1= "06";
		break;
		case "jul": nmm1= "07";	
		break;
		case "aug": nmm1= "08";
		break;
		case "sep": nmm1= "09";
		break;
		case "oct": nmm1= "10";
		break;
		case "nov": nmm1= "11";	
		break;
		case "dec": nmm1= "12";	
		break;
	}
	var nmm2;
	switch(mm2.toLowerCase())
	{
		case "jan": nmm2= "01";
		break;
		case "feb": nmm2= "02";
		break;
		case "mar": nmm2= "03";
		break;
		case "apr": nmm2= "04";		
		break;
		case "may": nmm2= "05";
		break;
		case "jun": nmm2= "06";
		break;
		case "jul": nmm2= "07";	
		break;
		case "aug": nmm2= "08";
		break;
		case "sep": nmm2= "09";
		break;
		case "oct": nmm2= "10";
		break;
		case "nov": nmm2= "11";	
		break;
		case "dec": nmm2= "12";	
		break;
	 }
     dt1 = nmm1 + "/" + dd1 + "/" + yr1;
     dt2 = nmm2 + "/" + dd2 + "/" + yr2;
     return;
}

//Function to Calculate Age for Height in ART Followup Form
function CalculateAgeHeight(Height, txtheight, txtDT1, txtDT2)
{
   var txtDOB = document.getElementById(txtDT1).value;
   var txtvisitdate = document.getElementById(txtDT2).value;
   CalculateDate(txtDOB, txtvisitdate);
   var val1 = Math.round((dateDiff("d",dt1,dt2,"","")/365));
   if (val1 > 18)
   { 
      if (Height != 0)
      {
         document.getElementById(txtheight).value = Height
      }
      else 
      {
         document.getElementById(txtheight).value = "";
      }
   }
   else
   {
   document.getElementById(txtheight).value = "";
   }
  return false;
}

////Function to Calculate Age for Enrolment Form
function CalcualteAge(txtAge,txtmonth,txtDT1,txtDT2)
{
    var txtDT1 = document.getElementById(txtDT1).value;
    var txtDT2 = document.getElementById(txtDT2).value;
    if (txtDT1 == "" || txtDT2 == "")
	{
  	   document.getElementById(txtmonth).value ="";
	   document.getElementById(txtAge).value="";
	   return true;
	}
    CalculateDate(txtDT1, txtDT2);
    var val1 = dateDiff("d",dt1,dt2,"","")/365;
    var val2 = Math.round((dateDiff("d",dt1,dt2,"","")/365));
    if (val2 > val1 )
    {
       document.getElementById(txtAge).value = Math.round((dateDiff("d",dt1,dt2,"","")/365))-1;  
       var yr= Math.round(dateDiff("d",dt1,dt2,"","")/365)-1;
       document.getElementById(txtmonth).value = Math.round((dateDiff("d",dt1,dt2,"","") -  (365*yr))/30);  
    }
    else
    {
       document.getElementById(txtAge).value = Math.round((dateDiff("d",dt1,dt2,"","")/365));  
       var yr= Math.round(dateDiff("d",dt1,dt2,"","")/365);
       document.getElementById(txtmonth).value = Math.round((dateDiff("d",dt1,dt2,"","") -  (365*yr))/30);  
    }
}

//Function to check valid date - Date Format or Future Date
function isCheckValidDate(sysdate, frmdate, obj) // 2
{
    var objtxt = document.getElementById(obj);
    //form date
    var frmdatetxt = document.getElementById(frmdate).value;
    if (frmdatetxt == "")
	{
	    return true;
	}
    var frmdatetxt=(frmdd + "-"+ fmm + "-" + frmyr);
    //System Date
	var sysdatetxt;
	var sysdd = sysdate.toString().substr(0,2);
    var sysmm = sysdate.toString().substr(3,3);
    var sysyr = sysdate.toString().substr(7,4);
    var smm;
	    switch(sysmm.toLowerCase())
	    {
		    case "jan": smm = "01";
		    break;
		    case "feb": smm = "02";
		    break;
		    case "mar": smm = "03";
		    break;
		    case "apr": smm = "04";		
		    break;
		    case "may": smm = "05";
		    break;
		    case "jun": smm = "06";
		    break;
		    case "jul": smm = "07";	
		    break;
		    case "aug": smm = "08";
		    break;
		    case "sep": smm = "09";
		    break;
		    case "oct": smm = "10";
		    break;
		    case "nov": smm = "11";	
		    break;
		    case "dec": smm = "12";	
		    break;
     	}
    var sysdatetxt=(sysyr+smm+sysdd);
    //frmdate
    var frmdatetxt = document.getElementById(frmdate).value;
    var frmdd = document.getElementById(frmdate).value.toString().substr(0,2);
    var frmmm = document.getElementById(frmdate).value.toString().substr(3,3);
    var frmyr = document.getElementById(frmdate).value.toString().substr(7,4);
    var fmm;
	    switch(frmmm.toLowerCase())
	    {
		    case "jan": fmm = "01";
		    break;
		    case "feb": fmm = "02";
		    break;
		    case "mar": fmm = "03";
		    break;
		    case "apr": fmm = "04";		
		    break;
		    case "may": fmm = "05";
		    break;
		    case "jun": fmm = "06";
		    break;
		    case "jul": fmm = "07";	
		    break;
		    case "aug": fmm = "08";
		    break;
		    case "sep": fmm = "09";
		    break;
		    case "oct": fmm = "10";
		    break;
		    case "nov": fmm = "11";	
		    break;
		    case "dec": fmm = "12";	
		    break;
    	}
	
	var frmdatetxt=(frmyr+fmm+frmdd);
		
  	if(frmdatetxt <= sysdatetxt)
  	{
  	   return true;
  	}
  	else
  	{
	   alert("Invalid date\nCheck DateFormat or\nCheck Future Date");
	   objtxt.value = "";
	   objtxt.focus();
	   objtxt.select();
	   return false;
	}   
}

function isCheckValidDateForCustom(sysdate, frmdate, obj) // 2
{
    var objtxt = document.getElementById("ctl00_IQCareContentPlaceHolder_" +obj);
    //form date
    var frmdatetxt = document.getElementById("ctl00_IQCareContentPlaceHolder_" + frmdate).value;
    if (frmdatetxt == "") {
        return true;
    }
    var frmdatetxt = (frmdd + "-" + fmm + "-" + frmyr);
    //System Date
    var sysdatetxt;
    var sysdd = sysdate.toString().substr(0, 2);
    var sysmm = sysdate.toString().substr(3, 3);
    var sysyr = sysdate.toString().substr(7, 4);
    var smm;
    switch (sysmm.toLowerCase()) {
        case "jan": smm = "01";
            break;
        case "feb": smm = "02";
            break;
        case "mar": smm = "03";
            break;
        case "apr": smm = "04";
            break;
        case "may": smm = "05";
            break;
        case "jun": smm = "06";
            break;
        case "jul": smm = "07";
            break;
        case "aug": smm = "08";
            break;
        case "sep": smm = "09";
            break;
        case "oct": smm = "10";
            break;
        case "nov": smm = "11";
            break;
        case "dec": smm = "12";
            break;
    }
    var sysdatetxt = (sysyr + smm + sysdd);
    //frmdate
    var frmdatetxt = document.getElementById("ctl00_IQCareContentPlaceHolder_" + frmdate).value;
    var frmdd = document.getElementById("ctl00_IQCareContentPlaceHolder_" + frmdate).value.toString().substr(0, 2);
    var frmmm = document.getElementById("ctl00_IQCareContentPlaceHolder_" + frmdate).value.toString().substr(3, 3);
    var frmyr = document.getElementById("ctl00_IQCareContentPlaceHolder_" + frmdate).value.toString().substr(7, 4);
    var fmm;
    switch (frmmm.toLowerCase()) {
        case "jan": fmm = "01";
            break;
        case "feb": fmm = "02";
            break;
        case "mar": fmm = "03";
            break;
        case "apr": fmm = "04";
            break;
        case "may": fmm = "05";
            break;
        case "jun": fmm = "06";
            break;
        case "jul": fmm = "07";
            break;
        case "aug": fmm = "08";
            break;
        case "sep": fmm = "09";
            break;
        case "oct": fmm = "10";
            break;
        case "nov": fmm = "11";
            break;
        case "dec": fmm = "12";
            break;
    }

    var frmdatetxt = (frmyr + fmm + frmdd);

    if (frmdatetxt <= sysdatetxt) {
        return true;
    }
    else {
        alert("Invalid date\nCheck DateFormat or\nCheck Future Date");
        objtxt.value = "";
        objtxt.focus();
        objtxt.select();
        return false;
    }
}
 

//Comparsion of Other Dates with Visit Date 
function isCheckValidDateHIVrelated(frmvisitdate, frmotherdate, Disp, obj)
{
    
    var objtxt = document.getElementById(obj);
    //Other Date
	var frmothertxt = document.getElementById(frmotherdate).value;
	if (frmothertxt == "")
	{
	   return true;
	}

	//Visit Date
	var frmvisitdatetxt = document.getElementById(frmvisitdate).value;
	if (frmvisitdatetxt == "")
	{
	    alert("Please Enter Visit Date First");
	    objtxt.value = "";
	    objtxt.focus();
	    objtxt.select();
	    return false;
	}
    var frmvisitdatetxt = document.getElementById(frmvisitdate).value;
    var frmvisitdd = document.getElementById(frmvisitdate).value.toString().substr(0,2);
    var frmvisitmm = document.getElementById(frmvisitdate).value.toString().substr(3,3);
    var frmvisityr = document.getElementById(frmvisitdate).value.toString().substr(7,4);
    var hmm;
	    switch(frmvisitmm.toLowerCase())
	    {
		    case "jan": hmm = "01";
		    break;
		    case "feb": hmm = "02";
		    break;
		    case "mar": hmm = "03";
		    break;
		    case "apr": hmm = "04";		
		    break;
		    case "may": hmm = "05";
		    break;
		    case "jun": hmm = "06";
		    break;
		    case "jul": hmm = "07";	
		    break;
		    case "aug": hmm = "08";
		    break;
		    case "sep": hmm = "09";
		    break;
		    case "oct": hmm = "10";
		    break;
		    case "nov": hmm = "11";	
		    break;
		    case "dec": hmm = "12";	
		    break;
   	    }
	var frmvisitdatetxt=(frmvisityr+hmm+frmvisitdd);

    //form Other dates
    var frmothertxt = document.getElementById(frmotherdate).value;
    var frmotherdd = document.getElementById(frmotherdate).value.toString().substr(0,2);
    var frmothermm = document.getElementById(frmotherdate).value.toString().substr(3,3);
    var frmotheryr = document.getElementById(frmotherdate).value.toString().substr(7,4);
    var fmm;
	    switch(frmothermm.toLowerCase())
	    {
		    case "jan": fmm = "01";
		    break;
		    case "feb": fmm = "02";
		    break;
		    case "mar": fmm = "03";
		    break;
		    case "apr": fmm = "04";		
		    break;
		    case "may": fmm = "05";
		    break;
		    case "jun": fmm = "06";
		    break;
		    case "jul": fmm = "07";	
		    break;
		    case "aug": fmm = "08";
		    break;
		    case "sep": fmm = "09";
		    break;
		    case "oct": fmm = "10";
		    break;
		    case "nov": fmm = "11";	
		    break;
		    case "dec": fmm = "12";	
		    break;
    	}
	var frmothertxt=(frmotheryr+fmm+frmotherdd);
  	if(frmothertxt <= frmvisitdatetxt)
  	{
        return true;
  	}
  	else
  	{
  	    alert(Disp+" date should be before or equal to visit date");
	    objtxt.value = "";
	    objtxt.focus();
	    objtxt.select();
	    return false;
	}
}

//Comparsion of Other Dates(MMM-YYYYY) with Visit Date 
function isCheckValidDate_MMM_YR(frmvisitdate, frmotherdate, Disp, obj)
{
    var objtxt = document.getElementById(obj);
    //Other Date
	var frmothertxt = document.getElementById(frmotherdate).value;
	if (frmothertxt == "")
	{
	   return true;
	}
	//Visit Date
	var frmvisitdatetxt = document.getElementById(frmvisitdate).value;
	if (frmvisitdatetxt == "")
	{
	    alert("Please Enter Visit Date First");
	    objtxt.value = "";
	    objtxt.focus();
	    objtxt.select();
	    return false;
	}
    var frmvisitdatetxt = document.getElementById(frmvisitdate).value;
    var frmvisitdd = document.getElementById(frmvisitdate).value.toString().substr(0,2);
    var frmvisitmm = document.getElementById(frmvisitdate).value.toString().substr(3,3);
    var frmvisityr = document.getElementById(frmvisitdate).value.toString().substr(7,4);
    var hmm;
	    switch(frmvisitmm.toLowerCase())
	    {
		    case "jan": hmm = "01";
		    break;
		    case "feb": hmm = "02";
		    break;
		    case "mar": hmm = "03";
		    break;
		    case "apr": hmm = "04";		
		    break;
		    case "may": hmm = "05";
		    break;
		    case "jun": hmm = "06";
		    break;
		    case "jul": hmm = "07";	
		    break;
		    case "aug": hmm = "08";
		    break;
		    case "sep": hmm = "09";
		    break;
		    case "oct": hmm = "10";
		    break;
		    case "nov": hmm = "11";	
		    break;
		    case "dec": hmm = "12";	
		    break;
    	}
	var frmvisitdatetxt=(frmvisityr+hmm);

    //form Other dates
    var frmothertxt = document.getElementById(frmotherdate).value;
    var frmothermm = document.getElementById(frmotherdate).value.toString().substr(0,3);
    var frmotheryr = document.getElementById(frmotherdate).value.toString().substr(4,4);
    var fmm;
	    switch(frmothermm.toLowerCase())
	    {
		    case "jan": fmm = "01";
		    break;
		    case "feb": fmm = "02";
		    break;
		    case "mar": fmm = "03";
		    break;
		    case "apr": fmm = "04";		
		    break;
		    case "may": fmm = "05";
		    break;
		    case "jun": fmm = "06";
		    break;
		    case "jul": fmm = "07";	
		    break;
		    case "aug": fmm = "08";
		    break;
		    case "sep": fmm = "09";
		    break;
		    case "oct": fmm = "10";
		    break;
		    case "nov": fmm = "11";	
		    break;
		    case "dec": fmm = "12";	
		    break;
    	}

	var frmothertxt=(frmotheryr+fmm);
  	if(frmothertxt <= frmvisitdatetxt)
  	{
     	return true;
  	}
  	else
  	{
  	    alert(Disp+" date should be before or equal to the Month & Year of visit date");
	    objtxt.value = "";
	    objtxt.focus();
	    objtxt.select();
	    return false;
	}
}
////////////////////////////////////////////

//Function to check/uncheck list
function display_chkddl(textBoxID, Id)
{
    e = document.getElementById(textBoxID);
    if(e.type == "textbox")
    {
        if (e.text = "")
        {
          d = document.getElementById(Id);
          d.style.display = '';
        }
        else if (e.text != "")
        {
            d = document.getElementById(Id);
            d.style.display = "none";
        }
    }
}

//Function for Death Reason
function DeathReason(Id, value)
{
    var deathdivId = 'specifyDeathReason';
    var dropreason = 'dropOther';
   
    if (Id == 'Other' && value == 1)
    {
       show(deathdivId);
    }
    else
    {
       hide(deathdivId);
    }
    
    if(Id == 'Other' && value == 2)
    {
       show(dropreason);
    }
    else
    {
       hide(dropreason);
    }
    
//    if (Id == 'Unknown' && value == 1)
//    {
//       hide(deathdivId);
//    }
//    else
//    {
//       hide(deathdivId);
//    }
//    
//    if (Id != 'Other' && value == 2)
//    {
//       hide(dropreason);
//    } 
   
}
   
function GetWeeks(txtNumofWeeks)
{
   var VisitWeekDiv   = 'VisitPerWeekShow';
   var VisitWeekShow1 = 'VisitPerWeekShow1';
   var VisitWeekShow2 = 'VisitPerWeekShow2';
   var VisitWeekShow3 = 'VisitPerWeekShow3';
   var VisitWeekShow4 = 'VisitPerWeekShow4';
   
   var val =document.getElementById(txtNumofWeeks).value;
   if(val == 1)
   {
        
        show(VisitWeekDiv);	
        show(VisitWeekShow1);
        hide(VisitWeekShow2);
        hide(VisitWeekShow3);
        hide(VisitWeekShow4);
   }
   else if(val == 2)
   {
        show(VisitWeekDiv);
        show(VisitWeekShow1);
        show(VisitWeekShow2);
        hide(VisitWeekShow3);
        hide(VisitWeekShow4);
   }
   else if(val == 3)
   {
        show(VisitWeekDiv);
        show(VisitWeekShow1);
        show(VisitWeekShow2);
        show(VisitWeekShow3);
        hide(VisitWeekShow4);
   }
   else if(val == 4)
   {
        show(VisitWeekDiv);
        show(VisitWeekShow1);
        show(VisitWeekShow2);
        show(VisitWeekShow3);
        show(VisitWeekShow4);
   }
}

//shows div
function show1(divId) 
{
    document.layers[divId].visibility = 'show';
    document.getElementById(divId).style.display = 'inline';
}

function To_Change_Color(lblId)
{
    if (document.all) 
    {
       document.all[lblId].style.color = 'Red';
    }
    else if (document.getElementById) 
    {
      document.getElementById(lblId).style.color = 'Red';
    }
}

//ARV Message
function getMessage(txt1, txt2, txt3, txt4, txt5, txt6, txt7, txt8, txt9, txt10, txt11, txt12, chk1,chk2, rdo1) 
{ 
    var objtxt1 = document.getElementById(txt1); var objtxt2 = document.getElementById(txt2); var objtxt3 = document.getElementById(txt3);
    var objtxt4 = document.getElementById(txt4); var objtxt5 = document.getElementById(txt5); var objtxt6 = document.getElementById(txt6);
    var objtxt7 = document.getElementById(txt7); var objtxt8 = document.getElementById(txt8); var objtxt9 = document.getElementById(txt9);
    var objtxt10 = document.getElementById(txt10); var objtxt11 = document.getElementById(txt11); var objtxt12 = document.getElementById(txt12);
    objchk1 = document.getElementById(chk1); objchk1.type == "checkbox"
    objchk2 = document.getElementById(chk2); objchk2.type == "checkbox"
    objrdo1 = document.getElementById(rdo1); objrdo1.type == "radio"
    if (objtxt1.value != "" || objtxt2.value != "" || objtxt3.value != "" || objtxt4.value != "" || objtxt5.value != "" || objtxt6.value != "" || objtxt7.value != "" || objtxt8.value != "" || objtxt9.value != "" || objtxt10.value != "" || objtxt11.value != "" || objtxt12.value != "" || objchk1.checked==true || objchk2.checked==true)
    {
       var ans; 
       ans=window.confirm('All fields in previous exposure will be cleared.....?'); 
       if (ans==true) 
       { 
         objtxt1.value = ""; objtxt2.value = ""; objtxt3.value = ""; objtxt4.value = ""; objtxt5.value = "";
         objtxt6.value = ""; objtxt7.value = ""; objtxt8.value = ""; objtxt9.value = ""; objtxt10.value = "";
         objtxt11.value = ""; objtxt12.value = ""; objchk1.checked=false; objchk2.checked=false;
         hide('prevexpdiv');
         return;
       }
       else 
       {  
         objrdo1.checked=true;
       }
    } 
    if ((objtxt1.value == "") && (objtxt2.value == "") && (objtxt3.value == "") && (objtxt4.value == "") && (objtxt5.value == "") && (objtxt6.value == "") && (objtxt7.value == "") && (objtxt8.value == "") && (objtxt9.value == "") && (objtxt10.value == "") && (objtxt11.value == "") && (objtxt12.value == "") && (objchk1.checked==false) && (objchk2.checked==false))
    {
      hide('prevexpdiv');
      return;
    }
} 

function GetWeeks1(txtNumofWeeks)
{
   var VisitWeekDiv   = 'VisitPerWeekShow';
   var VisitWeekShow1 = 'VisitPerWeekShow1';
   var VisitWeekShow2 = 'VisitPerWeekShow2';
   var VisitWeekShow3 = 'VisitPerWeekShow3';
   var VisitWeekShow4 = 'VisitPerWeekShow4';
  
   if(txtNumofWeeks == 1)
   {
        show(VisitWeekDiv);	
        show(VisitWeekShow1);
        hide(VisitWeekShow2);
        hide(VisitWeekShow3);
        hide(VisitWeekShow4);
   }
   else if(txtNumofWeeks == 2)
   {
        show1(VisitWeekDiv);	
        show1(VisitWeekShow1);
        //hide(VisitWeekShow3);
        //hide(VisitWeekShow4);
   }
   else if(txtNumofWeeks == 3)
   {
        show(VisitWeekDiv);
        show(VisitWeekShow1);
        show(VisitWeekShow2);
        show(VisitWeekShow3);
        hide(VisitWeekShow4);
   }
   else if(txtNumofWeeks == 4)
   {
        show(VisitWeekDiv);
        show(VisitWeekShow1);
        show(VisitWeekShow2);
        show(VisitWeekShow3);
        show(VisitWeekShow4);
   }
}

function checkval(SelectedVal) 
{
  
  if( SelectedVal == "ARV" )
  {
    show('abbrevation');
  }
  else
  {
    hide('abbrevation');
  }
}

function specifyChange(id)
{
    var drugTypeID = "37";
    var arvdivId = 'arvShow';
    var arvdivId1 = 'arvShow1';
    var nonARVdivId1 = 'nonARVShow1';
    if(id == drugTypeID)
    {
       
       show (arvdivId);
       show (arvdivId1);
       hide (nonARVdivId1);
    }
    else
    {
       
       show (nonARVdivId1);
       hide (arvdivId);
       hide (arvdivId1);
    }
}
// HIVCareARTEncounter TBRxSTOP
function isCheckValidDate_MMM_YR_TBRxSTOP(frmvisitdate, frmotherdate, Disp, obj) {
    var objtxt = document.getElementById(obj);
    //Other Date
    var frmothertxt = document.getElementById(frmotherdate).value;
    if (frmothertxt == "") {
        return true;
    }
    //Visit Date
    var frmvisitdatetxt = document.getElementById(frmvisitdate).value;
    if (frmvisitdatetxt == "") {
        alert("Please Enter Visit TB Rx Start");
        objtxt.value = "";
        objtxt.focus();
        objtxt.select();
        return false;
    }
    var frmvisitdatetxt = document.getElementById(frmvisitdate).value;
   // var frmvisitdd = document.getElementById(frmvisitdate).value.toString().substr(0, 2);
    var frmvisitmm = document.getElementById(frmvisitdate).value.toString().substr(0, 3);
    var frmvisityr = document.getElementById(frmvisitdate).value.toString().substr(4, 4);
    var hmm;
    switch (frmvisitmm.toLowerCase()) {
        case "jan": hmm = "01";
            break;
        case "feb": hmm = "02";
            break;
        case "mar": hmm = "03";
            break;
        case "apr": hmm = "04";
            break;
        case "may": hmm = "05";
            break;
        case "jun": hmm = "06";
            break;
        case "jul": hmm = "07";
            break;
        case "aug": hmm = "08";
            break;
        case "sep": hmm = "09";
            break;
        case "oct": hmm = "10";
            break;
        case "nov": hmm = "11";
            break;
        case "dec": hmm = "12";
            break;
    }
    var frmvisitdatetxt = (frmvisityr + hmm);

    //form Other dates
    var frmothertxt = document.getElementById(frmotherdate).value;
    var frmothermm = document.getElementById(frmotherdate).value.toString().substr(0, 3);
    var frmotheryr = document.getElementById(frmotherdate).value.toString().substr(4, 4);
    var fmm;
    switch (frmothermm.toLowerCase()) {
        case "jan": fmm = "01";
            break;
        case "feb": fmm = "02";
            break;
        case "mar": fmm = "03";
            break;
        case "apr": fmm = "04";
            break;
        case "may": fmm = "05";
            break;
        case "jun": fmm = "06";
            break;
        case "jul": fmm = "07";
            break;
        case "aug": fmm = "08";
            break;
        case "sep": fmm = "09";
            break;
        case "oct": fmm = "10";
            break;
        case "nov": fmm = "11";
            break;
        case "dec": fmm = "12";
            break;
    }

    var frmothertxt = (frmotheryr + fmm);
    if (frmothertxt >= frmvisitdatetxt) {
        return true;
    }
    else {
        alert(Disp + " date should be greater or equal to the Month & Year of TB Rx Start");
        objtxt.value = "";
        objtxt.focus();
        objtxt.select();
        return false;
    }
}
//HIVCareARTEncounter TBRxStart
function isCheckValidDate_MMM_YR_TBRxStart(frmvisitdate, frmotherdate, Disp, obj) {
    var objtxt = document.getElementById(obj);
    //Other Date
    var frmothertxt = document.getElementById(frmotherdate).value;
    if (frmothertxt == "") {
        return true;
    }
//    //Visit Date
//    var frmvisitdatetxt = document.getElementById(frmvisitdate).value;
//    if (frmvisitdatetxt == "") {
//        alert("Please Enter Visit Date First");
//        objtxt.value = "";
//        objtxt.focus();
//        objtxt.select();
//        return false;
//    }
    //  var frmvisitdatetxt = document.getElementById(frmvisitdate).value;
    var frmvisitdatetxt = frmvisitdate;
    var frmvisitdd = frmvisitdatetxt.toString().substr(0, 2);
    var frmvisitmm = frmvisitdatetxt.toString().substr(3, 3);
    var frmvisityr = frmvisitdatetxt.toString().substr(7, 4);
    var hmm;
    switch (frmvisitmm.toLowerCase()) {
        case "jan": hmm = "01";
            break;
        case "feb": hmm = "02";
            break;
        case "mar": hmm = "03";
            break;
        case "apr": hmm = "04";
            break;
        case "may": hmm = "05";
            break;
        case "jun": hmm = "06";
            break;
        case "jul": hmm = "07";
            break;
        case "aug": hmm = "08";
            break;
        case "sep": hmm = "09";
            break;
        case "oct": hmm = "10";
            break;
        case "nov": hmm = "11";
            break;
        case "dec": hmm = "12";
            break;
    }
    var frmvisitdatetxt = (frmvisityr + hmm);

    //form Other dates
    var frmothertxt = document.getElementById(frmotherdate).value;
    var frmothermm = document.getElementById(frmotherdate).value.toString().substr(0, 3);
    var frmotheryr = document.getElementById(frmotherdate).value.toString().substr(4, 4);
    var fmm;
    switch (frmothermm.toLowerCase()) {
        case "jan": fmm = "01";
            break;
        case "feb": fmm = "02";
            break;
        case "mar": fmm = "03";
            break;
        case "apr": fmm = "04";
            break;
        case "may": fmm = "05";
            break;
        case "jun": fmm = "06";
            break;
        case "jul": fmm = "07";
            break;
        case "aug": fmm = "08";
            break;
        case "sep": fmm = "09";
            break;
        case "oct": fmm = "10";
            break;
        case "nov": fmm = "11";
            break;
        case "dec": fmm = "12";
            break;
    }

    var frmothertxt = (frmotheryr + fmm);
    if (frmothertxt >= frmvisitdatetxt) {
        return true;
    }
    else {
        alert(Disp + " date should be geater than or equal  to the Month & Year of DOB");
        objtxt.value = "";
        objtxt.focus();
        objtxt.select();
        return false;
    }
}




//Function for Care Tracking Form

function frmCareTrackingprogramARTended(rdoARTpEndedY, rdoARTpEndedN, rdoCareEndedY, rdoCareEndedN)
{
   objrdo1 = document.getElementById(rdoARTpEndedY); objrdo1.type == "radio"
   objrdo2 = document.getElementById(rdoARTpEndedN); objrdo2.type == "radio"
   objrdo3 = document.getElementById(rdoCareEndedY); objrdo3.type == "radio"
   objrdo4 = document.getElementById(rdoCareEndedN); objrdo4.type == "radio"
   show('tdsignature');
   // condition for Patient ART Ended but still on care true
   if (objrdo1.checked == true) 
   {
      objrdo3.disabled = true;
      objrdo4.checked = true;
      return;
   }

   if (objrdo3.checked == true)
   {
      objrdo1.disabled = true;
      objrdo2.disabled = true;
      objrdo4.disabled = false;
      objrdo2.checked = false;
      return;
   }

   if(objrdo2.checked == true) 
   {
      objrdo1.disabled = true;
      objrdo3.disabled = false;
      objrdo4.checked = false;
      
      return;
   }

   if (objrdo4.checked == true)
   {
      objrdo1.disabled = false;
      objrdo2.disabled = false;
      objrdo3.disabled = true;
      objrdo4.disabled = true;
      return;
   }

   
   // condition for Patient ART Ended but still on care false
   ////if (objrdo2.checked == true) 
   ////{
   ////   objrdo3.disabled = false;
   ////}
  
//   if (objrdo2.checked == true)
//   {
//      //document.getElementById(ProgramStatus).disabled = true;
//      document.getElementById(txtDate).disabled = true;
//      document.getElementById(Img).disabled = true;
//      document.getElementById(DD).disabled = true;
//   }
//   else if (objrdo3.checked == false)
//   {
//     document.getElementById(ProgramStatus).disabled = false;
//     document.getElementById(txtDate).disabled = false;
//     document.getElementById(Img).disabled = false;
//     document.getElementById(DD).disabled = false;
//   }

}
//////function frmCareTrackingARTendedN(rdoARTpEndedN, rdoCareEndedN, ProgramStatus, txtDate, Img, DD)
//////{
//////   objrdo1 = document.getElementById(rdoARTpEndedN); objrdo1.type == "radio"
//////   objrdo2 = document.getElementById(rdoCareEndedN); objrdo2.type == "radio"
//////   if (objrdo1.checked == true || objrdo1.checked == false)
//////   {
//////     document.getElementById(ProgramStatus).disabled = false;
//////     document.getElementById(txtDate).disabled = false;
//////     document.getElementById(Img).disabled = false;
//////     document.getElementById(DD).disabled = false;
//////   }
//////   if(objrdo1.checked == true && objrdo2.checked == true)
//////   {
//////     document.getElementById(txtDate).disabled = true;
//////     document.getElementById(Img).disabled = true;
//////     document.getElementById(DD).disabled = true;
//////   }
//////}

////////function frmCareTrackingprogramARTEnded(rdoARTpEndedN, rdoCareEndedN, txtDate, Img, DD)
////////{
////////    objrdo1 = document.getElementById(rdoARTpEndedN); objrdo1.type == "radio"
////////   // objrdo2 = document.getElementById(rdoCareEndedN); objrdo2.type == "radio"
////////    if(objrdo1.checked == true)
////////    {
////////     document.getElementById(txtDate).disabled = true;
////////     document.getElementById(Img).disabled = true;
////////     document.getElementById(DD).disabled = true;
////////    }
//////// }
 
 function cleartxtbox(ChkboxId, txtvalue)
    {
//        objchk = document.getElementById(ChkboxId); 
//        if(objchk.type == "checkbox")
//        {
//            if (objchk.checked==false)
//            {
//            //alert(txtvalue);
//            d = document.getElementById(txtvalue).value;
//            //alert(d);
//            //document.getElementById('').value = ""
//            }
//        }
        
   }
   //

   //HIVcareArt ARVDrugsPoorFair
   function ARVDrugsPoorFairBRule(Id,ddlclient) {
       var cdivId = 'divARVDrugsPoorFair';
       var otherdiv ='divReasonARVDrugsother'
       
       if ((Id == 'G=Good')||(Id == 'Select')) {
           hide(otherdiv);
           HIVcareArtDisableARVDrugsPoor(ddlclient,'1')
       }
       else {
           //var divothervalue = document.getElementById(otherdiv).value;
           //show(otherdiv);
           
           HIVcareArtEnableARVDrugsPoor(ddlclient,'1')
       }


   }

   function HIVcareArtPoorFairOtherBRule(otherobj, tobebecheck, divname) {
    var text=   document.getElementById(otherobj).value
       var cdivId = divname;
       if (text == tobebecheck) {
           show(cdivId);
       }
       else {
           hide(cdivId);
       }

   }

   function HIVcareArtDisableARVDrugsPoor(ddlClientID,isindexzero) {
       document.getElementById(ddlClientID).disabled = true;
       if (isindexzero == '1') {
           document.getElementById(ddlClientID).selectedIndex = 0;
       }
   }
   function HIVcareArtEnableARVDrugsPoor(ddlClientID, isindexzero) {

       document.getElementById(ddlClientID).disabled = false;

       if (isindexzero == '1') {
           document.getElementById(ddlClientID).selectedIndex = 0;
       }

   }
   
   
  function HIVCareEncounterARTStartBrule(HIVdate, artstdate,objCtl)
 // function HIVCareEncounterARTStartBrule(obj)
  {
      if (artstdate == 0) {
          document.getElementById(objCtl).value = 0;
          return true;
      }
    var retValue ;
    var HIVdd = document.getElementById(HIVdate).value.toString().substr(0,2);
    var HIVmm = document.getElementById(HIVdate).value.toString().substr(3,3);
    var HIVyr = document.getElementById(HIVdate).value.toString().substr(7,4);
    var hmm;
	switch(HIVmm.toLowerCase())
	{
		case "jan": hmm = "01";
		break;
		case "feb": hmm = "02";
		break;
		case "mar": hmm = "03";
		break;
		case "apr": hmm = "04";		
		break;
		case "may": hmm = "05";
		break;
		case "jun": hmm = "06";
		break;
		case "jul": hmm = "07";	
		break;
		case "aug": hmm = "08";
		break;
		case "sep": hmm = "09";
		break;
		case "oct": hmm = "10";
		break;
		case "nov": hmm = "11";	
		break;
		case "dec": hmm = "12";	
		break;
    }


    var visitdateinMonth =parseInt(hmm) +  parseInt(HIVyr) *12 ;
   
  if(artstdate < visitdateinMonth )
  {
     retValue  = visitdateinMonth  - artstdate;
  }
  else
  {
   retValue  =0;
  }
 document.getElementById(objCtl).value =retValue ;

}
function HIVCareEncounterARTRegimeBrule(HIVdate, regimeDate, objCtl)
// function HIVCareEncounterARTStartBrule(obj)
{
    if (regimeDate == 0) {
        document.getElementById(objCtl).value = 0;
        return true;
    }
    var retValue;
    var HIVdd = document.getElementById(HIVdate).value.toString().substr(0, 2);
    var HIVmm = document.getElementById(HIVdate).value.toString().substr(3, 3);
    var HIVyr = document.getElementById(HIVdate).value.toString().substr(7, 4);
    var hmm;
    switch (HIVmm.toLowerCase()) {
        case "jan": hmm = "01";
            break;
        case "feb": hmm = "02";
            break;
        case "mar": hmm = "03";
            break;
        case "apr": hmm = "04";
            break;
        case "may": hmm = "05";
            break;
        case "jun": hmm = "06";
            break;
        case "jul": hmm = "07";
            break;
        case "aug": hmm = "08";
            break;
        case "sep": hmm = "09";
            break;
        case "oct": hmm = "10";
            break;
        case "nov": hmm = "11";
            break;
        case "dec": hmm = "12";
            break;
    }


    var visitdateinMonth = parseInt(hmm) + parseInt(HIVyr) * 12;

    if (regimeDate < visitdateinMonth) {
        retValue = visitdateinMonth - regimeDate;
    }
    else {
        retValue = 0;
    }
    document.getElementById(objCtl).value = retValue;

}

function validateStrongPassword(objtxt, options) {

    var pw = document.getElementById(objtxt).value;
    if (pw != '') {
        // default options (allows any password)
        var o = {
            lower: 0,
            upper: 0,
            alpha: 0, /* lower + upper */
            numeric: 0,
            special: 0,
            length: [0, Infinity],
            custom: [ /* regexes and/or functions */],
            badWords: [],
            badSequenceLength: 0,
            noQwertySequences: false,
            noSequential: false
        };

        for (var property in options)
            o[property] = options[property];

        var re = {
            lower: /[a-z]/g,
            upper: /[A-Z]/g,
            alpha: /[A-Z]/gi,
            numeric: /[0-9]/g,
            special: /[\W_]/g
        },
		rule, i;
        // enforce min/max length
        if (pw.length < o.length[0] || pw.length > o.length[1]) {
            //alert("Password Length should be geater than or equal to 6 characters")
            document.getElementById(objtxt).focus();
            return false;
        }

        // enforce lower/upper/alpha/numeric/special rules
        for (rule in re) {
            if ((((pw.match(re[rule]) || []).length > 0) && (o[rule] == -1)) || ((pw.match(re[rule]) || []).length < o[rule])) {
                //alert("Password should contain at least one Upper case Letter, one numeric and one non alpha character.");
                document.getElementById(objtxt).focus();
                return false;
            }
//            if ((pw.match(re[rule]) || []).length < o[rule]) {
//                alert("Password should contain at least one Upper case Letter, one numeric and one non alpha character.");
//                document.getElementById(objtxt).focus();
//                return false;
//            }
        }

        // enforce word ban (case insensitive)
            for (i = 0; i < o.badWords.length; i++) {
                if (pw.toLowerCase().indexOf(o.badWords[i].toLowerCase()) > -1) {
                    //alert("Password should not contain 'password',firstname, Lastname,Username word.");
                    document.getElementById(objtxt).focus();
                    return false;
                }
            }

        // enforce the no sequential, identical characters rule
        if (o.noSequential && /([\S\s])\1/.test(pw))
            return false;

        // enforce alphanumeric/qwerty sequence ban rules
        if (o.badSequenceLength) {
            var lower = "abcdefghijklmnopqrstuvwxyz",
			upper = lower.toUpperCase(),
			numbers = "0123456789",
			qwerty = "qwertyuiopasdfghjklzxcvbnm",
			start = o.badSequenceLength - 1,
			seq = "_" + pw.slice(0, start);
            for (i = start; i < pw.length; i++) {
                seq = seq.slice(1) + pw.charAt(i);
                if (lower.indexOf(seq) > -1 ||upper.indexOf(seq) > -1 ||numbers.indexOf(seq) > -1 ||(o.noQwertySequences && qwerty.indexOf(seq) > -1)) {
                    //alert("Password should not contain not more than 3 consecutive characters.e.g '1234' or 'abcd'");
                    document.getElementById(objtxt).focus();
                    return false;
                }
            }
        }

        // enforce custom regex/function rules
        for (i = 0; i < o.custom.length; i++) {
            rule = o.custom[i];
            if (rule instanceof RegExp) {
                if (!rule.test(pw))
                    return false;
            } else if (rule instanceof Function) {
                if (!rule(pw))
                    return false;
            }
        }
    }

    // great success!
    return true;
}

function CalculateBSA(txtWeight, txtHeight, txtBSA) {
    var weight = document.getElementById(txtWeight).value;
    var height = document.getElementById(txtHeight).value;

    if (weight == "" || height == "") {
        weight = 0;
        height = 0;
        document.getElementById(txtBSA).value = "";
    }
    else {
        var BSA = Math.sqrt((height * weight) / 3600.0);
        BSA = BSA.toFixed(2);
        document.getElementById(txtBSA).value = BSA;
    }
}

function CalculateDaysToNextAppointment(txtAppointmentDate, txtDays) {
    var appointmentdate = new Date(Date.parse(document.getElementById(txtAppointmentDate).value));
    var todayDate = new Date();

    alert(appointmentdate);

    var noOfDays = ((appointmentdate.getTime() - todayDate.getTime()) / 1000 * 60 * 60 * 24);

    alert(noOfDays);

    document.getElementById(txtDays).value = noOfDays;
}

function CalculateNextAppointment(txtAppointmentDate, txtDays) {
    var todaydate = new Date();
    var days = document.getElementById(txtDays).value;

    var month = new Array();
    month[0] = "Jan";
    month[1] = "Feb";
    month[2] = "Mar";
    month[3] = "Apr";
    month[4] = "May";
    month[5] = "Jun";
    month[6] = "Jul";
    month[7] = "Aug";
    month[8] = "Sep";
    month[9] = "Oct";
    month[10] = "Nov";
    month[11] = "Dec";

    var appDate = new Date(todaydate.setTime(todaydate.getTime() + days * 86400000));

    var dd = appDate.getDate();
    var mm = month[appDate.getMonth()];
    var y = appDate.getFullYear();

    document.getElementById(txtAppointmentDate).value = dd + '-' + mm + '-' + y;
}

function CalculateDrugsPrescribed(txtMorning, txtMidday, txtEvening, txtNight, txtDuration, txtQtyPrescribed, valSyrup, valQtyUnitDisp) {
    var morning = document.getElementById(txtMorning).value;
    var midday = document.getElementById(txtMidday).value;
    var evening = document.getElementById(txtEvening).value;
    var night = document.getElementById(txtNight).value;
    var duration = document.getElementById(txtDuration).value;

    if (morning == "") morning = 0;
    if (midday == "") midday = 0;
    if (evening == "") evening = 0;
    if (night == "") night = 0;

    document.getElementById(txtMorning).value = morning;
    document.getElementById(txtMidday).value = midday;
    document.getElementById(txtEvening).value = evening;
    document.getElementById(txtNight).value = night;

    //alert((morning + midday + evening + night));

    if ((parseFloat(morning) + parseFloat(midday) + parseFloat(evening) + parseFloat(night)) > -1 && duration != "") {

        if (valSyrup == "1") {
            var qty = 1;
            document.getElementById(txtQtyPrescribed).value = qty;
        }
        else {

            var qty = duration * (1 * parseFloat(morning) + 1 * parseFloat(midday) + 1 * parseFloat(evening) + 1 * parseFloat(night));
            document.getElementById(txtQtyPrescribed).value = qty;
        }
    }
    else {
        document.getElementById(txtQtyPrescribed).value = "";
    }
}


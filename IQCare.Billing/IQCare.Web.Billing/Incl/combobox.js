
var isComboBoxOpen=false;
var arrItemList=new Array();
function createComboBox(div,defaultItem,items)
{
   //container=document.getElementById(div);
  
   arrItemList=items;
   myCombo='<div class="leftCorner"></div><div class="midSection" id="ComboBoxmidSection"><label>'+defaultItem+'</label></div><div class="rightCorner" onClick="toggleCombobox()"></div><div class="clearDiv"></div>';
   myItems='<div class="itemContainer" id="comboBoxItems">';
   for(i=0;items.length>i;i++)
   {
      
      myItems=myItems+'<div class="comboBoxItem" onClick="openURL('+i+')">'+items[i].Label+'</div>';
   }
   myItems=myItems+'</div>';
   myCombo=myCombo+myItems;
   //container.innerHTML=myCombo;
   document.getElementById(div).innerHTML=myCombo;
}

function toggleCombobox()
{
   if(isComboBoxOpen)
   {
      closeComboBox();
   }
   else
   {
      openCombo()
   }
}

function openCombo()
{
   isComboBoxOpen=true;
   document.getElementById("comboBoxItems").style.display="block";
}

function closeComboBox()
{
   isComboBoxOpen=false;
   document.getElementById("comboBoxItems").style.display="none";
}

function openURL(id)
{
   closeComboBox();
   window.location.href=arrItemList[id].URL;
}
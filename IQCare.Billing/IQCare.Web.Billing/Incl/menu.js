
function expandmainmenu(s)
{
  var td = s;
  var d = td.getElementsByTagName("div").item(0);

  td.className = "menuhovermainmenu";
  d.className = "menuhovermainmenu";

}

function expandsubmenu(s)
{
  var td = s;
  var d = td.getElementsByTagName("div").item(0);

  td.className = "menuhoversubmenu";
  d.className = "menuhoversubmenu";
}

function collapsesubmenu(s)
{
  var td = s;
  var d = td.getElementsByTagName("div").item(0);

  td.className = "menuitemsub";
  d.className = "navbutton2";
}

function collapsesubmenulast(s)
{
  var td = s;
  var d = td.getElementsByTagName("div").item(0);

  td.className = "menuitemsublast";
  d.className = "navbutton2";
}


function expand2(s)
{
  var td = s;
  var d = td.getElementsByTagName("div").item(0);

  td.className = "menuHover2";
  d.className = "menuHover2";
}

function collapse2(s)
{
 var td = s;
 var d = td.getElementsByTagName("div").item(0);
 td.className = "navbutton2";
 d.className = "navbutton2";
}


function expand(s)
{
  var td = s;
  var d = td.getElementsByTagName("div").item(0);
  td.className = "menuHover";
  d.className = "menuHover";
}

function expandfindadd(s)
{
      var td = s;
      var d = td.getElementsByTagName("div").item(0);
      td.className = "menuhoverfindadd";
      d.className = "menuhoverfindadd";

}


function collapse(s)
{
  var td = s;
  var d = td.getElementsByTagName("div").item(0);

  td.className = "navbutton";
  d.className = "navbutton";
}


// This is the implementation of SimpleSwap
// by Jehiah Czebotar
// Version 1.1 - June 10, 2005
// Distributed under Creative Commons
//
// Include this script on your page
// then make image rollovers simple like:
// <img src="/images/ss_img.gif" oversrc="/images/ss_img_over.gif">
//
// http://jehiah.com/archive/simple-swap
// 


function SimpleSwap(el,which){
  el.src=el.getAttribute(which || "origsrc");
}

function SimpleSwapSetup(){
  var x = document.getElementsByTagName("img");
  for (var i=0;i<x.length;i++){
    var oversrc = x[i].getAttribute("oversrc");
    if (!oversrc) continue;
      
    // preload image
    // comment the next two lines to disable image pre-loading
    x[i].oversrc_img = new Image();
    x[i].oversrc_img.src=oversrc;
    // set event handlers
    x[i].onmouseover = new Function("SimpleSwap(this,'oversrc');");
    x[i].onmouseout = new Function("SimpleSwap(this);");
    // save original src
    x[i].setAttribute("origsrc",x[i].src);
  }
}

function imgswap(name, overimg)
        {
          eval("document." + name + ".src ='" + overimg + ".gif'");
        }



          var myimages=new Array()


var PreSimpleSwapOnload =(window.onload)? window.onload : function(){};
window.onload = function(){PreSimpleSwapOnload(); SimpleSwapSetup();}


    var ischecked;
function up(radiobutton) 
{
    ischecked = radiobutton.checked;
} 
function down(radiobutton) 
{

    if (ischecked) 
    { 
        radiobutton.checked = false; 
        ischecked = false;
    } 
    else 
    {
        ischecked = true;
    }
}
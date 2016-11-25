<?xml version="1.0" encoding="utf-8" ?>
<!--  
	Transformations of Price List
	Created: February 23, 2015 by Joseph Njung'e
-->
<!DOCTYPE xsl:stylesheet [
    <!ENTITY nbsp "&#160;">
    <!ENTITY copy "&#169;">
]>

<xsl:stylesheet version="1.0"
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
	xmlns:xm="http://www.w3.org/TR/WD-xsl"
	xmlns:fo="http://www.w3.org/1999/XSL/Format"
	xmlns:n1="urn:schemas-microsoft-com:xml-data"
	xmlns:dt="urn:schemas-microsoft-com:datatypes"
	xmlns:msxsl="urn:schemas-microsoft-com:xslt"
	xmlns:fghmiskenya="urn:fghmiskenya">

    <xsl:output method="html" indent="yes" encoding="UTF-8"/>
    <msxsl:script language="Javascript" implements-prefix="fghmiskenya">
        <![CDATA[ 

var Key_Words = new Array(10);
var iKeyNum = 0;
function FormDateTime(nodeDate){
if(nodeDate == null) return("");
var sDate = nodeDate;
if(sDate.length == 0) return("");
return(sDate.substr(8,2) + "/" + sDate.substr(5,2) + "/" + sDate.substr(0,4) + " " + sDate.substr(11,5));
}
function FormShortDateTime(nodeDate){
if(nodeDate == null) return("");
var sDate = nodeDate;
if(sDate.length == 0) return("");
return(sDate.substr(8,2) + "/" + sDate.substr(5,2) + "/" + sDate.substr(0,4));

}
function FormDate(nodeDate){
if(nodeDate == null) return("");
var sDate = nodeDate;
if(sDate.length == 0) return("");
var sMonth = '';
switch(sDate.substr(5,2)){
	case '01': sMonth = 'Jan'; break;
	case '02': sMonth = 'Feb'; break;
	case '03': sMonth = 'Mar'; break;
	case '04': sMonth = 'Apr'; break;
	case '05': sMonth = 'May'; break;
	case '06': sMonth = 'Jun'; break;
	case '07': sMonth = 'Jul'; break;
	case '08': sMonth = 'Aug'; break;
	case '09': sMonth = 'Sep'; break;
	case '10': sMonth = 'Oct'; break;
	case '11': sMonth = 'Nov'; break;
	case '12': sMonth = 'Dec'; break;
	}
return(sDate.substr(8,2) + "-" + sMonth + "-" + sDate.substr(0,4));

}
]]>
    </msxsl:script>

    <xsl:template match="/">
      <![CDATA[<!DOCTYPE html>]]>
        <html>
            <xsl:apply-templates select="/" />
        </html>
    </xsl:template>
    <xsl:template match="/" >
        <head>
            <title>Price List</title>
            <meta http-equiv="Pragma" content="no-cache"/>
            <meta http-equiv="Expires" content="-1"/>
                 <style type="text/css" media="screen">body, td, span, input, div {font-family: Tahoma, Verdana, Arial; font-size: 11px;} .btn {} .docheader td {font-size: 16px; font-family: Times; white-spacing: nowrap}  .printdiv div{page-break-before:always;width:90%;}</style>
                <style type="text/css" media="print">body, td, span, input, div {font-family: Tahoma, Verdana, Arial; font-size: 13px;} .btn {display: none} .docheader td {font-size: 16px; font-family: Times; white-spacing: nowrap}   .printdiv div{page-break-before:always;width:90%;}</style> 

            <script LANGUAGE="Javascript">
                function WinClose(){window.close(); }
                function PrintMe(text){if (confirm("Print this price list?")){   window.print();     }  
            </script>
        </head>
        <body style="margin: 10px; margin-top: 5px">
            <!--Header-->		
            <center>
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <!--<tr class="docheader">
                        <td width="100%" align="center">
                            <img src="../Images/collage_admin.jpg" border="0" width="424" height="117"/>
                        </td>
                    </tr>-->
                    <tr>
                        <td align="center" width="100%" style="padding-top:10px;">
                            <span style="font-size:16px;">
                                <b>
                                    <xsl:value-of select="//Report/Summary/Facility_Name"/>
                                </b>                                
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" width="100%" style="padding-top:10px;">
                            <span style="font-size:16px;">
                                <b>
                                    <xsl:value-of select="fghmiskenya:FormDateTime(string(//Report/Summary/Report_Date))"/>
                                </b>
                            </span>
                        </td>
                    </tr>
                </table>
                <table width="100%" style="margin-bottom: 10px;margin-top: 10px">
                    <tr>
                        <td style="width: 100%;" align="center">
                            <p style="font-size:16px;">
                                Price List as of <xsl:value-of select="fghmiskenya:FormDate(string(//Report/Summary/Price_Date))"/>
                            </p>
                        </td>
                    </tr>
                </table>
                <form id="frmPrint" name="form1">
                    <p align="center" class="btn">
                        <input type="button" name="cmdPrint" class="btn" style="width: 100px" value="Print" onclick="Javascript:window.print();return false;"></input>&nbsp;
                        <input type="button" name="cmdClose" class="btn" style="width: 75px" value="Close" onclick="window.close()"/>
                    </p>
                </form>
                <table cellpadding="2" cellspacing="0" border="0" style="width: 80%; border-top: solid 2px #000000;" align="center">
                    <tr>
                        <td width="80%" style="border-bottom:1px;border-top:1px;border-left:1px;border-right:1px bordercolor:#000000" colspan="2">
                          	<table width="100%" style="width:100%;" >	
							<xsl:apply-templates select="//Report/Data/ItemTypes" mode="pagination" />	
							
                            </table>
                        </td>
                    </tr>
					
                </table>
            </center>
        </body>
    </xsl:template>	
	<xsl:template name="items">
		<xsl:param name="ItemTypeName" />	
		<xsl:param name="ItemTypeID" />			
		<xsl:param name="ItemCount" />	
		<tr>
			<td  colspan="4">
				<b>
				   <xsl:value-of select="$ItemTypeName"/> (<xsl:value-of select="$ItemCount"/>)
				</b>				
			</td>				
		</tr>
		<tr> 
			<td style="white-space:nowrap"><b>Item-Code</b></td>		
			<td  width="50%"><b>Item Name</b></td>		
			<td style="white-space:nowrap"><b>Selling Price</b></td>
			<td style="white-space:nowrap" ><b>Price Date</b></td>
		</tr>		
		<xsl:for-each select="//PriceList/Item[ItemTypeID =$ItemTypeID]">	
			<tr>
				<td><xsl:value-of select="$ItemTypeID"/>-<xsl:value-of select="ItemID"/></td>		
				<td  width="50%"><xsl:value-of select="ItemName"/></td>		
				<td style="white-space:nowrap">
					<xsl:value-of select="format-number(SellingPrice,'#.00')" /> 
					<xsl:choose>
						<xsl:when test="$ItemTypeName = 'Pharmaceuticals'">
						 Per<xsl:choose>
							<xsl:when test="PricedPerItem = 'true'"> Item</xsl:when>
							<xsl:otherwise> Dose</xsl:otherwise>
						</xsl:choose>
					</xsl:when>
					</xsl:choose>					
				</td>
				<td style="white-space:nowrap"><xsl:value-of select="fghmiskenya:FormDate(string(PriceDate))" /></td>	
			</tr>			
		</xsl:for-each>					
	</xsl:template>
	<xsl:template name="footer">
		<tr>
			<td colspan="4" align="center" style="margin-top: 10px;" >
				<br />
				<b>	Generated By: <xsl:value-of select="//Report/Summary/User_Details"/> from IQCare</b>	
			</td>
		</tr>	
	</xsl:template>	
	<xsl:template match="//Report/Data/ItemTypes" mode="pagination"> 
		<xsl:for-each select="ItemType">			
			<xsl:variable name ="ItemTypeName" select="ItemTypeName" />
			<xsl:variable name ="ItemTypeID" select="ItemTypeID" />
			<xsl:variable name ="count"  select="ItemCount" />				
			<xsl:call-template name="items">
				<xsl:with-param name="ItemTypeName" select="ItemTypeName" />
				<xsl:with-param name="ItemTypeID" select="ItemTypeID" />	
				<xsl:with-param name="ItemCount" select="ItemCount" />					
			</xsl:call-template>
		</xsl:for-each>
		<xsl:call-template name="footer" />
	</xsl:template>
</xsl:stylesheet>
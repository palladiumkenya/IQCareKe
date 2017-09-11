<?xml version="1.0" encoding="utf-8"?>
<!--  
	Transformations of MoH 711  XML Data
	Created: March 18, 2014 by Joseph Njung'e
-->

<!--<!DOCTYPE xsl:stylesheet [
	<!ENTITY nbsp "&#160;">
	<!ENTITY copy "&#169;">
]>-->
<xsl:stylesheet version="1.0"
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
	xmlns:xm="http://www.w3.org/TR/WD-xsl"
	xmlns:n1="urn:schemas-microsoft-com:xml-data"
	xmlns:dt="urn:schemas-microsoft-com:datatypes"
	xmlns:msxsl="urn:schemas-microsoft-com:xslt"
xmlns:v="urn:schemas-microsoft-com:vml"
xmlns:o="urn:schemas-microsoft-com:office:office"
xmlns:x="urn:schemas-microsoft-com:office:excel"
	xmlns:futuresgroup="urn:futuresgroup" exclude-result-prefixes="n1 dt v o x futuresgroup">
  <xsl:output method="html" indent="yes" version="4.0" omit-xml-declaration="yes" encoding="UTF-8" doctype-public="-//W3C//DTD XHTML 1.0 Strict//EN"
  doctype-system="http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd"/>
  <msxsl:script language="javascript" implements-prefix="futuresgroup">
    <![CDATA[ 
          function NaNToZero(nodeValue)
          {
            if (nodeValue == 'NaN' || nodeValue == '') return 0
            return nodeValue;
          }
        ]]>
  </msxsl:script>

  <xsl:template match="Root">
    <html>
      <xsl:apply-templates select="/"  mode="main"/>
    </html>
  </xsl:template>
  <xsl:template match="Root" mode="main">
    <head>
      <meta http-equiv="Pragma" content="no-cache" />
      <meta http-equiv="Expires" content="-1" />
      <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
      <meta name="ProgId" content="Excel.Sheet" />
      <meta name="Generator" content="Microsoft Excel  14" />
      <style type="text/css" media="screen">
        body, td, span, input, div
        {
        font-family: Tahoma, Verdana, Arial;
        font-size: 11px;
        text-align: left;
        }
        input
        {
        border-right: #104A7B 1px solid;
        border-top: #AFC4D5 1px solid;
        background: #D6E7EF;
        border-left: #AFC4D5 1px solid;
        cursor: hand;
        color: #000066;
        border-bottom: #104A7B 1px solid;
        height: 19px;
        margin-bottom: 4px;
        margin-right: 4px;
        text-decoration: none;
        font-weight: bold;
        font-size: 11px;
        }
        .Highlight
        {
        font-family: Tahoma, Verdana, Arial;
        font-size: 11px;
        color: #FFFF00;
        background-color: #FF0000;
        }
        .btn
        {
        }
        .bodyheader_1 td
        {
        font-size: 14px;
        font-weight: bold;
        font-family: Times;
        white-space: nowrap;
        border-top: 2px solid #000000;
        }
        .bodyheader_2 td
        {
        font-size: 12px;
        font-weight: bold;
        font-family: Times;
        white-space: nowrap;
        border-top: 1px solid #000000;
        }
        .bodyhighlight td
        {
        font-size: 12px;
        font-weight: bold;
        font-family: Times;
        white-space: nowrap;
        color: #800000;
        border-top: 1px solid #000000;
        }
        .docheader td
        {
        font-size: 16px;
        font-weight: bold;
        font-family: Times;
        white-space: nowrap;
        }
        .footertext
        {
        font-size: 11px;
        }
        .bottomborder
        {
        border-bottom: 1px solid #000000;
        padding-bottom: 10px;
        }
        .errortext
        {
        font-weight: bold;
        color: #800000;
        }
      </style>
      <style type="text/css" media="print">
        body, td, span, input, div
        {
        font-family: Tahoma, Verdana, Arial;
        font-size: 11px;
        }
        .btn
        {
        display: none;
        }
        Highlight
        {
        font-family: Tahoma, Verdana, Arial;
        font-size: 11px;
        color: #FFFF00;
        background-color: #FF0000;
        }
        .docheader td
        {
        font-size: 16px;
        font-weight: bold;
        font-family: Times;
        white-space: nowrap;
        }
        .footertext
        {
        font-size: 9px;
        }
      </style>
      <style type="text/css">
        tr
        {mso-height-source:auto;}
        col
        {mso-width-source:auto;}
        br
        {mso-data-placement:same-cell;}
        .style0
        {mso-number-format:General;
        text-align:general;
        vertical-align:bottom;
        white-space:nowrap;
        mso-rotate:0;
        mso-background-source:auto;
        mso-pattern:auto;
        color:windowtext;
        font-size:10.0pt;
        font-weight:400;
        font-style:normal;
        text-decoration:none;
        font-family:Arial, sans-serif;
        mso-font-charset:0;
        border:none;
        mso-protection:locked visible;
        mso-style-name:Normal;
        mso-style-id:0;}
        .font9
        {color:windowtext;
        font-size:12.0pt;
        font-weight:700;
        font-style:normal;
        text-decoration:none;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;}
        .font10
        {color:windowtext;
        font-size:10.0pt;
        font-weight:700;
        font-style:normal;
        text-decoration:none;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;}
        .font13
        {color:windowtext;
        font-size:10.0pt;
        font-weight:400;
        font-style:normal;
        text-decoration:none;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;}
        .font15
        {color:windowtext;
        font-size:10.0pt;
        font-weight:400;
        font-style:normal;
        text-decoration:none;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;}
        .font17
        {color:black;
        font-size:8.0pt;
        font-weight:400;
        font-style:normal;
        text-decoration:none;
        font-family:Tahoma, sans-serif;
        mso-font-charset:0;}
        .font23
        {color:windowtext;
        font-size:8.0pt;
        font-weight:400;
        font-style:normal;
        text-decoration:none;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;}
        td
        {mso-style-parent:style0;
        padding:0px;
        mso-ignore:padding;
        color:windowtext;
        font-size:10.0pt;
        font-weight:400;
        font-style:normal;
        text-decoration:none;
        font-family:Arial, sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:general;
        vertical-align:bottom;
        border:none;
        mso-background-source:auto;
        mso-pattern:auto;
        mso-protection:locked visible;
        white-space:nowrap;
        mso-rotate:0;}
        .xl65
        {mso-style-parent:style0;
        font-size:11.0pt;
        background:white;
        mso-pattern:black none;
        white-space:normal;}
        .xl66
        {mso-style-parent:style0;
        font-size:11.0pt;
        font-weight:700;
        background:white;
        mso-pattern:black none;
        white-space:normal;}
        .xl67
        {mso-style-parent:style0;
        color:red;
        font-size:11.0pt;
        font-weight:700;
        background:white;
        mso-pattern:black none;
        white-space:normal;}
        .xl68
        {mso-style-parent:style0;
        color:red;
        font-size:9.0pt;
        font-weight:700;
        text-align:center;
        vertical-align:middle;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:none;
        border-left:.5pt solid windowtext;
        background:#D9D9D9;
        mso-pattern:black none;
        white-space:normal;}
        .xl69
        {mso-style-parent:style0;
        color:#4F81BD;
        font-size:11.0pt;
        font-weight:700;
        font-family:Calibri, sans-serif;
        mso-font-charset:0;
        border:.5pt solid windowtext;
        background:white;
        mso-pattern:black none;}
        .xl70
        {mso-style-parent:style0;
        font-size:11.0pt;
        font-family:Calibri, sans-serif;
        mso-font-charset:0;
        border:.5pt solid windowtext;}
        .xl71
        {mso-style-parent:style0;
        font-size:11.0pt;
        border:.5pt solid windowtext;
        background:white;
        mso-pattern:black none;
        white-space:normal;}
        .xl72
        {mso-style-parent:style0;
        font-size:11.0pt;
        font-family:Calibri, sans-serif;
        mso-font-charset:0;
        text-align:left;
        border:.5pt solid windowtext;}
        .xl73
        {mso-style-parent:style0;
        color:black;
        font-size:11.0pt;
        font-family:Calibri, sans-serif;
        mso-font-charset:0;
        border:.5pt solid windowtext;
        background:white;
        mso-pattern:black none;}
        .xl74
        {mso-style-parent:style0;
        color:black;
        border:.5pt solid windowtext;
        background:white;
        mso-pattern:black none;}
        .xl75
        {mso-style-parent:style0;
        font-size:9.0pt;
        text-align:left;
        border:.5pt solid windowtext;}
        .xl76
        {mso-style-parent:style0;
        font-size:12.0pt;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        background:white;
        mso-pattern:black none;}
        .xl77
        {mso-style-parent:style0;
        font-size:11.0pt;
        white-space:normal;}
        .xl78
        {mso-style-parent:style0;
        color:black;
        mso-number-format:"\@";
        border:.5pt solid windowtext;
        background:white;
        mso-pattern:#DCE6F1 none;
        white-space:normal;}
        .xl79
        {mso-style-parent:style0;
        color:#4F81BD;
        font-weight:700;
        mso-number-format:"\@";
        border:.5pt solid windowtext;
        background:white;
        mso-pattern:#DCE6F1 none;
        white-space:normal;}
        .xl80
        {mso-style-parent:style0;
        color:#C0504D;
        font-size:11.0pt;
        font-weight:700;
        font-family:Calibri, sans-serif;
        mso-font-charset:0;
        border:.5pt solid windowtext;
        background:white;
        mso-pattern:black none;}
        .xl81
        {mso-style-parent:style0;
        color:#60497A;
        font-size:11.0pt;
        font-weight:700;
        font-family:Calibri, sans-serif;
        mso-font-charset:0;
        border:.5pt solid windowtext;
        background:white;
        mso-pattern:black none;}
        .xl82
        {mso-style-parent:style0;
        color:#60497A;
        font-weight:700;
        mso-number-format:"\@";
        border:.5pt solid windowtext;
        background:white;
        mso-pattern:#DCE6F1 none;
        white-space:normal;}
        .xl83
        {mso-style-parent:style0;
        white-space:normal;}
        .xl84
        {mso-style-parent:style0;
        font-size:9.0pt;
        text-align:left;
        border:.5pt solid windowtext;
        background:white;
        mso-pattern:black none;}
        .xl85
        {mso-style-parent:style0;
        font-size:9.0pt;}
        .xl86
        {mso-style-parent:style0;
        font-size:11.0pt;
        font-weight:700;
        white-space:normal;}
        .xl87
        {mso-style-parent:style0;
        color:red;
        font-size:11.0pt;
        font-weight:700;
        white-space:normal;}
        .xl88
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:none;
        border-right:1.0pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:none;
        background:#FFFF99;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl89
        {mso-style-parent:style0;
        font-size:11.0pt;
        font-weight:700;
        text-align:center;
        white-space:normal;}
        .xl90
        {mso-style-parent:style0;
        font-size:11.0pt;
        background:yellow;
        mso-pattern:black none;
        white-space:normal;}
        .xl91
        {mso-style-parent:style0;
        font-size:11.0pt;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        background:white;
        mso-pattern:black none;
        white-space:normal;}
        .xl92
        {mso-style-parent:style0;
        font-size:11.0pt;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        background:white;
        mso-pattern:black none;
        white-space:normal;}
        .xl93
        {mso-style-parent:style0;
        font-size:11.0pt;
        font-family:Calibri, sans-serif;
        mso-font-charset:0;
        border:.5pt solid windowtext;
        background:white;
        mso-pattern:black none;}
        .xl94
        {mso-style-parent:style0;
        font-size:11.0pt;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        white-space:normal;}
        .xl95
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:.5pt solid windowtext;
        background:#FFFF99;
        mso-pattern:black none;
        mso-protection:unlocked visible;}
        .xl96
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:none;
        border-right:1.0pt solid windowtext;
        border-bottom:none;
        border-left:none;
        background:#FFFF99;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl97
        {mso-style-parent:style0;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:none;
        border-right:1.0pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:none;
        background:#FFFF99;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl98
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:top;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:1.0pt solid windowtext;
        white-space:normal;}
        .xl99
        {mso-style-parent:style0;
        color:black;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        border:.5pt solid windowtext;
        background:white;
        mso-pattern:black none;}
        .xl100
        {mso-style-parent:style0;
        font-size:11.0pt;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        border:.5pt solid windowtext;}
        .xl101
        {mso-style-parent:style0;
        font-size:11.0pt;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        border:.5pt solid windowtext;
        background:white;
        mso-pattern:black none;
        white-space:normal;}
        .xl102
        {mso-style-parent:style0;
        font-size:9.0pt;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:left;
        border:.5pt solid windowtext;}
        .xl103
        {mso-style-parent:style0;
        color:#4F81BD;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        mso-number-format:"\@";
        border:.5pt solid windowtext;
        background:white;
        mso-pattern:#DCE6F1 none;
        white-space:normal;}
        .xl104
        {mso-style-parent:style0;
        font-size:11.0pt;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:left;
        border:.5pt solid windowtext;}
        .xl105
        {mso-style-parent:style0;
        font-size:16.0pt;
        text-align:center;
        vertical-align:middle;
        border:.5pt solid windowtext;
        background:yellow;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl106
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border-top:.5pt solid windowtext;
        border-right:none;
        border-bottom:.5pt solid windowtext;
        border-left:.5pt solid windowtext;
        background:#FFFF99;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl107
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:top;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:none;
        border-left:.5pt solid windowtext;
        background:#FFFF99;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl108
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border-top:.5pt solid windowtext;
        border-right:none;
        border-bottom:none;
        border-left:.5pt solid windowtext;
        background:#FFFF99;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl109
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border:.5pt dot-dot-dash windowtext;
        background:#FFFF99;
        mso-pattern:black none;
        mso-protection:unlocked visible;}
        .xl110
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:1.0pt solid windowtext;
        background:#FFFF99;
        mso-pattern:black none;
        mso-protection:unlocked visible;}
        .xl111
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border-top:.5pt solid windowtext;
        border-right:1.0pt solid windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:.5pt solid windowtext;
        background:#FFFF99;
        mso-pattern:black none;
        mso-protection:unlocked visible;}
        .xl112
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border-top:.5pt dot-dot-dash windowtext;
        border-right:none;
        border-bottom:.5pt dot-dot-dash windowtext;
        border-left:.5pt dot-dot-dash windowtext;
        background:#FFFF99;
        mso-pattern:black none;
        mso-protection:unlocked visible;}
        .xl113
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:1.0pt solid windowtext;
        background:#FFFF99;
        mso-pattern:black none;
        mso-protection:unlocked visible;}
        .xl114
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border-top:.5pt solid windowtext;
        border-right:1.0pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:.5pt solid windowtext;
        background:#FFFF99;
        mso-pattern:black none;
        mso-protection:unlocked visible;}
        .xl115
        {mso-style-parent:style0;
        font-size:11.0pt;
        background:white;
        mso-pattern:black none;}
        .xl116
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border:.5pt solid windowtext;
        background:#FFFF99;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl117
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border:.5pt solid windowtext;
        background:#FFFF99;
        mso-pattern:black none;
        mso-protection:unlocked visible;}
        .xl118
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border:.5pt solid windowtext;
        background:#FFFF99;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl119
        {mso-style-parent:style0;
        font-size:11.0pt;
        background:white;
        mso-pattern:black none;
        mso-protection:unlocked visible;}
        .xl120
        {mso-style-parent:style0;
        font-size:11.0pt;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        background:white;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl121
        {mso-style-parent:style0;
        font-size:11.0pt;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        border:.5pt solid windowtext;
        background:silver;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl122
        {mso-style-parent:style0;
        font-size:11.0pt;
        background:white;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl123
        {mso-style-parent:style0;
        font-size:11.0pt;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        border-top:none;
        border-right:none;
        border-bottom:none;
        border-left:1.0pt solid windowtext;
        background:gray;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl124
        {mso-style-parent:style0;
        font-size:11.0pt;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        background:gray;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl125
        {mso-style-parent:style0;
        font-size:11.0pt;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        background:gray;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl126
        {mso-style-parent:style0;
        font-size:11.0pt;
        font-weight:700;
        background:gray;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl127
        {mso-style-parent:style0;
        color:red;
        font-size:11.0pt;
        font-weight:700;
        border-top:none;
        border-right:1.0pt solid windowtext;
        border-bottom:none;
        border-left:none;
        background:gray;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl128
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:1.0pt solid windowtext;
        mso-protection:unlocked visible;}
        .xl129
        {mso-style-parent:style0;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        vertical-align:middle;
        border:.5pt solid windowtext;
        mso-protection:unlocked visible;}
        .xl130
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        vertical-align:middle;
        border:.5pt solid windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl131
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:1.0pt solid windowtext;
        mso-protection:unlocked visible;}
        .xl132
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        vertical-align:middle;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:.5pt solid windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl133
        {mso-style-parent:style0;
        background:white;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl134
        {mso-style-parent:style0;
        font-size:11.0pt;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        border-top:none;
        border-right:none;
        border-bottom:none;
        border-left:1.0pt solid windowtext;
        background:white;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl135
        {mso-style-parent:style0;
        font-size:11.0pt;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        background:white;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl136
        {mso-style-parent:style0;
        font-size:11.0pt;
        font-weight:700;
        background:white;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl137
        {mso-style-parent:style0;
        color:red;
        font-size:11.0pt;
        font-weight:700;
        background:white;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl138
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:1.0pt solid windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl139
        {mso-style-parent:style0;
        color:red;
        font-size:11.0pt;
        font-weight:700;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl140
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:1.0pt solid windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl141
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:1.0pt solid windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl142
        {mso-style-parent:style0;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border-top:1.0pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:.5pt solid windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl143
        {mso-style-parent:style0;
        color:red;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border-top:1.0pt solid windowtext;
        border-right:1.0pt solid windowtext;
        border-bottom:none;
        border-left:.5pt solid windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl144
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border:.5pt solid windowtext;
        background:silver;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl145
        {mso-style-parent:style0;
        color:red;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border:1.0pt solid windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl146
        {mso-style-parent:style0;
        color:red;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:top;
        border:1.0pt solid windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl147
        {mso-style-parent:style0;
        color:red;
        font-size:11.0pt;
        font-weight:700;
        border:1.0pt solid windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl148
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:2.0pt double windowtext;
        mso-protection:unlocked visible;}
        .xl149
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:2.0pt double windowtext;
        border-left:2.0pt double windowtext;
        mso-protection:unlocked visible;}
        .xl150
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:2.0pt double windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl151
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:2.0pt double windowtext;
        border-left:2.0pt double windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl152
        {mso-style-parent:style0;
        font-weight:700;
        text-align:center;
        vertical-align:top;
        border-top:none;
        border-right:.5pt solid windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:1.0pt solid windowtext;
        mso-protection:unlocked visible;}
        .xl153
        {mso-style-parent:style0;
        font-weight:700;
        text-align:center;
        vertical-align:top;
        border-top:none;
        border-right:.5pt solid windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:.5pt solid windowtext;
        mso-protection:unlocked visible;}
        .xl154
        {mso-style-parent:style0;
        font-weight:700;
        text-align:center;
        vertical-align:top;
        border-top:none;
        border-right:1.0pt solid windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:.5pt solid windowtext;
        mso-protection:unlocked visible;}
        .xl155
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:1.0pt solid windowtext;
        mso-protection:unlocked visible;}
        .xl156
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border-top:.5pt dot-dot-dash windowtext;
        border-right:none;
        border-bottom:.5pt dot-dot-dash windowtext;
        border-left:.5pt dot-dot-dash windowtext;
        background:#D9D9D9;
        mso-pattern:black none;
        mso-protection:unlocked visible;}
        .xl157
        {mso-style-parent:style0;
        color:red;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border:1.0pt solid windowtext;
        mso-protection:unlocked visible;}
        .xl158
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:1.0pt solid windowtext;
        mso-protection:unlocked visible;}
        .xl159
        {mso-style-parent:style0;
        font-size:7.0pt;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:none;
        border-right:1.0pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl160
        {mso-style-parent:style0;
        color:red;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:none;
        border-right:1.0pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl161
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:.5pt solid windowtext;
        border-right:none;
        border-bottom:none;
        border-left:.5pt solid windowtext;
        background:silver;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl162
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:.5pt solid windowtext;
        border-right:none;
        border-bottom:none;
        border-left:none;
        background:silver;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl163
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:none;
        border-left:none;
        background:silver;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl164
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:none;
        border-right:none;
        border-bottom:none;
        border-left:.5pt solid windowtext;
        background:silver;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl165
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        background:silver;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl166
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:none;
        border-right:.5pt solid windowtext;
        border-bottom:none;
        border-left:none;
        background:silver;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl167
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:none;
        border-right:none;
        border-bottom:1.0pt solid windowtext;
        border-left:.5pt solid windowtext;
        background:silver;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl168
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:none;
        border-right:none;
        border-bottom:1.0pt solid windowtext;
        border-left:none;
        background:silver;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl169
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:none;
        border-right:.5pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:none;
        background:silver;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl170
        {mso-style-parent:style0;
        font-size:11.0pt;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl171
        {mso-style-parent:style0;
        font-size:11.0pt;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        background:white;
        mso-pattern:black none;
        mso-protection:unlocked visible;}
        .xl172
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:left;
        vertical-align:middle;
        background:white;
        mso-pattern:black none;
        mso-protection:unlocked visible;}
        .xl173
        {mso-style-parent:style0;
        font-size:11.0pt;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        background:white;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl174
        {mso-style-parent:style0;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:top;
        border-top:none;
        border-right:1.0pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl175
        {mso-style-parent:style0;
        color:red;
        font-size:9.0pt;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:top;
        border-top:none;
        border-right:1.0pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl176
        {mso-style-parent:style0;
        color:red;
        font-size:9.0pt;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:none;
        border-right:1.0pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl177
        {mso-style-parent:style0;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border:.5pt solid windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl178
        {mso-style-parent:style0;
        color:red;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border-top:.5pt solid windowtext;
        border-right:1.0pt solid windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:.5pt solid windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl179
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:left;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:1.0pt solid windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl180
        {mso-style-parent:style0;
        color:red;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border-top:1.0pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:1.0pt solid windowtext;
        mso-protection:unlocked visible;}
        .xl181
        {mso-style-parent:style0;
        font-size:11.0pt;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl182
        {mso-style-parent:style0;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:top;
        border:.5pt solid windowtext;
        white-space:normal;}
        .xl183
        {mso-style-parent:style0;
        font-size:11.0pt;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:top;
        border:.5pt solid windowtext;
        white-space:normal;}
        .xl184
        {mso-style-parent:style0;
        color:#963634;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:top;
        border:.5pt solid windowtext;
        white-space:normal;}
        .xl185
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        vertical-align:top;
        border:.5pt solid windowtext;
        white-space:normal;}
        .xl186
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:top;
        border:.5pt solid windowtext;
        background:#FFFF99;
        mso-pattern:black none;
        white-space:normal;}
        .xl187
        {mso-style-parent:style0;
        color:red;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border-top:1.0pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:1.0pt solid windowtext;}
        .xl188
        {mso-style-parent:style0;
        font-size:11.0pt;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:top;
        border:.5pt solid windowtext;
        background:#FFFF99;
        mso-pattern:black none;
        white-space:normal;}
        .xl189
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:top;
        border:.5pt solid windowtext;
        background:gray;
        mso-pattern:black none;
        white-space:normal;}
        .xl190
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border:.5pt solid windowtext;
        background:#FFFF99;
        mso-pattern:black none;
        white-space:normal;}
        .xl191
        {mso-style-parent:style0;
        color:#963634;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border:.5pt solid windowtext;
        white-space:normal;}
        .xl192
        {mso-style-parent:style0;
        color:red;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:1.0pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:1.0pt solid windowtext;}
        .xl193
        {mso-style-parent:style0;
        color:red;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        vertical-align:top;
        border:.5pt solid windowtext;
        white-space:normal;}
        .xl194
        {mso-style-parent:style0;
        color:red;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:top;
        border:.5pt solid windowtext;
        white-space:normal;}
        .xl195
        {mso-style-parent:style0;
        color:red;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:top;
        border:.5pt solid windowtext;
        white-space:normal;}
        .xl196
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border:.5pt solid windowtext;
        background:gray;
        mso-pattern:black none;
        white-space:normal;}
        .xl197
        {mso-style-parent:style0;
        color:#963634;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border:.5pt solid windowtext;
        background:gray;
        mso-pattern:black none;
        white-space:normal;}
        .xl198
        {mso-style-parent:style0;
        color:red;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:top;
        border-top:.5pt solid windowtext;
        border-right:none;
        border-bottom:.5pt solid windowtext;
        border-left:.5pt solid windowtext;
        white-space:normal;}
        .xl199
        {mso-style-parent:style0;
        color:red;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:top;
        border-top:1.0pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:1.5pt solid windowtext;
        white-space:normal;}
        .xl200
        {mso-style-parent:style0;
        color:red;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        vertical-align:top;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:.5pt solid windowtext;
        white-space:normal;}
        .xl201
        {mso-style-parent:style0;
        color:red;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:.5pt solid windowtext;
        white-space:normal;}
        .xl202
        {mso-style-parent:style0;
        color:#963634;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:.5pt solid windowtext;
        white-space:normal;}
        .xl203
        {mso-style-parent:style0;
        color:white;
        font-weight:700;
        text-align:left;
        vertical-align:middle;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:2.0pt double windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl204
        {mso-style-parent:style0;
        font-weight:700;
        text-align:left;
        border-top:2.0pt double windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:.5pt solid windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl205
        {mso-style-parent:style0;
        font-weight:700;
        text-align:left;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:2.0pt double windowtext;
        border-left:.5pt solid windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl206
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border:.5pt solid windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl207
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border:.5pt solid windowtext;
        background:white;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl208
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border-top:.5pt solid windowtext;
        border-right:none;
        border-bottom:none;
        border-left:.5pt solid windowtext;
        background:white;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl209
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border-top:.5pt solid windowtext;
        border-right:none;
        border-bottom:none;
        border-left:none;
        background:white;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl210
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:none;
        border-left:none;
        background:white;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl211
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:.5pt solid windowtext;
        background:white;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl212
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border-top:.5pt solid windowtext;
        border-right:none;
        border-bottom:1.0pt solid windowtext;
        border-left:.5pt solid windowtext;
        background:white;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl213
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border-top:.5pt solid windowtext;
        border-right:none;
        border-bottom:1.0pt solid windowtext;
        border-left:none;
        background:white;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl214
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:none;
        background:white;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl215
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:left;
        border:.5pt solid windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl216
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:top;
        border:.5pt solid windowtext;
        background:#FFFF99;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl217
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:top;
        border-top:.5pt solid windowtext;
        border-right:2.0pt double windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:.5pt solid windowtext;
        background:#FFFF99;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl218
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:top;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:2.0pt double windowtext;
        border-left:.5pt solid windowtext;
        background:#FFFF99;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl219
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:top;
        border-top:.5pt solid windowtext;
        border-right:2.0pt double windowtext;
        border-bottom:2.0pt double windowtext;
        border-left:.5pt solid windowtext;
        background:#FFFF99;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl220
        {mso-style-parent:style0;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border:.5pt solid windowtext;
        background:#FFFF99;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl221
        {mso-style-parent:style0;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:.5pt solid windowtext;
        border-right:2.0pt double windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:.5pt solid windowtext;
        background:#FFFF99;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl222
        {mso-style-parent:style0;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:top;
        border-top:1.0pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:.5pt solid windowtext;
        white-space:normal;}
        .xl223
        {mso-style-parent:style0;
        color:white;
        font-weight:700;
        text-align:left;
        vertical-align:middle;
        border-top:1.0pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:1.0pt solid windowtext;
        background:navy;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl224
        {mso-style-parent:style0;
        color:white;
        font-weight:700;
        text-align:left;
        vertical-align:middle;
        border-top:1.0pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:.5pt solid windowtext;
        background:navy;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl225
        {mso-style-parent:style0;
        color:white;
        font-weight:700;
        text-align:left;
        vertical-align:middle;
        border-top:1.0pt solid windowtext;
        border-right:1.0pt solid windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:.5pt solid windowtext;
        background:navy;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl226
        {mso-style-parent:style0;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:left;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:1.0pt solid windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl227
        {mso-style-parent:style0;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:left;
        border:.5pt solid windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl228
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:left;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:.5pt solid windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl229
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:left;
        border-top:.5pt solid windowtext;
        border-right:none;
        border-bottom:1.0pt solid windowtext;
        border-left:.5pt solid windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl230
        {mso-style-parent:style0;
        color:white;
        font-weight:700;
        text-align:left;
        vertical-align:middle;
        border-top:2.0pt double windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:2.0pt double windowtext;
        background:navy;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl231
        {mso-style-parent:style0;
        color:white;
        font-weight:700;
        text-align:left;
        vertical-align:middle;
        border-top:2.0pt double windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:.5pt solid windowtext;
        background:navy;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl232
        {mso-style-parent:style0;
        color:white;
        font-weight:700;
        text-align:left;
        vertical-align:middle;
        border-top:2.0pt double windowtext;
        border-right:2.0pt double windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:.5pt solid windowtext;
        background:navy;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl233
        {mso-style-parent:style0;
        font-weight:700;
        text-align:center;
        vertical-align:top;
        border:.5pt solid windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl234
        {mso-style-parent:style0;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border-top:.5pt solid windowtext;
        border-right:2.0pt double windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:.5pt solid windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl235
        {mso-style-parent:style0;
        color:white;
        font-size:11.0pt;
        font-weight:700;
        text-align:left;
        vertical-align:middle;
        border-top:1.0pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:1.0pt solid windowtext;
        background:navy;
        mso-pattern:black none;
        white-space:normal;}
        .xl236
        {mso-style-parent:style0;
        color:white;
        font-size:11.0pt;
        font-weight:700;
        text-align:left;
        vertical-align:middle;
        border-top:1.0pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:.5pt solid windowtext;
        background:navy;
        mso-pattern:black none;
        white-space:normal;}
        .xl237
        {mso-style-parent:style0;
        color:white;
        font-size:11.0pt;
        font-weight:700;
        text-align:left;
        vertical-align:middle;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:1.0pt solid windowtext;
        background:navy;
        mso-pattern:black none;
        white-space:normal;}
        .xl238
        {mso-style-parent:style0;
        color:white;
        font-size:11.0pt;
        font-weight:700;
        text-align:left;
        vertical-align:middle;
        border:.5pt solid windowtext;
        background:navy;
        mso-pattern:black none;
        white-space:normal;}
        .xl239
        {mso-style-parent:style0;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border-top:1.0pt solid windowtext;
        border-right:none;
        border-bottom:1.0pt solid windowtext;
        border-left:1.0pt solid windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl240
        {mso-style-parent:style0;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border-top:1.0pt solid windowtext;
        border-right:1.0pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl241
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:1.0pt solid windowtext;
        border-right:none;
        border-bottom:none;
        border-left:1.0pt solid windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl242
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:none;
        border-right:none;
        border-bottom:none;
        border-left:1.0pt solid windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl243
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:none;
        border-right:none;
        border-bottom:1.0pt solid windowtext;
        border-left:1.0pt solid windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl244
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:left;
        vertical-align:middle;
        border:.5pt solid windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl245
        {mso-style-parent:style0;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:top;
        border-top:2.0pt double windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:.5pt solid windowtext;
        mso-protection:unlocked visible;}
        .xl246
        {mso-style-parent:style0;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:top;
        border-top:none;
        border-right:2.0pt double windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:.5pt solid windowtext;
        mso-protection:unlocked visible;}
        .xl247
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border:.5pt solid windowtext;
        background:#FFFF99;
        mso-pattern:black none;
        mso-protection:unlocked visible;}
        .xl248
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:.5pt solid windowtext;
        border-right:2.0pt double windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:.5pt solid windowtext;
        background:#FFFF99;
        mso-pattern:black none;
        mso-protection:unlocked visible;}
        .xl249
        {mso-style-parent:style0;
        color:white;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:left;
        border-top:2.0pt double windowtext;
        border-right:none;
        border-bottom:.5pt solid windowtext;
        border-left:2.0pt double windowtext;
        background:navy;
        mso-pattern:black none;
        mso-protection:unlocked visible;}
        .xl250
        {mso-style-parent:style0;
        color:white;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:left;
        border-top:2.0pt double windowtext;
        border-right:none;
        border-bottom:.5pt solid windowtext;
        border-left:none;
        background:navy;
        mso-pattern:black none;
        mso-protection:unlocked visible;}
        .xl251
        {mso-style-parent:style0;
        color:white;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:left;
        border-top:2.0pt double windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:none;
        background:navy;
        mso-pattern:black none;
        mso-protection:unlocked visible;}
        .xl252
        {mso-style-parent:style0;
        color:red;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:1.0pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:1.0pt solid windowtext;
        background:white;
        mso-pattern:black none;
        mso-protection:unlocked visible;}
        .xl253
        {mso-style-parent:style0;
        color:red;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:1.0pt solid windowtext;
        border-right:1.0pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:.5pt solid windowtext;
        background:white;
        mso-pattern:black none;
        mso-protection:unlocked visible;}
        .xl254
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:none;
        border-left:.5pt solid windowtext;
        background:#FFFF99;
        mso-pattern:black none;
        mso-protection:unlocked visible;}
        .xl255
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:.5pt solid windowtext;
        border-right:2.0pt double windowtext;
        border-bottom:none;
        border-left:.5pt solid windowtext;
        background:#FFFF99;
        mso-pattern:black none;
        mso-protection:unlocked visible;}
        .xl256
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:.5pt solid windowtext;
        border-right:2.0pt double windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:.5pt solid windowtext;
        background:#FFFF99;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl257
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:left;
        border-top:1.0pt solid windowtext;
        border-right:none;
        border-bottom:1.0pt solid windowtext;
        border-left:.5pt solid windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl258
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:left;
        border-top:1.0pt solid windowtext;
        border-right:none;
        border-bottom:1.0pt solid windowtext;
        border-left:none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl259
        {mso-style-parent:style0;
        color:black;
        font-weight:700;
        text-align:center;
        vertical-align:middle;
        border-top:.5pt dot-dot-dash windowtext;
        border-right:none;
        border-bottom:.5pt dot-dot-dash windowtext;
        border-left:.5pt dot-dot-dash windowtext;
        background:white;
        mso-pattern:black none;
        mso-protection:unlocked visible;}
        .xl260
        {mso-style-parent:style0;
        font-weight:700;
        text-align:center;
        vertical-align:top;
        border-top:1.0pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:1.0pt solid windowtext;
        mso-protection:unlocked visible;}
        .xl261
        {mso-style-parent:style0;
        font-weight:700;
        text-align:center;
        vertical-align:top;
        border-top:1.0pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:.5pt solid windowtext;
        mso-protection:unlocked visible;}
        .xl262
        {mso-style-parent:style0;
        font-weight:700;
        text-align:center;
        vertical-align:top;
        border-top:1.0pt solid windowtext;
        border-right:1.0pt solid windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:.5pt solid windowtext;
        mso-protection:unlocked visible;}
        .xl263
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:left;
        border-top:.5pt solid windowtext;
        border-right:none;
        border-bottom:.5pt solid windowtext;
        border-left:.5pt solid windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl264
        {mso-style-parent:style0;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:2.0pt double windowtext;
        border-right:2.0pt double windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:2.0pt double windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl265
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:none;
        border-right:.5pt solid windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:.5pt solid windowtext;
        background:#FFFF99;
        mso-pattern:black none;
        mso-protection:unlocked visible;}
        .xl266
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:none;
        border-right:2.0pt double windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:.5pt solid windowtext;
        background:#FFFF99;
        mso-pattern:black none;
        mso-protection:unlocked visible;}
        .xl267
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:2.0pt double windowtext;
        border-left:.5pt solid windowtext;
        background:#FFFF99;
        mso-pattern:black none;
        mso-protection:unlocked visible;}
        .xl268
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:2.0pt double windowtext;
        border-left:.5pt solid windowtext;
        background:#FFFF99;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl269
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:.5pt solid windowtext;
        border-right:2.0pt double windowtext;
        border-bottom:2.0pt double windowtext;
        border-left:.5pt solid windowtext;
        background:#FFFF99;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl270
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:left;
        border:.5pt solid windowtext;
        mso-protection:unlocked visible;}
        .xl271
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:left;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:2.0pt double windowtext;
        border-left:.5pt solid windowtext;
        mso-protection:unlocked visible;}
        .xl272
        {mso-style-parent:style0;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:2.0pt double windowtext;
        border-right:2.0pt double windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:2.0pt double windowtext;
        mso-protection:unlocked visible;}
        .xl273
        {mso-style-parent:style0;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:.5pt solid windowtext;
        border-right:1.0pt solid windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:.5pt solid windowtext;
        background:#FFFF99;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl274
        {mso-style-parent:style0;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:none;
        border-right:.5pt solid windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:.5pt solid windowtext;
        background:#FFFF99;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl275
        {mso-style-parent:style0;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:none;
        border-right:1.0pt solid windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:.5pt solid windowtext;
        background:#FFFF99;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl276
        {mso-style-parent:style0;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:1.0pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:.5pt solid windowtext;
        background:white;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl277
        {mso-style-parent:style0;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:1.0pt solid windowtext;
        border-right:1.0pt solid windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:.5pt solid windowtext;
        background:white;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl278
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:left;
        vertical-align:middle;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:.5pt solid windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl279
        {mso-style-parent:style0;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:.5pt solid windowtext;
        background:#FFFF99;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl280
        {mso-style-parent:style0;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:.5pt solid windowtext;
        border-right:1.0pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:.5pt solid windowtext;
        background:#FFFF99;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl281
        {mso-style-parent:style0;
        font-size:12.0pt;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:1.0pt solid windowtext;
        border-right:none;
        border-bottom:.5pt solid windowtext;
        border-left:1.0pt solid windowtext;
        white-space:normal;}
        .xl282
        {mso-style-parent:style0;
        font-size:12.0pt;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:1.0pt solid windowtext;
        border-right:none;
        border-bottom:.5pt solid windowtext;
        border-left:none;}
        .xl283
        {mso-style-parent:style0;
        font-size:12.0pt;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:1.0pt solid windowtext;
        border-right:1.0pt solid windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:none;}
        .xl284
        {mso-style-parent:style0;
        font-size:12.0pt;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border-top:.5pt solid windowtext;
        border-right:none;
        border-bottom:.5pt solid windowtext;
        border-left:1.0pt solid windowtext;}
        .xl285
        {mso-style-parent:style0;
        font-size:12.0pt;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border-top:.5pt solid windowtext;
        border-right:none;
        border-bottom:.5pt solid windowtext;
        border-left:none;}
        .xl286
        {mso-style-parent:style0;
        font-size:12.0pt;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border-top:.5pt solid windowtext;
        border-right:1.0pt solid windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:none;}
        .xl287
        {mso-style-parent:style0;
        font-size:16.0pt;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border:.5pt solid windowtext;
        background:silver;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl288
        {mso-style-parent:style0;
        font-size:14.0pt;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border:.5pt solid windowtext;
        background:yellow;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl289
        {mso-style-parent:style0;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:top;
        border-top:1.0pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:none;
        border-left:.5pt solid windowtext;
        background:white;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl290
        {mso-style-parent:style0;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:top;
        border-top:1.0pt solid windowtext;
        border-right:1.0pt solid windowtext;
        border-bottom:none;
        border-left:.5pt solid windowtext;
        background:white;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl291
        {mso-style-parent:style0;
        color:red;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:1.0pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:1.0pt solid windowtext;
        background:white;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl292
        {mso-style-parent:style0;
        color:red;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:1.0pt solid windowtext;
        border-right:1.0pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:.5pt solid windowtext;
        background:white;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl293
        {mso-style-parent:style0;
        color:white;
        font-weight:700;
        text-align:left;
        vertical-align:middle;
        border-top:1.0pt solid windowtext;
        border-right:none;
        border-bottom:none;
        border-left:1.0pt solid windowtext;
        background:navy;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl294
        {mso-style-parent:style0;
        color:white;
        font-weight:700;
        text-align:left;
        vertical-align:middle;
        border-top:1.0pt solid windowtext;
        border-right:none;
        border-bottom:none;
        border-left:none;
        background:navy;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl295
        {mso-style-parent:style0;
        color:white;
        font-weight:700;
        text-align:left;
        vertical-align:middle;
        border-top:1.0pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:none;
        border-left:none;
        background:navy;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl296
        {mso-style-parent:style0;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border-top:1.0pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:none;
        border-left:.5pt solid windowtext;
        background:white;
        mso-pattern:black none;
        mso-protection:unlocked visible;}
        .xl297
        {mso-style-parent:style0;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border-top:1.0pt solid windowtext;
        border-right:none;
        border-bottom:none;
        border-left:.5pt solid windowtext;
        background:white;
        mso-pattern:black none;
        mso-protection:unlocked visible;}
        .xl298
        {mso-style-parent:style0;
        text-align:center;
        border-top:1.0pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:none;
        border-left:none;
        mso-protection:unlocked visible;}
        .xl299
        {mso-style-parent:style0;
        color:red;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border-top:1.0pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:none;
        border-left:.5pt solid windowtext;
        background:white;
        mso-pattern:black none;
        mso-protection:unlocked visible;}
        .xl300
        {mso-style-parent:style0;
        color:red;
        border-top:1.0pt solid windowtext;
        border-right:1.0pt solid windowtext;
        border-bottom:none;
        border-left:.5pt solid windowtext;
        mso-protection:unlocked visible;}
        .xl301
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border-top:.5pt solid windowtext;
        border-right:none;
        border-bottom:.5pt solid windowtext;
        border-left:.5pt solid windowtext;
        background:#FFFF99;
        mso-pattern:black none;
        mso-protection:unlocked visible;}
        .xl302
        {mso-style-parent:style0;
        color:red;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border-top:1.0pt solid windowtext;
        border-right:1.0pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:.5pt solid windowtext;
        mso-protection:unlocked visible;}
        .xl303
        {mso-style-parent:style0;
        font-size:11.0pt;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border:.5pt solid windowtext;
        background:yellow;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl304
        {mso-style-parent:style0;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:top;
        border-top:.5pt solid windowtext;
        border-right:none;
        border-bottom:.5pt solid windowtext;
        border-left:.5pt solid windowtext;
        white-space:normal;}
        .xl305
        {mso-style-parent:style0;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:top;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:none;
        white-space:normal;}
        .xl306
        {mso-style-parent:style0;
        color:red;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:1.0pt solid windowtext;
        border-right:1.0pt solid windowtext;
        border-bottom:none;
        border-left:.5pt solid windowtext;
        white-space:normal;}
        .xl307
        {mso-style-parent:style0;
        color:red;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:none;
        border-right:1.0pt solid windowtext;
        border-bottom:none;
        border-left:.5pt solid windowtext;
        white-space:normal;}
        .xl308
        {mso-style-parent:style0;
        color:red;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:none;
        border-right:1.0pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:.5pt solid windowtext;
        white-space:normal;}
        .xl309
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:left;
        vertical-align:middle;
        border:.5pt solid windowtext;
        mso-protection:unlocked visible;}
        .xl310
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border:.5pt solid windowtext;
        background:#A6A6A6;
        mso-pattern:gray none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl311
        {mso-style-parent:style0;
        color:red;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:left;
        vertical-align:middle;
        border:.5pt solid windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl312
        {mso-style-parent:style0;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border:.5pt solid windowtext;
        mso-protection:unlocked visible;}
        .xl313
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:left;
        vertical-align:middle;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:.5pt solid windowtext;
        mso-protection:unlocked visible;}
        .xl314
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:.5pt solid windowtext;
        background:#FFFF99;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl315
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:.5pt solid windowtext;
        background:#FFFF99;
        mso-pattern:black none;
        mso-protection:unlocked visible;}
        .xl316
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:.5pt solid windowtext;
        border-right:1.0pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:.5pt solid windowtext;
        background:#FFFF99;
        mso-pattern:black none;
        mso-protection:unlocked visible;}
        .xl317
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border-top:.5pt solid windowtext;
        border-right:none;
        border-bottom:.5pt solid windowtext;
        border-left:1.0pt solid windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl318
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border-top:.5pt solid windowtext;
        border-right:none;
        border-bottom:.5pt solid windowtext;
        border-left:none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl319
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border-top:.5pt solid windowtext;
        border-right:none;
        border-bottom:.5pt solid windowtext;
        border-left:none;
        background:#FFFFCC;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl320
        {mso-style-parent:style0;
        color:white;
        font-weight:700;
        text-align:left;
        border-top:1.0pt solid windowtext;
        border-right:none;
        border-bottom:.5pt solid windowtext;
        border-left:1.0pt solid windowtext;
        background:navy;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl321
        {mso-style-parent:style0;
        color:white;
        font-weight:700;
        text-align:left;
        border-top:1.0pt solid windowtext;
        border-right:none;
        border-bottom:.5pt solid windowtext;
        border-left:none;
        background:navy;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl322
        {mso-style-parent:style0;
        color:white;
        font-weight:700;
        text-align:left;
        border-top:1.0pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:none;
        background:navy;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl323
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:left;
        border-top:.5pt solid windowtext;
        border-right:none;
        border-bottom:.5pt solid windowtext;
        border-left:none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl324
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:left;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl325
        {mso-style-parent:style0;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:top;
        border-top:1.0pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:.5pt solid windowtext;
        background:white;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl326
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:.5pt solid windowtext;
        border-right:none;
        border-bottom:.5pt solid windowtext;
        border-left:.5pt solid windowtext;
        background:#FFFF99;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl327
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:1.0pt solid windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl328
        {mso-style-parent:style0;
        color:red;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:left;
        border:.5pt solid windowtext;
        mso-protection:unlocked visible;}
        .xl329
        {mso-style-parent:style0;
        color:red;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:left;
        border-top:.5pt solid windowtext;
        border-right:none;
        border-bottom:.5pt solid windowtext;
        border-left:.5pt solid windowtext;
        mso-protection:unlocked visible;}
        .xl330
        {mso-style-parent:style0;
        font-size:9.0pt;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:top;
        border-top:2.0pt double windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:.5pt solid windowtext;
        background:white;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl331
        {mso-style-parent:style0;
        font-size:9.0pt;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:top;
        border-top:2.0pt double windowtext;
        border-right:2.0pt double windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:.5pt solid windowtext;
        background:white;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl332
        {mso-style-parent:style0;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border:.5pt solid windowtext;
        background:#FFFF99;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl333
        {mso-style-parent:style0;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border-top:.5pt solid windowtext;
        border-right:2.0pt double windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:.5pt solid windowtext;
        background:#FFFF99;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl334
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:left;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:2.0pt double windowtext;
        border-left:.5pt solid windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl335
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:left;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:none;
        border-left:.5pt solid windowtext;
        mso-protection:unlocked visible;}
        .xl336
        {mso-style-parent:style0;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:left;
        border:.5pt solid windowtext;
        mso-protection:unlocked visible;}
        .xl337
        {mso-style-parent:style0;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:left;
        border-top:.5pt solid windowtext;
        border-right:none;
        border-bottom:.5pt solid windowtext;
        border-left:.5pt solid windowtext;
        mso-protection:unlocked visible;}
        .xl338
        {mso-style-parent:style0;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:2.0pt double windowtext;
        border-left:.5pt solid windowtext;
        background:#FFFF99;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl339
        {mso-style-parent:style0;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border-top:.5pt solid windowtext;
        border-right:2.0pt double windowtext;
        border-bottom:2.0pt double windowtext;
        border-left:.5pt solid windowtext;
        background:#FFFF99;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl340
        {mso-style-parent:style0;
        color:white;
        font-weight:700;
        text-align:left;
        vertical-align:middle;
        border-top:1.0pt solid windowtext;
        border-right:1.0pt solid windowtext;
        border-bottom:none;
        border-left:none;
        background:navy;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl341
        {mso-style-parent:style0;
        color:white;
        font-weight:700;
        text-align:left;
        vertical-align:middle;
        border-top:none;
        border-right:none;
        border-bottom:none;
        border-left:1.0pt solid windowtext;
        background:navy;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl342
        {mso-style-parent:style0;
        color:white;
        font-weight:700;
        text-align:left;
        vertical-align:middle;
        background:navy;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl343
        {mso-style-parent:style0;
        color:white;
        font-weight:700;
        text-align:left;
        vertical-align:middle;
        border-top:none;
        border-right:1.0pt solid windowtext;
        border-bottom:none;
        border-left:none;
        background:navy;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl344
        {mso-style-parent:style0;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border-top:1.0pt solid windowtext;
        border-right:none;
        border-bottom:1.0pt solid windowtext;
        border-left:none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl345
        {mso-style-parent:style0;
        color:red;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:1.0pt solid windowtext;
        border-right:1.0pt solid windowtext;
        border-bottom:none;
        border-left:1.0pt solid windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl346
        {mso-style-parent:style0;
        color:red;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:none;
        border-right:1.0pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:1.0pt solid windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl347
        {mso-style-parent:style0;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border-top:none;
        border-right:none;
        border-bottom:1.0pt solid windowtext;
        border-left:none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl348
        {mso-style-parent:style0;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border-top:none;
        border-right:1.0pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl349
        {mso-style-parent:style0;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:top;
        border-top:none;
        border-right:none;
        border-bottom:1.0pt solid windowtext;
        border-left:1.0pt solid windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl350
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:left;
        border:1.0pt solid windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl351
        {mso-style-parent:style0;
        color:white;
        font-weight:700;
        text-align:left;
        vertical-align:middle;
        border-top:none;
        border-right:none;
        border-bottom:1.0pt solid windowtext;
        border-left:1.0pt solid windowtext;
        background:navy;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl352
        {mso-style-parent:style0;
        color:white;
        font-weight:700;
        text-align:left;
        vertical-align:middle;
        border-top:none;
        border-right:none;
        border-bottom:1.0pt solid windowtext;
        border-left:none;
        background:navy;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl353
        {mso-style-parent:style0;
        color:white;
        font-weight:700;
        text-align:left;
        vertical-align:middle;
        border-top:none;
        border-right:1.0pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:none;
        background:navy;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl354
        {mso-style-parent:style0;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border-top:1.0pt solid windowtext;
        border-right:none;
        border-bottom:none;
        border-left:none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl355
        {mso-style-parent:style0;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border-top:1.0pt solid windowtext;
        border-right:1.0pt solid windowtext;
        border-bottom:none;
        border-left:none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl356
        {mso-style-parent:style0;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:top;
        border-top:1.0pt solid windowtext;
        border-right:none;
        border-bottom:none;
        border-left:1.0pt solid windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl357
        {mso-style-parent:style0;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:top;
        border-top:1.0pt solid windowtext;
        border-right:1.0pt solid windowtext;
        border-bottom:none;
        border-left:none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl358
        {mso-style-parent:style0;
        color:red;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:left;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:.5pt solid windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl359
        {mso-style-parent:style0;
        color:red;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:left;
        border-top:.5pt solid windowtext;
        border-right:none;
        border-bottom:1.0pt solid windowtext;
        border-left:.5pt solid windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl360
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border-top:1.0pt solid windowtext;
        border-right:1.0pt solid windowtext;
        border-bottom:none;
        border-left:1.0pt solid windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl361
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border-top:none;
        border-right:1.0pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:1.0pt solid windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl362
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:left;
        vertical-align:middle;
        border-top:1.0pt solid windowtext;
        border-right:1.0pt solid windowtext;
        border-bottom:none;
        border-left:1.0pt solid windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl363
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:left;
        vertical-align:middle;
        border-top:none;
        border-right:1.0pt solid windowtext;
        border-bottom:none;
        border-left:1.0pt solid windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl364
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:left;
        vertical-align:middle;
        border-top:.5pt solid windowtext;
        border-right:1.0pt solid windowtext;
        border-bottom:none;
        border-left:1.0pt solid windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl365
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:left;
        vertical-align:middle;
        border-top:none;
        border-right:1.0pt solid windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:1.0pt solid windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl366
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        vertical-align:middle;
        border:.5pt solid windowtext;
        white-space:normal;}
        .xl367
        {mso-style-parent:style0;
        color:white;
        font-weight:700;
        text-align:left;
        vertical-align:middle;
        border-top:1.0pt solid windowtext;
        border-right:none;
        border-bottom:none;
        border-left:1.0pt solid windowtext;
        background:navy;
        mso-pattern:black none;
        mso-protection:unlocked visible;}
        .xl368
        {mso-style-parent:style0;
        color:white;
        font-weight:700;
        text-align:left;
        vertical-align:middle;
        border-top:1.0pt solid windowtext;
        border-right:none;
        border-bottom:none;
        border-left:none;
        background:navy;
        mso-pattern:black none;
        mso-protection:unlocked visible;}
        .xl369
        {mso-style-parent:style0;
        color:white;
        font-weight:700;
        text-align:left;
        vertical-align:middle;
        border-top:none;
        border-right:none;
        border-bottom:.5pt solid windowtext;
        border-left:1.0pt solid windowtext;
        background:navy;
        mso-pattern:black none;
        mso-protection:unlocked visible;}
        .xl370
        {mso-style-parent:style0;
        color:white;
        font-weight:700;
        text-align:left;
        vertical-align:middle;
        border-top:none;
        border-right:none;
        border-bottom:.5pt solid windowtext;
        border-left:none;
        background:navy;
        mso-pattern:black none;
        mso-protection:unlocked visible;}
        .xl371
        {mso-style-parent:style0;
        color:red;
        font-size:9.0pt;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:1.0pt solid windowtext;
        border-right:1.0pt solid windowtext;
        border-bottom:none;
        border-left:1.0pt solid windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl372
        {mso-style-parent:style0;
        color:red;
        font-size:9.0pt;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:middle;
        border-top:none;
        border-right:1.0pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:1.0pt solid windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl373
        {mso-style-parent:style0;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:top;
        border-top:1.0pt solid windowtext;
        border-right:none;
        border-bottom:.5pt solid windowtext;
        border-left:.5pt solid windowtext;
        white-space:normal;}
        .xl374
        {mso-style-parent:style0;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:top;
        border-top:1.0pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:none;
        white-space:normal;}
        .xl375
        {mso-style-parent:style0;
        color:black;
        font-weight:700;
        text-align:center;
        vertical-align:middle;
        border:.5pt dot-dot-dash windowtext;
        background:white;
        mso-pattern:black none;
        mso-protection:unlocked visible;}
        .xl376
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:left;
        vertical-align:middle;
        border-top:none;
        border-right:1.0pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:1.0pt solid windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl377
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:left;
        vertical-align:top;
        border:.5pt solid windowtext;
        white-space:normal;}
        .xl378
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:top;
        border:.5pt solid windowtext;
        white-space:normal;}
        .xl379
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        vertical-align:top;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:1.0pt solid windowtext;
        white-space:normal;}
        .xl380
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        vertical-align:top;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:.5pt solid windowtext;
        white-space:normal;}
        .xl381
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:left;
        vertical-align:top;
        border:.5pt solid windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl382
        {mso-style-parent:style0;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:left;
        vertical-align:top;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:2.0pt double windowtext;
        border-left:.5pt solid windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl383
        {mso-style-parent:style0;
        font-size:11.0pt;
        text-align:center;
        border-top:2.0pt double windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:none;
        border-left:2.0pt double windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl384
        {mso-style-parent:style0;
        font-size:11.0pt;
        text-align:center;
        border-top:none;
        border-right:.5pt solid windowtext;
        border-bottom:2.0pt double windowtext;
        border-left:2.0pt double windowtext;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl385
        {mso-style-parent:style0;
        font-weight:700;
        text-align:left;
        vertical-align:middle;
        border-top:2.0pt double windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:.5pt solid windowtext;
        background:#FFFF99;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl386
        {mso-style-parent:style0;
        font-size:11.0pt;
        font-weight:700;
        text-align:center;
        border-top:2.0pt double windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:.5pt solid windowtext;
        background:#FFFF99;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl387
        {mso-style-parent:style0;
        font-size:11.0pt;
        font-weight:700;
        text-align:center;
        border-top:2.0pt double windowtext;
        border-right:2.0pt double windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:.5pt solid windowtext;
        background:#FFFF99;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl388
        {mso-style-parent:style0;
        font-weight:700;
        mso-number-format:"Short Date";
        text-align:left;
        vertical-align:middle;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:2.0pt double windowtext;
        border-left:.5pt solid windowtext;
        background:#FFFF99;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl389
        {mso-style-parent:style0;
        font-size:11.0pt;
        font-weight:700;
        text-align:center;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:2.0pt double windowtext;
        border-left:.5pt solid windowtext;
        background:#FFFF99;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl390
        {mso-style-parent:style0;
        font-size:11.0pt;
        font-weight:700;
        text-align:center;
        border-top:.5pt solid windowtext;
        border-right:2.0pt double windowtext;
        border-bottom:2.0pt double windowtext;
        border-left:.5pt solid windowtext;
        background:#FFFF99;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl391
        {mso-style-parent:style0;
        color:red;
        font-weight:700;
        text-align:center;
        vertical-align:middle;
        border-top:1.0pt solid windowtext;
        border-right:1.0pt solid windowtext;
        border-bottom:none;
        border-left:1.0pt solid windowtext;
        mso-protection:unlocked visible;}
        .xl392
        {mso-style-parent:style0;
        color:red;
        font-weight:700;
        text-align:center;
        vertical-align:middle;
        border-top:none;
        border-right:1.0pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:1.0pt solid windowtext;
        mso-protection:unlocked visible;}
        .xl393
        {mso-style-parent:style0;
        font-size:11.0pt;
        font-weight:700;
        font-family:"Times New Roman", serif;
        mso-font-charset:0;
        text-align:center;
        border:.5pt solid windowtext;
        background:silver;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl394
        {mso-style-parent:style0;
        font-size:11.0pt;
        font-weight:700;
        text-align:center;
        vertical-align:middle;
        border:.5pt solid windowtext;
        background:yellow;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl395
        {mso-style-parent:style0;
        font-size:11.0pt;
        text-align:center;
        vertical-align:middle;
        border:.5pt solid windowtext;
        background:yellow;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl396
        {mso-style-parent:style0;
        color:white;
        font-weight:700;
        text-align:left;
        border-top:1.0pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:1.0pt solid windowtext;
        background:navy;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
        .xl397
        {mso-style-parent:style0;
        color:white;
        font-weight:700;
        text-align:left;
        border-top:1.0pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:.5pt solid windowtext;
        background:navy;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:normal;}
      </style>
      <script language="javascript" type="text/javascript">

        function WinClose() {
        window.close();
        }

        function PrintMe(text) {
        if (confirm("Print this Report?")) {
        window.print();
        }
        }

      </script>
	  <title>
        MOH 711A - REPRODUCTIVE HEALTH, HIV/AIDS, MALARIA, TB and NUTRITION (Facility
        Form)
      </title>
    </head>
    <body link="blue" vlink="purple" class="xl77">
      <table border="1" cellpadding="0" cellspacing="0" width="720" style="border-collapse: collapse;
        table-layout: fixed; width: 720pt">
        <col class="xl77" style="mso-width-source: userset; mso-width-alt: 1060;" />
        <col class="xl77" width="89" span="6" style="mso-width-source: userset; mso-width-alt: 3254;
            width: 67pt" />
        <col class="xl86" width="89" span="2" style="mso-width-source: userset; mso-width-alt: 3254;
            width: 67pt" />
        <col class="xl87" width="89" style="mso-width-source: userset; mso-width-alt: 3254;
            width: 67pt" />
        <tbody>
          <tr class="xl65" height="20" style="mso-outline-level: 1; height: 15.0pt">
            <td height="20" class="xl119" width="29" style="height: 15.0pt; width: 22pt">
              &#160;
            </td>
            <td class="style1">
              &#160;
            </td>
            <td class="xl65" width="89" style="width: 67pt">
              &#160;
            </td>
            <td class="xl65" colspan="4">
              <form id="frmPrint" name="form1">
                <p align="center" class="btn">
                  <input type="button" name="cmdPrint" class="btn" style="width: 100px" value="Print Report"
                      onclick="Javascript:PrintMe()" />
                  <input type="button" name="cmdClose" class="btn" style="width: 75px" value="Close"
                      onclick="window.close()" />
                </p>
              </form>
            </td>
            <td class="xl66" width="89" style="width: 67pt">
              &#160;
            </td>
            <td class="xl66" width="89" style="width: 67pt">
              &#160;
            </td>
            <td class="xl67" width="89" style="width: 67pt">
              &#160;
            </td>
          </tr>
          <tr class="xl65" height="20" style="mso-outline-level: 1; height: 15.0pt">
            <td height="20" class="xl119" width="29" style="height: 15.0pt; width: 22pt">
              &#160;
            </td>
            <td class="style1">
              &#160;
            </td>
            <td class="xl65" width="89" style="width: 67pt">
              &#160;
            </td>
            <td class="xl65" width="89" style="width: 67pt">
              &#160;
            </td>
            <td width="89" style="width: 67pt" align="left" valign="top">
              &#160;
            </td>
            <td class="xl65" width="89" style="width: 67pt">
              &#160;
            </td>
            <td class="xl65" width="89" style="width: 67pt">
              &#160;
            </td>
            <td class="xl66" width="89" style="width: 67pt">
              &#160;
            </td>
            <td class="xl66" width="89" style="width: 67pt">
              &#160;
            </td>
            <td class="xl67" width="89" style="width: 67pt">
              &#160;
            </td>
          </tr>
          <tr class="xl65" height="20" style="mso-outline-level: 1; height: 15.0pt">
            <td height="20" class="xl119" width="29" style="height: 15.0pt; width: 22pt">
              <a name="Print_Area">&#160;</a>
            </td>
            <td class="style1">
              &#160;
            </td>
            <td class="xl65" width="89" style="width: 67pt">
              &#160;
            </td>
            <td class="xl65" width="89" style="width: 67pt">
              &#160;
            </td>
            <td width="89" style="width: 67pt" align="left" valign="top">
              <span style="mso-ignore: vglayout; position: absolute; z-index: 2; margin-left: 64px;
                    margin-top: 0px; width: 102px; height: 81px">
                <img width="102" height="81" src="../images/kenyaarms.png"  alt="Kenya Arms"  v:shapes="_x0000_s1025" />
              </span>
              <span style="mso-ignore: vglayout2">
                <table cellpadding="0" cellspacing="0">
                  <tr>
                    <td height="20" class="xl65" width="89" style="height: 15.0pt; width: 67pt">
                      &#160;
                    </td>
                  </tr>
                </table>
              </span>
            </td>
            <td class="xl65" width="89" style="width: 67pt">
              &#160;
            </td>
            <td class="xl65" width="89" style="width: 67pt">
              &#160;
            </td>
            <td class="xl66" width="89" style="width: 67pt">
              &#160;
            </td>
            <td class="xl66" width="89" style="width: 67pt">
              &#160;
            </td>
            <td class="xl67" width="89" style="width: 67pt">
              &#160;
            </td>
          </tr>
          <tr class="xl65" height="20" style="mso-outline-level: 1; height: 15.0pt">
            <td height="20" class="xl115" style="height: 15.0pt">
              &#160;
            </td>
            <td class="style1">
              &#160;
            </td>
            <td class="xl65" width="89" style="width: 67pt">
              &#160;
            </td>
            <td class="xl65" width="89" style="width: 67pt">
              &#160;
            </td>
            <td class="xl65" width="89" style="width: 67pt">
              &#160;
            </td>
            <td class="xl65" width="89" style="width: 67pt">
              &#160;
            </td>
            <td class="xl65" width="89" style="width: 67pt">
              &#160;
            </td>
            <td class="xl66" width="89" style="width: 67pt">
              &#160;
            </td>
            <td class="xl66" width="89" style="width: 67pt">
              &#160;
            </td>
            <td class="xl67" width="89" style="width: 67pt">
              &#160;
            </td>
          </tr>
          <tr class="xl65" height="20" style="mso-outline-level: 1; height: 15.0pt">
            <td height="20" class="xl65" width="29" style="height: 15.0pt; width: 22pt">
              &#160;
            </td>
            <td class="style1">
              &#160;
            </td>
            <td class="xl65" width="89" style="width: 67pt">
              &#160;
            </td>
            <td class="xl65" width="89" style="width: 67pt">
              &#160;
            </td>
            <td class="xl65" width="89" style="width: 67pt">
              &#160;
            </td>
            <td class="xl65" width="89" style="width: 67pt">
              &#160;
            </td>
            <td class="xl65" width="89" style="width: 67pt">
              &#160;
            </td>
            <td class="xl66" width="89" style="width: 67pt">
              &#160;
            </td>
            <td class="xl66" width="89" style="width: 67pt">
              &#160;
            </td>
            <td class="xl67" width="89" style="width: 67pt">
              &#160;
            </td>
          </tr>
          <tr class="xl65" height="21" style="mso-outline-level: 1; height: 15.75pt">
            <td height="21" class="xl65" width="29" style="height: 15.75pt; width: 22pt">
              &#160;
            </td>
            <td class="style1">
              &#160;
            </td>
            <td class="xl65" width="89" style="width: 67pt">
              &#160;
            </td>
            <td class="xl65" width="89" style="width: 67pt">
              &#160;
            </td>
            <td class="xl65" width="89" style="width: 67pt">
              &#160;
            </td>
            <td class="xl65" width="89" style="width: 67pt">
              &#160;
            </td>
            <td class="xl65" width="89" style="width: 67pt">
              &#160;
            </td>
            <td class="xl66" width="89" style="width: 67pt">
              &#160;
            </td>
            <td class="xl66" width="89" style="width: 67pt">
              &#160;
            </td>
            <td class="xl67" width="89" style="width: 67pt">
              &#160;
            </td>
          </tr>
          <tr height="36" style="mso-height-source: userset; mso-outline-level: 1; height: 27.0pt">
            <td colspan="10" height="36" class="xl281" width="830" style="border-right: 1.0pt solid black;
                height: 27.0pt; width: 625pt">
              REPUBLIC OF KENYA MINISTRY OF HEALTH
            </td>
          </tr>
          <tr height="21" style="mso-outline-level: 1; height: 15.75pt">
            <td colspan="10" height="21" class="xl284" style="border-right: 1.0pt solid black;
                height: 15.75pt">
              MOH 711A - <font class="font10">REPRODUCTIVE HEALTH, HIV/AIDS, MALARIA, TB and NUTRITION</font><font
                    class="font9"> (Facility Form)</font>
            </td>
          </tr>

          <xsl:apply-templates select="Data/Results[@QUERY_NAME='MoH711_0']"  mode="reportheader" />
          <xsl:call-template name="FAMILYPLANNING" />
          <xsl:call-template name="MCH_ANC_PMTCT" />
          <xsl:call-template name="MATERNITY_PMTCT" />
          <xsl:call-template name="STI" />
          <xsl:call-template name="MATERNITY_SAFE_DELIVERIES" />
          <xsl:call-template name="PAC_SERVICES" />
          <xsl:call-template name="TB" />
          <xsl:call-template name="VCT" />
          <xsl:call-template name="DTC" />
          <xsl:call-template name="CHANIS" />
          <xsl:call-template name="ART" />
          <xsl:call-template name="BLOOD_SAFETY" />
          <xsl:call-template name="SIGNATURE" />
        </tbody>
      </table>

    </body>
  </xsl:template>

  <xsl:template match="Results[@QUERY_NAME = 'MoH711_0']" mode="reportheader">
    <tr class="xl94" height="27" style="mso-outline-level: 1; height: 20.25pt">
      <td colspan="2" height="27" class="xl287" style="height: 20.25pt; ">
        Facility Name
      </td>
      <td colspan="8" class="xl288" width="712" style="border-left: none; width: 536pt">
        &#160;<xsl:value-of select="QueryResults/Row[Count='0']/Indicators[Name='FacilityName']/Result"/>
      </td>
    </tr>
    <tr class="xl94" height="20" style="mso-outline-level: 1; height: 15.0pt">
      <td colspan="2" height="20" class="xl393" style="height: 15.0pt; ">
        CCC Code No.
      </td>
      <td colspan="3" class="xl393" width="267" style="border-left: none; width: 201pt">
        District
      </td>
      <td colspan="2" class="xl393" width="178" style="border-left: none; width: 134pt">
        Province
      </td>
      <td colspan="2" class="xl393" width="178" style="border-left: none; width: 134pt">
        Month
      </td>
      <td class="xl121" width="89" style="border-top: none; border-left: none; width: 67pt">
        Year<span style="mso-spacerun: yes"> </span>
      </td>
    </tr>
    <tr height="27" style="mso-outline-level: 1; height: 20.25pt">
      <td colspan="2" height="27" class="xl303" style="height: 20.25pt; ">
        <xsl:value-of select="QueryResults/Row[Count='0']/Indicators[Name='SiteCode']/Result"/>
      </td>
      <td colspan="3" class="xl395" width="267" style="border-left: none; width: 201pt">
        <xsl:value-of select="QueryResults/Row[Count='0']/Indicators[Name='District']/Result"/>
      </td>
      <td colspan="2" class="xl395" width="178" style="border-left: none; width: 134pt">
        &#160;<xsl:value-of select="QueryResults/Row[Count='0']/Indicators[Name='Region']/Result"/>
      </td>
      <td colspan="2" class="xl394" width="178" style="border-left: none; width: 134pt">
        &#160;<xsl:value-of select="QueryResults/Row[Count='0']/Indicators[Name='Month']/Result"/>
      </td>
      <td class="xl105" width="89" style="border-top: none; border-left: none; width: 67pt">
        &#160;<xsl:value-of select="QueryResults/Row[Count='0']/Indicators[Name='Year']/Result"/>
      </td>
    </tr>
    <tr height="21" style="height: 15.75pt">
      <td height="21" class="xl123" width="29" style="height: 15.75pt; width: 22pt">
        &#160;
      </td>
      <td class="style2">
        &#160;
      </td>
      <td class="xl124" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl124" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl124" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl124" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl124" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl125" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl126" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl127" width="89" style="width: 67pt">
        &#160;
      </td>
    </tr>
  </xsl:template>

  <xsl:template name="FAMILYPLANNING">
    <tr height="21" style="height: 15.75pt">
      <td colspan="4" height="21" class="xl293" width="296" style="border-right: .5pt solid black;
                height: 15.75pt; width: 223pt">
        <span style="mso-spacerun: yes"></span>A: FAMILY PLANNING
      </td>
      <td colspan="2" class="xl296" style="border-left: none">
        New Clients
      </td>
      <td colspan="2" class="xl297" style="border-right: .5pt solid black; border-left: none">
        Re-Visits
      </td>
      <td colspan="2" class="xl299" style="border-right: 1.0pt solid black; border-left: none">
        TOTAL
      </td>
    </tr>
    <tr height="21" style="height: 15.75pt">
      <td rowspan="2" height="42" class="xl128" style="height: 31.5pt">
        1
      </td>
      <td rowspan="2" class="style2">
        Pills
      </td>
      <td colspan="2" class="xl309" style="border-left: none">
        Microlut
      </td>
      <td colspan="2" class="xl116" width="178" style="border-left: none; width: 134pt">
        0
      </td>
      <td colspan="2" class="xl117" style="border-left: none">
        0
      </td>
      <td colspan="2" class="xl180" style="border-right: 1.0pt solid black">
        0
      </td>
    </tr>
    <tr height="21" style="height: 15.75pt">
      <td colspan="2" height="21" class="xl309" style="height: 15.75pt; border-left: none">
        Microgynon
      </td>
      <td colspan="2" class="xl116" width="178" style="border-left: none; width: 134pt">
        0
      </td>
      <td colspan="2" class="xl117" style="border-left: none">
        0
      </td>
      <td colspan="2" class="xl180" style="border-right: 1.0pt solid black">
        0
      </td>
    </tr>
    <tr height="21" style="height: 15.75pt">
      <td height="21" class="xl128" style="height: 15.75pt; border-top: none">
        2
      </td>
      <td class="style2" style="border-top: none; border-left: none">
        Injection
      </td>
      <td colspan="2" class="xl309" style="border-left: none">
        Injection
      </td>
      <td colspan="2" class="xl116" width="178" style="border-left: none; width: 134pt">
        0
      </td>
      <td colspan="2" class="xl117" style="border-left: none">
        0
      </td>
      <td colspan="2" class="xl180" style="border-right: 1.0pt solid black">
        0
      </td>
    </tr>
    <tr height="21" style="height: 15.75pt">
      <td height="21" class="xl128" style="height: 15.75pt; border-top: none">
        3
      </td>
      <td class="style2" style="border-top: none; border-left: none">
        I.U.C.D
      </td>
      <td colspan="2" class="xl309" style="border-left: none">
        Insertion&#160;
      </td>
      <td colspan="2" class="xl116" width="178" style="border-left: none; width: 134pt">
        0
      </td>
      <td colspan="2" class="xl116" width="178" style="border-left: none; width: 134pt">
        0
      </td>
      <td colspan="2" class="xl180" style="border-right: 1.0pt solid black">
        0
      </td>
    </tr>
    <tr height="21" style="height: 15.75pt">
      <td height="21" class="xl128" style="height: 15.75pt; border-top: none">
        4
      </td>
      <td class="style2" style="border-top: none; border-left: none">
        Implants
      </td>
      <td colspan="2" class="xl309" style="border-left: none">
        Insertion&#160;
      </td>
      <td colspan="2" class="xl116" width="178" style="border-left: none; width: 134pt">
        0
      </td>
      <td colspan="2" class="xl116" width="178" style="border-left: none; width: 134pt">
        0
      </td>
      <td colspan="2" class="xl180" style="border-right: 1.0pt solid black">
        0
      </td>
    </tr>
    <tr height="21" style="height: 15.75pt">
      <td rowspan="2" height="42" class="xl128" style="height: 31.5pt; border-top: none">
        5
      </td>
      <td rowspan="2" class="style2" style="border-top: none">
        Sterelization
      </td>
      <td colspan="2" class="xl309" style="border-left: none">
        <span style="mso-spacerun: yes"></span>B.T.L
      </td>
      <td colspan="2" class="xl116" width="178" style="border-left: none; width: 134pt">
        0
      </td>
      <td colspan="2" class="xl310" width="178" style="border-left: none; width: 134pt">
        &#160;
      </td>
      <td colspan="2" class="xl180" style="border-right: 1.0pt solid black">
        0
      </td>
    </tr>
    <tr height="21" style="height: 15.75pt">
      <td colspan="2" height="21" class="xl309" style="height: 15.75pt; border-left: none">
        Vasectomy
      </td>
      <td colspan="2" class="xl116" width="178" style="border-left: none; width: 134pt">
        0
      </td>
      <td colspan="2" class="xl310" width="178" style="border-left: none; width: 134pt">
        &#160;
      </td>
      <td colspan="2" class="xl180" style="border-right: 1.0pt solid black">
        0
      </td>
    </tr>
    <tr height="21" style="height: 15.75pt">
      <td height="21" class="xl128" style="height: 15.75pt; border-top: none">
        6
      </td>
      <td class="style2" style="border-top: none; border-left: none">
        Condoms
      </td>
      <td colspan="2" class="xl309" style="border-left: none">
        No. of Clients receiving
      </td>
      <td colspan="2" class="xl116" width="178" style="border-left: none; width: 134pt">
        0
      </td>
      <td colspan="2" class="xl116" width="178" style="border-left: none; width: 134pt">
        0
      </td>
      <td colspan="2" class="xl180" style="border-right: 1.0pt solid black">
        0
      </td>
    </tr>
    <tr height="35" style="height: 26.25pt">
      <td height="35" class="xl128" style="height: 26.25pt; border-top: none">
        7
      </td>
      <td class="style2"
          style="border-left-style: none; border-left-color: inherit; border-left-width: medium; border-top-style: none; border-top-color: inherit; border-top-width: medium;">
        ALL OTHERS:<br />
        <span style="mso-spacerun: yes"></span>(specify)<span style="mso-spacerun: yes">
        </span>
      </td>
      <td colspan="2" class="xl116" width="178" style="border-left: none; width: 134pt">
        &#160;
      </td>
      <td colspan="2" class="xl116" width="178" style="border-left: none; width: 134pt">
        0
      </td>
      <td colspan="2" class="xl116" width="178" style="border-left: none; width: 134pt">
        0
      </td>
      <td colspan="2" class="xl180" style="border-right: 1.0pt solid black">
        0
      </td>
    </tr>
    <tr height="21" style="height: 15.75pt">
      <td height="21" class="xl128" style="height: 15.75pt; border-top: none">
        8
      </td>
      <td colspan="3" class="xl311" width="267" style="border-left: none; width: 201pt">
        TOTAL NO. OF CLIENTS
      </td>
      <td colspan="2" class="xl312" style="border-left: none">
        0
      </td>
      <td colspan="2" class="xl312" style="border-left: none">
        0
      </td>
      <td colspan="2" class="xl180" style="border-right: 1.0pt solid black">
        0
      </td>
    </tr>
    <tr class="xl83" height="21" style="height: 15.75pt">
      <td height="21" class="xl131" style="height: 15.75pt; border-top: none">
        9
      </td>
      <td class="style2"
          style="border-left-style: none; border-left-color: inherit; border-left-width: medium; border-top-style: none; border-top-color: inherit; border-top-width: medium;">
        REMOVALS:
      </td>
      <td colspan="2" class="xl313" style="border-left: none">
        &#160;IUCD
      </td>
      <td colspan="2" class="xl314" width="178" style="border-left: none; width: 134pt">
        0
      </td>
      <td colspan="2" class="xl278" width="178" style="border-left: none; width: 134pt">
        <span style="mso-spacerun: yes"></span>IMPLANTS
      </td>
      <td colspan="2" class="xl315" style="border-right: 1.0pt solid black; border-left: none">
        0
      </td>
    </tr>
    <tr class="xl65" height="21" style="height: 15.75pt">
      <td height="21" class="xl134" width="29" style="height: 15.75pt; width: 22pt">
        &#160;
      </td>
      <td class="style2">
        &#160;
      </td>
      <td class="xl120" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl120" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl120" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl120" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl120" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl135" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl136" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl137" width="89" style="width: 67pt">
        &#160;
      </td>
    </tr>
  </xsl:template>

  <xsl:template name="MCH_ANC_PMTCT">
    <tr height="21" style="height: 15.75pt">
      <td colspan="4" height="21" class="xl320" width="296" style="border-right: .5pt solid black;
                height: 15.75pt; width: 223pt">
        B: MCH - ANC / PMCT
      </td>
      <td colspan="2" class="xl325" width="178" style="border-left: none; width: 134pt">
        New
      </td>
      <td colspan="2" class="xl325" width="178" style="border-left: none; width: 134pt">
        Re-visit
      </td>
      <td colspan="2" class="xl289" width="178" style="border-right: 1.0pt solid black;
                border-left: none; width: 134pt">
        TOTAL
      </td>
    </tr>
    <tr height="21" style="height: 15.75pt">
      <td height="21" class="xl138" width="29" style="height: 15.75pt; border-top: none;
                width: 22pt">
        1
      </td>
      <td colspan="3" class="xl263" width="267" style="border-right: .5pt solid black;
                border-left: none; width: 201pt">
        No. of ANC Clients
      </td>
      <td colspan="2" class="xl118" width="178" style="border-left: none; width: 134pt">
        0
      </td>
      <td colspan="2" class="xl118" width="178" style="border-left: none; width: 134pt">
        0
      </td>
      <td colspan="2" class="xl291" width="178" style="border-right: 1.0pt solid black;
                width: 134pt">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td height="20" class="xl138" width="29" style="height: 15.0pt; border-top: none;
                width: 22pt">
        2
      </td>
      <td colspan="7" class="xl215" width="623" style="border-left: none; width: 469pt">
        No. of Clients with Hb &lt; 7 g/dl<span style="mso-spacerun: yes"> </span>
      </td>
      <td colspan="2" class="xl274" width="178" style="border-right: 1.0pt solid black;
                border-left: none; width: 134pt">
        0
      </td>
    </tr>
    <tr height="22" style="height: 16.5pt">
      <td height="22" class="xl138" width="29" style="height: 16.5pt; border-top: none;
                width: 22pt">
        3
      </td>
      <td colspan="7" class="xl270" style="border-left: none">
        No. of Clients given IPT (1<font class="font15">
          <sup>st</sup>
        </font><font class="font13">
          dose)<span style="mso-spacerun: yes"> </span>
        </font>
      </td>
      <td colspan="2" class="xl220" width="178" style="border-right: 1.0pt solid black;
                border-left: none; width: 134pt">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td height="20" class="xl138" width="29" style="height: 15.0pt; border-top: none;
                width: 22pt">
        4
      </td>
      <td colspan="7" class="xl215" width="623" style="border-left: none; width: 469pt">
        No. of Clients given IPT (2<font class="font15">
          <sup>nd</sup>
        </font><font class="font13">
          dose)<span style="mso-spacerun: yes"> </span>
        </font>
      </td>
      <td colspan="2" class="xl220" width="178" style="border-right: 1.0pt solid black;
                border-left: none; width: 134pt">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td height="20" class="xl138" width="29" style="height: 15.0pt; border-top: none;
                width: 22pt">
        5
      </td>
      <td colspan="7" class="xl215" width="623" style="border-left: none; width: 469pt">
        No. of Clients completed 4th Antenatal Visit<span style="mso-spacerun: yes"> </span>
      </td>
      <td colspan="2" class="xl220" width="178" style="border-right: 1.0pt solid black;
                border-left: none; width: 134pt">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td height="20" class="xl138" width="29" style="height: 15.0pt; border-top: none;
                width: 22pt">
        6
      </td>
      <td colspan="7" class="xl244" width="623" style="border-left: none; width: 469pt">
        No. of ITNs distributed to ANC clients
      </td>
      <td colspan="2" class="xl220" width="178" style="border-right: 1.0pt solid black;
                border-left: none; width: 134pt">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td rowspan="3" height="60" class="xl138" width="29" style="height: 45.0pt; border-top: none;
                width: 22pt">
        7
      </td>
      <td colspan="4" rowspan="3" class="xl244" width="356" style="width: 268pt">
        No. of ANC clients<span style="mso-spacerun: yes"> </span>
      </td>
      <td colspan="3" class="xl215" width="267" style="border-left: none; width: 201pt">
        Counselled
      </td>
      <td colspan="2" class="xl220" width="178" style="border-right: 1.0pt solid black;
                border-left: none; width: 134pt">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td colspan="3" height="20" class="xl215" width="267" style="height: 15.0pt; border-left: none;
                width: 201pt">
        tested for HIV
      </td>
      <td colspan="2" class="xl220" width="178" style="border-right: 1.0pt solid black;
                border-left: none; width: 134pt">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td colspan="3" height="20" class="xl215" width="267" style="height: 15.0pt; border-left: none;
                width: 201pt">
        HIV+
      </td>
      <td colspan="2" class="xl220" width="178" style="border-right: 1.0pt solid black;
                border-left: none; width: 134pt">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td rowspan="2" height="40" class="xl138" width="29" style="height: 30.0pt; border-top: none;
                width: 22pt">
        8
      </td>
      <td colspan="4" rowspan="2" class="xl244" width="356" style="width: 268pt">
        No. of clients<span style="mso-spacerun: yes"> </span>
      </td>
      <td colspan="3" class="xl215" width="267" style="border-left: none; width: 201pt">
        Tested for Syphilis
      </td>
      <td colspan="2" class="xl220" width="178" style="border-right: 1.0pt solid black;
                border-left: none; width: 134pt">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td colspan="3" height="20" class="xl215" width="267" style="height: 15.0pt; border-left: none;
                width: 201pt">
        +ve
      </td>
      <td colspan="2" class="xl220" width="178" style="border-right: 1.0pt solid black;
                border-left: none; width: 134pt">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td height="20" class="xl138" width="29" style="height: 15.0pt; border-top: none;
                width: 22pt">
        9
      </td>
      <td colspan="7" class="xl215" width="623" style="border-left: none; width: 469pt">
        No. of clients issued with preventive ARVs
      </td>
      <td colspan="2" class="xl220" width="178" style="border-right: 1.0pt solid black;
                border-left: none; width: 134pt">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td rowspan="2" height="40" class="xl138" width="29" style="height: 30.0pt; border-top: none;
                width: 22pt">
        10
      </td>
      <td colspan="4" rowspan="2" class="xl244" width="356" style="width: 268pt">
        No. of infants tested for HIV
      </td>
      <td colspan="3" class="xl215" width="267" style="border-left: none; width: 201pt">
        At 6 wks
      </td>
      <td colspan="2" class="xl220" width="178" style="border-right: 1.0pt solid black;
                border-left: none; width: 134pt">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td colspan="3" height="20" class="xl215" width="267" style="height: 15.0pt; border-left: none;
                width: 201pt">
        After 3 Months
      </td>
      <td colspan="2" class="xl220" width="178" style="border-right: 1.0pt solid black;
                border-left: none; width: 134pt">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td rowspan="2" height="40" class="xl138" width="29" style="height: 30.0pt; border-top: none;
                width: 22pt">
        11
      </td>
      <td colspan="4" rowspan="2" class="xl244" width="356" style="width: 268pt">
        HIV+ referred for follow up
      </td>
      <td colspan="3" class="xl215" width="267" style="border-left: none; width: 201pt">
        Mothers
      </td>
      <td colspan="2" class="xl220" width="178" style="border-right: 1.0pt solid black;
                border-left: none; width: 134pt">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td colspan="3" height="20" class="xl215" width="267" style="height: 15.0pt; border-left: none;
                width: 201pt">
        Partners
      </td>
      <td colspan="2" class="xl220" width="178" style="border-right: 1.0pt solid black;
                border-left: none; width: 134pt">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td height="20" class="xl138" width="29" style="height: 15.0pt; border-top: none;
                width: 22pt">
        12
      </td>
      <td colspan="7" class="xl215" width="623" style="border-left: none; width: 469pt">
        No. of infants issued with preventive ARVs
      </td>
      <td colspan="2" class="xl220" width="178" style="border-right: 1.0pt solid black;
                border-left: none; width: 134pt">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td height="20" class="xl138" width="29" style="height: 15.0pt; border-top: none;
                width: 22pt">
        13
      </td>
      <td colspan="7" class="xl215" width="623" style="border-left: none; width: 469pt">
        No. of mothers counselled on infant feeding options
      </td>
      <td colspan="2" class="xl220" width="178" style="border-right: 1.0pt solid black;
                border-left: none; width: 134pt">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td rowspan="3" height="61" class="xl138" width="29" style="border-bottom: 1.0pt solid black;
                height: 45.75pt; border-top: none; width: 22pt">
        14
      </td>
      <td colspan="4" rowspan="3" class="xl244" width="356" style="border-bottom: 1.0pt solid black;
                width: 268pt">
        No. of partners
      </td>
      <td colspan="3" class="xl215" width="267" style="border-left: none; width: 201pt">
        Counselled
      </td>
      <td colspan="2" class="xl220" width="178" style="border-right: 1.0pt solid black;
                border-left: none; width: 134pt">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td colspan="3" height="20" class="xl215" width="267" style="height: 15.0pt; border-left: none;
                width: 201pt">
        Tested
      </td>
      <td colspan="2" class="xl220" width="178" style="border-right: 1.0pt solid black;
                border-left: none; width: 134pt">
        0
      </td>
    </tr>
    <tr height="21" style="height: 15.75pt">
      <td colspan="3" height="21" class="xl228" width="267" style="height: 15.75pt; border-left: none;
                width: 201pt">
        HIV+
      </td>
      <td colspan="2" class="xl279" width="178" style="border-right: 1.0pt solid black;
                border-left: none; width: 134pt">
        0
      </td>
    </tr>
    <tr height="21" style="height: 15.75pt">
      <td height="21" class="xl122" width="29" style="height: 15.75pt; width: 22pt">
        &#160;
      </td>
      <td class="style2">
      </td>
      <td class="xl122" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl139" width="89" style="width: 67pt">
      </td>
      <td class="xl122" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl139" width="89" style="width: 67pt">
      </td>
      <td class="xl122" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl139" width="89" style="width: 67pt">
      </td>
      <td class="xl122" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl139" width="89" style="width: 67pt">
      </td>
    </tr>
  </xsl:template>

  <xsl:template name="MATERNITY_PMTCT">
    <tr height="20" style="height: 15.0pt">
      <td colspan="8" height="20" class="xl223" width="652" style="height: 15.0pt; width: 491pt">
        C: MATERNITY- PMCT
      </td>
      <td colspan="2" class="xl276" width="178" style="border-right: 1.0pt solid black;
                border-left: none; width: 134pt">
        TOTAL
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td height="20" class="xl140" width="29" style="height: 15.0pt; border-top: none;
                width: 22pt">
        1
      </td>
      <td colspan="7" class="xl215" width="623" style="border-left: none; width: 469pt">
        No of Women counselled
      </td>
      <td colspan="2" class="xl220" width="178" style="border-right: 1.0pt solid black;
                border-left: none; width: 134pt">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td height="20" class="xl140" width="29" style="height: 15.0pt; border-top: none;
                width: 22pt">
        2
      </td>
      <td colspan="7" class="xl215" width="623" style="border-left: none; width: 469pt">
        Women tested for HIV
      </td>
      <td colspan="2" class="xl220" width="178" style="border-right: 1.0pt solid black;
                border-left: none; width: 134pt">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td height="20" class="xl140" width="29" style="height: 15.0pt; border-top: none;
                width: 22pt">
        3
      </td>
      <td colspan="7" class="xl215" width="623" style="border-left: none; width: 469pt">
        Women found HIV +ve
      </td>
      <td colspan="2" class="xl220" width="178" style="border-right: 1.0pt solid black;
                border-left: none; width: 134pt">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td height="20" class="xl140" width="29" style="height: 15.0pt; border-top: none;
                width: 22pt">
        4
      </td>
      <td colspan="7" class="xl215" width="623" style="border-left: none; width: 469pt">
        No. of Women issued with preventive ARVs
      </td>
      <td colspan="2" class="xl220" width="178" style="border-right: 1.0pt solid black;
                border-left: none; width: 134pt">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td height="20" class="xl140" width="29" style="height: 15.0pt; border-top: none;
                width: 22pt">
        5
      </td>
      <td colspan="7" class="xl215" width="623" style="border-left: none; width: 469pt">
        No. of infant Preventive ARVs administered
      </td>
      <td colspan="2" class="xl220" width="178" style="border-right: 1.0pt solid black;
                border-left: none; width: 134pt">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td height="20" class="xl140" width="29" style="height: 15.0pt; border-top: none;
                width: 22pt">
        6
      </td>
      <td colspan="7" class="xl215" width="623" style="border-left: none; width: 469pt">
        Total Deliveries from HIV +ve women
      </td>
      <td colspan="2" class="xl220" width="178" style="border-right: 1.0pt solid black;
                border-left: none; width: 134pt">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td rowspan="2" height="41" class="xl140" width="29" style="border-bottom: 1.0pt solid black;
                height: 30.75pt; border-top: none; width: 22pt">
        7
      </td>
      <td colspan="5" rowspan="2" class="xl215" width="445" style="border-bottom: 1.0pt solid black;
                width: 335pt">
        No initiated on cotrimoxaxole
      </td>
      <td colspan="2" class="xl215" width="178" style="border-left: none; width: 134pt">
        Women
      </td>
      <td colspan="2" class="xl220" width="178" style="border-right: 1.0pt solid black;
                border-left: none; width: 134pt">
        0
      </td>
    </tr>
    <tr height="21" style="height: 15.75pt">
      <td colspan="2" height="21" class="xl228" width="178" style="height: 15.75pt; border-left: none;
                width: 134pt">
        Infants
      </td>
      <td colspan="2" class="xl279" width="178" style="border-right: 1.0pt solid black;
                border-left: none; width: 134pt">
        0
      </td>
    </tr>
    <tr height="21" style="height: 15.75pt">
      <td height="21" class="xl122" width="29" style="height: 15.75pt; width: 22pt">
        &#160;
      </td>
      <td class="style2">
        &#160;
      </td>
      <td class="xl122" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl122" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl122" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl122" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl122" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl122" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl122" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl122" width="89" style="width: 67pt">
        &#160;
      </td>
    </tr>
  </xsl:template>

  <xsl:template name="STI">
    <tr height="21" style="height: 15.75pt">
      <td rowspan="3" height="63" class="xl140" width="29" style="height: 47.25pt; border-top: none;
                width: 22pt">
        1
      </td>
      <td colspan="4" rowspan="3" class="xl244" width="356" style="width: 268pt">
        Urethral Discharge<span style="mso-spacerun: yes"> </span>
      </td>
      <td colspan="2" class="xl215" width="178" style="border-left: none; width: 134pt">
        Initial visit
      </td>
      <td class="xl144" width="89" style="border-top: none; border-left: none; width: 67pt">
        &#160;
      </td>
      <td class="xl106" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl145" width="89" style="width: 67pt">
        0
      </td>
    </tr>
    <tr height="21" style="height: 15.75pt">
      <td colspan="2" height="21" class="xl215" width="178" style="height: 15.75pt; border-left: none;
                width: 134pt">
        Re-att
      </td>
      <td class="xl144" width="89" style="border-top: none; border-left: none; width: 67pt">
        &#160;
      </td>
      <td class="xl106" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl145" width="89" style="border-top: none; width: 67pt">
        0
      </td>
    </tr>
    <tr height="21" style="height: 15.75pt">
      <td colspan="2" height="21" class="xl215" width="178" style="height: 15.75pt; border-left: none;
                width: 134pt">
        Referrals<span style="mso-spacerun: yes"> </span>
      </td>
      <td class="xl144" width="89" style="border-top: none; border-left: none; width: 67pt">
        &#160;
      </td>
      <td class="xl106" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl145" width="89" style="border-top: none; width: 67pt">
        0
      </td>
    </tr>
    <tr height="21" style="height: 15.75pt">
      <td rowspan="3" height="63" class="xl140" width="29" style="height: 47.25pt; border-top: none;
                width: 22pt">
        2
      </td>
      <td colspan="4" rowspan="3" class="xl244" width="356" style="width: 268pt">
        Cases of Genital ulcer disease (GUD)<span style="mso-spacerun: yes"> </span>
      </td>
      <td colspan="2" class="xl215" width="178" style="border-left: none; width: 134pt">
        Initial visit
      </td>
      <td class="xl116" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl106" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl145" width="89" style="border-top: none; width: 67pt">
        0
      </td>
    </tr>
    <tr height="21" style="height: 15.75pt">
      <td colspan="2" height="21" class="xl215" width="178" style="height: 15.75pt; border-left: none;
                width: 134pt">
        Re-att
      </td>
      <td class="xl116" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl106" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl145" width="89" style="border-top: none; width: 67pt">
        0
      </td>
    </tr>
    <tr height="21" style="height: 15.75pt">
      <td colspan="2" height="21" class="xl215" width="178" style="height: 15.75pt; border-left: none;
                width: 134pt">
        Referrals<span style="mso-spacerun: yes"> </span>
      </td>
      <td class="xl116" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl106" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl145" width="89" style="border-top: none; width: 67pt">
        0
      </td>
    </tr>
    <tr height="21" style="height: 15.75pt">
      <td rowspan="3" height="63" class="xl140" width="29" style="height: 47.25pt; border-top: none;
                width: 22pt">
        3
      </td>
      <td colspan="4" rowspan="3" class="xl244" width="356" style="width: 268pt">
        Cases of Ophthalmia Neonatorum<span style="mso-spacerun: yes"> </span>
      </td>
      <td colspan="2" class="xl215" width="178" style="border-left: none; width: 134pt">
        Initial visit
      </td>
      <td class="xl116" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl106" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl145" width="89" style="border-top: none; width: 67pt">
        0
      </td>
    </tr>
    <tr height="21" style="height: 15.75pt">
      <td colspan="2" height="21" class="xl215" width="178" style="height: 15.75pt; border-left: none;
                width: 134pt">
        Re-att
      </td>
      <td class="xl116" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl106" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl145" width="89" style="border-top: none; width: 67pt">
        0
      </td>
    </tr>
    <tr height="21" style="height: 15.75pt">
      <td colspan="2" height="21" class="xl215" width="178" style="height: 15.75pt; border-left: none;
                width: 134pt">
        Referrals<span style="mso-spacerun: yes"> </span>
      </td>
      <td class="xl116" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl106" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl145" width="89" style="border-top: none; width: 67pt">
        0
      </td>
    </tr>
    <tr height="21" style="height: 15.75pt">
      <td height="21" class="xl140" width="29" style="height: 15.75pt; border-top: none;
                width: 22pt">
        4
      </td>
      <td colspan="6" class="xl215" width="534" style="border-left: none; width: 402pt">
        Cases of Syphilis Serology
      </td>
      <td class="xl107" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl108" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl145" width="89" style="border-top: none; width: 67pt">
        0
      </td>
    </tr>
    <tr height="21" style="height: 15.75pt">
      <td height="21" class="xl141" width="29" style="height: 15.75pt; border-top: none;
                width: 22pt">
        5
      </td>
      <td colspan="6" class="xl358" width="534" style="border-left: none; width: 402pt">
        Grand Totals
      </td>
      <td class="xl146" width="89" style="width: 67pt">
        0
      </td>
      <td class="xl145" width="89" style="border-left: none; width: 67pt">
        0
      </td>
      <td class="xl145" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
    </tr>
    <tr height="21" style="height: 15.75pt">
      <td height="21" class="xl122" width="29" style="height: 15.75pt; width: 22pt">
        &#160;
      </td>
      <td class="style2">
      </td>
      <td class="xl122" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl139" width="89" style="width: 67pt">
      </td>
      <td class="xl122" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl139" width="89" style="width: 67pt">
      </td>
      <td class="xl122" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl139" width="89" style="width: 67pt">
      </td>
      <td class="xl122" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl147" width="89" style="border-top: none; width: 67pt">
        &#160;
      </td>
    </tr>
  </xsl:template>

  <xsl:template name="MATERNITY_SAFE_DELIVERIES">
    <tr height="21" style="height: 15.75pt">
      <td colspan="8" height="21" class="xl249" style="border-right: .5pt solid black;
                height: 15.75pt">
        E. MATERNITY / SAFE DELIVERIES
      </td>
      <td colspan="2" class="xl245" style="border-right: 2.0pt double black; border-left: none">
        Number
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td height="20" class="xl148" style="height: 15.0pt; border-top: none">
        1
      </td>
      <td colspan="7" class="xl270" style="border-left: none">
        Normal Deliveries
      </td>
      <td colspan="2" class="xl247" style="border-right: 2.0pt double black; border-left: none">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td height="20" class="xl148" style="height: 15.0pt; border-top: none">
        2
      </td>
      <td colspan="7" class="xl270" style="border-left: none">
        Caesarean Sections
      </td>
      <td colspan="2" class="xl247" style="border-right: 2.0pt double black; border-left: none">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td height="20" class="xl148" style="height: 15.0pt; border-top: none">
        3
      </td>
      <td colspan="7" class="xl270" style="border-left: none">
        Breech Delivery
      </td>
      <td colspan="2" class="xl247" style="border-right: 2.0pt double black; border-left: none">
        0
      </td>
    </tr>
    <tr height="21" style="height: 15.75pt">
      <td height="21" class="xl148" style="height: 15.75pt; border-top: none">
        4
      </td>
      <td colspan="7" class="xl270" style="border-left: none">
        Assisted vaginal delivery
      </td>
      <td colspan="2" class="xl254" style="border-right: 2.0pt double black; border-left: none">
        0
      </td>
    </tr>
    <tr height="21" style="height: 15.75pt">
      <td height="21" class="xl148" style="height: 15.75pt; border-top: none">
        5
      </td>
      <td colspan="7" class="xl328" style="border-left: none">
        Total Deliveries
      </td>
      <td colspan="2" class="xl252" style="border-right: 1.0pt solid black">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td height="20" class="xl148" style="height: 15.0pt; border-top: none">
        6
      </td>
      <td colspan="7" class="xl215" width="623" style="border-left: none; width: 469pt">
        Live Births
      </td>
      <td colspan="2" class="xl265" style="border-right: 2.0pt double black; border-left: none">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td height="20" class="xl148" style="height: 15.0pt; border-top: none">
        7
      </td>
      <td colspan="7" class="xl270" style="border-left: none">
        Still Births<span style="mso-spacerun: yes"> </span>
      </td>
      <td colspan="2" class="xl247" style="border-right: 2.0pt double black; border-left: none">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td height="20" class="xl148" style="height: 15.0pt; border-top: none">
        8
      </td>
      <td colspan="7" class="xl215" width="623" style="border-left: none; width: 469pt">
        Under Weight Babies
        <br />
        ( Weight below 2500 grams)<span style="mso-spacerun: yes"> </span>
      </td>
      <td colspan="2" class="xl247" style="border-right: 2.0pt double black; border-left: none">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td height="20" class="xl148" style="height: 15.0pt; border-top: none">
        9
      </td>
      <td colspan="7" class="xl215" width="623" style="border-left: none; width: 469pt">
        Pre-Term babies
      </td>
      <td colspan="2" class="xl247" style="border-right: 2.0pt double black; border-left: none">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td height="20" class="xl148" style="height: 15.0pt; border-top: none">
        10
      </td>
      <td colspan="7" class="xl270" style="border-left: none">
        No. of babies discharged alive
      </td>
      <td colspan="2" class="xl247" style="border-right: 2.0pt double black; border-left: none">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td height="20" class="xl148" style="height: 15.0pt; border-top: none">
        11
      </td>
      <td colspan="7" class="xl215" width="623" style="border-left: none; width: 469pt">
        Referrals
      </td>
      <td colspan="2" class="xl247" style="border-right: 2.0pt double black; border-left: none">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td height="20" class="xl148" style="height: 15.0pt; border-top: none">
        12
      </td>
      <td colspan="7" class="xl270" style="border-left: none">
        Neonatal Deaths
      </td>
      <td colspan="2" class="xl247" style="border-right: 2.0pt double black; border-left: none">
        0
      </td>
    </tr>
    <tr height="21" style="height: 15.75pt">
      <td height="21" class="xl148" style="height: 15.75pt; border-top: none">
        13
      </td>
      <td colspan="7" class="xl270" style="border-left: none">
        Maternal Deaths
      </td>
      <td colspan="2" class="xl254" style="border-right: 2.0pt double black; border-left: none">
        0
      </td>
    </tr>
    <tr height="21" style="height: 15.75pt">
      <td height="21" class="xl148" style="height: 15.75pt; border-top: none">
        &#160;
      </td>
      <td colspan="5" class="xl336" style="border-left: none">
        Maternal complications
      </td>
      <td colspan="2" class="xl272">
        Alive&#160;
      </td>
      <td colspan="2" class="xl264" width="178" style="border-left: none; width: 134pt">
        Deaths
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td height="20" class="xl148" style="height: 15.0pt; border-top: none">
        14
      </td>
      <td colspan="5" class="xl270" style="border-left: none">
        A.P.H. (Ante Partum Haemorrhage)
      </td>
      <td colspan="2" class="xl247" style="border-left: none">
        0
      </td>
      <td colspan="2" class="xl118" width="178" style="border-right: 2.0pt double black;
                border-left: none; width: 134pt">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td height="20" class="xl148" style="height: 15.0pt; border-top: none">
        15
      </td>
      <td colspan="5" class="xl270" style="border-left: none">
        P.P.H. (Post Partum Haemorrhage)
      </td>
      <td colspan="2" class="xl247" style="border-left: none">
        0
      </td>
      <td colspan="2" class="xl118" width="178" style="border-right: 2.0pt double black;
                border-left: none; width: 134pt">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td height="20" class="xl148" style="height: 15.0pt; border-top: none">
        16
      </td>
      <td colspan="5" class="xl270" style="border-left: none">
        Eclampsia
      </td>
      <td colspan="2" class="xl247" style="border-left: none">
        0
      </td>
      <td colspan="2" class="xl118" width="178" style="border-right: 2.0pt double black;
                border-left: none; width: 134pt">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td height="20" class="xl148" style="height: 15.0pt; border-top: none">
        17
      </td>
      <td colspan="5" class="xl270" style="border-left: none">
        Ruptured Uterus
      </td>
      <td colspan="2" class="xl247" style="border-left: none">
        0
      </td>
      <td colspan="2" class="xl118" width="178" style="border-right: 2.0pt double black;
                border-left: none; width: 134pt">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td height="20" class="xl148" style="height: 15.0pt; border-top: none">
        18
      </td>
      <td colspan="5" class="xl270" style="border-left: none">
        Obstructed labour
      </td>
      <td colspan="2" class="xl247" style="border-left: none">
        0
      </td>
      <td colspan="2" class="xl118" width="178" style="border-right: 2.0pt double black;
                border-left: none; width: 134pt">
        0
      </td>
    </tr>
    <tr height="21" style="height: 15.75pt">
      <td height="21" class="xl149" style="height: 15.75pt; border-top: none">
        19
      </td>
      <td colspan="5" class="xl271" style="border-left: none">
        Sepsis
      </td>
      <td colspan="2" class="xl267" style="border-left: none">
        0
      </td>
      <td colspan="2" class="xl268" width="178" style="border-right: 2.0pt double black;
                border-left: none; width: 134pt">
        0
      </td>
    </tr>
    <tr height="22" style="page-break-before: always; height: 16.5pt">
      <td height="22" class="xl122" width="29" style="height: 16.5pt; width: 22pt">
        &#160;
      </td>
      <td class="style2">
      </td>
      <td class="xl122" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl139" width="89" style="width: 67pt">
      </td>
      <td class="xl122" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl139" width="89" style="width: 67pt">
      </td>
      <td class="xl122" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl139" width="89" style="width: 67pt">
      </td>
      <td class="xl122" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl139" width="89" style="width: 67pt">
      </td>
    </tr>
  </xsl:template>

  <xsl:template name="PAC_SERVICES">
    <tr height="21" style="height: 15.75pt">
      <td colspan="8" height="21" class="xl230" width="652" style="height: 15.75pt; width: 491pt">
        F: PAC SERVICES
      </td>
      <td colspan="2" class="xl330" width="178" style="border-right: 2.0pt double black;
                border-left: none; width: 134pt">
        TOTAL
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td height="20" class="xl150" width="29" style="height: 15.0pt; border-top: none;
                width: 22pt">
        1
      </td>
      <td colspan="7" class="xl215" width="623" style="border-left: none; width: 469pt">
        No. of MVA
      </td>
      <td colspan="2" class="xl332" width="178" style="border-right: 2.0pt double black;
                border-left: none; width: 134pt">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td height="20" class="xl150" width="29" style="height: 15.0pt; border-top: none;
                width: 22pt">
        2
      </td>
      <td colspan="7" class="xl215" width="623" style="border-left: none; width: 469pt">
        No.<span style="mso-spacerun: yes"> </span>of D &amp; C
      </td>
      <td colspan="2" class="xl332" width="178" style="border-right: 2.0pt double black;
                border-left: none; width: 134pt">
        0
      </td>
    </tr>
    <tr height="21" style="height: 15.75pt">
      <td height="21" class="xl151" width="29" style="height: 15.75pt; border-top: none;
                width: 22pt">
        3
      </td>
      <td colspan="7" class="xl334" width="623" style="border-left: none; width: 469pt">
        No. of FP Up take
      </td>
      <td colspan="2" class="xl338" width="178" style="border-right: 2.0pt double black;
                border-left: none; width: 134pt">
        0
      </td>
    </tr>
    <tr height="22" style="height: 16.5pt">
      <td height="22" class="xl122" width="29" style="height: 16.5pt; width: 22pt">
        &#160;
      </td>
      <td class="style2">
      </td>
      <td class="xl122" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl139" width="89" style="width: 67pt">
      </td>
      <td class="xl122" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl139" width="89" style="width: 67pt">
      </td>
      <td class="xl122" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl139" width="89" style="width: 67pt">
      </td>
      <td class="xl122" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl139" width="89" style="width: 67pt">
      </td>
    </tr>
  </xsl:template>

  <xsl:template name="TB">
    <tr height="20" style="height: 15.0pt">
      <td colspan="4" rowspan="2" height="41" class="xl367" style="border-bottom: .5pt solid black;
                height: 30.75pt">
        G: TB<span style="mso-spacerun: yes"> </span>
      </td>
      <td rowspan="2" class="xl375">
        New<span style="mso-spacerun: yes"> </span>
      </td>
      <td rowspan="2" class="xl259">
        Reattendant
      </td>
      <td colspan="2" class="xl260">
        Children 0 - 14 yrs
      </td>
      <td colspan="2" class="xl261" style="border-right: 1.0pt solid black; border-left: none">
        Adults &gt; 14 yrs
      </td>
    </tr>
    <tr height="21" style="height: 15.75pt">
      <td height="21" class="xl152" style="height: 15.75pt">
        F
      </td>
      <td class="xl153" style="border-left: none">
        M
      </td>
      <td class="xl153" style="border-left: none">
        F
      </td>
      <td class="xl154" style="border-left: none">
        M
      </td>
    </tr>
    <tr height="21" style="mso-height-source: userset; height: 15.75pt">
      <td height="21" class="xl155" style="height: 15.75pt; border-top: none">
        1
      </td>
      <td colspan="3" class="xl215" width="267" style="border-left: none; width: 201pt">
        No. of TB cases detected
      </td>
      <td class="xl109" style="border-top: none">
        0
      </td>
      <td class="xl156" style="border-top: none; border-left: none">
        &#160;
      </td>
      <td class="xl110" style="border-top: none">
        0
      </td>
      <td class="xl117" style="border-top: none; border-left: none">
        0
      </td>
      <td class="xl117" style="border-top: none; border-left: none">
        0
      </td>
      <td class="xl111" style="border-top: none; border-left: none">
        0
      </td>
    </tr>
    <tr height="21" style="mso-height-source: userset; height: 15.75pt">
      <td height="21" class="xl155" style="height: 15.75pt; border-top: none">
        2
      </td>
      <td colspan="3" class="xl215" width="267" style="border-left: none; width: 201pt">
        No. of smear positive<span style="mso-spacerun: yes"> </span>
      </td>
      <td class="xl109" style="border-top: none">
        0
      </td>
      <td class="xl112" style="border-top: none; border-left: none">
        0
      </td>
      <td class="xl110" style="border-top: none">
        0
      </td>
      <td class="xl117" style="border-top: none; border-left: none">
        0
      </td>
      <td class="xl117" style="border-top: none; border-left: none">
        0
      </td>
      <td class="xl111" style="border-top: none; border-left: none">
        0
      </td>
    </tr>
    <tr height="21" style="mso-height-source: userset; height: 15.75pt">
      <td height="21" class="xl155" style="height: 15.75pt; border-top: none">
        3
      </td>
      <td colspan="3" class="xl215" width="267" style="border-left: none; width: 201pt">
        No. of smear<span style="mso-spacerun: yes"> </span>negatives
      </td>
      <td class="xl109" style="border-top: none">
        0
      </td>
      <td class="xl112" style="border-top: none; border-left: none">
        0
      </td>
      <td class="xl110" style="border-top: none">
        0
      </td>
      <td class="xl117" style="border-top: none; border-left: none">
        0
      </td>
      <td class="xl117" style="border-top: none; border-left: none">
        0
      </td>
      <td class="xl111" style="border-top: none; border-left: none">
        0
      </td>
    </tr>
    <tr height="21" style="mso-height-source: userset; height: 15.75pt">
      <td height="21" class="xl155" style="height: 15.75pt; border-top: none">
        4
      </td>
      <td colspan="3" class="xl215" width="267" style="border-left: none; width: 201pt">
        No. of Extrapulmonary TB patients on treatment
      </td>
      <td class="xl109" style="border-top: none">
        0
      </td>
      <td class="xl112" style="border-top: none; border-left: none">
        0
      </td>
      <td class="xl110" style="border-top: none">
        0
      </td>
      <td class="xl117" style="border-top: none; border-left: none">
        0
      </td>
      <td class="xl117" style="border-top: none; border-left: none">
        0
      </td>
      <td class="xl111" style="border-top: none; border-left: none">
        0
      </td>
    </tr>
    <tr height="21" style="mso-height-source: userset; height: 15.75pt">
      <td height="21" class="xl155" style="height: 15.75pt; border-top: none">
        5
      </td>
      <td colspan="3" class="xl215" width="267" style="border-left: none; width: 201pt">
        Total No. of TB patients on Re-treatment
      </td>
      <td class="xl109" style="border-top: none">
        0
      </td>
      <td class="xl112" style="border-top: none; border-left: none">
        0
      </td>
      <td class="xl110" style="border-top: none">
        0
      </td>
      <td class="xl117" style="border-top: none; border-left: none">
        0
      </td>
      <td class="xl117" style="border-top: none; border-left: none">
        0
      </td>
      <td class="xl111" style="border-top: none; border-left: none">
        0
      </td>
    </tr>
    <tr height="21" style="mso-height-source: userset; height: 15.75pt">
      <td height="21" class="xl155" style="height: 15.75pt; border-top: none">
        6
      </td>
      <td colspan="3" class="xl215" width="267" style="border-left: none; width: 201pt">
        Total No.<span style="mso-spacerun: yes"> </span>of TB Patients tested for HIV<span
                    style="mso-spacerun: yes"> </span>
      </td>
      <td class="xl109" style="border-top: none">
        0
      </td>
      <td class="xl112" style="border-top: none; border-left: none">
        0
      </td>
      <td class="xl110" style="border-top: none">
        0
      </td>
      <td class="xl117" style="border-top: none; border-left: none">
        0
      </td>
      <td class="xl117" style="border-top: none; border-left: none">
        0
      </td>
      <td class="xl111" style="border-top: none; border-left: none">
        0
      </td>
    </tr>
    <tr height="21" style="mso-height-source: userset; height: 15.75pt">
      <td height="21" class="xl155" style="height: 15.75pt; border-top: none">
        7
      </td>
      <td colspan="3" class="xl215" width="267" style="border-left: none; width: 201pt">
        Total No.<span style="mso-spacerun: yes"> </span>of TB Patients HIV+ve
      </td>
      <td class="xl109" style="border-top: none">
        0
      </td>
      <td class="xl112" style="border-top: none; border-left: none">
        0
      </td>
      <td class="xl110" style="border-top: none">
        0
      </td>
      <td class="xl117" style="border-top: none; border-left: none">
        0
      </td>
      <td class="xl117" style="border-top: none; border-left: none">
        0
      </td>
      <td class="xl111" style="border-top: none; border-left: none">
        0
      </td>
    </tr>
    <tr height="21" style="mso-height-source: userset; height: 15.75pt">
      <td height="21" class="xl155" style="height: 15.75pt; border-top: none">
        8
      </td>
      <td colspan="3" class="xl215" width="267" style="border-left: none; width: 201pt">
        No. of TB HIVpatients on CPT
      </td>
      <td class="xl109" style="border-top: none">
        0
      </td>
      <td class="xl112" style="border-top: none; border-left: none">
        0
      </td>
      <td class="xl110" style="border-top: none">
        0
      </td>
      <td class="xl117" style="border-top: none; border-left: none">
        0
      </td>
      <td class="xl117" style="border-top: none; border-left: none">
        0
      </td>
      <td class="xl111" style="border-top: none; border-left: none">
        0
      </td>
    </tr>
    <tr height="21" style="mso-height-source: userset; height: 15.75pt">
      <td height="21" class="xl155" style="height: 15.75pt; border-top: none">
        9
      </td>
      <td colspan="3" class="xl228" width="267" style="border-left: none; width: 201pt">
        No. of defaulters<span style="mso-spacerun: yes"> </span>
      </td>
      <td class="xl109" style="border-top: none">
        0
      </td>
      <td class="xl112" style="border-top: none; border-left: none">
        0
      </td>
      <td class="xl110" style="border-top: none">
        0
      </td>
      <td class="xl117" style="border-top: none; border-left: none">
        0
      </td>
      <td class="xl117" style="border-top: none; border-left: none">
        0
      </td>
      <td class="xl111" style="border-top: none; border-left: none">
        0
      </td>
    </tr>
    <tr height="51" style="mso-height-source: userset; height: 38.25pt">
      <td height="51" class="xl155" style="height: 38.25pt; border-top: none">
        10
      </td>
      <td colspan="3" class="xl257" width="267" style="border-left: none; width: 201pt">
        The true No. completed treatment (all forms of TB) <font class="font23">
          <br />
          who started treatment this month last year
        </font>
      </td>
      <td class="xl109" style="border-top: none">
        0
      </td>
      <td class="xl112" style="border-top: none; border-left: none">
        0
      </td>
      <td class="xl110" style="border-top: none">
        0
      </td>
      <td class="xl117" style="border-top: none; border-left: none">
        0
      </td>
      <td class="xl117" style="border-top: none; border-left: none">
        0
      </td>
      <td class="xl111" style="border-top: none; border-left: none">
        0
      </td>
    </tr>
    <tr height="39" style="mso-height-source: userset; height: 29.25pt">
      <td height="39" class="xl158" style="height: 29.25pt; border-top: none">
        11
      </td>
      <td colspan="3" class="xl257" width="267" style="border-left: none; width: 201pt">
        No. of TB deaths
        <br />
        <font class="font23">who started treatment this month last year</font>
      </td>
      <td class="xl109" style="border-top: none">
        0
      </td>
      <td class="xl112" style="border-top: none; border-left: none">
        0
      </td>
      <td class="xl113" style="border-top: none">
        0
      </td>
      <td class="xl95" style="border-top: none; border-left: none">
        0
      </td>
      <td class="xl95" style="border-top: none; border-left: none">
        0
      </td>
      <td class="xl114" style="border-top: none; border-left: none">
        0
      </td>
    </tr>
    <tr height="21" style="height: 15.75pt">
      <td height="21" class="xl122" width="29" style="height: 15.75pt; width: 22pt">
        &#160;
      </td>
      <td class="style2">
      </td>
      <td class="xl122" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl139" width="89" style="width: 67pt">
      </td>
      <td class="xl122" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl139" width="89" style="width: 67pt">
      </td>
      <td class="xl122" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl139" width="89" style="width: 67pt">
      </td>
      <td class="xl122" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl139" width="89" style="width: 67pt">
      </td>
    </tr>
  </xsl:template>


  <xsl:template name="VCT">
    <tr height="20" style="height: 15.0pt">
      <td colspan="5" rowspan="2" height="40" class="xl293" width="385" style="border-right: 1.0pt solid black;
                height: 30.0pt; width: 290pt">
        H: VCT
      </td>
      <td colspan="2" class="xl344" width="178" style="border-right: 1.0pt solid black;
                width: 134pt">
        15-24 years
      </td>
      <td colspan="2" class="xl239" width="178" style="border-right: 1.0pt solid black;
                border-left: none; width: 134pt">
        &#8805; 25 years
      </td>
      <td rowspan="2" class="xl371" width="89" style="border-bottom: 1.0pt solid black;
                width: 67pt">
        Total
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td height="20" class="xl159" width="89" style="height: 15.0pt; width: 67pt">
        F
      </td>
      <td class="xl159" width="89" style="width: 67pt">
        M
      </td>
      <td class="xl159" width="89" style="width: 67pt">
        F
      </td>
      <td class="xl159" width="89" style="width: 67pt">
        M
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td rowspan="3" height="60" class="xl241" width="29" style="border-bottom: 1.0pt solid black;
                height: 45.0pt; width: 22pt">
        1
      </td>
      <td colspan="2" rowspan="3" class="xl244" width="178" style="width: 134pt">
        VCT Clients
      </td>
      <td colspan="2" class="xl215" width="178" style="border-left: none; width: 134pt">
        Counselled
      </td>
      <td class="xl88" width="89" style="width: 67pt">
        0
      </td>
      <td class="xl88" width="89" style="width: 67pt">
        0
      </td>
      <td class="xl88" width="89" style="width: 67pt">
        0
      </td>
      <td class="xl88" width="89" style="width: 67pt">
        0
      </td>
      <td class="xl160" width="89" style="width: 67pt">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td colspan="2" height="20" class="xl215" width="178" style="height: 15.0pt; border-left: none;
                width: 134pt">
        Tested
      </td>
      <td class="xl88" width="89" style="width: 67pt">
        0
      </td>
      <td class="xl88" width="89" style="width: 67pt">
        0
      </td>
      <td class="xl88" width="89" style="width: 67pt">
        0
      </td>
      <td class="xl88" width="89" style="width: 67pt">
        0
      </td>
      <td class="xl160" width="89" style="width: 67pt">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td colspan="2" height="20" class="xl215" width="178" style="height: 15.0pt; border-left: none;
                width: 134pt">
        HIV+
      </td>
      <td class="xl96" width="89" style="width: 67pt">
        0
      </td>
      <td class="xl96" width="89" style="width: 67pt">
        0
      </td>
      <td class="xl96" width="89" style="width: 67pt">
        0
      </td>
      <td class="xl96" width="89" style="width: 67pt">
        0
      </td>
      <td class="xl160" width="89" style="width: 67pt">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td rowspan="4" height="80" class="xl241" width="29" style="border-bottom: 1.0pt solid black;
                height: 60.0pt; border-top: none; width: 22pt">
        2
      </td>
      <td colspan="2" rowspan="4" class="xl244" width="178" style="border-bottom: 1.0pt solid black;
                width: 134pt">
        No of couples
      </td>
      <td colspan="2" class="xl215" width="178" style="border-left: none; width: 134pt">
        Counselled
      </td>
      <td class="xl161" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl162" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl162" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl163" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl97" width="89" style="width: 67pt">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td colspan="2" height="20" class="xl215" width="178" style="height: 15.0pt; border-left: none;
                width: 134pt">
        Tested
      </td>
      <td class="xl164" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl165" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl165" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl166" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl97" width="89" style="width: 67pt">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td colspan="2" height="20" class="xl215" width="178" style="height: 15.0pt; border-left: none;
                width: 134pt">
        Both HIV+
      </td>
      <td class="xl164" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl165" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl165" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl166" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl97" width="89" style="width: 67pt">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td colspan="2" height="20" class="xl228" width="178" style="height: 15.0pt; border-left: none;
                width: 134pt">
        With discordant
      </td>
      <td class="xl167" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl168" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl168" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl169" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl97" width="89" style="width: 67pt">
        0
      </td>
    </tr>
    <tr height="21" style="height: 15.75pt">
      <td height="21" class="xl170" width="29" style="height: 15.75pt; width: 22pt">
      </td>
      <td class="style2">
        &#160;
      </td>
      <td class="xl120" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl120" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl120" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl171">
        &#160;
      </td>
      <td class="xl172">
        &#160;
      </td>
      <td class="xl172">
        &#160;
      </td>
      <td class="xl171">
        &#160;
      </td>
      <td class="xl173" width="89" style="width: 67pt">
        &#160;
      </td>
    </tr>
  </xsl:template>

  <xsl:template name="DTC">
    <tr height="19" style="height: 14.25pt">
      <td colspan="5" rowspan="3" height="59" class="xl293" width="385" style="border-right: 1.0pt solid black;
                border-bottom: 1.0pt solid black; height: 44.25pt; width: 290pt">
        I: DTC<span style="mso-spacerun: yes"> </span>
      </td>
      <td colspan="2" class="xl354" width="178" style="border-right: 1.0pt solid black;
                width: 134pt">
        Children
      </td>
      <td colspan="2" class="xl356" width="178" style="border-right: 1.0pt solid black;
                border-left: none; width: 134pt">
        Adults
      </td>
      <td rowspan="2" class="xl345" width="89" style="border-bottom: 1.0pt solid black;
                width: 67pt">
        Total
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td colspan="2" height="20" class="xl347" width="178" style="border-right: 1.0pt solid black;
                height: 15.0pt; width: 134pt">
        (0-14 years)
      </td>
      <td colspan="2" class="xl349" width="178" style="border-right: 1.0pt solid black;
                border-left: none; width: 134pt">
        ( &gt;14yrs)
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td height="20" class="xl174" width="89" style="height: 15.0pt; width: 67pt">
        F
      </td>
      <td class="xl174" width="89" style="width: 67pt">
        M
      </td>
      <td class="xl174" width="89" style="width: 67pt">
        F
      </td>
      <td class="xl174" width="89" style="width: 67pt">
        M
      </td>
      <td class="xl175" width="89" style="width: 67pt">
        &#160;
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td rowspan="2" height="40" class="xl360" width="29" style="border-bottom: 1.0pt solid black;
                height: 30.0pt; border-top: none; width: 22pt">
        1
      </td>
      <td colspan="2" rowspan="2" class="xl362" width="178" style="width: 134pt">
        No. counselled
      </td>
      <td colspan="2" class="xl350" width="178" style="border-left: none; width: 134pt">
        Outpatient
      </td>
      <td class="xl88" width="89" style="width: 67pt">
        0
      </td>
      <td class="xl88" width="89" style="width: 67pt">
        0
      </td>
      <td class="xl88" width="89" style="width: 67pt">
        0
      </td>
      <td class="xl88" width="89" style="width: 67pt">
        0
      </td>
      <td class="xl176" width="89" style="width: 67pt">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td colspan="2" height="20" class="xl350" width="178" style="height: 15.0pt; border-left: none;
                width: 134pt">
        In-Patient
      </td>
      <td class="xl88" width="89" style="width: 67pt">
        0
      </td>
      <td class="xl88" width="89" style="width: 67pt">
        0
      </td>
      <td class="xl88" width="89" style="width: 67pt">
        0
      </td>
      <td class="xl88" width="89" style="width: 67pt">
        0
      </td>
      <td class="xl176" width="89" style="width: 67pt">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td rowspan="2" height="40" class="xl360" width="29" style="border-bottom: 1.0pt solid black;
                height: 30.0pt; border-top: none; width: 22pt">
        2
      </td>
      <td colspan="2" rowspan="2" class="xl364" width="178" style="border-bottom: .5pt solid black;
                width: 134pt">
        No. tested
      </td>
      <td colspan="2" class="xl350" width="178" style="border-left: none; width: 134pt">
        Outpatient
      </td>
      <td class="xl88" width="89" style="width: 67pt">
        0
      </td>
      <td class="xl88" width="89" style="width: 67pt">
        0
      </td>
      <td class="xl88" width="89" style="width: 67pt">
        0
      </td>
      <td class="xl88" width="89" style="width: 67pt">
        0
      </td>
      <td class="xl176" width="89" style="width: 67pt">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td colspan="2" height="20" class="xl350" width="178" style="height: 15.0pt; border-left: none;
                width: 134pt">
        In-Patient
      </td>
      <td class="xl88" width="89" style="width: 67pt">
        0
      </td>
      <td class="xl88" width="89" style="width: 67pt">
        0
      </td>
      <td class="xl88" width="89" style="width: 67pt">
        0
      </td>
      <td class="xl88" width="89" style="width: 67pt">
        0
      </td>
      <td class="xl176" width="89" style="width: 67pt">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td rowspan="2" height="40" class="xl360" width="29" style="border-bottom: 1.0pt solid black;
                height: 30.0pt; border-top: none; width: 22pt">
        3
      </td>
      <td colspan="2" rowspan="2" class="xl363" width="178" style="border-bottom: 1.0pt solid black;
                width: 134pt">
        No. HIV+
      </td>
      <td colspan="2" class="xl350" width="178" style="border-left: none; width: 134pt">
        Outpatient
      </td>
      <td class="xl88" width="89" style="width: 67pt">
        0
      </td>
      <td class="xl88" width="89" style="width: 67pt">
        0
      </td>
      <td class="xl88" width="89" style="width: 67pt">
        0
      </td>
      <td class="xl88" width="89" style="width: 67pt">
        0
      </td>
      <td class="xl176" width="89" style="width: 67pt">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td colspan="2" height="20" class="xl350" width="178" style="height: 15.0pt; border-left: none;
                width: 134pt">
        In-Patient
      </td>
      <td class="xl88" width="89" style="width: 67pt">
        0
      </td>
      <td class="xl88" width="89" style="width: 67pt">
        0
      </td>
      <td class="xl88" width="89" style="width: 67pt">
        0
      </td>
      <td class="xl88" width="89" style="width: 67pt">
        0
      </td>
      <td class="xl176" width="89" style="width: 67pt">
        0
      </td>
    </tr>
    <tr height="21" style="height: 15.75pt">
      <td height="21" class="xl170" width="29" style="height: 15.75pt; width: 22pt">
      </td>
      <td class="style2">
        &#160;
      </td>
      <td class="xl120" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl120" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl120" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl171">
        &#160;
      </td>
      <td class="xl172">
        &#160;
      </td>
      <td class="xl172">
        &#160;
      </td>
      <td class="xl171">
        &#160;
      </td>
      <td class="xl173" width="89" style="width: 67pt">
        &#160;
      </td>
    </tr>
  </xsl:template>

  <xsl:template name="CHANIS">
    <tr height="19" style="height: 14.25pt">
      <td colspan="10" height="19" class="xl223" width="830" style="border-right: 1.0pt solid black;
                height: 14.25pt; width: 625pt">
        J: CHILD HEALTH AND NUTRITION INFORMATION SYSTEM (CHANIS)
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td colspan="7" height="20" class="xl226" width="563" style="height: 15.0pt; width: 424pt">
        Children Needing Follow up
      </td>
      <td class="xl177" width="89" style="border-top: none; border-left: none; width: 67pt">
        F
      </td>
      <td class="xl177" width="89" style="border-top: none; border-left: none; width: 67pt">
        M
      </td>
      <td class="xl178" width="89" style="border-top: none; border-left: none; width: 67pt">
        TOTAL
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td height="20" class="xl179" width="29" style="height: 15.0pt; border-top: none;
                width: 22pt">
        1
      </td>
      <td colspan="6" class="xl215" width="534" style="border-left: none; width: 402pt">
        <span style="mso-spacerun: yes"></span>Marasmus
      </td>
      <td class="xl118" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl118" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl180">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td height="20" class="xl179" width="29" style="height: 15.0pt; border-top: none;
                width: 22pt">
        2
      </td>
      <td colspan="6" class="xl215" width="534" style="border-left: none; width: 402pt">
        <span style="mso-spacerun: yes"></span>Kwashiorkor
      </td>
      <td class="xl118" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl118" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl180" style="border-top: none">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td height="20" class="xl179" width="29" style="height: 15.0pt; border-top: none;
                width: 22pt">
        3
      </td>
      <td colspan="6" class="xl215" width="534" style="border-left: none; width: 402pt">
        <span style="mso-spacerun: yes"></span>Anaemia
      </td>
      <td class="xl118" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl118" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl180" style="border-top: none">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td height="20" class="xl179" width="29" style="height: 15.0pt; border-top: none;
                width: 22pt">
        4
      </td>
      <td colspan="6" class="xl215" width="534" style="border-left: none; width: 402pt">
        <span style="mso-spacerun: yes"></span>Faltering Wt
      </td>
      <td class="xl118" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl118" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl180" style="border-top: none">
        0
      </td>
    </tr>
    <tr height="19" style="mso-height-source: userset; height: 14.25pt">
      <td colspan="4" height="19" class="xl317" width="296" style="height: 14.25pt; width: 223pt">
        <span style="mso-spacerun: yes"></span>Others e.g. Vitamin A deficiency, etc. (Specify):<span
                    style="mso-spacerun: yes"> </span>
      </td>
      <td colspan="3" class="xl319" width="267" style="width: 201pt">
        &#160;
      </td>
      <td class="xl118" width="89" style="border-top: none; width: 67pt">
        0
      </td>
      <td class="xl118" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl180" style="border-top: none">
        0
      </td>
    </tr>
    <tr height="19" style="mso-height-source: userset; height: 14.25pt">
      <td rowspan="3" height="59" class="xl206" width="29" style="height: 44.25pt; border-top: none;
                width: 22pt">
        5
      </td>
      <td colspan="2" rowspan="3" class="xl207" width="178" style="width: 134pt">
        Total Underweight
      </td>
      <td colspan="4" class="xl208" width="356" style="border-right: .5pt solid black;
                border-left: none; width: 268pt">
        0 -11 Months
      </td>
      <td class="xl106" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl106" width="89" style="border-top: none; width: 67pt">
        0
      </td>
      <td class="xl157" style="border-top: none">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td colspan="4" height="20" class="xl208" width="356" style="border-right: .5pt solid black;
                height: 15.0pt; border-left: none; width: 268pt">
        12 -35 Months
      </td>
      <td class="xl106" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl106" width="89" style="border-top: none; width: 67pt">
        0
      </td>
      <td class="xl157" style="border-top: none">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td colspan="4" height="20" class="xl208" width="356" style="border-right: .5pt solid black;
                height: 15.0pt; border-left: none; width: 268pt">
        36 -59 Months
      </td>
      <td class="xl106" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl106" width="89" style="border-top: none; width: 67pt">
        0
      </td>
      <td class="xl157" style="border-top: none">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td rowspan="3" height="60" class="xl206" width="29" style="height: 45.0pt; border-top: none;
                width: 22pt">
        6
      </td>
      <td colspan="2" rowspan="3" class="xl207" width="178" style="border-bottom: 1.0pt solid black;
                width: 134pt">
        Total Normal Weight
      </td>
      <td colspan="4" class="xl208" width="356" style="border-right: .5pt solid black;
                border-left: none; width: 268pt">
        0 -11 Months
      </td>
      <td class="xl106" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl106" width="89" style="border-top: none; width: 67pt">
        0
      </td>
      <td class="xl157" style="border-top: none">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td colspan="4" height="20" class="xl208" width="356" style="border-right: .5pt solid black;
                height: 15.0pt; border-left: none; width: 268pt">
        12 -35 Months
      </td>
      <td class="xl106" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl106" width="89" style="border-top: none; width: 67pt">
        0
      </td>
      <td class="xl157" style="border-top: none">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td colspan="4" height="20" class="xl212" width="356" style="border-right: .5pt solid black;
                height: 15.0pt; border-left: none; width: 268pt">
        36 -59 Months
      </td>
      <td class="xl106" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl106" width="89" style="border-top: none; width: 67pt">
        0
      </td>
      <td class="xl157" style="border-top: none">
        0
      </td>
    </tr>
    <tr height="21" style="height: 15.75pt">
      <td height="21" class="xl94" width="29" style="height: 15.75pt; width: 22pt">
      </td>
      <td class="style2">
        &#160;
      </td>
      <td class="xl91" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl91" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl91" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl91" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl91" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl92" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl66" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl67" width="89" style="width: 67pt">
        &#160;
      </td>
    </tr>
  </xsl:template>

  <xsl:template name="KART-1">
    <xsl:variable name="KART-1-PMTCT-F-014" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K1_PMTCT']/QueryResults/Row[Indicators/Result/text() = 'Female' and Indicators/Result/text() = '0-14']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="KART-1-PMTCT-M-014" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K1_PMTCT']/QueryResults/Row[Indicators/Result/text() = 'Male' and Indicators/Result/text() = '0-14']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="KART-1-PMTCT-F-15" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K1_PMTCT']/QueryResults/Row[Indicators/Result/text() = 'Female' and Indicators/Result/text() = 'Adult']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="KART-1-PMTCT-M-15" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K1_PMTCT']/QueryResults/Row[Indicators/Result/text() = 'Male' and Indicators/Result/text() = 'Adult']/Indicators[Name  = 'Total']/Result))" />

    <xsl:variable name="KART-1-VCT-F-014" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K1_VCT']/QueryResults/Row[Indicators/Result/text() = 'Female' and Indicators/Result/text() = '0-14']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="KART-1-VCT-M-014" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K1_VCT']/QueryResults/Row[Indicators/Result/text() = 'Male' and Indicators/Result/text() = '0-14']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="KART-1-VCT-F-15" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K1_VCT']/QueryResults/Row[Indicators/Result/text() = 'Female' and Indicators/Result/text() = 'Adult']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="KART-1-VCT-M-15" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K1_VCT']/QueryResults/Row[Indicators/Result/text() = 'Male' and Indicators/Result/text() = 'Adult']/Indicators[Name  = 'Total']/Result))" />

    <xsl:variable name="KART-1-TB-F-014" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K1_TB']/QueryResults/Row[Indicators/Result/text() = 'Female' and Indicators/Result/text() = '0-14']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="KART-1-TB-M-014" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K1_TB']/QueryResults/Row[Indicators/Result/text() = 'Male' and Indicators/Result/text() = '0-14']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="KART-1-TB-F-15" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K1_TB']/QueryResults/Row[Indicators/Result/text() = 'Female' and Indicators/Result/text() = 'Adult']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="KART-1-TB-M-15" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K1_TB']/QueryResults/Row[Indicators/Result/text() = 'Male' and Indicators/Result/text() = 'Adult']/Indicators[Name  = 'Total']/Result))" />

    <xsl:variable name="KART-1-IP-F-014" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K1_InPatient']/QueryResults/Row[Indicators/Result/text() = 'Female' and Indicators/Result/text() = '0-14']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="KART-1-IP-M-014" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K1_InPatient']/QueryResults/Row[Indicators/Result/text() = 'Male' and Indicators/Result/text() = '0-14']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="KART-1-IP-F-15" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K1_InPatient']/QueryResults/Row[Indicators/Result/text() = 'Female' and Indicators/Result/text() = 'Adult']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="KART-1-IP-M-15" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K1_InPatient']/QueryResults/Row[Indicators/Result/text() = 'Male' and Indicators/Result/text() = 'Adult']/Indicators[Name  = 'Total']/Result))" />

    <xsl:variable name="KART-1-CWC-F-014" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K1_CWC']/QueryResults/Row[Indicators/Result/text() = 'Female' and Indicators/Result/text() = '0-14']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="KART-1-CWC-M-014" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K1_CWC']/QueryResults/Row[Indicators/Result/text() = 'Male' and Indicators/Result/text() = '0-14']/Indicators[Name  = 'Total']/Result))" />

    <xsl:variable name="KART-1-OTHER-F-014" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K1_AllOthers']/QueryResults/Row[Indicators/Result/text() = 'Female' and Indicators/Result/text() = '0-14']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="KART-1-OTHER-M-014" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K1_AllOthers']/QueryResults/Row[Indicators/Result/text() = 'Male' and Indicators/Result/text() = '0-14']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="KART-1-OTHER-F-15" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K1_AllOthers']/QueryResults/Row[Indicators/Result/text() = 'Female' and Indicators/Result/text() = 'Adult']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="KART-1-OTHER-M-15" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K1_AllOthers']/QueryResults/Row[Indicators/Result/text() = 'Male' and Indicators/Result/text() = 'Adult']/Indicators[Name  = 'Total']/Result))" />    
    <tr height="20" style="height: 15.0pt">
      <td rowspan="7" height="162" class="xl98" width="29" style="height: 121.5pt; border-top: none;
                width: 22pt">
        1
      </td>
      <td rowspan="7" class="xl377" width="178" style="border-left: none; width: 134pt">
        Number of new patients enrolled within the month for HIV care by entry point
      </td>
      <td class="xl185" width="89" style="border-top: none; border-left: none; width: 67pt">
        PMCT clients
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-1-PMTCT-F-014"/>
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-1-PMTCT-M-014"/>
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-1-PMTCT-F-15"/>
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-1-PMTCT-M-15"/>
      </td>
      <td class="xl184" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-1-PMTCT-F-014)+number($KART-1-PMTCT-F-15)"/>
      </td>
      <td class="xl184" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-1-PMTCT-M-014)+number($KART-1-PMTCT-M-15)"/>
      </td>
      <td class="xl187" style="border-top: none">
        <xsl:value-of select="number($KART-1-PMTCT-F-014)+number($KART-1-PMTCT-M-014)+number($KART-1-PMTCT-F-15)+number($KART-1-PMTCT-M-15)"/>
      </td>
    </tr>
    <tr height="21" style="height: 15.75pt">
      <td height="21" class="xl185" width="89" style="height: 15.75pt; border-top: none;
                border-left: none; width: 67pt">
        VCT clients
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-1-VCT-F-014"/>
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-1-VCT-M-014"/>
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-1-VCT-F-15"/>
      </td>
      <td class="xl188" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-1-VCT-M-15"/>
      </td>
      <td class="xl184" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-1-VCT-F-014)+number($KART-1-VCT-F-15)"/>
      </td>
      <td class="xl184" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-1-VCT-M-014)+number($KART-1-VCT-M-15)"/>
      </td>
      <td class="xl187" style="border-top: none">
        <xsl:value-of select="number($KART-1-VCT-F-014)+number($KART-1-VCT-M-014)+number($KART-1-VCT-F-15)+number($KART-1-VCT-M-15)"/>
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td height="20" class="xl185" width="89" style="height: 15.0pt; border-top: none;
                border-left: none; width: 67pt">
        TB patients
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-1-TB-F-014"/>
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-1-TB-M-014"/>
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-1-TB-F-15"/>
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-1-TB-M-15"/>
      </td>
      <td class="xl184" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-1-TB-F-014)+number($KART-1-TB-F-15)"/>
      </td>
      <td class="xl184" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-1-TB-M-014)+number($KART-1-TB-M-15)"/>
      </td>
      <td class="xl187" style="border-top: none">
        <xsl:value-of select="number($KART-1-TB-F-014)+number($KART-1-TB-M-014)+number($KART-1-TB-F-15)+number($KART-1-TB-M-15)"/>
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td height="20" class="xl185" width="89" style="height: 15.0pt; border-top: none;
                border-left: none; width: 67pt">
        In patients
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-1-IP-F-014"/>
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-1-IP-M-014"/>
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-1-IP-F-15"/>
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-1-IP-M-15"/>
      </td>
      <td class="xl184" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-1-IP-F-014)+number($KART-1-IP-F-15)"/>
      </td>
      <td class="xl184" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-1-IP-M-014)+number($KART-1-IP-M-15)"/>
      </td>
      <td class="xl187" style="border-top: none">
        <xsl:value-of select="number($KART-1-IP-F-014)+number($KART-1-IP-M-014)+number($KART-1-IP-F-15)+number($KART-1-IP-M-15)"/>
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td height="20" class="xl185" width="89" style="height: 15.0pt; border-top: none;
                border-left: none; width: 67pt">
        CWC
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-1-CWC-F-014"/>
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-1-CWC-M-014"/>
      </td>
      <td class="xl189" width="89" style="border-top: none; border-left: none; width: 67pt">
        &#160;
      </td>
      <td class="xl189" width="89" style="border-top: none; border-left: none; width: 67pt">
        &#160;
      </td>
      <td class="xl184" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-1-CWC-F-014"/>
      </td>
      <td class="xl184" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-1-CWC-M-014"/>
      </td>
      <td class="xl187" style="border-top: none">
        <xsl:value-of select="number($KART-1-CWC-F-014)+number($KART-1-CWC-M-014)"/>
      </td>
    </tr>
    <tr height="41" style="height: 30.75pt">
      <td height="41" class="xl185" width="89" style="height: 30.75pt; border-top: none;
                border-left: none; width: 67pt">
        All others
      </td>
      <td class="xl190" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-1-OTHER-F-014"/>
      </td>
      <td class="xl190" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-1-OTHER-M-014"/>
      </td>
      <td class="xl190" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-1-OTHER-F-15"/>
      </td>
      <td class="xl190" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-1-OTHER-M-15"/>
      </td>
      <td class="xl191" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-1-OTHER-F-014)+number($KART-1-OTHER-F-15)"/>
      </td>
      <td class="xl191" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-1-OTHER-M-014)+number($KART-1-OTHER-M-15)"/>
      </td>
      <td class="xl192" style="border-top: none">
        <xsl:value-of select="number($KART-1-OTHER-F-014)+number($KART-1-OTHER-M-014)+number($KART-1-OTHER-F-15)+number($KART-1-OTHER-M-15)"/>
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td height="20" class="xl193" width="89" style="height: 15.0pt; border-top: none;
                border-left: none; width: 67pt">
        Sub-total
      </td>
      <td class="xl194" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-1-PMTCT-F-014)+number($KART-1-VCT-F-014)+number($KART-1-TB-F-014)+number($KART-1-IP-F-014)+number($KART-1-CWC-F-014)+number($KART-1-OTHER-F-014)"/>
      </td>
      <td class="xl194" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-1-PMTCT-M-014)+number($KART-1-VCT-M-014)+number($KART-1-TB-M-014)+number($KART-1-IP-M-014)+number($KART-1-CWC-F-014)+number($KART-1-OTHER-M-014)"/>
      </td>
      <td class="xl194" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-1-PMTCT-F-15)+number($KART-1-VCT-F-15)+number($KART-1-TB-F-15)+number($KART-1-IP-F-15)+number($KART-1-OTHER-F-15)"/>
      </td>
      <td class="xl194" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-1-PMTCT-M-15)+number($KART-1-VCT-M-15)+number($KART-1-TB-M-15)+number($KART-1-IP-M-15)+number($KART-1-OTHER-M-15)"/>
      </td>
      <td class="xl184" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-1-PMTCT-F-014)+number($KART-1-VCT-F-014)+number($KART-1-TB-F-014)+number($KART-1-IP-F-014)+number($KART-1-CWC-F-014)+number($KART-1-OTHER-F-014)+number($KART-1-PMTCT-F-15)+number($KART-1-VCT-F-15)+number($KART-1-TB-F-15)+number($KART-1-IP-F-15)+number($KART-1-OTHER-F-15)"/>
      </td>
      <td class="xl184" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-1-PMTCT-M-014)+number($KART-1-VCT-M-014)+number($KART-1-TB-M-014)+number($KART-1-IP-M-014)+number($KART-1-CWC-F-014)+number($KART-1-OTHER-M-014)+number($KART-1-PMTCT-M-15)+number($KART-1-VCT-M-15)+number($KART-1-TB-M-15)+number($KART-1-IP-M-15)+number($KART-1-OTHER-M-15)"/>
      </td>
      <td class="xl187" style="border-top: none">
        <xsl:value-of select="number($KART-1-PMTCT-F-014)+number($KART-1-VCT-F-014)+number($KART-1-TB-F-014)+number($KART-1-IP-F-014)+number($KART-1-CWC-F-014)+number($KART-1-OTHER-F-014)+number($KART-1-PMTCT-F-15)+number($KART-1-VCT-F-15)+number($KART-1-TB-F-15)+number($KART-1-IP-F-15)+number($KART-1-OTHER-F-15)+number($KART-1-PMTCT-M-014)+number($KART-1-VCT-M-014)+number($KART-1-TB-M-014)+number($KART-1-IP-M-014)+number($KART-1-CWC-F-014)+number($KART-1-OTHER-M-014)+number($KART-1-PMTCT-M-15)+number($KART-1-VCT-M-15)+number($KART-1-TB-M-15)+number($KART-1-IP-M-15)+number($KART-1-OTHER-M-15)"/>
      </td>
    </tr>
  </xsl:template>

  <xsl:template name="KART-2">
    <xsl:variable name="KART-2-F-014" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K2']/QueryResults/Row[Indicators/Result/text() = 'Female' and Indicators/Result/text() = '0-14']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="KART-2-M-014" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K2']/QueryResults/Row[Indicators/Result/text() = 'Male' and Indicators/Result/text() = '0-14']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="KART-2-F-15" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K2']/QueryResults/Row[Indicators/Result/text() = 'Female' and Indicators/Result/text() = 'Adult']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="KART-2-M-15" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K2']/QueryResults/Row[Indicators/Result/text() = 'Male' and Indicators/Result/text() = 'Adult']/Indicators[Name  = 'Total']/Result))" />

    <tr height="55" style="mso-height-source: userset; height: 41.25pt">
      <td height="55" class="xl98" width="29" style="height: 41.25pt; border-top: none;
                width: 22pt">
        2
      </td>
      <td colspan="2" class="xl185" width="178" style="border-left: none; width: 134pt">
        Cumulative No. of persons<span style="mso-spacerun: yes"> </span>enrolled in HIV
        care at this facility at end of the month
      </td>
      <td class="xl190" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-2-F-014"/>
      </td>
      <td class="xl190" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-2-M-014"/>
      </td>
      <td class="xl190" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-2-F-15"/>
      </td>
      <td class="xl190" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-2-M-15"/>
      </td>
      <td class="xl191" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-2-F-014) + number($KART-2-F-15) "/>
      </td>
      <td class="xl191" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-2-M-014) + number($KART-2-M-15) "/>
      </td>
      <td class="xl192" style="border-top: none">
        <xsl:value-of select="number($KART-2-F-014) + number($KART-2-F-15) + number($KART-2-M-014) + number($KART-2-M-15)"/>
      </td>
    </tr>
  </xsl:template>

  <xsl:template name="KART-2A-COTB">
    <xsl:variable name="KART-2A-COTB-F-014" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K2a']/QueryResults/Row[Indicators/Result/text() = 'Female' and Indicators/Result/text() = '0-14']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="KART-2A-COTB-M-014" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K2a']/QueryResults/Row[Indicators/Result/text() = 'Male' and Indicators/Result/text() = '0-14']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="KART-2A-COTB-F-15" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K2a']/QueryResults/Row[Indicators/Result/text() = 'Female' and Indicators/Result/text() = 'Adult']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="KART-2A-COTB-M-15" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K2a']/QueryResults/Row[Indicators/Result/text() = 'Male' and Indicators/Result/text() = 'Adult']/Indicators[Name  = 'Total']/Result))" />
    <tr height="36" style="mso-height-source: userset; height: 27.0pt">
      <td height="36" class="xl98" width="29" style="height: 27.0pt; border-top: none;
                width: 22pt">
        2a
      </td>
      <td colspan="2" class="xl377" width="178" style="border-left: none; width: 134pt">
        Number of HIV infected patients receiving treatment for TB
      </td>
      <td class="xl190" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-2A-COTB-F-014"/>
      </td>
      <td class="xl190" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-2A-COTB-M-014"/>
      </td>
      <td class="xl190" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-2A-COTB-F-15"/>
      </td>
      <td class="xl190" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-2A-COTB-M-15"/>
      </td>
      <td class="xl191" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-2A-COTB-F-014) + number($KART-2A-COTB-F-15) "/>
      </td>
      <td class="xl191" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-2A-COTB-M-014) + number($KART-2A-COTB-M-15) "/>
      </td>
      <td class="xl187" style="border-top: none">
        <xsl:value-of select="number($KART-2A-COTB-F-014) + number($KART-2A-COTB-F-15) + number($KART-2A-COTB-M-014) + number($KART-2A-COTB-M-15)"/>
      </td>
    </tr>
  </xsl:template>

  <xsl:template name="KART-3-WHO">
    <xsl:variable name="KART-3-WHO1-F-014" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K3_1']/QueryResults/Row[Indicators/Result/text() = 'Female' and Indicators/Result/text() = '0-14']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="KART-3-WHO1-M-014" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K3_1']/QueryResults/Row[Indicators/Result/text() = 'Male' and Indicators/Result/text() = '0-14']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="KART-3-WHO1-F-15" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K3_1']/QueryResults/Row[Indicators/Result/text() = 'Female' and Indicators/Result/text() = 'Adult']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="KART-3-WHO1-M-15" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K3_1']/QueryResults/Row[Indicators/Result/text() = 'Male' and Indicators/Result/text() = 'Adult']/Indicators[Name  = 'Total']/Result))" />


    <xsl:variable name="KART-3-WHO2-F-014" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K3_2']/QueryResults/Row[Indicators/Result/text() = 'Female' and Indicators/Result/text() = '0-14']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="KART-3-WHO2-M-014" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K3_2']/QueryResults/Row[Indicators/Result/text() = 'Male' and Indicators/Result/text() = '0-14']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="KART-3-WHO2-F-15" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K3_2']/QueryResults/Row[Indicators/Result/text() = 'Female' and Indicators/Result/text() = 'Adult']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="KART-3-WHO2-M-15" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K3_2']/QueryResults/Row[Indicators/Result/text() = 'Male' and Indicators/Result/text() = 'Adult']/Indicators[Name  = 'Total']/Result))" />

    <xsl:variable name="KART-3-WHO3-F-014" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K3_3']/QueryResults/Row[Indicators/Result/text() = 'Female' and Indicators/Result/text() = '0-14']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="KART-3-WHO3-M-014" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K3_3']/QueryResults/Row[Indicators/Result/text() = 'Male' and Indicators/Result/text() = '0-14']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="KART-3-WHO3-F-15" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K3_3']/QueryResults/Row[Indicators/Result/text() = 'Female' and Indicators/Result/text() = 'Adult']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="KART-3-WHO3-M-15" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K3_3']/QueryResults/Row[Indicators/Result/text() = 'Male' and Indicators/Result/text() = 'Adult']/Indicators[Name  = 'Total']/Result))" />

    <xsl:variable name="KART-3-WHO4-F-014" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K3_4']/QueryResults/Row[Indicators/Result/text() = 'Female' and Indicators/Result/text() = '0-14']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="KART-3-WHO4-M-014" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K3_4']/QueryResults/Row[Indicators/Result/text() = 'Male' and Indicators/Result/text() = '0-14']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="KART-3-WHO4-F-15" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K3_4']/QueryResults/Row[Indicators/Result/text() = 'Female' and Indicators/Result/text() = 'Adult']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="KART-3-WHO4-M-15" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K3_4']/QueryResults/Row[Indicators/Result/text() = 'Male' and Indicators/Result/text() = 'Adult']/Indicators[Name  = 'Total']/Result))" />

    <tr height="20" style="height: 15.0pt">
      <td rowspan="5" height="100" class="xl98" width="29" style="height: 75.0pt; border-top: none;
                width: 22pt">
        3
      </td>
      <td rowspan="5" class="xl377" width="178" style="border-left: none; width: 134pt">
        No. of patients starting ARVs within the month by WHO stage
      </td>
      <td class="xl185" width="89" style="border-top: none; border-left: none; width: 67pt">
        WHO stage 1
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-3-WHO1-F-014"/>
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-3-WHO1-M-014"/>
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-3-WHO1-F-15"/>
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-3-WHO1-M-15"/>
      </td>
      <td class="xl184" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-3-WHO1-F-014) + number($KART-3-WHO1-F-15) "/>
      </td>
      <td class="xl184" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-3-WHO1-M-014) + number($KART-3-WHO1-M-15) "/>
      </td>
      <td class="xl187" style="border-top: none">
        <xsl:value-of select="number($KART-3-WHO1-F-014) + number($KART-3-WHO1-F-15) + number($KART-3-WHO1-M-014) + number($KART-3-WHO1-M-15) "/>
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td height="20" class="xl185" width="89" style="height: 15.0pt; border-top: none;
                border-left: none; width: 67pt">
        WHO stage 2
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-3-WHO2-F-014"/>
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-3-WHO2-M-014"/>
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-3-WHO2-F-15"/>
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-3-WHO2-M-15"/>
      </td>
      <td class="xl184" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-3-WHO2-F-014) + number($KART-3-WHO2-F-15) "/>
      </td>
      <td class="xl184" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-3-WHO2-M-014) + number($KART-3-WHO2-M-15) "/>
      </td>
      <td class="xl187" style="border-top: none">
        <xsl:value-of select="number($KART-3-WHO2-F-014) + number($KART-3-WHO2-F-15) + number($KART-3-WHO2-M-014) + number($KART-3-WHO2-M-15) "/>
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td height="20" class="xl185" width="89" style="height: 15.0pt; border-top: none;
                border-left: none; width: 67pt">
        WHO stage 3
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-3-WHO3-F-014"/>
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-3-WHO3-M-014"/>
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-3-WHO3-F-15"/>
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-3-WHO3-M-15"/>
      </td>
      <td class="xl184" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-3-WHO3-F-014) + number($KART-3-WHO3-F-15) "/>
      </td>
      <td class="xl184" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-3-WHO3-M-014) + number($KART-3-WHO3-M-15) "/>
      </td>
      <td class="xl187" style="border-top: none">
        <xsl:value-of select="number($KART-3-WHO3-F-014) + number($KART-3-WHO3-F-15) + number($KART-3-WHO3-M-014) + number($KART-3-WHO3-M-15) "/>
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td height="20" class="xl185" width="89" style="height: 15.0pt; border-top: none;
                border-left: none; width: 67pt">
        WHO stage 4
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-3-WHO4-F-014"/>
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-3-WHO4-M-014"/>
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-3-WHO4-F-15"/>
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-3-WHO4-M-15"/>
      </td>
      <td class="xl184" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-3-WHO4-F-014) + number($KART-3-WHO4-F-15) "/>
      </td>
      <td class="xl184" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-3-WHO4-M-014) + number($KART-3-WHO4-M-15) "/>
      </td>
      <td class="xl187" style="border-top: none">
        <xsl:value-of select="number($KART-3-WHO4-F-014) + number($KART-3-WHO4-F-15) + number($KART-3-WHO4-M-014) + number($KART-3-WHO4-M-15) "/>
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td height="20" class="xl193" width="89" style="height: 15.0pt; border-top: none;
                border-left: none; width: 67pt">
        Sub-total
      </td>
      <td class="xl195" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-3-WHO1-F-014) + number($KART-3-WHO2-F-014) + number($KART-3-WHO3-F-014) + number($KART-3-WHO4-F-014) "/>
      </td>
      <td class="xl195" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-3-WHO1-M-014) + number($KART-3-WHO2-M-014) + number($KART-3-WHO3-M-014) + number($KART-3-WHO4-M-014) "/>
      </td>
      <td class="xl195" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-3-WHO1-F-15) + number($KART-3-WHO2-F-15) + number($KART-3-WHO3-F-15) + number($KART-3-WHO4-F-15) "/>
      </td>
      <td class="xl195" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-3-WHO1-M-15) + number($KART-3-WHO2-M-15) + number($KART-3-WHO3-M-15) + number($KART-3-WHO4-M-15) "/>
      </td>
      <td class="xl184" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-3-WHO1-F-014) + number($KART-3-WHO2-F-014) + number($KART-3-WHO3-F-014) + number($KART-3-WHO4-F-014)+number($KART-3-WHO1-F-15) + number($KART-3-WHO2-F-15) + number($KART-3-WHO3-F-15) + number($KART-3-WHO4-F-15) "/>
      </td>
      <td class="xl184" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-3-WHO1-M-014) + number($KART-3-WHO2-M-014) + number($KART-3-WHO3-M-014) + number($KART-3-WHO4-M-014) + number($KART-3-WHO1-M-15) + number($KART-3-WHO2-M-15) + number($KART-3-WHO3-M-15) + number($KART-3-WHO4-M-15) "/>
      </td>
      <td class="xl187" style="border-top: none">
        <xsl:value-of select="number($KART-3-WHO1-F-014) + number($KART-3-WHO2-F-014) + number($KART-3-WHO3-F-014) + number($KART-3-WHO4-F-014)+number($KART-3-WHO1-F-15) + number($KART-3-WHO2-F-15) + number($KART-3-WHO3-F-15) + number($KART-3-WHO4-F-15)+number($KART-3-WHO1-M-014) + number($KART-3-WHO2-M-014) + number($KART-3-WHO3-M-014) + number($KART-3-WHO4-M-014) + number($KART-3-WHO1-M-15) + number($KART-3-WHO2-M-15) + number($KART-3-WHO3-M-15) + number($KART-3-WHO4-M-15) "/>
      </td>
    </tr>

  </xsl:template>

  <xsl:template name="KART-4">
    <xsl:variable name="KART-4-F-014" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K4']/QueryResults/Row[Indicators/Result/text() = 'Female' and Indicators/Result/text() = '0-14']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="KART-4-M-014" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K4']/QueryResults/Row[Indicators/Result/text() = 'Male' and Indicators/Result/text() = '0-14']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="KART-4-F-15" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K4']/QueryResults/Row[Indicators/Result/text() = 'Female' and Indicators/Result/text() = 'Adult']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="KART-4-M-15" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K4']/QueryResults/Row[Indicators/Result/text() = 'Male' and Indicators/Result/text() = 'Adult']/Indicators[Name  = 'Total']/Result))" />
    <tr height="58" style="mso-height-source: userset; height: 43.5pt">
      <td height="58" class="xl98" width="29" style="height: 43.5pt; border-top: none;
                width: 22pt">
        4
      </td>
      <td colspan="2" class="xl185" width="178" style="border-left: none; width: 134pt">
        Cumulative No. of persons started on ARVs at this facility at end of the month.
      </td>
      <td class="xl190" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-4-F-014"/>
      </td>
      <td class="xl190" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-4-M-014"/>
      </td>
      <td class="xl190" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-4-F-15"/>
      </td>
      <td class="xl190" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-4-M-15"/>
      </td>
      <td class="xl191" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-4-F-014) + number($KART-4-F-15)"/>
      </td>
      <td class="xl191" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-4-M-014) + number($KART-4-M-15)"/>
      </td>
      <td class="xl192" style="border-top: none">
        <xsl:value-of select="number($KART-4-F-014) + number($KART-4-F-15)+number($KART-4-M-014) + number($KART-4-M-15)"/>
      </td>
    </tr>
  </xsl:template>

  <xsl:template name="KART-5">
    <xsl:variable name="KART-5-PREG-014" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K5_Pregnant']/QueryResults/Row[Indicators/Result/text() = 'Female' and Indicators/Result/text() = '0-14']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="KART-5-PREG-15" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K5_Pregnant']/QueryResults/Row[Indicators/Result/text() = 'Female' and Indicators/Result/text() = 'Adult']/Indicators[Name  = 'Total']/Result))" />

    <xsl:variable name="KART-5-ALL-F-014" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K5_AllOthers']/QueryResults/Row[Indicators/Result/text() = 'Female' and Indicators/Result/text() = '0-14']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="KART-5-ALL-F-15" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K5_AllOthers']/QueryResults/Row[Indicators/Result/text() = 'Female' and Indicators/Result/text() = 'Adult']/Indicators[Name  = 'Total']/Result))" />

    <xsl:variable name="KART-5-ALL-M-15" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K5_AllOthers']/QueryResults/Row[Indicators/Result/text() = 'Male' and Indicators/Result/text() = 'Adult']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="KART-5-ALL-M-014" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K5_AllOthers']/QueryResults/Row[Indicators/Result/text() = 'Male' and Indicators/Result/text() = '0-14']/Indicators[Name  = 'Total']/Result))" />

    <tr height="35" style="height: 26.25pt">
      <td rowspan="3" height="75" class="xl98" width="29" style="height: 56.25pt; border-top: none;
                width: 22pt">
        5
      </td>
      <td rowspan="3" class="xl377" width="178" style="border-left: none; width: 134pt">
        Total No. of patients currently on ARVs
      </td>
      <td class="xl185" width="89" style="border-top: none; border-left: none; width: 67pt">
        Pregnant Women
      </td>
      <td class="xl190" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-5-PREG-014"/>
      </td>
      <td class="xl196" width="89" style="border-top: none; border-left: none; width: 67pt">
        &#160;
      </td>
      <td class="xl190" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-5-PREG-15"/>
      </td>
      <td class="xl196" width="89" style="border-top: none; border-left: none; width: 67pt">
        &#160;
      </td>
      <td class="xl191" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-5-PREG-014) + number($KART-5-PREG-15)"/>
      </td>
      <td class="xl197" width="89" style="border-top: none; border-left: none; width: 67pt">
        &#160;
      </td>
      <td class="xl192" style="border-top: none">
        <xsl:value-of select="number($KART-5-PREG-014) + number($KART-5-PREG-15)"/>
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td height="20" class="xl185" width="89" style="height: 15.0pt; border-top: none;
                border-left: none; width: 67pt">
        All others
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-5-ALL-F-014"/>
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-5-ALL-M-014"/>
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-5-ALL-F-15"/>
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-5-ALL-M-15"/>
      </td>
      <td class="xl184" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-5-ALL-F-014) + number($KART-5-ALL-F-15)"/>
      </td>
      <td class="xl184" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-5-ALL-M-014) + number($KART-5-ALL-M-15)"/>
      </td>
      <td class="xl187" style="border-top: none">
        <xsl:value-of select="number($KART-5-ALL-F-014) + number($KART-5-ALL-F-15)+number($KART-5-ALL-M-014) + number($KART-5-ALL-M-15)"/>
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td height="20" class="xl193" width="89" style="height: 15.0pt; border-top: none;
                border-left: none; width: 67pt">
        Sub-total
      </td>
      <td class="xl195" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-5-PREG-014) + number($KART-5-ALL-F-014)"/>
      </td>
      <td class="xl195" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-5-ALL-M-014"/>
      </td>
      <td class="xl195" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-5-PREG-15) + number($KART-5-ALL-F-15)"/>
      </td>
      <td class="xl195" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-5-ALL-M-15"/>
      </td>
      <td class="xl195" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-5-PREG-014) + number($KART-5-ALL-F-014)+number($KART-5-PREG-15) + number($KART-5-ALL-F-15)"/>
      </td>
      <td class="xl198" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-5-ALL-M-014) + number($KART-5-ALL-M-15)"/>
      </td>
      <td class="xl199" width="89" style="border-top: none; width: 67pt">
        <xsl:value-of select="number($KART-5-PREG-014) + number($KART-5-ALL-F-014)+number($KART-5-PREG-15) + number($KART-5-ALL-F-15)+number($KART-5-ALL-M-014) + number($KART-5-ALL-M-15)"/>
      </td>
    </tr>
  </xsl:template>

  <xsl:template name="KART-6">
    <xsl:variable name="KART-6-F-014" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K6']/QueryResults/Row[Indicators/Result/text() = 'Female' and Indicators/Result/text() = '0-14']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="KART-6-M-014" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K6']/QueryResults/Row[Indicators/Result/text() = 'Male' and Indicators/Result/text() = '0-14']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="KART-6-F-15" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K6']/QueryResults/Row[Indicators/Result/text() = 'Female' and Indicators/Result/text() = 'Adult']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="KART-6-M-15" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K6']/QueryResults/Row[Indicators/Result/text() = 'Male' and Indicators/Result/text() = 'Adult']/Indicators[Name  = 'Total']/Result))" />
    <tr height="56" style="mso-height-source: userset; height: 42.0pt">
      <td height="56" class="xl98" width="29" style="height: 42.0pt; border-top: none;width: 22pt">
        6
      </td>
      <td colspan="2" class="xl185" width="178" style="border-left: none; width: 134pt">
        No. of persons who are enrolled and eligible for ART but have not been started on ART
      </td>
      <td class="xl190" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-6-F-014" />
      </td>
      <td class="xl190" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-6-M-014" />
      </td>
      <td class="xl190" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-6-F-15" />
      </td>
      <td class="xl190" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-6-M-15" />
      </td>
      <td class="xl191" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-6-F-014) + number($KART-6-F-15)" />
      </td>
      <td class="xl191" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-6-M-014) + number($KART-6-M-15)" />
      </td>
      <td class="xl192" style="border-top: none">
        <xsl:value-of select="number($KART-6-F-014) + number($KART-6-F-15) + number($KART-6-M-014) + number($KART-6-M-15)" />
      </td>
    </tr>
  </xsl:template>

  <xsl:template name="KART-7-PEP">
    <tr height="20" style="height: 15.0pt">
      <td rowspan="4" height="80" class="xl98" width="29" style="height: 60.0pt; border-top: none;
                width: 22pt">
        7
      </td>
      <td rowspan="4" class="xl377" width="178" style="border-left: none; width: 134pt">
        Post exposure prophylaxis (PEP)
      </td>
      <td class="xl185" width="89" style="border-top: none; border-left: none; width: 67pt">
        Sexual assault
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl184" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl184" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl187" style="border-top: none">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td height="20" class="xl185" width="89" style="height: 15.0pt; border-top: none;
                border-left: none; width: 67pt">
        Occupational
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl184" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl184" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl187" style="border-top: none">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td height="20" class="xl185" width="89" style="height: 15.0pt; border-top: none;
                border-left: none; width: 67pt">
        All others
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl184" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl184" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl187" style="border-top: none">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td height="20" class="xl193" width="89" style="height: 15.0pt; border-top: none;
                border-left: none; width: 67pt">
        Sub-total
      </td>
      <td class="xl195" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl195" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl195" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl195" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl184" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl184" width="89" style="border-top: none; border-left: none; width: 67pt">
        0
      </td>
      <td class="xl187" style="border-top: none">
        0
      </td>
    </tr>
  </xsl:template>

  <xsl:template name="KART-8-PROPHYLAXIS">
    <xsl:variable name="KART-8-CTX-F-014" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K9_CTX']/QueryResults/Row[Indicators/Result/text() = 'Female' and Indicators/Result/text() = '0-14']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="KART-8-CTX-M-014" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K9_CTX']/QueryResults/Row[Indicators/Result/text() = 'Male' and Indicators/Result/text() = '0-14']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="KART-8-CTX-F-15" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K9_CTX']/QueryResults/Row[Indicators/Result/text() = 'Female' and Indicators/Result/text() = 'Adult']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="KART-8-CTX-M-15" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K9_CTX']/QueryResults/Row[Indicators/Result/text() = 'Male' and Indicators/Result/text() = 'Adult']/Indicators[Name  = 'Total']/Result))" />

    <xsl:variable name="KART-8-FCX-F-014" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K9_Fluconazole']/QueryResults/Row[Indicators/Result/text() = 'Female' and Indicators/Result/text() = '0-14']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="KART-8-FCX-M-014" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K9_Fluconazole']/QueryResults/Row[Indicators/Result/text() = 'Male' and Indicators/Result/text() = '0-14']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="KART-8-FCX-F-15" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K9_Fluconazole']/QueryResults/Row[Indicators/Result/text() = 'Female' and Indicators/Result/text() = 'Adult']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="KART-8-FCX-M-15" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH711_K9_Fluconazole']/QueryResults/Row[Indicators/Result/text() = 'Male' and Indicators/Result/text() = 'Adult']/Indicators[Name  = 'Total']/Result))" />

    <tr height="29" style="mso-height-source: userset; height: 21.75pt">
      <td rowspan="3" height="87" class="xl98" width="29" style="border-bottom: 1.0pt solid black;
                height: 65.25pt; border-top: none; width: 22pt">
        8
      </td>
      <td rowspan="3" class="xl377" width="178" style="border-left: none; width: 134pt">
        Total No. of patients currently on prophylaxis
      </td>
      <td class="xl185" width="89" style="border-top: none; border-left: none; width: 67pt">
        Cotrimoxazole
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-8-CTX-F-014"/>
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-8-CTX-M-014"/>
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-8-CTX-F-15"/>
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-8-CTX-M-15"/>
      </td>
      <td class="xl184" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-8-CTX-F-014)+number($KART-8-CTX-F-15)"/>
      </td>
      <td class="xl184" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-8-CTX-M-014)+number($KART-8-CTX-M-15)"/>
      </td>
      <td class="xl187" style="border-top: none">
        <xsl:value-of select="number($KART-8-CTX-F-014)+number($KART-8-CTX-F-15)+number($KART-8-CTX-M-014)+number($KART-8-CTX-M-15)"/>
      </td>
    </tr>
    <tr height="29" style="mso-height-source: userset; height: 21.75pt">
      <td height="29" class="xl185" width="89" style="height: 21.75pt; border-top: none;
                border-left: none; width: 67pt">
        Fluconazole
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-8-FCX-F-014"/>
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-8-FCX-M-014"/>
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-8-FCX-F-15"/>
      </td>
      <td class="xl186" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="$KART-8-FCX-M-15"/>
      </td>
      <td class="xl184" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-8-FCX-F-014)+number($KART-8-FCX-F-15)"/>
      </td>
      <td class="xl184" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-8-FCX-M-014)+number($KART-8-FCX-M-15)"/>
      </td>
      <td class="xl187" style="border-top: none">
        <xsl:value-of select="number($KART-8-FCX-F-014)+number($KART-8-FCX-F-15)+number($KART-8-FCX-M-014)+number($KART-8-FCX-M-15)"/>
      </td>
    </tr>
    <tr height="29" style="mso-height-source: userset; height: 21.75pt">
      <td height="29" class="xl200" width="89" style="height: 21.75pt; border-top: none;
                border-left: none; width: 67pt">
        Sub-total
      </td>
      <td class="xl201" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-8-CTX-F-014)+number($KART-8-FCX-F-014)"/>
      </td>
      <td class="xl201" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-8-CTX-M-014)+number($KART-8-CTX-M-014)"/>
      </td>
      <td class="xl201" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-8-CTX-F-15)+number($KART-8-FCX-F-15)"/>
      </td>
      <td class="xl201" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-8-CTX-M-014)+number($KART-8-FCX-M-15)"/>      </td>
      <td class="xl202" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-8-CTX-F-014)+number($KART-8-FCX-F-014) + number($KART-8-CTX-F-15)+number($KART-8-FCX-F-15)"/>
      </td>
      <td class="xl202" width="89" style="border-top: none; border-left: none; width: 67pt">
        <xsl:value-of select="number($KART-8-CTX-M-014)+number($KART-8-FCX-M-014) + number($KART-8-CTX-M-15)+number($KART-8-FCX-M-15)"/>
      </td>
      <td class="xl192" style="border-top: none">
        <xsl:value-of select="number($KART-8-CTX-F-014)+number($KART-8-FCX-F-014) + number($KART-8-CTX-F-15)+number($KART-8-FCX-F-15) + number($KART-8-CTX-M-014)+number($KART-8-FCX-M-014) + number($KART-8-CTX-M-15)+number($KART-8-FCX-M-15)"/>
      </td>
    </tr>
  </xsl:template>
  <xsl:template name="ART">
    <tr height="19" style="height: 14.25pt">
      <td colspan="3" rowspan="3" height="58" class="xl235" width="207" style="height: 43.5pt;
                width: 156pt">
        K: ART
      </td>
      <td colspan="2" class="xl222" width="178" style="border-left: none; width: 134pt">
        Children
      </td>
      <td colspan="2" class="xl373" width="178" style="border-right: .5pt solid black;
                border-left: none; width: 134pt">
        Adults.
      </td>
      <td colspan="2" rowspan="2" class="xl222" width="178" style="width: 134pt">
        Totals
      </td>
      <td rowspan="3" class="xl306" width="89" style="border-bottom: 1.0pt solid black;
                width: 67pt">
        Grand Totals
      </td>
    </tr>
    <tr height="19" style="height: 14.25pt">
      <td colspan="2" height="19" class="xl182" width="178" style="height: 14.25pt; border-left: none;
                width: 134pt">
        0-14 Years
      </td>
      <td colspan="2" class="xl304" width="178" style="border-right: .5pt solid black;
                border-left: none; width: 134pt">
        &gt;14 yrs
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td height="20" class="xl183" width="89" style="height: 15.0pt; border-top: none;
                border-left: none; width: 67pt">
        F
      </td>
      <td class="xl183" width="89" style="border-top: none; border-left: none; width: 67pt">
        M
      </td>
      <td class="xl182" width="89" style="border-top: none; border-left: none; width: 67pt">
        F
      </td>
      <td class="xl182" width="89" style="border-top: none; border-left: none; width: 67pt">
        M
      </td>
      <td class="xl184" width="89" style="border-top: none; border-left: none; width: 67pt">
        F
      </td>
      <td class="xl184" width="89" style="border-top: none; border-left: none; width: 67pt">
        M
      </td>
    </tr>
    <xsl:call-template name="KART-1" />
    <xsl:call-template name="KART-2" />
    <xsl:call-template name="KART-2A-COTB" />
    <xsl:call-template name="KART-3-WHO" />
    <xsl:call-template name="KART-4" />
    <xsl:call-template name="KART-5" />
    <xsl:call-template name="KART-6" />
    <xsl:call-template name="KART-7-PEP" />
    <xsl:call-template name="KART-8-PROPHYLAXIS" />   
    <tr height="21" style="height: 15.75pt">
      <td height="21" class="xl77" width="29" style="height: 15.75pt; width: 22pt">
      </td>
      <td class="style1">
        &#160;
      </td>
      <td class="xl65" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl65" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl65" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl65" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl65" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl66" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl66" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl67" width="89" style="width: 67pt">
        &#160;
      </td>
    </tr>
  </xsl:template>

  <xsl:template name="BLOOD_SAFETY">
    <tr height="20" style="height: 15.0pt">
      <td colspan="10" height="20" class="xl230" width="830" style="border-right: 2.0pt double black;
                height: 15.0pt; width: 625pt">
        L: BLOOD<span style="mso-spacerun: yes"> </span>SAFETY
      </td>
    </tr>
    <tr height="19" style="height: 14.25pt">
      <td height="19" class="xl203" width="29" style="height: 14.25pt; border-top: none;
                width: 22pt">
        &#160;
      </td>
      <td colspan="7" class="xl233" width="623" style="border-left: none; width: 469pt">
        &#160;
      </td>
      <td colspan="2" class="xl177" width="178" style="border-right: 2.0pt double black;
                border-left: none; width: 134pt">
        Number
      </td>
    </tr>
    <tr height="19" style="height: 14.25pt">
      <td height="19" class="xl150" width="29" style="height: 14.25pt; border-top: none;
                width: 22pt">
        1
      </td>
      <td colspan="7" class="xl215" width="623" style="border-left: none; width: 469pt">
        Blood units collected from Regional Blood Transfusion Centers<span style="mso-spacerun: yes">
        </span>
      </td>
      <td colspan="2" class="xl220" width="178" style="border-right: 2.0pt double black;
                border-left: none; width: 134pt">
        0
      </td>
    </tr>
    <tr height="19" style="height: 14.25pt">
      <td height="19" class="xl150" width="29" style="height: 14.25pt; border-top: none;
                width: 22pt">
        2
      </td>
      <td colspan="7" class="xl215" width="623" style="border-left: none; width: 469pt">
        Blood units collected from other sources Other than Regional Blood
      </td>
      <td colspan="2" class="xl220" width="178" style="border-right: 2.0pt double black;
                border-left: none; width: 134pt">
        0
      </td>
    </tr>
    <tr height="19" style="height: 14.25pt">
      <td height="19" class="xl150" width="29" style="height: 14.25pt; border-top: none;
                width: 22pt">
        3
      </td>
      <td colspan="7" class="xl215" width="623" style="border-left: none; width: 469pt">
        Blood units screened at health facility
      </td>
      <td colspan="2" class="xl220" width="178" style="border-right: 2.0pt double black;
                border-left: none; width: 134pt">
        0
      </td>
    </tr>
    <tr height="19" style="height: 14.25pt">
      <td height="19" class="xl150" width="29" style="height: 14.25pt; border-top: none;
                width: 22pt">
        4
      </td>
      <td colspan="7" class="xl215" width="623" style="border-left: none; width: 469pt">
        Blood units screened found HIV+
      </td>
      <td colspan="2" class="xl220" width="178" style="border-right: 2.0pt double black;
                border-left: none; width: 134pt">
        0
      </td>
    </tr>
    <tr height="19" style="height: 14.25pt">
      <td height="19" class="xl150" width="29" style="height: 14.25pt; border-top: none;
                width: 22pt">
        5
      </td>
      <td colspan="7" class="xl215" width="623" style="border-left: none; width: 469pt">
        Blood Units transfused
      </td>
      <td colspan="2" class="xl220" width="178" style="border-right: 2.0pt double black;
                border-left: none; width: 134pt">
        0
      </td>
    </tr>
    <tr height="19" style="height: 14.25pt">
      <td height="19" class="xl150" width="29" style="height: 14.25pt; border-top: none;
                width: 22pt">
        6
      </td>
      <td colspan="7" class="xl381" width="623" style="border-left: none; width: 469pt">
        Blood units screened for hepatitis B
      </td>
      <td colspan="2" class="xl216" width="178" style="border-right: 2.0pt double black;
                border-left: none; width: 134pt">
        0
      </td>
    </tr>
    <tr height="19" style="height: 14.25pt">
      <td height="19" class="xl150" width="29" style="height: 14.25pt; border-top: none;
                width: 22pt">
        7
      </td>
      <td colspan="7" class="xl381" width="623" style="border-left: none; width: 469pt">
        Blood units screened for hepatitis C
      </td>
      <td colspan="2" class="xl216" width="178" style="border-right: 2.0pt double black;
                border-left: none; width: 134pt">
        0
      </td>
    </tr>
    <tr height="20" style="height: 15.0pt">
      <td height="20" class="xl151" width="29" style="height: 15.0pt; border-top: none;
                width: 22pt">
        8
      </td>
      <td colspan="7" class="xl382" width="623" style="border-left: none; width: 469pt">
        Blood units screened for syphilis
      </td>
      <td colspan="2" class="xl218" width="178" style="border-right: 2.0pt double black;
                border-left: none; width: 134pt">
        0
      </td>
    </tr>
    <tr height="22" style="height: 16.5pt">
      <td height="22" class="xl136" width="29" style="height: 16.5pt; width: 22pt">
        &#160;
      </td>
      <td class="style2">
        &#160;
      </td>
      <td class="xl136" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl136" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl136" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl136" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl136" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl136" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl136" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl137" width="89" style="width: 67pt">
        &#160;
      </td>
    </tr>
  </xsl:template>

  <xsl:template name="SIGNATURE">
    <tr height="36" style="height: 27.0pt">
      <td rowspan="2" height="57"  width="29" style="border: 0.0pt double black;
                height: 42.75pt; width: 22pt">
        &#160;
      </td>
      <td class="style2"
          style="border-left-style: none; border-left-color: inherit; border-left-width: medium;">
        Prepared By:
      </td>
      <td colspan="3" class="xl385" width="267" style="border-left: none; width: 201pt">
        &#160;
      </td>
      <td colspan="2" class="xl204" width="178" style="border-left: none; width: 134pt">
        Designation:
      </td>
      <td colspan="3" class="xl386" width="267" style="border-right: 2.0pt double black;
                border-left: none; width: 201pt">
        &#160;
      </td>
    </tr>
    <tr height="21" style="height: 15.75pt">
      <td height="21" class="style3"
          style="border-left-style: none; border-left-color: inherit; border-left-width: medium; border-top-style: none; border-top-color: inherit; border-top-width: medium;">
        Date:
      </td>
      <td colspan="3" class="xl388" width="267" style="border-left: none; width: 201pt">
        &#160;
      </td>
      <td colspan="2" class="xl205" width="178" style="border-left: none; width: 134pt">
        Signature:
      </td>
      <td colspan="3" class="xl389" width="267" style="border-right: 2.0pt double black;
                border-left: none; width: 201pt">
        &#160;
      </td>
    </tr>
    <tr height="21" style="height: 15.75pt">
      <td height="21" class="xl77" width="29" style="height: 15.75pt; width: 22pt">
      </td>
      <td class="style1">
        &#160;
      </td>
      <td class="xl65" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl65" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl65" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl65" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl65" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl66" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl66" width="89" style="width: 67pt">
        &#160;
      </td>
      <td class="xl67" width="89" style="width: 67pt">
        &#160;
      </td>
    </tr>
    <tr height="0" style="display: none">
      <td width="29" style="width: 22pt">
      </td>
      <td class="style2">
      </td>
      <td width="89" style="width: 67pt">
      </td>
      <td width="89" style="width: 67pt">
      </td>
      <td width="89" style="width: 67pt">
      </td>
      <td width="89" style="width: 67pt">
      </td>
      <td width="89" style="width: 67pt">
      </td>
      <td width="89" style="width: 67pt">
      </td>
      <td width="89" style="width: 67pt">
      </td>
      <td width="89" style="width: 67pt">
      </td>
    </tr>
  </xsl:template>
</xsl:stylesheet>

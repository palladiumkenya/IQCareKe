<?xml version="1.0" encoding="utf-8"?>
<!--  
	Transformations of MoH 731  XML Data
	Created: March 18, 2014 by Joseph Njung"e
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
xmlns:futuresgroup="urn:futuresgroup"
xmlns:v="urn:schemas-microsoft-com:vml"
xmlns:o="urn:schemas-microsoft-com:office:office"
xmlns:x="urn:schemas-microsoft-com:office:excel"
xmlns="http://www.w3.org/TR/REC-html40">
  <xsl:output method="html" indent="yes" encoding="UTF-8"/>
  <msxsl:script language="javascript" implements-prefix="futuresgroup">
    <![CDATA[ 
					function NaNToZero(nodeValue)
					{
						if (nodeValue == "NaN" || nodeValue == "") return 0
						return nodeValue;
					}
				]]>
  </msxsl:script>
  <xsl:template match="Root">
    <html>
      <xsl:apply-templates select="/"  mode="main"/>
    </html>
  </xsl:template>

  <xsl:template match="Results[@QUERY_NAME = 'MOH731_00']" mode="reportheader">
    <tr class="xl10126296" height="28" style="mso-height-source:userset;height:21.0pt">
      <td height="28" class="xl10226296" style="height:21.0pt;border-top:none">Facility:</td>
      <td class="xl10326296" style="border-top:none">
        <xsl:value-of select="QueryResults/Row[Count='0']/Indicators[Name='FacilityName']/Result"/>
      </td>
      <td class="xl10426296" style="border-top:none">Country:</td>
      <td class="xl11426296" style="border-top:none">Kenya</td>
      <td class="xl6826296" width="16" style="border-top:none;width:12pt">&#160;</td>
      <td class="xl10426296" style="border-top:none">District</td>
      <td class="xl10526296" style="border-top:none">
        &#160;<xsl:value-of select="QueryResults/Row[Count='0']/Indicators[Name='District']/Result"/>
      </td>
      <td class="xl10626296" style="border-top:none">Month</td>
      <td class="xl10726296" style="border-top:none">
        &#160;<xsl:value-of select="QueryResults/Row[Count='0']/Indicators[Name='Month']/Result"/>
      </td>
      <td class="xl10426296" style="border-top:none">Year</td>
      <td class="xl10726296" style="border-top:none">
        &#160;<xsl:value-of select="QueryResults/Row[Count='0']/Indicators[Name='Year']/Result"/>
      </td>
      <td class="xl10426296" style="border-top:none">MFL Code:</td>
      <td class="xl10826296" style="border-top:none">
        &#160;<xsl:value-of select="QueryResults/Row[Count='0']/Indicators[Name='SiteCode']/Result"/>
      </td>
    </tr>
  </xsl:template>
  <xsl:template match="Root" mode="main">
    <!--  1.1 Testing -->
    <xsl:variable name="HV01-01"  select="0" />
    <xsl:variable name="HV01-02"  select="0" />
    <xsl:variable name="HV01-03"  select="number($HV01-01) + number($HV01-02)" />
    <xsl:variable name="HV01-05"  select="0" />
    <xsl:variable name="HV01-06"  select="0" />
    <xsl:variable name="HV01-07"  select="0" />
    <!--  1.1 Testing -->

    <!--  1.2 Receiving results - (Couples only) -->
    <xsl:variable name="HV01-08"  select="0" />
    <xsl:variable name="HV01-09"  select="0" />
    <!--  1.2 Receiving results - (Couples only)-->

    <!--  1.3 Receiving Positive Results -->
    <xsl:variable name="HV01-10"  select="0" />
    <xsl:variable name="HV01-11"  select="0" />
    <xsl:variable name="HV01-12"  select="0" />
    <xsl:variable name="HV01-13"  select="0" />
    <xsl:variable name="HV01-14"  select="0" />
    <xsl:variable name="HV01-15"  select="0" />
    <xsl:variable name="HV01-16"  select="number($HV01-10) + number($HV01-11) + number($HV01-12) + number($HV01-13)+ number($HV01-13) + number($HV01-14) + number($HV01-15)  " />
    <!--  1.3 Receiving Positive Results -->

    <!--  2.1 Testing for HIV -->
    <xsl:variable name="HV02-01"  select="0" />
    <xsl:variable name="HV02-02"  select="0" />
    <xsl:variable name="HV02-03"  select="0" />
    <xsl:variable name="HV02-04"  select="number($HV02-01) + number($HV02-02) + number($HV02-03) " />
    <!--  2.1 Testing for HIV -->

    <!--  2.2 HIV Positive Results -->
    <xsl:variable name="HV02-05"  select="0" />
    <xsl:variable name="HV02-06"  select="0" />
    <xsl:variable name="HV02-07"  select="0" />
    <xsl:variable name="HV02-08"  select="0" />
    <xsl:variable name="HV02-09"  select="number($HV02-05) + number($HV02-06) + number($HV02-06) + number($HV02-07)" />
    <xsl:variable name="HV02-10"  select="number($HV02-04) + number($HV02-05) " />
    <!--  2.2 HIV Positive Resultst -->

    <!--  2.3 Partner Involvement -->
    <xsl:variable name="HV02-11"  select="0" />
    <xsl:variable name="HV02-12"  select="0" />
    <!--  2.3 Partner Involvement -->

    <!-- Maternal Prophlaxis -->
    <xsl:variable name="HV02-13" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_2.4_01']/QueryResults/Row[1]/Indicators[Name  = 'Total']/Result))"/>
    <xsl:variable name="HV02-14" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_2.4_02']/QueryResults/Row[1]/Indicators[Name  = 'Total']/Result))"/>
    <xsl:variable name="HV02-15"  select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_2.4_03']/QueryResults/Row[1]/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="HV02-16"  select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_2.4_04']/QueryResults/Row[1]/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="HV02-17"  select="number($HV02-16) + number($HV02-15) + number($HV02-14) + number($HV02-13)" />
    <!-- End Of Maternal Prophylaxis -->
    <!-- 2.5 Assessment for ART Eligibility-->
    <xsl:variable name="HV02-18" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_2.5_01']/QueryResults/Row[1]/Indicators[Name  = 'Total']/Result))"/>
    <xsl:variable name="HV02-19" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_2.5_02']/QueryResults/Row[1]/Indicators[Name  = 'Total']/Result))"/>
    <xsl:variable name="HV02-20"  select="number($HV02-19) + number($HV02-18)" />
    <xsl:variable name="HV02-21"  select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_2.5_03']/QueryResults/Row[1]/Indicators[Name  = 'Total']/Result))" />
    <!-- 2.5 Assessment for ART Eligibility -->

    <!-- 2.6 Infant Testing (Initial tests only)-->
    <xsl:variable name="HV02-24"  select="0" />
    <xsl:variable name="HV02-25"  select="0" />
    <xsl:variable name="HV02-26"  select="0" />
    <xsl:variable name="HV02-27" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_2.6_04']/QueryResults/Row[1]/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="HV02-28"  select="number($HV02-24) + number($HV02-25) + number($HV02-26)   " />
    <!-- 2.6 Infant Testing (Initial tests only) -->

    <!-- 2.7 Confirmed Infant Tests Results -->
    <xsl:variable name="HV02-29" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_2.7_01']/QueryResults/Row[1]/Indicators[Name  = 'Total']/Result))"/>
    <xsl:variable name="HV02-30" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_2.7_02']/QueryResults/Row[1]/Indicators[Name  = 'Total']/Result))"/>
    <xsl:variable name="HV02-31"  select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_2.7_03']/QueryResults/Row[1]/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="HV02-32"  select="number($HV02-29) + number($HV02-30) + number($HV02-31)   " />
    <!-- 2.7 Confirmed Infant Tests Results -->

    <!-- 2.8  Infant Feeding -->
    <xsl:variable name="HV02-33" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_2.8_01']/QueryResults/Row[1]/Indicators[Name  = 'Total']/Result))"/>
    <xsl:variable name="HV02-34" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_2.8_02']/QueryResults/Row[1]/Indicators[Name  = 'Total']/Result))"/>
    <xsl:variable name="HV02-35"  select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_2.8_03']/QueryResults/Row[1]/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="HV02-36"  select="number($HV02-33) + number($HV02-34) + number($HV02-35)   " />
    <xsl:variable name="HV02-37" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_2.8_04']/QueryResults/Row[1]/Indicators[Name  = 'Total']/Result))"/>
    <xsl:variable name="HV02-38" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_2.8_05']/QueryResults/Row[1]/Indicators[Name  = 'Total']/Result))"/>
    <xsl:variable name="HV02-39"  select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_2.8_06']/QueryResults/Row[1]/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="HV02-40"  select="number($HV02-37) + number($HV02-38) + number($HV02-39)   " />
    <!-- 2.8 Infant Feeding -->

    <!-- 2.9 Infant ARV Prophylaxis (at first contact only) -->
    <xsl:variable name="HV02-41" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_2.9_01']/QueryResults/Row[1]/Indicators[Name  = 'Total']/Result))"/>
    <xsl:variable name="HV02-42" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_2.9_02']/QueryResults/Row[1]/Indicators[Name  = 'Total']/Result))"/>
    <xsl:variable name="HV02-43"  select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_2.9_03']/QueryResults/Row[1]/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="HV02-44"  select="number($HV02-41) + number($HV02-42) + number($HV02-43)   " />
    <!-- 2.9 Infant ARV Prophylaxis (at first contact only) -->

    <!--  3.1 On Cotrimoxazole Prophylaxis -->
    <xsl:variable name="HV03-01" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_3.1_1']/QueryResults/Row[position()=1]/Indicators[Name='Total']/Results))"/>
    <xsl:variable name="HV03-02" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_3.1_2']/QueryResults/Row[position()=1]/Indicators[Name='Total']/Results))"/>
    <xsl:variable name="HV03-03" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_3.1_3']/QueryResults/Row[Indicators/Result/text() = 'Male' and Indicators/Result/text() = '0-14']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="HV03-04" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_3.1_3']/QueryResults/Row[Indicators/Result/text() = 'Female' and Indicators/Result/text() = '0-14']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="HV03-05" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_3.1_3']/QueryResults/Row[Indicators/Result/text() = 'Male' and Indicators/Result/text() = 'Adult']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="HV03-06" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_3.1_3']/QueryResults/Row[Indicators/Result/text() = 'Female' and Indicators/Result/text() = 'Adult']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="HV03-07"  select="number($HV03-03) + number($HV03-04) + number($HV03-05) + number($HV03-06)  " />
    <!--  3.1 On Cotrimoxazole Prophylaxis -->

    <!--  3.2 Enrolled in Care -->
    <xsl:variable name="HV03-08"  select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_3.2_1']/QueryResults/Row[1]/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="HV03-09" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_3.2']/QueryResults/Row[Indicators/Result/text() = 'Male' and Indicators/Result/text() = '0-14']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="HV03-10" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_3.2']/QueryResults/Row[Indicators/Result/text() = 'Female' and Indicators/Result/text() = '0-14']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="HV03-11" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_3.2']/QueryResults/Row[Indicators/Result/text() = 'Male' and Indicators/Result/text() = 'Adult']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="HV03-12" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_3.2']/QueryResults/Row[Indicators/Result/text() = 'Female' and Indicators/Result/text() = 'Adult']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="HV03-13"  select="number($HV03-09) + number($HV03-10) + number($HV03-11) + number($HV03-12)  " />
    <!--  3.2 Enrolled in Care -->

    <!--  3.3 Currently in Care -->

    <xsl:variable name="HV03-14"  select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_3.3_1']/QueryResults/Row[1]/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="HV03-15" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_3.3_2']/QueryResults/Row[Indicators/Result/text() = 'Male' and Indicators/Result/text() = '0-14']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="HV03-16" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_3.3_2']/QueryResults/Row[Indicators/Result/text() = 'Female' and Indicators/Result/text() = '0-14']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="HV03-17" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_3.3_2']/QueryResults/Row[Indicators/Result/text() = 'Male' and Indicators/Result/text() = 'Adult']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="HV03-18" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_3.3_2']/QueryResults/Row[Indicators/Result/text() = 'Female' and Indicators/Result/text() = 'Adult']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="HV03-19" select="number($HV03-15) + number($HV03-16) + number($HV03-17) + number($HV03-18)"/>

    <!--  3.3 Currently in Care -->

    <!--  3.4 Starting ART -->
    <xsl:variable name="HV03-20"  select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_3.4_1']/QueryResults/Row[1]/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="HV03-21" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_3.4_2']/QueryResults/Row[Indicators/Result/text() = 'Male' and Indicators/Result/text() = '0-14']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="HV03-22" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_3.4_2']/QueryResults/Row[Indicators/Result/text() = 'Female' and Indicators/Result/text() = '0-14']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="HV03-23" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_3.4_2']/QueryResults/Row[Indicators/Result/text() = 'Male' and Indicators/Result/text() = 'Adult']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="HV03-24" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_3.4_2']/QueryResults/Row[Indicators/Result/text() = 'Female' and Indicators/Result/text() = 'Adult']/Indicators[Name  = 'Total']/Result))" />

    <xsl:variable name="HV03-25" select="number($HV03-21) + number($HV03-22) + number($HV03-23) + number($HV03-24)"/>

    <xsl:variable name="HV03-26"  select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_3.4_Preg']/QueryResults/Row[1]/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="HV03-27"  select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_3.4_TB']/QueryResults/Row[1]/Indicators[Name  = 'Total']/Result))" />
    <!--  3.4 Starting ART -->

    <!-- 3.5 Revisits on ART -->
    <xsl:variable name="HV03-28" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_3.5_1']/QueryResults/Row[1]/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="HV03-29" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_3.5_2']/QueryResults/Row[Indicators/Result/text() = 'Male' and Indicators/Result/text() = '0-14']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="HV03-30" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_3.5_2']/QueryResults/Row[Indicators/Result/text() = 'Female' and Indicators/Result/text() = '0-14']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="HV03-31" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_3.5_2']/QueryResults/Row[Indicators/Result/text() = 'Male' and Indicators/Result/text() = 'Adult']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="HV03-32" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_3.5_2']/QueryResults/Row[Indicators/Result/text() = 'Female' and Indicators/Result/text() = 'Adult']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="HV03-33" select="number($HV03-29) + number($HV03-30) + number($HV03-31) + number($HV03-32)"/>
    <!-- 3.5 Revisits on ART -->

    <!-- 3.6 Currently on ART [All]  -->
    <xsl:variable name="HV03-34" select="number($HV03-28)  + number($HV03-20) "/>
    <xsl:variable name="HV03-35" select="number($HV03-29)  + number($HV03-21) "/>
    <xsl:variable name="HV03-36" select="number($HV03-30)  + number($HV03-22) "/>
    <xsl:variable name="HV03-37" select="number($HV03-31)  + number($HV03-23) "/>
    <xsl:variable name="HV03-38" select="number($HV03-32)  + number($HV03-24) "/>
    <xsl:variable name="HV03-39" select="number($HV03-35)  + number($HV03-36)  + number($HV03-37) + number($HV03-38)"/>
    <!-- 3.6 Currently on ART [All]  -->

    <!-- 3.7 Cumulative Ever on ART  -->
    <xsl:variable name="HV03-40" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_3.7']/QueryResults/Row[Indicators/Result/text() = 'Male' and Indicators/Result/text() = '0-14']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="HV03-41" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_3.7']/QueryResults/Row[Indicators/Result/text() = 'Female' and Indicators/Result/text() = '0-14']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="HV03-42" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_3.7']/QueryResults/Row[Indicators/Result/text() = 'Male' and Indicators/Result/text() = 'Adult']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="HV03-43" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_3.7']/QueryResults/Row[Indicators/Result/text() = 'Female' and Indicators/Result/text() = 'Adult']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="HV03-44" select="number($HV03-40)  + number($HV03-41)  + number($HV03-42) + number($HV03-43)"/>
    <!-- 3.7 Cumulative Ever on ART  -->

    <!--3.8 Survival and Retention -->
    <xsl:variable name="HV03-45" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_3.8_1']/QueryResults/Row[position()=1]/Indicators[Name='Total']/Result))" />
    <xsl:variable name="HV03-46" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_3.8_2']/QueryResults/Row[position()=1]/Indicators[Name='Total']/Result))" />
    <xsl:variable name="HV03-47" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_3.8_3']/QueryResults/Row[position()=1]/Indicators[Name='Total']/Result))" />
    <xsl:variable name="HV03-48" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_3.8_4']/QueryResults/Row[position()=1]/Indicators[Name='Total']/Result))" />
    <xsl:variable name="HV03-49" select="number($HV03-46)  + number($HV03-47)  + number($HV03-48) "/>
    <!--3.8 Survival and Retention -->

    <!--3.9 Screening -->
    <xsl:variable name="HV03-50" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_3.10_01']/QueryResults/Row[Indicators/Result/text() = 'Male' and Indicators/Result/text() = '0-14']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="HV03-51" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_3.10_01']/QueryResults/Row[Indicators/Result/text() = 'Female' and Indicators/Result/text() = '0-14']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="HV03-52" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_3.10_01']/QueryResults/Row[Indicators/Result/text() = 'Male' and Indicators/Result/text() = 'Adult']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="HV03-53" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_3.10_01']/QueryResults/Row[Indicators/Result/text() = 'Female' and Indicators/Result/text() = 'Adult']/Indicators[Name  = 'Total']/Result))" />
    <xsl:variable name="HV03-54" select="number($HV03-50)  + number($HV03-51)  + number($HV03-52)  + number($HV03-53)"/>
    <xsl:variable name="HV03-55" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_3.9_2']/QueryResults/Row[position()=1]/Indicators[Name='Total']/Result))" />
    <!--3.9 Screening -->

    <!--3.10 PWP -->
    <xsl:variable name="HV09-04" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_3.10_02']/QueryResults/Row[position()=1]/Indicators[Name='Total']/Result))" />
    <xsl:variable name="HV09-05" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_3.10_01']/QueryResults/Row[position()=1]/Indicators[Name='Total']/Result))" />
    <!-- 3.10 PWP -->

    <!--3.11 HIV Care Visits-->
    <xsl:variable name="HV03-70" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_3.11_1']/QueryResults/Row[position()=1]/Indicators[Name='Total']/Result))" />
    <xsl:variable name="HV03-71" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_3.11_2']/QueryResults/Row[position()=1]/Indicators[Name='Total']/Result))" />
    <xsl:variable name="HV03-72" select="futuresgroup:NaNToZero(string(Data/Results[@QUERY_NAME='MoH731_3.11_3']/QueryResults/Row[position()=1]/Indicators[Name='Total']/Result))"/>
    <xsl:variable name="HV03-73" select="number($HV03-71)  + number($HV03-72)  "/>
    <!--3.11 HIV Care Visits-->

    <!-- 4 Voluntary Medical Male Circumcision -->
    <xsl:variable name="HV04-01" select="0"/>
    <xsl:variable name="HV04-02" select="0"/>
    <xsl:variable name="HV04-03" select="0"/>
    <xsl:variable name="HV04-06"  select="number($HV04-01)  + number($HV04-02)" />
    <xsl:variable name="HV04-07" select="0"/>
    <xsl:variable name="HV04-08" select="0"/>
    <xsl:variable name="HV04-09" select="0"/>
    <xsl:variable name="HV04-10" select="0"/>
    <xsl:variable name="HV04-11" select="0"/>
    <xsl:variable name="HV04-12" select="0"/>
    <xsl:variable name="HV04-13" select="0"/>
    <xsl:variable name="HV04-14" select="number($HV04-10)  + number($HV04-11)" />
    <xsl:variable name="HV04-15" select="number($HV04-12)  + number($HV04-13)" />
    <!-- 4 Voluntary Medical Male Circumcision-->

    <!-- 5 PEP -->
    <xsl:variable name="HV05-01" select="0"/>
    <xsl:variable name="HV05-02" select="0"/>
    <xsl:variable name="HV05-03" select="0"/>
    <xsl:variable name="HV05-04" select="0" />
    <xsl:variable name="HV05-05" select="0"/>
    <xsl:variable name="HV05-06" select="0"/>
    <xsl:variable name="HV05-07" select="number($HV05-01)  + number($HV05-02)+ number($HV05-03)+ number($HV05-04)+ number($HV05-05)+ number($HV05-06)"/>
    <xsl:variable name="HV05-08" select="0"/>
    <xsl:variable name="HV05-09" select="0"/>
    <xsl:variable name="HV05-10" select="0"/>
    <xsl:variable name="HV05-11" select="0"/>
    <xsl:variable name="HV05-12" select="0" />
    <xsl:variable name="HV05-13" select="0" />
    <xsl:variable name="HV05-14" select="number($HV05-08)  + number($HV05-09)+ number($HV05-10)+ number($HV05-11)+ number($HV05-12)+ number($HV05-13)"/>
    <!-- 5 PEP-->

    <!-- 6 Blood Safety -->
    <xsl:variable name="HV06-01" select="0"/>
    <xsl:variable name="HV06-02" select="0"/>
    <xsl:variable name="HV06-05"  select="0" />
    <!-- 6 Blood Safety -->
    <head>
      <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
      <meta name="ProgId" content="Excel.Sheet" />
      <meta name="Generator" content="Microsoft Excel 14" />
      <meta http-equiv="Pragma" content="no-cache" />
      <meta http-equiv="Expires" content="-1" />

      <title>MOH 731 - Comprehensive HIV/AIDS Facility Reporting Form - NASCOP</title>
      <style type="text/css" media="screen">
        body, td, span, input, div
        {
        font-family: Tahoma, Verdana, Arial;
        font-size: 11px;
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
        color: #FFFF00;
        color: #FF0000;
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
        .xl6826296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:12.0pt;
        font-weight:700;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:general;
        vertical-align:top;
        border-top:.5pt solid windowtext;
        border-right:none;
        border-bottom:1.0pt solid windowtext;
        border-left:none;
        background:#C5D9F1;
        mso-pattern:black none;
        white-space:normal;}
        .xl6926296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:10.0pt;
        font-weight:400;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:general;
        vertical-align:bottom;
        mso-background-source:auto;
        mso-pattern:auto;
        white-space:nowrap;}
        .xl7026296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:10.0pt;
        font-weight:700;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:general;
        vertical-align:bottom;
        border-top:none;
        border-right:none;
        border-bottom:none;
        border-left:1.0pt solid windowtext;
        background:#FDE9D9;
        mso-pattern:black none;
        white-space:nowrap;}
        .xl7126296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:10.0pt;
        font-weight:700;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:"\@";
        text-align:right;
        vertical-align:top;
        border-top:none;
        border-right:none;
        border-bottom:none;
        border-left:1.0pt solid windowtext;
        background:#FDE9D9;
        mso-pattern:black none;
        white-space:nowrap;}
        .xl7226296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:10.0pt;
        font-weight:700;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:general;
        vertical-align:bottom;
        background:#FDE9D9;
        mso-pattern:black none;
        white-space:nowrap;}
        .xl7326296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:10.0pt;
        font-weight:700;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:general;
        vertical-align:bottom;
        border-top:none;
        border-right:1.0pt solid windowtext;
        border-bottom:none;
        border-left:none;
        background:#FDE9D9;
        mso-pattern:black none;
        white-space:nowrap;}
        .xl7426296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:10.0pt;
        font-weight:700;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:general;
        vertical-align:bottom;
        border-top:.5pt solid windowtext;
        border-right:none;
        border-bottom:none;
        border-left:1.0pt solid windowtext;
        background:#FDE9D9;
        mso-pattern:black none;
        white-space:nowrap;}
        .xl7526296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:10.0pt;
        font-weight:700;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:general;
        vertical-align:bottom;
        border-top:.5pt solid windowtext;
        border-right:none;
        border-bottom:none;
        border-left:none;
        background:#FDE9D9;
        mso-pattern:black none;
        white-space:nowrap;}
        .xl7626296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:10.0pt;
        font-weight:700;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:general;
        vertical-align:bottom;
        border-top:.5pt solid windowtext;
        border-right:1.0pt solid windowtext;
        border-bottom:none;
        border-left:none;
        background:#FDE9D9;
        mso-pattern:black none;
        white-space:nowrap;}
        .xl7726296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:10.0pt;
        font-weight:700;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:"\@";
        text-align:right;
        vertical-align:top;
        border-top:none;
        border-right:none;
        border-bottom:none;
        border-left:1.0pt solid windowtext;
        mso-background-source:auto;
        mso-pattern:auto;
        white-space:nowrap;}
        .xl7826296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:10.0pt;
        font-weight:700;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:general;
        vertical-align:bottom;
        mso-background-source:auto;
        mso-pattern:auto;
        white-space:nowrap;}
        .xl7926296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:10.0pt;
        font-weight:400;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:general;
        vertical-align:bottom;
        border-top:none;
        border-right:1.0pt solid windowtext;
        border-bottom:none;
        border-left:none;
        mso-background-source:auto;
        mso-pattern:auto;
        white-space:nowrap;}
        .xl8026296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:10.0pt;
        font-weight:400;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:general;
        vertical-align:bottom;
        border-top:none;
        border-right:none;
        border-bottom:none;
        border-left:1.0pt solid windowtext;
        mso-background-source:auto;
        mso-pattern:auto;
        white-space:nowrap;}
        .xl8126296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:10.0pt;
        font-weight:400;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:general;
        vertical-align:bottom;
        border:.5pt solid windowtext;
        background:#FFFFCC;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:nowrap;}
        .xl8226296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:10.0pt;
        font-weight:400;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:"\@";
        text-align:right;
        vertical-align:top;
        border-top:none;
        border-right:none;
        border-bottom:none;
        border-left:1.0pt solid windowtext;
        mso-background-source:auto;
        mso-pattern:auto;
        white-space:nowrap;}
        .xl8326296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:red;
        font-size:10.0pt;
        font-weight:700;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:general;
        vertical-align:bottom;
        border-top:none;
        border-right:none;
        border-bottom:1.0pt solid windowtext;
        border-left:none;
        mso-background-source:auto;
        mso-pattern:auto;
        white-space:nowrap;}
        .xl8426296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:10.0pt;
        font-weight:400;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:general;
        vertical-align:bottom;
        border-top:.5pt solid windowtext;
        border-right:none;
        border-bottom:none;
        border-left:none;
        mso-background-source:auto;
        mso-pattern:auto;
        white-space:nowrap;}
        .xl8526296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:10.0pt;
        font-weight:400;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:general;
        vertical-align:bottom;
        border-top:1.0pt solid windowtext;
        border-right:none;
        border-bottom:1.0pt solid windowtext;
        border-left:none;
        mso-background-source:auto;
        mso-pattern:auto;
        white-space:nowrap;}
        .xl8626296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:10.0pt;
        font-weight:400;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:general;
        vertical-align:bottom;
        border-top:none;
        border-right:none;
        border-bottom:1.0pt solid windowtext;
        border-left:none;
        mso-background-source:auto;
        mso-pattern:auto;
        white-space:nowrap;}
        .xl8726296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:10.0pt;
        font-weight:400;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:general;
        vertical-align:bottom;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:.5pt solid windowtext;
        background:#FFFFCC;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:nowrap;}
        .xl8826296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:red;
        font-size:10.0pt;
        font-weight:700;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:general;
        vertical-align:bottom;
        mso-background-source:auto;
        mso-pattern:auto;
        white-space:nowrap;}
        .xl8926296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:10.0pt;
        font-weight:700;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:"\@";
        text-align:right;
        vertical-align:top;
        border-top:.5pt solid windowtext;
        border-right:none;
        border-bottom:none;
        border-left:1.0pt solid windowtext;
        background:#FDE9D9;
        mso-pattern:black none;
        white-space:nowrap;}
        .xl9026296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:10.0pt;
        font-weight:400;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:general;
        vertical-align:bottom;
        border-top:none;
        border-right:none;
        border-bottom:1.0pt solid windowtext;
        border-left:1.0pt solid windowtext;
        mso-background-source:auto;
        mso-pattern:auto;
        white-space:nowrap;}
        .xl9126296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:10.0pt;
        font-weight:400;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:general;
        vertical-align:bottom;
        border-top:none;
        border-right:1.0pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:none;
        mso-background-source:auto;
        mso-pattern:auto;
        white-space:nowrap;}
        .xl9226296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:10.0pt;
        font-weight:700;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:general;
        vertical-align:bottom;
        border-top:1.0pt solid windowtext;
        border-right:none;
        border-bottom:none;
        border-left:1.0pt solid windowtext;
        background:#FDE9D9;
        mso-pattern:black none;
        white-space:nowrap;}
        .xl9326296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:10.0pt;
        font-weight:700;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:general;
        vertical-align:bottom;
        border-top:1.0pt solid windowtext;
        border-right:none;
        border-bottom:none;
        border-left:none;
        background:#FDE9D9;
        mso-pattern:black none;
        white-space:nowrap;}
        .xl9426296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:10.0pt;
        font-weight:700;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:general;
        vertical-align:bottom;
        border-top:1.0pt solid windowtext;
        border-right:1.0pt solid windowtext;
        border-bottom:none;
        border-left:none;
        background:#FDE9D9;
        mso-pattern:black none;
        white-space:nowrap;}
        .xl9526296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:10.0pt;
        font-weight:400;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:general;
        vertical-align:bottom;
        border:.5pt solid windowtext;
        background:#FFFFCC;
        mso-pattern:black none;
        white-space:nowrap;}
        .xl9626296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:10.0pt;
        font-weight:400;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:general;
        vertical-align:bottom;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:.5pt solid windowtext;
        background:#FFFFCC;
        mso-pattern:black none;
        white-space:nowrap;}
        .xl9726296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:10.0pt;
        font-weight:400;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:"\@";
        text-align:right;
        vertical-align:top;
        border-top:none;
        border-right:none;
        border-bottom:1.0pt solid windowtext;
        border-left:1.0pt solid windowtext;
        mso-background-source:auto;
        mso-pattern:auto;
        white-space:nowrap;}
        .xl9826296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:10.0pt;
        font-weight:700;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:general;
        vertical-align:bottom;
        border-top:none;
        border-right:none;
        border-bottom:1.0pt solid windowtext;
        border-left:none;
        mso-background-source:auto;
        mso-pattern:auto;
        white-space:nowrap;}
        .xl9926296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:10.0pt;
        font-weight:700;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:"\@";
        text-align:right;
        vertical-align:top;
        border-top:1.0pt solid windowtext;
        border-right:none;
        border-bottom:none;
        border-left:1.0pt solid windowtext;
        background:#FDE9D9;
        mso-pattern:black none;
        white-space:nowrap;}
        .xl10026296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:10.0pt;
        font-weight:400;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:center;
        vertical-align:bottom;
        background:#A6A6A6;
        mso-pattern:black none;
        white-space:nowrap;}
        .xl10126296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:12.0pt;
        font-weight:400;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:general;
        vertical-align:bottom;
        mso-background-source:auto;
        mso-pattern:auto;
        white-space:nowrap;}
        .xl10226296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:12.0pt;
        font-weight:700;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:general;
        vertical-align:bottom;
        border-top:.5pt solid windowtext;
        border-right:none;
        border-bottom:1.0pt solid windowtext;
        border-left:1.0pt solid windowtext;
        background:#C5D9F1;
        mso-pattern:black none;
        white-space:nowrap;}
        .xl10326296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:12.0pt;
        font-weight:700;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:general;
        vertical-align:bottom;
        border-top:.5pt solid windowtext;
        border-right:none;
        border-bottom:1.0pt solid windowtext;
        border-left:none;
        background:#FFFFCC;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:nowrap;}
        .xl10426296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:12.0pt;
        font-weight:700;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:general;
        vertical-align:bottom;
        border-top:.5pt solid windowtext;
        border-right:none;
        border-bottom:1.0pt solid windowtext;
        border-left:none;
        background:#C5D9F1;
        mso-pattern:black none;
        white-space:nowrap;}
        .xl10526296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:12.0pt;
        font-weight:700;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:general;
        vertical-align:bottom;
        border-top:.5pt solid windowtext;
        border-right:none;
        border-bottom:1.0pt solid windowtext;
        border-left:none;
        background:#FFFFCC;
        mso-pattern:black none;
        white-space:nowrap;}
        .xl10626296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:12.0pt;
        font-weight:700;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:general;
        vertical-align:bottom;
        border-top:.5pt solid windowtext;
        border-right:none;
        border-bottom:1.0pt solid windowtext;
        border-left:.5pt solid windowtext;
        background:#C5D9F1;
        mso-pattern:black none;
        white-space:nowrap;}
        .xl10726296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:red;
        font-size:12.0pt;
        font-weight:700;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:center;
        vertical-align:bottom;
        border-top:.5pt solid windowtext;
        border-right:none;
        border-bottom:1.0pt solid windowtext;
        border-left:none;
        background:#FFFFCC;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:nowrap;}
        .xl10826296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:12.0pt;
        font-weight:700;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:general;
        vertical-align:bottom;
        border-top:.5pt solid windowtext;
        border-right:1.0pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:none;
        background:#FFFFCC;
        mso-pattern:black none;
        white-space:nowrap;}
        .xl10926296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:12.0pt;
        font-weight:700;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:center;
        vertical-align:bottom;
        border-top:none;
        border-right:none;
        border-bottom:.5pt solid windowtext;
        border-left:1.0pt solid windowtext;
        background:#C5D9F1;
        mso-pattern:black none;
        white-space:nowrap;}
        .xl11026296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:12.0pt;
        font-weight:700;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:center;
        vertical-align:bottom;
        border-top:none;
        border-right:none;
        border-bottom:.5pt solid windowtext;
        border-left:none;
        background:#C5D9F1;
        mso-pattern:black none;
        white-space:nowrap;}
        .xl11126296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:12.0pt;
        font-weight:700;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:center;
        vertical-align:bottom;
        border-top:none;
        border-right:1.0pt solid windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:none;
        background:#C5D9F1;
        mso-pattern:black none;
        white-space:nowrap;}
        .xl11226296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:10.0pt;
        font-weight:400;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:center;
        vertical-align:bottom;
        border-top:none;
        border-right:none;
        border-bottom:1.0pt solid windowtext;
        border-left:none;
        background:#A6A6A6;
        mso-pattern:black none;
        white-space:nowrap;}
        .xl11326296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:10.0pt;
        font-weight:700;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:center;
        vertical-align:bottom;
        border-top:.5pt solid windowtext;
        border-right:1.0pt solid windowtext;
        border-bottom:none;
        border-left:none;
        background:#FDE9D9;
        mso-pattern:black none;
        white-space:nowrap;}
        .xl11426296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:12.0pt;
        font-weight:700;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:center;
        vertical-align:bottom;
        border-top:.5pt solid windowtext;
        border-right:none;
        border-bottom:1.0pt solid windowtext;
        border-left:none;
        background:#FFFFCC;
        mso-pattern:black none;
        white-space:nowrap;}
        .xl11526296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:10.0pt;
        font-weight:400;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:center;
        vertical-align:bottom;
        border:.5pt solid windowtext;
        background:#FFFFCC;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:nowrap;}
        .xl11626296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:red;
        font-size:10.0pt;
        font-weight:700;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:center;
        vertical-align:bottom;
        border-top:none;
        border-right:1.0pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:none;
        mso-background-source:auto;
        mso-pattern:auto;
        white-space:nowrap;}
        .xl11726296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:10.0pt;
        font-weight:400;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:center;
        vertical-align:bottom;
        border-top:1.0pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:.5pt solid windowtext;
        background:#FFFFCC;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:nowrap;}
        .xl11826296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:10.0pt;
        font-weight:400;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:center;
        vertical-align:bottom;
        border-top:none;
        border-right:.5pt solid windowtext;
        border-bottom:.5pt solid windowtext;
        border-left:.5pt solid windowtext;
        background:#FFFFCC;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:nowrap;}
        .xl11926296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:10.0pt;
        font-weight:400;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:center;
        vertical-align:bottom;
        border-top:none;
        border-right:1.0pt solid windowtext;
        border-bottom:none;
        border-left:none;
        mso-background-source:auto;
        mso-pattern:auto;
        white-space:nowrap;}
        .xl12026296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:10.0pt;
        font-weight:400;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:center;
        vertical-align:bottom;
        border-top:none;
        border-right:1.0pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:none;
        mso-background-source:auto;
        mso-pattern:auto;
        white-space:nowrap;}
        .xl12126296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:10.0pt;
        font-weight:700;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:center;
        vertical-align:bottom;
        border-top:none;
        border-right:1.0pt solid windowtext;
        border-bottom:none;
        border-left:none;
        background:#FDE9D9;
        mso-pattern:black none;
        white-space:nowrap;}
        .xl12226296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:10.0pt;
        font-weight:400;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:center;
        vertical-align:bottom;
        border-top:.5pt solid windowtext;
        border-right:.5pt solid windowtext;
        border-bottom:1.0pt solid windowtext;
        border-left:.5pt solid windowtext;
        background:#FFFFCC;
        mso-pattern:black none;
        mso-protection:unlocked visible;
        white-space:nowrap;}
        .xl12326296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:red;
        font-size:10.0pt;
        font-weight:700;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:center;
        vertical-align:bottom;
        border-top:none;
        border-right:1.0pt solid windowtext;
        border-bottom:none;
        border-left:none;
        mso-background-source:auto;
        mso-pattern:auto;
        white-space:nowrap;}
        .xl12426296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:10.0pt;
        font-weight:700;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:center;
        vertical-align:bottom;
        border-top:1.0pt solid windowtext;
        border-right:1.0pt solid windowtext;
        border-bottom:none;
        border-left:none;
        background:#FDE9D9;
        mso-pattern:black none;
        white-space:nowrap;}
        .xl12526296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:10.0pt;
        font-weight:700;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:center;
        vertical-align:bottom;
        border-top:1.0pt solid windowtext;
        border-right:none;
        border-bottom:none;
        border-left:none;
        background:#FDE9D9;
        mso-pattern:black none;
        white-space:nowrap;}
        .xl12626296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:10.0pt;
        font-weight:700;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:center;
        vertical-align:bottom;
        background:#FDE9D9;
        mso-pattern:black none;
        white-space:nowrap;}
        .xl12726296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:red;
        font-size:10.0pt;
        font-weight:700;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:center;
        vertical-align:bottom;
        mso-background-source:auto;
        mso-pattern:auto;
        white-space:nowrap;}
        .xl12826296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:10.0pt;
        font-weight:700;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:center;
        vertical-align:bottom;
        border-top:.5pt solid windowtext;
        border-right:none;
        border-bottom:none;
        border-left:none;
        background:#FDE9D9;
        mso-pattern:black none;
        white-space:nowrap;}
        .xl12926296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:10.0pt;
        font-weight:400;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:center;
        vertical-align:bottom;
        border-top:none;
        border-right:none;
        border-bottom:1.0pt solid windowtext;
        border-left:none;
        mso-background-source:auto;
        mso-pattern:auto;
        white-space:nowrap;}
        .xl13026296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:10.0pt;
        font-weight:400;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:center;
        vertical-align:bottom;
        mso-background-source:auto;
        mso-pattern:auto;
        white-space:nowrap;}
        .xl13126296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:10.0pt;
        font-weight:700;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:left;
        vertical-align:top;
        border-top:.5pt solid windowtext;
        border-right:none;
        border-bottom:none;
        border-left:none;
        background:#FDE9D9;
        mso-pattern:black none;
        white-space:nowrap;}
        .xl13226296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:10.0pt;
        font-weight:700;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:left;
        vertical-align:top;
        border-top:.5pt solid windowtext;
        border-right:1.0pt solid windowtext;
        border-bottom:none;
        border-left:none;
        background:#FDE9D9;
        mso-pattern:black none;
        white-space:nowrap;}
        .xl13326296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:10.0pt;
        font-weight:700;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:left;
        vertical-align:bottom;
        border-top:.5pt solid windowtext;
        border-right:none;
        border-bottom:none;
        border-left:none;
        background:#FDE9D9;
        mso-pattern:black none;
        white-space:nowrap;}
        .xl13426296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:10.0pt;
        font-weight:700;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:left;
        vertical-align:bottom;
        border-top:.5pt solid windowtext;
        border-right:1.0pt solid windowtext;
        border-bottom:none;
        border-left:none;
        background:#FDE9D9;
        mso-pattern:black none;
        white-space:nowrap;}
        .xl13526296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:10.0pt;
        font-weight:700;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:left;
        vertical-align:bottom;
        border-top:1.0pt solid windowtext;
        border-right:none;
        border-bottom:none;
        border-left:none;
        background:#FDE9D9;
        mso-pattern:black none;
        white-space:nowrap;}
        .xl13626296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:10.0pt;
        font-weight:700;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:left;
        vertical-align:bottom;
        border-top:1.0pt solid windowtext;
        border-right:1.0pt solid windowtext;
        border-bottom:none;
        border-left:none;
        background:#FDE9D9;
        mso-pattern:black none;
        white-space:nowrap;}
        .xl13726296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:10.0pt;
        font-weight:700;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:left;
        vertical-align:bottom;
        background:#FDE9D9;
        mso-pattern:black none;
        white-space:nowrap;}
        .xl13826296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:10.0pt;
        font-weight:700;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:left;
        vertical-align:bottom;
        border-top:none;
        border-right:1.0pt solid windowtext;
        border-bottom:none;
        border-left:none;
        background:#FDE9D9;
        mso-pattern:black none;
        white-space:nowrap;}
        .xl13926296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:12.0pt;
        font-weight:700;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:center;
        vertical-align:bottom;
        background:#C5D9F1;
        mso-pattern:black none;
        white-space:nowrap;}
        .xl14026296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:black;
        font-size:12.0pt;
        font-weight:700;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:center;
        vertical-align:bottom;
        border-top:none;
        border-right:1.0pt solid windowtext;
        border-bottom:none;
        border-left:none;
        background:#C5D9F1;
        mso-pattern:black none;
        white-space:nowrap;}
        .xl14126296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:red;
        font-size:10.0pt;
        font-weight:700;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:left;
        vertical-align:bottom;
        border-top:1.0pt solid windowtext;
        border-right:none;
        border-bottom:none;
        border-left:none;
        mso-background-source:auto;
        mso-pattern:auto;
        white-space:nowrap;}
        .xl14226296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:red;
        font-size:10.0pt;
        font-weight:700;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:left;
        vertical-align:bottom;
        mso-background-source:auto;
        mso-pattern:auto;
        white-space:nowrap;}
        .xl14326296
        {padding-top:1px;
        padding-right:1px;
        padding-left:1px;
        mso-ignore:padding;
        color:red;
        font-size:10.0pt;
        font-weight:700;
        font-style:normal;
        text-decoration:none;
        font-family:"Segoe UI", sans-serif;
        mso-font-charset:0;
        mso-number-format:General;
        text-align:left;
        vertical-align:bottom;
        border-top:1.0pt solid windowtext;
        border-right:none;
        border-bottom:1.0pt solid windowtext;
        border-left:none;
        mso-background-source:auto;
        mso-pattern:auto;
        white-space:nowrap;}
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
    </head>
    <body>
      <div id="MoH731 Template_26296" align="center" x:publishsource="Excel">
        <form id="frmPrint" name="form1">
          <p align="center" class="btn">
            <input type="button" name="cmdPrint" class="btn" style="width: 100px" value="Print Report" onclick="Javascript:PrintMe()"></input>&#160;
            <input type="button" name="cmdClose" class="btn" style="width: 75px" value="Close" onclick="window.close()"/>
          </p>
        </form>
        <table border="0" cellpadding="0" cellspacing="0"  class="xl6926296" style="border-collapse:collapse;table-layout:fixed;width:720pt">
          <col class="xl6926296" width="80" style="mso-width-source:userset;mso-width-alt: 2925;width:60pt"/>
          <col class="xl6926296" width="299" style="mso-width-source:userset;mso-width-alt: 14592;width:299pt"/>
          <col class="xl6926296" width="94" style="mso-width-source:userset;mso-width-alt: 3437;width:71pt"/>
          <col class="xl13026296" width="66" style="mso-width-source:userset;mso-width-alt: 2413;width:50pt"/>
          <col class="xl6926296" width="16" style="mso-width-source:userset;mso-width-alt: 585;width:12pt"/>

          <col class="xl6926296" width="50" style="mso-width-source:userset;mso-width-alt: 2413;width:50pt"/>
          <col class="xl6926296" width="207" style="mso-width-source:userset;mso-width-alt: 10093;width:207pt"/>
          <col class="xl6926296" width="81" style="mso-width-source:userset;mso-width-alt: 2962;width:61pt"/>
          <col class="xl6926296" width="102" style="mso-width-source:userset;mso-width-alt: 3730;width:77pt"/>
          <col class="xl6926296" width="88" style="mso-width-source:userset;mso-width-alt: 4278;width:88pt"/>
          <col class="xl6926296" width="91" style="mso-width-source:userset;mso-width-alt: 3328;width:68pt"/>
          <col class="xl6926296" width="77" style="mso-width-source:userset;mso-width-alt: 3730;width:77pt"/>
          <col class="xl6926296" width="77" style="mso-width-source:userset;mso-width-alt: 4388;width:90pt"/>
          <tr class="xl10126296" height="23" style="height:17.25pt">
            <td colspan="13" height="23" class="xl13926296" width="1110" style="border-right:	1.0pt solid black;height:17.25pt;width:1110pt">
              National AIDS &amp; STI	Control Programme
            </td>
          </tr>
          <tr class="xl10126296" height="23" style="height:17.25pt">
            <td colspan="11" height="23" class="xl10926296" style="height:17.25pt">
              MOH 731 -
              Comprehensive HIV/AIDS Facility Reporting Form - NASCOP
            </td>
            <td colspan="2" class="xl11026296" style="border-right:1.0pt solid black">[MOH731]</td>
          </tr>
          <xsl:apply-templates select="Data/Results[@QUERY_NAME='MOH731_00']"  mode="reportheader" />

          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl7026296" align="right" style="height:14.25pt">1</td>
            <td colspan="3" class="xl7226296" style="border-right:1.0pt solid black">
              HIV Counselling<span style="mso-spacerun:yes">  </span>and Testing
            </td>
            <td rowspan="85" class="xl10026296" style="border-bottom:1.0pt solid black">&#160;</td>
            <td class="xl7126296">3</td>
            <td colspan="7" class="xl13526296" style="border-right:1.0pt solid black">
              Care and Treatment
            </td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl7426296" align="right" style="height:14.25pt">1.1</td>
            <td class="xl7526296">Testing</td>
            <td class="xl7526296">&#160;</td>
            <td class="xl11326296">Value</td>
            <td class="xl7726296">3.1</td>
            <td class="xl7826296">On Cotrimoxazole Prophylaxis</td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl7826296"></td>
            <td class="xl6926296"></td>
            <td class="xl7826296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl8026296" style="height:14.25pt">&#160;</td>
            <td class="xl6926296">First</td>
            <td class="xl6926296">HV01-01</td>
            <td class="xl11526296">
              <xsl:value-of select="$HV01-01" />
            </td>
            <td class="xl8226296">&#160;</td>
            <td class="xl6926296">HIV Exposed Infant(within 2 months)</td>
            <td class="xl6926296"></td>
            <td class="xl6926296">HV03-01</td>
            <td class="xl8126296" align="right">
              <xsl:value-of select="$HV03-01" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl7826296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl8026296" style="height:14.25pt">&#160;</td>
            <td class="xl6926296">Repeat</td>
            <td class="xl6926296">HV01-02</td>
            <td class="xl11526296" style="border-top:none">
              <xsl:value-of select="$HV01-02" />
            </td>
            <td class="xl8226296">&#160;</td>
            <td class="xl6926296" colspan="2">
              HIV Exposed Infant(Eligible for CTX at 2 months)
            </td>
            <td class="xl6926296">HV03-02</td>
            <td class="xl8126296" align="right" style="border-top:none">
              <xsl:value-of select="$HV03-02" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl7826296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="20" style="height:15.0pt">
            <td height="20" class="xl8026296" style="height:15.0pt">&#160;</td>
            <td class="xl8326296">Total Tested(HV01-01 plus HV01-02</td>
            <td class="xl8326296">HV01-03</td>
            <td class="xl11626296">
              <xsl:value-of select="$HV01-03" />
            </td>
            <td class="xl8226296">&#160;</td>
            <td class="xl8426296">On CTX - Below 15 Years</td>
            <td class="xl8426296">&#160;</td>
            <td class="xl8426296">HV03-03(M)</td>
            <td class="xl8126296" align="right" style="border-top:none">
              <xsl:value-of select="$HV03-03" />
            </td>
            <td class="xl8426296">HV03-04(F)</td>
            <td class="xl8126296" align="right">
              <xsl:value-of select="$HV03-04" />
            </td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="20" style="height:15.0pt">
            <td height="20" class="xl8026296" style="height:15.0pt">&#160;</td>
            <td class="xl8526296" style="border-top:none">Couples</td>
            <td class="xl8526296" style="border-top:none">HV01-05</td>
            <td class="xl11726296" style="border-top:none">
              <xsl:value-of select="$HV01-05" />
            </td>
            <td class="xl8226296">&#160;</td>
            <td class="xl8626296">On CTX - 15 years &amp; Older</td>
            <td class="xl8626296">&#160;</td>
            <td class="xl8626296">HV03-05(M)</td>
            <td class="xl8726296" align="right" style="border-top:none">
              <xsl:value-of select="$HV03-05" />
            </td>
            <td class="xl8626296">HV03-06(F)</td>
            <td class="xl8726296" align="right" style="border-top:none">
              <xsl:value-of select="$HV03-06" />
            </td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl8026296" style="height:14.25pt">&#160;</td>
            <td class="xl6926296">Static[Facility]</td>
            <td class="xl6926296">HV01-06</td>
            <td class="xl11826296">
              <xsl:value-of select="$HV01-06" />
            </td>
            <td class="xl8226296">&#160;</td>
            <td colspan="2" class="xl14126296">Total on CTX (Sum HV03-03 to HV03-06</td>
            <td class="xl8826296">HV03-07</td>
            <td class="xl8826296" align="right">
              <xsl:value-of select="$HV03-07" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl7826296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl8026296" style="height:14.25pt">&#160;</td>
            <td class="xl6926296">Outreach</td>
            <td class="xl6926296">HV01-07</td>
            <td class="xl11526296" style="border-top:none">
              <xsl:value-of select="$HV01-07" />
            </td>
            <td class="xl8226296">&#160;</td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl7826296"></td>
            <td class="xl6926296"></td>
            <td class="xl7826296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl8026296" style="height:14.25pt">&#160;</td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl11926296">&#160;</td>
            <td class="xl8926296">3.2</td>
            <td colspan="7" class="xl13326296" style="border-right:1.0pt solid black">
              Enrolled  in Care
            </td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl7426296" align="right" style="height:14.25pt">1.2</td>
            <td class="xl7526296">Receiving results -(Couples only)</td>
            <td class="xl7526296">&#160;</td>
            <td class="xl11326296">&#160;</td>
            <td class="xl8226296">&#160;</td>
            <td class="xl6926296">Enrolled in Care - Below 1 year</td>
            <td class="xl6926296"></td>
            <td class="xl6926296">HV03-08</td>
            <td class="xl8126296" align="right">
              <xsl:value-of select="$HV03-08" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl7826296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl8026296" style="height:14.25pt">&#160;</td>
            <td class="xl6926296">Cocordant Couples</td>
            <td class="xl6926296">HV01-08</td>
            <td class="xl11526296">
              <xsl:value-of select="$HV01-08" />
            </td>
            <td class="xl8226296">&#160;</td>
            <td class="xl6926296">Enrolled in Care - Below 15 years</td>
            <td class="xl6926296"></td>
            <td class="xl6926296">HV03-09 (M)</td>
            <td class="xl8126296" align="right" style="border-top:none">
              <xsl:value-of select="$HV03-09" />
            </td>
            <td class="xl6926296">HV03-10 (F)</td>
            <td class="xl8126296" align="right">
              <xsl:value-of select="$HV03-10" />
            </td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="20" style="height:15.0pt">
            <td height="20" class="xl8026296" style="height:15.0pt">&#160;</td>
            <td class="xl6926296">DiscordantCouples</td>
            <td class="xl6926296">HV01-09</td>
            <td class="xl11526296" style="border-top:none">
              <xsl:value-of select="$HV01-09" />
            </td>
            <td class="xl8226296">&#160;</td>
            <td class="xl8626296">Enrolled in Care - 15 years &amp; older</td>
            <td class="xl8626296">&#160;</td>
            <td class="xl8626296">HV03-11 (M)</td>
            <td class="xl8726296" align="right" style="border-top:none">
              <xsl:value-of select="$HV03-11" />
            </td>
            <td class="xl8626296">HV03-12 (F)</td>
            <td class="xl8726296" align="right" style="border-top:none">
              <xsl:value-of select="$HV03-12" />
            </td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="20" style="height:15.0pt">
            <td height="20" class="xl9026296" style="height:15.0pt">&#160;</td>
            <td class="xl8626296">&#160;</td>
            <td class="xl8626296">&#160;</td>
            <td class="xl12026296">&#160;</td>
            <td class="xl8226296">&#160;</td>
            <td colspan="2" class="xl14126296">
              Enrolled in Care - Total (Sum HV03-09 to  HV03-12)
            </td>
            <td class="xl8826296">HV03-13</td>
            <td class="xl8826296" align="right">
              <xsl:value-of select="$HV03-13" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl7826296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl7026296" align="right" style="height:14.25pt">1.3</td>
            <td class="xl7226296">Receiving Positive Results</td>
            <td class="xl7226296">&#160;</td>
            <td class="xl12126296">&#160;</td>
            <td class="xl8226296">&#160;</td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl7826296"></td>
            <td class="xl6926296"></td>
            <td class="xl7826296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl8026296" style="height:14.25pt">&#160;</td>
            <td class="xl6926296">Males - Below 15 years</td>
            <td class="xl6926296">HV01-10</td>
            <td class="xl11526296">
              <xsl:value-of select="$HV01-10" />
            </td>
            <td class="xl8926296">3.3</td>
            <td colspan="7" class="xl13126296" style="border-right:1.0pt solid black">
              Currently in Care - (from the tally sheet- this month only and from last 2 months
            </td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl8026296" style="height:14.25pt">&#160;</td>
            <td class="xl6926296">Females - Below 15 years</td>
            <td class="xl6926296">HV01-11</td>
            <td class="xl11526296" style="border-top:none">
              <xsl:value-of select="$HV01-11" />
            </td>
            <td class="xl8226296">&#160;</td>
            <td class="xl6926296">
              Currently in Care - Below 1 year<span	style="mso-spacerun:yes"> </span>
            </td>
            <td class="xl6926296"></td>
            <td class="xl6926296">HV03-14</td>
            <td class="xl8126296" align="right">
              <xsl:value-of select="$HV03-14" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl7826296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl8026296" style="height:14.25pt">&#160;</td>
            <td class="xl6926296">Males - 15 to 24 years</td>
            <td class="xl6926296">HV01-12</td>
            <td class="xl11526296" style="border-top:none">
              <xsl:value-of select="$HV01-12" />
            </td>
            <td class="xl8226296">&#160;</td>
            <td class="xl6926296">
              Currently in Care - Below 15 years<span	style="mso-spacerun:yes"> </span>
            </td>
            <td class="xl6926296"></td>
            <td class="xl6926296">HV03-15 (M)</td>
            <td class="xl8126296" align="right" style="border-top:none">
              <xsl:value-of select="$HV03-15" />
            </td>
            <td class="xl6926296">HV03-16(F)</td>
            <td class="xl8126296" align="right">
              <xsl:value-of select="$HV03-16" />
            </td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl8026296" style="height:14.25pt">&#160;</td>
            <td class="xl6926296">Females - 15 to 24 years</td>
            <td class="xl6926296">HV01-13</td>
            <td class="xl11526296" style="border-top:none">
              <xsl:value-of select="$HV01-13" />
            </td>
            <td class="xl8226296">&#160;</td>
            <td class="xl6926296">
              Currently in Care - 15 years &amp; older<span	style="mso-spacerun:yes"> </span>
            </td>
            <td class="xl6926296"></td>
            <td class="xl6926296">HV03-17 (M)</td>
            <td class="xl8126296" align="right" style="border-top:none">
              <xsl:value-of select="$HV03-17" />
            </td>
            <td class="xl6926296">HV03-18(F)</td>
            <td class="xl8126296" align="right" style="border-top:none">
              <xsl:value-of select="$HV03-18" />
            </td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl8026296" style="height:14.25pt">&#160;</td>
            <td class="xl6926296">Males - 25 years &amp; older</td>
            <td class="xl6926296">HV01-14</td>
            <td class="xl11526296" style="border-top:none">
              <xsl:value-of select="$HV01-14" />
            </td>
            <td class="xl8226296">&#160;</td>
            <td colspan="2" class="xl14226296">
              Currently in Care - Total (Sum HV03-15 to HV03-18)<span style="mso-spacerun:yes"> </span>
            </td>
            <td class="xl8826296">HV03-19</td>
            <td class="xl8826296" align="right">
              <xsl:value-of select="$HV03-19" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl7826296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="20" style="height:15.0pt">
            <td height="20" class="xl8026296" style="height:15.0pt">&#160;</td>
            <td class="xl8626296">Females - 25 years &amp; older</td>
            <td class="xl8626296">HV01-15</td>
            <td class="xl12226296" style="border-top:none">
              <xsl:value-of select="$HV01-15" />
            </td>
            <td class="xl8226296">&#160;</td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl7826296"></td>
            <td class="xl6926296"></td>
            <td class="xl7826296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl8026296" style="height:14.25pt">&#160;</td>
            <td class="xl8826296">Total receiving positive results (Sum HV01-10 to 15)</td>
            <td class="xl8826296">HV01-16</td>
            <td class="xl12326296">
              <xsl:value-of select="$HV01-16" />
            </td>
            <td class="xl8926296">3.4</td>
            <td colspan="7" class="xl13326296" style="border-right:1.0pt solid black">
              Starting
              ART
            </td>
          </tr>
          <tr height="20" style="height:15.0pt">
            <td height="20" class="xl9026296" style="height:15.0pt">&#160;</td>
            <td class="xl8626296">&#160;</td>
            <td class="xl8626296">&#160;</td>
            <td class="xl12026296">&#160;</td>
            <td class="xl8226296">&#160;</td>
            <td class="xl6926296">Starting ART - Below 1 year</td>
            <td class="xl6926296"></td>
            <td class="xl6926296">HV03-20</td>
            <td class="xl8126296" align="right">
              <xsl:value-of select="$HV03-20" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl7826296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl9226296" align="right" style="height:14.25pt;border-top:	none">2</td>
            <td class="xl9326296" style="border-top:none">
              Prevention of Mother-to-Child transmission
            </td>
            <td class="xl9326296" style="border-top:none">&#160;</td>
            <td class="xl12426296" style="border-top:none">&#160;</td>
            <td class="xl8226296">&#160;</td>
            <td class="xl6926296">
              Starting ART - Below 15 years<span	style="mso-spacerun:yes"> </span>
            </td>
            <td class="xl6926296"></td>
            <td class="xl6926296">HV03-21 (M)</td>
            <td class="xl8126296" align="right" style="border-top:none">
              <xsl:value-of select="$HV03-21" />
            </td>
            <td class="xl6926296">HV03-22(F)</td>
            <td class="xl8126296" align="right">
              <xsl:value-of select="$HV03-22" />
            </td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="20" style="height:15.0pt">
            <td height="20" class="xl7426296" align="right" style="height:15.0pt">2.1</td>
            <td class="xl7526296">Testing for HIV</td>
            <td class="xl7526296">&#160;</td>
            <td class="xl11326296">&#160;</td>
            <td class="xl8226296">&#160;</td>
            <td class="xl8626296">
              Starting ART - 15 years &amp; older<span	style="mso-spacerun:yes"> </span>
            </td>
            <td class="xl8626296">&#160;</td>
            <td class="xl8626296">HV03-23 (M)</td>
            <td class="xl8726296" align="right" style="border-top:none">
              <xsl:value-of select="$HV03-23" />
            </td>
            <td class="xl8626296">HV03-24(F)</td>
            <td class="xl8726296" align="right" style="border-top:none">
              <xsl:value-of select="$HV03-24" />
            </td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="21" style="mso-height-source:userset;height:15.75pt">
            <td height="21" class="xl8026296" style="height:15.75pt">&#160;</td>
            <td class="xl6926296">Antenatal</td>
            <td class="xl6926296">HV02-01</td>
            <td class="xl11526296">
              <xsl:value-of select="$HV02-01" />
            </td>
            <td class="xl8226296">&#160;</td>
            <td colspan="2" class="xl14326296">Starting ART - Total (Sum HV03-21 to HV03-24)</td>
            <td class="xl8326296">HV03-25</td>
            <td class="xl8326296" align="right">
              <xsl:value-of select="$HV03-25" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl7826296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl8026296" style="height:14.25pt">&#160;</td>
            <td class="xl6926296">Labour and Delivery</td>
            <td class="xl6926296">HV02-02</td>
            <td class="xl11526296" style="border-top:none">
              <xsl:value-of select="$HV02-02" />
            </td>
            <td class="xl8226296">&#160;</td>
            <td class="xl6926296">
              Starting - Pregnant<span	style="mso-spacerun:yes"> </span>
            </td>
            <td class="xl6926296"></td>
            <td class="xl6926296">HV03-26</td>
            <td class="xl8126296" align="right">
              <xsl:value-of select="$HV03-26" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl7826296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="20" style="height:15.0pt">
            <td height="20" class="xl8026296" style="height:15.0pt">&#160;</td>
            <td class="xl8626296">Postnatal (within 72hrs)</td>
            <td class="xl8626296">HV02-03</td>
            <td class="xl12226296" style="border-top:none">
              <xsl:value-of select="$HV02-03" />
            </td>
            <td class="xl8226296">&#160;</td>
            <td class="xl6926296">
              Starting - TB Patient<span	style="mso-spacerun:yes"> </span>
            </td>
            <td class="xl6926296"></td>
            <td class="xl6926296">HV03-27</td>
            <td class="xl8126296" align="right" style="border-top:none">
              <xsl:value-of select="$HV03-27" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl7826296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl8026296" style="height:14.25pt">&#160;</td>
            <td class="xl8826296">Total tested(PMTCT) (Sum HV02-01 to HV02-03)</td>
            <td class="xl8826296">HV02-04</td>
            <td class="xl12326296">
              <xsl:value-of select="$HV02-04" />
            </td>
            <td class="xl8226296">&#160;</td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl7826296"></td>
            <td class="xl6926296"></td>
            <td class="xl7826296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl8026296" style="height:14.25pt">&#160;</td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl11926296">&#160;</td>
            <td class="xl8926296">3.5</td>
            <td colspan="7" class="xl13326296" style="border-right:1.0pt solid black">
              Revisits
              on ART (from the tally sheet- this month only and from last 2 months )
            </td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl7426296" align="right" style="height:14.25pt">2.2</td>
            <td class="xl7526296">HIV Positive Results</td>
            <td class="xl7526296">&#160;</td>
            <td class="xl11326296">&#160;</td>
            <td class="xl8226296">&#160;</td>
            <td class="xl6926296">Revisit on ART - Below 1 year</td>
            <td class="xl6926296"></td>
            <td class="xl6926296">HV03-28</td>
            <td class="xl8126296" align="right">
              <xsl:value-of select="$HV03-28" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl7826296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl8026296" style="height:14.25pt">&#160;</td>
            <td class="xl6926296">Known positive status (at entry into ANC)</td>
            <td class="xl6926296">HV02-05</td>
            <td class="xl11526296">
              <xsl:value-of select="$HV02-05" />
            </td>
            <td class="xl8226296">&#160;</td>
            <td class="xl6926296">
              Revisit on ART - Below 15 years<span	style="mso-spacerun:yes"> </span>
            </td>
            <td class="xl6926296"></td>
            <td class="xl6926296">HV03-29 (M)</td>
            <td class="xl8126296" align="right" style="border-top:none">
              <xsl:value-of select="$HV03-29" />
            </td>
            <td class="xl6926296">HV03-30(F)</td>
            <td class="xl8126296" align="right">
              <xsl:value-of select="$HV03-30" />
            </td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="20" style="height:15.0pt">
            <td height="20" class="xl8026296" style="height:15.0pt">&#160;</td>
            <td class="xl6926296">Antenatal</td>
            <td class="xl6926296">HV02-06</td>
            <td class="xl11526296" style="border-top:none">
              <xsl:value-of select="$HV02-06" />
            </td>
            <td class="xl8226296">&#160;</td>
            <td class="xl8626296">
              Revisit on ART - 15 years &amp; older<span	style="mso-spacerun:yes"> </span>
            </td>
            <td class="xl8626296">&#160;</td>
            <td class="xl8626296">HV03-31 (M)</td>
            <td class="xl8726296" align="right" style="border-top:none">
              <xsl:value-of select="$HV03-31" />
            </td>
            <td class="xl8626296">HV03-32 (F)</td>
            <td class="xl8726296" align="right" style="border-top:none">
              <xsl:value-of select="$HV03-32" />
            </td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl8026296" style="height:14.25pt">&#160;</td>
            <td class="xl6926296">Labout and Delivery</td>
            <td class="xl6926296">HV02-07</td>
            <td class="xl11526296" style="border-top:none">
              <xsl:value-of select="$HV02-07" />
            </td>
            <td class="xl8226296">&#160;</td>
            <td colspan="2" class="xl14126296">
              Total Revisit on ART (Sum HV03-29 to
              HV03-32)<span style="mso-spacerun:yes"> </span>
            </td>
            <td class="xl8826296">HV03-33</td>
            <td class="xl8826296" align="right">
              <xsl:value-of select="$HV03-33" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl7826296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="20" style="height:15.0pt">
            <td height="20" class="xl8026296" style="height:15.0pt">&#160;</td>
            <td class="xl8626296">Postnatal (within 72hrs)</td>
            <td class="xl8626296">HV02-08</td>
            <td class="xl12226296" style="border-top:none">
              <xsl:value-of select="$HV02-08" />
            </td>
            <td class="xl8226296">&#160;</td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl7826296"></td>
            <td class="xl6926296"></td>
            <td class="xl7826296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="20" style="height:15.0pt">
            <td height="20" class="xl8026296" style="height:15.0pt">&#160;</td>
            <td class="xl8326296">Total Positive (PMTCT) (Sum HV02-05 to HV02-08)</td>
            <td class="xl8326296">HV02-09</td>
            <td class="xl11626296">
              <xsl:value-of select="$HV02-09" />
            </td>
            <td class="xl8926296">3.6</td>
            <td colspan="7" class="xl13326296" style="border-right:1.0pt solid black">
              Currently on ART [All] - (Add 3.4 and 3.5 e.g. HV03-34 = HV03-20 + HV03-28)
            </td>
          </tr>
          <tr height="20" style="height:15.0pt">
            <td height="20" class="xl8026296" style="height:15.0pt">&#160;</td>
            <td class="xl8326296">Total with known status (HV02-04 plus HV02-05)</td>
            <td class="xl8326296">HV02-10</td>
            <td class="xl11626296">
              <xsl:value-of select="$HV02-10" />
            </td>
            <td class="xl8226296">&#160;</td>
            <td class="xl6926296">
              Currently on ART - Below 1 year<span	style="mso-spacerun:yes"> </span>
            </td>
            <td class="xl6926296"></td>
            <td class="xl6926296">HV03-34</td>
            <td class="xl9526296" align="right">
              <xsl:value-of select="$HV03-34" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl7826296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl8026296" style="height:14.25pt">&#160;</td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl11926296">&#160;</td>
            <td class="xl8226296">&#160;</td>
            <td class="xl6926296">
              Currently on ART - Below 15 years<span	style="mso-spacerun:yes"> </span>
            </td>
            <td class="xl6926296"></td>
            <td class="xl6926296">HV03-35 (M)</td>
            <td class="xl9526296" align="right" style="border-top:none">
              <xsl:value-of select="$HV03-35" />
            </td>
            <td class="xl6926296">HV03-36 (F)</td>
            <td class="xl9526296" align="right">
              <xsl:value-of select="$HV03-36" />
            </td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="20" style="height:15.0pt">
            <td height="20" class="xl7426296" align="right" style="height:15.0pt">2.3</td>
            <td class="xl7526296">Partner Involvement</td>
            <td class="xl7526296">&#160;</td>
            <td class="xl11326296">&#160;</td>
            <td class="xl8226296">&#160;</td>
            <td class="xl8626296">
              Currently on ART - 15 years &amp; older<span	style="mso-spacerun:yes"> </span>
            </td>
            <td class="xl8626296">&#160;</td>
            <td class="xl8626296">HV03-37 (M)</td>
            <td class="xl9626296" align="right" style="border-top:none">
              <xsl:value-of select="$HV03-37" />
            </td>
            <td class="xl8626296">HV03-38 (F)</td>
            <td class="xl9626296" align="right" style="border-top:none">
              <xsl:value-of select="$HV03-38" />
            </td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl8026296" style="height:14.25pt">&#160;</td>
            <td class="xl6926296">Male partners tested - (ANC/L&amp;D)</td>
            <td class="xl6926296">HV02-11</td>
            <td class="xl11526296">
              <xsl:value-of select="$HV02-11" />
            </td>
            <td class="xl8226296">&#160;</td>
            <td colspan="2" class="xl14126296">
              Total Current on ART (Sum HV03-35 to HV03-38)<span style="mso-spacerun:yes"> </span>
            </td>
            <td class="xl8826296">HV03-39</td>
            <td class="xl8826296" align="right">
              <xsl:value-of select="$HV03-39" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl7826296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl8026296" style="height:14.25pt">&#160;</td>
            <td class="xl6926296">Discordant Couples</td>
            <td class="xl6926296">HV02-12</td>
            <td class="xl11526296" style="border-top:none">
              <xsl:value-of select="$HV02-12" />
            </td>
            <td class="xl8226296">&#160;</td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl7826296"></td>
            <td class="xl6926296"></td>
            <td class="xl7826296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl8026296" style="height:14.25pt">&#160;</td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl11926296">&#160;</td>
            <td class="xl8926296">3.7</td>
            <td colspan="7" class="xl13326296" style="border-right:1.0pt solid black">
              Cumulative  Ever on ART
            </td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl7426296" align="right" style="height:14.25pt">2.4</td>
            <td class="xl7526296">Maternal Prophylaxis (at first contact only)</td>
            <td class="xl7526296">&#160;</td>
            <td class="xl11326296">&#160;</td>
            <td class="xl8226296">&#160;</td>
            <td class="xl6926296">
              Ever on ART - Below 15 years<span style="mso-spacerun:yes"> </span>
            </td>
            <td class="xl6926296"></td>
            <td class="xl6926296">HV03-40 (M)</td>
            <td class="xl8126296" align="right">
              <xsl:value-of select="$HV03-40" />
            </td>
            <td class="xl6926296">HV03-41 (F)</td>
            <td class="xl8126296" align="right">
              <xsl:value-of select="$HV03-41" />
            </td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="20" style="height:15.0pt">
            <td height="20" class="xl8026296" style="height:15.0pt">&#160;</td>
            <td class="xl6926296">Prophylaxis - NVP Only</td>
            <td class="xl6926296">HV02-13</td>
            <td class="xl11526296">
              <xsl:value-of select="$HV02-13" />
            </td>
            <td class="xl8226296">&#160;</td>
            <td class="xl8626296">
              Ever on ART - 15 years &amp; older<span	style="mso-spacerun:yes"> </span>
            </td>
            <td class="xl8626296">&#160;</td>
            <td class="xl8626296">HV03-42 (M)</td>
            <td class="xl8726296" align="right" style="border-top:none">
              <xsl:value-of select="$HV03-42" />
            </td>
            <td class="xl8626296">HV03-43 (F)</td>
            <td class="xl8726296" align="right" style="border-top:none">
              <xsl:value-of select="$HV03-43" />
            </td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl8026296" style="height:14.25pt">&#160;</td>
            <td class="xl6926296">Prophylaxis - (AZT + SdNVP)</td>
            <td class="xl6926296">HV02-14</td>
            <td class="xl11526296" style="border-top:none">
              <xsl:value-of select="$HV02-14" />
            </td>
            <td class="xl8226296">&#160;</td>
            <td colspan="2" class="xl14126296">
              Total Ever on ART (Sum HV03-40 to
              HV03-43)<span style="mso-spacerun:yes"> </span>
            </td>
            <td class="xl8826296">HV03-44</td>
            <td class="xl8826296" align="right">
              <xsl:value-of select="$HV03-44" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl7826296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl8026296" style="height:14.25pt">&#160;</td>
            <td class="xl6926296">Prophylaxis - Interrupted HAART</td>
            <td class="xl6926296">HV02-15</td>
            <td class="xl11526296" style="border-top:none">
              <xsl:value-of select="$HV02-15" />
            </td>
            <td class="xl8226296">&#160;</td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl7826296"></td>
            <td class="xl6926296"></td>
            <td class="xl7826296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="20" style="height:15.0pt">
            <td height="20" class="xl8026296" style="height:15.0pt">&#160;</td>
            <td class="xl8626296">HAART (ART)</td>
            <td class="xl8626296">HV02-16</td>
            <td class="xl12226296" style="border-top:none">
              <xsl:value-of select="$HV02-16" />
            </td>
            <td class="xl8926296">3.8</td>
            <td colspan="7" class="xl13326296" style="border-right:1.0pt solid black">
              Survival and Retention on ART at 12 months
            </td>
          </tr>
          <tr height="20" style="height:15.0pt">
            <td height="20" class="xl8026296" style="height:15.0pt">&#160;</td>
            <td class="xl8826296">Total PMTCT prophylaxis (Sum HV02-13 to HV02-16)</td>
            <td class="xl8826296">HV02-17</td>
            <td class="xl12326296">
              <xsl:value-of select="$HV02-17" />
            </td>
            <td class="xl8226296">&#160;</td>
            <td class="xl8626296">
              ART Net Cohort at 12 months<span style="mso-spacerun:yes"> </span>
            </td>
            <td class="xl8626296">&#160;</td>
            <td class="xl8626296">HV03-45</td>
            <td class="xl8126296" align="right">
              <xsl:value-of select="$HV03-45" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl7826296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl8026296" style="height:14.25pt">&#160;</td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl11926296">&#160;</td>
            <td class="xl8226296">&#160;</td>
            <td class="xl6926296">On Original 1st Line at 12 months</td>
            <td class="xl6926296"></td>
            <td class="xl6926296">HV03-46</td>
            <td class="xl8126296" align="right" style="border-top:none">
              <xsl:value-of select="$HV03-46" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl7826296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl7426296" align="right" style="height:14.25pt">2.5</td>
            <td class="xl7526296">Assessment for ART Eligibility in MCH(at diagnosis)</td>
            <td class="xl7526296">&#160;</td>
            <td class="xl11326296">&#160;</td>
            <td class="xl8226296">&#160;</td>
            <td class="xl6926296">On alternative 1st Line at 12 months<span	style="mso-spacerun:yes"> </span>
            </td>
            <td class="xl6926296"></td>
            <td class="xl6926296">HV03-47</td>
            <td class="xl8126296" align="right" style="border-top:none">
              <xsl:value-of select="$HV03-47" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl7826296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="20" style="height:15.0pt">
            <td height="20" class="xl8026296" style="height:15.0pt">&#160;</td>
            <td class="xl6926296">Assessed for eligibility at 1st ANC - WHO Staging done</td>
            <td class="xl6926296">HV02-18</td>
            <td class="xl11526296">
              <xsl:value-of select="$HV02-18" />
            </td>
            <td class="xl8226296">&#160;</td>
            <td class="xl8626296">
              On 2nd Line (or higher) at 12 months<span	style="mso-spacerun:yes"> </span>
            </td>
            <td class="xl8626296">&#160;</td>
            <td class="xl8626296">HV03-48</td>
            <td class="xl8726296" align="right" style="border-top:none">
              <xsl:value-of select="$HV03-48" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl7826296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="20" style="height:15.0pt">
            <td height="20" class="xl8026296" style="height:15.0pt">&#160;</td>
            <td class="xl8626296">Assessed for eligibility at 1st ANC - CD4</td>
            <td class="xl8626296">HV02-19</td>
            <td class="xl12226296" style="border-top:none">
              <xsl:value-of select="$HV02-19" />
            </td>
            <td class="xl8226296">&#160;</td>
            <td colspan="2" class="xl14126296">On therapy at 12 months (Sum HV03-46 to HV03-48)<span style="mso-spacerun:yes"> </span>
            </td>
            <td class="xl8826296">HV03-49</td>
            <td class="xl8826296" align="right">
              <xsl:value-of select="$HV03-49" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl7826296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="20" style="height:15.0pt">
            <td height="20" class="xl8026296" style="height:15.0pt">&#160;</td>
            <td class="xl8326296">Assessed for eligibility in ANC (Sum HV02 - 18 to HV02 -
              19)
            </td>
            <td class="xl8326296">HV02-20</td>
            <td class="xl11626296">
              <xsl:value-of select="$HV02-20" />
            </td>
            <td class="xl8226296">&#160;</td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl7826296"></td>
            <td class="xl6926296"></td>
            <td class="xl7826296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl8026296" style="height:14.25pt">&#160;</td>
            <td class="xl6926296">Started on ART during ANC<span	style="mso-spacerun:yes"> </span>
            </td>
            <td class="xl6926296">HV02-21</td>
            <td class="xl11826296">
              <xsl:value-of select="$HV02-21" />
            </td>
            <td class="xl8926296">3.9</td>
            <td colspan="7" class="xl13326296" style="border-right:1.0pt solid black">Screening</td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl8026296" style="height:14.25pt">&#160;</td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl11926296">&#160;</td>
            <td class="xl8226296">&#160;</td>
            <td class="xl6926296">Screened for TB - Below 15 years<span	style="mso-spacerun:yes"> </span>
            </td>
            <td class="xl6926296"></td>
            <td class="xl6926296">HV03-50 (M)</td>
            <td class="xl8126296" align="right">
              <xsl:value-of select="$HV03-50" />
            </td>
            <td class="xl6926296">HV03-51 (F)</td>
            <td class="xl8126296" align="right">
              <xsl:value-of select="$HV03-51" />
            </td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="20" style="height:15.0pt">
            <td height="20" class="xl7426296" align="right" style="height:15.0pt">2.6</td>
            <td class="xl7526296">Infant Testing (Initial tests only)</td>
            <td class="xl7526296">&#160;</td>
            <td class="xl11326296">&#160;</td>
            <td class="xl8226296">&#160;</td>
            <td class="xl8626296">Screened for TB - 15 years &amp; older<span	style="mso-spacerun:yes"> </span>
            </td>
            <td class="xl8626296">&#160;</td>
            <td class="xl8626296">HV03-52 (M)</td>
            <td class="xl8726296" align="right" style="border-top:none">
              <xsl:value-of select="$HV03-52" />
            </td>
            <td class="xl8626296">HV03-53 (F)</td>
            <td class="xl8726296" align="right" style="border-top:none">
              <xsl:value-of select="$HV03-53" />
            </td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="21" style="mso-height-source:userset;height:15.75pt">
            <td height="21" class="xl8026296" style="height:15.75pt">&#160;</td>
            <td class="xl6926296">PCR (within 2 months)</td>
            <td class="xl6926296">HV02-24</td>
            <td class="xl11526296">
              <xsl:value-of select="$HV02-24" />
            </td>
            <td class="xl8226296">&#160;</td>
            <td colspan="2" class="xl14326296">Total Screened for TB (Sum HV03-50 to -53)<span style="mso-spacerun:yes"> </span>
            </td>
            <td class="xl8326296">HV03-54</td>
            <td class="xl8326296" align="right">
              <xsl:value-of select="$HV03-54" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl7826296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl8026296" style="height:14.25pt">&#160;</td>
            <td class="xl6926296">PCR (from 3 to 8 months)</td>
            <td class="xl6926296">HV02-25</td>
            <td class="xl11526296" style="border-top:none">
              <xsl:value-of select="$HV02-25" />
            </td>
            <td class="xl8226296">&#160;</td>
            <td class="xl6926296">Screened for cervical cancer (F 18+)<span	style="mso-spacerun:yes"> </span>
            </td>
            <td class="xl6926296"></td>
            <td class="xl6926296">HV03-55</td>
            <td class="xl8126296" align="right">
              <xsl:value-of select="$HV03-55" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl7826296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl8026296" style="height:14.25pt">&#160;</td>
            <td class="xl6926296">Serology antibody test (from 9 to 12 months)</td>
            <td class="xl6926296">HV02-26</td>
            <td class="xl11526296" style="border-top:none">
              <xsl:value-of select="$HV02-26" />
            </td>
            <td class="xl8226296">&#160;</td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl7826296"></td>
            <td class="xl6926296"></td>
            <td class="xl7826296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="20" style="height:15.0pt">
            <td height="20" class="xl8026296" style="height:15.0pt">&#160;</td>
            <td class="xl8626296">PCR ( from 9 to 12 months)</td>
            <td class="xl8626296">HV02-27</td>
            <td class="xl12226296" style="border-top:none">
              <xsl:value-of select="$HV02-27" />
            </td>
            <td class="xl8926296">3.10</td>
            <td colspan="7" class="xl13326296" style="border-right:1.0pt solid black">
              Prevention   with Positives
            </td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl8026296" style="height:14.25pt">&#160;</td>
            <td class="xl8826296">Total HEI Tested by 12 months (Sum HV02-24 to HV02-26)</td>
            <td class="xl8826296">HV02-28</td>
            <td class="xl12326296">
              <xsl:value-of select="$HV02-28" />
            </td>
            <td class="xl8226296">&#160;</td>
            <td class="xl6926296">
              Modern contraceptive methods<span	style="mso-spacerun:yes"> </span>
            </td>
            <td class="xl6926296"></td>
            <td class="xl6926296">HV09-04</td>
            <td class="xl8126296" align="right">
              <xsl:value-of select="$HV09-04" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl7826296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl8026296" style="height:14.25pt">&#160;</td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl11926296">&#160;</td>
            <td class="xl8226296">&#160;</td>
            <td class="xl6926296">Provided with condoms<span style="mso-spacerun:yes"> </span>
            </td>
            <td class="xl6926296"></td>
            <td class="xl6926296">HV09-05</td>
            <td class="xl8126296" align="right" style="border-top:none">
              <xsl:value-of select="$HV09-05" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl7826296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl7426296" align="right" style="height:14.25pt">2.7</td>
            <td class="xl7526296">Confirmed Infant Tests Results</td>
            <td class="xl7526296">&#160;</td>
            <td class="xl11326296">&#160;</td>
            <td class="xl8226296">&#160;</td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl7826296"></td>
            <td class="xl6926296"></td>
            <td class="xl7826296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl8026296" style="height:14.25pt">&#160;</td>
            <td class="xl6926296">Positive - (within 2 months) - PCR</td>
            <td class="xl6926296">HV02-29</td>
            <td class="xl11526296">
              <xsl:value-of select="$HV02-29" />
            </td>
            <td class="xl8926296">3.11</td>
            <td colspan="7" class="xl13326296" style="border-right:1.0pt solid black">
              HIV  Care Visits
            </td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl8026296" style="height:14.25pt">&#160;</td>
            <td class="xl6926296">Positive - (3-8 months) PCR</td>
            <td class="xl6926296">HV02-30</td>
            <td class="xl11526296" style="border-top:none">
              <xsl:value-of select="$HV02-30" />
            </td>
            <td class="xl8226296">&#160;</td>
            <td class="xl6926296">
              Females (18+)<span style="mso-spacerun:yes"> </span>
            </td>
            <td class="xl6926296"></td>
            <td class="xl6926296">HV03-70</td>
            <td class="xl8126296" align="right">
              <xsl:value-of select="$HV03-70" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl7826296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="20" style="height:15.0pt">
            <td height="20" class="xl8026296" style="height:15.0pt">&#160;</td>
            <td class="xl8626296">Positive - (9-12months) - PCR</td>
            <td class="xl8626296">HV02-31</td>
            <td class="xl12226296" style="border-top:none">
              <xsl:value-of select="$HV02-31" />
            </td>
            <td class="xl8226296">&#160;</td>
            <td class="xl6926296">
              Scheduled<span style="mso-spacerun:yes"> </span>
            </td>
            <td class="xl6926296"></td>
            <td class="xl6926296">HV03-71</td>
            <td class="xl8126296" align="right" style="border-top:none">
              <xsl:value-of select="$HV03-71" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl7826296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="20" style="height:15.0pt">
            <td height="20" class="xl8026296" style="height:15.0pt">&#160;</td>
            <td class="xl8826296">Total Confirmed Positive (Sum HV02-29 t0 HV02-31)</td>
            <td class="xl8826296">HV02-32</td>
            <td class="xl12326296">
              <xsl:value-of select="$HV02-32" />
            </td>
            <td class="xl8226296">&#160;</td>
            <td class="xl8626296">
              Unscheduled<span style="mso-spacerun:yes"> </span>
            </td>
            <td class="xl8626296">&#160;</td>
            <td class="xl8626296">HV03-72</td>
            <td class="xl8726296" align="right" style="border-top:none">
              <xsl:value-of select="$HV03-72" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl7826296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl8026296" style="height:14.25pt">&#160;</td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl11926296">&#160;</td>
            <td class="xl8226296">&#160;</td>
            <td colspan="2" class="xl14126296">Total visits (HV03-71 &amp; -72)</td>
            <td class="xl8826296">HV03-73</td>
            <td class="xl8826296" align="right">
              <xsl:value-of select="$HV03-73" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl7826296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="20" style="height:15.0pt">
            <td height="20" class="xl7426296" align="right" style="height:15.0pt">2.8</td>
            <td class="xl7526296">Infant Feeding</td>
            <td class="xl7526296">&#160;</td>
            <td class="xl11326296">&#160;</td>
            <td class="xl9726296">&#160;</td>
            <td class="xl8626296">&#160;</td>
            <td class="xl8626296">&#160;</td>
            <td class="xl8626296">&#160;</td>
            <td class="xl9826296">&#160;</td>
            <td class="xl8626296">&#160;</td>
            <td class="xl9826296">&#160;</td>
            <td class="xl9126296">&#160;</td>
          </tr>
          <tr height="20" style="mso-height-source:userset;height:15.0pt">
            <td height="20" class="xl8026296" style="height:15.0pt">&#160;</td>
            <td class="xl6926296">EBF (at 6 months)</td>
            <td class="xl6926296">HV02-33</td>
            <td class="xl11526296">
              <xsl:value-of select="$HV02-33" />
            </td>
            <td class="xl9926296" style="border-top:none">4</td>
            <td colspan="7" class="xl13526296" style="border-right:1.0pt solid black">
              Voluntary Medical Male Circumcision
            </td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl8026296" style="height:14.25pt">&#160;</td>
            <td class="xl6926296">ERF (at 6 months)</td>
            <td class="xl6926296">HV02-34</td>
            <td class="xl11526296" style="border-top:none">
              <xsl:value-of select="$HV02-34" />
            </td>
            <td class="xl7026296" align="right">4.1</td>
            <td colspan="7" class="xl13726296" style="border-right:1.0pt solid black">
              Number Circumcised
            </td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl8026296" style="height:14.25pt">&#160;</td>
            <td class="xl6926296">MF (at 6 months)</td>
            <td class="xl6926296">HV02-35</td>
            <td class="xl11526296" style="border-top:none">
              <xsl:value-of select="$HV02-35" />
            </td>
            <td class="xl8026296">&#160;</td>
            <td class="xl6926296">0-14</td>
            <td class="xl6926296"></td>
            <td class="xl6926296">HV04-01</td>
            <td class="xl8126296" align="right">
              <xsl:value-of select="$HV04-01" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="20" style="height:15.0pt">
            <td height="20" class="xl8026296" style="height:15.0pt">&#160;</td>
            <td class="xl8326296">Total Exposed aged 6 months(Sum HV02-33 to HV02-35)</td>
            <td class="xl8326296">HV02-36</td>
            <td class="xl11626296">
              <xsl:value-of select="$HV02-36" />
            </td>
            <td class="xl8026296">&#160;</td>
            <td class="xl6926296">15-24</td>
            <td class="xl6926296"></td>
            <td class="xl6926296">HV04-02</td>
            <td class="xl8126296" align="right" style="border-top:none">
              <xsl:value-of select="$HV04-02" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl8026296" style="height:14.25pt">&#160;</td>
            <td class="xl6926296">BF (12 months)</td>
            <td class="xl6926296">HV02-37</td>
            <td class="xl11526296">
              <xsl:value-of select="$HV02-37" />
            </td>
            <td class="xl8026296">&#160;</td>
            <td class="xl6926296">25+</td>
            <td class="xl6926296"></td>
            <td class="xl6926296">HV04-03</td>
            <td class="xl8126296" align="right" style="border-top:none">
              <xsl:value-of select="$HV04-03" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl8026296" style="height:14.25pt">&#160;</td>
            <td class="xl6926296">NotBF (12 months)</td>
            <td class="xl6926296">HV02-38</td>
            <td class="xl11526296" style="border-top:none">
              <xsl:value-of select="$HV02-38" />
            </td>
            <td class="xl8026296">&#160;</td>
            <td class="xl6926296">Total (Sum HV04-01 to HV04-02) HV04-06</td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl6926296">
              <xsl:value-of select="$HV04-06" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="20" style="height:15.0pt">
            <td height="20" class="xl8026296" style="height:15.0pt">&#160;</td>
            <td class="xl8626296">NotKnown</td>
            <td class="xl8626296">HV02-39</td>
            <td class="xl12226296" style="border-top:none">
              <xsl:value-of select="$HV02-39" />
            </td>
            <td class="xl7026296" align="right">4.2</td>
            <td colspan="7" class="xl13726296" style="border-right:1.0pt solid black">
              HIV    Status (at circumcision)
            </td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl8026296" style="height:14.25pt">&#160;</td>
            <td class="xl8826296">Total Exposed aged 12 months (Sum HV02-37 to HV02-39)</td>
            <td class="xl8826296">HV02-40</td>
            <td class="xl12326296">0</td>
            <td class="xl8026296">&#160;</td>
            <td class="xl6926296">Positive</td>
            <td class="xl6926296"></td>
            <td class="xl6926296">HV04-07</td>
            <td class="xl8126296" align="right">
              <xsl:value-of select="$HV04-07" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl8026296" style="height:14.25pt">&#160;</td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl11926296">&#160;</td>
            <td class="xl8026296">&#160;</td>
            <td class="xl6926296">Negative</td>
            <td class="xl6926296"></td>
            <td class="xl6926296">HV04-08</td>
            <td class="xl8126296" align="right" style="border-top:none">
              <xsl:value-of select="$HV04-08" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl7426296" align="right" style="height:14.25pt">2.9</td>
            <td class="xl7526296">Infant ARV Prophylaxis (at first contact only)</td>
            <td class="xl7526296">&#160;</td>
            <td class="xl11326296">&#160;</td>
            <td class="xl8026296">&#160;</td>
            <td class="xl6926296">Unknown</td>
            <td class="xl6926296"></td>
            <td class="xl6926296">HV04-09</td>
            <td class="xl8126296" align="right" style="border-top:none">
              <xsl:value-of select="$HV04-09" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl8026296" style="height:14.25pt">&#160;</td>
            <td class="xl6926296">Issued in ANC</td>
            <td class="xl6926296">HV02-41</td>
            <td class="xl11526296">
              <xsl:value-of select="$HV02-41" />
            </td>
            <td class="xl7026296" align="right">4.3</td>
            <td colspan="7" class="xl13726296" style="border-right:1.0pt solid black">
              Adverse Events (Circumcision)
            </td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl8026296" style="height:14.25pt">&#160;</td>
            <td class="xl6926296">Labour and Delivery</td>
            <td class="xl6926296">HV02-42</td>
            <td class="xl11526296" style="border-top:none">
              <xsl:value-of select="$HV02-42" />
            </td>
            <td class="xl8026296">&#160;</td>
            <td class="xl6926296">During -AE(s)– moderate</td>
            <td class="xl6926296"></td>
            <td class="xl6926296">HV04-10</td>
            <td class="xl8126296" align="right">
              <xsl:value-of select="$HV04-10" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="20" style="height:15.0pt">
            <td height="20" class="xl8026296" style="height:15.0pt">&#160;</td>
            <td class="xl8626296">PNC (&lt;72hrs)</td>
            <td class="xl8626296">HV02-43</td>
            <td class="xl12226296" style="border-top:none">
              <xsl:value-of select="$HV02-43" />
            </td>
            <td class="xl8026296">&#160;</td>
            <td class="xl6926296">During– AE(s) – severe</td>
            <td class="xl6926296"></td>
            <td class="xl6926296">HV04-11</td>
            <td class="xl8126296" align="right" style="border-top:none">
              <xsl:value-of select="$HV04-11" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="20" style="height:15.0pt">
            <td height="20" class="xl8026296" style="height:15.0pt">&#160;</td>
            <td class="xl8826296">
              Total Infants Issued Prophylaxis (Sum HV02-41 to HV02-43)
            </td>
            <td class="xl8826296">HV02-44</td>
            <td class="xl12326296">
              <xsl:value-of select="$HV02-44" />
            </td>
            <td class="xl8026296">&#160;</td>
            <td class="xl6926296">Post -AE(s)– moderate</td>
            <td class="xl6926296"></td>
            <td class="xl6926296">HV04-12</td>
            <td class="xl8126296" align="right" style="border-top:none">
              <xsl:value-of select="$HV04-12" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="20" style="height:15.0pt">
            <td height="20" class="xl9226296" colspan="2" style="height:15.0pt">6 Blood Safety</td>
            <td class="xl9326296">&#160;</td>
            <td class="xl12526296">&#160;</td>
            <td class="xl8026296">&#160;</td>
            <td class="xl8626296">Post– AE(s) – severe</td>
            <td class="xl8626296">&#160;</td>
            <td class="xl8626296">HV04-13</td>
            <td class="xl8726296" align="right" style="border-top:none">
              <xsl:value-of select="$HV04-13" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl8026296" style="height:14.25pt">&#160;</td>
            <td class="xl6926296">Donated blood units</td>
            <td class="xl6926296">HV06-01</td>
            <td class="xl11526296">
              <xsl:value-of select="$HV06-01" />
            </td>
            <td class="xl8026296">&#160;</td>
            <td colspan="2" class="xl14126296">
              Total AE During (Sum HV04-10 &amp; -11)
              HV04-14
            </td>
            <td class="xl8826296"></td>
            <td class="xl8826296" align="right">
              <xsl:value-of select="$HV04-14" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="20" style="height:15.0pt">
            <td height="20" class="xl8026296" style="height:15.0pt">&#160;</td>
            <td class="xl8626296">
              Blood units screened for TTIs<span style="mso-spacerun:yes"> </span>
            </td>
            <td class="xl8626296">HV06-02</td>
            <td class="xl11526296" style="border-top:none">
              <xsl:value-of select="$HV06-02" />
            </td>
            <td class="xl8026296">&#160;</td>
            <td colspan="2" class="xl14226296">Total AE Post (Sum HV04-12 &amp; -13) HV04-15</td>
            <td class="xl8826296"></td>
            <td class="xl8826296" align="right">
              <xsl:value-of select="$HV04-15" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="20" style="height:15.0pt">
            <td height="20" class="xl9026296" style="height:15.0pt">&#160;</td>
            <td class="xl8626296">Blood units reactive to HIV</td>
            <td class="xl8626296">HV06-05</td>
            <td class="xl12226296" style="border-top:none">
              <xsl:value-of select="$HV06-05" />
            </td>
            <td class="xl10026296">&#160;</td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl8826296"></td>
            <td class="xl8826296"></td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl9226296" colspan="2" style="height:14.25pt">
              5
              Post-Exposure Prophylaxis
            </td>
            <td class="xl9326296" style="border-top:none">&#160;</td>
            <td class="xl12526296" style="border-top:none">&#160;</td>
            <td class="xl9326296">&#160;</td>
            <td class="xl9326296">&#160;</td>
            <td class="xl9326296">&#160;</td>
            <td class="xl9326296">&#160;</td>
            <td class="xl9326296">&#160;</td>
            <td class="xl9326296">&#160;</td>
            <td class="xl9326296">&#160;</td>
            <td class="xl9326296">&#160;</td>
            <td class="xl9426296">&#160;</td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl7226296" colspan="2" style="height:14.25pt">
              5.1 Type of Exposure
            </td>
            <td class="xl7226296">&#160;</td>
            <td class="xl12626296">&#160;</td>
            <td class="xl7226296">&#160;</td>
            <td class="xl7226296">&#160;</td>
            <td class="xl7226296">&#160;</td>
            <td class="xl7226296">&#160;</td>
            <td class="xl7226296">&#160;</td>
            <td class="xl7226296">&#160;</td>
            <td class="xl7226296">&#160;</td>
            <td class="xl7226296">&#160;</td>
            <td class="xl7326296">&#160;</td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl6926296" style="height:14.25pt"></td>
            <td class="xl6926296">Occupational</td>
            <td class="xl6926296">HV05-01 (M)</td>
            <td class="xl11526296">
              <xsl:value-of select="$HV05-01" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl6926296">HV05-02 (F)</td>
            <td class="xl8126296" align="right">
              <xsl:value-of select="$HV05-02" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl8026296" style="height:14.25pt">&#160;</td>
            <td class="xl6926296">
              Sexual assault<span style="mso-spacerun:yes"> </span>
            </td>
            <td class="xl6926296">HV05-03 (M)</td>
            <td class="xl11526296" style="border-top:none">
              <xsl:value-of select="$HV05-03" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl6926296">HV05-04 (F)</td>
            <td class="xl8126296" align="right" style="border-top:none">
              <xsl:value-of select="$HV05-04" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="20" style="height:15.0pt">
            <td height="20" class="xl8026296" style="height:15.0pt">&#160;</td>
            <td class="xl8626296">
              Other reasons<span style="mso-spacerun:yes"> </span>
            </td>
            <td class="xl8626296">HV05-05 (M)</td>
            <td class="xl12226296" style="border-top:none">
              <xsl:value-of select="$HV05-05" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl6926296">HV05-06 (F)</td>
            <td class="xl8126296" align="right" style="border-top:none">
              <xsl:value-of select="$HV05-06" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl8026296" style="height:14.25pt">&#160;</td>
            <td class="xl8826296">Total</td>
            <td class="xl8826296">HV05-07</td>
            <td class="xl12726296">
              <xsl:value-of select="$HV05-07" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl7426296" colspan="2" style="height:14.25pt">
              5.2 Provided  with Prophylaxis
            </td>
            <td class="xl7526296">&#160;</td>
            <td class="xl12826296">&#160;</td>
            <td class="xl7526296">&#160;</td>
            <td class="xl7526296">&#160;</td>
            <td class="xl7526296">&#160;</td>
            <td class="xl7526296">&#160;</td>
            <td class="xl7526296">&#160;</td>
            <td class="xl7526296">&#160;</td>
            <td class="xl7526296">&#160;</td>
            <td class="xl7526296">&#160;</td>
            <td class="xl7626296">&#160;</td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl8026296" style="height:14.25pt">&#160;</td>
            <td class="xl6926296">Occupational</td>
            <td class="xl6926296">HV05-08 (M)</td>
            <td class="xl11526296">
              <xsl:value-of select="$HV05-08" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl6926296">HV05-09 (F)</td>
            <td class="xl8126296" align="right">
              <xsl:value-of select="$HV05-09" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl8026296" style="height:14.25pt">&#160;</td>
            <td class="xl6926296">Sexual assault</td>
            <td class="xl6926296">HV05-10 (M)</td>
            <td class="xl11526296" style="border-top:none">
              <xsl:value-of select="$HV05-10" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl6926296">HV05-11 (F)</td>
            <td class="xl8126296" align="right" style="border-top:none">
              <xsl:value-of select="$HV05-11" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="20" style="height:15.0pt">
            <td height="20" class="xl8026296" style="height:15.0pt">&#160;</td>
            <td class="xl8626296">Other reasons</td>
            <td class="xl8626296">HV05-12 (M)</td>
            <td class="xl12226296" style="border-top:none">
              <xsl:value-of select="$HV05-12" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl6926296">HV05-13 (F)</td>
            <td class="xl8126296" align="right" style="border-top:none">
              <xsl:value-of select="$HV05-13" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl8026296" style="height:14.25pt">&#160;</td>
            <td class="xl8826296">Total PEP</td>
            <td class="xl8826296">HV05-14</td>
            <td class="xl12726296">
              <xsl:value-of select="$HV05-14" />
            </td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl7926296">&#160;</td>
          </tr>
          <tr height="20" style="height:15.0pt">
            <td height="20" class="xl9026296" style="height:15.0pt">&#160;</td>
            <td class="xl8626296">&#160;</td>
            <td class="xl8626296">&#160;</td>
            <td class="xl12926296">&#160;</td>
            <td class="xl8626296">&#160;</td>
            <td class="xl8626296">&#160;</td>
            <td class="xl8626296">&#160;</td>
            <td class="xl8626296">&#160;</td>
            <td class="xl8626296">&#160;</td>
            <td class="xl8626296">&#160;</td>
            <td class="xl8626296">&#160;</td>
            <td class="xl8626296">&#160;</td>
            <td class="xl9126296">&#160;</td>
          </tr>
          <tr height="19" style="height:14.25pt">
            <td height="19" class="xl6926296" style="height:14.25pt"></td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl13026296"></td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
            <td class="xl6926296"></td>
          </tr>
        </table>
      </div>
    </body>
  </xsl:template>
</xsl:stylesheet>

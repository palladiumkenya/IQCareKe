IF EXISTS(select * from Indicator where IndicatorName='Total Assessed for HIV Risk')
BEGIN
UPDATE Indicator SET Code='HV01-45' WHERE IndicatorName='Total Assessed for HIV Risk';
END

IF EXISTS(select * from Indicator where IndicatorName='Self Testing Total')
BEGIN
UPDATE Indicator SET Code='HV01-50' WHERE IndicatorName='Self Testing Total';
END

IF EXISTS(select * from Indicator where IndicatorName='PEP Total')
BEGIN
UPDATE Indicator SET Code='HV05-06' WHERE IndicatorName='PEP Total';
END



IF EXISTS(select * from Indicator where IndicatorName='Circumcised Total')
BEGIN
UPDATE Indicator SET Code='HV04-07' WHERE IndicatorName='Circumcised Total';
END



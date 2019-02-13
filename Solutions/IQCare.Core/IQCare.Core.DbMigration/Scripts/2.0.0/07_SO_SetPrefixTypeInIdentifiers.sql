IF (EXISTS (SELECT *  FROM sys.all_columns  WHERE name = 'PrefixType'))
BEGIN 
	UPDATE Identifiers SET PrefixType = NULL WHERE Code='CCCNumber';
END


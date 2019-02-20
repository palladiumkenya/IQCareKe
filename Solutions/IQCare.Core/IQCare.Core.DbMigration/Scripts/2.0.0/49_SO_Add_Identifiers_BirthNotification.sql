  IF NOT EXISTS(SELECT * FROM Identifiers i WHERE i.Name='BirthNotification' AND i.IdentifierType=2)
  BEGIN
   INSERT INTO Identifiers(Name,Code,DisplayName,DataType,DeleteFlag,CreatedBy,IdentifierType,MinLength,MaxLength)
   VALUES(
		'BirthNotification',
		'PersonIdentification',
		'Birth Notification',
		'Text',
		0,
		1,
		2,
		10,
		10
   )
  END
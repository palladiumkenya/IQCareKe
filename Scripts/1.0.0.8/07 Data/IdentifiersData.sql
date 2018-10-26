UPDATE Identifiers SET ValidatorRegex = '/^[0-9]{10}$/', FailedValidationMessage = 'Please enter a valid CCC Number. Value must be in numbers and 10 characters in length' 
	WHERE Code = 'CCCNumber' AND (ValidatorRegex = '' OR FailedValidationMessage = '')

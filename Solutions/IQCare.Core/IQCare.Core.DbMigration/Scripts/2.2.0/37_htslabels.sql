UPDATE LookupItem SET Name = 'H: Hearing impairment', DisplayName = 'H: Hearing impairment' WHERE Name = 'D: Deaf/hearing impaired';
UPDATE LookupItem SET Name = 'V: Visual impairment', DisplayName = 'V: Visual impairment' WHERE Name = 'B: Blind/Visually impaired';
UPDATE LookupMasterItem SET DisplayName = 'V: Visual impairment' WHERE LookupItemId = (SELECT TOP 1 Id FROM LookupItem WHERE Name = 'V: Visual impairment');
UPDATE LookupMasterItem SET DisplayName = 'H: Hearing impairment' WHERE LookupItemId = (SELECT TOP 1 Id FROM LookupItem WHERE Name = 'H: Hearing impairment');
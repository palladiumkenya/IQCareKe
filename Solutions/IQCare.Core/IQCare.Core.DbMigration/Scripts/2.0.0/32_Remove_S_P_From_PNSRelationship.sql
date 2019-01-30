DELETE FROM LookupMasterItem WHERE LookupMasterId = (select Id from LookupMaster where Name = 'PNSRelationship') and (DisplayName = 'S' OR DisplayName = 'P')

DECLARE @ConstraintName nvarchar(200)
SELECT @ConstraintName = (select t.FK_name from (SELECT
    o1.name AS FK_table,
    c1.name AS FK_column,
    fk.name AS FK_name,
    o2.name AS PK_table,
    c2.name AS PK_column,
    pk.name AS PK_name,
    fk.delete_referential_action_desc AS Delete_Action,
    fk.update_referential_action_desc AS Update_Action
FROM sys.objects o1
    INNER JOIN sys.foreign_keys fk
        ON o1.object_id = fk.parent_object_id
    INNER JOIN sys.foreign_key_columns fkc
        ON fk.object_id = fkc.constraint_object_id
    INNER JOIN sys.columns c1
        ON fkc.parent_object_id = c1.object_id
        AND fkc.parent_column_id = c1.column_id
    INNER JOIN sys.columns c2
        ON fkc.referenced_object_id = c2.object_id
        AND fkc.referenced_column_id = c2.column_id
    INNER JOIN sys.objects o2
        ON fk.referenced_object_id = o2.object_id
    INNER JOIN sys.key_constraints pk
        ON fk.referenced_object_id = pk.parent_object_id
        AND fk.key_index_id = pk.unique_index_id
		where o1.name = 'PatientOVCStatus' and c1.name='GuardianId'
--ORDER BY o1.name, o2.name, fkc.constraint_column_id
)t)
 IF EXISTS   (select t.FK_name from (SELECT
    o1.name AS FK_table,
    c1.name AS FK_column,
    fk.name AS FK_name,
    o2.name AS PK_table,
    c2.name AS PK_column,
    pk.name AS PK_name,
    fk.delete_referential_action_desc AS Delete_Action,
    fk.update_referential_action_desc AS Update_Action
FROM sys.objects o1
    INNER JOIN sys.foreign_keys fk
        ON o1.object_id = fk.parent_object_id
    INNER JOIN sys.foreign_key_columns fkc
        ON fk.object_id = fkc.constraint_object_id
    INNER JOIN sys.columns c1
        ON fkc.parent_object_id = c1.object_id
        AND fkc.parent_column_id = c1.column_id
    INNER JOIN sys.columns c2
        ON fkc.referenced_object_id = c2.object_id
        AND fkc.referenced_column_id = c2.column_id
    INNER JOIN sys.objects o2
        ON fk.referenced_object_id = o2.object_id
    INNER JOIN sys.key_constraints pk
        ON fk.referenced_object_id = pk.parent_object_id
        AND fk.key_index_id = pk.unique_index_id
		where o1.name = 'PatientOVCStatus' and c1.name='GuardianId'
--ORDER BY o1.name, o2.name, fkc.constraint_column_id
)t)

BEGIN 
EXEC('ALTER TABLE PatientOVCStatus DROP Constraint' +  @ConstraintName);
END

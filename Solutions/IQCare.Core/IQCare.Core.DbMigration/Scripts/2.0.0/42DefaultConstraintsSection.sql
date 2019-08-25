IF EXISTS(SELECT
    default_constraints.name
FROM 
    sys.all_columns

        INNER JOIN
    sys.tables
        ON all_columns.object_id = tables.object_id

        INNER JOIN 
    sys.schemas
        ON tables.schema_id = schemas.schema_id

        INNER JOIN
    sys.default_constraints
        ON all_columns.default_object_id = default_constraints.object_id

WHERE 
        schemas.name = 'dbo'
    AND tables.name = 'Section'
    AND all_columns.name = 'Active')
BEGIN
DECLARE @ConstraintName nvarchar(200)
SELECT @ConstraintName = (SELECT
    default_constraints.name
FROM 
    sys.all_columns

        INNER JOIN
    sys.tables
        ON all_columns.object_id = tables.object_id

        INNER JOIN 
    sys.schemas
        ON tables.schema_id = schemas.schema_id

        INNER JOIN
    sys.default_constraints
        ON all_columns.default_object_id = default_constraints.object_id

WHERE 
        schemas.name = 'dbo'
    AND tables.name = 'Section'
    AND all_columns.name = 'Active')
EXEC('ALTER TABLE Section DROP CONSTRAINT ' + @ConstraintName)
ALTER TABLE [Section] ADD DEFAULT 1 FOR [Active]

END
UPDATE Section SET Active=1 

GO



IF EXISTS(SELECT
    default_constraints.name
FROM 
    sys.all_columns

        INNER JOIN
    sys.tables
        ON all_columns.object_id = tables.object_id

        INNER JOIN 
    sys.schemas
        ON tables.schema_id = schemas.schema_id

        INNER JOIN
    sys.default_constraints
        ON all_columns.default_object_id = default_constraints.object_id

WHERE 
        schemas.name = 'dbo'
    AND tables.name = 'SubSection'
    AND all_columns.name = 'Active')
BEGIN
DECLARE @ConstraintName nvarchar(200)
SELECT @ConstraintName = (SELECT
    default_constraints.name
FROM 
    sys.all_columns

        INNER JOIN
    sys.tables
        ON all_columns.object_id = tables.object_id

        INNER JOIN 
    sys.schemas
        ON tables.schema_id = schemas.schema_id

        INNER JOIN
    sys.default_constraints
        ON all_columns.default_object_id = default_constraints.object_id

WHERE 
        schemas.name = 'dbo'
    AND tables.name = 'SubSection'
    AND all_columns.name = 'Active')
EXEC('ALTER TABLE SubSection DROP CONSTRAINT ' + @ConstraintName)
ALTER TABLE [SubSection] ADD DEFAULT 1 FOR [Active]

END
UPDATE SubSection SET Active=1 


GO

update Section set DeleteFlag=1 where SectionName='Prevention of Mother-to-Child Transmission (PMTCT)'
update Section set DeleteFlag=1 where SectionName='HIV and TB treatment'

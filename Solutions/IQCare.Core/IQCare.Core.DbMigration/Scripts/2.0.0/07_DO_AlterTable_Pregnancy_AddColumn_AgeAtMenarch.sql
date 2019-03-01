

IF NOT EXISTS (SELECT 1  FROM   INFORMATION_SCHEMA.COLUMNS  WHERE  TABLE_NAME = 'Pregnancy'
                      AND COLUMN_NAME = 'AgeAtMenarche'
                      AND TABLE_SCHEMA='dbo')
                      BEGIN
                        ALTER TABLE Pregnancy ADD AgeAtMenarche DECIMAL(8,2) NULL
                      END



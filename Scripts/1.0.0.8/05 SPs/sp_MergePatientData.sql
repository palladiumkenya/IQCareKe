IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_MergePatientData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_MergePatientData]
GO

/****** Object:  StoredProcedure [dbo].[sp_MergePatientData]    Script Date: 9/10/2018 11:19:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		KOchieng
-- Create date: 2018-07-25
-- Description:	
-- Merges 2 patient records. 
-- All records from one patient are transferred to the preferred patient and the abandoned patient record deleted softly
-- =============================================
CREATE PROCEDURE [dbo].[sp_MergePatientData] 
	-- Add the parameters for the stored procedure here
	@preferredPatientId int = 0, 
	@unpreferredPatientId int = 0,
	@userId int = 0
AS
BEGIN TRY
    BEGIN TRANSACTION

		-- SET NOCOUNT ON added to prevent extra result sets from
		-- interfering with SELECT statements.
		SET NOCOUNT ON;
		DECLARE @tableName as NVARCHAR(250)
		DECLARE @patientFk as NVARCHAR(50)
		DECLARE @sqlStr as NVARCHAR(MAX)

		DECLARE @preferredPtnPk as Int
		DECLARE @unpreferredPtnPk as Int
		DECLARE @preferredPersonId as Int
		DECLARE @unPreferredPersonId as Int

		BEGIN TRY 
			DROP table #tmpTables
		END TRY
		BEGIN CATCH
		END CATCH

		BEGIN TRY 
			DROP table #tmpPatientTables
		END TRY
		BEGIN CATCH
		END CATCH

		CREATE TABLE #tmpPatientTables (TableName NVARCHAR(250),PatientFK NVARCHAR(50))

		SET NOCOUNT ON;
	
		-- Begin Map all the tables holding patient data with their respective Foreign Key Columns i.e. PatientId or ptn_pk
		SELECT 
			[Name] as TableName 
		INTO #tmpTables
		FROM sys.all_objects WHERE type= 'U' AND SCHEMA_ID = 1 AND [Name] NOT IN ('Patient', 'mst_Patient', 'person', 'PatientIdentifier', 'Lnk_PatientProgramStart', 'ARTPataient', 'PatientEnrollment')

		SELECT @tableName = min(TableName) FROM #tmpTables
		WHILE @tableName IS NOT NULL
		BEGIN
			SELECT top 1 @PatientFk = [name] FROM sys.columns WHERE object_id = OBJECT_ID(@tableName) AND [name] in ('PatientId','Ptn_pk','PatientPk','PtnPk','Patient_Pk') AND user_type_id = 56
	
			IF @@ROWCOUNT > 0 
			BEGIN
				INSERT INTO #tmpPatientTables (TableName,PatientFk) VALUES (@tableName,@patientFk) 
			END
	
			DELETE FROM #tmpTables WHERE TableName = @TableName
			SELECT @tableName = min(TableName) FROM #tmpTables
		END
 
		DROP TABLE #tmpTables
		-- End Map all the tables holding patient data with their respective Foreign Key Columns

		-- Begin Replace the FK column value for the preferred Patient Record with the abandonded patient record for all tables 
		SELECT @tableName = min(TableName) FROM #tmpPatientTables
		WHILE @tableName IS NOT NULL
		BEGIN
			SET @patientFk = (SELECT PatientFk FROM #tmpPatientTables WHERE TableName = @tableName)

			IF @patientFk = 'PatientId'
				BEGIN
					SET @sqlStr = CONCAT('UPDATE [', @tableName, '] SET [PatientId] =', @preferredPatientId,' WHERE [', @patientFk,'] = ', @unpreferredPatientId)
				END
			ELSE
			BEGIN
				IF @patientFk =  'PersonId'
				BEGIN
					SET @preferredPersonId = (SELECT PersonId FROM Patient WHERE Id = @preferredPatientId)
					SET @unpreferredPersonId = (SELECT PersonId FROM Patient WHERE Id = @unpreferredPatientId)
					SET @sqlStr = CONCAT('UPDATE [', @tableName, '] SET [', @patientFk , '] = ', @preferredPersonId,' WHERE [', @patientFk,'] = ', @unpreferredPersonId)
				END
				ELSE
				BEGIN
					SET @preferredPtnPk = (SELECT Ptn_Pk FROM Patient WHERE Id = @preferredPatientId)
					SET @unpreferredPtnPk = (SELECT Ptn_Pk FROM Patient WHERE Id = @unpreferredPatientId)
					SET @sqlStr = CONCAT('UPDATE [', @tableName, '] SET [', @patientFk , '] = ', @preferredPtnPk,' WHERE [', @patientFk,'] = ', @unpreferredPtnPk)
				END
			END

			EXECUTE sp_executesql @sqlStr

			DELETE FROM #tmpPatientTables WHERE TableName = @TableName
			SELECT @tableName = min(TableName) FROM #tmpPatientTables
		END
		-- End Replace the FK column value for the preferred Patient Record with the abandonded patient record for all tables 

		-- Soft delete the reevant patient master tables
		UPDATE Patient SET DeleteFlag = 1 WHERE Id = @unpreferredPatientId
		UPDATE Person SET DeleteFlag = 1 WHERE Id = (SELECT PersonId FROM Patient WHERE Id = @unpreferredPatientId)
		UPDATE mst_Patient SET DeleteFlag = 1 WHERE Ptn_Pk = (SELECT Ptn_Pk FROM Patient WHERE Id = @unpreferredPatientId)
		UPDATE PatientEnrollment SET DeleteFlag = 0 WHERE Id = @unpreferredPatientId
	
		INSERT INTO PatientMergingLog (PreferredPatientId,UnpreferredPatientId,CreatedBy) VALUES (@preferredPatientId, @unpreferredPatientId, @userId)
		COMMIT TRAN -- Transaction Success!
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRAN

        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE()
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY()
        DECLARE @ErrorState INT = ERROR_STATE()

    RAISERROR (@ErrorMessage, -- Message text.  
               @ErrorSeverity, -- Severity.  
               @ErrorState -- State.  
               );
END CATCH

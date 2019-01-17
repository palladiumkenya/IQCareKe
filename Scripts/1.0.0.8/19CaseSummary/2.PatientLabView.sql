IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[VW_PatientLabsView]'))
DROP VIEW [dbo].[VW_PatientLabsView]
GO

/****** Object:  View [dbo].[gcPatientView]    Script Date: 7/23/2018 11:11:14 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW   VW_PatientLabsView
AS
Select distinct    CAST(row_number() over(order by VisitId) as INT) as Id,a.Ptn_pk,p.Id as PatientId , v.VisitId VisitID, a.TestName
				,  Coalesce(Cast(a.TestResults As varchar(100)), Cast(a.[Parameter Result] As
				varchar(100)), Cast(a.TestResults1 As varchar(100)),
				cast((case when  a.undetectable = 1 then 10 else null end) as varchar(20))) TestResult
				, a.OrderedbyDate
				, a.ReportedbyDate
				From VW_PatientLaboratory a 
				left join Patient p on p.ptn_pk=a.Ptn_Pk
				LEFT join ord_PatientLabOrder v on v.Ptn_Pk = a.ptn_pk  and a.LabID = v.LabID
				Where Coalesce(Cast(a.TestResults As varchar(100)), Cast(a.[Parameter Result] As
				varchar(100)), Cast(a.TestResults1 As varchar(100)),cast((case when a.undetectable = 1 then 10  else null end) as varchar(20))) Is Not Null And
				Coalesce(Cast(a.TestResults As varchar(100)), Cast(a.[Parameter Result] As
				varchar(100)), Cast(a.TestResults1 As varchar(100)),cast((case when a.undetectable = 1 then 10  else null end) as varchar(20))) != ''				

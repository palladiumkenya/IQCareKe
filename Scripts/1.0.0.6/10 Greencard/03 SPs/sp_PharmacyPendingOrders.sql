IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_PharmacyPendingOrders]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_PharmacyPendingOrders]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE sp_PharmacyPendingOrders 

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	select count(*) Number from ord_patientpharmacyorder where orderstatus = 1 and dispensedbydate is null and (deleteflag = 0 or deleteflag is null)


    select convert(varchar(20),orderedbydate,106) [Prescription Date], count(*) [Pending Orders] from ord_patientpharmacyorder where orderstatus = 1 and dispensedbydate is null and (deleteflag = 0 or deleteflag is null)
	group by orderedbydate
END
GO

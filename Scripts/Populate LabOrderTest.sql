USE [IQCareDefault]
GO
/****** Object:  Trigger [dbo].[lis_loadOrderfrom_ordLab]    Script Date: 3/20/2017 8:15:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<pwangoo>
-- Create date: <18032017>
-- Description:	<Insert lab order from ord_table>
-- =============================================

ALTER TRIGGER  [dbo].[lis_loadOrderfrom_ordLab] ON [dbo].[ord_LabOrder]
FOR INSERT AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO 
    [dbo].[dtl_LabOrderTest]
    (
        LabOrderId,
		LabTestId,	 
        TestNotes,
        StatusDate
    )
    SELECT 
        Id, 
		LabTestId,		
        ClinicalOrderNotes, 
        OrderDate
    FROM 
        INSERTED
END

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[VW_PatientPharmacy]'))
DROP VIEW [dbo].[VW_PatientPharmacy]
GO


CREATE VIEW [dbo].[VW_PatientPharmacy]
AS
SELECT        dbo.ord_PatientPharmacyOrder.Ptn_pk, dbo.ord_PatientPharmacyOrder.VisitID, dbo.ord_PatientPharmacyOrder.LocationID, 
                         dbo.ord_PatientPharmacyOrder.OrderedBy, dbo.ord_PatientPharmacyOrder.OrderedByDate, dbo.ord_PatientPharmacyOrder.DispensedBy, 
                         dbo.ord_PatientPharmacyOrder.DispensedByDate, dbo.ord_PatientPharmacyOrder.ProgID, dbo.ord_PatientPharmacyOrder.OrderType, 
                         dbo.ord_PatientPharmacyOrder.Height, dbo.ord_PatientPharmacyOrder.Weight, dbo.ord_PatientPharmacyOrder.ProviderID, 
                         dbo.ord_PatientPharmacyOrder.PharmacyPeriodTaken, dbo.VW_Drug.Drug_pk, dbo.VW_Drug.DrugName, dbo.dtl_RegimenMap.RegimenType, 
                         dbo.dtl_RegimenMap.RegimenId, dbo.dtl_PatientPharmacyOrder.Duration, dbo.dtl_PatientPharmacyOrder.OrderedQuantity, 
                         dbo.dtl_PatientPharmacyOrder.DispensedQuantity, dbo.dtl_PatientPharmacyOrder.Prophylaxis, dbo.ord_Visit.VisitDate, dbo.ord_Visit.VisitType, 
                         dbo.ord_PatientPharmacyOrder.ptn_pharmacy_pk, dbo.VW_Drug.DrugTypeId, dbo.VW_Drug.DrugTypeName AS DrugType, Stock.Quantity AS ActualQtyDispensed, 
                         Stock.ExpiryDate, dbo.Mst_Store.Id AS StoreId, dbo.Mst_Store.Name AS StoreName, dbo.Mst_Batch.ID AS BatchId, dbo.Mst_Batch.Name AS BatchNo, 
                         ISNULL(Bill.SellingPrice, 0) AS SellingPrice, ISNULL(Bill.CostPrice, 0) AS CostPrice, Bill.Margin, Bill.BillAmount, dbo.VW_Drug.[Item Code], dbo.VW_Drug.[FDA Code], 
                         dbo.VW_Drug.[Dispensing Unit], dbo.VW_Drug.[Dispensing Unit Id], dbo.VW_Drug.MaxStock, dbo.VW_Drug.MinStock, dbo.VW_Drug.PurchaseUnitId, 
                         dbo.VW_Drug.[Purchase Unit], dbo.dtl_PatientPharmacyOrder.FrequencyID, dbo.dtl_PatientPharmacyOrder.TreatmentPhase, 
                         dbo.dtl_PatientPharmacyOrder.WhyPartial, dbo.dtl_PatientPharmacyOrder.Month, dbo.ord_PatientPharmacyOrder.HoldMedicine, 
                         dbo.ord_PatientPharmacyOrder.RegimenLine, dbo.ord_PatientPharmacyOrder.PharmacyNotes, dbo.dtl_PatientPharmacyOrder.StrengthID, 
                         dbo.ord_PatientPharmacyOrder.CreateDate, dbo.ord_PatientPharmacyOrder.EmployeeID, dbo.ord_PatientPharmacyOrder.Signature, dbo.mst_Strength.StrengthName,
                          dbo.mst_Frequency.Name AS FrequencyName, dbo.VW_Drug.[Selling Price] AS UnitSellingPrice, dbo.VW_Drug.GenericID, dbo.VW_Drug.GenericName, 
                         dbo.dtl_PatientPharmacyOrder.SingleDose, dbo.dtl_PatientPharmacyOrder.Financed, dbo.dtl_PatientPharmacyOrder.PrintPrescriptionStatus, 
                         dbo.dtl_PatientPharmacyOrder.PatientInstructions, dbo.ord_PatientPharmacyOrder.ReportingID, dbo.dtl_PatientPharmacyOrder.pillCount, 
                         dbo.mst_Frequency.multiplier AS FreqMultiplier, dbo.VW_Drug.QtyUnitDisp, dbo.VW_Drug.syrup, dbo.dtl_PatientPharmacyOrder.MorningDose, 
                         dbo.dtl_PatientPharmacyOrder.MiddayDose, dbo.dtl_PatientPharmacyOrder.EveningDose, dbo.dtl_PatientPharmacyOrder.NightDose, 
                         dbo.dtl_PatientPharmacyOrder.comments, dbo.ord_Visit.UserID, dbo.ord_PatientPharmacyOrder.TreatmentPlan, dbo.ord_Visit.PatientClassification, 
                         dbo.ord_Visit.IsEnrolDifferenciatedCare, dbo.ord_Visit.ARTRefillModel
FROM            dbo.dtl_RegimenMap RIGHT OUTER JOIN
                         dbo.mst_Strength RIGHT OUTER JOIN
                         dbo.mst_Frequency RIGHT OUTER JOIN
                         dbo.ord_PatientPharmacyOrder INNER JOIN
                         dbo.ord_Visit ON dbo.ord_PatientPharmacyOrder.VisitID = dbo.ord_Visit.Visit_Id INNER JOIN
                         dbo.dtl_PatientPharmacyOrder ON dbo.ord_PatientPharmacyOrder.ptn_pharmacy_pk = dbo.dtl_PatientPharmacyOrder.ptn_pharmacy_pk ON 
                         dbo.mst_Frequency.ID = dbo.dtl_PatientPharmacyOrder.FrequencyID ON dbo.mst_Strength.StrengthId = dbo.dtl_PatientPharmacyOrder.StrengthID LEFT OUTER JOIN
                         dbo.VW_Drug ON dbo.dtl_PatientPharmacyOrder.Drug_Pk = dbo.VW_Drug.Drug_pk ON 
                         dbo.dtl_RegimenMap.OrderID = dbo.ord_PatientPharmacyOrder.ptn_pharmacy_pk LEFT OUTER JOIN
                             (SELECT        Ptn_Pharmacy_Pk, ItemId, BatchId, ExpiryDate, StoreId, SUM(Quantity) AS Quantity
                               FROM            dbo.Dtl_StockTransaction
                               WHERE        (Ptn_Pharmacy_Pk IS NOT NULL)
                               GROUP BY Ptn_Pharmacy_Pk, ItemId, BatchId, ExpiryDate, StoreId) AS Stock ON dbo.dtl_PatientPharmacyOrder.ptn_pharmacy_pk = Stock.Ptn_Pharmacy_Pk AND 
                         dbo.dtl_PatientPharmacyOrder.Drug_Pk = Stock.ItemId AND dbo.dtl_PatientPharmacyOrder.BatchNo = Stock.BatchId AND 
                         dbo.dtl_PatientPharmacyOrder.ExpiryDate = Stock.ExpiryDate LEFT OUTER JOIN
                         dbo.Mst_Store ON dbo.Mst_Store.Id = Stock.StoreId LEFT OUTER JOIN
                         dbo.Mst_Batch ON dbo.Mst_Batch.ID = Stock.BatchId LEFT OUTER JOIN
                             (SELECT        PharmacyId, ItemId, BatchId, VisitId, SUM(SellingPrice) AS SellingPrice, CostPrice, Margin, SUM(BillAmount) AS BillAmount
                               FROM            dbo.Dtl_PatientBillTransaction
                               GROUP BY PharmacyId, ItemId, BatchId, VisitId, CostPrice, Margin) AS Bill ON dbo.dtl_PatientPharmacyOrder.ptn_pharmacy_pk = Bill.PharmacyId AND 
                         dbo.dtl_PatientPharmacyOrder.Drug_Pk = Bill.ItemId AND dbo.dtl_PatientPharmacyOrder.BatchNo = Bill.BatchId AND 
                         dbo.ord_PatientPharmacyOrder.VisitID = Bill.VisitId
WHERE        (dbo.ord_Visit.DeleteFlag = 0) OR
                         (dbo.ord_Visit.DeleteFlag IS NULL)

GO



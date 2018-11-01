export interface MaternityDeliveryCommand {
   PatinetMasterVisitId?: number;
   ProfileId?: number;
   DurationOfLabour?: number;
   DateOfDelivery?: Date;
   TimeOfDelivery?: Date;
   ModeOfDelivery?: number;
   PlacentaComplete?: number;
   BloodLossCapacity?: number;
   BloodLossClassification?: number;
   MotherCondition?: number;
   MaternalDeathAudited?: number;
    MaternalDeathAuditDate?: Date;
    DeliveryComplicationsExperienced?: boolean;
    DeliveryComplicationNotes?: string;
    DeliveryConductedBy?: string;
    CreatedBy?: number;




}

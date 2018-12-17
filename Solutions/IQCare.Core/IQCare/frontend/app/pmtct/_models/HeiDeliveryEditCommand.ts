export interface HeiDeliveryEditCommand {
    Id: number;
    PlaceOfDeliveryId: number;
    ModeOfDeliveryId: number;
    BirthWeight: number;
    ArvProphylaxisId: number;
    ArvProphylaxisOther: string;
    MotherIsRegistered: boolean;
    MotherArtInfantEnrolRegimenId: number;
    MotherPersonId?: number;
    MotherStatusId: number;
    PrimaryCareGiverID: number;
    MotherName: string;
    MotherCCCNumber: string;
    MotherPMTCTDrugsId: number;
    MotherPMTCTRegimenId?: number;
    MotherPMTCTRegimenOther: string;
    MotherArtInfantEnrolId: number;
}

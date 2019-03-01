export interface MaternityCounsellingCommand {
    PatientId?: number;
    PatientMasterVisitId?: number;
    CounsellingTopicId?: number;
    IsCounsellingDone?: boolean;
    CounsellingDate?: Date;
    Description?: Date;
    CreatedBy?: number;
}

export interface EditMaternityCounsellingCommand{
    Id : number,
    CounsellingTopicId?: number;
    IsCounsellingDone?: boolean;
    Description ? :string
}
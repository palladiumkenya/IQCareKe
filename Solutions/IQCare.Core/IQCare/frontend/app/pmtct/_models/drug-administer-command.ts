import {AdministeredDrugInfo} from '../maternity/commands/administer-drug-info';

export interface DrugAdministerCommand {
    Id?: number;
    PatientId?: number;
    PatientMasterVisitId?: number;
    CreatedBy?: number;
    AdministeredDrugs: AdministeredDrugInfo[];
}

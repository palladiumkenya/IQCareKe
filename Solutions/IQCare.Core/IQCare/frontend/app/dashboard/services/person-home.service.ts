import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { ErrorHandlerService } from '../../shared/_services/errorhandler.service';
import { CircumcisionCommand } from '../_model/ClientCircumcisionStatusCommand';
import { LookupItemView } from '../../shared/_models/LookupItemView';
import { PersonView } from '../../records/_models/personView';
import { EncounterDetails } from '../_model/HtsEncounterdetails';
import { PregnancyIndicatorCommand } from '../_model/PregnancyIndicatorCommand';
import { FamilyPlanningCommand } from '../../pmtct/_models/FamilyPlanningCommand';
import { FamilyPlanningMethodCommand } from '../../pmtct/_models/FamilyPlanningMethodCommand';
import { FamilyPlanningEditCommand } from '../../pmtct/_models/FamilyPlanningEditCommand';
import { PatientFamilyPlanningMethodEditCommand } from '../../pmtct/_models/PatientFamilyPlanningMethodEditCommand';
const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
    providedIn: 'root'
})
export class PersonHomeService {
    private API_URL = environment.API_URL;
    private API_PREPURL = environment.API_PREP_URL;
    private MATERNITY_API_URL = environment.API_PMTCT_URL;
    private API_PMTCT_URL = environment.API_PMTCT_URL;

    private _url = '/api/PatientServices/GetPatientByPersonId';
    private _htsurl = '/api/HtsEncounter';
    public person: PersonView;
    constructor(private http: HttpClient,
        private errorHandler: ErrorHandlerService) { }

    public getPatientByPersonId(personId: Number): Observable<PersonView> {
        return this.http.get<PersonView>(this.API_URL + '' + this._url + '/' + personId).pipe(
            tap(getPatientByPersonId => this.errorHandler.log('get ' + personId + 'options by Name')),
            catchError(this.errorHandler.handleError<PersonView>('getPatientByPersonId'))
        );
    }
    public getHTSEncounterDetailsBypersonId(personId: number): Observable<any[]> {
        return this.http.get<EncounterDetails[]>(this.API_URL + this._htsurl + '/getEncounterDetailsByPersonId/' + personId).pipe(
            tap(getHTSEncounterDetailsBypersonId => this.errorHandler.log('fetched a single client encounter details')),
            catchError(this.errorHandler.handleError<any[]>('getHTSEncounterDetailsBypersonId', []))
        );
    }

    public getAllServices(): Observable<any> {
        return this.http.get<any>(this.API_URL + '/api/ServiceArea/GetAllServices').pipe(
            tap(getAllServices => this.errorHandler.log(`get all facility services`)),
            catchError(this.errorHandler.handleError<PersonView>('getAllServices'))
        );
    }

    public getPersonEnrolledServices(personId: number): Observable<any> {
        return this.http.get<any>(this.API_URL + '/api/PatientServices/GetEnrolledServicesByPersonId/' + personId).pipe(
            tap(getPersonEnrolledServices => this.errorHandler.log(`get person enrolled services`)),
            catchError(this.errorHandler.handleError<any>('getPersonEnrolledServices'))
        );
    }
    public getServiceArea(name: string): Observable<any> {
        return this.http.get<any>(this.API_URL + '/api/ServiceArea/GetServiceArea?name=' + name).pipe(
            tap(getServiceArea => this.errorHandler.log(`get service area`)),
            catchError(this.errorHandler.handleError<any>('getServiceArea'))
        );
    }
    public saveCircumcisionStatus(clientCircumcisionStatusCommand: CircumcisionCommand): Observable<any> {
        return this.http.post<any>(this.API_PREPURL + '/api/CircumcisionStatus/AddCircumcisionStatus',
            JSON.stringify(clientCircumcisionStatusCommand), httpOptions).pipe(
                tap(saveCircumcisionStatus => this.errorHandler.log('Successfully saved patient circumcision status')),
                catchError(this.errorHandler.handleError<any>('Error in saving Patient circumcision status'))
            );
    }

    public getCircumcisionStatus(patientId: number): Observable<any> {
        return this.http.get<any>(this.API_PREPURL + '/api/CircumcisionStatus/GetCircumcisionStatus/' + patientId).pipe(
            tap(getCircumcisionStatus => this.errorHandler.log('Successfully saved patient circumcision status')),
            catchError(this.errorHandler.handleError<any>('Error in saving Patient circumcision status'))
        );
    }

    public getServiceAreaIdentifiers(serviceAreaId: number): Observable<any> {
        return this.http.get<any>(this.API_URL + '/api/Lookup/GetServiceAreaIdentifiers/' + serviceAreaId).pipe(
            tap(getServiceAreaIdentifiers => this.errorHandler.log(`get service area identifiers`)),
            catchError(this.errorHandler.handleError<any>('getServiceAreaIdentifiers'))
        );
    }

    public getPatientTypes(): Observable<any[]> {
        const options = JSON.stringify(['PatientType']);

        return this.http.post<any[]>(this.API_URL + '/api/Lookup/getCustomOptions', options, httpOptions).pipe(
            tap(getPatientType => this.errorHandler.log('fetched patien type options')),
            catchError(this.errorHandler.handleError<any[]>('getPatientType'))
        );
    }

    public getPatientAllergies(patientId: number): Observable<any> {
        return this.http.get<any>(this.API_URL + '/api/PatientAllergy/GetPatientAllergy/' + patientId).pipe(
            tap(getPatientAllergies => this.errorHandler.log('get patient Allergy')),
            catchError(this.errorHandler.handleError<any[]>('getPatientAllergies'))
        );
    }

    public getChronicIllnessesByPatientId(patientId: number): Observable<any> {
        return this.http.get<any>(this.API_URL + '/api/PatientChronicIllness/GetByPatientId/' + patientId).pipe(
            tap(getChronicIllnessesByPatientId => this.errorHandler.log(`get patient chronic illnesses`)),
            catchError(this.errorHandler.handleError<any>('getChronicIllnessesByPatientId'))
        );
    }

    public getRelationshipsByPatientId(patientId: number): Observable<any> {
        return this.http.get<any>(this.API_URL + '/api/PatientServices/GetRelationshipsByPatientId/' + patientId).pipe(
            tap(getRelationshipsByPatientId => this.errorHandler.log(`get patient relationships`)),
            catchError(this.errorHandler.handleError<any>('getRelationshipsByPatientId'))
        );
    }
    public GetCurrentPatientVitalsInfo(personId: number): Observable<any> {
        return this.http.get<any>(this.API_URL + '/api/PatientServices/GetCurrentPersonVitals/' + personId).pipe(
            tap(GetCurrentPatientVitalsInfo => this.errorHandler.log('get patient vitals details')),
            catchError(this.errorHandler.handleError<any>('GetCurrentPatientVitalsInfo'))
        );
    }

    public GetPatientAppoitment(patientId: number): Observable<any> {
        return this.http.get<any>(this.API_URL + '/api/PatientReferralAndAppointment/GetPatientAppoitment/' + patientId).pipe(
            tap(getPatientAllergies => this.errorHandler.log('get patient Allergy')),
            catchError(this.errorHandler.handleError<any[]>('getPatientAllergies'))
        );
    }

    public getPatientById(patientId: number): Observable<any> {
        return this.http.get<any>(this.API_URL + '/api/Register/GetPatientById/' + patientId).pipe(
            tap(getPatientById => this.errorHandler.log(`get patient details`)),
            catchError(this.errorHandler.handleError<any>('getPatientById'))
        );
    }

    public getPatientModelByPersonId(personId: number): Observable<any> {
        return this.http.get<any>(this.API_URL + '/api/Register/GetPatientByPersonId/' + personId).pipe(
            tap(getPatientModelByPersonId => this.errorHandler.log(`get patient model by personId ` + personId)),
            catchError(this.errorHandler.handleError<any>('getPatientModelByPersonId'))
        );
    }

    public getPersonPopulationType(personId: number): Observable<any[]> {
        return this.http.get<any[]>(this.API_URL + '/api/Registration/Person/GetPersonPopulationDetails/' + personId).pipe(
            tap(getPersonPopulationType => this.errorHandler.log(`get person population type ` + personId)),
            catchError(this.errorHandler.handleError<any>('getPersonPopulationType'))
        );
    }

    public getPatientServiceAreaEntryPoints(serviceAreaId: number, patientId: number): Observable<any> {
        return this.http.get<any>(this.API_URL + '/api/Register/getServiceEntryPoint/' + serviceAreaId + '/' + patientId).pipe(
            tap(getPatientServiceAreaEntryPoints => this.errorHandler.log(`get patient serviceentrypoints for patientId:  `
                + patientId + ` and serviceAreaId: ` + serviceAreaId)),
            catchError(this.errorHandler.handleError<any>('getPatientServiceAreaEntryPoints'))
        );
    }

    public getPatientTransferInDetails(serviceAreaId: number, personId: number): Observable<any> {
        return this.http.get<any>(this.API_URL + '/api/Register/GetPatientTransferIn/' + serviceAreaId + '/' + personId).pipe(
            tap(getPatientTransferInDetails => this.errorHandler.log(`get patient transefin details for personId:  `
                + personId + ` and serviceAreaId: ` + serviceAreaId)),
            catchError(this.errorHandler.handleError<any>('getPatientTransferInDetails'))
        );
    }

    public getPatientOVCStatusDetails(personId: number): Observable<any> {
        return this.http.get<any>(this.API_URL + '/api/Register/GetPatientOVCStatus/' + personId).pipe(
            tap(getPatientOVCStatusDetails => this.errorHandler.log(`get patient OVC details for personId:  `
                + personId)),
            catchError(this.errorHandler.handleError<any>('getPatientOVCStatusDetails'))
        );
    }
    public getPatientARVDetails(serviceAreaId: number, personId: number): Observable<any> {
        return this.http.get<any>(this.API_URL + '/api/Register/GetPatientARVHistory/' + serviceAreaId + '/' + personId).pipe(
            tap(getPatientARVDetails => this.errorHandler.log(`get patient ARV details for personId:  `
                + personId + ` and serviceAreaId: ` + serviceAreaId)),
            catchError(this.errorHandler.handleError<any>('getPatientARVDetails'))
        );
    }



    public getPatientEnrollmentMasterVisitByServiceAreaId(patientId: number, serviceAreaId: number): Observable<any> {
        return this.http.get<any>(this.API_URL + '/api/Register/getEnrollmentMasterVisitId/'
            + patientId + '/' + serviceAreaId).pipe(
                tap(getPatientEnrollmentDateByServiceAreaId => this.errorHandler.log(`get patient enrollment mastervisitid for patientId:  `
                    + patientId + ` and serviceAreaId: ` + serviceAreaId)),
                catchError(this.errorHandler.handleError<any>('getPatientEnrollmentMasterVisitByServiceAreaId'))
            );
    }

    public getPatientEnrollmentDateByServiceAreaId(patientId: number, serviceAreaId: number): Observable<any> {
        return this.http.get<any>(this.API_URL + '/api/Register/GetPatientEnrollmentByServiceAreaId/'
            + patientId + '/' + serviceAreaId).pipe(
                tap(getPatientEnrollmentDateByServiceAreaId => this.errorHandler.log(`get patient enrollment date for patientId:  `
                    + patientId + ` and serviceAreaId: ` + serviceAreaId)),
                catchError(this.errorHandler.handleError<any>('getPatientEnrollmentDateByServiceAreaId'))
            );
    }

    public getPersonPriorityTypes(personId: number): Observable<any[]> {
        return this.http.get<any[]>(this.API_URL + '/api/Registration/Person/GetPersonPriorityDetails/' + personId).pipe(
            tap(getPersonPriorityTypes => this.errorHandler.log(`get person priority type for personId: ` + personId)),
            catchError(this.errorHandler.handleError<any>('getPersonPriorityTypes'))
        );
    }

    CheckPrepencounterExists(personId: number): Observable<any[]> {
        const Indata = {
            'PersonId': personId
        };
        return this.http.post<any>(this.API_PREPURL + '/api/BehaviourRisk/Encounterexists', JSON.stringify(Indata), httpOptions)
            .pipe(tap(CheckencounterExists => this.errorHandler.log('checked if RiskAssessmentEncounter Exists')),
                catchError(this.errorHandler.handleError<any[]>('CheckencounterExists'))
            );
    }

    public filterFacilities(filterString: string) {
        return this.http.get<any[]>(this.API_URL + '/api/Lookup/searchFacilityList?searchString=' + filterString).pipe(
            tap(filterFacilities => this.errorHandler.log('fetched filtered facilities')),
            catchError(this.errorHandler.handleError<any[]>('filterFacilities'))
        );
    }

    public filtermflcode(filterString: string) {
        return this.http.get<any[]>(this.API_URL + '/api/Lookup/searchFacilityMflCodeList?searchString=' + filterString).pipe(
            tap(filterFacilities => this.errorHandler.log('fetched filtered facilities')),
            catchError(this.errorHandler.handleError<any[]>('filterFacilities'))
        );
    }
    public getFacility(mflCode: string) {
        return this.http.get<any>(this.API_URL + '/api/Lookup/getFacility/' + mflCode).pipe(
            tap(getFacility => this.errorHandler.log('get Facility')),
            catchError(this.errorHandler.handleError<any[]>('getFacility'))
        );
    }


    public getPatientCareEndedHistory(patientId: number): Observable<any[]> {
        return this.http.get<any[]>(this.API_URL + '/api/PatientServices/GetLatestCareEndDetails/' + patientId).pipe(
            tap(getPatientCareEndedHistory => this.errorHandler.log(`get Patient CareEnded details` + patientId)),
            catchError(this.errorHandler.handleError<any>('getPatientCareEndedHistory'))
        );
    }

    public getAllHTSEncounterBypersonId(personId: number): Observable<any[]> {
        return this.http.get<EncounterDetails[]>(this.API_URL + this._htsurl + '/getLatestEncounterDetails/' + personId).pipe(
            tap(getHTSEncounterDetailsBypersonId => this.errorHandler.log('fetched a single client encounter details')),
            catchError(this.errorHandler.handleError<any[]>('getHTSEncounterDetailsBypersonId', []))
        );
    }


    public getPregnancyIndicator(patientId: number, patientMasterVisitId: number): Observable<any> {
        return this.http.get<any>(this.MATERNITY_API_URL +
            '/api/PregnancyIndicator/GetPregnancyIndicator/' + patientId + '/' + patientMasterVisitId).pipe(
                tap(getPregnancyIndicator => this.errorHandler.log('Successfully fetched patient pregnancy indicator status')),
                catchError(this.errorHandler.handleError<any>('Error in fetching Patient pregnancy indicator status'))
            );
    }

    public savePregnancyIndicatorCommand(pregnancyIndicatorCommand: PregnancyIndicatorCommand): Observable<any> {
        return this.http.post<any>(this.MATERNITY_API_URL + '/api/PregnancyIndicator/AddPregnancyIndicator',
            JSON.stringify(pregnancyIndicatorCommand), httpOptions).pipe(
                tap(savePregnancyIndicatorCommand => this.errorHandler.log('Successfully saved patient pregnancy indicator status')),
                catchError(this.errorHandler.handleError<any>('Error in saving Patient pregnancy indicator status'))
            );
    }


    public getFamilyPlanning(patientId: number, patientMasterVisitId: number): Observable<any[]> {
        return this.http.get<any[]>(this.API_PMTCT_URL + '/api/FamilyPlanning/' + patientId + '/' + patientMasterVisitId).pipe(
            tap(getFamilyPlanning => this.errorHandler.log(`successfully fetched family planning`)),
            catchError(this.errorHandler.handleError<any>('Error fetching family planning'))
        );
    }

    public getFamilyPlanningMethod(patientId: number): Observable<any[]> {
        return this.http.get<any[]>(this.API_PMTCT_URL + '/api/FamilyPlanningMethods/GetFamilyPlanningInfo/' + patientId).pipe(
            tap(getFamilyPlanningMethod => this.errorHandler.log(`successfully fetched family planning method`)),
            catchError(this.errorHandler.handleError<any>('Error fetching family planning method'))
        );
    }



    public savePncFamilyPlanning(familyPlanningCommand: FamilyPlanningCommand): Observable<any> {
        return this.http.post<any>(this.API_PMTCT_URL + '/api/FamilyPlanning', JSON.stringify(familyPlanningCommand),
            httpOptions).pipe(
                tap(savePncFamilyPlanning => this.errorHandler.log(`successfully saved pnc family planning`)),
                catchError(this.errorHandler.handleError<any>('Error saving pnc family planning'))
            );
    }

    public updateFamilyPlanning(familyPlanningEditCommand: FamilyPlanningEditCommand): Observable<any> {
        return this.http.post(this.API_PMTCT_URL + '/api/FamilyPlanning/UpdateFamilyPlanning',
            JSON.stringify(familyPlanningEditCommand), httpOptions).pipe(
                tap(updateFamilyPlanning => this.errorHandler.log(`successfully updated family planning`)),
                catchError(this.errorHandler.handleError<any>('Error updating family planning'))
            );
    }


    public updateNextAppointment(appointment: any): Observable<any> {
        if (!appointment.AppointmentDate || appointment.AppointmentDate == null || appointment.AppointmentDate == 'null') {
            if (appointment.AppointmentId) {
                return this.http.delete(this.API_URL
                    + '/api/PatientReferralAndAppointment/DeleteAppointment/' + appointment.AppointmentId).pipe(
                        tap(update => this.errorHandler.log(`successfully updated appointment`)),
                        catchError(this.errorHandler.handleError<any>('Error updating appointments'))
                    );
            } else {
                return of([]);
            }
        }

        return this.http.post(this.API_URL + '/api/PatientReferralAndAppointment/UpdatePatientNextAppointment', JSON.stringify(appointment),
            httpOptions).pipe(tap(update => this.errorHandler.log(`successfully updated appointment`)),
                catchError(this.errorHandler.handleError<any>('Error updating appointments'))
            );
    }

    public AddHivPartnerProfile(PatientId: number, hivpartnerprofiles: any[]): Observable<any> {
        if (hivpartnerprofiles.length == 0) {
            return of([]);
        }

        const Indata = {
            'PatientId': PatientId,
            'patientPartnerProfiles': hivpartnerprofiles
        };

        return this.http.post<any>(this.API_PREPURL + 
            '/api/HivPartnerProfile/AddHivPartnerProfile', JSON.stringify(Indata), httpOptions).pipe(
            tap(saveHivPartnerProfile => this.errorHandler.log('Successfully saved hiv profile')),
            catchError(this.errorHandler.handleError<any>('Error in saving Patient Partner Hiv Profiles'))
        );
    }

    public getHivPartnerProfile(patientId: number): Observable<any> {
        return this.http.get(this.API_PREPURL
            + '/api/HivPartnerProfile/GetHivPartnerProfile/' + patientId).pipe(
                tap(getAppointments => this.errorHandler.log(`successfully fetched hivprofiles`)),
                catchError(this.errorHandler.handleError<any>('Error fetching profiles'))
            );

    }

    public saveNextAppointment(appointment: any): Observable<any> {
        if (!appointment.AppointmentDate || appointment.AppointmentDate == null
            || appointment.AppointmentDate == 'null') {
            return of([]);
        }

        return this.http.post(this.API_URL + '/api/PatientReferralAndAppointment/AddPatientNextAppointment', JSON.stringify(appointment),
            httpOptions).pipe(
                tap(saveReferrals => this.errorHandler.log(`successfully added patient appointment details`)),
                catchError(this.errorHandler.handleError<any>('Error saving appointment details'))
            );
    }
    public getAppointments(patientId: number, patientMasterVisitId: number): Observable<any> {
        return this.http.get(this.API_URL
            + '/api/PatientReferralAndAppointment/GetAppointment/' + patientId + '/' + patientMasterVisitId).pipe(
                tap(getAppointments => this.errorHandler.log(`successfully fetched appointment`)),
                catchError(this.errorHandler.handleError<any>('Error fetching appointment'))
            );
    }



    public updatePncFamilyPlanningMethod(updateFamilyPlanningMethodCommand: PatientFamilyPlanningMethodEditCommand): Observable<any> {
        return this.http.post(this.API_PMTCT_URL + '/api/FamilyPlanningMethods/UpdateFamilyPlanningMethod',
            JSON.stringify(updateFamilyPlanningMethodCommand), httpOptions).pipe(
                tap(updatePncFamilyPlanningMethod => this.errorHandler.log(`successfully updated family planning method`)),
                catchError(this.errorHandler.handleError<any>('Error updating family planning method'))
            );
    }

    public savePncFamilyPlanningMethod(familyPlanningMethodCommand: FamilyPlanningMethodCommand): Observable<any> {
        if (!familyPlanningMethodCommand.FPMethodId || familyPlanningMethodCommand.FPMethodId == null) {
            return of([]);
        }

        return this.http.post<any>(this.API_PMTCT_URL + '/api/FamilyPlanningMethods/AddFamilyPlanning',
            JSON.stringify(familyPlanningMethodCommand),
            httpOptions).pipe(
                tap(savePncFamilyPlanningMethod => this.errorHandler.log(`successfully saved pnc family planning method`)),
                catchError(this.errorHandler.handleError<any>('Error saving pnc family planning method'))
            );
    }


}

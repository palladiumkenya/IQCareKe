import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { ErrorHandlerService } from '../../shared/_services/errorhandler.service';
import { Observable, of } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';
import { Frequency } from '../models/frequency';
const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
    providedIn: 'root'
})

export class PharmacyService {
    private API_PHARM_URL = environment.API_PHARM_URL;
    private API_URL = environment.API_URL;



    constructor(private http: HttpClient,
        private errorHandler: ErrorHandlerService) {



    }


    public getFrequencylist(): Observable<Frequency[]> {
        return this.http.get<Frequency[]>(this.API_PHARM_URL + '/api/LookupPharmacy/getFrequency').pipe(
            tap(getFrequencylist => this.errorHandler.log('get frequency list details')),
            catchError(this.errorHandler.handleError<Frequency[]>('getFrequency list'))
        );
    }


    public getActiveFacilityModules(locationId: number): Observable<any[]> {
        return this.http.get<any[]>(this.API_PHARM_URL + '/api/LookupPharmacy/getActiveFacilityModules/' + locationId).pipe(
            tap(getActiveFacilityModules => this.errorHandler.log('get active modules details')),
            catchError(this.errorHandler.handleError<any[]>('get active modules list'))
        );
    }

    public hasPatientStartedTreatment(patientId: number): Observable<any[]> {
        return this.http.get<any[]>(this.API_PHARM_URL + '/api/PatientTreatmentTracker/HasPatientTreatmentStarted/' + patientId).pipe(
            tap(hasPatientStartedTreatment => this.errorHandler.log('get patient start treatment details')),
            catchError(this.errorHandler.handleError<any[]>('get patient start treatment details list'))
        );
    }

    public getPharmacyDrugList(pmscm: number, program: string, value: string): Observable<any[]> {
        return this.http.get<any[]>(this.API_PHARM_URL + '/api/PatientPharmacy/getPharmacyDrugList'
            + '?pmscm=' + pmscm + '&tp=' + program + '&filteritem=' + value).pipe(
                tap(getPharmacyDrugList => this.errorHandler.log('get drug list details')),
                catchError(this.errorHandler.handleError<any[]>('get patient drug  list'))
            );
    }

    public GetCurrentPatientVitalsInfo(personId: number): Observable<any> {
        return this.http.get<any>(this.API_URL + '/api/PatientServices/GetCurrentPersonVitals/' + personId).pipe(
            tap(GetCurrentPatientVitalsInfo => this.errorHandler.log('get patient vitals details')),
            catchError(this.errorHandler.handleError<any>('GetCurrentPatientVitalsInfo'))
        );
    }

    public getPharmacyDrugBatches(drug: string): Observable<any[]> {

        return this.http.get<any[]>(this.API_PHARM_URL + '/api/PatientPharmacy/getDrugBatches/' + drug).pipe(
            tap(getPharmacyDrugBatches => this.errorHandler.log('get drug batches details')),
            catchError(this.errorHandler.handleError<any[]>('get drug batches details list'))
        );
    }


    public AddSaveUpdatePharmacyRecord(ptnpk: number, patientmastervisitid: number, patientid: number,
        locationId: number, userid: number, PresciptionDate: string,
        prescribedBy: number,
        dispensedDate: string, pmscm: number, prescriptiondetails: any[], visitDate: Date, dispensedBy?: number): Observable<any[]> {
        const Indata = {
            'PatientMasterVisitId': patientmastervisitid,
            'Ptn_pk': ptnpk,
            'PatientId': patientid,
            'LocationId': locationId,
            'UserId': userid,
            'PrescriptionDate': PresciptionDate,
            'PrescribedBy': prescribedBy,
            'DispensedBy': dispensedBy,
            'DispensedDate': dispensedDate,
            'VisitDate': visitDate,
            'pmscm': pmscm,
            'PrescriptionDetails': prescriptiondetails

        };



        return this.http.post<any>(this.API_PHARM_URL + '/api/PatientPharmacy/saveUpdatePharmacy', JSON.stringify(Indata)
            , httpOptions).pipe(
                tap((AddSaveUpdatePharmacyRecord: any) => this.errorHandler.log(`Submit Pharmacy Records`)),
                catchError(this.errorHandler.handleError<any>('submitPharmacyRecords'))
            );

    }
    public getPharmacyCurrentRegimen(patientId: number): Observable<any[]> {
        return this.http.get<any[]>(this.API_PHARM_URL + '/api/PatientPharmacy/getPharmacyCurrentRegimen/' + patientId).pipe(
            tap(getPharmacyCurrentRegimen => this.errorHandler.log('get current regimen details')),
            catchError(this.errorHandler.handleError<any[]>('get patient current regimen list'))
        );
    }

    public getPharmacyRegimens(regimenline: string): Observable<any[]> {
        return this.http.get<any[]>(this.API_PHARM_URL + '/api/PatientPharmacy/getPharmacyRegimens/' + regimenline).pipe(
            tap(getPharmacyRegimens => this.errorHandler.log('get  regimen details')),
            catchError(this.errorHandler.handleError<any[]>('get  regimen list'))
        );
    }

    public getPharmacyVisit(patientId: number): Observable<any[]> {
        return this.http.get<any[]>(this.API_PHARM_URL + '/api/PatientPharmacy/getPharmacyVisit/' + patientId).pipe(
            tap(getPharmacyVisit => this.errorHandler.log('get Pharmacy Visit ')),
            catchError(this.errorHandler.handleError<any[]>('get Pharmacy Visit'))
        );
    }

    public getPharmacyVisitDetails(patientId: number, PatientMasterVisitId: number): Observable<any[]> {
        return this.http.get<any[]>(this.API_PHARM_URL + '/api/PatientPharmacy/getPharmacyVisitDrugDetails/'
            + patientId + '/' + PatientMasterVisitId).pipe(
                tap(getPharmacyVisitDetails => this.errorHandler.log('get Pharmacy Visit details')),
                catchError(this.errorHandler.handleError<any[]>('get Pharmacy Visit details '))
            );



    }
}
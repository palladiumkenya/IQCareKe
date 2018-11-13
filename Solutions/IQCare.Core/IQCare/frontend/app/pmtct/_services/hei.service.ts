import { forkJoin, Observable, of } from 'rxjs/index';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ErrorHandlerService } from '../../shared/_services/errorhandler.service';
import { environment } from '../../../environments/environment';
import { tap, catchError } from 'rxjs/operators';
import { Vaccination } from '../_models/hei/Vaccination';
import { Milestone } from '../_models/hei/Milestone';
import { PatientIcf } from '../_models/hei/PatientIcf';
import { PatientIcfAction } from '../_models/hei/PatientIcfAction';
import { PatientIptWorkup } from '../_models/hei/PatientIptWorkup';
import { PatientIptOutcome } from '../_models/hei/PatientIptOutcome';
import { PatientIpt } from '../_models/hei/PatientIpt';
import { LabOrder } from '../_models/hei/LabOrder';
import { OrdVisitCommand } from '../_models/hei/OrdVisitCommand';
import { CompleteLabOrderCommand } from '../_models/hei/CompleteLabOrderCommand';


const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
    providedIn: 'root'
})
export class HeiService {
    private API_URL = environment.API_URL;
    private API_LAB_URL = environment.API_LAB_URL;

    constructor(private http: HttpClient, private errorHandler: ErrorHandlerService) { }

    public getHeiVisitDetails(patientId: number) {
        return this.http.get<any[]>(this.API_URL + '/api/HeiVisitDetails/' + patientId).pipe(
            tap(getHeiVisitDetails => this.errorHandler.log('get HEI visit data')),
            catchError(this.errorHandler.handleError<any[]>('getHeiVisitDetails'))
        );
    }

    public saveHeiVisitDetails(visitDetails: any): Observable<any> {
        return this.http.post<any>(this.API_URL + '/api/VisitDetails/AddPNCVisitDetails', JSON.stringify(visitDetails), httpOptions).pipe(
            tap(saveHeiVisitDetails => this.errorHandler.log(`successfully added hei visit details`)),
            catchError(this.errorHandler.handleError<any>('Error saving hei delivery'))
        );
    }

    public saveImmunizationHistory(vaccination: Vaccination[]): Observable<Vaccination[]> {

        return this.http.post<any>(this.API_URL + '/api/ImmunizationHistory', JSON.stringify(vaccination), httpOptions).pipe(
            tap(saveImmunizationHistory => this.errorHandler.log(`successfully added hei Immunization History`)),
            catchError(this.errorHandler.handleError<any>('Error saving hei Immunization History'))
        );
    }

    public saveMilestoneHistory(milestone: Milestone[]): Observable<Milestone[]> {
        return this.http.post<any>(this.API_URL + '/api/HeiMilestone', JSON.stringify(milestone), httpOptions).pipe(
            tap(saveMilestoneHistory => this.errorHandler.log(`successfully added hei Milestone History`)),
            catchError(this.errorHandler.handleError<any>('Error saving hei milestone History'))
        );
    }

    public saveHieDelivery(patientId: number, patientMasterVisitId: number, userId: number,
        isMotherRegistered: boolean, heidelivery: any, maternalHistory: any): Observable<any> {
        const Indata = {
            'PatientId': patientId,
            'PatientMasterVisitId': patientMasterVisitId,
            'PlaceOfDelivery': heidelivery['placeofdelivery'],
            'ModeOfDelivery': heidelivery['modeofdelivery'],
            'BirthWeight': heidelivery['birthweight'],
            'ProphylaxisReceived': heidelivery['arvprophylaxisreceived'],
            'ProphylaxisReceivedOther': heidelivery['arvprophylaxisother'],

            'MotherIsRegistered': isMotherRegistered,
            'MotherPersonId': maternalHistory['motherpersonid'],
            'MotherStatusId': maternalHistory['stateofmother'],
            'PrimaryCareGiverID': maternalHistory['primarycaregiver'],
            'MotherName': maternalHistory['nameofmother'],
            'MotherCCCNumber': maternalHistory['cccno'],
            'MotherPMTCTDrugsId': maternalHistory['pmtctheimotherreceivedrugs'],
            'MotherPMTCTRegimenId': maternalHistory['pmtctheimotherregimen'],
            'MotherPMTCTRegimenOther': maternalHistory['otherspecify'],
            'MotherArtInfantEnrolId': maternalHistory['motheronartatinfantenrollment'],
            'MotherArtInfantEnrolRegimenId': maternalHistory['pmtctheimotherdrugsatinfantenrollment'],

            'CreatedBy': userId
        };

        return this.http.post<any>(this.API_URL + '/api/DeliveryMaternalHistory', JSON.stringify(Indata), httpOptions).pipe(
            tap(saveHieDelivery => this.errorHandler.log(`successfully added hei delivery`)),
            catchError(this.errorHandler.handleError<any>('Error saving hei delivery'))
        );
    }

    public saveTbAssessment(patientIcf: PatientIcf, patientIcfAction: PatientIcfAction): Observable<any> {

        const Icf = this.http.post<any>(this.API_URL + '/api/tbAssessment/AddPatientIcf', JSON.stringify(patientIcf), httpOptions).pipe(
            tap(saveTbAssessmentIcf => this.errorHandler.log(`successfully added hei patient icf`)),
            catchError(this.errorHandler.handleError<any>('Error saving hei patient icf Action'))
        );

        const IcfAction = this.http.post<any>(this.API_URL + '/api/tbAssessment/AddPatientIcfAction', JSON.stringify(patientIcfAction),
            httpOptions)
            .pipe(
                tap(saveTbAssessmentIcfAction => this.errorHandler.log(`successfully added hei patient icf Action`)),
                catchError(this.errorHandler.handleError<any>('Error saving hei patient icf'))
            );
        return forkJoin([Icf, IcfAction]);
    }

    public saveIptWorkup(patientIptWorkup: PatientIptWorkup): Observable<PatientIptWorkup> {
        return this.http.post<any>(this.API_URL + '/api/IptWorkup', JSON.stringify(patientIptWorkup), httpOptions).pipe(
            tap(saveIptWorkup => this.errorHandler.log(`successfully added IPT Workup`)),
            catchError(this.errorHandler.handleError<any>('Error saving IPT workup'))
        );
    }

    public saveIptOutcome(patientIptOutcome: PatientIptOutcome): Observable<PatientIptOutcome> {
        return this.http.post<any>(this.API_URL + '/api/IptOutcome', JSON.stringify(patientIptOutcome), httpOptions).pipe(
            tap(saveIptOutcome => this.errorHandler.log(`successfully added IPT Outcome`)),
            catchError(this.errorHandler.handleError<any>('Error saving IPT Outcome'))
        );
    }

    public saveIpt(patientIpt: PatientIpt): Observable<PatientIptOutcome> {
        return this.http.post<any>(this.API_URL + '/api/PatientIpt', JSON.stringify(patientIpt), httpOptions).pipe(
            tap(saveIpt => this.errorHandler.log(`successfully added IPT `)),
            catchError(this.errorHandler.handleError<any>('Error saving IPT '))
        );
    }

    public saveHeiLabOrder(labOrder: LabOrder): Observable<any> {
        if (labOrder.LabTests.length == 0) {
            return of([]);
        }

        return this.http.post<any>(this.API_LAB_URL + '/api/LabOrder/AddLabOrder', JSON.stringify(labOrder), httpOptions).pipe(
            tap(saveHeiLabOrder => this.errorHandler.log(`successfully added laborder`)),
            catchError(this.errorHandler.handleError<any>('Error saving laborder'))
        );
    }

    public saveCompleteHeiLabOrder(completeLabOrderCommand: CompleteLabOrderCommand): Observable<any> {
        return this.http.post(this.API_LAB_URL + '/api/LabOrder/CompleteLabOrder', JSON.stringify(completeLabOrderCommand),
            httpOptions).pipe(
                tap(saveCompleteHeiLabOrder => this.errorHandler.log(`successfully completed hei laborder`)),
                catchError(this.errorHandler.handleError<any>('Error completing hei laborder'))
            );
    }

    public getPatientById(patientId: number): Observable<any> {
        return this.http.get<any>(this.API_URL + '/api/Register/GetPatientById/' + patientId).pipe(
            tap(getPatientById => this.errorHandler.log(`successfully fetched patient`)),
            catchError(this.errorHandler.handleError<any>('Getting patient'))
        );
    }

    public saveOrdVisit(ordVisitCommand: OrdVisitCommand, laborder: LabOrder): Observable<any> {
        if (laborder.LabTests.length == 0) {
            return of([]);
        }

        return this.http.post<any>(this.API_URL + '/api/PatientMasterVisit/addOrdVisit', JSON.stringify(ordVisitCommand), httpOptions).pipe(
            tap(saveOrdVisit => this.errorHandler.log(`successfully added ordVisit`)),
            catchError(this.errorHandler.handleError<any>('Error saving ordVisit'))
        );
    }

    public getHeiLabTests(): Observable<any[]> {
        const options = JSON.stringify(['PCR', 'Viral Load', 'HIV Rapid Test']);
        return this.http.post<any[]>(this.API_LAB_URL + '/api/LabTests/GetLabTests', options, httpOptions).pipe(
            tap(getHeiLabTests => this.errorHandler.log(`successfully fetched hei labtests`)),
            catchError(this.errorHandler.handleError<any>('Error fetching labtests'))
        );
    }

    public getLabTestPametersByLabTestId(labTestId: number): Observable<any[]> {
        return this.http.get<any[]>(this.API_LAB_URL + '/api/LabTests/GetLabTestPametersByLabTestId/' + labTestId).pipe(
            tap(getHeiLabTests => this.errorHandler.log(`successfully fetched labtest parameters`)),
            catchError(this.errorHandler.handleError<any>('Error fetching labtest parameters'))
        );
    }
}

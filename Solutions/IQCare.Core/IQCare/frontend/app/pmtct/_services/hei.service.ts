import { Observable } from 'rxjs/index';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ErrorHandlerService } from '../../shared/_services/errorhandler.service';
import { environment } from '../../../environments/environment';
import { tap, catchError } from 'rxjs/operators';
import {Vaccination} from '../_models/hei/Vaccination';
import {Milestone} from '../_models/hei/Milestone';


const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
    providedIn: 'root'
})
export class HeiService {
    private API_URL = environment.API_URL;

    constructor(private http: HttpClient, private errorHandler: ErrorHandlerService) { }

    public getHeiVisitDetails(patientId: number) {
        return this.http.get<any[]>(this.API_URL + '/api/HeiVisitDetails/' + patientId).pipe(
            tap(getHeiVisitDetails => this.errorHandler.log('get HEI visit data')),
            catchError(this.errorHandler.handleError<any[]>('getHeiVisitDetails'))
        );
    }

    public saveHeiVisitDetails(patientId: number, patientMasterVisitId: number, visitData: any,
                            userId: number): Observable<any> {
        const visitDetailsData = {
            'Id': 0,
            'PatientMasterVisitId': patientMasterVisitId,
            'PatientId': patientId,
            'VisitDate': visitData['visitDate'],
            'VisitType': visitData['visitType'],
            'CreatedDate': new Date(),
            'CreatedBy': userId,
            'DeleteFlag': 0
        };
        return this.http.post<any>(this.API_URL + '/api/HeiVisitDetails', JSON.stringify(visitDetailsData), httpOptions).pipe(
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


}

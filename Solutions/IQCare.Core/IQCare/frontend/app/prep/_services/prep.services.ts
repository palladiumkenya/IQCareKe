import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { ErrorHandlerService } from '../../shared/_services/errorhandler.service';
import { tap, catchError } from 'rxjs/operators';
import { Observable } from 'rxjs';


const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
    providedIn: 'root'
})


export class PrepService {
    private API_URL = environment.API_PREP_URL;

    constructor(private http: HttpClient, private errorHandler: ErrorHandlerService) {

    }


    AddEditBehaviourRisk(EncounterTypeId: number,
        createdby: number, patientid: number, patientmastervisitid: number, visitdate: string, serviceareaId:
            number, riskassessment: any[], clinicalnotes: any[]) {
        const Indata = {
            'EncounterTypeId': EncounterTypeId,
            'UserId': createdby,
            'PatientId': patientid,
            'PatientMasterVisitId': patientmastervisitid,
            'VisitDate': visitdate,
            'ServiceAreaId': serviceareaId,
            'riskAssessments': riskassessment,
            'ClinicalNotes': clinicalnotes
        };



        return this.http.post<any>(this.API_URL + '/api/BehaviourRisk/AddAssessmentVisitDetail', JSON.stringify(Indata)
            , httpOptions).pipe(
                tap((submitRiskAssessments: any) => this.errorHandler.log(`Submit RiskAssessment Results`)),
                catchError(this.errorHandler.handleError<any>('submitRiskAssessmentResults'))
            );

    }

    CheckencounterExists(patientid: number): Observable<any[]> {
        const Indata = {
            'PatientId': patientid
        };
        return this.http.post<any>(this.API_URL + '/api/BehaviourRisk/Encounterexists', JSON.stringify(Indata), httpOptions)
        .pipe (tap (CheckencounterExists => this.errorHandler.log('checked if RiskAssessmentEncounter Exists' )),
          catchError(this.errorHandler.handleError<any[]>('CheckencounterExists'))
        );
    }

    GetAssessmentDetails(patientid: number, patientmastervisitid: number): Observable<any[]> {
        const Indata = {
            'PatientId': patientid,
            'PatientMasterVisitId': patientmastervisitid
        };

        return this.http.post<any>(this.API_URL + '/api/BehaviourRisk/GetAssessmentFormDetails', JSON.stringify(Indata), httpOptions)
        .pipe (tap (GetAssessmentDetails => this.errorHandler.log('GetAssessment Form Details ' )),
          catchError(this.errorHandler.handleError<any[]>('GetAssessmentDetails'))
        );

    }
}

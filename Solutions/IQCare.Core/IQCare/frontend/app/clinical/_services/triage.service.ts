import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ErrorHandlerService } from '../../shared/_services/errorhandler.service';
import { AddPatientVitalCommand } from "../_models/AddPatientVitalCommand";
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';
import { CalculateZscoreCommand } from '../_models/CalculateZscoreCommand';
import { PersonHomeService } from '../../dashboard/services/person-home.service';
import * as moment from 'moment';
import { PersonView } from '../../records/_models/personView';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
    providedIn: 'root'
})
export class TriageService {
    private API_URL = environment.API_URL;

    constructor(private httpClient: HttpClient, private errorHandlerService: ErrorHandlerService,
        private personHomeService :PersonHomeService) {

    }


    public AddPatientVitalInfo(patientVitalCommand: AddPatientVitalCommand): Observable<any> {
        return this.httpClient.post<AddPatientVitalCommand>(this.API_URL + '/api/PatientVitals/Add',
            JSON.stringify(patientVitalCommand), httpOptions).pipe(
                tap(AddPatientTriageInfo => this.errorHandlerService.log(`successfully added patient vitals info`)),
                catchError(this.errorHandlerService.handleError<any>('Error adding ntmastervisit'))
            );
    }


    public GetPatientVitalsInfo(masterVisitId: number): Observable<any> {
        return this.httpClient.get<any>(this.API_URL + '/api/PatientVitals/GetByMasterVisitId/' + masterVisitId).pipe(
            tap(GetPatientVitalsInfo => this.errorHandlerService.log('get patient master visit details')),
            catchError(this.errorHandlerService.handleError<any>('GetPatientVitalsInfo'))
        );
    }

    public calculateZscore(zscoreCommand : CalculateZscoreCommand) : any {
        return this.httpClient.post<CalculateZscoreCommand>(this.API_URL + '/api/PatientVitals/CalculateZscore',
        JSON.stringify(zscoreCommand), httpOptions).pipe(
            tap(calculatePatientZscore => this.errorHandlerService.log(`successfully calculated patient zscores`)),
            catchError(this.errorHandlerService.handleError<any>('Error calculating patient zscore'))
        );
    }

    public calculateBmi(weight: number, heightInCm: number): any {
        const heightInMetres = heightInCm / 100;
        return weight / (heightInMetres * heightInMetres);
    }

   
    public getPersonDetails(personId:number) : any {
       var personDetails : PersonView = null;
      this.personHomeService.getPatientByPersonId(personId).subscribe(person=>{
          console.log(">> Person "+person.personId);
          personDetails = person
          return personDetails;
      });
    }

    public qualifiesForZscoreCalculation(dateOfBirth:Date) : any{
       var zscoreMinimumAgeQualification = 15;

        var dobMoment = moment(dateOfBirth);
        var currentDate = moment(Date());
       
        var age = moment.duration(currentDate.diff(dobMoment)).asYears().toFixed(1);
        return parseInt(age) <= zscoreMinimumAgeQualification;
    }
}

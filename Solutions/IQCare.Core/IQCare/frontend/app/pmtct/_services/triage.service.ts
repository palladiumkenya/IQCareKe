import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ErrorHandlerService } from '../../shared/_services/errorhandler.service';
import { AddPatientVitalCommand } from "../../clinical/_models/AddPatientVitalCommand";
import { CalculateZscoreCommand } from "../../clinical/_models/CalculateZscoreCommand"
import { PersonHomeService } from "../../dashboard/services/person-home.service";
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';
import * as moment from 'moment';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})

export class TriageService {
  private API_URL = environment.API_URL;
 
  constructor(private httpClient: HttpClient, private personHomeService : PersonHomeService, 
    private errorHandlerService : ErrorHandlerService ) {

   }

  
   public AddPatientVitalInfo(patientVitalCommand:AddPatientVitalCommand): Observable<any> {
    return this.httpClient.post<AddPatientVitalCommand>(this.API_URL + '/api/PatientVitals/Add',
    JSON.stringify(patientVitalCommand), httpOptions).pipe(
        tap(AddPatientTriageInfo => this.errorHandlerService.log(`successfully added patient vitals info`)),
        catchError(this.errorHandlerService.handleError<any>('Error adding ntmastervisit'))
    );
   }


   public GetPatientVitalsInfo(masterVisitId:number) : Observable<any> {
    return this.httpClient.get<any>(this.API_URL + '/api/PatientVitals/GetByMasterVisitId/'+masterVisitId).pipe(
      tap(GetPatientVitalsInfo => this.errorHandlerService.log('get patient master visit details')),
      catchError(this.errorHandlerService.handleError<any>('GetPatientVitalsInfo'))
  );
   }

   public calculateBmi(weight:number, heightInCm:number) : any {
    var heightInMetres = heightInCm / 100;
    return weight / (heightInMetres * heightInMetres)
 }


public calculatePatientZscore(calculateZscoreCommand: CalculateZscoreCommand): Observable<any> {
  return this.httpClient.post<CalculateZscoreCommand>(this.API_URL + '/api/PatientVitals/CalculateZscore',
  JSON.stringify(calculateZscoreCommand), httpOptions).pipe(
      tap(calculatePatientZscore => this.errorHandlerService.log(`succesfully calculated patient zscore`)),
      catchError(this.errorHandlerService.handleError<any>('Error calculating patient Zscore'))
  );
}


public getPatientDetails(patiendId:number): any {
   this.personHomeService.getPatientById(patiendId).subscribe(pat=>{
      return pat;
  });
}


//Patient less than or equal to 15 qualifies for zscore calculation
public qualifiesForZscoreCalculation(dateOfBirth:Date) : any {
 var minimumZscoreCalculationAge = 15;
  var dob = moment(dateOfBirth);
  var todaysDate = moment(); 
  const age = moment.duration(todaysDate.diff(dob)).asYears().toFixed(1);
  return parseInt(age) <= minimumZscoreCalculationAge;
}

}

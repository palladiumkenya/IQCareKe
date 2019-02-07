import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { ErrorHandlerService } from '../../shared/_services/errorhandler.service';
import { catchError, tap } from 'rxjs/operators';
import { Observable, of, BehaviorSubject, Subscription } from 'rxjs/index';
import { PatientMasterVisitEncounter } from '../_models/PatientMasterVisitEncounter';
import { PatientProfileViewModel } from '../_models/viewModel/PatientProfileViewModel';
import { PatientDeliveryInformationViewModel } from '../_models/viewModel/PatientDeliveryInformationViewModel';
import { PatientScreeningCommand } from '../_models/PatientScreeningCommand';
import { HivStatusCommand } from '../_models/HivStatusCommand';
import { BabyConditionCommand } from '../maternity/commands/baby-condition-command';
import { FormGroup } from '@angular/forms';
import { LookupItemService } from '../../shared/_services/lookup-item.service';
import { ApgarScoreCommand } from '../maternity/commands/apgar-score-command';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};


@Injectable({
    providedIn: 'root'
})
export class MaternityService {
    private API_URL = environment.API_URL;
    private API_PMTCT_URL = environment.API_PMTCT_URL;
    private babyDataMessageSource = new BehaviorSubject([]);
    currentBabyData = this.babyDataMessageSource.asObservable();
    private lookUpItemSubscription: Subscription;
    apgarScoreOptions: any[] = [];

    constructor(private http: HttpClient, private errorHandler: ErrorHandlerService,
        private lookUpService: LookupItemService) {
        this.getLookupItems('ApgarScore', this.apgarScoreOptions);
        // console.log(this.apgarScoreOptions.length + ' >> Apgar score')
    }


    public updateBabyDataInfo(babyInfo: any) {
        this.babyDataMessageSource.next(babyInfo)
    }
    public saveMaternityMasterVisit(patientMasterVisitEncounter: PatientMasterVisitEncounter): Observable<any> {
        return this.http.post<PatientMasterVisitEncounter>(this.API_URL + '/api/PatientMasterVisit',
            JSON.stringify(patientMasterVisitEncounter), httpOptions).pipe(
                tap(saveMaternityMasterVisit => this.errorHandler.log(`successfully added maternity patientmastervisit`)),
                catchError(this.errorHandler.handleError<any>('Error saving maternity patientmastervisit'))
            );
    }

    public getCurrentVisitDetails(patientId: number, serviceAreaName: string) {
        return this.http.get<any>(this.API_URL + '/api/AncVisitDetails/GetVisitDetailsByServiceAreaName/' + patientId + '/'
            + serviceAreaName).pipe(
                tap(getCurrentVisitDetails => this.errorHandler.log('get current visit data')),
                catchError(this.errorHandler.handleError<any[]>('getCurrentVisitDetails'))
            );
    }

    public getPregnancyDetails(patientId: number) {
        return this.http.get<any>(this.API_URL + '/api/VisitDetails/GetPregnancyProfile/' + patientId).pipe(
            tap(getPregnancyDetails => this.errorHandler.log('get current pregnancy details')),
            catchError(this.errorHandler.handleError<any[]>('getPregnancyDetails'))
        );
    }

    public getMaternityEncounter(patientId: number) {
        return this.http.get<any>(this.API_PMTCT_URL + '/api/MaternityEncounter/' + patientId).pipe(
            tap(getMaternityEncounter => this.errorHandler.log('get current maternity details')),
            catchError(this.errorHandler.handleError<any[]>('GetMaternityEncounter'))
        );
    }

    public saveVisitDetails(visitDetails: any): Observable<any> {
        return this.http.post(this.API_URL + '/api/AncVisitDetails/Post', JSON.stringify(visitDetails), httpOptions).pipe(
            tap(saveVisitDetals => this.errorHandler.log(`successfully added maternity visits`)),
            catchError(this.errorHandler.handleError<any>('Error saving maternity visit'))
        );
    }

    public savePregnancyProfile(motherProfile: any): Observable<any> {
        return this.http.post(this.API_URL + '/api/VisitDetails/postPregnancy', JSON.stringify(motherProfile), httpOptions).pipe(
            tap(savePregnancyProfile => this.errorHandler.log(`successfully added pregnancy profile`)),
            catchError(this.errorHandler.handleError<any>('Error pregnancy profile'))
        );
    }

    public saveDiagnosis(diagnosis: any): Observable<any> {
        return this.http.post(this.API_PMTCT_URL + '/api/PatientDiagnosis/AddDiagnosis', JSON.stringify(diagnosis), httpOptions).pipe(
            tap(saveVisitDetals => this.errorHandler.log(`successfully added maternity diagnosis`)),
            catchError(this.errorHandler.handleError<any>('Error saving maternity diagnosis'))
        );
    }

    public savePatientDelivery(delivery: any): Observable<any> {
        return this.http.post(this.API_PMTCT_URL + '/api/MaternityPatientDeliveryInfo/AddPatientDeliveryInfo', JSON.stringify(delivery),
            httpOptions).pipe(
                tap(savePatientDelivery => this.errorHandler.log(`successfully added maternity delivery info`)),
                catchError(this.errorHandler.handleError<any>('Error saving maternity Delivery info'))
            );
    }

    public saveBabySection(babysection: any): Observable<any> {
        return this.http.post(this.API_PMTCT_URL + '/api/MaternityPatientDeliveryInfo/AddDeliveredBabyBirthInfoCollection',
            JSON.stringify(babysection),
            httpOptions).pipe(
                tap(saveBabySection => this.errorHandler.log(`successfully added maternity baby section`)),
                catchError(this.errorHandler.handleError<any>('Error saving maternity baby section'))
            );
    }

    public addNewBabyInfo(babyInfoCommand: any): Observable<any> {
        return this.http.post(this.API_PMTCT_URL + '/api/MaternityPatientDeliveryInfo/AddDeliveredBabyBirthInfo',
            JSON.stringify(babyInfoCommand),
            httpOptions).pipe(
                tap(addNewBabyInfo => this.errorHandler.log(`successfully added maternity baby section`)),
                catchError(this.errorHandler.handleError<any>('Error saving maternity baby section'))
            );
    }

    public updateBabyInfo(babyInfo: any): Observable<any> {
        return this.http.post(this.API_PMTCT_URL + '/api/MaternityPatientDeliveryInfo/UpdateDeliveredBabyBirthInfo', JSON.stringify(babyInfo),
            httpOptions).pipe(tap(updateBabyInfo => this.errorHandler.log(`successfully updated baby info`)),
                catchError(this.errorHandler.handleError<any>('Error updating baby information'))
            );
    }

    public saveMaternalDrugAdministration(drug: any): Observable<any> {
        return this.http.post(this.API_PMTCT_URL + '/api/PatientDrugAdministration/Add',
            JSON.stringify(drug), httpOptions).pipe(
                tap(saveMaternalDrugAdministration => this.errorHandler.log(`successfully added maternal drug administration`)),
                catchError(this.errorHandler.handleError<any>('Error saving maternal drug administration'))
            );
    }


    public savePartnerTesting(partner: any): Observable<any> {
        return this.http.post(this.API_PMTCT_URL + '/api/PatientPartnerTesting/AddPartnerTesting',
            JSON.stringify(partner), httpOptions).pipe(
                tap(savePartnerTesting => this.errorHandler.log(`successfully added Partner testing details`)),
                catchError(this.errorHandler.handleError<any>('Error saving Partner testing details'))
            );
    }


    public savePatientEducation(patientEducation: any): Observable<any> {
        if (!patientEducation.IsCounsellingDone) {
            return of([]);
        }

        return this.http.post(this.API_URL + '/api/PatientEducationExamination/AddPatientCounsellingInfo',
            JSON.stringify(patientEducation), httpOptions).pipe(
                tap(savePatientEducation => this.errorHandler.log(`successfully added patient education details`)),
                catchError(this.errorHandler.handleError<any>('Error saving patient education details'))
            );
    }

    public getPatientEducation(patientId: number, patientMasterVisitId: number): Observable<any[]> {
        return this.http.get<any[]>(this.API_URL + '/api/PatientEducationExamination/GetPatientEducation/'
            + patientId + '/' + patientMasterVisitId).pipe(
                tap(getPatientEducation =>
                    this.errorHandler.log(`successfully fetched patient education by patientId: `
                        + patientId + ` and patientmastervisitid: ` + patientMasterVisitId)),
                catchError(this.errorHandler.handleError<any>('Error fetching patient education'))
            );
    }

    public updatePatientEducation(): Observable<any> {
        return this.http.post(this.API_URL + '/api/PatientEducationExamination/UpdatePatientCounsellingInfo',
            JSON.stringify(''), httpOptions).pipe(
                tap(updatePatientEducation => this.errorHandler.log(`successfully updated patient education details`)),
                catchError(this.errorHandler.handleError<any>('Error updating patient education details'))
            );
    }

    public saveDischarge(discharge: any): Observable<any> {
        return this.http.post(this.API_PMTCT_URL + '/api/MaternityPatientDeliveryInfo/DischargePatient',
            JSON.stringify(discharge), httpOptions).pipe(
                tap(saveDischarge => this.errorHandler.log(`successfully added discharge details`)),
                catchError(this.errorHandler.handleError<any>('Error saving Partner discharge details'))
            );
    }

    public saveReferrals(referral: any): Observable<any> {
        return this.http.post(this.API_URL + '/api/PatientReferralAndAppointment/AddPatientReferralInfo',
            JSON.stringify(referral), httpOptions).pipe(
                tap(saveReferrals => this.errorHandler.log(`successfully added Referral details`)),
                catchError(this.errorHandler.handleError<any>('Error saving Referral details'))
            );
    }

    public savePncHivStatus(hivStatusCommand: HivStatusCommand, anyTests: any[]): Observable<any> {
        if (anyTests.length == 0) {
            return of([]);
        }

        const Indata = {
            'Encounter': hivStatusCommand
        };

        return this.http.post<any>(this.API_URL + '/api/HtsEncounter', JSON.stringify(Indata), httpOptions).pipe(
            tap(savePncBabyExamination => this.errorHandler.log(`successfully saved pnc hiv status`)),
            catchError(this.errorHandler.handleError<any>('Error saving pnc hiv status'))
        );
    }

    public saveNextAppointment(appointment: any): Observable<any> {
        return this.http.post(this.API_URL + '/api/PatientReferralAndAppointment/AddPatientNextAppointment', JSON.stringify(appointment),
            httpOptions).pipe(
                tap(saveReferrals => this.errorHandler.log(`successfully added Referral details`)),
                catchError(this.errorHandler.handleError<any>('Error saving Referral details'))
            );
    }

    public getInitialProfileDetailsByPatientd(patientId: number): Observable<PatientProfileViewModel> {
        return this.http.get<PatientProfileViewModel>(this.API_URL + '/api/VisitDetails/GetInitialProfileDetailsByPatientId/' + patientId)
            .pipe(
                tap(getInitialProfileDetailsByPatientd => this.errorHandler.log(`successfully fetched initial profile`)),
                catchError(this.errorHandler.handleError<any>('Fetching initial profile'))
            );
    }

    public getPatientDeliveryInfoByPregnancyId(pregnancyId: number): Observable<PatientDeliveryInformationViewModel[]> {
        return this.http.get<PatientDeliveryInformationViewModel[]>(this.API_PMTCT_URL
            + '/api/MaternityPatientDeliveryInfo/GetDeliveryInfoByPregnancyId/' + pregnancyId)
            .pipe(
                tap(getPatientDeliveryInfoByPregnancyId => this.errorHandler.log(`successfully fetched patient delivery info by profile Id`)),
                catchError(this.errorHandler.handleError<any>('Error Fetching patient delivery info by profile Id'))
            );
    }

    public saveScreening(patientScreeningCommand: PatientScreeningCommand): Observable<any> {
        if (patientScreeningCommand.ScreeningTypeId == 0) {
            return of([]);
        }

        return this.http.post<any>(this.API_PMTCT_URL + '/api/PatientScreening', JSON.stringify(patientScreeningCommand), httpOptions)
            .pipe(
                tap(saveScreening => this.errorHandler.log(`successfully added patient screening`)),
                catchError(this.errorHandler.handleError<any>('Error saving patient screening'))
            );
    }

    public getPatientScreening(patientId: number, patientMasterVisitId: number): Observable<any[]> {
        return this.http.get<any[]>(this.API_PMTCT_URL + '/api/PatientScreening/' + patientId + '/' + patientMasterVisitId).pipe(
            tap(getPatientScreening => this.errorHandler.log(`successfully fetched patient screening`)),
            catchError(this.errorHandler.handleError<any>('Error fetching patient screening'))
        );
    }

    public GetDeliveredBabyInfo(masterVisitId: number) {
        return this.http.get<any>(this.API_PMTCT_URL + '/api/MaternityPatientDeliveryInfo/GetDeliveredBabyInfoByMasterVisitId/'
            + masterVisitId).pipe(
                tap(GetDeliveredBabyInfoByMasterVisitId => this.errorHandler.log('get delivered baby info by master Id')),
                catchError(this.errorHandler.handleError<any[]>('GetDeliveredBabyInfoByMasterVisitId'))
            );
    }


    public GetPatientDeliveryInfo(masterVisitId: number) {
        return this.http.get<any>(this.API_PMTCT_URL + '/api/MaternityPatientDeliveryInfo/GetDeliveryInfoByMasterVisitId/' +
            masterVisitId).pipe(
                tap(GetPatientDeliveryInfoByMasterVisitId => this.errorHandler.log('get patient delivery info by master Id')),
                catchError(this.errorHandler.handleError<any[]>('GetPatientDeliveryInfoByMasterVisitId'))
            );
    }

    public GetPatientDischargeInfo(masterVisitId: number) {
        return this.http.get<any>(this.API_PMTCT_URL + '/api/MaternityPatientDeliveryInfo/GetDischargeInfoByMasterVisitId/' +
            masterVisitId).pipe(
                tap(GetPatientDischargeInfo => this.errorHandler.log('get patient discharge info by master Id')),
                catchError(this.errorHandler.handleError<any[]>('GetPatientDischargeInfo'))
            );
    }

    public GetPatientDiagnosisInfo(masterVisitId: number) {
        return this.http.get<any>(this.API_PMTCT_URL + '/api/PatientDiagnosis/GetDiagnosisByMasterVisitId/' + masterVisitId).pipe(
            tap(GetPatientDiagnosisInfo => this.errorHandler.log('get patient diagnosis info by master Id')),
            catchError(this.errorHandler.handleError<any[]>('GetPatientDiagnosisInfo'))
        );
    }

    public getMaternityLookUpOptionByName(lookUpOptions: any[], lookupName: string): any {
        for (let index = 0; index < lookUpOptions.length; index++) {
            if (lookUpOptions[index].itemName.toUpperCase() === lookupName.toUpperCase()) {
                return lookUpOptions[index];
            }
        }
        return null;
    }

    public getMaternityLoopUpOptionById(lookUpOptions: any[], lookUpId: any): any {
        for (let index = 0; index < lookUpOptions.length; index++) {
            if (lookUpOptions[index].itemId == lookUpId) {
                return lookUpOptions[index];
            }
        }
        return null;
    }


    public buildAddBabyCommandModel(babyFormGroup: FormGroup): BabyConditionCommand {

        var apgarScores: ApgarScoreCommand[] = [];
        apgarScores.push(
            {
                ApgarScoreId: this.GetScoreTypeId("Apgar Score 1 min"),
                Score: babyFormGroup.get('agparScore1min').value
            },
            {
                ApgarScoreId: this.GetScoreTypeId("Apgar Score 5 min"),
                Score: babyFormGroup.get('agparScore5min').value
            },
            {
                ApgarScoreId: this.GetScoreTypeId("Apgar Score 10 min"),
                Score: babyFormGroup.get('agparScore10min').value
            })

        const babyCondition: BabyConditionCommand = {
            Sex: babyFormGroup.get('babySex').value.itemId,
            BirthWeight: babyFormGroup.get('birthWeight').value,
            DeliveryOutcome: babyFormGroup.get('outcome').value.itemId,
            ApgarScores: apgarScores,
            ResuscitationDone: (babyFormGroup.get('resuscitationDone').value.itemName == 'Yes') ? true : false,
            BirthDeformity: (babyFormGroup.get('deformity').value.itemName == 'Yes') ? true : false,
            TeoGiven: (babyFormGroup.get('teoGiven').value.itemName == 'Yes') ? true : false,
            BreastFedWithinHour: (babyFormGroup.get('breastFed').value.itemName == 'Yes') ? true : false,
            Comment: babyFormGroup.get('comment').value,
            BirthNotificationNumber: babyFormGroup.get('notificationNumber').value
        }

        return babyCondition;
    }

    private getLookupItems(groupName: string, objOptions: any[] = []) {
        this.lookUpItemSubscription = this.lookUpService.getByGroupName(groupName)
            .subscribe(
                p => {
                    const options = p['lookupItems'];
                    console.log(options.length + ' OPtions Man')
                    for (let i = 0; i < options.length; i++) {
                        objOptions.push({ 'itemId': options[i]['itemId'], 'itemName': options[i]['itemName'] });
                    }
                    console.log("New options Length ?? " + objOptions.length)
                });
    }

    private GetScoreTypeId(scoreType: string): any {
        var score = this.apgarScoreOptions.filter(x => x.itemName == scoreType);
        if (score.length < 0)
            return 0;
        return score[0].itemId;
    }

}

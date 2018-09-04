import { VisitDetails } from './../_models/visitDetails';
import { Component, EventEmitter, Input, OnInit, Output, OnChanges, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {Subscription} from 'rxjs';
import {LookupItemService} from '../../shared/_services/lookup-item.service';
import {NotificationService} from '../../shared/_services/notification.service';
import { SnotifyService } from 'ng-snotify';
import * as moment from 'moment';
import {PatientProfile} from '../_models/patientProfile';
import {VisitDetailsService} from '../_services/visit-details.service';
import {PatientPregnancy} from '../_models/PatientPregnancy';

@Component({
  selector: 'app-visit-details',
  templateUrl: './visit-details.component.html',
  styleUrls: ['./visit-details.component.css']
})
export class VisitDetailsComponent implements OnInit, OnChanges {
    visitDetailsFormGroup: FormGroup;
    isLinear: true;
    entryPoints: any[];
    dateLMP: Date;
    lookupItemView$: Subscription;
    Ancprofile$: Subscription;
    pregnancy$: Subscription;
    patientProfile: PatientProfile;
    pregnancyProfile: PatientPregnancy;
    visitDetails: VisitDetails;
    private personId: number;
    private patientId: number;
    public ancVisitTypes: any[] = [];
    @Output() nextStep = new EventEmitter<VisitDetails>(); 
    @Input() visitProtocol: VisitDetails;
    
    constructor(private fb: FormBuilder, private _lookupItemService: LookupItemService,
    private snotifyService: SnotifyService,
    private notificationService: NotificationService, private visitDetailsService: VisitDetailsService) {

  }

  public ngOnChanges(changes: SimpleChanges) {    
    if (changes['dateLMP']) { console.log(changes['dateLMP'].currentValue);
    }
 }

  ngOnInit() {
    this.visitDetailsFormGroup = this.fb.group({
        visitDate: ['', Validators.required],
        ancVisitType: ['', Validators.required],
        dateLMP: ['', Validators.required],
        dateEDD: ['', Validators.required],
        ancVisitNumber: ['', Validators.required],
        gestation: ['', Validators.required],
        ageAtMenarche: ['', Validators.required],
        parityOne: ['', Validators.required],
        parityTwo: ['', Validators.required],
        gravidae: ['', Validators.required]
    });
      this.personId = JSON.parse(localStorage.getItem('personId'));
      this.patientId = (JSON.parse(localStorage.getItem('patientId'))) ? JSON.parse(localStorage.getItem('patientId')) : 0;
     // this.linkage.userId = JSON.parse(localStorage.getItem('appUserId'));
    this.getANCVisits('ANCVisitType');
      this.visitDetailsFormGroup.controls['gravidae'].disable({ onlySelf: true });
    
    // getANCProfile
     this. getAncInitialProfileVisitDetails(this.patientId);

  }

    public getANCVisits(groupName: string) {
        this.lookupItemView$ = this._lookupItemService.getByGroupName(groupName)
            .subscribe(
                p => {
                    const options = p['lookupItems'];
                    for (let i = 0; i < options.length; i++) {
                        // console.log(options[i]);
                        if (options[i].key == 'lookupItems') {
                            this.entryPoints = options[i].value;
                        }
                    }
                    console.log(options);
                    for (let i = 0; i < options.length; i++) {
                        this.ancVisitTypes.push({ 'itemId': options[i]['itemId'], 'itemName': options[i]['itemName']});
                    }
                    // console.log(options[0]['itemName']);
                    console.log(this.ancVisitTypes);
                },
                (err) => {
                    console.log(err);
                    this.snotifyService.error('Error editing encounter ' + err, 'Encounter', this.notificationService.getConfig());
                },
                () => {
                    console.log(this.lookupItemView$);
                });
    }
    
    public getAncInitialProfileVisitDetails(patientId: number) {
        this.Ancprofile$ = this.visitDetailsService.getAncInitialProfile(patientId)
            .subscribe(
                p => {
                    this.patientProfile = (p) ? p : this.patientProfile;
                },
            (error) => {
                console.log(error);
                this.snotifyService.error('Error editing encounter ' + error, 'Encounter', this.notificationService.getConfig());
                },
            () => {
                    if (!this.patientProfile) {
                        this.visitDetailsFormGroup.controls['ancVisitType'].setValue(this.patientProfile.VisitType);
                        this.visitDetailsFormGroup.controls['ancVisitType'].disable({ onlySelf: true });
                        this.visitDetailsFormGroup.controls['ancVisitNumber'].setValue(this.patientProfile.VisitNumber + 1);
                        this.visitDetailsFormGroup.controls['ancVisitNumber'].disable({ onlySelf: true });
                        this.visitDetailsFormGroup.controls['ageAtMenarche'].setValue(this.patientProfile.AgeMenarche);
                        this.visitDetailsFormGroup.controls['ageAtMenarche'].disable({ onlySelf: true });
                    } else {
                        this.visitDetailsFormGroup.controls['ancVisitType'].setValue(1724);
                        this.visitDetailsFormGroup.controls['ancVisitType'].disable({ onlySelf: true });
                        this.visitDetailsFormGroup.controls['ancVisitNumber'].setValue(1);
                        this.visitDetailsFormGroup.controls['ancVisitNumber'].disable({ onlySelf: true });
                    }
            });
    }

    public getPregnancyProfile(patientId: number) {
        this.pregnancy$ = this.visitDetailsService.getPregnancyProfile(patientId)
            .subscribe(
                p => {
                    this.pregnancyProfile = (p) ? p : this.pregnancyProfile;
                },
                (error)  => {
                    console.log(error);
                    this.snotifyService.error('Error fetching pregnancy ' + error, 'Pregnancy Profile',
                        this.notificationService.getConfig());
               },
                () => {
                    if (!this.pregnancyProfile) {
                        this.visitDetailsFormGroup.controls['dateLMP'].setValue(this.pregnancyProfile.lmp);
                        this.visitDetailsFormGroup.controls['dateEDD'].setValue(this.pregnancyProfile.edd);
                        this.visitDetailsFormGroup.controls['parityOne'].setValue(this.pregnancyProfile.parity);
                        this.visitDetailsFormGroup.controls['parityTwo'].setValue(this.pregnancyProfile.parity);
                        this.visitDetailsFormGroup.controls['gravidae'].setValue(this.pregnancyProfile.gravidae);

                        // disable the fields:
                        this.visitDetailsFormGroup.controls['dateLMP'].disable({ onlySelf: true });
                        this.visitDetailsFormGroup.controls['dateEDD'].disable({ onlySelf: true });
                        this.visitDetailsFormGroup.controls['parityOne'].disable({ onlySelf: true });
                        this.visitDetailsFormGroup.controls['parityTwo'].disable({ onlySelf: true });
                        this.visitDetailsFormGroup.controls['ancVisitType'].disable({ onlySelf: true });
                    }

            });
    }

  public moveNextStep() {
      console.log(this.visitDetailsFormGroup.value);

      this.visitDetails = {
          PatientId: 9,
          ServiceAreaId: 3,
          VisitDate: this.visitDetailsFormGroup.controls['visitDate'].value,
          VisitType: this.visitDetailsFormGroup.controls['ancVisitType'].value,
          VisitNumber: parseInt(this.visitDetailsFormGroup.controls['ancVisitNumber'].value, 10),
          Lmp : this.visitDetailsFormGroup.controls['dateLMP'].value,
          Edd:  this.visitDetailsFormGroup.controls['dateEDD'].value,
          Gestation: parseInt(this.visitDetailsFormGroup.controls['gestation'].value, 10),
          AgeAtMenarche: parseInt(this.visitDetailsFormGroup.controls['ageAtMenarche'].value, 10),
          ParityOne: parseInt(this.visitDetailsFormGroup.controls['parityOne'].value, 10),
          ParityTwo: parseInt( this.visitDetailsFormGroup.controls['parityTwo'].value, 10),
          Gravidae: this.visitDetailsFormGroup.controls['gravidae'].value,
     };
      //  this.nextStep.emit(this.visitDetailsFormGroup.value);
      console.log(this.visitDetails);
      this.nextStep.emit(this.visitDetails);
    }

    public onLMPDateChange() {
        this.dateLMP = this.visitDetailsFormGroup.controls['dateLMP'].value;
      //  this.dateEDD = moment(this.visitDetailsFormGroup.controls['dateLMP'].value, 'DD-MM-YYYY').add(280, 'days');
      //  this.visitDetailsFormGroup.controls['dateEDD'].setValue(moment(this.visitDetailsFormGroup.controls['dateLMP'].value,
     //       'DD-MM-YYYY').add(280, 'days').format(''));

        this.visitDetailsFormGroup.controls['dateEDD'].setValue(moment(this.visitDetailsFormGroup.controls['dateLMP'].value,
            'DD-MM-YYYY').add(280, 'days').format(''));


        const now = moment(new Date());
        const gestation = moment.duration(now.diff( this.dateLMP)).asWeeks().toFixed(1);
        this.visitDetailsFormGroup.controls['gestation'].setValue(gestation);

        this.visitDetailsFormGroup.controls['dateEDD'].disable({ onlySelf: true });
        console.log(moment(this.visitDetailsFormGroup.controls['dateLMP'].value, 'DD-MM-YYYY').add(280, 'days'));
    }
    
    public onParityTwoChange() {
        const parityOne: number = this.visitDetailsFormGroup.controls['parityOne'].value;
        const parityTwo: number = this.visitDetailsFormGroup.controls['parityTwo'].value;
        const gravidae: number = parseInt(parityOne.toString(), 10 ) + parseInt(String(parityTwo), 10);
        this.visitDetailsFormGroup.controls['gravidae'].setValue(gravidae + 1);
        this.visitDetailsFormGroup.controls['gravidae'].disable({ onlySelf: true });

    }

}

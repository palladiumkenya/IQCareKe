import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {Subscription} from 'rxjs';
import {SnotifyService} from 'ng-snotify';
import {PatientEducationEmitter} from '../../emitters/PatientEducationEmitter';
import {CounsellingTopicsEmitters} from '../../emitters/counsellingTopicsEmitters';
import {PatientEducationCommand} from '../../_models/PatientEducationCommand';
import {LookupItemService} from '../../../shared/_services/lookup-item.service';
import {NotificationService} from '../../../shared/_services/notification.service';


export interface PeriodicElement {
    topicId: number;
    topic: string;
    onSetDate: string;
}

const ELEMENT_DATA: PeriodicElement[] = [
    {topicId: 1, topic: 'sex', onSetDate: 'Hydrogen'},
    {topicId: 2, topic: 'church', onSetDate: 'Helium'}
];


@Component({
    selector: 'app-patient-education-examination',
    templateUrl: './patient-education-examination.component.html',
    styleUrls: ['./patient-education-examination.component.css']
})


export class PatientEducationExaminationComponent implements OnInit {
    PatientEducationFormGroup: FormGroup;

    public yesnos: any[] = [];
    lookupItemView$: Subscription;
    LookupItems$: Subscription;
    public testResults: any[] = [];
    public userId: number;

    public patientEducationEmitterData: PatientEducationEmitter;
    public counsellingOptions: any[] = [];
    public yesNoOptions: any[] = [];
    public hivStatusOptions: any[] = [];

    public counselling_data: CounsellingTopicsEmitters[] = [];
    @Output() nextStep = new EventEmitter<PatientEducationEmitter>();
    @Output() notify: EventEmitter<object> = new EventEmitter<object>();
    @Input() patientEducationData: PatientEducationCommand;
    @Input() patientEducationFormOptions: any[] = [];

    displayedColumns: string[] = ['topicId', 'topic', 'onSetDate'];
    dataSource = ELEMENT_DATA;

    constructor(private _formBuilder: FormBuilder, private _lookupItemService: LookupItemService,
                private  snotifyService: SnotifyService,
                private notificationService: NotificationService) {
    }

    ngOnInit() {
        this.PatientEducationFormGroup = this._formBuilder.group({
            breastExamDone: new FormControl(['', Validators.required]),
            counsellingDate: new FormControl(['', Validators.required]),
            counselledOn: new FormControl(['', Validators.required]),
            treatedSyphilis: new FormControl(['', Validators.required]),
            testResult: new FormControl(['', Validators.required])
        });
        this.userId = JSON.parse(localStorage.getItem('appUserId'));
        //  this.getLookupOptions('counselledOn', this.topics);
        //  this.getLookupOptions('yesno', this.yesnos);
        //  this.getLookupOptions('HivTestingResult', this.testResults);

        const {
            yesnoOptions,
            patientEducationOptions,
            hivStatusOptions
        } = this.patientEducationFormOptions[0];
        this.yesNoOptions = yesnoOptions;
        this.counsellingOptions = patientEducationOptions;
        this.hivStatusOptions = hivStatusOptions;


        this.nextStep.emit(this.patientEducationEmitterData);
        this.notify.emit({ 'form': this.PatientEducationFormGroup, 'counselling_data': this.counselling_data});
    }

    /*  public getLookupOptions(groupName: string, masterName: any[]) {
          this.LookupItems$ = this._lookupItemService.getByGroupName(groupName)
              .subscribe(
                  p => {
                      const lookupOptions = p['lookupItems'];
                      for (let i = 0; i < lookupOptions.length; i++) {
                          masterName.push({'itemId': lookupOptions[i]['itemId'], 'itemName': lookupOptions[i]['itemName']});
                      }
                  },
                  (err) => {
                      console.log(err);
                      this.snotifyService.error('Error fetching lookups' + err, 'Encounter', this.notificationService.getConfig());
                  },
                  () => {
                      console.log(this.lookupItemView$);
                  });
      }*/

    public moveNextStep() {
        console.log(this.PatientEducationFormGroup.value);

        this.patientEducationEmitterData = {
            breastExamDone: parseInt(this.PatientEducationFormGroup.get('breastExamDone').value, 10),
            treatedSyphilis: parseInt(this.PatientEducationFormGroup.get('treatedSyphilis').value, 10),
            counsellingTopics: this.counselling_data
        };
            console.log('breastexamDone' + this.patientEducationEmitterData.breastExamDone + 'from form ' +
                this.PatientEducationFormGroup.get('breastExamDone').value.itemId);
        console.log(this.patientEducationEmitterData);
        this.nextStep.emit(this.patientEducationEmitterData);
    }

    public addTopics() {

        const topic = this.PatientEducationFormGroup.controls['counselledOn'].value.itemName;
        const topicId = this.PatientEducationFormGroup.controls['counselledOn'].value.itemId;

        if (this.counselling_data.filter(x => x.counsellingTopic === topic).length > 0) {
            this.snotifyService.warning('' + topic + ' exists', 'Counselling', this.notificationService.getConfig());
        } else {
            this.counselling_data.push({
                counselledOn: parseInt(topicId, 10),
                counsellingTopic: topic,
                counsellingTopicId: topicId,
                description: 'n/a',
                CounsellingDate: this.PatientEducationFormGroup.controls['counsellingDate'].value
            });
        }
    }

    public removeRow(idx) {
        this.counselling_data.splice(idx, 1);
    }
}

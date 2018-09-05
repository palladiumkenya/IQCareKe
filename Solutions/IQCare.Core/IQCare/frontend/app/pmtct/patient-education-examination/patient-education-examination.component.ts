import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {  FormBuilder, FormGroup, Validators  } from '@angular/forms';
import {LookupItemService} from '../../shared/_services/lookup-item.service';
import {Subscription} from 'rxjs';
import {NotificationService} from '../../shared/_services/notification.service';
import { SnotifyService } from 'ng-snotify';
import {PatientEducationCommand} from '../_models/PatientEducationCommand';
import {PatientEducationEmitter} from '../emitters/PatientEducationEmitter';
import {VisitDetails} from '../_models/visitDetails';
import {PatientEducation} from '../_models/PatientEducation';
import {CounsellingTopicsEmitters} from '../emitters/counsellingTopicsEmitters';



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
    public topics: any[] = [];
    public testResults: any[] = [];

    public patientEducationEmitterData: PatientEducationEmitter;

    public counselling_data: CounsellingTopicsEmitters[] = [];
    @Output() nextStep = new EventEmitter <PatientEducationEmitter> ();
    @Input() patientEducationData: PatientEducationCommand;

    displayedColumns: string[] = ['topicId', 'topic', 'onSetDate'];
    dataSource = ELEMENT_DATA;

  constructor(private _formBuilder: FormBuilder, private _lookupItemService: LookupItemService,
              private  snotifyService: SnotifyService,
              private notificationService: NotificationService) { }

  ngOnInit() {
    this.PatientEducationFormGroup = this._formBuilder.group({
        breastExamDone: ['', Validators.required],
        counsellingDate: ['', Validators.required],
        counselledOn: ['', Validators.required],
        treatedSyphilis: ['', Validators.required],
        testResult: ['', Validators.required]
    });
     this.getLookupOptions('counselledOn', this.topics);
     this.getLookupOptions('yesno', this.yesnos);
      this.getLookupOptions('HivTestingResult', this.testResults);

      console.log(this.counselling_data + ' hu');
  }

    public  getLookupOptions(groupName: string, masterName: any[]) {
      this.LookupItems$ = this._lookupItemService.getByGroupName(groupName)
          .subscribe(
              p => {
                  const lookupOptions =  p['lookupItems'];
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
    }

    public moveNextStep() {
        console.log(this.PatientEducationFormGroup.value);

        this.patientEducationEmitterData = {
            breastExamDone : parseInt(this.PatientEducationFormGroup.controls['breastExamDone'].value, 10),
            treatedSyphilis: parseInt(this.PatientEducationFormGroup.controls['treatedSyphilis'].value, 10 ),
            counsellingTopics: this.counselling_data
        };

        console.log(this.patientEducationEmitterData);
        this.nextStep.emit(this.patientEducationEmitterData);
    }

    public addTopics() {

        const topic = this.PatientEducationFormGroup.controls['counselledOn'].value.itemName;
        const topicId = this.PatientEducationFormGroup.controls['counselledOn'].value.itemId;

        console.log(this.counselling_data + ' hu');

        if (this.counselling_data.filter(x => x.counsellingTopic === topic ).length > 0) {
            this.snotifyService.warning('' + topic + ' exists', 'Counselling', this.notificationService.getConfig());
        } else {
            this.counselling_data.push({
                counselledOn: parseInt(topicId, 10 ),
                counsellingTopic: topic,
                topicDate: this.PatientEducationFormGroup.controls['counsellingDate'].value});
        }
        console.log(this.counselling_data);
    }

    public  removeRow(idx) {
      this.counselling_data.splice(idx, 1);
    }
}

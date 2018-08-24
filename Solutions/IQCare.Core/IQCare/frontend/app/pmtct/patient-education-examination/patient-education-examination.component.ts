import { Component, OnInit } from '@angular/core';
import {  FormBuilder, FormGroup, Validators  } from '@angular/forms';
import {LookupItemService} from '../../shared/_services/lookup-item.service';
import {Subscription} from 'rxjs';
import {NotificationService} from '../../shared/_services/notification.service';
import { SnotifyService } from 'ng-snotify';

export interface Topic {
  value: string;
  viewValue: string;
}

export interface PatientEducation {
    position: number;
    dateDone: string;
    topic: string;
}

const PatientEducation_Data: PatientEducation[] = [
    {position: 1, dateDone: '11/11/2017', topic: 'Birth plans'}
] ;

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


  displayedColumns: string[] = ['position', 'dateDone', 'topic'];
    dataSource = PatientEducation_Data;

  constructor(private _formBuilder: FormBuilder, private _lookupItemService: LookupItemService,
              private  snotifyService: SnotifyService,
              private notificationService: NotificationService) { }

  ngOnInit() {
    this.PatientEducationFormGroup = this._formBuilder.group({
        breastExamDone: ['', Validators.required],
        counsellingDate: ['', Validators.required],
        counselledOn: ['', Validators.required],
        topicDate: ['', Validators.required]
    });
     this.getLookupOptions('counselledOn', this.topics);
     this.getLookupOptions('yesno', this.yesnos);
  }

   /* public getCounsellingTopics(groupName: string) {
        this.lookupItemView$ = this._lookupItemService.getByGroupName(groupName)
            .subscribe(
                p => {
                    const options = p['lookupItems'];

                    for(let i=0; i<options.length; i++){
                        this.topics.push({"itemId":options[i]['itemId'],"itemName": options[i]['itemName']});
                    }
                },
                (err) => {
                    console.log(err);
                    this.snotifyService.error('Error editing encounter ' + err, 'Encounter', this.notificationService.getConfig());
                },
                () => {
                    console.log(this.lookupItemView$);
                });
    }*/

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


}

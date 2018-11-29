import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {  FormBuilder, FormGroup, Validators  } from '@angular/forms';
import {LookupItemService} from '../../../shared/_services/lookup-item.service';
import {NotificationService} from '../../../shared/_services/notification.service';
import {Subscription} from 'rxjs';
import { SnotifyService } from 'ng-snotify';
import {PatientEducationCommand} from '../../_models/PatientEducationCommand';


export interface AntenatalProfile {
    testName: string;
    dateDone: string;
    results: string;
    position: number;
}

const AntenatalProfile_Data: AntenatalProfile[] = [
  //  {position: 1, testName: 'Blood Group', dateDone: '11/11/2017', results: 'O+'}
] ;

@Component({
  selector: 'app-antenatal-profile',
  templateUrl: './antenatal-profile.component.html',
  styleUrls: ['./antenatal-profile.component.css']
})
export class AntenatalProfileComponent implements OnInit {

    AntenatalProfileFormGroup: FormGroup;
    displayedColumns: string[] = ['position', 'testName', 'dateDone', 'results'];
    dataSource = AntenatalProfile_Data;
    public lookupItemView$: Subscription;
    public yesnos: any[] = [];
  //  @Output() nextStep = new EventEmitter <AntenatalProfileEmmiter> ();
    @Input() patientEducationData: PatientEducationCommand;

  constructor(private _formBuilder: FormBuilder, private _lookupItemService: LookupItemService,
              private notificationService: NotificationService, private snotifyService: SnotifyService) { }

  ngOnInit() {
    this.AntenatalProfileFormGroup = this._formBuilder.group({
      //  treatedSyphilis: ['', Validators.required]
    });
    this.getLookupOptions('yesno', this.yesnos);
  }

    public  getLookupOptions(groupName: string, masterName: any[]) {
        this.lookupItemView$ = this._lookupItemService.getByGroupName(groupName)
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

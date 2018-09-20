import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {LookupItemService} from '../../shared/_services/lookup-item.service';
import {SnotifyService} from 'ng-snotify';
import {NotificationService} from '../../shared/_services/notification.service';
import {Subscription} from 'rxjs/index';
import {ClientMonitoringEmitter} from '../emitters/ClientMonitoringEmitter';
import {PreventiveEmitter} from '../emitters/PreventiveEmitter';
import {PreventiveServiceEmitter} from '../emitters/PreventiveServiceEmitter';
export interface Options {
  value: string;
  viewValue: string;
}

@Component({
  selector: 'app-preventive-services',
  templateUrl: './preventive-services.component.html',
  styleUrls: ['./preventive-services.component.css']
})
export class PreventiveServicesComponent implements OnInit {
  public PreventiveServicesFormGroup: FormGroup;
    lookupItemView$: Subscription;
    services: any[] = [];
    public yesnos: any[] = [];
    public YesNoNas: any[] = [];
    public FinalResults: any[] = [];
    @Output() nextStep = new EventEmitter <PreventiveServiceEmitter> ();
    @Input() preventiveServices: PreventiveServiceEmitter;
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();
    public preventiveServicesData: PreventiveServiceEmitter;
    public serviceData: PreventiveEmitter[] = [];


  constructor(private _formBuilder: FormBuilder, private _lookupItemService: LookupItemService,
              private  snotifyService: SnotifyService,
              private notificationService: NotificationService) { }

  ngOnInit() {
    this.PreventiveServicesFormGroup = this._formBuilder.group({
        preventiveServices: ['', Validators.required],
        dateGiven: ['', Validators.required],
        comments: ['', Validators.required],
        nextSchedule: ['', Validators.required],
        insecticideTreatedNet: ['', Validators.required],
        insecticideTreatedNetGivenDate: ['', Validators.required],
        antenatalExercise: ['', Validators.required],
        insecticideGivenDate: ['', Validators.required],
        PartnerTestingVisit: ['', Validators.required],
        finalHIVResult: ['', Validators.required]
    });
    this.getLookupItems('PreventiveService', this.services);
     this.getLookupItems('YesNo', this.yesnos);
      this.getLookupItems('HIVFinalResultsPMTCT', this.FinalResults);
      this.getLookupItems('YesNoNa', this.YesNoNas);
  }

    public getLookupItems(groupName: string , _options: any[]) {
        this.lookupItemView$ = this._lookupItemService.getByGroupName(groupName)
            .subscribe(
                p => {
                    const options = p['lookupItems'];
                    console.log(options);
                    for (let i = 0; i < options.length; i++) {
                        _options.push({ 'itemId': options[i]['itemId'], 'itemName': options[i]['itemName']});
                    }
                },
                (err) => {
                    console.log(err);
                    this.snotifyService.error('Error editing encounter ' + err, 'Encounter', this.notificationService.getConfig());
                },
                () => {
                    console.log(this.lookupItemView$);
                });
    }

    public moveNextStep() {
        console.log(this.PreventiveServicesFormGroup.value);

        this.preventiveServicesData = {
            preventiveService: this.serviceData,
            insecticideTreatedNet: parseInt(this.PreventiveServicesFormGroup.controls['insecticideTreatedNet'].value, 10),
            insecticideTreatedNetGivenDate: this.PreventiveServicesFormGroup.controls['insecticideTreatedNetGivenDate'].value,
            antenatalExercise: parseInt(this.PreventiveServicesFormGroup.controls['antenatalExercise'].value, 10 ),
          //  insecticideGivenDate: this.PreventiveServicesFormGroup.controls['insecticideGivenDate'].value,
            PartnerTestingVisit: parseInt(this.PreventiveServicesFormGroup.controls['PartnerTestingVisit'].value, 10 ),
            finalHIVResult: parseInt(this.PreventiveServicesFormGroup.controls['finalHIVResult'].value, 10 ),

        };
        console.log(this.preventiveServicesData);
        this.nextStep.emit(this.preventiveServicesData);
        this.notify.emit(this.PreventiveServicesFormGroup);
    }

    public addTopics() {

        const service = this.PreventiveServicesFormGroup.controls['preventiveServices'].value.itemName;
        const serviceId = this.PreventiveServicesFormGroup.controls['preventiveServices'].value.itemId;


        if (this.serviceData.filter(x => x.preventiveService === service ).length > 0) {
            this.snotifyService.warning('' + service + ' exists', 'preventive Service', this.notificationService.getConfig());
        } else {
            this.serviceData.push({
                preventiveService: service,
                preventiveServiceId: serviceId,
                dateGiven: this.PreventiveServicesFormGroup.controls['dateGiven'].value,
                comments: this.PreventiveServicesFormGroup.controls['comments'].value,
                nextSchedule: this.PreventiveServicesFormGroup.controls['nextSchedule'].value});
        }
        console.log(this.serviceData);
    }

    public  removeRow(idx) {
        this.serviceData.splice(idx, 1);
    }
}

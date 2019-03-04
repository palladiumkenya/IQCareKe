import { MaternityService } from './../../_services/maternity.service';
import { Component, OnInit, EventEmitter, Output, Input, AfterViewInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { LookupItemView } from '../../../shared/_models/LookupItemView';
import { NotificationService } from '../../../shared/_services/notification.service';
import { SnotifyService } from 'ng-snotify';
import { LookupItemService } from '../../../shared/_services/lookup-item.service';

@Component({
    selector: 'app-pnc-patienteducation',
    templateUrl: './pnc-patienteducation.component.html',
    styleUrls: ['./pnc-patienteducation.component.css']
})
export class PncPatienteducationComponent implements OnInit, AfterViewInit {
    PatientEducationForm: FormGroup;
    yesnoOptions: LookupItemView[] = [];
    infantFeedingTopicId: number;

    @Input('patientEducationOptions') patientEducationOptions: any[] = [];
    @Input('isEdit') isEdit: boolean;
    @Input('patientId') patientId: number;
    @Input('patientMasterVisitId') patientMasterVisitId: number;

    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    constructor(private _formBuilder: FormBuilder,
        private maternityService: MaternityService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private lookupitemservice: LookupItemService) { }

    ngOnInit() {
        this.PatientEducationForm = this._formBuilder.group({
            counselledInfantFeeding: new FormControl('', [Validators.required]),
            'id': new FormControl('')
        });
          
       console.log(this.patientEducationOptions.length +' patientEducationOptions') 
        const { yesnoOptions, infantFeedingTopicId } = this.patientEducationOptions[0];
        this.yesnoOptions = yesnoOptions;
        this.infantFeedingTopicId = infantFeedingTopicId;

        this.notify.emit(this.PatientEducationForm);
    }

    ngAfterViewInit(): void {
        if (this.isEdit) {
            this.loadPncPatientEducation();
        }
    }

    loadPncPatientEducation(): void {
        let isCounsellingDone = false;
        this.maternityService.getPatientEducation(this.patientId, this.patientMasterVisitId).subscribe(
            (result) => {
                console.log(result);
                for (let i = 0; i < result.length; i++) {
                    if (result[i].counsellingTopicId == this.infantFeedingTopicId) {
                        isCounsellingDone = true;
                    }
                    this.PatientEducationForm.get('id').setValue(result[i].id);
                }

                if (isCounsellingDone) {
                    const yesOption = this.yesnoOptions.filter(obj => obj.itemName == 'Yes');
                    this.PatientEducationForm.get('counselledInfantFeeding').setValue(yesOption[0].itemId);
                } else {
                    const noOption = this.yesnoOptions.filter(obj => obj.itemName == 'No');
                    this.PatientEducationForm.get('counselledInfantFeeding').setValue(noOption[0].itemId);
                }
                console.log(isCounsellingDone +'  isCounsellingDone')
            },
            (error) => {
                this.snotifyService.error('Fetching patient education ' + error, 'PNC Encounter',
                    this.notificationService.getConfig());
            }
        );
    }
}

import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { NotificationService } from '../../../shared/_services/notification.service';
import { SnotifyService } from 'ng-snotify';
import { LookupItemService } from '../../../shared/_services/lookup-item.service';
import { MaternityService } from '../../_services/maternity.service';

@Component({
    selector: 'app-diagnosis',
    templateUrl: './diagnosis.component.html',
    styleUrls: ['./diagnosis.component.css']
})
export class DiagnosisComponent implements OnInit {
    PatientdiagnosisFormGroup: FormGroup;
    @Input() diagnosisOptions: any[] = [];
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();
    @Input('isEdit') isEdit: boolean;
    @Input('PatientId') PatientId: number;
    @Input('PatientMasterVisitId') PatientMasterVisitId: number;

    constructor(private _formBuilder: FormBuilder,
        private _lookupItemService: LookupItemService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private maternityService : MaternityService) {
    }

    ngOnInit() {
        this.PatientdiagnosisFormGroup = this._formBuilder.group({
            diagnosis: new FormControl('', [Validators.required])
        });

        this.notify.emit(this.PatientdiagnosisFormGroup);
         console.log("Master Visit Id " + this.PatientMasterVisitId);
        if(this.isEdit)
         {
             this.getPatientDiagnosisInfo(this.PatientMasterVisitId);
         }

    }

    public getPatientDiagnosisInfo(masterVisitId: number): void {
          this.maternityService.GetPatientDiagnosisInfo(masterVisitId)
            .subscribe(
                diag => {
                    if (diag != null) {
                        this.PatientdiagnosisFormGroup.controls['diagnosis'].setValue(diag.diagnosis);

                    }
                },
                (err) => {
                    this.snotifyService.error('Error fetching patient diagnosis details' + err,
                        'Encounter', this.notificationService.getConfig());
                },
                () => {

                });
    }

}

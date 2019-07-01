import { PrepService } from './../../_services/prep.service';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { LookupItemView } from '../../../shared/_models/LookupItemView';

@Component({
    selector: 'app-prep-status',
    templateUrl: './prep-status.component.html',
    styleUrls: ['./prep-status.component.css'],
    providers: [PrepService]
})
export class PrepStatusComponent implements OnInit {
    PrepStatusForm: FormGroup;
    yesnoOptions: LookupItemView[] = [];
    prepStatusOptions: LookupItemView[] = [];
    prepContraindicationsOptions: LookupItemView[] = [];

    @Input() PrepStatusOptions: any;
    @Input() patientId: number;
    @Input() personId: number;
    @Input() patientEncounterId: number;
    @Input() isEdit: number;
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    constructor(private _formBuilder: FormBuilder,
        private prepservice: PrepService) { }

    ngOnInit() {
        this.PrepStatusForm = this._formBuilder.group({
            signsOrSymptomsHIV: new FormControl('', [Validators.required]),
            contraindications_PrEP_Present: new FormControl('', [Validators.required]),
            adherenceCounselling: new FormControl('', [Validators.required]),
            PrEPStatusToday: new FormControl('', [Validators.required]),
            condomsIssued: new FormControl('', [Validators.required]),
            noCondomsIssued: new FormControl('', [Validators.required]),
        });

        // Set initial form state
        this.PrepStatusForm.controls.noCondomsIssued.disable({ onlySelf: true });

        // emit form to the stepper 
        this.notify.emit(this.PrepStatusForm);

        const { yesnoOptions, prepStatusOptions, prepContraindicationsOptions } = this.PrepStatusOptions[0];
        this.yesnoOptions = yesnoOptions;
        this.prepStatusOptions = prepStatusOptions;
        this.prepContraindicationsOptions = prepContraindicationsOptions;

        if (this.isEdit == 1) {
            this.loadPrepStatus();
        }
    }

    loadPrepStatus(): void {
        this.prepservice.getPrepStatus(this.patientId, this.patientEncounterId).subscribe(
            (res) => {
                if (res.length > 0) {
                    this.PrepStatusForm.controls.signsOrSymptomsHIV.setValue(res[0].signsOrSymptomsHIV);
                    this.PrepStatusForm.controls.contraindications_PrEP_Present.setValue(res[0].contraindicationsPrepPresent);
                    this.PrepStatusForm.controls.adherenceCounselling.setValue(res[0].adherenceCounsellingDone);
                    this.PrepStatusForm.controls.PrEPStatusToday.setValue(res[0].prepStatusToday);
                    this.PrepStatusForm.controls.condomsIssued.setValue(res[0].condomsIssued);
                    this.PrepStatusForm.controls.noCondomsIssued.setValue(res[0].noOfCondoms);
                }
            },
            (error) => {
                console.log(error);
            }
        );
    }

    onCondomsIssuedSelection(event) {
        // disable referral to VMMC when client is already circumcised
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Yes') {
            this.PrepStatusForm.controls.noCondomsIssued.enable({ onlySelf: true });
        } else if (event.isUserInput && event.source.selected && event.source.viewValue == 'No') {
            this.PrepStatusForm.controls.noCondomsIssued.disable({ onlySelf: true });
            this.PrepStatusForm.controls.noCondomsIssued.setValue('');
        }
    }

}

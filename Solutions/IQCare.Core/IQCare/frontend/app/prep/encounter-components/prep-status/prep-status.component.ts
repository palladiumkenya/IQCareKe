import { PrepService } from './../../_services/prep.service';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { LookupItemView } from '../../../shared/_models/LookupItemView';
import { SearchService } from '../../../registration/_services/search.service';

@Component({
    selector: 'app-prep-status',
    templateUrl: './prep-status.component.html',
    styleUrls: ['./prep-status.component.css'],
    providers: [PrepService, SearchService]
})
export class PrepStatusComponent implements OnInit {
    PrepStatusForm: FormGroup;
    yesnoOptions: LookupItemView[] = [];
    prepStatusOptions: LookupItemView[] = [];
    prepContraindicationsOptions: LookupItemView[] = [];

    @Input() PrepStatusOptions: any;
    @Input() patientId: number;
    @Input() personId: number;
    @Input() patientMasterVisitId: number;
    @Input() patientEncounterId: number;
    @Input() isEdit: number;
    @Input() Age: number;
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();
    userId: number;

    constructor(private _formBuilder: FormBuilder,
        private searchService: SearchService,
        private prepservice: PrepService) { }

    ngOnInit() {
        this.PrepStatusForm = this._formBuilder.group({
            signsOrSymptomsHIV: new FormControl('', [Validators.required]),
            contraindications_PrEP_Present: new FormControl('', [Validators.required]),
            adherenceCounselling: new FormControl('', [Validators.required]),
            PrEPStatusToday: new FormControl('', [Validators.required]),
            condomsIssued: new FormControl('', [Validators.required]),
            noCondomsIssued: new FormControl('', [Validators.required]),
            id: new FormControl()
        });

        this.userId = JSON.parse(localStorage.getItem('appUserId'));
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
            this.loadcontraIndications();
        }
    }

    loadcontraIndications(): void {
        this.prepservice.getStiScreeningTreatment(this.patientId, this.patientMasterVisitId).subscribe(
            (res) => {
                if (res.length > 0) {
                    const contraindications = [];
                    res.forEach(element => {
                        if (element.screeningTypeName == 'ContraindicationsPrEP') {
                            contraindications.push(element.screeningValueId);
                        }
                    });

                    this.PrepStatusForm.controls.contraindications_PrEP_Present.setValue(contraindications);

                }
            },
            (error) => {
                console.log(error);
            }
        );
    }
    Oncontraindications(event) {
        const value = event.source.value;


        if (event.source.viewValue !== 'None' && event.source.selected == true) {
            for (let i = 0; i < event.source._parent.options.length; i++) {
                if (event.source._parent.options._results[i].viewValue
                    === 'None') {
                    event.source._parent.options._results[i].deselect();
                }
            }
        }

        if (event.source.viewValue === 'None' && event.source.selected == true) {
            for (let i = 0; i < event.source._parent.options.length; i++) {
                if (event.source._parent.options._results[i].viewValue
                    !== event.source.viewValue) {
                    event.source._parent.options._results[i].deselect();
                }
            }
        }
    }
    loadPrepStatus(): void {
        this.prepservice.getPrepStatus(this.patientId, this.patientEncounterId).subscribe(
            (res) => {
                // console.log(res);
                if (res.length > 0) {
                    this.PrepStatusForm.controls.signsOrSymptomsHIV.setValue(res[0].signsOrSymptomsHIV);
                    //  this.PrepStatusForm.controls.contraindications_PrEP_Present.setValue(res[0].contraindicationsPrepPresent);
                    this.PrepStatusForm.controls.adherenceCounselling.setValue(res[0].adherenceCounsellingDone);
                    this.PrepStatusForm.controls.PrEPStatusToday.setValue(res[0].prepStatusToday);
                    this.PrepStatusForm.controls.condomsIssued.setValue(res[0].condomsIssued);
                    this.PrepStatusForm.controls.noCondomsIssued.setValue(res[0].noOfCondoms);
                    this.PrepStatusForm.controls.id.setValue(res[0].id);
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

    onPharmacyClick() {
        this.searchService.setSession(this.personId, this.patientId, this.userId).subscribe((sessionres) => {
            this.searchService.setVisitSession(this.patientMasterVisitId, this.Age, 261).subscribe((setVisitSession) => {
                const url = location.protocol + '//' + window.location.hostname + ':' + window.location.port +
                    '/IQCare/CCC/Encounter/PharmacyPrescription.aspx';
                const win = window.open(url, '_blank');
                win.focus();
            });
        });
    }

}

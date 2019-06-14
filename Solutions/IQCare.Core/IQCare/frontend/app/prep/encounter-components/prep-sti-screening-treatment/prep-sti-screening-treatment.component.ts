import { LookupItemView } from './../../../shared/_models/LookupItemView';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import * as moment from 'moment';
import { SearchService } from '../../../registration/_services/search.service';

@Component({
    selector: 'app-prep-sti-screening-treatment',
    templateUrl: './prep-sti-screening-treatment.component.html',
    styleUrls: ['./prep-sti-screening-treatment.component.css'],
    providers: [SearchService]
})
export class PrepSTIScreeningTreatmentComponent implements OnInit {
    STIScreeningForm: FormGroup;
    yesnoOptions: LookupItemView[] = [];
    stiScreeningOptions: LookupItemView[] = [];

    maxDate: Date;

    @Input() STIScreeningAndTreatmentOptions: any;
    @Input() patientId: number;
    @Input() personId: number;
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    constructor(private _formBuilder: FormBuilder,
        private searchService: SearchService) {
        this.maxDate = new Date();
    }

    ngOnInit() {
        this.STIScreeningForm = this._formBuilder.group({
            visitDate: new FormControl('', [Validators.required]),
            signsOrSymptomsOfSTI: new FormControl('', [Validators.required]),
            signsOfSTI: new FormControl('', [Validators.required]),
            stiTreatmentOffered: new FormControl(''),
            stiReferredLabInvestigation: new FormControl('')
        });
        this.STIScreeningForm.controls.visitDate.setValue(new Date(localStorage.getItem('visitDate')));

        // emit form to the stepper 
        this.notify.emit(this.STIScreeningForm);

        this.STIScreeningForm.controls.signsOfSTI.disable({ onlySelf: true });

        const { yesnoOptions, stiScreeningOptions } = this.STIScreeningAndTreatmentOptions[0];
        this.yesnoOptions = yesnoOptions;
        this.stiScreeningOptions = stiScreeningOptions;
    }

    public onSignsOrSymptomsSelection(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Yes') {
            this.STIScreeningForm.controls.signsOfSTI.enable({ onlySelf: true });
        } else if (event.isUserInput && event.source.selected && event.source.viewValue == 'No') {
            this.STIScreeningForm.controls.signsOfSTI.setValue([]);
            this.STIScreeningForm.controls.signsOfSTI.disable({ onlySelf: true });
        }
    }

    onReferLabSelection(event) {

    }

    onSTITreatmentelection(event) {

    }

    onPharmacyClick() {
        this.searchService.setSession(this.personId, this.patientId).subscribe((sessionres) => {
            const url = location.protocol + '//' + window.location.hostname + ':' + window.location.port +
                '/IQCare/CCC/Patient/PatientHome.aspx';
            const win = window.open(url, '_blank');
            win.focus();
        });
    }
}

import { PrepService } from './../../_services/prep.service';
import { LookupItemView } from './../../../shared/_models/LookupItemView';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import * as moment from 'moment';
import { SearchService } from '../../../registration/_services/search.service';
import { EncounterService } from '../../../shared/_services/encounter.service';

@Component({
    selector: 'app-prep-sti-screening-treatment',
    templateUrl: './prep-sti-screening-treatment.component.html',
    styleUrls: ['./prep-sti-screening-treatment.component.css'],
    providers: [SearchService, EncounterService]
})
export class PrepSTIScreeningTreatmentComponent implements OnInit {
    STIScreeningForm: FormGroup;
    yesnoOptions: LookupItemView[] = [];
    stiScreeningOptions: LookupItemView[] = [];
    screenedForSTIOptions: LookupItemView[] = [];

    maxDate: Date;
    hasLabBeenOrdered: boolean = false;
    hasPharmacyBeenOrdered: boolean = false;

    @Input() STIScreeningAndTreatmentOptions: any;
    @Input() patientId: number;
    @Input() personId: number;
    @Input() patientMasterVisitId: number;
    @Input() isEdit: number;
    @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

    constructor(private _formBuilder: FormBuilder,
        private searchService: SearchService,
        private prepService: PrepService,
        private encounterService: EncounterService) {
        this.maxDate = new Date();
    }

    ngOnInit() {
        this.STIScreeningForm = this._formBuilder.group({
            visitDate: new FormControl('', [Validators.required]),
            Specify: new FormControl(''),
            signsOrSymptomsOfSTI: new FormControl('', [Validators.required]),
            signsOfSTI: new FormControl('', [Validators.required]),
            stiTreatmentOffered: new FormControl(''),
            stiReferredLabInvestigation: new FormControl('')
        });
        this.STIScreeningForm.controls.Specify.disable({ onlySelf: true })
        // set the date for only new encounters
        if (!this.isEdit) {
            this.STIScreeningForm.controls.visitDate.setValue(new Date(localStorage.getItem('visitDate')));
        }

        // emit form to the stepper 
        this.notify.emit(this.STIScreeningForm);

        this.STIScreeningForm.controls.signsOfSTI.disable({ onlySelf: true });

        const { yesnoOptions, stiScreeningOptions, screenedForSTIOptions } = this.STIScreeningAndTreatmentOptions[0];
        this.yesnoOptions = yesnoOptions;
        this.stiScreeningOptions = stiScreeningOptions;
        this.screenedForSTIOptions = screenedForSTIOptions;

        if (this.isEdit == 1) {
            this.loadSTIScreening();
            this.loadPatientMasterVisit();
        }
    }

    OnSTISelection(event) {
        const value = event.source.value;
        const othersItem = this.stiScreeningOptions.filter(obj => obj.itemId == value);
        if (othersItem[0].itemDisplayName == 'Others (O)' && event.source.selected == true) {
            this.STIScreeningForm.controls.Specify.enable({ onlySelf: true });
        }
        if (othersItem[0].itemDisplayName == 'Others (O)' && event.source.selected == false) {
           
            this.STIScreeningForm.controls.Specify.setValue('');
            this.STIScreeningForm.controls.Specify.disable({ onlySelf: true });
        }
        

    }

    loadPatientMasterVisit() {
        this.encounterService.getPatientMasterVisit(this.patientId, this.patientMasterVisitId).subscribe(
            (res) => {
                if (res.length > 0) {
                    this.STIScreeningForm.controls.visitDate.setValue(res[0].visitDate);
                }
            },
            (error) => {
                console.log(error);
            }
        );
    }

    loadSTIScreening(): void {
        this.prepService.getStiScreeningTreatment(this.patientId, this.patientMasterVisitId).subscribe(
            (res) => {
                let STISymptoms = [];
                const stiScreeningObject = this.screenedForSTIOptions.filter(obj => obj.itemName == 'STIScreeningDone');
                const stiSignsAndSymptomsObject = this.screenedForSTIOptions.filter(obj => obj.itemName == 'STISymptoms');
                const stiLabInvestigationDoneObject = this.screenedForSTIOptions.filter(obj => obj.itemName == 'STILabInvestigationDone');
                const stiTreatmentOfferedObject = this.screenedForSTIOptions.filter(obj => obj.itemName == 'STITreatmentOffered');
                const othersItem = this.stiScreeningOptions.filter(obj => obj.itemName == 'Others (O)');

                if (res.length > 0) {
                    console.log('STIScreening');
                    console.log(res);
                    res.forEach(element => {
                        if (element.screeningTypeName == 'ScreenedForSTI' && element.screeningCategoryId == stiScreeningObject[0].itemId) {
                            this.STIScreeningForm.controls.signsOrSymptomsOfSTI.setValue(element.screeningValueId);
                        } else if (element.screeningTypeName == 'ScreenedForSTI'
                            && element.screeningCategoryId == stiSignsAndSymptomsObject[0].itemId) {
                            STISymptoms.push(element.screeningValueId);
                            if (element.screeningValueId == othersItem[0].itemId) {
                                this.STIScreeningForm.controls.Specify.setValue(element.comment);
                            }

                        } else if (element.screeningTypeName == 'ScreenedForSTI'
                            && element.screeningCategoryId == stiLabInvestigationDoneObject[0].itemId) {
                            this.STIScreeningForm.controls.stiReferredLabInvestigation.setValue(element.screeningValueId);
                        } else if (element.screeningTypeName == 'ScreenedForSTI'
                            && element.screeningCategoryId == stiTreatmentOfferedObject[0].itemId) {
                            this.STIScreeningForm.controls.stiTreatmentOffered.setValue(element.screeningValueId);
                        }
                    });

                    this.STIScreeningForm.controls.signsOfSTI.setValue(STISymptoms);
                }
            },
            (error) => {
                console.log(error);
            }
        );
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
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Yes') {
            this.hasLabBeenOrdered = true;
        } else if (event.isUserInput && event.source.selected && event.source.viewValue == 'No') {
            this.hasLabBeenOrdered = false;
        }
    }

    onSTITreatmentelection(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Yes') {
            this.hasPharmacyBeenOrdered = true;
        } else if (event.isUserInput && event.source.selected && event.source.viewValue == 'No') {
            this.hasPharmacyBeenOrdered = false;
        }
    }

    onPharmacyClick() {
        this.searchService.setSession(this.personId, this.patientId).subscribe((sessionres) => {
            this.searchService.setVisitSession(this.patientMasterVisitId, 20).subscribe((setVisitSession) => {
                const url = location.protocol + '//' + window.location.hostname + ':' + window.location.port +
                    '/IQCare/CCC/Patient/PatientHome.aspx';
                const win = window.open(url, '_blank');
                win.focus();
            });
        });
    }
}

import { PrepService } from './../../_services/prep.service';
import { LookupItemView } from './../../../shared/_models/LookupItemView';
import { Component, OnInit, Input, Output, EventEmitter, NgZone } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import * as moment from 'moment';
import { SearchService } from '../../../registration/_services/search.service';
import { EncounterService } from '../../../shared/_services/encounter.service';
import { Router, ActivatedRoute } from '@angular/router';
import { APP_BASE_HREF } from '@angular/common';
import { LookupItemService } from './../../../shared/_services/lookup-item.service';
import { Subscription } from 'rxjs';
import { SnotifyService } from 'ng-snotify';
import { NotificationService } from './../../../shared/_services/notification.service';
import { PersonHomeService } from '../../../dashboard/services/person-home.service';
import { PersonView } from '../../../dashboard/_model/personView';

@Component({
    selector: 'app-prep-sti-screening-treatment',
    templateUrl: './prep-sti-screening-treatment.component.html',
    styleUrls: ['./prep-sti-screening-treatment.component.css'],
    providers: [SearchService, EncounterService, PersonHomeService]
})
export class PrepSTIScreeningTreatmentComponent implements OnInit {
    public person: PersonView;
    public personView$: Subscription;
    STIScreeningForm: FormGroup;
    yesnoOptions: LookupItemView[] = [];
    stiScreeningOptions: LookupItemView[] = [];
    screenedForSTIOptions: LookupItemView[] = [];
    stiOptions: LookupItemView[] = [];

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
        private personHomeService: PersonHomeService,
        private searchService: SearchService,
        private prepService: PrepService,
        private snotifyService: SnotifyService,
        private _lookupItemService: LookupItemService,
        private notificationService: NotificationService,
        private router: Router,
        private route: ActivatedRoute,
        public zone: NgZone,
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
        this.STIScreeningForm.controls.Specify.disable({ onlySelf: true });
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
        this.getPatientDetailsById(this.personId);
        if (this.isEdit == 1) {
            this.loadSTIScreening();
            this.loadPatientMasterVisit();
        }
    }

    public getPatientDetailsById(personId: number) {
        this.personView$ = this.personHomeService.getPatientByPersonId(personId).subscribe(
            p => {

                this.person = p;

                if (this.person != null) {


                    if (this.person.gender != null && this.person.gender != undefined) {
                        if (this.person.gender.toLowerCase() == 'male') {

                            this._lookupItemService.getByGroupName('STIScreeningTreatment').subscribe((res) => {
                                this.stiScreeningOptions = res['lookupItems'];
                                this.stiOptions = this.stiScreeningOptions.filter(x => x.itemName !== 'Cervicitis and/or Cervical discharge'
                                    && x.itemName !== 'Vaginitis or Vaginal discharge (VG)'
                                    && x.itemName !== 'Pelvic Inflammatory Disease (PID)');

                            });

                        } else if (this.person.gender.toLowerCase() == 'female') {

                            this._lookupItemService.getByGroupName('STIScreeningTreatment').subscribe((res) => {
                                this.stiScreeningOptions = res['lookupItems'];
                                this.stiOptions = this.stiScreeningOptions;

                            });
                        }
                    }

                }


            },
            (err) => {
                this.snotifyService.error('Error editing encounter ' + err, 'person detail service',
                    this.notificationService.getConfig());
            },
            () => {

            });
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
                const STISymptoms = [];
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


        this.zone.run(() => {
            this.router.navigate(['/pharm/' + this.patientId + '/' + this.personId],
                { relativeTo: this.route });
        });

    }
}

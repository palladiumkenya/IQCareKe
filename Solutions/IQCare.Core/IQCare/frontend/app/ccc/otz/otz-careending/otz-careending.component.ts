import {Component, NgZone, OnInit} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {ActivatedRoute, Router} from '@angular/router';
import {OtzService} from '../../_services/otz.service';
import {LookupItemView} from '../../../shared/_models/LookupItemView';
import {PatientMasterVisitEncounter} from '../../../pmtct/_models/PatientMasterVisitEncounter';
import {EncounterService} from '../../../shared/_services/encounter.service';

@Component({
    selector: 'app-otz-careending',
    templateUrl: './otz-careending.component.html',
    styleUrls: ['./otz-careending.component.css'],
    providers: [EncounterService]
})
export class OtzCareendingComponent implements OnInit {
    OtzCareEndingForm: FormGroup;
    CareEndingOptions: LookupItemView[] = [];
    EncounterTypeOptions: LookupItemView[] = [];
    
    patientId: number;
    serviceId: number;
    personId: number;
    maxDate: Date;
    userId: number;
    
    constructor(private _formBuilder: FormBuilder,
                private route: ActivatedRoute,
                public zone: NgZone,
                private router: Router,
                private otzService: OtzService,
                private encounterService: EncounterService) { }
    
    async ngOnInit() {
        this.route.params.subscribe(
            p => {
                const { patientId, personId, serviceId, id } = p;
                this.patientId = patientId;
                this.personId = personId;
                this.serviceId = serviceId;
            }
        );
        
        this.OtzCareEndingForm = this._formBuilder.group({
            transitionOutcome: new FormControl('', [Validators.required]),
            outComeDate: new FormControl('', [Validators.required]),
        });

        this.userId = JSON.parse(localStorage.getItem('appUserId'));
        
        const careEndOptions = await this.otzService.getByGroupName('OTZ_CareEndedOptions').toPromise();
        this.CareEndingOptions = careEndOptions['lookupItems'];

        const careEndEncounterType = await this.otzService.getByGroupName('EncounterType').toPromise();
        this.EncounterTypeOptions = careEndEncounterType['lookupItems'];
    }
    
    async close() {
        this.zone.run(() => {
            this.router.navigate(['/ccc/encounterHistory/' + this.patientId + '/' + this.personId + '/' + this.serviceId],
                { relativeTo: this.route });
        });
    }
    
    async save() {
        const careEndEncounterType = this.EncounterTypeOptions.filter(obj => obj.itemName == 'CareEnded');
        const patientMasterVisitEncounter: PatientMasterVisitEncounter = {
            EncounterDate: this.OtzCareEndingForm.get('outComeDate').value,
            PatientId: this.patientId,
            EncounterType: careEndEncounterType[0].itemId,
            ServiceAreaId: this.serviceId,
            UserId: this.userId
        };
        
        const otzCareEndingCommand: PatientCareEndingInterface = {
            PatientId: this.patientId,
            CareEndedDate: this.OtzCareEndingForm.get('outComeDate').value,
            DisclosureReason: this.OtzCareEndingForm.get('transitionOutcome').value.itemId,
            PatientMasterVisitId: 1,
            ServiceAreaId: this.serviceId,
            Specify: null,
            UserId: this.userId
        };
        
        try {
            const patientMasterVisit = await this.encounterService.savePatientMasterVisit(patientMasterVisitEncounter).toPromise();
            otzCareEndingCommand.PatientMasterVisitId = patientMasterVisit.patientMasterVisitId;
            const result = await this.otzService.careEndOtz(otzCareEndingCommand).toPromise();
            console.log(result);
        } catch (e) {
            console.log(e);
        }
    }
}

export interface PatientCareEndingInterface {
    PatientId: number;
    ServiceAreaId: number;
    PatientMasterVisitId: number;
    CareEndedDate: Date;
    Specify: string;
    DisclosureReason: number;
    DeathDate?: Date;
    UserId: number;
}

import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {RecordsService} from '../_services/records.service';
import {PersonHomeService} from '../../dashboard/services/person-home.service';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';

@Component({
    selector: 'app-records-merge',
    templateUrl: './records-merge.component.html',
    styleUrls: ['./records-merge.component.css'],
    providers: [RecordsService, PersonHomeService]
})
export class RecordsMergeComponent implements OnInit {
    MergePreferredForm: FormGroup;
    
    preferredPerson: any;
    unPreferredPerson: any;

    preferredPatientIdentifiers: any[] = [];
    unPreferredPatientIdentifiers: any[] = [];
    identifiers: any[] = [];
    preferredPatientContact: any;
    unPreferredPatientContact: any;
    
    preferredPatientEncounters: any[] = [];
    unPreferredPatientEncounters: any[] = [];
    
    constructor(private dialogRef: MatDialogRef<RecordsMergeComponent>,
                @Inject(MAT_DIALOG_DATA) data,
                private recordsService: RecordsService,
                private personHomeService: PersonHomeService,
                private _formBuilder: FormBuilder) {
        const selectedRecords = data.selectedRecords;
        this.preferredPerson = selectedRecords[0];
        this.unPreferredPerson = selectedRecords[1];
    }
    
    async ngOnInit() {
        this.MergePreferredForm = this._formBuilder.group({
            preferred: new FormControl('', [Validators.required]),
        });
        
        try {
            // get system identifiers
            this.identifiers = await this.recordsService.getAllIdentifiers().toPromise();
            
            // get all identifiers of a patient
            this.preferredPatientIdentifiers = 
                await this.recordsService.getPatientIdentifiersList(this.preferredPerson.patientId).toPromise();
            this.unPreferredPatientIdentifiers = 
                await this.recordsService.getPatientIdentifiersList(this.unPreferredPerson.patientId).toPromise();
            
            // get all contacts for a person
            this.preferredPatientContact = await this.recordsService.getPersonContacts(this.preferredPerson.personId).toPromise();
            this.unPreferredPatientContact = await this.recordsService.getPersonContacts(this.unPreferredPerson.personId).toPromise();
            
            // get all encounters by a patient
            this.preferredPatientEncounters = 
                await this.personHomeService.getPatientEncountersCompleted(this.preferredPerson.patientId).toPromise();
            this.unPreferredPatientEncounters =
                await this.personHomeService.getPatientEncountersCompleted(this.unPreferredPerson.patientId).toPromise();
        } catch (e) {
            console.log(e);
        }        
    }
    
    getServiceName(id: number): string {
        return this.identifiers.filter(obj => obj.id == id)[0].code;
    }

    close() {
        this.dialogRef.close();
    }

    mergePerson() {
        this.dialogRef.close();
    }
}

import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { LookupItemView } from '../../_models/LookupItemView';
import { LookupItemService } from '../../_services/lookup-item.service';
import { debounceTime } from 'rxjs/operators';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { RequireMatch as RequireMatch } from '../../_models/requireMatch';

@Component({
    selector: 'app-allergies',
    templateUrl: './allergies.component.html',
    styleUrls: ['./allergies.component.css'],
    providers: [LookupItemService]
})
export class AllergiesComponent implements OnInit {
    PatientAllergiesForm: FormGroup;
    maxDate: Date;

    filteredOptions: LookupItemView[];
    reactionOptions: LookupItemView[];

    severityOptions: LookupItemView[] = [];

    constructor(private _formBuilder: FormBuilder,
        private lookupItemService: LookupItemService,
        private dialogRef: MatDialogRef<AllergiesComponent>,
        @Inject(MAT_DIALOG_DATA) dialogData) {
        // maxDate is today
        this.maxDate = new Date();
    }

    ngOnInit() {
        this.PatientAllergiesForm = this._formBuilder.group({
            substanceAllergy: new FormControl('', [Validators.required, RequireMatch]),
            allergyReaction: new FormControl('', [Validators.required, RequireMatch]),
            severity: new FormControl('', [Validators.required]),
            onSetDate: new FormControl('', [Validators.required])
        });

        // Filter Allergies
        this.PatientAllergiesForm.controls.substanceAllergy.valueChanges.pipe(
            debounceTime(400)
        ).subscribe(data => {
            this.lookupItemService.getByGroupNameAndItemName('Allergies', data).subscribe(
                (res) => {
                    this.filteredOptions = res;
                },
                (error) => {
                    console.log(error);
                }
            );
        });

        // Filter Allergy Reactions
        this.PatientAllergiesForm.controls.allergyReaction.valueChanges.pipe(
            debounceTime(400)
        ).subscribe(data => {
            this.lookupItemService.getByGroupNameAndItemName('AllergyReactions', data).subscribe(
                (res) => {
                    this.reactionOptions = res;
                },
                (error) => {
                    console.log(error);
                }
            );
        });

        this.loadAllergiesOptions();
    }

    loadAllergiesOptions() {
        this.lookupItemService.getByGroupName('ADRSeverity').subscribe(
            (res) => {
                this.severityOptions = res['lookupItems'];
            },
            (error) => {
                console.log(error);
            }
        );
    }

    displayFn(allergy?: any): string | undefined {
        return allergy ? allergy.itemName : undefined;
    }

    displayReactionFn(allergyReaction?: any): string | undefined {
        return allergyReaction ? allergyReaction.itemName : undefined;
    }

    save() {
        if (this.PatientAllergiesForm.valid) {
            this.dialogRef.close(this.PatientAllergiesForm.value);
        } else {
            return;
        }
    }

    close() {
        this.dialogRef.close();
    }
}

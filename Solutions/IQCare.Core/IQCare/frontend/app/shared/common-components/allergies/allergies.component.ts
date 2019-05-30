import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { LookupItemView } from '../../_models/LookupItemView';
import { LookupItemService } from '../../_services/lookup-item.service';
import { debounceTime } from 'rxjs/operators';

@Component({
    selector: 'app-allergies',
    templateUrl: './allergies.component.html',
    styleUrls: ['./allergies.component.css'],
    providers: [LookupItemService]
})
export class AllergiesComponent implements OnInit {
    PatientAllergiesForm: FormGroup;
    myControl: FormControl = new FormControl();
    reactionControl: FormControl = new FormControl();

    filteredOptions: Observable<any[]>;
    reactionOptions: Observable<any[]>;

    severityOptions: LookupItemView[] = [];

    constructor(private _formBuilder: FormBuilder,
        private lookupItemService: LookupItemService) {
        // Filter Allergies
        this.myControl.valueChanges.pipe(
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
        this.reactionControl.valueChanges.pipe(
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
    }

    ngOnInit() {
        this.PatientAllergiesForm = this._formBuilder.group({
            severity: new FormControl('', [Validators.required]),
            onSetDate: new FormControl('', [Validators.required])
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
}

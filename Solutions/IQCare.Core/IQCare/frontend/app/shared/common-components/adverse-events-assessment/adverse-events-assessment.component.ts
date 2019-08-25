import { LookupItemView } from './../../_models/LookupItemView';
import { LookupItemService } from './../../_services/lookup-item.service';
import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { debounceTime } from 'rxjs/operators';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { RequireMatch } from '../../_models/requireMatch';

@Component({
    selector: 'app-adverse-events-assessment',
    templateUrl: './adverse-events-assessment.component.html',
    styleUrls: ['./adverse-events-assessment.component.css'],
    providers: [LookupItemService]
})
export class AdverseEventsAssessmentComponent implements OnInit {
    AdverseEventsAssessmentForm: FormGroup;
    filteredOptions: LookupItemView[];

    severityOptions: LookupItemView[] = [];
    adverseEventsActionsOptions: LookupItemView[] = [];

    constructor(private _formBuilder: FormBuilder,
        private lookupItemService: LookupItemService,
        private dialogRef: MatDialogRef<AdverseEventsAssessmentComponent>,
        @Inject(MAT_DIALOG_DATA) dialogData) {

    }

    ngOnInit() {
        this.AdverseEventsAssessmentForm = this._formBuilder.group({
            adverseEvent: new FormControl('', [Validators.required, RequireMatch]),
            severity: new FormControl('', [Validators.required]),
            medicine_causing: new FormControl('', [Validators.required]),
            adverseEventsAction: new FormControl('', [Validators.required]),
        });

        // Filter Adverse Events and for user to pick one
        this.AdverseEventsAssessmentForm.controls.adverseEvent.valueChanges.pipe(
            debounceTime(400)
        ).subscribe(data => {
            this.lookupItemService.getByGroupNameAndItemName('AdverseEvents', data).subscribe(
                (res) => {
                    this.filteredOptions = res;
                },
                (error) => {
                    console.log(error);
                }
            );
        });
        this.loadFormOptions();
    }

    loadFormOptions() {
        this.lookupItemService.getByGroupName('ADRSeverity').subscribe(
            (res) => {
                this.severityOptions = res['lookupItems'];
            },
            (error) => {
                console.log(error);
            }
        );

        this.lookupItemService.getByGroupName('AdverseEventsActions').subscribe(
            (res) => {
                this.adverseEventsActionsOptions = res['lookupItems'];
            },
            (error) => {
                console.log(error);
            }
        );
    }

    displayFn(adverseEvents?: any): string | undefined {
        return adverseEvents ? adverseEvents.itemName : undefined;
    }

    save() {
        if (this.AdverseEventsAssessmentForm.valid) {
            this.dialogRef.close(this.AdverseEventsAssessmentForm.value);
        } else {
            return;
        }
    }

    close() {
        this.dialogRef.close();
    }
}

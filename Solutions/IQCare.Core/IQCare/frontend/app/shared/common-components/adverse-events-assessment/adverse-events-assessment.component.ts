import { LookupItemView } from './../../_models/LookupItemView';
import { LookupItemService } from './../../_services/lookup-item.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { debounceTime } from 'rxjs/operators';

@Component({
    selector: 'app-adverse-events-assessment',
    templateUrl: './adverse-events-assessment.component.html',
    styleUrls: ['./adverse-events-assessment.component.css'],
    providers: [LookupItemService]
})
export class AdverseEventsAssessmentComponent implements OnInit {
    AdverseEventsAssessmentForm: FormGroup;
    myControl: FormControl = new FormControl();
    filteredOptions: Observable<any[]>;

    severityOptions: LookupItemView[] = [];
    adverseEventsActionsOptions: LookupItemView[] = [];

    constructor(private _formBuilder: FormBuilder,
        private lookupItemService: LookupItemService) {
        this.myControl.valueChanges.pipe(
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
    }

    ngOnInit() {
        this.AdverseEventsAssessmentForm = this._formBuilder.group({
            anyAdverseEvents: new FormControl('', [Validators.required]),
            severity: new FormControl('', [Validators.required]),
            medicine_causing: new FormControl('', [Validators.required]),
            adverseEventsAction: new FormControl('', [Validators.required]),
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
}

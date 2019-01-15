import { InlineSearchComponent } from './../../../records/inline-search/inline-search.component';
import { LookupItemView } from './../../../shared/_models/LookupItemView';
import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormBuilder, FormControl, Validators, FormGroup } from '@angular/forms';
import { MatDialogConfig, MatDialog } from '@angular/material';
import { FormControlService } from '../../../shared/_services/form-control.service';
import { FormControlBase } from '../../../shared/_models/FormControlBase';
import { FamilyPartnerControlsService } from '../../_services/family-partner-controls.service';

@Component({
    selector: 'app-family-search',
    templateUrl: './family-search.component.html',
    styleUrls: ['./family-search.component.css']
})
export class FamilySearchComponent implements OnInit {
    formGroup: FormGroup;
    yesnoOptions: LookupItemView[] = [];
    relationshipPartnerOptions: LookupItemView[] = [];
    relationshipFamilyOptions: LookupItemView[] = [];
    findRegistered: boolean = false;

    questions: FormControlBase<any>[] = [];

    constructor(private route: ActivatedRoute,
        private _formBuilder: FormBuilder,
        private dialog: MatDialog,
        private fcs: FormControlService,
        private service: FamilyPartnerControlsService) { }

    ngOnInit() {
        this.formGroup = this._formBuilder.group({});
        this.route.data.subscribe(
            (res) => {
                const { yesnoOptions } = res;
                this.yesnoOptions = yesnoOptions['lookupItems'];
            }
        );

        /*this.service.getRelationshipTypes().subscribe(
            (res) => {
                const partnerOptions = ['Partner', 'Co-Wife', 'Spouse'];
                const options = res['lookupItems'];
                for (let j = 0; j < options.length; j++) {
                    if (partnerOptions.includes(options[j].itemName)) {
                        this.relationshipPartnerOptions.push(options[j]);
                    } else {
                        this.relationshipFamilyOptions.push(options[j]);
                    }
                }

                console.log(this.relationshipFamilyOptions);
                this.questions = this.service.getFamilyControls(this.relationshipFamilyOptions);

                if (this.questions.length > 0) {
                    this.formGroup = this.fcs.toFormGroup(this.questions);
                }
            }
        );

        this.formGroup.addControl('isRegisteredInFacility', new FormControl('isRegisteredInFacility'));*/
    }

    onRegisteredChange(event) {
        if (event.isUserInput && event.source.selected && event.source.viewValue == 'Yes') {
            this.findRegistered = true;
        } else if (event.isUserInput && event.source.selected && event.source.viewValue == 'No') {
            this.findRegistered = false;
        }
    }

    findFamilyOrPartner() {
        const dialogConfig = new MatDialogConfig();

        dialogConfig.disableClose = true;
        dialogConfig.autoFocus = true;
        dialogConfig.height = '75%';
        dialogConfig.width = '60%';

        dialogConfig.data = {
        };

        const dialogRef = this.dialog.open(InlineSearchComponent, dialogConfig);

        dialogRef.afterClosed().subscribe(
            data => {

                if (!data) {
                    return;
                }

                console.log(data);
            }
        );
    }
}

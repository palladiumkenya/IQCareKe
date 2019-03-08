import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MatDialog, MatDialogConfig } from '@angular/material';
import { AddTriageComponent } from './add-triage/add-triage.component';

@Component({
    selector: 'app-triage',
    templateUrl: './triage.component.html',
    styleUrls: ['./triage.component.css']
})
export class TriageComponent implements OnInit {
    patientId: number;
    personId: number;
    constructor(private route: ActivatedRoute,
        private dialog: MatDialog) {

    }

    ngOnInit() {
        this.route.params.subscribe(
            (params) => {
                this.patientId = params.patientId;
                this.personId = params.personId;
                localStorage.setItem('partnerId', this.personId.toString());
            }
        );
    }


    public AddTriageInfo() {
        const resultsDialogConfig = new MatDialogConfig();

        resultsDialogConfig.disableClose = false;
        resultsDialogConfig.autoFocus = true;

        resultsDialogConfig.data = {
            patientId: this.patientId,
            personId: this.personId
        };

        const dialogRef = this.dialog.open(AddTriageComponent, resultsDialogConfig);
        dialogRef.afterClosed().subscribe(
            data => {
                if (!data) {
                    return;
                }
                console.log(data);
            });
    }

}

import { TriageService } from './../_services/triage.service';
import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MatDialog, MatDialogConfig } from '@angular/material';
import { AddTriageComponent } from './add-triage/add-triage.component';
import { RecordsService } from '../../records/_services/records.service';

@Component({
    selector: 'app-triage',
    templateUrl: './triage.component.html',
    styleUrls: ['./triage.component.css'],
    providers: [RecordsService]
})
export class TriageComponent implements OnInit {
    patientId: number;
    personId: number;

    height: number;
    weight: number;

    constructor(private route: ActivatedRoute,
        private dialog: MatDialog,
        private recordsService: RecordsService,
        private triageService: TriageService) {

    }

    ngOnInit() {
        this.route.params.subscribe(
            (params) => {
                this.patientId = params.patientId;
                this.personId = params.personId;
                localStorage.setItem('partnerId', this.personId.toString());
            }
        );

        this.recordsService.getPersonDetails(this.personId).subscribe(
            (res) => {
                if (res.length > 0) {
                    const today = new Date();
                    const dob = new Date(res[0].dateOfBirth);

                    let age = today.getFullYear() - dob.getFullYear();
                    let ageMonths = today.getMonth() - dob.getMonth();
                    if (ageMonths < 0 || (ageMonths === 0 && today.getDate() < dob.getDate())) {
                        age--;
                    }

                    if (ageMonths < 0) {
                        ageMonths = 12 - (-ageMonths + 1);
                    }

                    if (age >= 18) {
                        this.triageService.GetPatientVitalsInfo(this.patientId).subscribe(
                            (result) => {
                                if (result.length > 0) {
                                    this.height = result[result.length - 1].height;
                                    this.weight = result[result.length - 1].weight;
                                }
                            },
                            (error) => {
                                console.log(error);
                            }
                        );
                    }
                }
            }
        );
    }


    public AddTriageInfo() {
        const resultsDialogConfig = new MatDialogConfig();

        resultsDialogConfig.disableClose = false;
        resultsDialogConfig.autoFocus = true;

        resultsDialogConfig.data = {
            patientId: this.patientId,
            personId: this.personId,
            height: this.height,
            weight: this.weight
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

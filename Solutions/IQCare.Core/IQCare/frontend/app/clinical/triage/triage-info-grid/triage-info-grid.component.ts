import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { MatTableDataSource, MatPaginator, MatDialog, MatDialogConfig } from '@angular/material';
import { TriageService } from '../../_services/triage.service';
import { AddTriageComponent } from '../add-triage/add-triage.component';

@Component({
    selector: 'app-triage-info-grid',
    templateUrl: './triage-info-grid.component.html',
    styleUrls: ['./triage-info-grid.component.css']
})
export class TriageInfoGridComponent implements OnInit {
    vitalsDataTable: any[] = [];
    displayedColumns = ['visitdate', 'height', 'weight', 'bmi', 'systolic', 'diastolic', 'temperature', 'respiratoryrate', 'heartrate',
        'action'];
    dataSource = new MatTableDataSource(this.vitalsDataTable);
    @ViewChild(MatPaginator) paginator: MatPaginator;

    @Input('PatientId') PatientId: number;
    constructor(private triageService: TriageService,
        private dialog: MatDialog) {
    }

    ngOnInit() {
        this.getPatientVitalsInfo(this.PatientId);
    }


    public getPatientVitalsInfo(patientId: number) {
        this.triageService.GetPatientVitalsInfo(patientId).subscribe(res => {
            if (res == null) {
                return;
            }

            this.vitalsDataTable = [];

            res.forEach(info => {
                this.vitalsDataTable.push({
                    id: info.id,
                    visitDate: info.visitDate,
                    height: info.height,
                    weight: info.weight,
                    bmi: info.bmi,
                    headCircumference: info.headCircumference,
                    muac: info.muac,
                    weightForAge: info.weightForAge,
                    weightForHeight: info.weightForHeight,
                    bmiZ: info.bmiZ,
                    diastolic: info.bpDiastolic,
                    systolic: info.bpSystolic,
                    temperature: info.temperature,
                    respiratoryRate: info.respiratoryRate,
                    heartRate: info.heartRate,
                    spo2: info.spo2,
                    comment: info.comment,
                    patientId: info.patientId,
                    patientMasterVisitId: info.patientMasterVisitId
                });

                this.dataSource = new MatTableDataSource(this.vitalsDataTable);
                this.dataSource.paginator = this.paginator;
            });

        }, (err) => {
            console.log(err + ' An error occured while getting patient vitals info');
        }, () => {

        }
        );
    }


    public editTriageInfo(triageInfo: any) {
        const triageDialogConfig = new MatDialogConfig();

        triageDialogConfig.disableClose = false;
        triageDialogConfig.autoFocus = true;

        triageDialogConfig.data = {
            isEdit: true,
            triageInfo: triageInfo
        };

        const dialogRef = this.dialog.open(AddTriageComponent, triageDialogConfig);
        dialogRef.afterClosed().subscribe(
            data => {
                if (!data) {
                    return;
                }
            });
    }

}

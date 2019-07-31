import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { MatTableDataSource, MatPaginator, MatDialog, MatDialogConfig } from '@angular/material';
import { TriageService } from '../../clinical/_services/triage.service';
import { ProvidersFeature } from '@angular/core/src/render3';

@Component({
    selector: 'app-prep-patientvitalsinfo',
    templateUrl: './prep-patientvitalsinfo.component.html',
    styleUrls: ['./prep-patientvitalsinfo.component.css'],
    providers: [
        TriageService
    ]
})
export class PrepPatientvitalsinfoComponent implements OnInit {
    vitalsDataTable: any[] = [];
    displayedColumns = ['visitdate', 'height', 'weight', 'bmi', 'diastolic', 'systolic', 'temperature', 'respiratoryrate', 'heartrate']
        ;
    dataSource = new MatTableDataSource(this.vitalsDataTable);
    @ViewChild(MatPaginator) paginator: MatPaginator;
    @Input('patientId') PatientId: number;
    constructor(private triageService: TriageService) { }

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

}

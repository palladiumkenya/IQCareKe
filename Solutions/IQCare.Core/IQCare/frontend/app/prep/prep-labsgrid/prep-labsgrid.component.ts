import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { PrepService } from '../_services/prep.service';
import { MatTableDataSource, MatPaginator, MatDialogConfig, MatDialog } from '@angular/material';

@Component({
    selector: 'app-prep-labsgrid',
    templateUrl: './prep-labsgrid.component.html',
    styleUrls: ['./prep-labsgrid.component.css']
})
export class PrepLabsgridComponent implements OnInit {
    @Input('patientId') PatientId: number;
    completed_labs_displaycolumns: any[] = ['test', 'orderReason', 'orderDate', 'result', 'unit'];

    @ViewChild(MatPaginator) paginator: MatPaginator;

    completedLabTests: any[] = [];
    creatinineLabTests: any[] = [];
    completedLabsDataSource = new MatTableDataSource(this.completedLabTests);
    completedCreatinineDataSource = new MatTableDataSource(this.creatinineLabTests);
    defaultResultUnit = 'No Units';

    constructor(private prepService: PrepService,
        private dialog: MatDialog) {
    }

    ngOnInit() {
        this.getCompletedLabs(this.PatientId);
        this.getCompletedCreatinineLabs(this.PatientId);
    }

    public getCompletedCreatinineLabs(patientId: number) {
        let creatinine: string[];

        creatinine = ['Creatinine'];

        this.prepService.getLabTestResults(patientId, 'Complete').subscribe(res => {
            if (res.length == 0)
                return;
        
            res.forEach(test => {
                if (test.labTestName == 'Creatinine') {
                    this.creatinineLabTests.push({
                        labOrderTestId: test.labOrderTestId,
                        labOrderId: test.labOrderId,
                        test: test.labTestName,
                        orderDate: test.orderDate,
                        orderReason: test.orderReason == null || test.orderReason == '' ? 'N/A' : test.orderReason,
                        labTestId: test.labTestId,
                        unit: test.resultUnits == null || (test.resultUnits.toUpperCase() == this.defaultResultUnit.toUpperCase() ?
                            'N/A' : test.resultUnits) ?
                            'N/A' : test.resultUnits,
                        resultDate: test.resultDate,
                        result: test.result,
                        status: test.resultStatus
                    });

                }
            });
            this.completedCreatinineDataSource = new MatTableDataSource(this.creatinineLabTests);
            this.completedCreatinineDataSource.paginator = this.paginator;

        }, (error) => {
            console.log(error + "An error occured while getting completed labs");
        });


    }
    public getCompletedLabs(patientId: number) {
        let labtests: string[];



        labtests = ['Hepatitis B core - antibody IgM (HBsAb)', 'Hepatitis B core – antibody, total',
            'Hepatitis B surface – antibody (HBsAb)', 'Hepatitis B surface – antigen (HBsAg)','Hepatitis C antibody'];




        this.prepService.getLabTestResults(patientId, 'Complete').subscribe(res => {
            if (res.length == 0)
                return;

            console.log('Hepatitis');
            console.log(res);
            labtests.forEach(x => {
                res.forEach(test => {
                    if (test.labTestName == x.toString()) {
                        this.completedLabTests.push({
                            labOrderTestId: test.labOrderTestId,
                            labOrderId: test.labOrderId,
                            test: test.labTestName,
                            orderDate: test.orderDate,
                            orderReason: test.orderReason == null || test.orderReason == '' ? 'N/A' : test.orderReason,
                            labTestId: test.labTestId,
                            unit: test.resultUnits == null || (test.resultUnits.toUpperCase() == this.defaultResultUnit.toUpperCase() ?
                                'N/A' : test.resultUnits) ?
                                'N/A' : test.resultUnits,
                            resultDate: test.resultDate,
                            result: test.result,
                            status: test.resultStatus
                        });
                    }
                });
            });
            this.completedLabsDataSource = new MatTableDataSource(this.completedLabTests);
            this.completedLabsDataSource.paginator = this.paginator;

        }, (error) => {
            console.log(error + "An error occured while getting completed labs");
        });
    }


}

import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { PrepService } from '../_services/prep.service';
import { MatTableDataSource, MatPaginator, MatDialogConfig, MatDialog } from '@angular/material';
import { VisitDetailsComponent } from '../../pmtct/anc/visit-details/visit-details.component';
import { VisitDetailsService } from '../../pmtct/_services/visit-details.service';
import { PatientId } from '../../shared/reducers/app.states';
import * as moment from 'moment';


@Component({
    selector: 'app-prep-riskassessmentgriddetails',
    templateUrl: './prep-riskassessmentgriddetails.component.html',
    styleUrls: ['./prep-riskassessmentgriddetails.component.css']
})
export class PrepRiskassessmentgriddetailsComponent implements OnInit {
    @Input('personId') personId: number;
    riskassessmentdetails: any[] = [];
    displayedcolumns: any[] = ['VisitDate', 'Clientwillingtakeprep'];
    @ViewChild(MatPaginator) paginator: MatPaginator;
    DataSource = new MatTableDataSource(this.riskassessmentdetails);
    constructor(private prepService: PrepService) { }

    ngOnInit() {
        this.GetPrepRiskassessmentVisits();
    }

    GetPrepRiskassessmentVisits() {

        this.prepService.GetRiskAssessmentDetails(this.personId).subscribe(res => {
            if (res.length == 0)
                return;

            res.forEach(test => {
                this.riskassessmentdetails.push({
                    patientId: test.patientId,
                    patientMasterVisitId: test.patientMasterVisitId,
                    VisitDate: moment(test.visitDate).format('DD-MMM-YYYY'),
                    Clientwillingtakeprep: test.clientWillingTakingPrep
                });
            });
            this.DataSource = new MatTableDataSource(this.riskassessmentdetails);
            this.DataSource.paginator = this.paginator;

        }, (error) => {
            console.log(error + 'An error occurred loading risk assessment details');
        });



    }

}

import { ActivatedRoute, Router } from '@angular/router';
import { PharmacyService } from '../services/pharmacy.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { Component, OnInit, ViewChild, Input, NgZone } from '@angular/core';
import { MatTableDataSource, MatPaginator, MatDialog, MatDialogConfig } from '@angular/material';

@Component({
    selector: 'app-pharm-mainpage',
    templateUrl: './pharm-mainpage.component.html',
    styleUrls: ['./pharm-mainpage.component.css']
})
export class PharmMainpageComponent implements OnInit {

    patientId: number;
    personId: number;

    height: number;
    weight: number;
    pharmacyvisittable: any[] = [];
    displayedColumns = ['visitDate', 'Status',
        'action'];
    dataSource = new MatTableDataSource(this.pharmacyvisittable);
    @ViewChild(MatPaginator) paginator: MatPaginator;
    constructor(private route: ActivatedRoute,
        private pharmacyservice: PharmacyService,
        private spinner: NgxSpinnerService,
        private zone: NgZone,
        private router: Router) { }

    ngOnInit() {
        this.route.params.subscribe(
            (params) => {
                this.patientId = params.patientId;
                this.personId = params.personId;

                this.getPatientVisits(this.patientId);
            }
        );



    }

    public getPatientVisits(patientId: number) {
        this.spinner.show();
        this.pharmacyservice.getPharmacyVisit(this.patientId).subscribe((res) => {
            if (res != null) {

                res.forEach(info => {
                    this.pharmacyvisittable.push({
                        visitDate: info.visitDate,
                        visitId: info.visitID,
                        visitName: info.visitName,
                        status: info.status,
                        PatientId: info.patientId,
                        statustext: info.orderStatusText

                    });

                    this.dataSource = new MatTableDataSource(this.pharmacyvisittable);
                    this.dataSource.paginator = this.paginator;
                });

            }
            this.spinner.hide();
        }, (err) => {
            console.log(err + ' An error occured while getting patient pharmacy info');
            this.spinner.hide();
        }, () => {
            this.spinner.hide();
        }
        );
    }
    AddPharmacy() {

        this.zone.run(() => {
            this.router.navigate(['/pharm/' + this.patientId + '/' + this.personId
            ], { relativeTo: this.route });
        });

    }
    editPharmacyForm(row) {
        console.log(row);

        this.zone.run(() => {
            this.router.navigate(['/pharm/' + this.patientId + '/' + this.personId + '/' + row.visitId + '/' + 1
            ], { relativeTo: this.route });
        });
    }

}

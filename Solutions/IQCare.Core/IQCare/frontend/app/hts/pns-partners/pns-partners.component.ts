import {Component, NgZone, OnInit} from '@angular/core';
import {PnsService} from '../_services/pns.service';
import {DataSource} from '@angular/cdk/collections';
import {Observable} from 'rxjs/Observable';
import {ActivatedRoute, Router} from '@angular/router';

@Component({
  selector: 'app-pns-partners',
  templateUrl: './pns-partners.component.html',
  styleUrls: ['./pns-partners.component.css']
})
export class PnsPartnersComponent implements OnInit {
    personId: number;
    patientId: number;
    highlightedRow: any[] = [];

    displayedColumns = ['firstName', 'midName', 'lastName', 'dateOfBirth', 'gender', 'relationshipType', 'actionsColumn'];
    dataSource = new PnsDataSource(this.pnsService, this.patientId);

    constructor(private pnsService: PnsService,
                private router: Router,
                private route: ActivatedRoute,
                public zone: NgZone) { }

    ngOnInit() {
        this.personId = JSON.parse(localStorage.getItem('personId'));
        this.patientId = JSON.parse(localStorage.getItem('patientId'));

        this.dataSource = new PnsDataSource(this.pnsService, this.patientId);
        // this.getPartners();
    }

    getPartners() {
        this.pnsService.getClientPartners(this.patientId).subscribe(data => {
            console.log(data);
        }, err => {
            console.log(err);
        });
    }

    getSelectedRow(row) {
        this.highlightedRow = [];
        this.highlightedRow.push(row);

    }

    screenClient(row) {
        console.log(row);

        localStorage.setItem('partnerId', row['personId']);
        this.zone.run(() => { this.router.navigate(['/hts/pnsform'], { relativeTo: this.route }); });
    }

}

export class PnsDataSource extends DataSource<any> {
    constructor(private pnsService: PnsService, private patientId: number) {
        super();
    }

    connect(): Observable<any[]> {
        return this.pnsService.getClientPartners(this.patientId);
    }

    disconnect() {

    }
}

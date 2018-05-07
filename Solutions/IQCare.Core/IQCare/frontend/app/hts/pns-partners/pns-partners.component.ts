import {Component, NgZone, OnInit} from '@angular/core';
import {PnsService} from '../_services/pns.service';
import {DataSource} from '@angular/cdk/collections';
import {Observable} from 'rxjs/Observable';
import {ActivatedRoute, Router} from '@angular/router';
import {select, Store} from '@ngrx/store';

@Component({
  selector: 'app-pns-partners',
  templateUrl: './pns-partners.component.html',
  styleUrls: ['./pns-partners.component.css']
})
export class PnsPartnersComponent implements OnInit {
    personId: number;
    patientId: number;
    highlightedRow: any[] = [];

    isPnsScreeningDone = [];
    isPnsTracingDone = [];

    displayedColumns = ['firstName', 'midName', 'lastName', 'dateOfBirth', 'gender', 'relationshipType', 'actionsColumn'];
    dataSource = new PnsDataSource(this.pnsService, this.patientId);

    constructor(private pnsService: PnsService,
                private router: Router,
                private route: ActivatedRoute,
                public zone: NgZone,
                private store: Store<AppState>) {
        store.pipe(select('app')).subscribe(res => {
            if (typeof(res['isPnsScreened']) !== 'undefined' && res['isPnsScreened'] !== null) {
                this.isPnsScreeningDone = res['isPnsScreened'];
            }
        });

        store.pipe(select('app')).subscribe(res => {
            if (typeof(res['isPnsTracingDone']) !== 'undefined' && res['isPnsTracingDone'] !== null) {
                this.isPnsTracingDone = res['isPnsTracingDone'];
            }
        });
    }

    ngOnInit() {
        console.log(this.isPnsScreeningDone);

        this.personId = JSON.parse(localStorage.getItem('personId'));
        this.patientId = JSON.parse(localStorage.getItem('patientId'));

        this.dataSource = new PnsDataSource(this.pnsService, this.patientId);
    }

    getSelectedRow(row) {
        console.log(row);
        this.highlightedRow = [];
        this.highlightedRow.push(row);
    }

    screenClient(row) {
        localStorage.setItem('partnerId', row['personId']);
        this.zone.run(() => { this.router.navigate(['/hts/pnsform'], { relativeTo: this.route }); });
    }

    traceClient(row) {
        localStorage.setItem('partnerId', row['personId']);
        this.zone.run(() => { this.router.navigate(['/hts/pnstracing'], { relativeTo: this.route }); });
    }

    newPartner() {
        const newPartner = {
            'partner': 1,
            'family': 0
        };
        localStorage.setItem('isPartner', JSON.stringify(newPartner));
        this.zone.run(() => { this.router.navigate(['/registration/register'], {relativeTo: this.route}); });
    }

    isPnsScreened(personID) {
        for (let i = 0; i < this.isPnsScreeningDone.length; i++) {
            if (this.isPnsScreeningDone[i]['partnerId'] == personID) {
                return true;
            }
        }
        return false;
    }

    isPnsTracing(personID) {
        for (let i = 0; i < this.isPnsTracingDone.length; i++) {
            if (this.isPnsTracingDone[i]['partnerId'] == personID) {
                return true;
            }
        }
        return false;
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

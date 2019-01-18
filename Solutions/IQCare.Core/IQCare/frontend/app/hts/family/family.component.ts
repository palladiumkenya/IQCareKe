import { Component, NgZone, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DataSource } from '@angular/cdk/collections';
import { Observable } from 'rxjs';
import { FamilyService } from '../_services/family.service';
import { Store, select } from '@ngrx/store';

@Component({
    selector: 'app-family',
    templateUrl: './family.component.html',
    styleUrls: ['./family.component.css']
})
export class FamilyComponent implements OnInit {
    patientId: number;
    highlightedRow: any[] = [];
    isFamilyScreeningDone = [];
    isFamilyScreenedPositive = [];

    displayedColumns = ['firstName', 'midName', 'lastName', 'dateOfBirth', 'gender', 'relationshipType', 'actionsColumn'];
    dataSource = new FamilyDataSource(this.familyService, this.patientId);

    constructor(private router: Router,
        private route: ActivatedRoute,
        public zone: NgZone,
        private familyService: FamilyService,
        private store: Store<AppState>) {
        store.pipe(select('app')).subscribe(res => {
            console.log(res);
            if (typeof (res['isFamilyScreeningDone']) !== 'undefined' && res['isFamilyScreeningDone'] !== null) {
                this.isFamilyScreeningDone = res['isFamilyScreeningDone'];
            }

            store.pipe(select('app')).subscribe(res => {
                if (typeof (res['FamilyScreenedPositive']) !== 'undefined' && res['FamilyScreenedPositive'] !== null) {
                    this.isFamilyScreenedPositive = res['FamilyScreenedPositive'];
                }
            });
        });
    }

    ngOnInit() {
        this.patientId = JSON.parse(localStorage.getItem('patientId'));

        this.dataSource = new FamilyDataSource(this.familyService, this.patientId);
    }

    newPartner() {
        const newPartner = {
            'partner': 0,
            'family': 1
        };
        localStorage.setItem('isPartner', JSON.stringify(newPartner));
        this.zone.run(() => { this.router.navigate(['/registration/register'], { relativeTo: this.route }); });
    }

    getSelectedRow(row) {
        this.highlightedRow = [];
        this.highlightedRow.push(row);
    }

    screenClient(row) {
        // console.log(row);
        localStorage.setItem('partnerId', row['personId']);
        this.zone.run(() => { this.router.navigate(['/hts/family/screening'], { relativeTo: this.route }); });
    }

    traceClient(row) {
        localStorage.setItem('partnerId', row['personId']);
        this.zone.run(() => { this.router.navigate(['/hts/family/familytracinglist'], { relativeTo: this.route }); });
    }

    isFamilyScreened(personID) {
        for (let i = 0; i < this.isFamilyScreeningDone.length; i++) {
            if (this.isFamilyScreeningDone[i]['familyId'] == personID) {
                return true;
            }
        }
        return false;
    }

    FamilyScreenedPositive(personID) {
        for (let i = 0; i < this.isFamilyScreenedPositive.length; i++) {
            if (this.isFamilyScreenedPositive[i]['familyId'] == personID) {
                return true;
            }
        }
        return false;
    }
}

export class FamilyDataSource extends DataSource<any> {
    constructor(private familyService: FamilyService, private patientId: number) {
        super();
    }

    connect(): Observable<any[]> {
        return this.familyService.getClientFamily(this.patientId);
    }

    disconnect() {

    }
}

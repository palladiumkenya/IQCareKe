import { Component, NgZone, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DataSource } from '@angular/cdk/collections';
import { Observable } from 'rxjs';
import { FamilyService } from '../_services/family.service';

@Component({
    selector: 'app-family',
    templateUrl: './family.component.html',
    styleUrls: ['./family.component.css']
})
export class FamilyComponent implements OnInit {
    patientId: number;
    highlightedRow: any[] = [];

    displayedColumns = ['firstName', 'midName', 'lastName', 'dateOfBirth', 'gender', 'relationshipType', 'actionsColumn'];
    dataSource = new FamilyDataSource(this.familyService, this.patientId);

    constructor(private router: Router,
        private route: ActivatedRoute,
        public zone: NgZone,
        private familyService: FamilyService) { }

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
        this.zone.run(() => { this.router.navigate(['/hts/family/tracing'], { relativeTo: this.route }); });
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

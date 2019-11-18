import {Component, NgZone, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {MatTableDataSource} from '@angular/material/table';
import {TopicsTableData} from '../activity-form/activity-form.component';

@Component({
  selector: 'app-encounter-history',
  templateUrl: './encounter-history.component.html',
  styleUrls: ['./encounter-history.component.css']
})
export class EncounterHistoryComponent implements OnInit {
    patientId: number;
    serviceId: number;
    personId: number;

    displayedColumns = ['module', 'dateCovered', 'action'];
    topics_table_data: TopicsTableData[] = [];
    dataSource = new MatTableDataSource(this.topics_table_data);
    
    constructor(private route: ActivatedRoute,
                public zone: NgZone,
                private router: Router) { }
    
    async ngOnInit() {
        this.route.params.subscribe(
            p => {
                const { patientId, personId, serviceId } = p;
                this.patientId = patientId;
                this.personId = personId;
                this.serviceId = serviceId;
            }
        );
    }

    onNewOtzClick() {
        this.zone.run(() => {
            this.router.navigate(['/ccc/activityForm/' + this.patientId + '/' + this.personId + '/' + this.serviceId],
                { relativeTo: this.route });
        });
    }
}

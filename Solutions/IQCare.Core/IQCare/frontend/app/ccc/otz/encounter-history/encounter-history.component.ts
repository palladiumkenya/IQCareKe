import {Component, NgZone, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {MatTableDataSource} from '@angular/material/table';
import {OtzService} from '../../_services/otz.service';

@Component({
  selector: 'app-encounter-history',
  templateUrl: './encounter-history.component.html',
  styleUrls: ['./encounter-history.component.css']
})
export class EncounterHistoryComponent implements OnInit {
    patientId: number;
    serviceId: number;
    personId: number;

    displayedColumns = ['visitDate', 'attendedSupportGroup', 'provider', 'modulesDone', 'action'];
    topics_table_data: any[] = [];
    dataSource = new MatTableDataSource(this.topics_table_data);
    
    constructor(private route: ActivatedRoute,
                public zone: NgZone,
                private router: Router,
                private otzService: OtzService) { }
    
    async ngOnInit() {
        this.route.params.subscribe(
            p => {
                const { patientId, personId, serviceId } = p;
                this.patientId = patientId;
                this.personId = personId;
                this.serviceId = serviceId;
            }
        );
        
        try {
            const activityForms = await this.otzService.getActivityForms(this.patientId).toPromise();
            this.topics_table_data = activityForms;
            this.dataSource = new MatTableDataSource(this.topics_table_data);
        } catch (e) {
            console.log(e);
        }
    }

    onNewOtzClick() {
        this.zone.run(() => {
            this.router.navigate(['/ccc/activityForm/' + this.patientId + '/' + this.personId + '/' + this.serviceId],
                { relativeTo: this.route });
        });
    }
}

import {Component, NgZone, OnInit} from '@angular/core';
import {OtzService} from '../../_services/otz.service';
import {ActivatedRoute, Router} from '@angular/router';
import {MatTableDataSource} from '@angular/material/table';

@Component({
  selector: 'app-view-otz-form',
  templateUrl: './view-otz-form.component.html',
  styleUrls: ['./view-otz-form.component.css']
})
export class ViewOtzFormComponent implements OnInit {
    id: number;
    patientId: number;

    visitDate: Date;
    enrollmentDate: Date;
    attendedSupportGroup: string;
    provider: string;

    displayedColumns = ['topic', 'dateCompleted'];
    topics_table_data: any[] = [];
    dataSource = new MatTableDataSource(this.topics_table_data);
    
    constructor(private otzService: OtzService,
                private route: ActivatedRoute,
                public zone: NgZone,
                private router: Router) { }
    
    async ngOnInit() {
        this.route.params.subscribe(
            p => {
                const { id, patientId } = p;
                this.id = id;
                this.patientId = patientId;
            }
        );
        
        const result = await this.otzService.getOtzActivityForm(this.id).toPromise();
        this.visitDate = result.visitDate;
        this.attendedSupportGroup = result.attendedSupportGroup;
        this.provider = result.provider;

        const otzEnrollment = await this.otzService.getOtzEnrollment(result.patientId, 8).toPromise();
        if (otzEnrollment) {
            this.enrollmentDate = otzEnrollment.enrollmentDate;
        }
        
        const completedOtzModules = await this.otzService.getOtzCompletedModules(this.patientId).toPromise();
        this.topics_table_data = completedOtzModules;
        this.dataSource = new MatTableDataSource(this.topics_table_data);
    }
}

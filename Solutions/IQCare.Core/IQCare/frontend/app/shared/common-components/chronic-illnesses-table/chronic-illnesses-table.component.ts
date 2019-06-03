import { Component, OnInit } from '@angular/core';
import { MatTableDataSource, MatDialogConfig, MatDialog } from '@angular/material';
import { PatientChronicIllnessesComponent } from '../patient-chronic-illnesses/patient-chronic-illnesses.component';
import { NotificationService } from '../../_services/notification.service';
import { SnotifyService } from 'ng-snotify';

@Component({
    selector: 'app-chronic-illnesses-table',
    templateUrl: './chronic-illnesses-table.component.html',
    styleUrls: ['./chronic-illnesses-table.component.css']
})
export class ChronicIllnessesTableComponent implements OnInit {
    public chronic_illness_table_data: ChronicIllnessTableData[] = [];

    displayedColumns = ['illness', 'currentTreatment', 'onsetDate', 'active', 'action'];
    dataSource = new MatTableDataSource(this.chronic_illness_table_data);

    constructor(private dialog: MatDialog,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService) { }

    ngOnInit() {
    }

    newChronicIllness() {
        const dialogConfig = new MatDialogConfig();
        dialogConfig.disableClose = true;
        dialogConfig.autoFocus = true;

        dialogConfig.data = {
        };

        const dialogRef = this.dialog.open(PatientChronicIllnessesComponent, dialogConfig);

        dialogRef.afterClosed().subscribe(
            data => {
                if (!data) {
                    return;
                }

                const illness = data.illness.itemName;

                if (this.chronic_illness_table_data.filter(x => x.illness === illness).length > 0) {
                    this.snotifyService.warning('' + illness + ' exists',
                        'Chronic Conditions and Comorbidities', this.notificationService.getConfig());
                } else {
                    this.chronic_illness_table_data.push({
                        illness: illness,
                        currentTreatment: data.currentTreatment,
                        onsetDate: data.onsetDate
                    });

                    this.dataSource = new MatTableDataSource(this.chronic_illness_table_data);
                }
            }
        );
    }

}

export interface ChronicIllnessTableData {
    illness?: string;
    currentTreatment?: string;
    onsetDate?: Date;
    active?: string;
}

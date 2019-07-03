import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';
import { MatTableDataSource, MatDialogConfig, MatDialog } from '@angular/material';
import { PatientChronicIllnessesComponent } from '../patient-chronic-illnesses/patient-chronic-illnesses.component';
import { NotificationService } from '../../_services/notification.service';
import { SnotifyService } from 'ng-snotify';

@Component({
    selector: 'app-chronic-illnesses-table',
    templateUrl: './chronic-illnesses-table.component.html',
    styleUrls: ['./chronic-illnesses-table.component.css'],
    providers: []
})
export class ChronicIllnessesTableComponent implements OnInit {
    public chronic_illness_table_data: ChronicIllnessTableData[] = [];
    public newChronic_illnesses: ChronicIllnessTableData[] = [];

    displayedColumns = ['illness', 'currentTreatment', 'onsetDate', 'active', 'action'];
    dataSource = new MatTableDataSource(this.chronic_illness_table_data);

    @Output() notify: EventEmitter<ChronicIllnessTableData[]> = new EventEmitter<ChronicIllnessTableData[]>();
    @Input() patientId: number;
    @Input() personId: number;

    constructor(private dialog: MatDialog,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService) { }

    ngOnInit() {
        // emit new chronic illnesses to stepper 
        this.notify.emit(this.newChronic_illnesses);

        this.loadPatientChronicIllnesses();
    }

    public loadPatientChronicIllnesses() {

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

                    this.newChronic_illnesses.push({
                        illness: data.illness,
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

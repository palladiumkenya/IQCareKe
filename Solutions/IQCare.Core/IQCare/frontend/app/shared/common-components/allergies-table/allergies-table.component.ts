import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';
import { MatTableDataSource, MatDialogConfig, MatDialog } from '@angular/material';
import { AllergiesComponent } from '../allergies/allergies.component';
import { SnotifyService } from 'ng-snotify';
import { NotificationService } from '../../_services/notification.service';
import { PrepService } from '../../../prep/_services/prep.service';

@Component({
    selector: 'app-allergies-table',
    templateUrl: './allergies-table.component.html',
    styleUrls: ['./allergies-table.component.css'],
    providers: [PrepService]
})
export class AllergiesTableComponent implements OnInit {
    public allergy_table_data: AllergyTableData[] = [];
    public newAllergyData: AllergyTableData[] = [];

    displayedColumns = ['allergy', 'reactionType', 'severity', 'onsetDate', 'active', 'action'];
    dataSource = new MatTableDataSource(this.allergy_table_data);

    @Output() notify: EventEmitter<AllergyTableData[]> = new EventEmitter<AllergyTableData[]>();
    @Input() patientId: number;
    @Input() personId: number;

    constructor(private dialog: MatDialog,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private prepservice: PrepService) { }

    ngOnInit() {
        // emit new allergies to stepper 
        this.notify.emit(this.newAllergyData);

        this.loadPatientAllergies();
    }

    public loadPatientAllergies(): void {
        this.prepservice.getPatientAllergies(this.patientId).subscribe(
            (res) => {
                res.forEach(allergyData => {
                    this.allergy_table_data.push({
                        allergy: allergyData.allergenName,
                        reactionType: allergyData.reactionName,
                        severity: allergyData.severityName,
                        onsetDate: allergyData.onsetDate,
                        active: ''
                    });
                });

                this.dataSource = new MatTableDataSource(this.allergy_table_data);
            },
            (error) => {
                console.log(error);
            }
        );
    }

    newAllergies() {
        const dialogConfig = new MatDialogConfig();
        dialogConfig.disableClose = true;
        dialogConfig.autoFocus = true;

        dialogConfig.data = {
        };

        const dialogRef = this.dialog.open(AllergiesComponent, dialogConfig);
        dialogRef.afterClosed().subscribe(
            data => {
                if (!data) {
                    return;
                }

                // console.log(data);

                const substanceAllergy = data.substanceAllergy.itemName;
                if (this.allergy_table_data.filter(x => x.allergy === substanceAllergy).length > 0) {
                    this.snotifyService.warning('' + substanceAllergy + ' exists',
                        'Allergies', this.notificationService.getConfig());
                } else {
                    this.allergy_table_data.push({
                        allergy: substanceAllergy,
                        reactionType: data.allergyReaction.itemName,
                        severity: data.severity.itemName,
                        onsetDate: data.onSetDate
                    });

                    this.newAllergyData.push({
                        allergy: data.substanceAllergy,
                        reactionType: data.allergyReaction,
                        severity: data.severity,
                        onsetDate: data.onSetDate
                    });

                    this.dataSource = new MatTableDataSource(this.allergy_table_data);
                }
            }
        );
    }

}

export interface AllergyTableData {
    allergy?: string;
    reactionType?: string;
    severity?: string;
    onsetDate?: Date;
    active?: string;
}

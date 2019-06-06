import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { MatTableDataSource, MatDialogConfig, MatDialog } from '@angular/material';
import { AllergiesComponent } from '../allergies/allergies.component';
import { SnotifyService } from 'ng-snotify';
import { NotificationService } from '../../_services/notification.service';

@Component({
    selector: 'app-allergies-table',
    templateUrl: './allergies-table.component.html',
    styleUrls: ['./allergies-table.component.css']
})
export class AllergiesTableComponent implements OnInit {
    public allergy_table_data: AllergyTableData[] = [];
    public newAllergyData: AllergyTableData[] = [];

    displayedColumns = ['allergy', 'reactionType', 'severity', 'onsetDate', 'active', 'action'];
    dataSource = new MatTableDataSource(this.allergy_table_data);

    @Output() notify: EventEmitter<AllergyTableData[]> = new EventEmitter<AllergyTableData[]>();

    constructor(private dialog: MatDialog,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService) { }

    ngOnInit() {
        // emit new allergies to stepper 
        this.notify.emit(this.newAllergyData);
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

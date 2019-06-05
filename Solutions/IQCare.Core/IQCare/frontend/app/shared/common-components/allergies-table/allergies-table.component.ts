import { Component, OnInit } from '@angular/core';
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

    displayedColumns = ['allergy', 'reactionType', 'severity', 'onsetDate', 'active', 'action'];
    dataSource = new MatTableDataSource(this.allergy_table_data);

    constructor(private dialog: MatDialog,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService) { }

    ngOnInit() {
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

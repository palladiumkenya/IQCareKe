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
        this.allergy_table_data = [];
        this.prepservice.getPatientAllergies(this.patientId).subscribe(
            (res) => {
                res.forEach(allergyData => {
                    this.allergy_table_data.push({
                        id: allergyData.id,
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


    onRowClicked(row) {
        let id: any;
        console.log(row);


        id = row.id;
        if (parseInt(id, 10) > 0) {
            this.prepservice.DeleteAllergy(id).subscribe((res) => {

                this.snotifyService.success(res['message'].toString(), 'Delete Allergy ', this.notificationService.getConfig());
            },
                (error) => {
                    this.snotifyService.error('Error deleting Allergy ' + error, 'Delete Allergy',
                        this.notificationService.getConfig());

                });


            this.loadPatientAllergies();
           

        } else {
            var idx = this.allergy_table_data.indexOf(row);
            this.allergy_table_data.splice(idx);
            this.dataSource = new MatTableDataSource(this.allergy_table_data);



        }



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
                        onsetDate: data.onSetDate,
                        id: 0
                    });

                    this.newAllergyData.push({
                        allergy: data.substanceAllergy,
                        reactionType: data.allergyReaction,
                        severity: data.severity,
                        onsetDate: data.onSetDate,
                        id: 0
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
    id: any;
}

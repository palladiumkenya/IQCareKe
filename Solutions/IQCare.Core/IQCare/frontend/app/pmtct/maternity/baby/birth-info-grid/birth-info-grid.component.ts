import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { MatTableDataSource } from '@angular/material';
import { MaternityService } from '../../../_services/maternity.service';
import { NotificationService } from '../../../../shared/_services/notification.service';
import { SnotifyService } from 'ng-snotify';

@Component({
  selector: 'app-birth-info-grid',
  templateUrl: './birth-info-grid.component.html',
  styleUrls: ['./birth-info-grid.component.css']
})
export class BirthInfoGridComponent implements OnInit {

  babyData: any[] = [];
  displayedColumns = ['sex', 'birthWeight', 'outcome', 'apgarScore', 'resuscitation', 'deformity', 'teo', 'breastFeeding', 'comment',
      'action'];
  
 @Input() PatientId: number;
 @Input() isEdit: boolean;
 @Input() PatientMasterVisitId: number;
 @Output() notify: EventEmitter<any[]> = new EventEmitter<any[]>();

 dataSource = new MatTableDataSource(this.babyData);
  ngOnInit() 
  {   
    if(this.isEdit)
    {
      this.getDeliveredBabyInfo(this.PatientMasterVisitId)
    }else
    {
      this.maternityService.currentBabyData.subscribe(
        data=> 
        {
          this.babyData = data
          this.dataSource = new MatTableDataSource(this.babyData);
          this.notify.emit(this.babyData);        
        });
    }
  }
  constructor(private maternityService: MaternityService,
                private notificationService: NotificationService,
                private snotifyService: SnotifyService)
               {
                   
               }

    

   public getDeliveredBabyInfo(masterVisitId: number): void {
    this.maternityService.GetDeliveredBabyInfo(masterVisitId)
        .subscribe(
            bInfo => {
                if (bInfo == null) {
                   return;
                }
             bInfo.forEach(info => {
                this.babyData.push({
                    sexStr: info.sex,
                    birthWeight: info.birthWeight,
                    outcomeName: info.deliveryOutcome,
                    apgarScore: info.apgarScores,
                    resuscitateStr: info.resuscitationDone ? 'Yes' : 'No',
                    deformityStr:  info.birthDeformity ? 'Yes' : 'No',
                    teoStr:  info.teoGiven ? 'Yes' : 'No',
                    breastFeedingStr: info.breastFedWithinHour ? 'Yes' : 'No',
                    comment: info.comment,
                    notificationNumber: info.birthNotificationNumber,
                    id: info.id
                });
             });
             this.dataSource = new MatTableDataSource(this.babyData);
            },
            (err) => {
                this.snotifyService.error('Error fetching baby details' + err,
                    'Encounter', this.notificationService.getConfig());
            },
            () => {

            });
}

  

}

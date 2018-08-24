import { Component, OnInit, OnDestroy } from '@angular/core';
import { NotificationService } from './../../shared/_services/notification.service';
import { SnotifyService } from 'ng-snotify';
import { ActivatedRoute } from '@angular/router';
import { VisitDetails } from './../_models/visitDetails'
import { VisitDetailsService } from '../_services/visit-details.service';

@Component({
  selector: 'app-anc',
  templateUrl: './anc.component.html',
  styleUrls: ['./anc.component.css']
})
export class AncComponent implements OnInit, OnDestroy {

    isLinear: true;
    visitDetails: VisitDetails;
    public saveVisitDetails$ ;
    
    constructor(private route: ActivatedRoute,  private visitDetailsService: VisitDetailsService, private snotifyService: SnotifyService,
        private notificationService: NotificationService) {}

  ngOnInit() {

  }

  public onSaveVisitDetails(data: VisitDetails): void  {
    
    this.saveVisitDetails$ = this.visitDetailsService.savePatientDetails(data)
          .subscribe(
              p => {
                  console.log(p);
                  this.snotifyService.success('Visit Details Added Successfully' + p);
              },
              (err) => {
                  console.log(err);
                  this.snotifyService.error('Error Adding VisitDetails' + err, 'VisitDetails service',
                   this.notificationService.getConfig());
              },
              () => {
                  console.log(this.saveVisitDetails$);
              });
  }


    ngOnDestroy(): void {
        if (this.saveVisitDetails$) {
            this.saveVisitDetails$.unsubscribe();
        }
    }
}

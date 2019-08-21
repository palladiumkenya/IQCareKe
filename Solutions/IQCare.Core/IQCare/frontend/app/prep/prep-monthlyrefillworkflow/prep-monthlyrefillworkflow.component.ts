import { Component, OnInit, NgZone } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SnotifyService } from 'ng-snotify';
import { NotificationService } from '../../shared/_services/notification.service';
import { LookupItemView } from '../../shared/_models/LookupItemView';
@Component({
  selector: 'app-prep-monthlyrefillworkflow',
  templateUrl: './prep-monthlyrefillworkflow.component.html',
  styleUrls: ['./prep-monthlyrefillworkflow.component.css']
})
export class PrepMonthlyrefillworkflowComponent implements OnInit {
    public personId = 0;

    patientId: number;
    serviceAreaId: number;
  constructor(
    private route: ActivatedRoute,
    private snotifyService: SnotifyService,
    private notificationService: NotificationService,
    private router: Router,
    public zone: NgZone
  ) { }

  ngOnInit() {
    this.route.params.subscribe(params => {
        this.personId = params['personId'];
        this.patientId = params['patientId'];
       
        this.serviceAreaId = params['serviceId'];
        
        
    });
  }
  RefillLink() {
    this.zone.run(() => {
        this.router.navigate(['/prep/monthlyrefill/' + '/' + this.patientId + '/' + this.personId + '/'
            + this.serviceAreaId],
            { relativeTo: this.route });
    });
  }
  DiscontinuationLink() {
    this.zone.run(() => {
        this.router.navigate(['/prep/prepcareend/' + '/' + this.patientId + '/' + this.personId + '/'
            + this.serviceAreaId],
            { relativeTo: this.route });
    });


    
}


Back() {
    this.zone.run(() => {
        this.zone.run(() => {
            this.router.navigate(
                ['/prep/prepformslist/' + this.patientId + '/' + this.personId + '/' + this.serviceAreaId],
                { relativeTo: this.route });
        });
    });
}

}

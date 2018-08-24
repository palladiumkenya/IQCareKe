import { Component, NgZone, OnInit } from '@angular/core';
import { Linkage } from '../_models/linkage';
import { LinkageReferralService } from '../_services/linkage-referral.service';
import { ActivatedRoute, Router } from '@angular/router';
import { SnotifyService } from 'ng-snotify';
import { NotificationService } from '../../shared/_services/notification.service';

@Component({
    selector: 'app-linkage',
    templateUrl: './linkage.component.html',
    styleUrls: ['./linkage.component.css']
})
export class LinkageComponent implements OnInit {
    linkage: Linkage;
    isEdit: boolean = false;
    public cccPattern = /^((?!(0))[0-9]{10})$/;

    constructor(private linkageService: LinkageReferralService,
        private router: Router,
        private route: ActivatedRoute,
        public zone: NgZone,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService) { }

    ngOnInit() {
        this.linkage = new Linkage();

        this.getPreviousLinkage();
    }

    onSubmit() {
        this.linkage.personId = JSON.parse(localStorage.getItem('personId'));
        this.linkage.userId = JSON.parse(localStorage.getItem('appUserId'));
        this.linkage.IsEdit = this.isEdit;


        console.log(this.linkage);
        this.linkageService.addLinkage(this.linkage).subscribe(data => {
            console.log(data);
            this.snotifyService.success('Successfully saved linkage', 'Linkage', this.notificationService.getConfig());
            this.zone.run(() => { this.router.navigate(['/registration/home'], { relativeTo: this.route }); });
        }, err => {
            console.log(`error`);
            this.snotifyService.error('Error saving linkage', 'Referral', this.notificationService.getConfig());
        });
    }

    getPreviousLinkage() {
        const personId = JSON.parse(localStorage.getItem('personId'));

        this.linkageService.getPersonLinkage(personId).subscribe(
            (res) => {
                console.log(res);
                for (let i = 0; i < res.length; i++) {
                    this.linkage.carde = res[i].cadre;
                    this.linkage.cccNumber = res[i].cccNumber;
                    this.linkage.dateEnrolled = res[i].linkageDate;
                    this.linkage.facility = res[i].facility;
                    this.linkage.healthworker = res[i].healthWorker;
                    this.linkage.id = res[i].id;

                    this.isEdit = true;
                    // this.linkage.remarks = res[i].cadre;
                }
            },
            (error) => {
                this.snotifyService.error('Failed to fetch previous linkage', 'Linkage', this.notificationService.getConfig());
            }
        );
    }
}

import {Component, NgZone, OnInit} from '@angular/core';
import {Linkage} from '../_models/linkage';
import {LinkageReferralService} from '../_services/linkage-referral.service';
import {ActivatedRoute, Router} from '@angular/router';

@Component({
  selector: 'app-linkage',
  templateUrl: './linkage.component.html',
  styleUrls: ['./linkage.component.css']
})
export class LinkageComponent implements OnInit {
    linkage: Linkage;

    constructor(private linkageService: LinkageReferralService,
                private router: Router,
                private route: ActivatedRoute,
                public zone: NgZone) { }
    ngOnInit() {
        this.linkage = new Linkage();
    }

    onSubmit() {
        this.linkage.personId = JSON.parse(localStorage.getItem('personId'));
        this.linkage.userId = JSON.parse(localStorage.getItem('appUserId'));

        console.log(this.linkage);
        this.linkageService.addLinkage(this.linkage).subscribe(data => {
            console.log(data);
            this.zone.run(() => { this.router.navigate(['/registration/home'], { relativeTo: this.route }); });
        }, err => {
           console.log(`error`);
        });
    }
}

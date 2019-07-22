import { Component, OnInit, Input } from '@angular/core';
import { ClientService } from '../_services/client.service';
import { PartnerView } from '../../hts/_models/pnsform';

@Component({
    selector: 'app-personbrief',
    templateUrl: './personbrief.component.html',
    styleUrls: ['./personbrief.component.css']
})
export class PersonbriefComponent implements OnInit {
    partnerId: number;
    partnerView: PartnerView;

    @Input() personId: any;

    constructor(private clientService: ClientService) { }

    ngOnInit() {
        if (this.personId > 0) {
            this.partnerId = this.personId;
        } else {
            this.partnerId = JSON.parse(localStorage.getItem('partnerId'));
        }

        this.partnerView = new PartnerView();

        this.getPartnerDetails();
    }

    getPartnerDetails() {
        this.clientService.getPersonDetails(this.partnerId).subscribe(res => {
            // console.log(this.partnerId + ' Partner Id');
            // console.log(res + ' Response Person');
            this.partnerView.fullName = res['firstName'] + ' ' + res['midName'] + ' ' + res['lastName'];
            this.partnerView.DateOfBirth = res['dateOfBirth'];
            this.partnerView.Gender = res['gender'];
            this.partnerView.MaritalStatus = res['maritalStatusName'];
        });
    }

}

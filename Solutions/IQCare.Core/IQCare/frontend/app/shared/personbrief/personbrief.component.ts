import { Component, OnInit } from '@angular/core';
import {ClientService} from '../_services/client.service';
import {PartnerView} from '../../hts/_models/pnsform';

@Component({
  selector: 'app-personbrief',
  templateUrl: './personbrief.component.html',
  styleUrls: ['./personbrief.component.css']
})
export class PersonbriefComponent implements OnInit {
    partnerId: number;
    partnerView: PartnerView;

    constructor(private clientService: ClientService) { }

    ngOnInit() {
        this.partnerId = JSON.parse(localStorage.getItem('partnerId'));
        this.partnerView = new PartnerView();

        this.getPartnerDetails();
    }

    getPartnerDetails() {
        this.clientService.getPersonDetails(this.partnerId).subscribe(res => {
            // console.log(res);
            // this.clientBrief = res['patientLookup'][0];
            this.partnerView.fullName = res['firstName'] + ' ' + res['midName'] + ' ' + res['lastName'];
            this.partnerView.DateOfBirth = res['dateOfBirth'];
            this.partnerView.Gender = res['gender'];
            // console.log(this.clientBrief);
        });
    }

}

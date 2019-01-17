import { Component, OnInit } from '@angular/core';
import { ClientService } from '../_services/client.service';
import { ClientBrief } from '../_models/ClientBrief';

@Component({
    selector: 'app-clientbrief',
    templateUrl: './clientbrief.component.html',
    styleUrls: ['./clientbrief.component.css']
})
export class ClientbriefComponent implements OnInit {
    patientId: number;
    serviceAreaId: number;
    clientBrief: ClientBrief;

    constructor(private clientService: ClientService) { }
    ngOnInit() {
        this.patientId = JSON.parse(localStorage.getItem('patientId'));
        this.serviceAreaId = 2;

        this.clientBrief = new ClientBrief();
        this.getClientDetails();
    }

    getClientDetails() {

        this.clientService.getClientDetails(this.patientId, this.serviceAreaId).subscribe(res => {
            // console.log(res);
            this.clientBrief = res['patientLookup'][0];
            this.clientBrief.fullName = this.clientBrief.firstName + ' ' + this.clientBrief.midName + ' ' + this.clientBrief.lastName;
        });
    }
}

import { Component, OnInit } from '@angular/core';
import {Pnsform} from '../_models/pnsform';
import {PnsService} from '../_services/pns.service';

@Component({
  selector: 'app-pnsform',
  templateUrl: './pnsform.component.html',
  styleUrls: ['./pnsform.component.css']
})
export class PnsformComponent implements OnInit {
    pnsForm: Pnsform;
    yesNoNAOptions: any[];
    yesNoOptions: any[];
    ipvOutcomeOptions: any[];
    yesNoDeclinedOptions: any[];
    pnsRelationshipOptions: any[];
    hivStatusOptions: any[];
    pnsApproachOptions: any[];

    constructor(private pnsService: PnsService) { }

    ngOnInit() {
        this.pnsForm = new Pnsform();

        this.getPnsOptions();
    }

    public getPnsOptions() {
        this.pnsService.getCustomOptions().subscribe(data => {
            console.log(data);
            const options = data['lookupItems'];
            for (let i = 0; i < options.length; i++) {
                // console.log(options[i]);
                if (options[i].key == 'YesNoNA') {
                    this.yesNoNAOptions = options[i].value;
                } else if (options[i].key == 'YesNo') {
                    this.yesNoOptions = options[i].value;
                } else if (options[i].key == 'IpvOutcome') {
                    this.ipvOutcomeOptions = options[i].value;
                } else if (options[i].key == 'YesNoDeclined') {
                    this.yesNoDeclinedOptions = options[i].value;
                } else if (options[i].key == 'PNSRelationship') {
                    this.pnsRelationshipOptions = options[i].value;
                } else if (options[i].key == 'HivStatus') {
                    this.hivStatusOptions = options[i].value;
                } else if (options[i].key == 'PnsApproach') {
                    this.pnsApproachOptions = options[i].value;
                }
            }
        }, err => {
           console.log(err);
        });
    }

    public onSubmit() {
        console.log(this.pnsForm);
    }
}

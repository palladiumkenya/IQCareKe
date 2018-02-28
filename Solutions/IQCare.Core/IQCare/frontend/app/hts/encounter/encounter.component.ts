import { Component, Input, OnInit } from '@angular/core';
import {Encounter} from '../_models/encounter';
import {FormGroup} from '@angular/forms';
import {EncounterService} from '../_services/encounter.service';

declare var $: any;

@Component({
  selector: 'app-encounter',
  templateUrl: './encounter.component.html',
  styleUrls: ['./encounter.component.css']
})
export class EncounterComponent implements OnInit {
    encounter: Encounter;

    constructor(private _encounterService: EncounterService) {
    }

    ngOnInit() {
        this.encounter = new Encounter();
        var self = this;

        setTimeout(() => {
            $("#myWizard").on("actionclicked.fu.wizard", function(evt, data) {
                var currentStep = data.step;
                var nextStep = 0;
                var previousStep = 0;

                if (data.direction === 'next')
                    nextStep=currentStep += 1;
                else
                    previousStep=nextStep -= 1;
                if (data.step === 1) {
                    if (data.direction === 'previous') {
                        return;
                    } else {
                        if($('#datastep1').parsley().validate()){
                            self.onSubmitForm();
                        }else{
                            evt.preventDefault();
                            return;
                        }
                        console.log();
                    }
                }
                else if (data.step === 2) {
                    if (data.direction === 'previous') {
                        return;
                    } else {
                        console.log(2);
                    }
                }
            })
                .on("changed.fu.wizard",function() {})
                .on('stepclicked.fu.wizard',function() {})
                .on('finished.fu.wizard',function(e) {});
        },0);
    }

    onSubmitForm(){
        this._encounterService.addEncounter(this.encounter).subscribe(data=>{console.log(data);}, err=>{ console.log(err);});
    }
}

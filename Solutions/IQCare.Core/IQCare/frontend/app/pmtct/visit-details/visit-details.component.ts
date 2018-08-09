import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {Subscription} from 'rxjs';
import {LookupItemService} from "../../shared/_services/lookup-item.service";
import {NotificationService} from "../../shared/_services/notification.service";
import { SnotifyService } from 'ng-snotify';
import {LookupItemView} from "../../shared/_models/LookupItemView";
import {Lookups} from "../../shared/_models/Lookups";

@Component({
  selector: 'app-visit-details',
  templateUrl: './visit-details.component.html',
  styleUrls: ['./visit-details.component.css']
})
export class VisitDetailsComponent implements OnInit {
    visitDetailsFormGroup: FormGroup;
    isLinear: true;
    entryPoints: any[];
    lookupItemView$: Subscription;
    public ancVisitTypes: any[]=[];
    constructor(private fb: FormBuilder,private _lookupItemService: LookupItemService,
    private snotifyService: SnotifyService,
    private notificationService: NotificationService) {

  }

  ngOnInit() {
    this.visitDetailsFormGroup = this.fb.group({
        visitDate: ['', Validators.required],
        ancVisitType: ['', Validators.required],
        dateLMP: ['', Validators.required],
        dateEDD: ['', Validators.required],
        ancVisitNumber: ['', Validators.required],
        gestation: ['', Validators.required],
        ageAtMenarche: ['', Validators.required],
        parityOne: ['', Validators.required],
        parityTwo: ['', Validators.required],
        gravidae: ['', Validators.required]
    });

    this.getANCVisits('ANCVisitType');


  }

    public getANCVisits(groupName: string) {
        this.lookupItemView$ = this._lookupItemService.getByGroupName(groupName)
            .subscribe(
                p => {
                    const options = p['lookupItems'];
                    for (let i = 0; i < options.length; i++) {
                        // console.log(options[i]);
                        if (options[i].key == 'lookupItems') {
                            this.entryPoints = options[i].value;
                        }
                    }
                    console.log(options);
                    for(let i=0; i<options.length; i++){
                        this.ancVisitTypes.push({"itemId":options[i]['itemId'],"itemName": options[i]['itemName']});
                    }
                    console.log(options[0]['itemName']);
                    console.log(this.ancVisitTypes);
                },
                (err) => {
                    console.log(err);
                    this.snotifyService.error('Error editing encounter ' + err, 'Encounter', this.notificationService.getConfig());
                },
                () => {
                    console.log(this.lookupItemView$);
                });
    }

  test(lmp: Date) {
  }

  test2() {
    console.log(this.visitDetailsFormGroup.value);
  }
}

import { filter } from 'rxjs/operators';
import { LookupItemView } from './../../../shared/_models/LookupItemView';
import { HeiService } from './../../_services/hei.service';
import { Component, OnInit, Input } from '@angular/core';
import { NotificationService } from '../../../shared/_services/notification.service';
import { SnotifyService } from 'ng-snotify';
import {DataService} from '../../_services/data.service';
import * as moment from 'moment';

@Component({
    selector: 'app-hei-messages',
    templateUrl: './hei-messages.component.html',
    styleUrls: ['./hei-messages.component.css']
})
export class HeiMessagesComponent implements OnInit {
    isPCRDone: boolean = false;
    isConfirmatoryPCRDone: boolean = false;
    isPCR_Positive: boolean = false;
    isBaselineVLDone: boolean = false;
    heiResultsString: string = '';
    maternalLastViralLoad: string = '';

    @Input() patientId: any;
    @Input() heiHivTestingOptions: LookupItemView[];
    @Input() heiHivTestingResultsOptions: LookupItemView[];

    constructor(private heiservice: HeiService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private dataservice: DataService) {
    }

    async ngOnInit() {
        this.loadPatientCompletedTestTypes();

        this.dataservice.labDone.subscribe(labDone => {
            this.loadPatientCompletedTestTypes();
        });

        this.dataservice.motherId.subscribe( async motherId => {
            if (motherId && motherId > 0) {
                const newVar = await this.loadMaternalLastViralLoad(motherId);
            }
        });
    }

    async loadMaternalLastViralLoad(motherId: number) {
        const patient = await this.heiservice.getMotherPatientId(motherId).toPromise();
        if (patient) {
            const viralLoads = await this.heiservice.getMaternalViralLoad(patient.id).toPromise();
            this.maternalLastViralLoad = '';
            if (viralLoads['patientViralLoad'].length > 0) {
                for (let i = 0; i < viralLoads['patientViralLoad'].length; i++) {
                    if (viralLoads['patientViralLoad'][i]['orderstatus'] == 'Complete') {
                        this.maternalLastViralLoad = 'MATERNAL VIRAL LOAD => Complete | Results : '
                            + viralLoads['patientViralLoad'][i]['resultvalue']
                            + ' copies/ml. ResultDate: ' + moment(viralLoads['patientViralLoad'][i]['resultDate']).format('DD-MMM-YYYY');
                        break;
                    }
                }
            }
        }
    }

    loadPatientCompletedTestTypes(): void {
        this.heiservice.getPatientHeiLabTestsTypes(this.patientId).subscribe(
            (res) => {
                this.loadHeiHivTests(res);
            },
            (error) => {
                this.snotifyService.error('Failed to load hei lab tests ', 'HEI',
                    this.notificationService.getConfig());
            }
        );
    }

    loadHeiHivTests(heiLabTests: any[]): void {
        this.heiservice.getLabOrderTestResults(this.patientId).subscribe(
            (res) => {
                this.heiResultsString = '';
                for (let i = 0; i < res.length; i++) {
                    const savedHeiLabTests = heiLabTests.filter(obj => obj.labOrderId == res[i].labOrderId);
                    let testType;
                    if (savedHeiLabTests.length > 0) {
                        testType = this.heiHivTestingOptions.filter(obj => obj.itemId == savedHeiLabTests[0]['heiLabTestTypeId']);
                    } else {
                        testType = this.heiHivTestingOptions.filter(obj => obj.itemName.includes(res[i].labTestName));
                    }

                    if ((testType[0]['itemName'] == '1st DNA PCR'
                        || testType[0]['itemName'] == '2nd DNA PCR'
                        || testType[0]['itemName'] == '3rd DNA PCR') && res[i].result != null) {
                        this.isPCRDone = true;
                        const pcrRes = res[i].result.toString();
                        if (pcrRes == 'Positive') {
                            this.isPCR_Positive = true;
                        }
                    }

                    if (testType[0]['itemName'] != null && res[i].result != null) {
                        this.heiResultsString += testType[0]['itemName'] + '  => ' + res[i].result + '<br/>';
                    }

                    if (testType[0]['itemName'] == 'Confirmatory PCR (for  +ve)' && res[i].result != null) {
                        this.isConfirmatoryPCRDone = true;
                    }

                    if (testType[0]['itemName'] == 'Baseline Viral Load (for +ve)' && res[i].result != null) {
                        this.isBaselineVLDone = true;
                    }
                }
            }
        );
    }
}

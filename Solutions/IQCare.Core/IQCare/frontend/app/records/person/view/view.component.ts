import { RecordsService } from './../../_services/records.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '../../../../../node_modules/@angular/router';
import { PersonDetails } from '../../_models/persondetails';
import { SnotifyService } from '../../../../../node_modules/ng-snotify';
import { NotificationService } from '../../../shared/_services/notification.service';

@Component({
    selector: 'app-view',
    templateUrl: './view.component.html',
    styleUrls: ['./view.component.css']
})
export class ViewComponent implements OnInit {
    personId: number;

    firstName: string = '';
    middleName: any;
    lastName: any;
    gender: any;
    maritalStatus: any;
    occupation: any;
    educationLevel: any;
    county: any;
    subCounty: any;
    ward: any;
    village: any;
    nearestHealthCentre: any;
    dateOfBirth: any;
    dobPrecision: any;
    mobileNumber: any;
    alternativeNumber: any;
    emailAddress: any;

    constructor(private route: ActivatedRoute,
        private recordsService: RecordsService,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService) { }

    ngOnInit() {
        this.route.params.subscribe(params => {
            this.personId = params['id'];
        });

        this.getPersonDetails(this.personId);
    }

    public getPersonDetails(personId: number) {
        this.recordsService.getPersonDetails(personId).subscribe(
            (res) => {
                console.log(res);
                const { id, firstName, middleName, lastName, gender,
                    maritalStatus, educationLevel, occupation, county,
                    subCounty, ward, village, nearestHealthCentre, dateOfBirth,
                    dobPrecision, mobileNumber, alternativeNumber, emailAddress } = res[0];

                this.firstName = firstName;
                this.middleName = middleName;
                this.lastName = lastName;
                this.gender = gender;
                this.maritalStatus = maritalStatus;
                this.educationLevel = educationLevel;
                this.occupation = occupation;
                this.county = county;
                this.subCounty = subCounty;
                this.ward = ward;
                this.village = village;
                this.nearestHealthCentre = nearestHealthCentre;
                this.dateOfBirth = dateOfBirth;
                this.dobPrecision = dobPrecision;
                this.mobileNumber = mobileNumber;
                this.alternativeNumber = alternativeNumber;
                this.emailAddress = emailAddress;
            },
            (error) => {
                this.snotifyService.error('Error viewing person ' + error, 'View Person', this.notificationService.getConfig());
            },
            () => {

            }
        );
    }
}

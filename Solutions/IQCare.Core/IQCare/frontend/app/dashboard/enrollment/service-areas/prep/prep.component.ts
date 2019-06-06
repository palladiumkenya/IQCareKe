
import { FormControlService } from './../../../../shared/_services/form-control.service';
import { PersonHomeService } from './../../../services/person-home.service';
import { LookupItemView } from './../../../../shared/_models/LookupItemView';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { Component, OnInit, NgZone } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { LookupItemService } from '../../../../shared/_services/lookup-item.service';
import { NotificationService } from '../../../../shared/_services/notification.service';
import { SnotifyService } from 'ng-snotify';
import { forkJoin } from 'rxjs';
import { RegistrationService } from '../../../../registration/_services/registration.service';
import { PersonPopulation } from '../../../../registration/_models/personPopulation';
import { ServiceEntryPointCommand } from '../../../_model/ServiceEntryPointCommand';
import { EnrollmentService } from '../../../../registration/_services/enrollment.service';
import { Store } from '@ngrx/store';
import * as Consent from '../../../../shared/reducers/app.states';
import { AppEnum } from '../../../../shared/reducers/app.enum';
import { AppStateService } from '../../../../shared/_services/appstate.service';
import { Enrollment } from '../../../../registration/_models/enrollment';
import { SearchService } from '../../../../registration/_services/search.service';
import { RecordsService } from '../../../../records/_services/records.service';

@Component({
  selector: 'app-prep',
  templateUrl: './prep.component.html',
  styleUrls: ['./prep.component.css']
})
export class PrepComponent implements OnInit {
    form: FormGroup;
    personId: number;
    patientId: number;
    serviceId: number;
    serviceCode: string;
    userId: number;
    posId: string;
    isEdit: boolean = false;
    maxDate: Date;
    minDate: Date;
    personPopulation: PersonPopulation;

    keyPops: LookupItemView[] = [];
    patientTypes: LookupItemView[] = [];
    yesNoOptions: LookupItemView[] = [];
    serviceAreaIdentifiers: any[] = [];
    identifiers: any[] = [];
  constructor(private route: ActivatedRoute,
    private router: Router,
    public zone: NgZone,
    private _formBuilder: FormBuilder,
    private _lookupItemService: LookupItemService,
    private personHomeService: PersonHomeService,
    private snotifyService: SnotifyService,
    private notificationService: NotificationService,
    private registrationService: RegistrationService,
    private enrollmentService: EnrollmentService,
    private store: Store<AppState>,
    private appStateService: AppStateService,
    private searchService: SearchService,
    private recordsService: RecordsService) {
    this.maxDate = new Date(); }

  ngOnInit() {

    this.route.params.subscribe(params => {
        const { id, serviceId, serviceCode, edit } = params;
        this.personId = id;
        this.serviceId = serviceId;
        this.serviceCode = serviceCode;
       

        if (edit == 1) {
            this.isEdit = true;
        }  this.userId = JSON.parse(localStorage.getItem('appUserId'));
        this.posId = localStorage.getItem('appPosID');
        this.personPopulation = new PersonPopulation();



    });
  }

}

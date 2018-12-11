import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { LabTestInfo, AddLabOrderCommand } from '../../_models/AddLabOrderCommand';
import { LaborderService } from '../../_services/laborder.service';
import { SnotifyService } from 'ng-snotify';
import { NotificationService } from '../../../shared/_services/notification.service';
import { ActivatedRoute } from '@angular/router';
import { PersonHomeService } from '../../../dashboard/services/person-home.service';
import { EncounterService } from '../../../shared/_services/encounter.service';
import { LookupItemService } from '../../../shared/_services/lookup-item.service';
import { AddPatientOrdVisitCommand } from '../../../shared/_models/patientordvisit';
import { PatientMasterVisitEncounter } from '../../../pmtct/_models/PatientMasterVisitEncounter';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-lab-order',
  templateUrl: './lab-order.component.html',
  styleUrls: ['./lab-order.component.css']
})
export class LabOrderComponent implements OnInit {

labOrderFormGroup : FormGroup;
labTestData : any[];
patientId : any;
patientInfo : any;
userId : any;
facilityId : any;
ordVisitId : any;
patientMasterVisitId : any;
encounterType : any;
serviceAreaId : any;
@Input() labTestReasons: any[] = [];
@Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();
@Output() notifyData: EventEmitter<any[]> = new EventEmitter<any[]>();


labTestReasonOptions : any[];

  constructor(private formBuilder :FormBuilder, 
    private labOrderService: LaborderService,
    private personService : PersonHomeService,
    private encouterService : EncounterService,
    private lookUpService : LookupItemService,
    private  snotifyService : SnotifyService,
    private notificationService: NotificationService,
    private activatedRoute : ActivatedRoute) {

     this.labOrderFormGroup = this.formBuilder.group({
      labTestId: new FormControl('', [Validators.required]),
      labtestReasonId: new FormControl('', [Validators.required]),
      labTestNotes: new FormControl('', [Validators.required]),
      orderDate: new FormControl('', [Validators.required]),
      clinicalOrderNotes: new FormControl('', [Validators.required])
    });
    this.notify.emit(this.labOrderFormGroup);
    this.notifyData.emit(this.labTestData);
   }

  ngOnInit() {
    this.labTestReasonOptions = this.labTestReasons;
    this.userId = JSON.parse(localStorage.getItem('appUserId'));

    this.activatedRoute.params.subscribe(params => {
      this.patientId = params['patientId'];
      this.personService.getPatientById(this.patientId).subscribe(patient => {
         this.patientInfo = patient;
     });   
     });

    this.getEncounterType();
    this.getFacilty();
    this.getServiceArea();  
  }

 
  public  AddLabTest() {
    this.labTestData.push({
      testId: this.labOrderFormGroup.get('labTestId').value.id,
      test: this.labOrderFormGroup.get('labTestId').value.itemName,
      orderReason: this.labOrderFormGroup.get('labtestReasonId').value.itemName,
      orderReasonId: this.labOrderFormGroup.get('labtestReasonId').value.itemId,
      testNotes: this.labOrderFormGroup.get('labTestNotes').value
    });
  }



  public SubmitOrder() {
    if(this.labOrderFormGroup.invalid)
        return;

        const ordVisitCommand = this.buildOrdVisitCommand();
        const submitOrdVisit = this.encouterService.savePatientOrdVisit(ordVisitCommand); 

        const patientEncounter = this.buildPatientEncounterCommand();
        const submitPatientEncounter = this.encouterService.savePatientMasterVisit(patientEncounter);

        forkJoin([
          submitOrdVisit,
          submitPatientEncounter
        ])
        .subscribe(
            (result) => {

              labOrderCommand.VisitId = result[0]['Visit_Id'];
              labOrderCommand.PatientMasterVisitId = result[1]['patientMasterVisitId'];
              
              this.labOrderService.addLabOrder(labOrderCommand).subscribe(
                res=>{
                  console.log(`Add lab order info`);
                  console.log(res);
                  this.snotifyService.success('Successfully added lab order ', 'Lab', this.notificationService.getConfig());
              }, 
              (err)=>
              {
                console.log("An error occured while adding lab order command "+err);
                this.snotifyService.error('Error saving lab order ' + err, 'Lab', this.notificationService.getConfig());
              },
              ()=>{
                this.snotifyService.success('Lab order details added successfully', 'Lab',
                this.notificationService.getConfig());
                this.labOrderFormGroup.reset();
              });
  
            },
            (error) => {
                console.log(`error ` + error);
            },
            () => {
                console.log(`complete`);
            }
        );

      var labOrderCommand = this.buildLabOrderCommand();
        
      

     }

     public buildLabOrderCommand() : AddLabOrderCommand {
      const labTestInfo : LabTestInfo [] = [];   

       this.labTestData.forEach(x=>
      {
        labTestInfo.push({
          Id : x.testId,
          Notes : x.testNotes,
          LabTestName :x.test
        })
      });
  
     const labOrderCommand : AddLabOrderCommand = {
        Ptn_Pk: this.patientInfo.ptn_Pk,
        PatientId : this.patientInfo.patientId,
        LocationId : this.facilityId,
        VisitId : null,
        Module : "Laboratory",
        OrderedBy : this.userId,
        OrderDate : this.labOrderFormGroup.get('orderDate').value,
        ClinicalOrderNotes :  this.labOrderFormGroup.get('orderNotes').value,
        CreateDate :new Date,
        OrderStatus : "Pending",
        UserId : this.userId,
        PatientMasterVisitId : null,
        LastTests : this.labTestData
        }

        return labOrderCommand;
  
    }

  
    public buildOrdVisitCommand() : AddPatientOrdVisitCommand {
      const ordVisitCommand : AddPatientOrdVisitCommand ={
        Ptn_Pk : this.patientInfo.ptn_Pk,
        LocationId : this.facilityId,
        UserId : this.userId,
        VisitDate : new Date()
     }
     return ordVisitCommand;
    }

    /**
     * buildPatientEncounterCommand
     */
    public buildPatientEncounterCommand() :PatientMasterVisitEncounter {
      const encounterVisit : PatientMasterVisitEncounter = {
         EncounterDate : new Date,
         EncounterType : this.encounterType,
         PatientId : this.patientId,
         ServiceAreaId : this.serviceAreaId,
         UserId : this.userId
      }
      return encounterVisit;
    }

    public getFacilty() {
      this.lookUpService.getActiveFacility().subscribe(result=>{
        this.facilityId = result.id;
    });
    }
  
    
    public getServiceArea() {
      this.personService.getServiceArea("Clinical").subscribe(result=>{
        this.serviceAreaId = result.id;
    });
    }
  
    
    public getEncounterType() {
      this.lookUpService.getByGroupNameAndItemName("EncounterType","Lab-encounter").subscribe(result=>{
        this.encounterType = result.ItemId;
    });
    }

}



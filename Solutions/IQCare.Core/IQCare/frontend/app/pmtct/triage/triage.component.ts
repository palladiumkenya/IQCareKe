import { Component, OnInit, Output, EventEmitter, Input, ViewChild, NgZone } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { TriageService } from '../_services/triage.service';
import { AddPatientVitalCommand  } from "../_models/AddPatientVitalCommand";
import { MatTableDataSource, MatPaginator } from '@angular/material';
import { SnotifyService } from 'ng-snotify';
import { NotificationService } from '../../shared/_services/notification.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-triage',
  templateUrl: './triage.component.html',
  styleUrls: ['./triage.component.css']
})
export class TriageComponent implements OnInit {
  @Input('PatientId') PatientId: number;
  @Input('PatientMasterVisitId') PatientMasterVisitId: number;

 vitalsDataTable : any [] = [];
 displayedColumns = ['height', 'weight', 'bmi', 'muac', 'diastolic', 'systolic', 'temperature', 'respiratoryrate', 'heartrate',
        'action'];
 dataSource = new MatTableDataSource(this.vitalsDataTable);
 @ViewChild(MatPaginator) paginator: MatPaginator;


 vitalsFormGroup : FormGroup;
 @Output() notify: EventEmitter<FormGroup> = new EventEmitter<FormGroup>();

  constructor(private _formBuilder: FormBuilder, private triageService:TriageService,
    private snotifyService: SnotifyService,
    private notificationService: NotificationService,
    public zone: NgZone,
    private router: Router,private route:ActivatedRoute) { }

  ngOnInit() {
    this.vitalsFormGroup = this.BuildVitalsFormGroup();
    this.notify.emit(this.vitalsFormGroup);
    this.getPatientVitalsInfo(21);
  }


  public BuildVitalsFormGroup() : FormGroup {
    return  this._formBuilder.group({
      visitDate: new FormControl('',[Validators.required]),
      height : new FormControl('', [ Validators.required,Validators.max(270)]),
      weight : new FormControl('',[Validators.required, Validators.max(600)]),
      bmi : new FormControl({value:0, disabled:true},[Validators.required]),
      headCircumference : new FormControl('',[Validators.required]),
      muac : new FormControl('',[Validators.required]),
      weightForAge : new FormControl('',[Validators.required]),
      weightForHeight : new FormControl('',[Validators.required]),
      bmiZ : new FormControl('',[Validators.required]),
      bpDiastolic : new FormControl('',[Validators.required]),
      bpSystolic : new FormControl('',[Validators.required]),
      temperature : new FormControl('',[Validators.required]),
      respiratoryRate : new FormControl('',[Validators.required]),
      heartRate : new FormControl('',[Validators.required]),
      spo2:new FormControl('',[Validators.required]),
      comment : new FormControl('',[Validators.required])
    });
  }

 

  public SubmitPatientVitalInfo() {
    if(this.vitalsFormGroup.invalid)
      return;
   const patientVitalCommand : AddPatientVitalCommand ={
    PatientId: 5, //this.PatientId,
    PatientmasterVisitId: 21, // this.PatientMasterVisitId,
    Temperature: this.vitalsFormGroup.get("temperature").value,
    RespiratoryRate: this.vitalsFormGroup.get("respiratoryRate").value,
    HeartRate: this.vitalsFormGroup.get("heartRate").value,
    BpDiastolic:this.vitalsFormGroup.get("bpDiastolic").value,
    BpSystolic:this.vitalsFormGroup.get("bpSystolic").value,
    Height: this.vitalsFormGroup.get("height").value,
    Weight:this.vitalsFormGroup.get("weight").value,
    Spo2:this.vitalsFormGroup.get("spo2").value,
    Bmi: this.vitalsFormGroup.get("bmi").value,
    HeadCircumference: this.vitalsFormGroup.get("headCircumference").value,
    BmiZ: this.vitalsFormGroup.get("bmiZ").value,
    WeightForAge: this.vitalsFormGroup.get("weightForAge").value,
    WeightForHeight: this.vitalsFormGroup.get("weightForHeight").value,
    Comment:this.vitalsFormGroup.get("comment").value,
    Muac : this.vitalsFormGroup.get("muac").value,
    VisitDate : this.vitalsFormGroup.get("visitDate").value
    }

    this.triageService.AddPatientVitalInfo(patientVitalCommand).subscribe(res=>{
      console.log(`Add Patient Vital info`);
      console.log(res);
    },(err)=>{

    },
    ()=>{
      this.snotifyService.success('Patient vitals information added sucessfully', 'Triage',
      this.notificationService.getConfig());
     this.zone.run(() => {
        this.router.navigate(['/pmtct/triage'], {relativeTo: this.route});
     });
    });
  }
 
  public calculateBmi() {
     var bmi = this.triageService.calculateBmi(this.vitalsFormGroup.get("weight").value,
     this.vitalsFormGroup.get("height").value);
    
     this.vitalsFormGroup.controls["bmi"].setValue(bmi.toFixed(2));  
  }

 
  public getPatientVitalsInfo(masterVisitId: number) {
    this.triageService.GetPatientVitalsInfo(masterVisitId).subscribe(res=>{
      if(res == null)
        return;

       res.forEach(info => {
         this.vitalsDataTable.push({
          height : info.height,
          weight : info.weight,
          bmi : info.bmi,
          headCircumference : info.headCircumference,
          muac : info.muac,
          weightForAge : info.weightForAge,
          weightForHeight : info.weightForHeight,
          bmiZ : info.bmiZ,
          diastolic : info.bpDiastolic,
          systolic : info.bpSystolic,
          temperature :info.temperature,
          respiratoryRate : info.respiratoryRate,
          heartRate : info.heartRate,
          spo2: info.spo2,
          comment : info.comment
         });

         this.dataSource = new  MatTableDataSource(this.vitalsDataTable);
         this.dataSource.paginator = this.paginator;

       });

    },(err)=>{
       console.log(err + " An error occured while getting patient vitals info")

    },()=>{

    })};


  }

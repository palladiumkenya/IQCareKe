import { Component, OnInit, NgZone, ViewChild, Directive } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { SnotifyService, SnotifyPosition } from 'ng-snotify';
import { NotificationService } from '../../shared/_services/notification.service';
import { RouterInitializer } from '@angular/router/src/router_module';
import { IndicatorQuestionBase } from '../_model/indicatorquestion-base';
import { NativeDateAdapter, DateAdapter, MatDatepicker } from '@angular/material';
import { Section, Form, SubSection, FormResults } from '../_model/Sectionidentifier';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators, FormArray } from '@angular/forms';
import * as _ from 'lodash';
import * as moment from 'moment';
import {FormDetailsService} from '../_services/formdetails.service';
import { NgxSpinnerService } from 'ngx-spinner';



@Component({
    selector: 'app-active-form-report',
    templateUrl: './active-form-report.component.html',
    styleUrls: ['./active-form-report.component.css']
})


export class ActiveFormReportComponent implements OnInit {


    @ViewChild(MatDatepicker) picker;
    date = new FormControl();
    IndicatorQuestions: IndicatorQuestionBase[] = [];
    ExistingData: any[] = [];
    Sections: Section[];
    Forms: Form[];
    total: number;
    IndicatorResults:any[];
    SubSections: SubSection[] = [];
    FormItems: [] = [];
    ControlType: string;
    ItemValue: string;
    ItemKey: string;
    formGroup: FormGroup;
    FormResults: FormResults;
    Indicators: FormArray;
    filteritems: IndicatorQuestionBase[];
    filterSubSection: SubSection[];
    filtervalue: boolean;
    numericnumber: number;
    resultvalue: string;
    ReportingFormId: number;
    existing:[]
    constructor(private route: ActivatedRoute,
        private formBuilder: FormBuilder,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private formdetailservice:FormDetailsService,
        public zone: NgZone,
        private router: Router,
        private spinner: NgxSpinnerService ) {
        this.Forms = [];
        this.ExistingData = [];
        this.Sections = [];
        this.IndicatorQuestions = [];
        this.SubSections = [];
        this.FormItems = [];
        this.ControlType = "";
        this.ItemValue = "";
        this.ItemKey = "";
        this.FormResults = new FormResults();
        this.filteritems = [];
        this.filterSubSection = [];
        this.total = 0;
        this.filtervalue = false;
        this.IndicatorResults=[];
        this.existing=[];
        
        //this.indicators=new FormArray([]);
        //const controls=this.IndicatorQuestions.map(c=>new FormControl(false));

    }
    monthSelected(params) {
        this.date.setValue(params);

        const datevalue = moment(params).toDate();
        this.FormResults.Period = moment(moment(datevalue).format('DD-MMM-YYYY')).toDate();
        this.picker.close();
    }

    ngOnInit() {

        this.route.data.subscribe(res => {
            console.log(res);
            const { FormDetails } = res;
            this.FormItems = FormDetails
        });
        console.log(this.FormItems);

        this.route.params.subscribe(params => {
            this.ReportingFormId = params['reportingformid'];

        });
       
        let Form = {
            FormId: this.FormItems['id'],
            FormName: this.FormItems['name']
        }




        console.log(Form);
        this.Forms.push(Form);

        console.log("form identificatioon");
        console.log(this.Forms);

        console.log(this.FormItems['reportSections']);
        console.log(this.FormItems['reportSections'][0]['reportSubSections']);

        for (let i = 0; i < this.FormItems['reportSections'].length; i++) {
            console.log(this.FormItems['reportSections'][i]);
            let section = {
                SectionId: this.FormItems['reportSections'][i].id,
                SectionName: this.FormItems['reportSections'][i].name,
                FormId: parseInt(this.FormItems['id'])
            }
            this.Sections.push(section);




            for (let t = 0; t < this.FormItems['reportSections'][i]['reportSubSections'].length; t++) {

                let SubSection = {
                    SectionId: this.FormItems['reportSections'][i].id,
                    SubSectionId: this.FormItems['reportSections'][i]['reportSubSections'][t].id,
                    SubSectionName: this.FormItems['reportSections'][i]['reportSubSections'][t].name
                }

                this.SubSections.push(SubSection);

                console.log(this.FormItems['reportSections'][i]['reportSubSections'][t]);
                for (let r = 0; r < this.FormItems['reportSections'][i]['reportSubSections'][t]['indicators'].length; r++) {
                    console.log(this.FormItems["reportSections"][i]['reportSubSections'][t]['indicators'][r]);
                    if (this.FormItems["reportSections"][i]['reportSubSections'][t]['indicators'][r].dataType === 'Text') {
                        this.ControlType = "Text";


                        this.ItemValue = "Empty";
                    }
                    else if (this.FormItems["reportSections"][i]['reportSubSections'][t]['indicators'][r].dataType === 'Numeric') {
                        this.ControlType = "number";

                        this.ItemValue = '';

                    }
                    let SubSectionName = this.FormItems['reportSections'][i]['reportSubSections'][t].name;
                    let Code = this.FormItems["reportSections"][i]['reportSubSections'][t]['indicators'][r].code
                    this.ItemKey = SubSectionName + '_' + Code + '_' + this.FormItems["reportSections"][i]['reportSubSections'][t]['indicators'][r].id
                    console.log("Itemkey");
                    console.log(this.ItemKey);
                    let IndicatorQuestion = {
                        SubSectionId: this.FormItems['reportSections'][i]['reportSubSections'][t].id,
                        code: this.FormItems["reportSections"][i]['reportSubSections'][t]['indicators'][r].code,
                        key: this.ItemKey,
                        Id:this.FormItems["reportSections"][i]['reportSubSections'][t]['indicators'][r].id,
                        //this.FormItems["reportSections"][i]['reportSubSections'][t]['indicators'][r].id,
                        label: this.FormItems["reportSections"][i]['reportSubSections'][t]['indicators'][r].name,
                        required: true,
                        value: this.ItemValue,
                        controlType: this.ControlType
                    }
                    this.IndicatorQuestions.push(IndicatorQuestion);

                }

            }



        }
         
     
    
        console.log("Section identificatioon");
        console.log(this.Sections);
        console.log("SubSection identificatioon");
        console.log(this.SubSections);
        console.log("indictator identificatioon");
        console.log(this.IndicatorQuestions);


        this.GetFormData();
        console.log('After loading existing data');
        console.log(this.IndicatorQuestions);
        this.formGroup = this.formBuilder.group({
            // IndicatorQuestions:this.formBuilder.control(this.IndicatorQuestions),
            IndicatorQuestions: this.formBuilder.array(this.IndicatorQuestions.map(o => new FormControl(o))),

            Period: new FormControl(this.FormResults.Period, [Validators.required])
        });


        console.log(this.formGroup);

       
}   
    GetFormData()
    {
        if (this.ReportingFormId > 0)
        {
            
            this.formdetailservice.getFormdata(this.ReportingFormId).subscribe(res => {

                console.log(res);
           
               this.ExistingData = res;

               console.log("existingdata");
               console.log(this.ExistingData['reportingValues'][0]['resultNumeric']);
               this.date.setValue(moment(this.ExistingData['reportingValues'][0]['reportDate']).toDate());

            
            this.FormResults.Period = moment(moment(this.ExistingData['reportingValues'][0]['reportDate']).format('DD-MMM-YYYY')).toDate();
              
       if(this.ExistingData['reportingValues'].length > 0)
       {
         for(let i = 0 ; i < this.ExistingData['reportingValues'].length;i++)
          {
              
             
              var Indicator = this.ExistingData['reportingValues'][i]['indicatorId'].toString();
               var numvalue = this.ExistingData['reportingValues'][i]['resultNumeric'].toString();
               var text = this.ExistingData['reportingValues'][i]['resultText'].toString();
              

              for (let t = 0; t < this.IndicatorQuestions.length; t++) {
             
                var  Identification = this.IndicatorQuestions[t].Id.toString();
                var  control = this.IndicatorQuestions[t].controlType.toString();
                if (Identification === Indicator)  {
                    if (control.toLowerCase() == 'number')
                    {
                        this.IndicatorQuestions[t].value = numvalue;
                      //  this.formGroup.controls.IndicatorQuestions.value[].value = numvalue;
                        console.log('ValueChanged');
                        console.log(this.IndicatorQuestions[t].value);
                    }
                    else if (control.toLowerCase() == 'text')
                    {
                        console.log('ValueChanged');
                        //this.formGroup.controls.IndicatorQuestions.value[index].value = text;
                        console.log(this.IndicatorQuestions[t].value);
                        this.IndicatorQuestions[t].value = text;
                    }
                  }
                }
               
           }
              

        } 
              // console.log(res.resultNumeric);

            });


        }


    

    }

    log(val) { console.log(val); }

    GetType(val) {
        console.log(val);
        if (val === 'Numeric') {
            this.ControlType = 'number';

        }
        else if (val === 'Text') {
            this.ControlType = 'text';
        }

        console.log(this.ControlType);
    }

    OnValueChange($event, codeId) {
        this.total = 0;
        console.log("Event change");
        console.log($event.target.id);
        console.log($event.target.name);
        console.log($event.target.value);
        let val = $event.target.value;
        let index = -1;

      
        const element = document.getElementById(codeId)  ;
        if(element != undefined)
        {
        element.remove();
        }
        for (let i = 0; i < this.IndicatorQuestions.length; i++) {
            const code = this.IndicatorQuestions[i].code.toString();
            if (code === codeId) {
                index = i
            }
        }
        this.IndicatorQuestions[index].value = val;
        this.formGroup.controls.IndicatorQuestions.value[index].value = val;
        console.log(this.formGroup.controls.IndicatorQuestions.value[index]);

        const name = $event.target.name;

        const SubSectionFilter = this.SubSections.filter(x => x.SubSectionId == parseInt(name, 10));



        if (SubSectionFilter.length > 0) {
            const SubSectionName = SubSectionFilter[0]['SubSectionName'];
            if (SubSectionName !== 'Circumcision Adverse Events') {
                this.filtervalue = true;
            }
            else {
                this.filtervalue = false;
            }
            if ((SubSectionName !== 'Methadone Assisted Therapy(MAT)')) {
                this.filtervalue = true;
            }

            else {
                this.filtervalue = false;
            }

        }

        this.filteritems = this.IndicatorQuestions.filter(item => item.SubSectionId == parseInt(name, 10));


        if (this.filteritems.length > 0) {
            if (this.filtervalue == true) {

                for (let i = 0; i < this.filteritems.length; i++) {
                    const filterlabel = this.filteritems[i]['label'].toString();
                    if (filterlabel !== 'Total Assessed for HIV Risk' && filterlabel !== 'Self Testing Total' && filterlabel !== 'Circumcised Total' && filterlabel !== 'PEP Total') {
                        const value = this.filteritems[i].value.toString();
                        if (value == '')
                        {
                            this.total += 0;
                        }
                        else { this.total += parseInt(this.filteritems[i].value, 10); }
                    }


                }

            }


        }
        if (SubSectionFilter.length > 0) {
            const SubSectionNameId = SubSectionFilter[0]['SubSectionName'].toString();




            if (SubSectionNameId === "Number Assessed for HIV risk") {
                const element = document.getElementById('Number Assessed for HIV risk_Sum HV01-37 to HV01-44_9') as HTMLInputElement;
                element.value = this.total.toString();
                const Indicatorlabel = 'Total Assessed for HIV Risk'
                for (let i = 0; i < this.IndicatorQuestions.length; i++) {
                    const code = this.IndicatorQuestions[i].label.toString();
                    if (code === Indicatorlabel) {
                        this.IndicatorQuestions[i].value = this.total.toString();
                        const codeValue = document.getElementById(this.IndicatorQuestions[i].code.toString()) ;
                        if(codeValue !=undefined)
                        {
                        codeValue.remove();
                        }
                    }
                }

            }
            if (SubSectionNameId === "Number Self Testing for HIV") {
                const element = document.getElementById('Number Self Testing for HIV_(Sum HV01-46 To HV01-49)HV01-50_14') as HTMLInputElement;
                element.value = this.total.toString();

                const Indicatorlabel = 'Self Testing Total'
                for (let i = 0; i < this.IndicatorQuestions.length; i++) {
                    const code = this.IndicatorQuestions[i].label.toString();
                    if (code === Indicatorlabel) {
                        this.IndicatorQuestions[i].value = this.total.toString();
                        const codeValue = document.getElementById(this.IndicatorQuestions[i].code.toString()) ;
                        if(codeValue !=undefined)
                        {
                        codeValue.remove();
                        }
                    }
                }

            }
            if (SubSectionNameId === "Number Circumcised") {
                const element = document.getElementById('Number Circumcised_(Sum HV04-01 to HV04-06)HV04-07_32') as HTMLInputElement;
                element.value = this.total.toString();
                const Indicatorlabel = 'Circumcised Total'
                for (let i = 0; i < this.IndicatorQuestions.length; i++) {
                    const code = this.IndicatorQuestions[i].label.toString();
                    if (code === Indicatorlabel) {
                        this.IndicatorQuestions[i].value = this.total.toString();
                        const codeValue = document.getElementById(this.IndicatorQuestions[i].code.toString()) ;
                        if(codeValue !=undefined)
                        {
                        codeValue.remove();
                        }
                    }
                }

            }
            if (SubSectionNameId === "Post-Exposure Prophylaxis(PEP)") {
                const element = document.getElementById('Post-Exposure Prophylaxis(PEP)_(Sum HV05-01 to HV05-05)HV05-06_20') as HTMLInputElement;
                element.value = this.total.toString();
                const Indicatorlabel = 'PEP Total'
                for (let i = 0; i < this.IndicatorQuestions.length; i++) {
                    const code = this.IndicatorQuestions[i].label.toString();
                    if (code === Indicatorlabel) {
                        this.IndicatorQuestions[i].value = this.total.toString();
                        const codeValue = document.getElementById(this.IndicatorQuestions[i].code.toString()) ;
                        if(codeValue !=undefined)
                        {
                        codeValue.remove();
                        }
                    }
                }

            }

        }




    }
    isNumberKey($evt) {
        const charCode = ($evt.which) ? $evt.which : $evt.keyCode
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }

        return true;
    }
  close()
  {
    this.zone.run(() => {
        this.router.navigate(['/air/'],
            { relativeTo: this.route });
    });
   // console.log(messag
  }

    submitResult() {
        let returnvalue = false;
        let valuevalidation = false;
        for (let i = 0; i < this.IndicatorQuestions.length; i++) {
            const val = this.IndicatorQuestions[i].value.toString();
            if ( val == '') {
                console.log(this.IndicatorQuestions[i].code.toString()); 
                const code = this.IndicatorQuestions[i].code.toString();
                const element = document.getElementById(code) ;
                
                element.innerHTML = "<span style='color:red;'>Required</span>";
                returnvalue = true;
                valuevalidation = true;
            }
        }

        if(valuevalidation==true)
        {
            this.snotifyService.error('Kindly note the all values of the indicator  are required ', 'Indicators',
            this.notificationService.getConfig());
        }
        if (this.FormResults.Period == undefined) {
            this.snotifyService.error('Kindly note the monthly period is required ', 'Monthly Period',
                this.notificationService.getConfig());
            returnvalue = true;
        }
        
        /// return returnvalue;
          

         if(returnvalue == true)
         {
             return;
         }
         else { 
           const reportingDate = moment(this.FormResults.Period).format('DD-MMM-YYYY').toString();
           const reportingFormId = this.Forms[0].FormId; 
           const createdby = parseInt(localStorage.getItem('appUserId'));
        

            for (let i = 0; i < this.IndicatorQuestions.length; i++) { 
           ///  if(this.IndicatorQuestions[i].ControlType=)
           const controltype = this.IndicatorQuestions[i].controlType.toString();
           
           if (controltype == 'number')
           {
           this.numericnumber = parseInt(this.IndicatorQuestions[i].value.toString())
           this.resultvalue = "";
           
           }
           else if  (controltype == 'Text')
           { 
            this.resultvalue = this.IndicatorQuestions[i].value.toString()
            this.numericnumber = 0;
          }
         
          
            this.IndicatorResults.push({
                'Id': this.IndicatorQuestions[i].Id,
                'ResultText': this.resultvalue,
                'ResultNumeric': this.numericnumber
            });
        }
            this.spinner.show();
               console.log("IndicatorResults");
               console.log(this.IndicatorResults);
            this.formdetailservice.submitIndicatorResults(reportingDate,reportingFormId,createdby,this.IndicatorResults).subscribe(
                (response) => {
                console.log(response);
               // const message = response;
                console.log(response['message']);
                console.log(response['reportingFormId']);
                console.log(response.message);
                console.log(response.reportingFormId);
                this.zone.run(() => {
                    this.router.navigate(['/air/'],
                        { relativeTo: this.route });
                });
               // console.log(message['Message']);
               // console.log(message('ReportingFormId'));
               this.snotifyService.success(response.message, 'Submit Indicator Results',
               this.notificationService.getConfig());
                },
                (error) => {
                this.snotifyService.error('Error submitting Indicator Results ' + error, 'Submit Indicator Results',
                    this.notificationService.getConfig());
                    this.spinner.hide();

                },
                
                () => {
                this.spinner.hide();
                }
            );


        
        }
        
    
    }
        // console.log(this.formGroup);

    
}






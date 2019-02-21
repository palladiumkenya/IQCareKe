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



@Component({
    selector: 'app-active-form-report',
    templateUrl: './active-form-report.component.html',
    styleUrls: ['./active-form-report.component.css']
})


export class ActiveFormReportComponent implements OnInit {


    @ViewChild(MatDatepicker) picker;
    date = new FormControl();
    IndicatorQuestions: IndicatorQuestionBase[] = [];

    Sections: Section[];
    Forms: Form[];
    total: number;
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
    constructor(private route: ActivatedRoute,
        private formBuilder: FormBuilder,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private router: Router) {
        this.Forms = [];
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

                        this.ItemValue = "0";

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



        this.formGroup = this.formBuilder.group({
            // IndicatorQuestions:this.formBuilder.control(this.IndicatorQuestions),
            IndicatorQuestions: this.formBuilder.array(this.IndicatorQuestions.map(o => new FormControl(o))),

            Period: new FormControl(this.FormResults.Period, [Validators.required])
        });
        console.log(this.formGroup);
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
        if(element !=undefined)
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
                        this.total += parseInt(this.filteritems[i].value, 10);
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


    submitResult() {
        let returnvalue= false;
        let valuevalidation=false;
        for (let i = 0; i < this.IndicatorQuestions.length; i++) {
            const val = this.IndicatorQuestions[i].value.toString();
            if (val == '0' || val == '') {
                console.log(this.IndicatorQuestions[i].code.toString()); 
                const code=this.IndicatorQuestions[i].code.toString();
                const element = document.getElementById(code) ;
                
                element.innerHTML="<span style='color:red;'>Required</span>";
                returnvalue =true;
                valuevalidation=true;
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
            returnvalue=true;
        }
        
        /// return returnvalue;
          

         if(returnvalue ==true)
         {
             return;
         }
         else {

        console.log(this.FormResults.Period);
         }
        // console.log(this.formGroup);

    }
}






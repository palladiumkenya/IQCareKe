import { Component, OnInit, NgZone, ViewChild, Directive } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { SnotifyService, SnotifyPosition } from 'ng-snotify';
import { NotificationService } from '../../shared/_services/notification.service';
import { RouterInitializer } from '@angular/router/src/router_module';
import { IndicatorQuestionBase } from '../_model/indicatorquestion-base';
import { NativeDateAdapter, DateAdapter, MatDatepicker, JAN } from '@angular/material';
import { Section, Form, SubSection, FormResults } from '../_model/Sectionidentifier';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators, FormArray } from '@angular/forms';
import * as _ from 'lodash';
import * as moment from 'moment';
import { FormDetailsService } from '../_services/formdetails.service';
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
    IndicatorResults: any[];
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
    existing: [];

    existingperiod: [];
    isEdit: boolean = false;
    reportingPeriodId: number;
    maxDate: Date = new Date();
    minDate: Date = new Date(2000, JAN);
    constructor(private route: ActivatedRoute,
        private formBuilder: FormBuilder,
        private snotifyService: SnotifyService,
        private notificationService: NotificationService,
        private formdetailservice: FormDetailsService,
        public zone: NgZone,
        private router: Router,
        private spinner: NgxSpinnerService) {
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
        this.IndicatorResults = [];
        this.existing = [];
        this.existingperiod = [];



    }
    monthSelected(params) {
        this.date.setValue(params);

        const datevalue = moment(params).toDate();
        this.FormResults.Period = moment(moment(datevalue).format('DD-MMM-YYYY')).toDate();
        this.picker.close();
    }

    ngOnInit() {

        this.route.data.subscribe(res => {
            this.isEdit = res.isEdit;
            const { FormDetails } = res;
            this.FormItems = FormDetails
        });

        this.route.params.subscribe(params => {
            this.ReportingFormId = params['reportingFormId'];
            this.reportingPeriodId = params['reportingPeriodId'];
        });

        let Form = {
            FormId: this.FormItems['id'],
            FormName: this.FormItems['name']
        }

        this.Forms.push(Form);

        for (let i = 0; i < this.FormItems['reportSections'].length; i++) {
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

                for (let r = 0; r < this.FormItems['reportSections'][i]['reportSubSections'][t]['indicators'].length; r++) {
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
                    this.ItemKey = SubSectionName + '_' + Code;

                    let IndicatorQuestion = {
                        SubSectionId: this.FormItems['reportSections'][i]['reportSubSections'][t].id,
                        code: this.FormItems["reportSections"][i]['reportSubSections'][t]['indicators'][r].code,
                        key: this.ItemKey,
                        Id: this.FormItems["reportSections"][i]['reportSubSections'][t]['indicators'][r].id,
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


        this.GetFormData();
        this.formGroup = this.formBuilder.group({
            // IndicatorQuestions:this.formBuilder.control(this.IndicatorQuestions),
            IndicatorQuestions: this.formBuilder.array(this.IndicatorQuestions.map(o => new FormControl(o))),

            Period: new FormControl(this.FormResults.Period, [Validators.required])
        });
        if (this.isEdit) {

            this.formGroup.controls.Period.disable();

        }
        else {
            this.formGroup.controls.Period.enable();
        }



        console.log("Questions");


        console.log(this.formGroup.controls['IndicatorQuestions']['controls'][0]);










    }
    GetFormData() {
        if (this.isEdit) {
            console.log(this.reportingPeriodId + ' reportingPeriodId')
            this.formdetailservice.getFormdata(this.reportingPeriodId).subscribe(res => {
                this.ExistingData = res;
                console.log(this.ExistingData['reportingValues'][0]['reportDate'] + 'Reporting date');
                console.log(moment(this.ExistingData['reportingValues'][0]['reportDate']).toDate() + ' Report Date');
                this.date.setValue(moment(this.ExistingData['reportingValues'][0]['reportDate']).toDate());


                this.FormResults.Period = moment(moment(this.ExistingData['reportingValues'][0]['reportDate']).format('DD-MMM-YYYY')).toDate();

                if (this.ExistingData['reportingValues'].length > 0) {
                    for (let i = 0; i < this.ExistingData['reportingValues'].length; i++) {


                        var Indicator = this.ExistingData['reportingValues'][i]['indicatorId'].toString();
                        var numvalue = this.ExistingData['reportingValues'][i]['resultNumeric'].toString();
                        var text = this.ExistingData['reportingValues'][i]['resultText'].toString();


                        for (let t = 0; t < this.IndicatorQuestions.length; t++) {

                            var Identification = this.IndicatorQuestions[t].Id.toString();
                            var control = this.IndicatorQuestions[t].controlType.toString();
                            if (Identification === Indicator) {
                                if (control.toLowerCase() == 'number') {
                                    this.IndicatorQuestions[t].value = numvalue;
                                    //  this.formGroup.controls.IndicatorQuestions.value[].value = numvalue;
                                }
                                else if (control.toLowerCase() == 'text') {
                                    //this.formGroup.controls.IndicatorQuestions.value[index].value = text;
                                    this.IndicatorQuestions[t].value = text;
                                }
                            }
                        }

                    }


                }

            });


        }




    }


    GetType(val) {
        if (val === 'Numeric') {
            this.ControlType = 'number';

        }
        else if (val === 'Text') {
            this.ControlType = 'text';
        }

    }

    OnValueChange($event, codeId) {
        this.total = 0;
        let val = $event.target.value;
        let index = -1;


        const element = document.getElementById(codeId);
        if (element != undefined) {
            //element.remove();
            element.innerHTML = "";
        }
        for (let i = 0; i < this.IndicatorQuestions.length; i++) {
            const code = this.IndicatorQuestions[i].code.toString();
            if (code === codeId) {
                index = i;
            }
        }
        this.IndicatorQuestions[index].value = val;
        this.formGroup.controls.IndicatorQuestions.value[index].value = val;
        const NumberCircumcisedHIV = ['Number Circumcised_HV04-08', 'Number Circumcised_HV04-09', 'Number Circumcised_HV04-10'];
        const TypeofCircumcision = ['Type of Circumcision_HV04-11', 'Type of Circumcision_HV04-12'];
        const exposedtotal = ['Post-Exposure Prophylaxis(PEP)_HV05-01', 'Post-Exposure Prophylaxis(PEP)_HV05-02'];
        const peptotal = ['Post-Exposure Prophylaxis(PEP)_HV05-04', 'Post-Exposure Prophylaxis(PEP)_HV05-05'];

        const Pep = ['Post-Exposure Prophylaxis(PEP)_HV05-01', 'Post-Exposure Prophylaxis(PEP)_HV05-02',
            'Post-Exposure Prophylaxis(PEP)_HV05-03'
            , 'Post-Exposure Prophylaxis(PEP)_HV05-04',
            'Post-Exposure Prophylaxis(PEP)_HV05-05', 'Post-Exposure Prophylaxis(PEP)_HV05-06']

        if (exposedtotal.includes(this.IndicatorQuestions[index].key)) {
            let valuetotal = 0;
            const totalno = document.getElementById('Post-Exposure Prophylaxis(PEP)_HV05-03') as HTMLInputElement;
            valuetotal = totalno.valueAsNumber;
            const result = this.IndicatorQuestions.filter((t) => exposedtotal.includes(t.key));
            const res = exposedtotal.filter(f => this.IndicatorQuestions.filter(t => t.key == f));
            console.log(result);
            let valuecalc = 0;
            for (let i = 0; i < result.length; i++) {
                const value = result[i].value.toString();
                if (value == '') {
                    valuecalc += 0;
                }
                else { valuecalc += parseInt(result[i].value, 10); }
            }
            const elementexposedtotal = document.getElementById('Post-Exposure Prophylaxis(PEP)_HV05-03') as HTMLInputElement;
            elementexposedtotal.value = valuecalc.toString();
            for (let i = 0; i < this.IndicatorQuestions.length; i++) {
                const key = this.IndicatorQuestions[i].key.toString();
                if (key === 'Post-Exposure Prophylaxis(PEP)_HV05-03') {
                    this.IndicatorQuestions[i].value = this.total.toString();

                    this.IndicatorQuestions[i].value = valuecalc.toString();
                    this.formGroup.controls.IndicatorQuestions.value[i].value = valuecalc;
                }
            }




        }
        if (peptotal.includes(this.IndicatorQuestions[index].key)) {
            let valuetotal = 0;
            let exposedtotal = 0;
            const totalp = document.getElementById('Post-Exposure Prophylaxis(PEP)_HV05-06') as HTMLInputElement;
            const elementexposedtotal = document.getElementById('Post-Exposure Prophylaxis(PEP)_HV05-03') as HTMLInputElement;
            exposedtotal = parseInt(elementexposedtotal.value.toString(), 10);
            valuetotal = totalp.valueAsNumber;
            const result = this.IndicatorQuestions.filter((t) => peptotal.includes(t.key));
            const res = peptotal.filter(f => this.IndicatorQuestions.filter(t => t.key == f));
            console.log(result);
            let valuecalc = 0;
            for (let i = 0; i < result.length; i++) {
                const value = result[i].value.toString();
                if (value == '') {
                    valuecalc += 0;
                }
                else { valuecalc += parseInt(result[i].value, 10); }
            }
            if (this.IndicatorQuestions[index].key == 'Post-Exposure Prophylaxis(PEP)_HV05-04') {
                const exposedoccupational = document.getElementById('Post-Exposure Prophylaxis(PEP)_HV05-01') as HTMLInputElement;
                const exposedocctotal = exposedoccupational.valueAsNumber;
                const pepoccupational = parseInt(this.IndicatorQuestions[index].value, 10);
                const elementexposedtotal = document.getElementById('Post-Exposure Prophylaxis(PEP)_HV05-03') as HTMLInputElement;
                const finaltotal = parseInt(elementexposedtotal.value.toString(), 10);
                if (exposedocctotal > 0) {
                    if (pepoccupational > exposedocctotal) {
                        let currentelement = document.getElementById(codeId);

                        currentelement.innerHTML = "<span style='color:red;'>The value cannot be greater than the exposed occupational value</span>";
                        const elementselected = document.getElementById(this.IndicatorQuestions[index].key) as HTMLInputElement;
                        elementselected.value = '';


                        this.IndicatorQuestions[index].value = '';
                        this.formGroup.controls.IndicatorQuestions.value[index].value = '';
                        return;

                    }
                    else if (pepoccupational > finaltotal) {
                        let currentelement = document.getElementById(codeId);

                        currentelement.innerHTML = "<span style='color:red;'>The value cannot be greater than the exposed total value</span>";
                        const elementselected = document.getElementById(this.IndicatorQuestions[index].key) as HTMLInputElement;
                        elementselected.value = '';


                        this.IndicatorQuestions[index].value = '';
                        this.formGroup.controls.IndicatorQuestions.value[index].value = '';
                        return;


                    }
                }

            }
            if (this.IndicatorQuestions[index].key == 'Post-Exposure Prophylaxis(PEP)_HV05-05') {
                const exposedother = document.getElementById('Post-Exposure Prophylaxis(PEP)_HV05-02') as HTMLInputElement;
                const exposedothertotal = exposedother.valueAsNumber;
                const pepother = parseInt(this.IndicatorQuestions[index].value, 10);
                const elementexposedtotal = document.getElementById('Post-Exposure Prophylaxis(PEP)_HV05-03') as HTMLInputElement;
                const finaltotal = elementexposedtotal.valueAsNumber;
                if (exposedothertotal > 0) {
                    if (pepother > exposedothertotal) {
                        let currentelement = document.getElementById(codeId);

                        currentelement.innerHTML = "<span style='color:red;'>The value cannot be greater than the exposed other value</span>";
                        const elementselected = document.getElementById(this.IndicatorQuestions[index].key) as HTMLInputElement;
                        elementselected.value = '';


                        this.IndicatorQuestions[index].value = '';
                        this.formGroup.controls.IndicatorQuestions.value[index].value = '';
                        return;

                    }
                    else if (pepother > finaltotal) {
                        let currentelement = document.getElementById(codeId);

                        currentelement.innerHTML = "<span style='color:red;'>The value cannot be greater than the exposed total value</span>";
                        const elementselected = document.getElementById(this.IndicatorQuestions[index].key) as HTMLInputElement;
                        elementselected.value = '';


                        this.IndicatorQuestions[index].value = '';
                        this.formGroup.controls.IndicatorQuestions.value[index].value = '';
                        return;


                    }
                }

            }
            if (exposedtotal > 0) {
                if (valuecalc > exposedtotal) {

                    this.IndicatorQuestions[index].value = '';
                    this.formGroup.controls.IndicatorQuestions.value[index].value = '';
                    const elementselected = document.getElementById(this.IndicatorQuestions[index].key) as HTMLInputElement;
                    elementselected.value = '';
                    const elementpeptotal = document.getElementById('Post-Exposure Prophylaxis(PEP)_HV05-06') as HTMLInputElement;
                    elementpeptotal.value = '';
                    this.snotifyService.error('Kindly note the total of values' +
                        'under code HV05-04, HV05-05  cannot be more than Exposed total ', 'Circumcised Total',
                        this.notificationService.getConfig());
                    return;

                }
                else {
                    const elementpeptotal = document.getElementById('Post-Exposure Prophylaxis(PEP)_HV05-06') as HTMLInputElement;
                    elementpeptotal.value = valuecalc.toString();

                    for (let i = 0; i < this.IndicatorQuestions.length; i++) {
                        const key = this.IndicatorQuestions[i].key.toString();
                        if (key === 'Post-Exposure Prophylaxis(PEP)_HV05-06') {
                            this.IndicatorQuestions[i].value = this.total.toString();

                            this.IndicatorQuestions[i].value = valuecalc.toString();
                            this.formGroup.controls.IndicatorQuestions.value[i].value = valuecalc;
                        }
                    }

                }
            }





        }

        if (TypeofCircumcision.includes(this.IndicatorQuestions[index].key)) {
            let valuetotal = 0;
            const element = document.getElementById('Number Circumcised_HV04-07') as HTMLInputElement;
            valuetotal = element.valueAsNumber;
            if (valuetotal > 0) {
                let valuecircumcised = 0;
                let valueIndicator = parseInt(this.IndicatorQuestions[index].value);
                if (valueIndicator > valuetotal) {

                    let currentelement = document.getElementById(codeId);

                    currentelement.innerHTML = "<span style='color:red;'>The value cannot be greater than the Circumcised Total</span>";
                    const elementselected = document.getElementById(this.IndicatorQuestions[index].key) as HTMLInputElement;
                    elementselected.value = '';


                    this.IndicatorQuestions[index].value = '';
                    this.formGroup.controls.IndicatorQuestions.value[index].value = '';
                    return;


                }
                else {
                    const result = this.IndicatorQuestions.filter((t) => TypeofCircumcision.includes(t.key));
                    const res = TypeofCircumcision.filter(f => this.IndicatorQuestions.filter(t => t.key == f));
                    console.log(result);
                    let valuecalc = 0;
                    for (let i = 0; i < result.length; i++) {
                        const value = result[i].value.toString();
                        if (value == '') {
                            valuecalc += 0;
                        }
                        else { valuecalc += parseInt(result[i].value, 10); }
                    }

                    if (valuecalc > valuetotal) {

                        this.IndicatorQuestions[index].value = '';
                        this.formGroup.controls.IndicatorQuestions.value[index].value = '';
                        const elementselected = document.getElementById(this.IndicatorQuestions[index].key) as HTMLInputElement;
                        elementselected.value = '';

                        this.snotifyService.error('Kindly note the total of values' +
                            'under code HV04-11, HV04-12  cannot be more than Circumcised total ', 'Circumcised Total',
                            this.notificationService.getConfig());
                        return;

                    }
                }

            }



        }



        if (NumberCircumcisedHIV.includes(this.IndicatorQuestions[index].key)) {
            let valuetotal = 0;
            const element = document.getElementById('Number Circumcised_HV04-07') as HTMLInputElement;
            valuetotal = element.valueAsNumber;
            if (valuetotal > 0) {
                let valuecircumcised = 0;
                let valueIndicator = parseInt(this.IndicatorQuestions[index].value);
                if (valueIndicator > valuetotal) {

                    let currentelement = document.getElementById(codeId);

                    currentelement.innerHTML = "<span style='color:red;'>The value cannot be greater than the Circumcised Total</span>";
                    const elementselected = document.getElementById(this.IndicatorQuestions[index].key) as HTMLInputElement;
                    elementselected.value = '';


                    this.IndicatorQuestions[index].value = '';
                    this.formGroup.controls.IndicatorQuestions.value[index].value = '';
                    return;


                }
                else {
                    const result = this.IndicatorQuestions.filter((t) => NumberCircumcisedHIV.includes(t.key));
                    const res = NumberCircumcisedHIV.filter(f => this.IndicatorQuestions.filter(t => t.key == f));
                    console.log(result);
                    let valuecalc = 0;
                    for (let i = 0; i < result.length; i++) {
                        const value = result[i].value.toString();
                        if (value == '') {
                            valuecalc += 0;
                        }
                        else { valuecalc += parseInt(result[i].value, 10); }
                    }

                    if (valuecalc > valuetotal) {

                        this.IndicatorQuestions[index].value = '';
                        this.formGroup.controls.IndicatorQuestions.value[index].value = '';
                        const elementselected = document.getElementById(this.IndicatorQuestions[index].key) as HTMLInputElement;
                        elementselected.value = '';

                        this.snotifyService.error('Kindly note the total of values' +
                            'under code HV04-08, HV04-09, HV04-10  cannot be more than Circumcised total ', 'Circumcised Total',
                            this.notificationService.getConfig());
                        return;

                    }
                }

            }

        }

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
            if (SubSectionName !== 'Post-Exposure Prophylaxis(PEP)') {
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
                    const CircumcisedCode = ['Circumcised HIV+', 'Circumcised HIV-', 'Circumcised_HIV NK'];
                    if (!CircumcisedCode.includes(filterlabel)) {
                        if (filterlabel !== 'Total Assessed for HIV Risk' && filterlabel !== 'Self Testing Total' && filterlabel !== 'Circumcised Total' && filterlabel !== 'PEP Total') {
                            const value = this.filteritems[i].value.toString();
                            if (value == '') {
                                this.total += 0;
                            }
                            else { this.total += parseInt(this.filteritems[i].value, 10); }
                        }
                    }

                }

            }


        }
        if (SubSectionFilter.length > 0) {
            const SubSectionNameId = SubSectionFilter[0]['SubSectionName'].toString();




            if (SubSectionNameId === "Number Assessed for HIV risk") {
                const element = document.getElementById('Number Assessed for HIV risk_HV01-45') as HTMLInputElement;
                element.value = this.total.toString();
                const Indicatorlabel = 'Total Assessed for HIV Risk'
                for (let i = 0; i < this.IndicatorQuestions.length; i++) {
                    const code = this.IndicatorQuestions[i].label.toString();
                    if (code === Indicatorlabel) {
                        this.IndicatorQuestions[i].value = this.total.toString();
                        const codeValue = document.getElementById(this.IndicatorQuestions[i].code.toString());
                        if (codeValue != undefined) {
                            codeValue.remove();
                        }
                    }
                }

            }
            if (SubSectionNameId === "Number Self Testing for HIV") {
                const element = document.getElementById('Number Self Testing for HIV_HV01-50') as HTMLInputElement;
                element.value = this.total.toString();

                const Indicatorlabel = 'Self Testing Total'
                for (let i = 0; i < this.IndicatorQuestions.length; i++) {
                    const code = this.IndicatorQuestions[i].label.toString();
                    if (code === Indicatorlabel) {
                        this.IndicatorQuestions[i].value = this.total.toString();
                        const codeValue = document.getElementById(this.IndicatorQuestions[i].code.toString());
                        if (codeValue != undefined) {
                            codeValue.remove();
                        }
                    }
                }

            }
            if (SubSectionNameId === "Number Circumcised") {
                const element = document.getElementById('Number Circumcised_HV04-07') as HTMLInputElement;
                element.value = this.total.toString();

                const Indicatorlabel = 'Circumcised Total';


                for (let i = 0; i < this.IndicatorQuestions.length; i++) {
                    const code = this.IndicatorQuestions[i].label.toString();
                    if (code === Indicatorlabel) {

                        this.IndicatorQuestions[i].value = this.total.toString();
                        const codeValue = document.getElementById(this.IndicatorQuestions[i].code.toString());
                        if (codeValue != undefined) {
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
    close() {
        this.zone.run(() => {
            this.router.navigate(['/air/report/' + this.ReportingFormId],
                { relativeTo: this.route });
        });
    }

    submitResult() {
        let returnvalue = false;
        let valuevalidation = false;
        for (let i = 0; i < this.IndicatorQuestions.length; i++) {
            const val = this.IndicatorQuestions[i].value.toString();
            if (val == '') {
                const code = this.IndicatorQuestions[i].code.toString();
                const element = document.getElementById(code);

                element.innerHTML = "<span style='color:red;'>Required</span>";
                returnvalue = true;
                valuevalidation = true;
            }
        }

        if (valuevalidation == true) {
            this.snotifyService.error('Kindly note the all values of the indicator  are required ', 'Indicators',
                this.notificationService.getConfig());
        }
        if (this.FormResults.Period == undefined) {
            this.snotifyService.error('Kindly note the monthly period is required ', 'Monthly Period',
                this.notificationService.getConfig());
            returnvalue = true;
        }

        /// return returnvalue;


        if (returnvalue == true) {
            return;
        }
        else {
            const reportingDate = moment(this.FormResults.Period).format('DD-MMM-YYYY').toString();
            const reportingFormId = this.Forms[0].FormId;
            const createdby = parseInt(localStorage.getItem('appUserId'));


            for (let i = 0; i < this.IndicatorQuestions.length; i++) {
                ///  if(this.IndicatorQuestions[i].ControlType=)
                const controltype = this.IndicatorQuestions[i].controlType.toString();

                if (controltype == 'number') {
                    this.numericnumber = parseInt(this.IndicatorQuestions[i].value.toString())
                    this.resultvalue = "";

                }
                else if (controltype == 'Text') {
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

            if (this.isEdit) {

                this.formdetailservice.EditIndicatorResults(reportingDate
                    , reportingFormId, this.ReportingFormId, createdby, this.IndicatorResults).subscribe(
                        (response) => {
                            this.zone.run(() => {
                                this.router.navigate(['/air/report/' + this.ReportingFormId],
                                    { relativeTo: this.route });
                            });
                            // console.log(message['Message']);
                            // console.log(message('ReportingFormId'));
                            this.snotifyService.success(response.message, 'Edited Indicator Results',
                                this.notificationService.getConfig());
                        },
                        (error) => {
                            // JSON.stringify(Indata), httpOptions
                            this.snotifyService.error('Error  Editing Indicator Results ' + error, 'Edit',
                                this.notificationService.getConfig());
                            this.spinner.hide();
                        },

                        () => {
                            this.spinner.hide();

                        });


            } else {
                this.formdetailservice.checkIfPeriodExists(reportingDate).subscribe(
                    (response) => {
                        this.existingperiod = response['period'];
                        if (this.existingperiod.length > 0) {
                            this.snotifyService.error('Kindly note the period already exists choose another reporting period ', 'ExistingPeriod',
                                this.notificationService.getConfig());


                            this.spinner.hide();
                            return;


                        } else {
                            this.spinner.show();
                            this.formdetailservice.submitIndicatorResults(reportingDate, reportingFormId, createdby, this.IndicatorResults).subscribe(
                                (response) => {
                                    this.zone.run(() => {
                                        this.router.navigate(['/air/report/' + this.ReportingFormId],
                                            { relativeTo: this.route });
                                    });

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

                    },
                    (error) => {
                        this.snotifyService.error('Error checking reporting period ' + error, 'Checking if Reporting Period Exists',
                            this.notificationService.getConfig());
                        this.spinner.hide();
                    },
                    () => {
                        this.spinner.hide();
                    }
                );

            }

        }


    }


}






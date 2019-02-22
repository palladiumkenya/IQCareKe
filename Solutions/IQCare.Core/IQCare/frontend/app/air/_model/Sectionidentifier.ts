export class Section {
    SectionId: number;
    SectionName: string;
    FormId: number;

}


export class SubSection{
SectionId: number;
SubSectionId: number;
SubSectionName: string;

}

export class Form {
FormId: number;
FormName: string;

}

export class FormResults{
    FormName: string;
    Period: Date;
}

export class IndicatorResults{
    Id:string;
    ResultText:string;
    ResultNumeric:string;
}
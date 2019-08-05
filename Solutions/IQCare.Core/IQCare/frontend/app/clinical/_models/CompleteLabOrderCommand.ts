export interface CompleteLabOrderCommand {
    LabOrderId: number;
    LabOrderTestId: number;
    LabTestId: number;
    UserId: number;
    LabTestResults: AddLabTestResultCommand[];
    StrLabTestResults: any;
}

export interface AddLabTestResultCommand {
    ParameterId: number;
    ResultValue: number;
    ResultText: string;
    ResultOptionId: number;
    ResultOption: number;
    ResultUnit: string;
    ResultUnitId?: number;
    ResultConfigId?: number;
    Undetectable: boolean;
    DetectionLimit: number;
}

export class ResultDataType {
    Numeric = 'NUMERIC';
    Text = 'TEXT';
    Select = 'SELECTLIST';
}

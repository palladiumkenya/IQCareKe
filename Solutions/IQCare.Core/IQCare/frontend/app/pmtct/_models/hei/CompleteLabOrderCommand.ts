export interface CompleteLabOrderCommand {
    LabOrderId: number;
    LabOrderTestId: number;
    LabTestId: number;
    UserId: number;
    DateResultsCollected: Date;
    LabTestResults: AddLabTestResultCommand[];
}

export interface AddLabTestResultCommand {
    ParameterId: number;
    ResultValue: number;
    ResultText: string;
    ResultOptionId: number;
    ResultOption: string;
    ResultUnit: string;
    ResultUnitId: number;
    Undetectable: boolean;
    DetectionLimit: number;
}

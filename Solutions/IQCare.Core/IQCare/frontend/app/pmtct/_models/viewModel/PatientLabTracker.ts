export interface PatientLabTracker {
    labOrderTestId: number;
    labTestName: string;
    sampleDate: Date;
    reasons: string;
    results: string;
    resultTexts: string;
    labTestId: number;
    resultValues: number;
    resultUnits: string;
    resultOptions: string;
    resultDate: Date;
}

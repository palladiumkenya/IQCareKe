UPDATE PatientVitals SET RespiratoryRate = 0.00 WHERE RespiratoryRate IS NULL;
UPDATE PatientVitals SET HeartRate = 0.00 WHERE HeartRate IS NULL;
UPDATE PatientVitals SET BPSystolic = 0.00 WHERE BPSystolic IS NULL;
UPDATE PatientVitals SET BPDiastolic = 0.00 WHERE BPDiastolic IS NULL;
UPDATE PatientVitals SET Muac = 0.00 WHERE Muac IS NULL;
UPDATE PatientVitals SET SpO2 = 0.00 WHERE SpO2 IS NULL;
UPDATE PatientVitals SET HeadCircumference = 0.00 WHERE HeadCircumference IS NULL;

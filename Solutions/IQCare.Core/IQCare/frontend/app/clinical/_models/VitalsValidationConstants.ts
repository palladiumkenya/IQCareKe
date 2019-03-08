export class VitalsValidationConstants {
    MaximumHeight = 210; // centimeters
    MinimumHeight = 0;
    MinimumWeight = 0;
    MaximumWeight = 200; // Kilograms 
    MaximumHeadCircumference = 100; // centimetres
    MinimumHeadCircumference = 20; // centimetres
    SystolicBP = 300;
    DiastolicaBP = 200;
    RespiratoryRateMax = 100; // Breathes per minute
    RespiratoryRateMin = 0;
    MaximumTemperature = 42; // Degrees Celcius
    MinimumTemperature = 32;
    HeartRateMin = 0; // Beats Per Minute
    HeartRateMax = 200; // Beats Per Minute
    NumberValidationPattern = "^\\d*\\.?\\d+$";
}
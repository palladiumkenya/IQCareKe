import { FormGroup, FormControl, AbstractControl, Validators } from "@angular/forms";

export const emergencyValidator = (control: AbstractControl): { [key: string]: boolean } => {
    const emgFirstName = control.get('emgFirstName');
    const emgMiddleName = control.get('emgMiddleName');
    const emgLastName = control.get('emgLastName');
    const emgGender = control.get('emgGender');
    const emgRelationShip = control.get('emgRelationShip');
    const emgPrimaryMobileNo = control.get('emgPrimaryMobileNo');
    const emgConsentToCall = control.get('emgConsentToCall');
    const emgRegisteredClinic = control.get('emgRegisteredClinic');

    if ((emgFirstName.value === undefined || emgFirstName.value === '' || emgFirstName.value === null)
        && (emgMiddleName.value === undefined || emgMiddleName.value === '' || emgMiddleName.value === null)
        && (emgLastName.value === undefined || emgLastName.value === '' || emgLastName.value === null)
        && (emgGender.value === undefined || emgGender.value === '' || emgGender.value === null)
        && (emgRelationShip.value === undefined || emgRelationShip.value === '' || emgRelationShip.value === null)
        && (emgPrimaryMobileNo.value === undefined || emgPrimaryMobileNo.value === '' || emgPrimaryMobileNo.value === null)
        && (emgConsentToCall.value === undefined || emgConsentToCall.value === '' || emgConsentToCall.value === null)
        && (emgRegisteredClinic.value === undefined || emgRegisteredClinic.value === '' || emgRegisteredClinic.value === null)) {
    
        emgFirstName.setValidators([Validators.required])
        emgFirstName.updateValueAndValidity({onlySelf:true})
        emgLastName.setValidators([Validators.required])
        emgLastName.updateValueAndValidity({ onlySelf: true })
        emgGender.setValidators([Validators.required])
        emgGender.updateValueAndValidity({ onlySelf: true })
        emgRelationShip.setValidators([Validators.required])
        emgRelationShip.updateValueAndValidity({ onlySelf: true })
        emgPrimaryMobileNo.setValidators([Validators.required])
        emgPrimaryMobileNo.updateValueAndValidity({ onlySelf: true })
        emgConsentToCall.setValidators([Validators.required])
        emgConsentToCall.updateValueAndValidity({ onlySelf: true })
        emgRegisteredClinic.setValidators([Validators.required])
        emgRegisteredClinic.updateValueAndValidity({ onlySelf: true })


        emgFirstName.setErrors({ 'incorrect': true });
        emgMiddleName.setErrors({ 'incorrect': true });
        emgLastName.setErrors({ 'incorrect': true });
        emgGender.setErrors({ 'incorrect': true });
        emgRelationShip.setErrors({ 'incorrect': true });
        emgPrimaryMobileNo.setErrors({ 'incorrect': true });
        emgConsentToCall.setErrors({ 'incorrect': true });
        emgRegisteredClinic.setErrors({ 'incorrect': true });

        return {
            
                valid: false
            
        };
  }
    else {
        return null;
    }
};

import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatTableDataSource } from '@angular/material';
import { PersonHomeService } from '../services/person-home.service';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'app-patient-appointment',
    templateUrl: './patient-appointment.component.html',
    styleUrls: ['./patient-appointment.component.css']
})
export class PatientAppointmentComponent implements OnInit {
    personId = 0;
    patient_appointment_data: any[] = [];
    dataSource = new MatTableDataSource(this.patient_appointment_data);
    patient_appointment_displaycolumns = ['appointmentReason', 'appointmentDate', 'appointmentStatus', 'appointmentType', 'description'];
    @ViewChild(MatPaginator) paginator: MatPaginator;

    constructor(private personService: PersonHomeService, private route: ActivatedRoute) {

    }

    ngOnInit() {
        this.route.params.subscribe(params => {
            this.personId = params['id'];
            // console.log("PersonId >>>> " + this.personId)

            this.personService.getPatientByPersonId(this.personId).subscribe(patient => {
                // console.log("Patient Id >>>>>" + patient.patientId)

                if (patient != null && patient != undefined) {
                    if (patient.patientId != null) {
                        this.GetPatientAppoitment(patient.patientId);
                    }
                }
            });
        });
    }

    public GetPatientAppoitment(patientId: number) {
        if (patientId == null) {
            return;
        }

        this.personService.GetPatientAppoitment(patientId).subscribe(
            data => {
                if (data.length == 0) {
                    return;
                }
                // console.log(data);
                data.forEach(appointment => {
                    this.patient_appointment_data.push({
                        appointmentReason: appointment.appointmentReason,
                        appointmentDate: appointment.appDate,
                        appointmentStatus: appointment.appointmentStatus,
                        appointmentType: appointment.appointmentType,
                        description: appointment.description
                    });
                });


                this.dataSource = new MatTableDataSource(this.patient_appointment_data);
                this.dataSource.paginator = this.paginator;
            }, (err) => {
                console.log(err);
            }
        );
    }

}

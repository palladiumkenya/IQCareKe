
import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { MatPaginator, MatTableDataSource } from '@angular/material';
import { PrepService } from '../_services/prep.service';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'app-prep-appointmentgrid',
    templateUrl: './prep-appointmentgrid.component.html',
    styleUrls: ['./prep-appointmentgrid.component.css']
})
export class PrepAppointmentgridComponent implements OnInit {
    @Input('patientId') PatientId: number;
    @Input('ServiceAreaId') serviceAreaId: number;
    patient_appointment_data: any[] = [];
    dataSource = new MatTableDataSource(this.patient_appointment_data);
    patient_appointment_displaycolumns = ['appointmentReason', 'appointmentDate', 'appointmentStatus', 'appointmentType', 'description'];
    @ViewChild(MatPaginator) paginator: MatPaginator;
    constructor(private prepService: PrepService) { }

    ngOnInit() {


        this.GetPatientAppoitment();

    }
    public GetPatientAppoitment() {

        this.prepService.getPatientAppointmentsServiceArea(this.PatientId, this.serviceAreaId).subscribe(
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

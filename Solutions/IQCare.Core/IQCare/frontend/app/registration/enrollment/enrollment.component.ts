import {Component, NgZone, OnInit} from '@angular/core';
import {Enrollment} from '../_models/enrollment';
import {EnrollmentService} from '../_services/enrollment.service';
import {ActivatedRoute, Router} from '@angular/router';

@Component({
  selector: 'app-enrollment',
  templateUrl: './enrollment.component.html',
  styleUrls: ['./enrollment.component.css']
})
export class EnrollmentComponent implements OnInit {
    enrollment: Enrollment;
    patientId: number;
    personId: number;
    createdBy: number;

    constructor(private enrollmentService: EnrollmentService,
                private router: Router,
                private route: ActivatedRoute,
                public zone: NgZone) { }
    ngOnInit() {
        localStorage.setItem('createdBy', JSON.stringify(1));
        this.patientId = JSON.parse(localStorage.getItem('patientId'));
        this.personId = JSON.parse(localStorage.getItem('personId'));
        this.createdBy = JSON.parse(localStorage.getItem('createdBy'));

        this.enrollment = new Enrollment();
    }


    onSubmit() {
        this.enrollment.PatientId = this.patientId;
        this.enrollment.PersonId = this.personId;
        this.enrollment.CreatedBy = this.createdBy;

        console.log(this.enrollment);

        this.enrollmentService.enrollClient(this.enrollment).subscribe(data => {
            console.log(data);

            this.zone.run(() => { this.router.navigate(['/registration/home'], { relativeTo: this.route }); });
        }, err => {
            console.log(err);
        });
    }
}

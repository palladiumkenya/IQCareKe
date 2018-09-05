import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'app-enrollment',
    templateUrl: './enrollment.component.html',
    styleUrls: ['./enrollment.component.css']
})
export class EnrollmentComponent implements OnInit {
    personId: number;
    serviceAreaId: number;
    constructor(private route: ActivatedRoute) { }

    ngOnInit() {
        this.route.params.subscribe(params => {
            console.log(params);
            const { id, serviceId } = params;
            this.personId = id;
            this.serviceAreaId = serviceId;
        });
    }
}

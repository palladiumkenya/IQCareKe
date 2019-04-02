import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'app-complete-lab-order',
    templateUrl: './complete-lab-order.component.html',
    styleUrls: ['./complete-lab-order.component.css']
})
export class CompleteLabOrderComponent implements OnInit {

    patientId: number;
    personId: string;

    constructor(private route: ActivatedRoute) {
        this.route.params.subscribe(params => {
            this.patientId = params['patientId'];
            this.personId = params['personId'];
            localStorage.setItem('partnerId', this.personId);
        });
    }

    ngOnInit() {

    }
}

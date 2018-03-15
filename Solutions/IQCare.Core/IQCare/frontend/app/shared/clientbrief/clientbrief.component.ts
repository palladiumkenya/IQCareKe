import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-clientbrief',
  templateUrl: './clientbrief.component.html',
  styleUrls: ['./clientbrief.component.css']
})
export class ClientbriefComponent implements OnInit {

    constructor() { }
    ngOnInit() {
        console.log(localStorage.getItem('personId'));
    }
}

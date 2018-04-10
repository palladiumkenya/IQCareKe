import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
    facilityName: string;

    constructor() { }

    ngOnInit() {
        console.log(localStorage.getItem('appLocation'));
        this.facilityName = localStorage.getItem('appLocation');
    }

}

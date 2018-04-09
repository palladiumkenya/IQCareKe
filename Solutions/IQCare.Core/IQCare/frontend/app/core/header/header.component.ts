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
        this.facilityName = JSON.parse(localStorage.getItem('appLocation'));
    }

}

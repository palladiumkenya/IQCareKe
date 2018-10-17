import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-nav-header',
  templateUrl: './nav-header.component.html',
  styleUrls: ['./nav-header.component.css']
})
export class NavHeaderComponent implements OnInit {
    appUserName: string;
    appdashboard:string=window.location.hostname;;
    constructor() { }

    ngOnInit() {
        this.appUserName = localStorage.getItem('appUserName');
        this.appdashboard= window.location.hostname;
    }

}

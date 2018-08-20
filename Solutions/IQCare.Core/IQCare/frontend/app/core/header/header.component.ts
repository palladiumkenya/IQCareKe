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
        if (!localStorage.getItem('appLocation')) {
           // window.location.href = 'http://' + window.location.hostname + '/IQCare/frmlogin.aspx';
            // console.log('here', localStorage.getItem('appLocation'));
        }
        this.facilityName = localStorage.getItem('appLocation');
    }

}

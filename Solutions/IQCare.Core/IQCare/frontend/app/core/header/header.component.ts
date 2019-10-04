import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
    facilityName: string;
    username: string;

    constructor() { }

    ngOnInit() {
        if (!localStorage.getItem('appLocation')) {
          window.location.href = location.protocol +
                '//' + window.location.hostname + ':' + window.location.port + '/IQCare/frmlogin.aspx';
        }
        this.facilityName = localStorage.getItem('appLocation');
        this.username = localStorage.getItem('appUserName');
    }

}

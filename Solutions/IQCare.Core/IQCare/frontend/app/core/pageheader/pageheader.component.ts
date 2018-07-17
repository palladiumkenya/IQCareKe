import { Component, OnInit } from '@angular/core';
declare var $: any;

@Component({
    selector: 'app-pageheader',
    templateUrl: './pageheader.component.html',
    styleUrls: ['./pageheader.component.css']
})
export class PageheaderComponent implements OnInit {

    constructor() { }

    ngOnInit() {
        setTimeout(() => {
            // Sidebar Toggler
            $('.sidebar-toggler').on('click', function () {
                $('#sidebar').toggleClass('hide');
                $('.sidebar-toggler').toggleClass('active');
                return false;
            });
            // End Sidebar Toggler
        }, 0);
    }

    toggleSideBar() {

    }
}

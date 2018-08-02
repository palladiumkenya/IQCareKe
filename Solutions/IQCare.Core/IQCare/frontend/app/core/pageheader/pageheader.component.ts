import { Component, OnInit } from '@angular/core';
declare var $: any;

@Component({
    selector: 'app-pageheader',
    templateUrl: './pageheader.component.html',
    styleUrls: ['./pageheader.component.css']
})
export class PageheaderComponent implements OnInit {
    isActive: boolean = false;

    constructor() { }

    ngOnInit() {
        const self = this;

        setTimeout(() => {
            // Sidebar Toggler
            $('.sidebar-toggler').on('click', function () {
                $('#sidebar').toggleClass('hide');
                $('.sidebar-toggler').toggleClass('active');

                if (!self.isActive) {
                    self.isActive = true;
                    $('.page-content').css({ 'margin-left': '0' });
                } else {
                    self.isActive = false;
                    $('.page-content').css({ 'margin-left': '224px' });
                }

                return false;
            });
            // End Sidebar Toggler
        }, 0);
    }

    toggleSideBar() {

    }
}

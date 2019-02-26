import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-breadcrumb',
  templateUrl: './breadcrumb.component.html',
  styleUrls: ['./breadcrumb.component.css']
})
export class BreadcrumbComponent implements OnInit {
  afyaMobileUrl : string;

  constructor() { }

  ngOnInit() {
    this.afyaMobileUrl = location.protocol + "//" + location.hostname + ":4747/dashboard";
  }

}

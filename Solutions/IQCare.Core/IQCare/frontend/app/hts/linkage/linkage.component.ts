import { Component, OnInit } from '@angular/core';
import {Linkage} from '../_models/linkage';

@Component({
  selector: 'app-linkage',
  templateUrl: './linkage.component.html',
  styleUrls: ['./linkage.component.css']
})
export class LinkageComponent implements OnInit {
    linkage: Linkage;

    constructor() { }
    ngOnInit() {
        this.linkage = new Linkage();
    }

    onSubmit() {
        console.log(this.linkage);
    }
}

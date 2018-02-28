import { Component, OnInit } from '@angular/core';
import { ModuleManagerService } from '../_services/module-manager.service';
import { Module } from '../_models/module';

@Component({
  selector: 'app-module-manager',
  templateUrl: './module-manager.component.html',
  styleUrls: ['./module-manager.component.css']
})
export class ModuleManagerComponent implements OnInit {
  modules:Module[];

  constructor(private _module : ModuleManagerService) { }

  ngOnInit() {
    this.getModules();
  }

  getModules(){
    this._module.getModules().subscribe(data => { this.modules = data },error => {});
  }

}

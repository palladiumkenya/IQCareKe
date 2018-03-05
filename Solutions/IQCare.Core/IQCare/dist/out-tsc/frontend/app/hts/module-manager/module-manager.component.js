"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var module_manager_service_1 = require("../_services/module-manager.service");
var ModuleManagerComponent = /** @class */ (function () {
    function ModuleManagerComponent(_module) {
        this._module = _module;
    }
    ModuleManagerComponent.prototype.ngOnInit = function () {
        this.getModules();
    };
    ModuleManagerComponent.prototype.getModules = function () {
        var _this = this;
        this._module.getModules().subscribe(function (data) { _this.modules = data; }, function (error) { });
    };
    ModuleManagerComponent = __decorate([
        core_1.Component({
            selector: 'app-module-manager',
            templateUrl: './module-manager.component.html',
            styleUrls: ['./module-manager.component.css']
        }),
        __metadata("design:paramtypes", [module_manager_service_1.ModuleManagerService])
    ], ModuleManagerComponent);
    return ModuleManagerComponent;
}());
exports.ModuleManagerComponent = ModuleManagerComponent;
//# sourceMappingURL=module-manager.component.js.map
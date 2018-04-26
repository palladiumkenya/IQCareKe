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
var http_1 = require("@angular/common/http");
var Observable_1 = require("rxjs/Observable");
require("rxjs/add/operator/catch");
require("rxjs/add/observable/throw");
var ModuleManagerService = /** @class */ (function () {
    function ModuleManagerService(http) {
        this._url = './api/modules';
        this._http = http;
    }
    ModuleManagerService.prototype.getModules = function () {
        return this._http.get(this._url)
            .catch(this.handleError);
    };
    ModuleManagerService.prototype.handleError = function (err) {
        if (err.status === 404) {
            return Observable_1.Observable.throw('no record(s) found');
        }
        return Observable_1.Observable.throw(err.error);
    };
    ModuleManagerService = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [http_1.HttpClient])
    ], ModuleManagerService);
    return ModuleManagerService;
}());
exports.ModuleManagerService = ModuleManagerService;
//# sourceMappingURL=module-manager.service.js.map
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
var of_1 = require("rxjs/observable/of");
var operators_1 = require("rxjs/operators");
var environment_1 = require("../../../environments/environment");
var httpOptions = {
    headers: new http_1.HttpHeaders({ 'Content-Type': 'application/json' })
};
var EncounterService = /** @class */ (function () {
    function EncounterService(http) {
        this.http = http;
        this.API_URL = environment_1.environment.API_URL;
        this._url = '/api/HtsEncounter';
        this._lookupurl = '/api/lookup';
    }
    EncounterService.prototype.getHtsEncounterOptions = function () {
        var _this = this;
        return this.http.get(this.API_URL + this._lookupurl + '/htsOptions').pipe(operators_1.tap(function (htsoptions) { return _this.log('fetched all hts options'); }), operators_1.catchError(this.handleError('getHtsOptions')));
    };
    EncounterService.prototype.addEncounter = function (encounter, finalTestingResults) {
        var _this = this;
        var body = JSON.stringify(encounter);
        var body2 = JSON.stringify(finalTestingResults);
        console.log(encounter);
        console.log(finalTestingResults);
        return this.http.post(this.API_URL + this._url, body, httpOptions).pipe(operators_1.tap(function (addedEncounter) { return _this.log("added encounter w/ id"); }), operators_1.catchError(this.handleError('addEncounter')));
    };
    EncounterService.prototype.handleError = function (operation, result) {
        var _this = this;
        if (operation === void 0) { operation = 'operation'; }
        return function (error) {
            // TODO: send the error to remote logging infrastructure
            console.error(error); // log to console instead
            // TODO: better job of transforming error for user consumption
            _this.log(operation + " failed: " + error.message);
            // Let the app keep running by returning an empty result.
            return of_1.of(result);
        };
    };
    /** Log a HeroService message with the MessageService */
    EncounterService.prototype.log = function (message) {
        console.log(message);
    };
    EncounterService = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [http_1.HttpClient])
    ], EncounterService);
    return EncounterService;
}());
exports.EncounterService = EncounterService;
//# sourceMappingURL=encounter.service.js.map
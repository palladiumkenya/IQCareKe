"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var common_1 = require("@angular/common");
var http_1 = require("@angular/common/http");
var forms_1 = require("@angular/forms");
var ng_pick_datetime_1 = require("ng-pick-datetime");
var hts_routing_module_1 = require("./hts-routing.module");
var encounter_component_1 = require("./encounter/encounter.component");
var linkage_referral_component_1 = require("./linkage-referral/linkage-referral.component");
var encounter_service_1 = require("./_services/encounter.service");
var pnsform_component_1 = require("./pnsform/pnsform.component");
var pnstracing_component_1 = require("./pnstracing/pnstracing.component");
var family_tracing_component_1 = require("./family-tracing/family-tracing.component");
var family_screening_component_1 = require("./family-screening/family-screening.component");
var HtsModule = /** @class */ (function () {
    function HtsModule() {
    }
    HtsModule = __decorate([
        core_1.NgModule({
            imports: [
                common_1.CommonModule,
                http_1.HttpClientModule,
                hts_routing_module_1.HtsRoutingModule,
                forms_1.FormsModule,
                ng_pick_datetime_1.OwlDateTimeModule,
                ng_pick_datetime_1.OwlNativeDateTimeModule,
            ],
            declarations: [
                encounter_component_1.EncounterComponent,
                linkage_referral_component_1.LinkageReferralComponent,
                pnsform_component_1.PnsformComponent,
                pnstracing_component_1.PnsTracingComponent,
                family_tracing_component_1.FamilyTracingComponent,
                family_screening_component_1.FamilyScreeningComponent
            ],
            exports: [],
            providers: [
                encounter_service_1.EncounterService
            ]
        })
    ], HtsModule);
    return HtsModule;
}());
exports.HtsModule = HtsModule;
//# sourceMappingURL=hts.module.js.map
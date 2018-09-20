"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var router_1 = require("@angular/router");
var encounter_component_1 = require("./encounter/encounter.component");
var linkage_referral_component_1 = require("./linkage-referral/linkage-referral.component");
var pnsform_component_1 = require("./pnsform/pnsform.component");
var pnstracing_component_1 = require("./pnstracing/pnstracing.component");
var family_tracing_component_1 = require("./family-tracing/family-tracing.component");
var family_screening_component_1 = require("./family-screening/family-screening.component");
var routes = [
    {
        path: '',
        component: encounter_component_1.EncounterComponent
    },
    {
        path: 'linkage',
        component: linkage_referral_component_1.LinkageReferralComponent
    },
    {
        path: 'pns',
        component: pnsform_component_1.PnsformComponent
    },
    {
        path: 'pnstracing',
        component: pnstracing_component_1.PnsTracingComponent
    },
    {
        path: 'familytracing',
        component: family_tracing_component_1.FamilyTracingComponent
    },
    {
        path: 'familyscreening',
        component: family_screening_component_1.FamilyScreeningComponent
    }
];
var HtsRoutingModule = /** @class */ (function () {
    function HtsRoutingModule() {
    }
    HtsRoutingModule = __decorate([
        core_1.NgModule({
            imports: [router_1.RouterModule.forChild(routes)],
            exports: [router_1.RouterModule]
        })
    ], HtsRoutingModule);
    return HtsRoutingModule;
}());
exports.HtsRoutingModule = HtsRoutingModule;
//# sourceMappingURL=hts-routing.module.js.map
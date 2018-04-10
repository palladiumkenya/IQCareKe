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
var router_1 = require("@angular/router");
var forms_1 = require("@angular/forms");
var core_routing_module_1 = require("./core-routing.module");
var header_component_1 = require("./header/header.component");
var not_found_component_1 = require("./not-found/not-found.component");
var footer_component_1 = require("./footer/footer.component");
var nav_header_component_1 = require("./nav-header/nav-header.component");
var clientbrief_component_1 = require("./clientbrief/clientbrief.component");
var nav_left_component_1 = require("./nav-left/nav-left.component");
var CoreModule = /** @class */ (function () {
    function CoreModule() {
    }
    CoreModule = __decorate([
        core_1.NgModule({
            imports: [
                common_1.CommonModule,
                http_1.HttpClientModule,
                core_routing_module_1.CoreRoutingModule,
                forms_1.FormsModule
            ],
            declarations: [
                header_component_1.HeaderComponent,
                not_found_component_1.NotFoundComponent,
                footer_component_1.FooterComponent,
                nav_header_component_1.NavHeaderComponent,
                clientbrief_component_1.ClientbriefComponent,
                nav_left_component_1.NavLeftComponent
            ],
            exports: [
                header_component_1.HeaderComponent,
                footer_component_1.FooterComponent,
                router_1.RouterModule,
                nav_header_component_1.NavHeaderComponent,
                clientbrief_component_1.ClientbriefComponent,
                nav_left_component_1.NavLeftComponent
            ],
            providers: []
        })
    ], CoreModule);
    return CoreModule;
}());
exports.CoreModule = CoreModule;
//# sourceMappingURL=core.module.js.map
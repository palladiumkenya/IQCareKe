"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var testing_1 = require("@angular/core/testing");
var module_manager_service_1 = require("./module-manager.service");
describe('ModuleManagerService', function () {
    beforeEach(function () {
        testing_1.TestBed.configureTestingModule({
            providers: [module_manager_service_1.ModuleManagerService]
        });
    });
    it('should be created', testing_1.inject([module_manager_service_1.ModuleManagerService], function (service) {
        expect(service).toBeTruthy();
    }));
});
//# sourceMappingURL=module-manager.service.spec.js.map
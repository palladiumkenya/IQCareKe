"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var testing_1 = require("@angular/core/testing");
var encounter_service_1 = require("./encounter.service");
describe('EncounterService', function () {
    beforeEach(function () {
        testing_1.TestBed.configureTestingModule({
            providers: [encounter_service_1.EncounterService]
        });
    });
    it('should be created', testing_1.inject([encounter_service_1.EncounterService], function (service) {
        expect(service).toBeTruthy();
    }));
});
//# sourceMappingURL=encounter.service.spec.js.map
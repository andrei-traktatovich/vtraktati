/// <reference path="../../../../../app.test-helpers/app-test-helpers.js"/>
/// <reference path="../trkTimeDiff/trkTimeDiff.ctrl.js"/>
"use strict";

describe("time diff controller", () => {

    var scope, controller;

    beforeEach(module("traktat.ui"));

    function getController(props) {
        ({ scope, controller } = window.instantiateController("TimeDiffController", props));
    }

    xit("if diff value is undefined, sets it to 0", () => {
        getController({});
        expect(scope.diff).toEqual(0);
    });
    
    xit("if diff is < -12, sets it to 0", () => {

        getController({ diff: -13 });
        expect(scope.diff).toEqual(0);
    });
    
    xit("if diff is > 12, sets it to 0", () => {

        getController({ diff: 13 });

        expect(scope.diff).toEqual(0);
    });

    it("decrements diff", () => {

        getController({ diff: -11 });
        scope.dec();
        expect(scope.diff).toEqual(-12);
    });

    it("decrements diff only if result does not fall below -12", () => {
        
        getController({ diff: -12 });
        scope.dec();
        expect(scope.diff).toEqual(-12);
    });

    it("increments diff", () => {
        
        getController({ diff: 11 });
        scope.inc();
        
        expect(scope.diff).toEqual(12);
    });

    it("increments diff only if result does not exceed 12", () => {
        
        getController({ diff: 12 });
        scope.inc();

        expect(scope.diff).toEqual(12);
    });

    it("if enabled is undefined, set it to true", () => {

        getController({ diff: 0 });
        expect(scope.enabled).toEqual(true);
    });

})
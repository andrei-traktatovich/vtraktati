/// <reference path="../../../../../Scripts/jasmine/jasmine.js" />
/// <reference path="../../../../../Scripts/angular.min.js" />
/// <reference path="../../../../../Scripts/angular-mocks.js" />

"use strict";

((global, isDebug) => {

    global.createModule = (name, deps) => {
        deps = deps || [];
        try {
            return angular.module(name);
        } catch (e) {
            if (isDebug)
                console.info(`createModule: module ${  name } is unavailable, creating it ... (${ e })`);

            return angular.module(name, deps);
        }
    }

    global.instantiateController = (name, props) => {
        var scope, controller;
        inject(($controller, $rootScope) => {
            scope = $rootScope.$new();
            Object.assign(scope, props);
            controller = $controller(name, { $scope: scope });
        });
        return { scope, controller };
    };

})(window, true);

(() => {
    
    var mod = window.createModule("traktat.ui");
    mod.controller("TimeDiffController", timeDiffController);

    function timeDiffController($scope) {

        normalizeDiffValue();

        $scope.inc = inc;

        $scope.dec = dec;

        function normalizeDiffValue() {
            $scope.diff = $scope.diff || 0;

            if (Math.abs($scope.diff) > 12)
                $scope.diff = 0;
        }

        function dec() {
            if ($scope.diff > -12)
                $scope.diff -= 1;
        }

        function inc() {
            if ($scope.diff < 12)
                $scope.diff += 1;
        }
    }
})();


describe("time diff controller", () => {

    var scope, controller;

    beforeEach(module("traktat.ui"));

    function getController(props) {
        ({ scope, controller } = window.instantiateController("TimeDiffController", props));
    }

    it("if diff value is undefined, sets it to 0", () => {
        getController({});
        expect(scope.diff).toEqual(0);
    });
    
    it("if diff is < -12, sets it to 0", () => {

        getController({ diff: -13 });
        expect(scope.diff).toEqual(0);
    });
    
    it("if diff is > 12, sets it to 0", () => {

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

})
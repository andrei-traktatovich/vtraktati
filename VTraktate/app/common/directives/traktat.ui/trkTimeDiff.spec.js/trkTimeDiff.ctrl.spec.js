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
                console.error(e);

            return angular.module(name, deps);
        }
    }
})(window, true);

(() => {
    var mod = createModule("traktat.ui");
    mod.controller("TimeDiffController", timeDiffController);

    function timeDiffController($scope) {

        normalizeDiffValue();

        $scope.inc = inc;

        $scope.dec = dec;

        function normalizeDiffValue() {
            $scope.diff = $scope.diff || 0;

            if (Math.module($scope.value) > 12)
                $scope.value = 0;
        }

        function dec() {
            if ($scope.value > -12)
                $scope.value += 1;
        }

        function inc() {
            if ($scope.value < 12)
                $scope.value += 1;
        }
    }
})();

describe("time diff controller", () => {

    var scope = {};

    beforeEach(module("traktat.ui"));

    beforeEach(inject(($rootScope) => {
        scope = $rootScope.$new();
    }));

    it("if diff value is undefined, sets it to 0", inject(($controller, $rootScope) => {

        var scope = $rootScope.$new();

        $controller('TimeDiffController', { $scope: scope });

        expect(scope.diff).toEqual(0);
    }));

    it("if diff is < -12, sets it to 0", () => {
        var scope = {
            diff: -13
        };

        // using controller ...
        expect(scope.diff).toEqual(0);
    });

    it("if diff is > 12, sets it to 0", () => {
        var scope = {
            diff: 13
        };

        // using controller ...
        expect(scope.diff).toEqual(0);
    });

    it("decrements diff", () => {
        var scope = {
            diff: -11
        };
        // using controller
        // decrement value 
        expect(scope.diff).toEqual(-12);
    });

    it("decrements diff only if result does not fall below -12", () => {
        var scope = {
            diff: -12
        };
        // using controller
        // decrement value 
        expect(scope.diff).toEqual(-12);
    });

    it("increments diff", () => {
        var scope = {
            diff: 11
        };
        // using controller
        // decrement value 
        expect(scope.diff).toEqual(12);
    });

    it("increments diff only if result does not exceed 12", () => {
        var scope = {
            diff: 12
        };
        // using controller
        // decrement value 
        expect(scope.diff).toEqual(12);
    });

    it("when incrementing value, max = -12");
})
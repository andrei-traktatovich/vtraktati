/// <reference path="../Scripts/jasmine/jasmine.js"/>
/// <reference path="../Scripts/angular.min.js"/>
/// <reference path="../Scripts/angular-mocks.js"/>

/// <reference path="../app/helpers/create-module.js" />

((global) => {
    global.instantiateController = (name, props) => {
        var scope, controller;
        inject(($controller, $rootScope) => {
            scope = $rootScope.$new();
            Object.assign(scope, props);
            controller = $controller(name, { $scope: scope });
        });
        return { scope, controller };
    };
})(window);

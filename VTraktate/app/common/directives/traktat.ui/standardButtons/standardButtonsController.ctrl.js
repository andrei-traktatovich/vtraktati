(function () {
    angular.module('traktat.ui.standardButtons')
    .controller('standardButtonsController', function ($scope) {
        $scope.clicked = $scope.action;
    });
})();
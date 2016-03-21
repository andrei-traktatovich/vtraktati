(function () {
    angular.module('traktat.ui.standardButtons')
    .directive('trkCancelButton', function () {
        return {
            templateUrl: 'app/common/directives/traktat.ui/standardButtons/trkCancelButton/trkCancelButton.tpl.html',
            controller: 'standardButtonsController',
            scope: {
                action: '&',
                disabled: '='
            }
        };
    });
})();
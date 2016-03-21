
angular.module('traktat.ui')
.directive('trkFormError', function () {

    return {
        restrict: 'EA',
        scope: {
            text: '='
        },
        templateUrl: 'app/common/directives/traktat.ui/trkFormError/trkFormError.tpl.html'
    };

});
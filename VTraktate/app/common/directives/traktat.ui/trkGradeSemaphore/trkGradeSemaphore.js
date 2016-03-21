angular.module('traktat.ui')
.directive('trkGradeSemaphore', function () {
    return {
        scope: {
            grade: '=trkGradeSemaphore'
        },
        transclude: true,
        controller: function ($scope) {
            // injectables/configurables? 
            var goodThreshold = 7,
                badThreshold = 6,
                good = 'bg-success',
                medium = 'bg-warning',
                bad = 'bg-danger',
                defaultClass = 'bg-default';

            $scope.getClass = function() {
                if ($scope.grade >= goodThreshold)
                    return good;
                if ($scope.grade < badThreshold)
                    return bad;
                return $scope.grade == null ? defaultClass : medium;
            }

        },
        templateUrl: 'app/common/directives/traktat.ui/trkGradeSemaphore/trkGradeSemaphore.tpl.html'
    }
});
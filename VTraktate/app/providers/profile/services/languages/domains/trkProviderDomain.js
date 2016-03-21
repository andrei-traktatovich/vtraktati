angular.module('app')
.directive('trkProviderDomain', function () {
    return {
        scope: {
            item: '=',
            onRemove: '&',
            readonly: '='
        },
        templateUrl: 'app/providers/profile/services/languages/domains/trkProviderDomain.tpl.html',
        link: function (scope, el, attrs) {
        },
        controller: function ($scope) {

            $scope.getDomainImportance = function () {
                return $scope.item.grade / 3 | 0; // doesn't take into account STARS
            };

            $scope.remove = function () {
                if ($scope.onRemove) 
                    $scope.onRemove();
            };
        }
    }

});
angular.module('app')
.directive('trkProviderServiceLanguageItem', function () {
    return {
        scope: {
            item: '=',
            onRemove: '&'
        },
        templateUrl: 'app/providers/services/languages/trkProviderServiceLanguageItem/trkProviderServiceLanguageItem.tpl.html',
        controller: function ($scope) {
            $scope.remove = function () {
                if ($scope.onRemove)
                    $scope.onRemove();
            }
        }
    };
});
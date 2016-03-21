angular.module('app')
.directive('trkProviderServiceItem', function (GlobalsService) {
    return {
        scope: {
            item: '=',
            onRemove: '&'
        },
        templateUrl: 'app/providers/services/trkProviderService/trkProviderService.tpl.html',
        controller: function ($scope) {
            $scope.lists = {
                currencies: GlobalsService.get('currencies'),
                uoms: GlobalsService.get('serviceUoms')
            };
            $scope.remove = function () {
                if ($scope.onRemove)
                    $scope.onRemove();
            }
        }
    };
});
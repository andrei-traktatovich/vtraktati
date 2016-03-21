angular.module('app')
.directive('trkProviderLanguage', function () {
    return {
        scope: {
            item: '=',
            readonly: '=',
            onEdit: '&',
            onRemove: '&'
        },
        templateUrl: 'app/providers/profile/services/languages/trkProviderLanguage.tpl.html',
        controller: function ($scope) {

            $scope.edit = function () {
                if ($scope.onEdit)
                    $scope.onEdit($scope.item);
            }
            $scope.remove = function () {
                if ($scope.onRemove) {
                    $scope.onRemove($scope.item)
                        .then(function (data) {
                            $scope.item = data;
                        })
                }

            }
        }
    }

});
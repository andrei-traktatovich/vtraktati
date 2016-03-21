angular.module('app')
.directive('trkProviderService', function () {
    return {
        scope: {
            item: "=",
            parentId: '=parent',
            onRemove: '&',
            onEdit: '&',
            readonly: '='
        },
        templateUrl: 'app/providers/profile/services/trkProviderService.tpl.html',
        controller: function ($scope) {

            var remove = $scope.onRemove,
                edit = $scope.onEdit;
            
            $scope.remove = function () {
                if (remove) remove();
            }

            $scope.edit = function () {
                if (edit) edit();
            }
        }
    }
})
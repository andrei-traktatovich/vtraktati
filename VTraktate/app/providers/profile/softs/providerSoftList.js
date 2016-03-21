angular.module('traktat.ui')
.directive('trkProfileSoftList', function () {
    return {
        scope: {
            items: '=',
            providerId: '=',
            readonly: '='

        },
        templateUrl: 'app/providers/profile/softs/provider-soft-list.tpl.html',
        controller: function ($scope, $http, confirmer, GlobalsService) {

            restoreState();

            $scope.beginAddItem = function () {
                $scope.addingItem = true;
                $scope.newItems = { ids: [] };
            }


            $scope.addItem = function () {
                console.log('adding new soft item to db')
                var url = 'api/provider/' + $scope.providerId + '/soft';

                $http.post(url, $scope.newItems)
                .success(success)
                .error(failure);

                function success(result) {

                    $scope.items = $scope.items.concat(result);
                    restoreState();
                }

                function failure() {
                    toastr.error('Я ошипко');
                }
            }





            function restoreState() {
                $scope.addingItem = false;

                $scope.newItems = [];
            }

            $scope.cancelItem = function () {
                restoreState();
            }

            $scope.removeItem = function (item) {
                if (confirmer.yes('Вы действительно хотите удалить это ПО?')) {

                    $http.delete('api/provider/' + $scope.providerId + '/soft/' + item.id)
                    .success(success)
                    .error(failure);

                    function success() {
                        if (item.hasOwnProperty("isDeleted"))
                            item.isDeleted = true;
                        else {
                            $scope.items = _.without($scope.items, item);
                        }
                        toastr.info('Запись о ПО удалена.')
                    }

                    function failure() {
                        toastr.error('Я ошипко')
                    }
                }
            }
        }
    };
});
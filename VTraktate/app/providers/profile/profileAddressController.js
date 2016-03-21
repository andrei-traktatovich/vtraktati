(function () {
    angular.module('app')
    .controller('profileAddressController', profileAddressController);

    function profileAddressController($scope, $http, GlobalsService, _) {
        
        $scope.addressEditing = false;

        $scope.lists = {
            regions: GlobalsService.get('regions')
        };

        $scope.beginEditAddress = function () {
            $scope.address = {
                city: $scope.model.city,
                address: $scope.model.address,
                regionId: $scope.model.region.id
            };
            $scope.addressEditing = true;
        }

        $scope.SaveAddressChanges = function () {
            var url = 'api/provider/' + $scope.model.id + '/address',
                data = $scope.address;
            $http.put(url, data)
            .success(updateView)
            .error(displayError);

            function updateView() {
                $scope.model.city = $scope.address.city;
                $scope.model.address = $scope.address.address;
                $scope.model.region = {
                    id: $scope.address.regionId,
                    name: getRegionById($scope.address.regionId)
                };
                $scope.addressEditing = false;
                toastr.info('Данные об адресе сохранены');

                function getRegionById(id) {
                    var result = _.find($scope.lists.regions, function(region){ return region.id === id; });
                    return result.title;
                }
            }

            function displayError() {
                toastr.error('Я ошипко');
            }
        }

        $scope.CancelAddressChanges = function () {
            $scope.addressEditing = false;
            $scope.address = {};
        }
    }

})();
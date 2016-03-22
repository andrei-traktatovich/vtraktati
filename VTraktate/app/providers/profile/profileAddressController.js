(function () {
    angular.module('app')
    .controller('profileAddressController', profileAddressController);

    function profileAddressController($scope, $http, GlobalsService, _) {
        
        $scope.addressEditing = false;

        $scope.lists = {
            regions: GlobalsService.get('regions'),
            legalForms: GlobalsService.get('legalForms')
        };

        $scope.beginEditAddress = function () {
            $scope.address = {
                city: $scope.model.city,
                address: $scope.model.address,
                regionId: $scope.model.region.id,
                legalFormId: $scope.model.legalForm.id,
                worksNightly: $scope.model.worksNightly,
                timeDifference : $scope.model.timeDifference 
            };
            $scope.addressEditing = true;
        }

        $scope.SaveAddressChanges = function () {
            var url = 'api/provider/' + $scope.model.id + '/address',
                data = $scope.address;
            console.log("sending provider address update data", data);

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
                $scope.model.timeDifference = $scope.address.timeDifference;
                $scope.model.worksNightly = $scope.address.worksNightly;
                
                $scope.model.legalForm = {
                    id: $scope.address.legalFormId,
                    name: getLegalFormNameById($scope.address.legalFormId)
                }; 

                $scope.addressEditing = false;
                toastr.info('Данные об адресе сохранены');
                function getLegalFormNameById(id) {
                    var result = _.find($scope.lists.legalForms, function (lf) { return lf.id === id; });
                    return result.name;
                }
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
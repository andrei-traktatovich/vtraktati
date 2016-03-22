(function() {
    angular.module('app')
        .controller('profileAddressController', profileAddressController)
        .directive("trkProviderAddress", function() {
            return {
                scope: {
                    vm: "=model"
                },
                controller: "profileAddressController",
                templateUrl: "app/providers/profile/trkProviderAddress.tpl.html"
            };
        });

    function profileAddressController($scope, $http, GlobalsService, _) {
        console.log("enter profileAddress controller");
        console.log("scope = ", $scope);

        $scope.addressEditing = false;

        $scope.lists = {
            regions: GlobalsService.get('regions'),
            legalForms: GlobalsService.get('legalForms')
        };
        
        $scope.beginEditAddress = function () {
            console.log("view model is ", $scope.vm);
            console.log("view model on $scope is ", $scope);
            $scope.address = {
                city: $scope.vm.city,
                address: $scope.vm.address,
                regionId: $scope.vm.region.id,
                legalFormId: $scope.vm.legalForm.id,
                worksNightly: $scope.vm.worksNightly,
                timeDifference : $scope.vm.timeDifference 
            };
            $scope.addressEditing = true;
        }
        
        $scope.SaveAddressChanges = function () {
            var url = 'api/provider/' + $scope.vm.id + '/address',
                data = $scope.address;
            console.log("sending provider address update data", data);

            $http.put(url, data)
            .success(updateView)
            .error(displayError);

            function updateView() {
                $scope.vm.city = $scope.address.city;
                $scope.vm.address = $scope.address.address;
                $scope.vm.region = {
                    id: $scope.address.regionId,
                    name: getRegionById($scope.address.regionId)
                };
                $scope.vm.timeDifference = $scope.address.timeDifference;
                $scope.vm.worksNightly = $scope.address.worksNightly;
                
                $scope.vm.legalForm = {
                    id: $scope.address.legalFormId,
                    name: getLegalFormNameById($scope.address.legalFormId)
                }; 

                $scope.addressEditing = false;
                toastr.info("Данные об адресе сохранены");
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
                toastr.error("Я ошипко");
            }
        }

        $scope.CancelAddressChanges = function () {
            $scope.addressEditing = false;
            $scope.address = {};
        }
       
    }

})();
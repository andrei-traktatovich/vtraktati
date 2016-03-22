(function () {
    angular.module('app')
        .controller('profileAddressController', profileAddressController)
        .directive("trkProviderAddress", function () {
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
        $scope.saveAddressChanges = saveAddressChanges;
        $scope.cancelAddressChanges = cancelAddressChanges;
        $scope.beginEditAddress = beginEditAddress;
        $scope.getLegalFormName = getLegalFormName;
        $scope.getRegionName = getRegionName;
        $scope.lists = {
            regions: GlobalsService.get('regions'),
            legalForms: GlobalsService.get('legalForms')
        };

        function beginEditAddress() {
            console.log("view model is ", $scope.vm);
            console.log("view model on $scope is ", $scope);
            $scope.address = angular.copy($scope.vm);
            $scope.addressEditing = true;
        }

        function getLegalFormName(id) {
            return GlobalsService.getNameById("legalForms", id);
        }

        function getRegionName(id) {
            return GlobalsService.getNameById("regions", id);
        }




        function saveAddressChanges() {
            var url = 'api/provider/' + $scope.vm.id + '/address',
                data = $scope.address;
            console.log("sending provider address update data", data);

            $http.put(url, data)
            .success(updateView)
            .error(displayError); // TODO: put it on rootScope !!! 

            function updateView() {
                angular.extend($scope.vm, $scope.address); // CHECK HOW THIS WORKS!!! 
                $scope.addressEditing = false;
                toastr.info("Данные об адресе сохранены");
            }

            function displayError() {
                toastr.error("Я ошипко");
            }
        }


        function cancelAddressChanges() {
            $scope.addressEditing = false;
            $scope.address = {};
        }

    }

})();
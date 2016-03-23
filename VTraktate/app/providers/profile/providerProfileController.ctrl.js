(() => {
    angular.module("providers.profile")
        .controller("providerProfileController", providerProfileController);

    function providerProfileController($scope, $stateParams, $http, ActionAuthorizationService) {
        
        var id = $stateParams.id;

        $scope.readonly = !ActionAuthorizationService.isHR();

        $http.get("/api/provider", { params: { id: id } })
            .success(function(result) {
                console.log("profile received", result);
                $scope.model = result;
                console.log("scope model is now", $scope.model)
            });

        $scope.contactsCount = function() {
            return $scope.model && $scope.model.contactPersons && $scope.model.contactPersons.length || "нет";
        }
        $scope.servicesCount = function() {
            return $scope.model && $scope.model.services && $scope.model.services.length || "нет";
        }
    }

})();
(() => {
    angular.module("providers.profile")
        .controller("providerProfileController", providerProfileController);

    function providerProfileController($scope, $state, $stateParams, $http, notifyClient, ActionAuthorizationService, setContactPersonName) {
        
        var id = $stateParams.id;

        $scope.readonly = !ActionAuthorizationService.isHR();
        if (id)
            $http.get("/api/provider", { params: { id: id } })
                .success(function(result) {

                    $scope.model = result;

                });
        else
            $scope.model = null;
            
        $scope.contactsCount = function() {
            return $scope.model && $scope.model.contactPersons && $scope.model.contactPersons.length || "нет";
        }
        $scope.servicesCount = function() {
            return $scope.model && $scope.model.services && $scope.model.services.length || "нет";
        }

        $scope.renameContactPerson = renameContactPerson;

        function renameContactPerson(person) {
            setContactPersonName(person.id, person.personName).then($state.current.reload, notifyClient.error);
        }
    }
})();
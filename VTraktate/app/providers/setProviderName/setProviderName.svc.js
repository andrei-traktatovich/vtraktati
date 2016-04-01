(() => {
    "use strict";

    angular.module("providers")
        .factory("setProviderName", setProviderName)
        .controller("setProviderNameDialogController", setProviderNameDialogController);

    function setProviderNameDialogController($scope, model, $modalInstance) {

        $scope.model = model;

        $scope.cancel = cancel;

        $scope.ok = ok;

        function ok() {
            $modalInstance.resolve(model);
        }

        function cancel() {
            $modalInstance.dismiss("Переименование отменено");
        }
    }

    function setProviderName($modal, $q, people) {
        
        return (personId) => {

            var deferred = $q.defer();

            $modal.open({
                    templateUrl: "app/providers/setProviderName/dialog.tpl.html",
                    resolve: {
                        model: () => people.getFullName(personId)
                    },
                    size: "lg",
                    controller: "setProviderNameDialogController"
                })
                .result
                .then(doRenameProvider, deferred.reject);

            function doRenameProvider(model) {
                people.setFullName(personId, model)
                    .then(deferred.resolve, deferred.reject);
            }

            return deferred.promise;
        };
    }

})();

(() => {

    angular.module("persons")
    .factory("people", people);

    var makeNameUrl = (personId) => `api/people/${personId}fullname`;

    function people($http) {
        return {
            getFullName,
            setFullName
            };

        function getFullName(personId) {
            return $http.get(makeNameUrl(personId));
        }

        function setFullName(personId, name) {
            return $http.put(makeNameUrl(personId), name);
        }
    }

})();


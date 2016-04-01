(() => {
    "use strict";

    angular.module("providers")
        .factory("setContactPersonName", setContactPersonName)
        .controller("setContactPersonNameDialogController", setContactPersonNameDialogController);

    function setContactPersonNameDialogController($scope, model, $modalInstance) {

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

    function setContactPersonName($modal, $q, people) {
        
        return (personId, personName) => {

            var deferred = $q.defer();

            $modal.open({
                    templateUrl: "app/providers/contacts/contactPersonName/dialog.tpl.html",
                    resolve: {
                        model: () => personName,
                        id: () => personId
                    },
                    size: "lg",
                    controller: "setContactPersonNameDialogController"
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




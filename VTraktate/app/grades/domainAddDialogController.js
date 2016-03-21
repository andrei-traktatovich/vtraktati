(function () {
    angular.module('grades')
    .controller('domainAddDialogController', domainAddDialogController);

    function domainAddDialogController($scope, $modalInstance, gradesService, config) {
        $scope.config = config;
        $scope.model = {
            stars: 0,
            comment: '',
            domainIds: [config.domain.id]
        };

        $scope.cancel = function () {
            $modalInstance.dismiss();
        }

        $scope.ok = save;

        function save() {

            gradesService.addDomain(config.languageInfoId, config.domain.id, $scope.model)
            .success(closeDialog)
            .fail(displayError);

            function closeDialog() {
                $modalInstance.close(true);
            };

            function displayError(data) {
                $$scope.errorMessage = data;
            }
        }
    }
})();
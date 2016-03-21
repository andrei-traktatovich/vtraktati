(function () {
    angular.module('grades')
    .controller('addUpdateGradeController', addUpdateGradeController);

    function addUpdateGradeController($scope, $modalInstance, gradesService, model, config, GlobalsService) {

        if (config) {
            $scope.providerName = config.providerName;
            $scope.providerKnown = config.providerKnown;
        }

        var method = !!model.providerId ? 'update' : 'add';

        $scope.ok = save;
        $scope.cancel = cancel;
        $scope.lists = {
            languages: GlobalsService.get('languages'),
            domains: GlobalsService.get('domains'),
            providers: $scope.providerKnown ? [] : GlobalsService.get('providers')
        };
        
        $scope.model = model;

        function save() {
            gradesService[method](model)
            .then(success, fail);

            function success(data) {
                $modalInstance.close(data);
            }

            function fail(error) {
                console.log('grade update error');
                console.log(error);
                $scope.errorMessage = error.data || "Неизвестная ошибка, сохранение невозможно";
            }
        }

        function cancel() {
            $modalInstance.dismiss();
        }

    }
})();
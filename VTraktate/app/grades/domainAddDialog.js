(function () {
    angular.module('grades')
    .service('domainAddDialog', domainAddDialog);

    function domainAddDialog($modal, gradesService) {
        return {
            open: open
        };

        function open(config) {
            var modalInstance = $modal.open({
                templateUrl: 'app/grades/domainAddDialog.tpl.html',
                controller: 'jobEditController',
                resolve: {
                    config: function () { return config; }
                }
            });

            return modalInstance.result;
        }
    }
})();
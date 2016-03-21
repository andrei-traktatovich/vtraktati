angular.module('app')
.directive('trkProviderEmployment', function () {
    return {
        scope: {
            employment: '='
        },
        templateUrl: 'app/providers/employment/formtemplate/trkProviderEmployment.tpl.html',
        controllerAs: 'model',
        controller: function ($scope, GlobalsService) {
            var model = this;

            model.data = $scope.employment;

            model.lists = {
                employmentStatuses: GlobalsService.get('employmentStatuses'),
                titles: GlobalsService.get('titles'),
                offices: GlobalsService.get('offices')
            };
        }
    }

});
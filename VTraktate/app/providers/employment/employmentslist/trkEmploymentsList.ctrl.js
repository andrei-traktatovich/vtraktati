angular.module('app')
.directive('trkEmploymentsList', function () {
    return {
        scope: {
            items: '='
        },
        templateUrl: 'app/providers/employment/employmentslist/trkemploymentslist.tpl.html'
    }
});
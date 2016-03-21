angular.module('app')
.directive('trkFreelancesList', function () {
    return {
        scope: {
            items: '='
        },
        templateUrl: 'app/providers/freelance/freelanceslist/trkfreelanceslist.tpl.html'
    }
});
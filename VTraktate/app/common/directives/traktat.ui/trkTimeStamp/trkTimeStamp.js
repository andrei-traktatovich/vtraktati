angular.module('traktat.ui')
.directive('trkTimeStamp', function () {
    return {
        scope: {
            item: '='
        },
        templateUrl: 'app/common/directives/traktat.ui/trkTimeStamp/trkTimeStamp.tpl.html'
    };
});
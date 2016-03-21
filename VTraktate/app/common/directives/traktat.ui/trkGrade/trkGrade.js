angular.module('traktat.ui')
.directive('trkGrade', function () {
    return {
        templateUrl: 'app/common/directives/traktat.ui/trkGrade/trkGrade.tpl.html',
        scope: {
            grade: '='
        }
    }
})
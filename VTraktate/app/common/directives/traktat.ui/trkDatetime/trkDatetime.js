angular.module('traktat.ui')
.directive('trkDatetime', function() {
    var formats = {
        'date': 'DD.MM.YY',
        'datetime': 'DD.MM.YY HH:mm'
    },
        defaultFormatName = 'datetime',
        defaultFormat = formats[defaultFormatName];

    return {
        scope: {
            mode : '@trkDtMode',
            val: '=trkDtVal'
        },
        template: '<span>{{ datetime }}</span>',
        controller: function ($scope) {

            var mode = $scope.mode || defaultFormatName;

            if ($scope.val) {
                $scope.datetime = moment($scope.val).format(formats[mode] || defaultFormat);
            }
            else
                $scope.datetime = '';
        }
    }
});
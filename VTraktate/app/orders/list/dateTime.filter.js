(function () {
    angular.module('order.list')
    .filter('dateTimeFilter', function () {
        return function (value) {
            if (value)
                return moment(value).format('DD.MM.YY HH:mm'); // formatted value
            else
                return null;
        }
    });
})();
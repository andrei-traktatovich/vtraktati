(function () {
    angular.module('orders.utilities', [
        'globals',
        'dateTimeUtils',
        'LocalStorageModule',
        'orders.utilities.volumeRoundingRules'
    ]);

})();
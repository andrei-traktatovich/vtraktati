
(function (moduleName) {
    angular.module(moduleName)
	.directive('trkVolumeAndPricing', trkVolumeAndPricing);

    function trkVolumeAndPricing() {
        return {
            scope: {
                item: '=',
                roundingRule: '=',
                showCharsAndWords: '='
            },
            templateUrl: 'app/orders/volAndPricing/trkVolumeAndPricing.tpl.html',
            controller: 'trkVolumeAndPricingController'
        };
    }

})('orders');

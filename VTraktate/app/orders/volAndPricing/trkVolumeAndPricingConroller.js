var moduleName = 'orders';
(function (moduleName) {
    angular.module(moduleName)
            .controller('trkVolumeAndPricingController', trkVolumeAndPricingController);

    function trkVolumeAndPricingController($scope, volumeManager, priceCalculator) {

        $scope.onCharsChanged = onCharsChanged;
        $scope.onPagesChanged = onPagesChanged;
        $scope.onWordsChanged = onWordsChanged;
        $scope.onRateChanged = onRateChanged;
        $scope.onDiscountChanged = onDiscountChanged;

        function onCharsChanged() {
            updateVolumeAndPrice(volumeManager.updateFromChars);
        }

        function onPagesChanged() {
            updateVolumeAndPrice(volumeManager.updateFromPages);
        }

        function onWordsChanged() {
            updateVolumeAndPrice(volumeManager.updateFromWords);
        }

        function onRateChanged() {
            updatePrice();
        }

        function onDiscountChanged() {
            updatePrice();
        }

        function updateVolumeAndPrice(func) {
            var rule = ($scope.roundingRule && $scope.roundingRule.func) || null;
            $scope.item.volume = func($scope.item.volume, rule);
            updatePrice();
        }

        function updatePrice() {
            if ($scope.item)
                priceCalculator.updatePrice($scope.item);
        }
    }

})(moduleName);
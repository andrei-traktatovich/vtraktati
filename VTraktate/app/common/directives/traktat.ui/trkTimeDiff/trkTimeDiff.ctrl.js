(() => {
    "use strict";

    var mod = window.createModule("traktat.ui");
    mod.controller("TimeDiffController", timeDiffController);

    function timeDiffController($scope) {

        normalizeDiffValue();
        
        if ($scope.enabled === undefined)
            $scope.enabled = true;

        $scope.inc = inc;

        $scope.dec = dec;

        function normalizeDiffValue() {
            $scope.diff = $scope.diff || 0;

            if (Math.abs($scope.diff) > 12)
                $scope.diff = 0;
        }

        function dec() {
            if ($scope.diff > -12)
                $scope.diff -= 1;
        }

        function inc() {
            if ($scope.diff < 12)
                $scope.diff += 1;
        }
    }
})();
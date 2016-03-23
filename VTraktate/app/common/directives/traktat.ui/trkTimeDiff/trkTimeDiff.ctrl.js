(() => {
    "use strict";
    

    var mod = window.createModule("traktat.ui");
    mod.controller("TimeDiffController", timeDiffController);

    const DEFAULT_TRUTH_VALUE = true;
    
    function timeDiffController($scope) {
        
        console.log("enters trkTimeDiff controller");

        // ACHTUNG: if I do this, I start having issues with inherited scope. 
        
        //normalizeDiffValue();
        
        if ([false, true].indexOf($scope.active) < 0)
            $scope.active = DEFAULT_TRUTH_VALUE;

        $scope.inc = inc;

        $scope.dec = dec;

        //function normalizeDiffValue() {
        //    $scope.diff = $scope.diff || 0;

        //    if (Math.abs($scope.diff) > 12)
        //        $scope.diff = 0;
        //}

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
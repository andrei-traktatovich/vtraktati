
(() => {
    "use strict";
    console.log('enters trkTimeDiff directve');

    window.createModule("traktat.ui")
    .directive("trkTimeDiff", trkTimeDiff);
    
    function trkTimeDiff() {
        return {
            scope: {
                diff: "=",
                active: "="
            },
            templateUrl: "app/common/directives/traktat.ui/trkTimeDiff/trkTimeDiff.tpl.html",
            controller: "TimeDiffController"
        };
    }
})();
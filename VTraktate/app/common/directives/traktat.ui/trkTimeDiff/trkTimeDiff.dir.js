
(() => {
    "use strict";

    window.createModule("traktat.ui")
    .directive("trkTimeDiff", trkTimeDiff);

    function trkTimeDiff() {
        return {
            scope: {
                diff: "=",
                templateUrl: "/common/directive/traktat.ui/trkTimeDiff.trkTimeDiff.tpl.html",
                controller: "TimeDiffController"
            }
        };
    }
});
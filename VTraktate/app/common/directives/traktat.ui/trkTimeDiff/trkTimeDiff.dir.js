
(() => {
    "use strict";

    window.createModule("traktat.ui")
    .directive("trkTimeDiff", trkTimeDiff);

    function trkTimeDiff() {
        return {
            scope: {
                diff: "=",
                enabled: "="
            },
            templateUrl: "/common/directive/traktat.ui/trkTimeDiff.trkTimeDiff.tpl.html",
            controller: "TimeDiffController"
        };
    }
});
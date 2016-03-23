(() => {
    angular.module("blocks", [
        "blocks.error-handling",
        "blocks.nofifyClient"
    ])
    .factory("now", () => () => Date());

})();
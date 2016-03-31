(() => {
    angular.module("providers")
        .directive("trkProvidersFilter", providersFilter);

    function providersFilter() {
        return {
            scope: {
                filters : "="
            },
            controller: "providersFilterCtrl",
            templateUrl: "app/providers/providersFilter/providersFilter.tpl.html"
        }
    }

})();
(function () {
    angular.module('grades')
    .service('defaultTableParams', defaultTableParams);

    function defaultTableParams(gradesService, ngTableParams) {
        return {
            get: get
        };

        function get() {

            return new ngTableParams({
                page: 1,
                count: 20,
                sorting: {  },
                filter: {} 
            }, {
                total: 0,
                counts: [20, 40, 80, 100 ],
                getData: gradesService.get
            });
        }
    }
})();
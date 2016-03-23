(() => {

    angular.module("app")
        .factory("SpinInterceptor", SpinInterceptor);

    function SpinInterceptor($q, $rootScope) {
        return {
            "request": function(config) {
                $rootScope.$emit("http-request");
                return config;
            },
            "response": function(response) {
                $rootScope.$emit("http-response");
                return response;
            },
            "responseError": function(rejection) {
                $rootScope.$emit("http-response-error");
                return $q.reject(rejection); // why is this? 
            }

        };
    }

})();
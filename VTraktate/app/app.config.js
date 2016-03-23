(() => {

    angular.module("app")
        .config([
            "$stateProvider", "$httpProvider", "$urlRouterProvider", "$provide", "$compileProvider", "routesProvider", 
            function($stateProvider, $httpProvider, $urlRouterProvider, $provide, $compileProvider, routesProvider) {

                configureHrefSanitize($compileProvider);
                
                configureGlobalExceptionHandler($provide);
                
                configureHttpInterceptors($httpProvider);

                configureRoutes($stateProvider, $urlRouterProvider, routesProvider.$get());

            }
        ]);

    /* helper functions */

    function configureGlobalExceptionHandler($provide) {
        $provide.decorator("$exceptionHandler", [
            "$log", "$delegate",
            ($log, $delegate) => {
                return function(exception, cause) {
                    $log.debug("Default exception handler.");
                    $delegate(exception, cause);
                };
            }
        ]);
    }
    
    function configureHttpInterceptors($httpProvider) {
        $httpProvider.interceptors.push("ReqAuthInterceptor");
        $httpProvider.interceptors.push("SpinInterceptor");
    }

    function configureRoutes($stateProvider, $urlRouterProvider, routes) {

        $urlRouterProvider.otherwise("/");

        routes.forEach((route) => {
            $stateProvider.state(route.name, route.state);
        });
    }

    function configureHrefSanitize($compileProvider) {
         $compileProvider.aHrefSanitizationWhitelist(/^\s*(https?|ftp|mailto|file|dial):/);
    }


})();

      
(() => {

    angular.module("app")
        .factory("handleHttpErrors", handleHttpErrors)
        .factory("handleAuthErrors", handleAuthErrors)
        .factory("handleApplicationErrors", handleApplicationErrors)
        .factory("handleStateChange", handleStateChange)
        .run(($rootScope, $state, $stateParams, handleHttpErrors, handleAuthErrors, handleApplicationErrors, handleStateChange, $window) => {

            $rootScope.$state = $state;
            $rootScope.$stateParams = $stateParams;

            $rootScope.back = () => {
                $window.history.back();
            }

            handleHttpErrors();

            handleAuthErrors();

            handleApplicationErrors();

            handleStateChange();
        });

    function handleStateChange($rootScope, SessionService) {
        return () => {

            $rootScope.$on("$stateChangeStart", function(event, toState, toParams, fromState, fromParams) {
                console.log("transitioning to state " + toState.name);
            });

            $rootScope.$on("$stateChangeSuccess", function(event, toState, toParams, fromState, fromParams) {
                console.log("done transitioning to state " + toState.name);
            });

            $rootScope.$on("$stateChangeError", function(event, toState, toParams, fromState, fromParams, error) {
                console.log("Error transitioning to state: " + error);
            });

            // intercepting state change to check authorization
            $rootScope.$on("$stateChangeStart",
                function(event, toState, toParams, fromState, fromParams) {
                    SessionService.checkAccess(event, toState, toParams, fromState, fromParams);
                });
        };
    }

    function handleHttpErrors($rootScope, notifyClient) {
        return () => {
            $rootScope.$on("http-error", (event, data) => {
                if (!data)
                    notifyClient.error("Неизвестная ошибка", "Нет данных об ошибке");
                else
                    notifyClient.error(data.title, data.text, data.error);
            });
        };
    }

    function handleAuthErrors($rootScope, notifyClient) {
        return () => {
            $rootScope.$on("auth-error-unauthorized", (event, data) => {
                notifyClient.error("Недостаточно прав", "У вас нет нужных прав");
            });
        };
    }

    function handleApplicationErrors($rootScope, notifyClient) {
        return () => {
            $rootScope.$on("application-error", (event, data) => {
                notifyClient.error("Ошибка клиентского приложения");
            });
        };
    }

})();
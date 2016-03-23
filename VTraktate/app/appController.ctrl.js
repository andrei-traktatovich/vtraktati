(() => {
    angular.module("app")
        .controller("appController", appController);
        
        function appController($scope, $rootScope, User, version, LoginService, StateGateKeeperService, usSpinnerService, mainNavMenu) {
        
            $rootScope.version = version;
            $scope.getUser = User.get;
            $scope.logout = logout;
            // configuring spinner 
            var spinnerActive = false, spinnerPromise;

            $rootScope.$on("http-request", startSpin);
            $rootScope.$on("http-response", stopSpin);
            $rootScope.$on("http-response-error", stopSpin);

            $scope.source = mainNavMenu;;
            $scope.gatekeeper = StateGateKeeperService;
            
            $rootScope.$on("logout", logout);

            function logout() {
                LoginService.logout();
            }

            function startSpin() {
                if (!spinnerActive) {
                    usSpinnerService.spin("spinner-1");
                    spinnerActive = true;
                    spinnerPromise = null;
                }
            }

            function stopSpin() {
                if (spinnerActive) {
                    usSpinnerService.stop("spinner-1");
                    spinnerActive = false;
                }
            }
        }

})();
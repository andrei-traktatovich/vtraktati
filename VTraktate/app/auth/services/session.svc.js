// SessionService
// if a state requires authentication and/or user roles, checks against current user.
// if user is not set, redirects to login page
// if user is set but doesn't have rights, throws error

angular.module('auth')
    .factory('SessionService', ['$rootScope', 'StateGateKeeperService', function ($rootScope, StateGateKeeperService) {

        function checkAccess(event, toState) {
            var result = {};
            try {
                result = StateGateKeeperService.validateStateChange(toState);
            } catch (err) {
                event.preventDefault();
                $rootScope.$state.go("login");
            }
            if (!result.ok) {
                event.preventDefault();
                if (result.authenticationRequired)
                    $rootScope.$state.go('login');
                else if (result.nonAuthorized)
                    $rootScope.$state.go("login");// $rootScope.$emit("auth-error-unauthorized");
                else
                    $rootScope.$emit("application-error", { message: "Неизвестная ошибка раутера" });
            }
        }

        return {
            checkAccess: checkAccess
        };

    }]);

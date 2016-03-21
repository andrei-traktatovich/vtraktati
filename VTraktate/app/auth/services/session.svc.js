// SessionService
// if a state requires authentication and/or user roles, checks against current user.
// if user is not set, redirects to login page
// if user is set but doesn't have rights, throws error

angular.module('auth')
    .factory('SessionService', ['$rootScope', 'StateGateKeeperService', function ($rootScope, StateGateKeeperService) {

        function checkAccess(event, toState) {
            var result = StateGateKeeperService.validateStateChange(toState);

            if (!result.ok) {
                event.preventDefault();
                if (result.authenticationRequired)
                    $rootScope.$state.go('login');
                else
                    if (result.nonAuthorized)
                        $rootScope.alertNonAuthorized();
                    else
                        $rootScope.alertUnknownError('Неизвестная ошибка раутера.');
                
            }
        }

        return {
            checkAccess: checkAccess
        };

    }]);

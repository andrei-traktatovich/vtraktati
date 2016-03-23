// SessionService
// if a state requires authentication and/or user roles, checks against current user.
// if user is not set, redirects to login page
// if user is set but doesn't have rights, throws error

angular.module("auth")
    
    .factory("StateGateKeeperService", ["$rootScope", "AuthorizationService", "User", function ($rootScope, AuthorizationService, User) {

        function validateStateChange(state) {
            var registeredState = $rootScope.$state.get(state),
                user = User.get(),
                result = {};
            console.log("validateStateChange");
            console.log(user);

            if (!registeredState) { // is state passed to this function always valid? 
                $rootScope.$emit("application-error", { message: `Ошибка роутера. Не найдено состояние ${state.name}` });
                throw ("Ошибка раутера: не найдено состояние " + (state.name || state));
            }

            if (registeredState.data && registeredState.data.auth) {
                // if noLogin is falsy or doesn't exit and user is not authenticated, require redirect to login page
                result.authenticationRequired = (registeredState.data.auth.noLogin === false && !user);
                // if user is authenticated, check roles. if user doesn't have the right roles, set authRequired to true
                if (!result.authenticationRequired) {
                    var roles = registeredState.data.auth.roles;
                    result.nonAuthorized = roles ? !AuthorizationService.rolesAuthorized(user, roles) : false;
                }

            }
            // if user is authenticated and roles are ok, then ok, otherwise not ok ...
            result.ok = (!result.authenticationRequired && !result.nonAuthorized);

            return result;
        }

        function authorizeState(state) {
            var validationResult = validateStateChange(state);
            return validationResult.ok === true;
        }

        return {
            validateStateChange: validateStateChange,
            authorizeState: authorizeState
        }

    }]);

// Authorization service
// checks if user has appropriate roles

angular.module('auth')
    .factory('AuthorizationService', [function () {

        var ADMIN = 1;

        // user should have at least one role; at least one of user's roles should match a role from the list
        function hasRole(user, roleId) {
            var roles = user && user.roles;
            return roles && roles.some(function (role) {
                return role == roleId;
            });
        }

        function isAdmin(user) {
            return hasRole(user, ADMIN);
        }

        function isUserInRole(user, roles) {
            if (!user || !user.roles)
                return false;
            
            if (!roles || roles.length === 0)
                return true;

            return angular.isArray(roles) ? roles.some(function (item) { return hasRole(user, item); }) : hasRole(user, roles);
        }

        function rolesAuthorized(user, roles) {
            return isAdmin(user) || isUserInRole(user, roles);
        }

        return {
            rolesAuthorized: rolesAuthorized,
            isAdmin: isAdmin,
            hasRole : hasRole 
        };
    }]);

angular.module('auth')
.factory('ActionAuthorizationService', function (AuthorizationService, User) {
    
    var FREELANCERMANAGER = 11,
        HR = 12;

    return {
        isHR: isHR
    };

    function isHR() {
        return checkRole(FREELANCERMANAGER) || checkRole(HR);
    }

    function checkRole(roleId) {
            var user = User.get();
            if (!user)
                return false;
            return AuthorizationService.isAdmin(user) || AuthorizationService.hasRole(user, roleId);
    }
})

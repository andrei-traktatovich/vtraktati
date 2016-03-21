(function () {
    angular.module('order.list')
    .service('appointmentRoles', appointmentRoles);

    function appointmentRoles() {

        var roles = {
            'default': null
        };

        return function (role) { return roles[role] || roles['default']; };
    }
})();

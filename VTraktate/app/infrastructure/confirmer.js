(function () {
    angular.module('infrastructure')
    .service('confirmer', function () {
        // this one will feature a modal dialog ... 
        return {
            yes: function (whatToConfirm) { return confirm(whatToConfirm); }
        };
    });
})();

(function () {
    angular.module('customerContactPerson')
    .directive('trkCustomerContactPersonGeneral', trkCustomerContactPersonGeneral);

    function trkCustomerContactPersonGeneral() {
        return {
            scope: {
                contactPerson: '=',
                templateUrl: 'app/customerContactPersons/trkCustomerContactPersonGeneral/trkCustomerContactPersonGeneral.tpl.html',
                controller: 'trkCustomerContactPersonGeneral'
            }
        };
    }
})();
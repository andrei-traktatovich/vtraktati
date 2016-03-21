(function () {
    angular.module('customerContactPerson')
    .directive('trkCustomerContactPersonEditor', trkCustomerContactPersonEditor);
    
    function trkCustomerContactPersonEditor() {
        return {
            templateUrl: 'app/customerContactPersons/edit/customerContactPersonEditor.tpl.html',
            scope: {
                contactPerson: '='
            }
        };
    }
    
})();
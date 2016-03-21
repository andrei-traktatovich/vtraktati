// <jobs-table jobs="model.jobs" price-list="priceList" total="total"></jobs-table>

(function () {
    angular.module('order.create')
    .directive('jobsTable', jobsTable);

    function jobsTable() {
        return {
            scope: {
                jobs: '=',
                order: '=',
                initialTotal: '=',
                orderOptions: '='
            },
            templateUrl: 'app/orders/create/jobsTable/jobsTable.tpl.html',
            controller: 'jobsTableController',
            restrict: 'E'
        }
    }

})();
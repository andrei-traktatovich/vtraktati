// <jobs-table jobs="model.jobs" price-list="priceList" total="total"></jobs-table>

(function () {
    angular.module('order.create')
    .controller('jobsTableController', jobsTableController);

    function jobsTableController($scope, jobFactory, orderManager) {
        $scope.jobs = $scope.jobs || [];
        $scope.$watch('jobs', getInitialTotal, true);

        $scope.remove = function (index) {
            $scope.jobs.splice(index, 1);
        }

        function getInitialTotal() {
            $scope.initialTotal = orderManager.initialTotal($scope.jobs);
        }

        $scope.addJob = function () {
            if (!$scope.jobs || !$scope.jobs.length)
                $scope.jobs = [];

            var newJob = jobFactory.add($scope.jobs, {
                customerId: $scope.order ? $scope.order.customerId : null,
                endDate: $scope.order ? $scope.order.plannedDeliveryDate : null
            });

            $scope.jobs.push(newJob);
        };
    }
})();
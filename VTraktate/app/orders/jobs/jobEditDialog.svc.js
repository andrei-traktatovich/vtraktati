(function () {

    angular.module('order.list')
    .service('jobEditDialog', jobEditDialog);

    function jobEditDialog($modal, $http) {
        return {
            open: open
        };

        function open(job, mode) {
            mode = mode || 'edit';
            // get orderOptions

            var modalInstance = $modal.open({
                size: 'lg',
                templateUrl: 'app/orders/jobs/jobEditDialog.tpl.html',
                controller: 'jobEditController',
                resolve: {
                    job: function () { return job; },
                    mode: function() { return mode; }
                }
            });

            return modalInstance.result;
        }


    }
})();

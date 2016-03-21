(function () {
    angular.module('order.list')
    .service('mapJobViewModelToJobEditViewModel', mapJobViewModelToJobEditViewModel)
    .controller('jobEditController', jobEditController);

    function mapJobViewModelToJobEditViewModel(constants) {

        return mapToViewModel;

        function mapToViewModel(source, mode) {
            var job = angular.copy(source);
            return {
                startDate: job.startDate,
                endDate: job.endDate,
                currencyId: job.currency.id,
                UOMId: job.uom.id,
                languageId: job.language ? job.language.id : null,
                jobTypeId: job.jobType ? job.jobType.id : null,
                domain1Id: job.domain1 ? job.domain1.id : null,
                domain2Id: job.domain2 ? job.domain2.id : null,
                statusId: mapJobStatus(source, mode),
                document: job.document,
                initial: job.initial || {},
                final: mapFinal(job.final, mode),
                id: mode === 'edit' ? job.id : null, // a cloned job can't have an id.
                orderId: job.order.id
            };

            function mapFinal(jobFinal, mode) {
                if (mode === 'clone')
                    return {};
                else
                    return jobFinal || {};
            }

            function mapJobStatus(source, mode) {
                if (mode === 'clone')
                    return constants.JOB_STATUSES.JOB_STATUS_CREATED;
                else
                    return source.status ? source.status.id : constants.JOB_STATUSES.JOB_STATUS_CREATED;
            }
        }

    }

    function jobEditController($scope, job, mode, $modalInstance, mapJobViewModelToJobEditViewModel, $http, constants) {

        $scope.job = mapJobViewModelToJobEditViewModel(job, mode);
        // do something with config.

        

        $scope.save = save;
        $scope.cancel = cancel;

        function save() {
            switch (mode) {
                case 'edit': updateJob(); break;
                case 'clone': addJob(); break;
            }
            function updateJob() {
                var url = 'api/job/' + $scope.job.id;
                $http.put(url, $scope.job).success(ok).error(fail);
            }
            
            function addJob() {
                var url = 'api/order/' + $scope.job.orderId + '/append';
                $http.post(url, $scope.job).success(ok).error(fail);
            }
            
            function ok(data) { $modalInstance.close(data); }
            function fail(data) { $scope.errorMessage = data; }
        }
        function cancel() {
            $modalInstance.dismiss();
        }

    }
})();
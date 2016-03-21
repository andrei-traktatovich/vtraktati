(function () {
    angular.module('orders')
    .service('jobValidation', jobValidation);

    function jobValidation(GlobalsService) {
        var jobStatuses = [];
        refresh();
        return {
            refresh: refresh,
            statusRequiresFinals: statusRequiresFinals,
            nonZeroFinalVolumeRequired: nonZeroFinalVolumeRequired
        };

        function refresh() {
            jobStatuses = GlobalsService.get('jobCompletionStatuses');
        }

        function statusRequiresFinals(jobStatusId) {
            var status = _.findWhere(jobStatuses, { id: jobStatusId });
            if (status === undefined)
                throw new Error('invalid job status ' + jobStatusId);
            return status.finalVolumeRequired;
        }

        function nonZeroFinalVolumeRequired(job) {
            throw new Error('nonZeroFinalVolumeRequired not implemented');
        }

    }
})();
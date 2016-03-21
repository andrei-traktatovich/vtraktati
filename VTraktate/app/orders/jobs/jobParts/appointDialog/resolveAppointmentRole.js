(function () {
    angular.module('order.list')
        .service('resolveAppointmentRole', resolveAppointmentRole)

    function resolveAppointmentRole(taskDurationManager, appointmentRoles) {

        return function (jobModel, role) {

            var options = appointmentRoles(role) || resolveDefault(jobModel);
            var timing = resolveTiming(jobModel.startDate, jobModel.endDate, options.isPostProcessing);

            return {
                startDate: timing.startDate,
                endDate: timing.endDate,
                defaultJobTypeId: options.jobTypeId || jobModel.jobType.id
            };

            function resolveTiming(startDate, endDate, isPostProcessing) {
                var pivotalDate = taskDurationManager.adjustedPivotalDate(jobModel.startDate, jobModel.endDate);
                return {
                    startDate: isPostProcessing ? pivotalDate : startDate,
                    endDate: isPostProcessing ? endDate : pivotalDate
                };
            }

            function resolveDefault(jobModel) {
                return {
                    isPostProcessing: false,
                    jobTypeId: jobModel.jobType.id
                };
            }
        }
    }
})();
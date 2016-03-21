(function () {
    angular.module('order.list')
        .service('computeInitialJobParticipantVolume', computeInitialJobParticipantVolume)
        .service('appointmentCreateModelFactory', appointmentCreateModelFactory);
        

    function computeInitialJobParticipantVolume() {
        return function (jobModel) {
            // nothing more sophisticated this time, 
            // because I will need to take care of many tricky parameters ... 
            if (!jobModel.jobParts || !jobModel.jobParts.length)
                return jobModel.initial.volume;
            else return {};
        }
    }
    
    function appointmentCreateModelFactory(constants, resolveAppointmentRole, computeInitialJobParticipantVolume) {
        return function (jobModel, config) {
            if (!jobModel)
                throw new Error('empty job model provided');
            var defaultConfig = {
                providerId: null,
                workInHouse: false,
                role: null,
            };

            config = config || defaultConfig;
            var initialVolume = computeInitialJobParticipantVolume(jobModel);
            var appointmentOptions = resolveAppointmentRole(jobModel, config.role);


            var model = {
                providerId: config.providerId,
                jobId: jobModel.id,
                currencyId: constants.CURRENCIES.RUBLE,
                UOMId: constants.DEFAULTSERVICEUOM,
                jobTypeId: appointmentOptions.defaultJobTypeId || jobModel.jobType.id,
                languageId: jobModel.language ? jobModel.language.id : null,
                domain1Id: jobModel.domain1 ? jobModel.domain1.id : null,
                domain2Id: jobModel.domain2 ? jobModel.domain2.id : null,
                startDate: appointmentOptions.startDate,
                workInHouse: config.workInHouse,
                statusId: constants.JOB_PART_STATUSES.JOB_PART_STATUS_CREATED,
                endDate: appointmentOptions.endDate,
                initial: {
                    volume: initialVolume,
                    pricing: {}
                }
            };
            return model;
        };
    }
})();
    
(function () {
    angular.module('order.list')
        .service('appointDialog', appointDialog);

    function appointDialog($modal, appointmentCreateModelFactory) {
        return {
            open: open
        };

        function open(jobModel, config) {
            if (!jobModel)
                throw new Error('appointDialog: no jobModel specified');

            config = config || {};
            
            var modalInstance = $modal.open({
                templateUrl: 'app/orders/jobs/jobParts/appointDialog/appointDialog.tpl.html',
                controller: 'appointDialogController',
                size: 'lg',
                resolve: {
                    model: function () {
                        return appointmentCreateModelFactory(jobModel, config);
                    },
                    config: function() {
                        return {
                            appointmentSource: config.appointmentSource
                        }
                    }
                }
            });
            return modalInstance.result;
        }
    }
})();
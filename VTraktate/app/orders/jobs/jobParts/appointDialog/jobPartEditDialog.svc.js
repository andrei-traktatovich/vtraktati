(function () {
    angular.module('order.list')
        .service('jobParticipantEditDialog', jobParticipantEditDialog);

    function jobParticipantEditDialog($modal, appointmentEditModelFactory) {
        return {
            open: open
        };

        function open(jobPart) {
            var modalInstance = $modal.open({
                templateUrl: 'app/orders/jobs/jobParts/appointDialog/appointDialog.tpl.html',
                controller: 'appointDialogController',
                size: 'lg',
                resolve: {
                    model: function () {
                        return appointmentEditModelFactory(jobPart);
                    },
                    config: function () {
                        return {
                            providerName: jobPart.provider.name
                        };
                    }
                }
            });
            return modalInstance.result;
        }
    }
})();
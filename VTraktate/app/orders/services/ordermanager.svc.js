(function () {
    angular.module('orders.utilities')
    .service('orderManager', orderManager);

    function orderManager(taskDurationManager, jobInfoStorage, volumeRoundingRuleLocator) {

        return {
            initialTotal: initialTotal,
            create: create,
            save: save,
            saveTemplate: saveTemplate,
            storeJobRates: storeJobRates,
            getVolumeRoundingRule: getVolumeRoundingRule,
            setCustomerId: setCustomerId,
            getOrderOptions: getOrderOptions
        };

        // test this
        function getVolumeRoundingRule(roundingPolicyId) {
            return volumeRoundingRuleLocator.getRule(roundingPolicyId);
        }
        function getOrderOptions(customerProfile) {
            return {
                roundingRule: getVolumeRoundingRule(customerProfile.roundingPolicyId),

                // pricelist is not currently supported, so this will always be null
                pricelist: customerProfile.priceList || null,

                orderLiteral: customerProfile.orderLiteral,
                isIndividual: customerProfile.isIndividual
            };
        }

        function setCustomerId(order, customerId) {
            order.customerId = customerId;
            order.jobs.forEach(function (item) {
                item.customerId = customerId;
            });
            return order;
        }

        //describe create
        function create(template, officeId, startDate, defaultJobEndDate) { 
            // throw if no officeId 
            if (!officeId || isNaN(officeId))
                throw new Error('Невозможно создать заказ, т.к. не указан офис, в котором создается заказ');
            defaultJobEndDate = defaultJobEndDate || taskDurationManager.defaultJobEndDate();
            startDate = startDate || new Date();
            // set mandatory properties
            var emptyOrder = {
                jobs: [],
                officeId: officeId,
                startDate: startDate,
                plannedDeliveryDate: defaultJobEndDate
            };
            
            // extend with template
            var order = angular.extend({}, emptyOrder, template);

            // ensure that jobs array is instantiated
            if (!order.jobs || !angular.isArray(order.jobs))
                order.jobs = [];

            return order;
        }

        function initialTotal(jobs) {
            if (!jobs || !jobs.length)
                return 0;

            return jobs.reduce(function (sum, item) {
                if (item &&
                    item.initial &&
                    item.initial.pricing)
                    return sum + (item.initial.pricing.discountedPrice || 0);
                else return sum;
            }, 0);
        }
        
        function saveTemplate(order, templateName) {
            orderTemplates.saveTemplate($scope.templateName, $scope.model);

            var template = orderTemplateFromOrderCreateModel.create($scope.model);
            orderTemplates.set($scope.templateName, template);
        }

        function storeJobRates(jobs) {
            jobs.forEach(jobInfoStorate.store);
        }

        function save(order, shouldStoreRates) {
            if (shouldStoreRates === undefined)
                shouldStoreRates = true;
            // store info in localStorage
            if (shouldStoreRates) {
                storeRates(order.jobs);
            }

            var url = 'api/order/create';
            // if post is successful & templateName is provided and is a string,
            // save the template 
            // return promise 
            return $http.post(url, order);
        }
    }
})();
(function () {

    angular.module('order.create')
    .controller('newOrderController', newOrderController);

    function newOrderController($scope, customers, GlobalsService, orderManager, $state, $stateParams) {

        // init 
        // init datetimepicker ==> encapsulate this somewhere 
        // can't encapsulate but test if this is done by default via localization ?
        $scope.dateOptions = {
            startingDay: 1,
            showWeeks: false
        };

        // init lists 
        // TODO: check if these lists are sufficient
        GlobalsService.populateLists('jobTypes,languages,currencies', $scope);

        // instantiate model based on template and officeId provided in stateParams
        try {
            $scope.model = orderManager.create($stateParams.template, $stateParams.officeId);
        }
        catch (err) {
            // make order invalid and display message if error thrown while instantiating model
            invalidateOrder(err);
        }

        // init order number by setting it to null (number will be assigned automatically when saving the order)
        disableManualNumber();

        // init error message & set it to null.
        // form can't be saved if errorMessage is not null
        invalidateOrder();

        // customers and their profiles & contact persons
        $scope.onCustomerChanged = function loadCustomerProfile(customer) {

            if (!customer)
                invalidateOrder('Внутренняя ошибка: пустой заказчик. Этого не может быть. Это очень странно.');
            else {
                if (!$scope.model)
                    throw 'newOrderController: order model empty or nonexistent';

                customers.getProfile(customer.id, $scope.model.officeId)
                    .success(function (data) {

                        $scope.orderOptions = orderManager.getOrderOptions(data);
                        
                        $scope.customer = customer;
                        orderManager.setCustomerId($scope.model, customer.id);

                        $scope.model.contactPerson = data.defaultContactPerson || null;
                        // Do I use this? 
                        //$scope.contactPersons = data.contactPersons || [];
                    })
                    .error(toastr.error);
            }
        };

        $scope.refreshCustomers = function refreshCustomers(val) {
            customers.getOptions(val).success(function (data) {
                $scope.customers = data;
            });
        };

        $scope.refreshContactPersons = function refreshContactPersons(val) {
            customers.getContactPersons($scope.model.customerId, val).success(function (data) {
                $scope.contactPersons = data;
            });
        }

        $scope.onContactPersonSelected = function onContactPersonSelected($item, $model) {
            // TODO: redo this to accomodate the new way I'm storing info about contact persons.
            $scope.model.contactPerson = {
                fullName: $item.fullName,
                phone: $item.phone,
                email: $item.email,
                ext: $item.ext
            };
        };

        // order number - manual or automatic 
        $scope.enableManualNumber = function enableManualNumber() {
            $scope.manualNumberEnabled = true;
        };

        $scope.disableManualNumber = function disableManualNumber() {
            $scope.manualNumberEnabled = false;
            $scope.model.number = null;
        };

        // save, cancel and invalidate order 
        $scope.save = function saveOrder() {
            var saveTemplate = $scope.storeTemplate && $scope.templateName;
            orderManager.save($scope.model)
            .success(function (data) {
                var message = '';
                if (saveTemplate) {
                    orderManager.saveTemplate($scope.model, $scope.templateName);
                    message = '. Заказ сохранен в качестве образца под именем ' + $scope.templateName;
                }

                toastr.success('Вы успешно создали заказ № ' + data.name + message);
                closeForm();
            })
            .error(invalidateOrder);

        };

        $scope.cancel = function cancelCreateOrder() {
            closeForm();
        };

        function closeForm() {
            $state.go('orders.list'); // rather, go to previous state
        }

        function invalidateOrder(errorMessage) {
            // set error message & ensure that form cannot be submitted
            $scope.errorMessage = errorMessage || null;
        }
    }

})();
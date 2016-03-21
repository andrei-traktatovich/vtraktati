(function() {

    angular.module('providers.profile.services')
    .directive('trkServicesList', trkServicesList)
    .controller('trkServicesListController', trkServicesListController)
    .controller('trkServiceAddUpdateController', trkServiceAddUpdateController)
    .service('addUpdateServiceDialog', addUpdateServiceDialog);

    function trkServicesList() {
        return {
            scope: {
                items: '=',
                providerId: '=parent',
                readonly: '='
            },
            templateUrl: 'app/providers/profile/services/trkServicesList.tpl.html',
            controller: 'trkServicesListController'
        };
    };

    function addUpdateServiceDialog($modal) {
        
        return {
            open: open
        };

        function open(model, config) {
            console.log('add service dialog config')
            console.log(config);
            var modalInstance = $modal.open({
                templateUrl: 'app/providers/profile/services/AddUpdate/addUpdateProviderService.tpl.html',
                resolve: {
                    model: function () { return model; },
                    config: function () { return config; }
                },
                controller: 'trkServiceAddUpdateController'
            });

            return modalInstance.result;
        };
    }

    function trkServicesListController($scope, confirmer, servicesManager, toastr, addUpdateServiceDialog) {

        $scope.remove = function (service) {
            
            if (confirmer.yes('Вы действительно хотите удалить услугу ' + service.serviceTypeName + '?')) {
                var serviceId = service.id;

                servicesManager.remove(serviceId)
                .success(function () {
                    var text = 'Услуга ' + service.serviceTypeName + ' удалена',
                        index = $scope.items.indexOf(service);

                    $scope.items.splice(index, 1);
                    toastr.success(text);
                });
            }
        };

        $scope.edit = function (service) {
            var config = {
                serviceName: service.serviceTypeName,
                isLinguistic: service.needsLanguage
            };
            // service var contains way too many properties ...
            addUpdateServiceDialog.open(service, config)
            .then(function success(editedService) {
                var index = $scope.items.indexOf(service);
                $scope.items[index] = merge($scope.items[index], editedService);
                toastr.success('Информация об услуге ' + service.serviceTypeName + ' обновлена');

                // undescore.js ? or encapsulate ?? 
                function merge(destination, source) {
                    var properties = ['comment', 'qaStars', 'uomName', 'currencyName', 'modifiedByName', 'modifiedDate' ];

                    properties.forEach(function (key) { destination[key] = source[key]; });
                    
                    return destination;
                }
            });
        }

        $scope.addService = function () {
            var config = {
                exclude: $scope.items
            },
                newItem = servicesManager.create({ providerId : $scope.providerId });
            console.log('add service config')
            console.log(config);
            addUpdateServiceDialog.open(newItem, config)
                .then(function success(item) {
                    $scope.items = $scope.items.concat(item);
            });
        };
    }

    function trkServiceAddUpdateController($scope, $modalInstance, GlobalsService, config, model, servicesManager) {
        // const, TODO: encapsulate do a service.
        var DEFAULT_SERVICE_UOM = 2,
            UNKNOWN_UOM = 1;

        console.log('enters trkserviceAddUpdateController');
        console.log(config)
        $scope.model = model;

        var adding = !model.id; // null id => new service;

        $scope.addingService = adding;
        
        $scope.title = getTitle(adding, config);

        $scope.lists = getLists();
        
        if (!adding) {
            $scope.serviceName = config.serviceName;
            toggleFields(config.isLinguistic);
        }
        
        $scope.toggleServiceType = toggleServiceType;
        $scope.ok = ok;
        $scope.cancel = cancel;

        function toggleServiceType(serviceType) {
            var isLinguistic = serviceType.requiresLanguage;
            toggleFields(isLinguistic);
        }

        function toggleFields(isLinguistic) {
            $scope.showRate = !isLinguistic;
            $scope.showUom = isLinguistic;
            if (isLinguistic) {
                $scope.model.minRate = null;
                $scope.model.maxRate = null;
                $scope.model.uomId = DEFAULT_SERVICE_UOM;
            }
            else {
                $scope.model.uomId = UNKNOWN_UOM;
            }
        }

        function ok() {
            servicesManager.save($scope.model)
            .success($modalInstance.close)
            .error(displayError);

            function displayError(errorMessage) {
                $scope.errorMessage = errorMessage || { message: 'Ошибка. Не удалось сохранить данные.' };
            }
        };

        function cancel() {
            $modalInstance.dismiss();
        };

        function getLists() {
            console.log('exclude list');
            console.log(config.exclude);
            return {
                services: GlobalsService.getExcept('serviceTypes', config && config.exclude, 'serviceTypeId'),
                serviceUoms: GlobalsService.get('serviceUoms'),
                currencies: GlobalsService.get('currencies')
            };
        }

        function getTitle(adding, config) {
            if (config.title)
                return config.title;
            else
                return adding ? 'Добавление услуги' : 'Редактирование услуги';
        }
    }
})();

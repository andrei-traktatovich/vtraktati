(function () {

    angular.module('app')
        .service('trkProviderDomainsAddDialog', trkProviderDomainsAddDialog)
        .service('trkProviderDomainUpdateDialog', trkProviderDomainUpdateDialog)
        .service('domainsManager', domainsManager)
        .controller('trkProviderDomainUpdateDialogController', trkProviderDomainUpdateDialogController)
        .controller('trkProviderDomainsAddDialogController', trkProviderDomainsAddDialogController)
        .controller('trkProviderDomainsListController', trkProviderDomainsListController)
        .directive('trkProviderDomainsList', trkProviderDomainsList);
    
    function trkProviderDomainsList() {
            return {
                scope: {
                    items: '=',
                    parentId: '=parent',
                    readonly: '='
                },
                controller: 'trkProviderDomainsListController',
                templateUrl: 'app/providers/profile/services/languages/domains/trkProviderDomainsList.tpl.html'
            };
    }
    function trkProviderDomainsAddDialog($modal, domainsManager) {
        return {
            open: open
        };

        function open(model, config) {
            var modalInstance = $modal.open({
                templateUrl: 'app/providers/profile/services/languages/domains/add/trkDomainsAdd.tpl.html',
                controller: 'trkProviderDomainsAddDialogController',
                resolve: {
                    model: function () { return model; },
                    config: function () { return config; }
                }
            });

            return modalInstance.result;
        }
    }

    function trkProviderDomainUpdateDialog($modal, domainsManager) {
        return {
            open: open
        };

        function open(model, config) {
            var modalInstance = $modal.open({
                templateUrl: 'app/providers/profile/services/languages/domains/update/trkDomainUpdate.tpl.html',
                controller: 'trkProviderDomainUpdateDialogController',
                resolve: {
                    model: function () { return model; },
                    config: function () { return config; }
                }
            });

            return modalInstance.result;
        }
    }
    
    function trkProviderDomainUpdateDialogController($scope, $modalInstance, domainsManager, model, config) {

        $scope.model = model;
        $scope.domainName = config.domainName;

        $scope.ok = ok;
        $scope.cancel = cancel;

        function ok() {
            domainsManager.update($scope.model)
            .success($modalInstance.close)
            .error(displayError);

            function displayError(error) {
                $scope.errorMessage = error || 'Ошибка при сохранении данных.';
            }
        }
        function cancel() {
            $modalInstance.dismiss();
        }

    }

    function trkProviderDomainsAddDialogController($scope, $modalInstance, domainsManager, model, config) {
        
        $scope.model = model; 
        $scope.existingDomains = config.exclude;

        $scope.ok = ok;
        $scope.cancel = cancel;

        function ok() {
            domainsManager.add($scope.model)
            .success($modalInstance.close)
            .error(displayError);

            function displayError(error) {
                $scope.errorMessage = error || 'Ошибка при сохранении данных.';
            }
        }
        function cancel() {
            $modalInstance.dismiss();
        }

    }

    function trkProviderDomainsListController($scope, $http, confirmer, $modal, GlobalsService, _, domainsManager, trkProviderDomainsAddDialog, trkProviderDomainUpdateDialog) {
        // TODO: show all domains if their number is below limit
        // glitch with toggle domain list
        var VIEW_LIMIT = 10;

        $scope.limit = null;
        $scope.limited = false;
        $scope.setLimit = setLimit;
        $scope.isLimitToggleable = isLimitToggleable;
        $scope.remove = remove;
        $scope.addDomains = addDomains;
        $scope.edit = edit;

        setLimit();
        
        function remove(item) {
            if (confirmer.yes('Вы действительно хотите удалить тематику "' + item.domainName + '"?')) {
                domainsManager.remove(item.id)
                .success(updateView, displayError);
                
                function updateView() {
                    $scope.items = _.without($scope.items, item);
                    toastr.success('Тематика "' + item.domainName + '" удалена');
                }

                function displayError() {
                    toastr.error('Ошибка при удалении тематики');
                }
            }
        };

        function addDomains() {
            var config = {
                exclude: $scope.items
            },
                model = domainsManager.create({ languagePairId: $scope.parentId });

            trkProviderDomainsAddDialog
                .open(model, config)
                .then(updateView);

            function updateView(newDomains) {
                $scope.items = $scope.items.concat(newDomains);
                toastr.success('Новые тематики добавлены');
            }
        };

        function edit(item) {
            var config = {
                domainName: item.domainName
            },
            model = {
                id: item.id,
                languagePairId: $scope.parentId,
                stars: item.stars,
                comment: item.comment
            };

            trkProviderDomainUpdateDialog
                .open(model, config)
                .then(updateView);

            function updateView(data) {
                toastr.success('Данные о тематике "' + item.domainName + '" обновлены');
                var index = $scope.items.indexOf(item);
                $scope.items[index] = data;
                $scope.$apply();
            }

        }

        //visualiter
        function isLimitToggleable() {
            return VIEW_LIMIT > $scope.items.length;
        }

        function setLimit() {
            if ($scope.limited) {
                $scope.limit = $scope.items.length;
                $scope.limitBtnText = 'Показать все';
                $scope.limitText = 'Топ 10';
            }
            else {
                $scope.limit = VIEW_LIMIT;
                $scope.limitBtnText = 'Топ 10';
                $scope.limitText = 'Все ' + $scope.items.length;
            }
            $scope.limited = !$scope.limited;
        };

    }
    
    function domainsManager($http, _) {
        return {
            create: create,
            add: add,
            update: update,
            remove: remove
        };

        function create(config) {
            var template = {
                domainIds: [],
                stars: 2,
                comment: ''
            };
            return _.extend(template, config);
        }

        function add(model) {
            return $http.post('/api/providerLanguage/' + model.languagePairId + '/domains', model);
        }

        function remove(id) {
            return $http.delete('/api/providerDomain', { params: { id: id } });
        }

        function update(model) {
            return $http.put('/api/providerDomain/' + model.id, model);
        }
    }
})();


angular.module("app")
    .factory("providers", ($resource) => {

        var Provider = $resource("/api/provider/:id", { id: "@id" });

        return {
            "delete": deleteProvider
        }

        function deleteProvider(id) {
            return Provider.delete({ id: id }).$promise;
        }
    })
    .factory("providerFilterCache", (LocalStorageService, notifyClient) => {
        var MYFILTERSETTINGS_KEY = "my_filter_settings";
        return {
            save,
            load,
            clear
        };

        function save(settings) {
            LocalStorageService.set(MYFILTERSETTINGS_KEY, settings);
            notifyClient.info("Настройки фильтра сохранены.");
        }

        function load() {
            var settings = LocalStorageService.get(MYFILTERSETTINGS_KEY) || {};
            notifyClient.info("Настройки фильтра восстановлены.");
            return settings;
        }

        function clear() {
            LocalStorageService.remove(MYFILTERSETTINGS_KEY);
        }
    })
    .controller("providersCtrl", function($q, $scope, $timeout, providers, providerFilterCache, $http, ngTableParams, $state, $modal, notifyClient, handleHttpError,
        GlobalsService, EmploymentService, FreelanceService, ProviderTypeService, AvailabilityService, ActionAuthorizationService) {

        // what is this doing?
        $scope.test = [];

        initState();

        $scope.toggleAdditionalFilter = toggleAdditionalFilter;
        $scope.toggleFilters = toggleFilters;
        $scope.deleteItem = deleteItem;
        $scope.clearSelection = clearSelection;
        // deprecated?
        $scope.setIncludeInhouse = setIncludeInhouse;

        $scope.saveFilterSettings = saveFilterSettings;
        // fn name collision!
        $scope.loadFilterSettings = loadFilterSettings_;
        // fn name collision!
        $scope.clearFilterSettings = clearFilterSettings_;

        $scope.select = select;
        $scope.reloadTable = reloadTable;
        $scope.changeFreelance = changeFreelance;
        $scope.changeEmployment = changeEmployment;
        $scope.changeType = changeType;
        $scope.isEmployee = isEmployee;
        $scope.showAvailabilityCalendar = showAvailabilityCalendar;


        function initState() {
            $scope.filtersVisible = true;
            $scope.showAdditionalFilter = false;
            $scope.selectedItemId = null;
            $scope.readonly = !ActionAuthorizationService.isHR();
            var types = GlobalsService.get("providerTypes"),
                tripleChoice = GlobalsService.get("tripleChoice");

            $scope.lists = {
                providerTypes: types,
                tripleChoice: tripleChoice
            };

            $scope.data = {
                employee: types[0], // ATTN : MAGIC NUMBER
                freelancer: types[1], // ATTN : MAGIC NUMBER
                contractor: types[2] // ATTN : MAGIC NUMBER
            };

            $scope.includeInhouse = true; // filter contains inHouse     
        }

        function clearSelection() {
            if ($scope.selectedItemId === item.id) {
                $scope.select(null);
                if ($state.current.name === "hr.employees.profile")
                    $state.go($state.current, { id: null }, { reload: true });
            }
        }

        function toggleAdditionalFilter() {
            $scope.showAdditionalFilter = !$scope.showAdditionalFilter;
        };


        function deleteItem(item) {
            if (confirm(`Вы действительно хотите удалить ${item.name}?`)) {
                providers.delete(id)
                    .then(
                        afterDelete,
                        handleHttpError(`Ошибка при удалении ${item.name}`)
                    );
            }

            function afterDelete() {
                notifyClient.success(`Данные о ${item.name} удалены`);
                $scope.reloadTable();
                clearSelection();
            }
        }


        function toggleFilters(value) {
            $scope.filtersVisible = !value;
        }


        // deprecated?
        function setIncludeInhouse(values) {
            $scope.includeInhouse = Array.prototype.indexOf.call(value, 0) >= 0;
        }

        function saveFilterSettings() {
            providerFilterCache.save($scope.tableParams.$params.filter);
        }

        function loadFilterSettings() {
            $scope.tableParams.$params.filter = providerFilterCache.load();
        }

        var tableSettings = $state.current.data.tableDefaults || {};

        setTableParams();

        function loadProviderData($defer, params) {

            // TODO: this is an URL and I'm expecting smth else ... 
            Api.get(params.url(), function(data) {
                // update table params
                params.total(data.result.total);
                // set new data

                $scope.showRateAndQA = false;

                $scope.showRateAndQA = data.showRateAndQA;
                //$scope.$apply(); // otherwise there may be no update of UI 

                // toggle visibility of columns 
                var providerTypes = $scope.tableParams.$params.filter.providerTypes || [];

                // todo: simplify!!! 
                $scope.includeInhouse = providerTypes.indexOf(0) >= 0 || providerTypes.length == 0;

                if ($scope.tableParams.$params.filter && $scope.tableParams.$params.filter.name !== undefined)
                    $scope.toggleFilters($scope.tableParams.$params.filter.name);

                $defer.resolve(data.result.result);
            });
        }


        function setTableParams() {
            $scope.tableParams = $state.current.data.tableParams || new ngTableParams({
                page: tableSettings.page || 1, // show first page
                count: tableSettings.count || 12, // count per page
                sorting: tableSettings.sorting || {
                    name: "asc" // initial sorting
                },
                filter: tableSettings.filter || loadFilterSettings() || {}
            }, {
                total: tableSettings.total || 0, // length of data
                counts: [12, 24, 50, 100],
                getData: loadProviderData
            });
        }

        $state.current.data.tableParams = $scope.tableParams;


        function reloadTable() {
            $scope.tableParams.reload();
        };

        function select(itemId) {
            $scope.selectedItemId = itemId;
        }


        function changeFreelance(item) {
            FreelanceService.changeFreelance(item)
                .then(function(freelance) {
                    item.freelance = freelance;
                    $scope.tableParams.reload();
                });
        }


        function changeEmployment(item) {
            EmploymentService.changeEmployment(item)
                .then(function(employment) {
                    item.employment = employment;
                    $scope.tableParams.reload();
                });
        }

        function changeType(item, typeId) {
            ProviderTypeService
                .changeType(item, typeId)
                .then(reloadTable);
        }


        $scope.isEmployee = isEmployee;

        function isEmployee(item) {
            return item.typeId === 0 && item.employment;
        }


        function showAvailabilityCalendar(item) {
            AvailabilityService.getAvailability(item.id)
                .then(success, handleHttpError("Не удалось загрузить календарь"));

            function success(eventsource) {
                var modalInstance = $modal.open({
                    size: "lg",
                    resolve: {
                        eventsource: function() { return eventsource; },
                        providerId: function() { return item.id; },

                    },
                    controller: "ProviderAvailabilityCalendarController",
                    templateUrl: "app/providers/availability/availabilityCalendar/availabilityCalendar.dlg.tpl.html"
                });

                modalInstance.result.then(reloadTable, reloadTable);
            }
        }
    });

angular.module('app')
.controller('providersCtrl', function ($q, $scope, $timeout, $resource, $http, ngTableParams, $state, $modal,
    GlobalsService, LocalStorageService, EmploymentService, FreelanceService, ProviderTypeService, AvailabilityService, ActionAuthorizationService) {
    var Api = $resource('/api/provider/:id', { id: '@id' });

    $scope.filtersVisible = true;

    $scope.toggleAdditionalFilter = function () {
        $scope.showAdditionalFilter = !$scope.showAdditionalFilter;
    };

    $scope.deleteItem = function(item) {
        // TODO: make it good.
        console.log("trying to delete", item);
        Api.delete({ id: item.id }).$promise 
            .then(() => {
                setTableParams(); // TODO: report deletion to client
            }, (err) => {
                console.log("error deleting item");
                console.log(err);
            });
    }

    $scope.showAdditionalFilter = false;

    $scope.toggleFilters = function (value) {
        $scope.filtersVisible = !value;
    }

    $scope.selectedItemId = null;
    $scope.readonly = !ActionAuthorizationService.isHR();

    var types = GlobalsService.get('providerTypes'),
        tripleChoice = GlobalsService.get('tripleChoice');

    $scope.lists = {
        providerTypes: types,
        tripleChoice: tripleChoice
    };

    $scope.data = {
        employee: types[0],    // ATTN : MAGIC NUMBER
        freelancer: types[1],   // ATTN : MAGIC NUMBER
        contractor: types[2]    // ATTN : MAGIC NUMBER
    };

    $scope.includeInhouse = true; // filter contains inHouse 

    // deprecated?
    $scope.setIncludeInhouse = function (values) {
        $scope.includeInhouse = Array.prototype.indexOf.call(value, 0) >= 0;
    };
    // todo: make this personalized !!!! 
    var MYFILTERSETTINGS_KEY = 'my_filter_settinsg'

    $scope.saveFilterSettings = function () {
        var filterSettings = $scope.tableParams.$params.filter;
        LocalStorageService.set(MYFILTERSETTINGS_KEY, filterSettings);
        toastr.info('Настройки фильтра сохранены.');
    }

    $scope.loadFilterSettings = function () {
        var settings = LocalStorageService.get(MYFILTERSETTINGS_KEY);
        $scope.tableParams.$params.filter = settings || {};
        toastr.info('Настройки фильтра восстановлены.');
    }

    function loadFilterSettings() {
        var result = LocalStorageService.get(MYFILTERSETTINGS_KEY) || clearFilterSettings();
        return result;
    }

    var tableSettings = $state.current.data.tableDefaults || {};

    function clearFilterSettings() {
        LocalStorageService.remove(MYFILTERSETTINGS_KEY);
    }

    $scope.clearFilterSettings = function () {
        $scope.tableParams.filter({});

        toastr.info('Настройки фильтра сброшены. Совсем...');
    }

    $scope.test = [];

    setTableParams();

    function setTableParams() {
        $scope.tableParams = $state.current.data.tableParams || new ngTableParams({
            page: tableSettings.page || 1,            // show first page
            count: tableSettings.count || 12,          // count per page
            sorting: tableSettings.sorting || {
                name: 'asc'     // initial sorting
            },
            filter: tableSettings.filter || loadFilterSettings() || {}
        }, {
            total: tableSettings.total || 0,           // length of data
            counts: [12, 24, 50, 100],
            getData: function ($defer, params) {

                console.log(params.filter());
                console.log('params');
                console.log(params);
                console.log(params.count() + ' ' + params.page());

                //var requestParams = {
                //    count: params.count(),
                //    page: params.page(),
                //    filter: params.filter(),
                //    sorting : params.sorting() || { name : 'asc' }
                //};

                Api.get(params.url(), function (data) {
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
        });
    }

    $state.current.data.tableParams = $scope.tableParams;

    $scope.select = function (itemId) {
        $scope.selectedItemId = itemId;
    }
    $scope.reloadTable = function () {
        $scope.tableParams.reload();
    };

    $scope.changeFreelance = function (item) {
        FreelanceService.changeFreelance(item)
        .then(function (freelance) {
            item.freelance = freelance;
            $scope.tableParams.reload();
        })
    }

    $scope.changeEmployment = function (item) {
        EmploymentService.changeEmployment(item)
            .then(function (employment) {
                item.employment = employment;
                $scope.tableParams.reload();
            })
    }

    $scope.changeType = function (item, typeId) {
        ProviderTypeService
            .changeType(item, typeId)
            .then(reloadTable);
    }

    function reloadTable() {
        $scope.tableParams.reload();
    }

    $scope.isEmployee = function (item) {
        return item.typeId === 0 && item.employment;
    }

    $scope.showAvailabilityCalendar = function (item) {
        AvailabilityService.getAvailability(item.id)
            .then(success, failure);

        function success(eventsource) {
            var modalInstance = $modal.open({
                size: 'lg',
                resolve: {
                    eventsource: function () { return eventsource; },
                    providerId: function () { return item.id; },

                },
                controller: 'ProviderAvailabilityCalendarController',
                templateUrl: 'app/providers/availability/availabilityCalendar/availabilityCalendar.dlg.tpl.html'
            });

            modalInstance.result.then(reloadTable, reloadTable);
        }

        function failure(data) {
            toastr.error('Я ошипко');
        }
    }
});

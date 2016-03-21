angular.module('app')
.controller('accountsCtrl', function ($q, $scope, $timeout, $resource, $http, ngTableParams, $state, $modal, GlobalsService) {
    var Api = $resource('/api/account/search');
    $scope.lists = {
        providerTypes: GlobalsService.get('providerTypes'),
        offices: GlobalsService.get('offices'),
        titles: GlobalsService.get('titles'),
        employmentStatuses: GlobalsService.get('employmentStatuses'),
        freelanceStatuses: GlobalsService.get('freelanceStatuses')
    };
        
    $scope.lockAccount = function (item) {
        setAccountDisabled(item, true);
    }

    $scope.unlockAccount = function (item) {
        setAccountDisabled(item, false)
    };

    function setAccountDisabled(item, val) {
        var id = item.account.accountId;

        // blockAccount.html"
        var modalInstance = $modal.open({
            templateUrl: 'blockAccount.html',
            controller: 'accountBlockCtrl',
            resolve: {
                item: function () { return item; },
                val: function () { return val; }
            }
        });

        modalInstance.result.then(function () {

            $http.put('/api/account/' + id + '/disable?val=' + val)
                .then(function success() {
                    item.account.accountDisabled = val;
                    toastr.info('Учетная запись ' + (val ? 'за' : 'раз') + 'блокирована.');
                }, function fail(data) { toastr.error('Ошибка при изменении статуса аккаунта: ' + JSON.stringify(data)); });
        },

            function fail(err) {
                toastr.error('Ошибка при изменении статуса акканута: ' + JSON.stringify(err));
            })
    };

    $scope.removeAccount = function (item) {

        console.log('remove account');
        console.log(item);
        //var modalInstance = $modal.open({
        //    templateUrl: 'removeAccount.html',
        //    controller: 'accountRemoveCtrl',
        //    resolve: { item: function () { return item; } }
        //});
        
        console.log('deleting account id = ' + item.account.accountId);

        $http.delete('/api/account/' + item.account.accountId)
        .then(function success() {
            item.account = null;
            item.hasAccount = false;
            toastr.info('Аккаунт удален');
        },
        function fail(err) {
            toastr.error('Ошибка при удалении акканута: ' + JSON.stringify(err));
        });

        //modalInstance.result.then(function () {
        //    console.log('deleting account id = ' + item.account.accountId);

        //    $http.delete('/api/account/' + item.account.accountId)
        //    .then(function success() {
        //        item.account = null;
        //        item.hasAccount = false;
        //        toastr.info('Аккаунт удален');
        //    },
        //    function fail(err) {
        //        toastr.error('Ошибка при удалении акканута: ' + JSON.stringify(err));
        //    });


        //}, function () {
        //    // do nothing 
        //});
    };

    var tableSettings = $state.current.data.tableDefaults || {};

    $scope.tableParams = $state.current.data.tableParams || new ngTableParams({
        page: tableSettings.page || 1,            // show first page
        count: tableSettings.count || 12,          // count per page
        sorting: tableSettings.sorting || {
            name: 'asc'     // initial sorting
        }
    }, {
        total: tableSettings.total || 0,           // length of data
        counts: [12, 24, 50, 100],
        getData: function ($defer, params) {
            // ajax request to api
            console.log('params.filter()');

            console.log(params.filter());
            console.log('params');
            console.log(params);
            console.log(params.count() + ' ' + params.page());

            var requestParams = {
                count: params.count(),
                page: params.page(),
                filter: params.filter()
            };

            Api.get(params.url(), function (data) {
                $timeout(function () {
                    // update table params
                    params.total(data.total);
                    // set new data
                    $defer.resolve(data.result);
                }, 500);
            });
        }
    });
    $state.current.data.tableParams = $scope.tableParams;
});
(function () {
    angular.module('orders', [
        'order.job',
        'order.data',
        'order.create',
        'order.list',
        'globals',
        'orders.utilities',
        'ui.router'])

    .config(['$stateProvider', function ($stateProvider) {

        $stateProvider.state('orders', {
            abstract: true,
            url: '/orders',
            template: '<ui-view></ui-view>',
            data: { auth: { noLogin: true } }
        });

        $stateProvider.state('orders.create', {
            url: '/orders/create',
            templateUrl: 'app/orders/create/order.create.tpl.html',
            controller: 'newOrderController',
            params: {
                template: {}
            },
            // TODO: make sure it's restricted to certain roles (which roles?) 
            data: { auth: { noLogin: true } }
        });

        $stateProvider.state('orders.list', {
            url: '/orders/list',
            templateUrl: 'app/orders/list/orders.list.tpl.html',
            controller: 'orderListController',
            data: { auth: { noLogin: true } }
        });
    }]);

})();
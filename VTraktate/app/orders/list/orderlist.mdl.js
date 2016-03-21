(function () {
    angular.module('order.list', [
        'constants',
        'ui.bootstrap',
        'ui.grid',
        
        'ui.grid.treeView',
        'ui.grid.pagination',
        'ui.grid.resizeColumns',
        'ui.grid.moveColumns',
        'nsPopover',
        'orderTemplate',
        'infrastructure'
    ]);
})();
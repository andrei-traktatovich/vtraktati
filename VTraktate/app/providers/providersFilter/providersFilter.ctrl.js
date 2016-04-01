(() => {
    angular.module("providers")
        .controller("providersFilterCtrl", ($scope, providersFilterCache) => {

            $scope.filtersVisible = true;

            $scope.toggleFilters = toggleFilters;
            $scope.save = save;
            $scope.load = load;
            $scope.clear = clear;
            
            function save() {
                providersFilterCache.save($scope.filters);
            }

            function load() {
                $scope.filter = providersFilterCache.load();
            }

            function clear() {
                $scope.filters = {};
            }

            function toggleFilters() {
                $scope.filtersVisible = !$scope.filters.name;
            }

        });
})();
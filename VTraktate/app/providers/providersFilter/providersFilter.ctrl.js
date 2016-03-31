(() => {
    angular.module("providers")
        .controller("providersFilterCtrl", ($scope, providersFilterCache) => {

            // filter is stored in $scope.filters 
            // implicit: $scope.applyFilter() -- func to call when search button clicked 

            $scope.showAdditionalFilter = true;
            $scope.filtersVisible = true;

            $scope.toggleAdditionalFilter = toggleAdditionalFilter;
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

            function toggleFilters(value) {
                $scope.filtersVisible = !value;
            }

            function toggleAdditionalFilter() {
                $scope.showAdditionalFilter = !$scope.showAdditionalFilter;
            };
        });
})();
(() => {
    angular.module("providers")
        .factory("Providers", providers);

    // TODO: how do I get rid of intermindled $resource & $http? 
    function providers($resource, $http) {

        const provider = $resource("/api/provider/:id", { id: "@id" });

        return {
            "delete": deleteProvider,
            query: query
        }
        
        function deleteProvider(id) {
            return provider.delete({ id: id }).$promise;
        }

        function query(queryString) {
            return provider.get(queryString).$promise;
        }
    }

})();

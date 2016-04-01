(() => {

    angular.module("providers")
    .factory("people", people);

    var makeNameUrl = (personId) => `api/people/${personId}/fullname`;

    function people($http) {
        return {
            getFullName,
            setFullName
            };

        function getFullName(personId) {
            return $http.get(makeNameUrl(personId));
        }

        function setFullName(personId, name) {
            return $http.put(makeNameUrl(personId), name);
        }
    }

})();
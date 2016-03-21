(function () {
    angular.module('customerContactPerson')
    .controller('trkCustomerContactPersonGeneral', trkCustomerContactPersonGeneral);

    function trkCustomerContactPersonGeneral($scope, $http) {

        initState();

        $scope.beginEdit = function () {
            $scope.editing = true;
            $scope.editModel = copyModel($scope.contactPerson);
        };

        $scope.cancel = initState;

        $scope.save = function () {
            $http.post(url, $scope.editModel).success(function () {
                $scope.contactPerson = copyModel($scope.editModel);
                initState();
            })
        };

        function initState() {
            $scope.editing = false;
            $scope.editModel = null;
        }

        function makeModel(cp) {
            var result = {
                personName: angular.copy(cp.personName),
                personOfficialInfo: angular.copy(cp.personOfficialInfo),
                birthDate: cp.birthDate
            };
            return result;
        }
    }

})();
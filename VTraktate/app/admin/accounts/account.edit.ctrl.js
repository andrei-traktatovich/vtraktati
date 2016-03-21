angular.module('app')
.controller('accountEditCtrl', function ($http, $q, $scope, $state, AccountNamePreview, GlobalsService) {
    $scope.roles = GlobalsService.get('roles');
    console.log("roles are:", roles);
    $scope.service = AccountNamePreview;
    $scope.creatingAccount = false;
    $scope.changingPassword = false;
    $scope.account = $state.params.account;
    // ATTN : I am passing roles as associative array, and this is causing problem here, the control won't bind to it because doesn't recognize as array 
    $scope.account.roles = Object.keys($scope.account.roles).map(function (key) { return $scope.account.roles[key] });
    $scope.oldName = $scope.account.name;

    $scope.personName = $state.params.personName;
    $scope.enablePasswordChange = function () {
        var val = !$scope.changingPassword;
        $scope.changingPassword = val;
        if (!val) {
            $scope.account.password = null;
            $scope.account.confirmPassword = null;
        }
    }
    $scope.submit = function () {
        var url = '/api/account';

        $http.put(url, $scope.account)
        .then(function success() {
            $state.go('admin.account');
        },
        function fail(data) {
            $scope.error = data;
        });



    };
});
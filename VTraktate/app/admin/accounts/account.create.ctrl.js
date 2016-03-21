angular.module('app')
.controller('accountCreateCtrl', function ($http, $q, $scope, $state, AccountNamePreview, GlobalsService) {

    // TODO: If I am the user, I need to reread my roles !!! (?)
    $scope.account = {
        roles: [],
        personId: $state.params.personId
    };
    $scope.roles = GlobalsService.get('roles');

    $scope.service = AccountNamePreview;

    $scope.creatingAccount = true;
    
    $scope.personName = $state.params.personName;
    $scope.editingUserName = false;

    function getErrorFromResponse(data) {
        var str = '(' + data.status + ') ' + (data.data.message || 'Неизвестная ошибка') + ': ' || '', 
            ms = data.data.modelState;
        if (ms) {
            var values = [];
            for(var key in ms)
                if (ms.hasOwnProperty(key)) {
                    var val = ms[key];
                    if (angular.isArray(val)) {
                        val.forEach(function(item) { values.push(item); });
                    }
                    else 
                        values.push(ms[key]);
                }
            str = str + values.join(', ');
        }
        return str;
    }


    $scope.submit = function () {

        var url = '/api/account';
        console.log('submitting account');
        console.log($scope.account);

        $http.post(url, $scope.account)
        .then(function success() {
            $state.go('admin.account');
        },
        function fail(data) {
            $scope.error = getErrorFromResponse(data);
        });
    };
});